Imports MySql.Data.MySqlClient

Public Class DeleteTransaction

    Private ReadOnly ConnectionString As String = "Server=PTI-032;Database=rfid_inventory;Uid=rfid;Pwd=rfid123;"

    Public Sub DeleteScanData(qrCode As String)

        Dim trans As MySqlTransaction = Nothing

        Try
            Using con As New MySqlConnection(ConnectionString)

                con.Open()
                trans = con.BeginTransaction()

                Using cmd As New MySqlCommand("
                    DELETE FROM parts_scan
                    WHERE QRcode = @QRcode;", con, trans)

                    cmd.Parameters.AddWithValue("@QRcode", qrCode)

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected = 0 Then
                        Throw New Exception("Transaction not found.")
                    End If

                End Using

                trans.Commit()

            End Using

        Catch ex As MySqlException

            If trans IsNot Nothing Then
                trans.Rollback()
            End If

            Throw New Exception("Database Error: " & ex.Message)

        Catch ex As Exception

            If trans IsNot Nothing Then
                trans.Rollback()
            End If

            Throw
        Finally
            con.Close()
        End Try

    End Sub

End Class