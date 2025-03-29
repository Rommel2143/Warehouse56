
Imports MySql.Data.MySqlClient
Public Class Delivery
    Private Sub txtqr_TextChanged(sender As Object, e As EventArgs) Handles txtqr.TextChanged

    End Sub

    Private Sub txtqr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr.KeyDown
        If e.KeyCode = Keys.Enter Then

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



            Dim query As String = "SELECT lm.partname,lu.partcode,SUM(lu.qty) AS qty, COUNT(lu.id) AS count from logistics_u5 lu
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
        reload("SELECT `id`,`qrcode`, `partcode`, `lotnumber`, `supplier`, `remarks`, `qty`,boxno FROM `logistics_u5` 
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
        If e.KeyCode = Keys.Enter Then
            If outQR(txtqr.Text.Trim, txt_batch.Text.Trim, txt_boxno.Text.Trim) = True Then
                getGroup()
                displayrecords()
            End If
            txt_boxno.Clear()
            txtqr.Clear()
            txtqr.Focus()
        End If
    End Sub
End Class