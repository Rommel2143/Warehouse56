Public Class expiring_stock
    Private Sub expiring_stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load



    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        Dim query As String
        If txt_search.Text = "" Then
            query = "
    SELECT 
        ms.`id`, 
        ms.`partcode`, 
        mm.`partname`,
        ms.`lotnumber`, 
        ms.`prod_date`, 
        TIMESTAMPDIFF(DAY, IFNULL(ms.`prod_date`, CURDATE()), CURDATE()) AS age_in_days
    FROM 
        `logistics_unit56` ms
    JOIN 
        `logistics_masterlist` mm ON ms.`partcode` = mm.`partcode`
    WHERE 
       (ms.`status` = 1 OR ms.status IS NULL )
    ORDER BY 
        age_in_days DESC
"

        Else
            query = "
    SELECT 
        ms.`id`, 
        ms.`partcode`, 
        mm.`partname`,
        ms.`lotnumber`, 
        ms.`prod_date`, 
        TIMESTAMPDIFF(DAY, IFNULL(ms.`prod_date`, CURDATE()), CURDATE()) AS age_in_days
    FROM 
        logistics_unit56` ms
    JOIN 
        `logistics_masterlist` mm ON ms.`partcode` = mm.`partcode`
    WHERE 
        (ms.`status` = 1 OR ms.status IS NULL ) AND (ms.partcode REGEXP '" & txt_search.Text & "' OR mm.partname REGEXP '" & txt_search.Text & "')
    ORDER BY 
        age_in_days DESC
"

        End If
        reload(query, datagrid1)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        'exportExcel(datagrid1, "Expiring Stocks", Date.Now.ToString)
    End Sub
End Class
