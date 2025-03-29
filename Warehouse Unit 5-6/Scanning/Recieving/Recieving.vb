Imports MySql.Data.MySqlClient
Public Class Recieving
    Private Sub txtqr_TextChanged(sender As Object, e As EventArgs) Handles txtqr.TextChanged

    End Sub

    Private Sub txtqr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtqr.KeyDown
        If e.KeyCode = Keys.Enter Then
            If inserttoDB(txtqr.Text.Trim, txt_batch.Text.Trim) = True Then
                getGroup()
                displayrecords()
            End If
            txtqr.Clear()
            txtqr.Focus()
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
                               WHERE datein='" & Date.Now.ToString("yyyy-MM-dd") & "' AND batchin='" & txt_batch.Text.Trim & "' AND userin='" & user_IDno & "'
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
        reload("SELECT `id`,`qrcode`, `partcode`, `lotnumber`, `supplier`, `remarks`, `qty` FROM `logistics_u5` 
                WHERE datein='" & Date.Now.ToString("yyyy-MM-dd") & "' AND batchin='" & txt_batch.Text.Trim & "' AND userin='" & user_IDno & "'
                ORDER by id DESC", datagrid1)
    End Sub

    Private Sub txt_batch_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_batch.KeyDown
        If e.KeyCode = Keys.Enter Then
            getGroup()
            displayrecords()
        End If
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) 

    End Sub
End Class