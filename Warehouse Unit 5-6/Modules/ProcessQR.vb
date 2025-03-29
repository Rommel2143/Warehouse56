Imports MySql.Data.MySqlClient
Module ProcessQR
    Dim QRpartcode As String
    Dim QRlotnumber As String
    Dim QRqty As Integer
    Dim QRremarks As String
    Dim QRsupplier As String

    Private Function ProcessQRcode(qrcode As String) As Boolean
        Try
            ' Split the QR code using the pipe character
            Dim parts() As String = qrcode.Split("|")

            ' Validate the number of parts in the QR code
            If parts.Length >= 5 AndAlso parts.Length <= 8 Then

                ' Assign values to module-level variables
                QRpartcode = parts(0).Remove(0, 2).Trim()
                QRsupplier = parts(1).Remove(0, 2).Trim()
                QRlotnumber = parts(2).Remove(0, 2).Trim()
                QRqty = Convert.ToInt32(parts(3).Remove(0, 2).Trim())
                QRremarks = parts(4).Remove(0, 2).Trim()

                Return True ' Indicate success
            Else
                ' Show an error if the QR code format is invalid
                show_error("Invalid QR format!.", 1)
                Return False ' Indicate failure
            End If
        Catch ex As Exception
            ' Handle unexpected errors gracefully
            show_error("Invalid QR format!", 1)
            Return False ' Indicate failure
        Finally
            ' Ensure the database connection is properly closed
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Function

    Private Sub cleardata()
        QRpartcode = ""
        QRlotnumber = ""
        QRqty = 0
        QRremarks = ""
        QRsupplier = ""
    End Sub

    Private Function CheckPartName(partcode As String) As Boolean
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT id FROM `logistics_masterlist` WHERE partcode = @partcode LIMIT 1"
            Using selectPartName As New MySqlCommand(query, con)
                selectPartName.Parameters.AddWithValue("@partcode", partcode)

                Using dr As MySqlDataReader = selectPartName.ExecuteReader()
                    If dr.HasRows = True Then
                        Return True
                    Else

                        show_error("Partcode Not Registered!", 1)
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception

            Return False
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function


    Public Function inserttoDB(qrcode As String, batch As String) As Boolean
        cleardata()
        Try
            If ProcessQRcode(qrcode) Then
                If CheckPartName(QRpartcode) Then
                    con.Close()
                    con.Open()
                    Dim query As String = "INSERT INTO `logistics_u5`(`id`, `status`, `qrcode`, `partcode`, `lotnumber`, 
                                                                `supplier`, `remarks`, `qty`, `boxno`, `datein`,
                                                                `userin`, `pcIN`, `batchin`, `timeIN`, `batchout`, 
                                                                `dateout`, `timeOUT`, `userout`, `pcOUT`)
                                      VALUES (NULL, 1, @qrcode, @partcode, @lotnumber, 
                                              @supplier, @remarks, @qty, '', CURDATE(), 
                                              @userin, @pcIN, @batchin, CURTIME(), '', NULL, NULL, '', '')"

                    Dim insert As New MySqlCommand(query, con)
                    insert.Parameters.AddWithValue("@qrcode", qrcode)
                    insert.Parameters.AddWithValue("@partcode", QRpartcode)
                    insert.Parameters.AddWithValue("@lotnumber", QRlotnumber)
                    insert.Parameters.AddWithValue("@supplier", QRsupplier)
                    insert.Parameters.AddWithValue("@remarks", QRremarks)
                    insert.Parameters.AddWithValue("@qty", QRqty)
                    insert.Parameters.AddWithValue("@userin", user_IDno)
                    insert.Parameters.AddWithValue("@pcIN", user_pc)
                    insert.Parameters.AddWithValue("@batchin", batch)

                    insert.ExecuteNonQuery()
                    Return True ' Insert successful
                End If
            End If
        Catch ex As MySqlException When ex.Number = 1062
            show_error("Duplicate entry detected", 2)
            Return False
        Catch ex As Exception
            show_error($"Error on saving: {ex.Message}", 1)
            Return False
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
        Return False
    End Function



    Public Function outQR(QRcode As String, batch As String, boxno As String) As Boolean
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT status FROM `logistics_u5` WHERE qrcode = @qrcode LIMIT 1"
            Using selectQR As New MySqlCommand(query, con)
                selectQR.Parameters.AddWithValue("@qrcode", QRcode)
                dr = selectQR.ExecuteReader
                If dr.Read = True Then

                    If dr.GetBoolean(0) = True Then
                        updateOUT(QRcode, batch, boxno)
                        Return 1
                    Else
                        show_error("QR status is already OUT", 2)
                        Return 0
                    End If

                Else

                    show_error("QR not found!", 1)

                    Return 0
                End If

            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return 0

        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function

    Public Sub updateOUT(qrcode As String, batch As String, boxno As String)

        Try

            con.Close()
            con.Open()
            Dim query As String = "UPDATE logistics_U5 SET boxno=@boxno,status=0,`batchout`=@batch,`dateout`=CURDATE(),timeOUT=CURTIME(),`userout`='" & user_IDno & "',`pcOUT`='" & user_pc & "'
                                    WHERE qrcode=@qrcode"

            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@qrcode", qrcode)
            cmd.Parameters.AddWithValue("@batch", batch)
            cmd.Parameters.AddWithValue("@boxno", boxno)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            show_error($"Error on updating: {ex.Message}", 1)

        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub



End Module
