
Imports MySql.Data.MySqlClient
Public Class Delivery
    Private Sub txtqr_TextChanged(sender As Object, e As EventArgs) Handles txtqr.TextChanged

    End Sub

    Private Sub txtqr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_boxno.Clear()
            txt_boxno.Enabled = True
            txt_boxno.Focus()
        End If
    End Sub

    Private Sub txt_batch_TextChanged(sender As Object, e As EventArgs) Handles txt_batch.TextChanged
        If txt_batch.Text.Trim = "" Then
            txtqr.Enabled = False
        Else
            txtqr.Enabled = True
        End If
    End Sub

    Private Sub getGroup()
        Try



            Dim query As String = "SELECT lm.partname,lu.partcode,SUM(lu.qty) AS qty, COUNT(lu.id) AS count from logistics_unit56 lu
                                JOIN logistics_masterlist lm ON lm.partcode=lu.partcode
                               WHERE dateout='" & Date.Now.ToString("yyyy-MM-dd") & "' AND batchout='" & txt_batch.Text.Trim & "' AND userout='" & user_IDno & "'
                                GROUP BY lu.partcode"
            Dim cmd As New MySqlCommand(query, con)
            con.Close()
            con.Open()
            dr = cmd.ExecuteReader
            flow1.Controls.Clear()

            While dr.Read = True
                addobject_group(flow1, dr.GetString("partname"), dr.GetString("partcode"), dr.GetInt32("qty"), dr.GetInt32("count"))
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub displayrecords()
        reload("SELECT `id`,`qrcode`, `partcode`, `lotnumber`, `supplier`, `remarks`, `qty`,boxno FROM `logistics_unit56` 
                 WHERE dateout='" & Date.Now.ToString("yyyy-MM-dd") & "' AND batchout='" & txt_batch.Text.Trim & "' AND userout='" & user_IDno & "'", datagrid1)
    End Sub

    Private Sub txt_batch_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_batch.KeyDown
        If e.KeyCode = Keys.Enter Then
            getGroup()
            displayrecords()
        End If
    End Sub

    Private Sub txt_boxno_TextChanged(sender As Object, e As EventArgs) Handles txt_boxno.TextChanged

    End Sub

    Private Sub txt_boxno_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_boxno.KeyDown

        If e.KeyCode <> Keys.Enter Then
            Return
        End If

        Try
            Dim rfid As String = txtRfid.Text.Trim()
            Dim qr As String = txtqr.Text.Trim()
            Dim batch As String = txt_batch.Text.Trim()
            Dim boxNo As String = txt_boxno.Text.Trim()

            ' Check RFID structure
            If String.IsNullOrWhiteSpace(rfid) OrElse
           rfid.Contains("|") OrElse
           Not rfid.StartsWith("E280116", StringComparison.OrdinalIgnoreCase) AndAlso
           Not rfid.StartsWith("800304", StringComparison.OrdinalIgnoreCase) Then

                Throw New Exception("Invalid RFID tag!")
            End If

            ' Check QR structure
            If Not qr.Contains("|") Then
                Throw New Exception("Invalid QR detected!")
            End If

            ' Parse QR
            Dim qrResult = QRParser.ParseQR(qr)

            If Not qrResult.HasValue Then
                Throw New Exception("Invalid QR code structure!")
            End If

            ' Process QR
            If outQR(qr, batch, boxNo) = True Then

                If cmbDestination.Text = "Factory 2" Then

                    Dim transaction As New CreateTransaction()

                    transaction.SaveScanData(
                    rfid,
                    qr,
                    batch,
                    "",
                    qrResult
                )

                End If

                getGroup()
                displayrecords()

            End If



            ' Prevent Enter from triggering other controls
            e.SuppressKeyPress = True
            e.Handled = True

        Catch ex As Exception

            show_error(ex.Message, 1)
        Finally
            ' Clear for next scan
            txt_boxno.Clear()
            txtRfid.Clear()
            txtqr.Clear()
            txtRfid.Focus()
        End Try

    End Sub


    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub txtRfid_TextChanged(sender As Object, e As EventArgs) Handles txtRfid.TextChanged

    End Sub

    Private Sub txtRfid_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRfid.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtqr.Clear()
            txtqr.Enabled = True
            txtqr.Focus()
        End If
    End Sub
End Class