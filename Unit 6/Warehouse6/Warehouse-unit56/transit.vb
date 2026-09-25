Public Class transit
    Private Sub transit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reload("SELECT `batchcode`, `partcode`,SUM( `qty`) AS QTY, `date_transit`, `scan_from` AS 'From', `scan_to` AS 'Destination' FROM `molding_stock_intransit` WHERE status =1 AND (scan_from='Sunbo' OR scan_to='Sunbo') GROUP BY batchcode,date_transit,partcode ", datagrid1)
    End Sub
End Class