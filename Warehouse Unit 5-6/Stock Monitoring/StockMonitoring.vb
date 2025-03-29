Public Class StockMonitoring
    Private Sub StockMonitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reload("SELECT partcode, SUM(qty) FROM logistics_u5 WHERE status = 1 GROUP BY partcode DESC", datagrid1)
    End Sub
End Class