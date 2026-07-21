Imports MySql.Data.MySqlClient

Public Class CreateTransaction

    Private ReadOnly ConnectionString As String = "Server=localhost;Database=rfid_inventory;Uid=root;Pwd=;"
    '"Server=PTI-032;Database=rfid_inventory;Uid=rfid;Pwd=rfid123;"


    Public Sub SaveScanData(rfidbarcode As String, qrlot As String, batch As String, dn As String)

        Dim trans As MySqlTransaction = Nothing

        Try
            'check RFID structure
            If String.IsNullOrWhiteSpace(rfidbarcode) Or rfidbarcode.Contains("|") Then
                Throw New Exception("Invalid RFID tag!")
            End If

            If Not qrlot.Contains("|") Then
                Throw New Exception("Invalid QR detected!")
            End If

            ' Parse QR
            Dim qrResult = QRParser.ParseQR(qrlot)

            If Not qrResult.HasValue Then
                Throw New Exception("Invalid QR code structure!")
            End If

            Dim qr = qrResult.Value

            Using con As New MySqlConnection(ConnectionString)

                con.Open()

                trans = con.BeginTransaction()

                Using cmd As New MySqlCommand()

                    cmd.Connection = con
                    cmd.Transaction = trans

                    cmd.CommandText =
                            "INSERT INTO parts_scan
                            (QRcode, rfidbarcode, partcode, lotnumber, batchcode, qty, status, rssi, antenna, suppliercode,warehouseId, remarks, dn)
                            VALUES
                            (@QRcode, @RFIDbarcode, @PartCode, @LotNumber, @Batch, @Qty, @status, 0, 0, @suppliercode,@warehouseId, @remarks, @dn);

                            UPDATE parts_scan
                            SET rfidbarcode = '', status = 'WIP'
                            WHERE rfidbarcode = @RFIDbarcode
                            AND QRcode <> @QRcode;"

                    cmd.Parameters.AddWithValue("@QRcode", qrlot)
                    cmd.Parameters.AddWithValue("@RFIDbarcode", rfidbarcode)
                    cmd.Parameters.AddWithValue("@suppliercode", qr.Supplier)
                    cmd.Parameters.AddWithValue("@PartCode", qr.PartCode)
                    cmd.Parameters.AddWithValue("@LotNumber", qr.LotNumber)
                    cmd.Parameters.AddWithValue("@remarks", qr.Remarks)
                    cmd.Parameters.AddWithValue("@warehouseId", "5")
                    cmd.Parameters.AddWithValue("@Batch", batch)
                    cmd.Parameters.AddWithValue("@Qty", qr.Qty)
                    cmd.Parameters.AddWithValue("@status", "PAIR")
                    cmd.Parameters.AddWithValue("@dn", dn)

                    cmd.ExecuteNonQuery()

                End Using

                trans.Commit()

            End Using

        Catch ex As MySqlException

            If trans IsNot Nothing Then
                trans.Rollback()
            End If

            If ex.Number = 1062 Then
                Throw New Exception("Duplicate detected!")
            Else
                Throw New Exception("Database Error: " & ex.Message)
            End If

        Catch ex As Exception

            If trans IsNot Nothing Then
                trans.Rollback()
            End If

            Throw

        End Try

    End Sub

End Class