Imports MySql.Data.MySqlClient
Public Class subframe
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btn_menu.Click
        If btn_menu.ContextMenuStrip IsNot Nothing Then
            btn_menu.ContextMenuStrip.Show(btn_menu, 0, btn_menu.Height)

        End If
    End Sub

    Private Sub btn_profile_Click(sender As Object, e As EventArgs) Handles btn_profile.Click
        btn_user.Text = "Hi, " & user_Username
        btn_administrator.Visible = isAdmin()

        If btn_profile.ContextMenuStrip IsNot Nothing Then
            btn_profile.ContextMenuStrip.Show(btn_profile, 0, btn_profile.Height)

        End If
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        display_inMain(Login)
        Me.Close()
    End Sub

    Private Sub RecievingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecievingToolStripMenuItem.Click
        display_inSub(Recieving)
    End Sub

    Private Sub DeliveryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeliveryToolStripMenuItem.Click
        display_inSub(Delivery)
    End Sub

    Private Sub RecievingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RecievingToolStripMenuItem1.Click
        display_inSub(Recieving_view)
    End Sub

    Private Sub DeliveryOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeliveryOUTToolStripMenuItem.Click
        display_inSub(Delivery_view)
    End Sub

    Private Sub StockMonitoringToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ReturnablesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnablesToolStripMenuItem.Click
        display_inSub(Returnables)
    End Sub

    Private Sub ReturnBoxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnBoxToolStripMenuItem.Click
        display_inSub(return_view)
    End Sub

    Private Sub btn_stock_Click(sender As Object, e As EventArgs) Handles btn_stock.Click
        If btn_stock.ContextMenuStrip IsNot Nothing Then
            btn_stock.ContextMenuStrip.Show(btn_stock, 0, btn_stock.Height)

        End If
    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddUserToolStripMenuItem.Click
        display_inSub(add_user)
    End Sub

    Private Sub btn_transit_Click(sender As Object, e As EventArgs) Handles btn_transit.Click
        btn_transit.Text = Checktransit().ToString()
        transit.ShowDialog()
        transit.BringToFront()
    End Sub

    Private Sub CheckUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckUpdatesToolStripMenuItem.Click
        CheckUpdate()
    End Sub

    Private Sub transit_timer_Tick(sender As Object, e As EventArgs) Handles transit_timer.Tick
        btn_transit.Text = Checktransit().ToString()

    End Sub


    Private Sub subframe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btn_transit.Text = Checktransit().ToString()
        transit_timer.Interval = 60000 ' 1 minute in milliseconds
        transit_timer.Start()
        Update_timer.Interval = 1800000 ' 30 minutes in milliseconds (30 * 60 * 1000)
        Update_timer.Start()
    End Sub

    Private Sub Update_timer_Tick(sender As Object, e As EventArgs) Handles Update_timer.Tick
        CheckUpdate(0)
    End Sub

    Private Sub StockMonitoringToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles StockMonitoringToolStripMenuItem.Click
        display_inSub(New StockMonitoring)
    End Sub

    Private Sub ExpiringStocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpiringStocksToolStripMenuItem.Click
        display_inSub(New expiring_stock)
    End Sub

    Private Sub UpdateproddateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateproddateToolStripMenuItem.Click
        display_inSub(New update_proddate)
    End Sub
End Class