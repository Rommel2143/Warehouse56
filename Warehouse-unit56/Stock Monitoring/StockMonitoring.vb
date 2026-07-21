Imports MySql.Data.MySqlClient
Public Class StockMonitoring
    Private Sub StockMonitoring_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadall()
        dtpicker1.Value = Date.Now
    End Sub

    Private Sub loadall()
        reload("
    SELECT 
        lu.partcode AS Partcode, 
        lm.partname, 
        COUNT(lu.id) AS 'Boxes', 
        IFNULL(SUM(lu.qty), 0) AS `TOTAL`
    FROM logistics_unit56 lu
    LEFT JOIN logistics_masterlist lm 
        ON lm.partcode = lu.partcode
    WHERE lu.status = 1
    GROUP BY lm.partcode, lm.partname
    ORDER BY `TOTAL` DESC
", datagrid1)


        ' Avoid adding handler multiple times
        RemoveHandler datagrid1.CellFormatting, AddressOf datagrid1_CellFormatting
        AddHandler datagrid1.CellFormatting, AddressOf datagrid1_CellFormatting
    End Sub

    Private Sub datagrid1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.ColumnIndex = datagrid1.Columns("TOTAL").Index Then
            e.CellStyle.BackColor = Color.LightBlue
            e.CellStyle.Font = New Font("Segoe UI", 11)
        End If
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        If Guna2TextBox1.Text = "" Then
            loadall()
        Else
            reload("SELECT lm.partcode AS Partcode, lm.partname, " &
           "COUNT(lu.id) AS 'Boxes', " &
           "IFNULL(SUM(lu.qty), 0) AS `TOTAL` " &
           "FROM logistics_masterlist lm " &
           "LEFT JOIN logistics_unit56 lu ON lm.partcode = lu.partcode AND lu.status = 1 " &
            "WHERE lm.partcode REGEXP '" & Guna2TextBox1.Text & "' OR lm.partname REGEXP '" & Guna2TextBox1.Text & "' " &
           "GROUP BY lm.partcode, lm.partname " &
           "ORDER BY `TOTAL` DESC", datagrid1)




        End If
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        'exportExcel(datagrid1, "Total Stock", Date.Now.ToString)
    End Sub

    Private Sub dtpicker1_ValueChanged(sender As Object, e As EventArgs) Handles dtpicker1.ValueChanged

        ' Using parameterized query for security
        Dim query As String = "SELECT ps.partcode AS Partcode, pm.partname AS 'Partname', 
                                SUM(CASE WHEN ps.datein IS NOT NULL THEN ps.qty ELSE 0 END) AS 'Total IN',
                                SUM(CASE WHEN ps.dateout IS NOT NULL THEN ps.qty ELSE 0 END) AS 'Total OUT'
                               FROM logistics_unit56 ps
                               JOIN logistics_masterlist pm ON ps.partcode = pm.partcode
                               WHERE ps.datein = @selectedDate
                               GROUP BY ps.partcode, pm.partname"


        Using cmd As New MySqlCommand(query, con)
            ' Add parameter for date
            cmd.Parameters.AddWithValue("@selectedDate", dtpicker1.Value.ToString("yyyy-MM-dd"))
            con.Close()
            ' Open connection
            con.Open()

            ' Execute the query and bind to DataGridView
            Using reader As MySqlDataReader = cmd.ExecuteReader()
                ' Assuming you already have a method to bind the result to datagrid1
                Dim dt As New DataTable()
                dt.Load(reader)
                datagrid2.DataSource = dt
            End Using

        End Using
    End Sub

    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        'exportExcel(datagrid2, "Daily Summary", Date.Now.ToString())
    End Sub
End Class