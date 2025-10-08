Imports MySql.Data.MySqlClient
Public Class update_proddate
    Private Sub update_proddate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reload("SELECT `id`,`lotnumber`, `prod_date` FROM `logistics_unit56` WHERE prod_date IS NULL", datagrid1)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        For Each row As DataGridViewRow In datagrid1.Rows
            If row.IsNewRow Then Continue For

            Dim id As String = row.Cells("id").Value.ToString()
            Dim lotnumber As String = row.Cells("lotnumber").Value.ToString()
            Dim prodDate As String = getProddate(lotnumber)

            Try
                    con.Open()
                Dim cmd As New MySqlCommand("UPDATE logistics_unit56 SET prod_date = @date WHERE id = @id", con)
                cmd.Parameters.AddWithValue("@date", prodDate)
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.ExecuteNonQuery()
                    con.Close()
                Catch ex As Exception
                    con.Close()
                    MessageBox.Show("Error updating ID " & id & ": " & ex.Message)
                End Try

        Next

        MessageBox.Show("Production dates updated successfully!")
        reload("SELECT `id`,`lotnumber`, `prod_date` FROM `logistics_unit56` WHERE prod_date IS NULL", datagrid1)
    End Sub

End Class