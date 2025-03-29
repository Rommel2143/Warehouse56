

Imports MySql.Data.MySqlClient
Public Class Delivery_view
    Private Sub Delivery_view_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpicker1.Value = Date.Now
    End Sub
    Private Sub getGroup()
        Try



            Dim query As String = "SELECT lm.partname,lu.partcode,SUM(lu.qty) AS qty, COUNT(lu.id) AS count from logistics_u5 lu
                                JOIN logistics_masterlist lm ON lm.partcode=lu.partcode
                                JOIN logistics_user l ON l.IDno = lu.userout
                               WHERE dateout='" & dtpicker1.Value.ToString("yyyy-MM-dd") & "' AND batchout='" & cmb_batch.Text & "' AND CONCAT(l.Firstname, ' ', l.Lastname)='" & cmb_user.Text & "'
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
        reload("SELECT logistics_u5.id,`qrcode`, `partcode`, `lotnumber`, `supplier`, `remarks`, `qty`,dateout,`timeOUT` FROM `logistics_u5` 
                 JOIN logistics_user lu ON lu.IDno = logistics_u5.userout
                 WHERE dateout='" & dtpicker1.Value.ToString("yyyy-MM-dd") & "' AND batchout='" & cmb_batch.Text & "' AND CONCAT(lu.Firstname, ' ', lu.Lastname)='" & cmb_user.Text & "'", datagrid1)
    End Sub



    Private Sub Guna2DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles dtpicker1.ValueChanged
        Dim query As String = "SELECT CONCAT(lu.Firstname, ' ', lu.Lastname) AS Fullname 
                                 FROM logistics_u5 l
                                 JOIN logistics_user lu ON lu.IDno = l.userout
                                 WHERE dateout = '" & dtpicker1.Value.ToString("yyyy-MM-dd") & "'
                                  GROUP BY CONCAT(lu.Firstname, ' ', lu.Lastname)"


        cmb_display(query, cmb_user)
        cmb_batch.Items.Clear()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub cmb_user_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_user.SelectedIndexChanged
        Dim query As String = "SELECT batchout 
                                 FROM logistics_u5 l
                                 JOIN logistics_user lu ON lu.IDno = l.userout
                                 WHERE CONCAT(lu.Firstname, ' ', lu.Lastname)='" & cmb_user.Text & "' AND dateout = '" & dtpicker1.Value.ToString("yyyy-MM-dd") & "'
                                  GROUP BY batchout"


        cmb_display(query, cmb_batch)
    End Sub

    Private Sub cmb_batch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_batch.SelectedIndexChanged
        getGroup()
        displayrecords()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        exporttoExcel(datagrid1)
    End Sub


End Class