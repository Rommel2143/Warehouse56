Public Class StockMonitoring
    Private Sub StockMonitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadall()
    End Sub

    Private Sub loadall()
        reload("SELECT lu.partcode AS Partcode,lm.partname, SUM(lu.qty) AS 'TOTAL' FROM logistics_u5 lu
JOIN logistics_masterlist lm ON lu.partcode=lm.partcode

WHERE lu.status = 1 GROUP BY lu.partcode DESC", datagrid1)
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        If Guna2TextBox1.Text = "" Then
            loadall()
        Else
            reload("SELECT lu.partcode AS Partcode,lm.partname, SUM(lu.qty) AS 'TOTAL' FROM logistics_u5 lu
JOIN logistics_masterlist lm ON lu.partcode=lm.partcode

WHERE lu.status = 1 AND lu.partcode REGEXP '" & Guna2TextBox1.Text & "' 
GROUP BY lu.partcode DESC", datagrid1)
        End If
    End Sub
End Class