Imports ClosedXML.Excel
Imports System.IO
Imports System.Windows.Forms

Module excelModule

    Public Sub exportExcel(datagrid As Object, Title As String)
        Try
            If datagrid.Rows.Count = 0 Then
                MessageBox.Show("No data available to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dt As New DataTable()

            ' Add only visible columns to DataTable (force as string type)
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each column As DataGridViewColumn In datagrid.Columns
                If column.Visible Then
                    visibleColumns.Add(column)
                    dt.Columns.Add(column.HeaderText, GetType(String))
                End If
            Next

            ' Add rows with date handling
            For Each row As DataGridViewRow In datagrid.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To visibleColumns.Count - 1
                        Dim colIndex As Integer = visibleColumns(i).Index
                        Dim cellValue = row.Cells(colIndex).Value

                        If TypeOf cellValue Is DateTime Then
                            newRow(i) = DirectCast(cellValue, DateTime).ToString("yyyy-MM-dd")
                        Else
                            newRow(i) = If(cellValue IsNot Nothing, cellValue.ToString(), "")
                        End If
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next

            ' Save to Excel file
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel Workbook|*.xlsx"
                sfd.Title = "Save an Excel File"
                sfd.FileName = $"{Title}_{DateTime.Now:yyMMdd_HHmmss}.xlsx"

                If sfd.ShowDialog() = DialogResult.OK AndAlso sfd.FileName <> "" Then
                    If File.Exists(sfd.FileName) Then
                        Dim result As DialogResult = MessageBox.Show("The file already exists. Do you want to overwrite it?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If result <> DialogResult.Yes Then Return
                    End If

                    Using wb As New XLWorkbook()
                        Dim ws = wb.Worksheets.Add("Sheet1")

                        ' Title row
                        ws.Cell(1, 1).Value = Title
                        ws.Range(1, 1, 1, visibleColumns.Count).Merge()
                        ws.Cell(1, 1).Style.Font.Bold = True
                        ws.Cell(1, 1).Style.Font.FontSize = 14
                        ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' Insert data
                        ws.Cell(2, 1).InsertTable(dt, False)

                        ' Format
                        ws.Columns().AdjustToContents()
                        ws.Row(2).Style.Font.Bold = True

                        wb.SaveAs(sfd.FileName)
                    End Using

                    MessageBox.Show("Data successfully exported to Excel.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub exportExcel(datagrid As Object, Title As String, withDate As String)
        Try
            If datagrid.Rows.Count = 0 Then
                MessageBox.Show("No data available to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dt As New DataTable()

            ' Add only visible columns to DataTable (force as string type)
            Dim visibleColumns As New List(Of DataGridViewColumn)
            For Each column As DataGridViewColumn In datagrid.Columns
                If column.Visible Then
                    visibleColumns.Add(column)
                    dt.Columns.Add(column.HeaderText, GetType(String))
                End If
            Next

            ' Add rows with date handling
            For Each row As DataGridViewRow In datagrid.Rows
                If Not row.IsNewRow Then
                    Dim newRow As DataRow = dt.NewRow()
                    For i As Integer = 0 To visibleColumns.Count - 1
                        Dim colIndex As Integer = visibleColumns(i).Index
                        Dim cellValue = row.Cells(colIndex).Value

                        If TypeOf cellValue Is DateTime Then
                            newRow(i) = DirectCast(cellValue, DateTime).ToString("yyyy-MM-dd")
                        Else
                            newRow(i) = If(cellValue IsNot Nothing, cellValue.ToString(), "")
                        End If
                    Next
                    dt.Rows.Add(newRow)
                End If
            Next

            ' Save to Excel file
            Using sfd As New SaveFileDialog()
                sfd.Filter = "Excel Workbook|*.xlsx"
                sfd.Title = "Save an Excel File"
                sfd.FileName = $"{Title}_{withDate}.xlsx"

                If sfd.ShowDialog() = DialogResult.OK AndAlso sfd.FileName <> "" Then
                    If File.Exists(sfd.FileName) Then
                        Dim result As DialogResult = MessageBox.Show("The file already exists. Do you want to overwrite it?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If result <> DialogResult.Yes Then Return
                    End If

                    Using wb As New XLWorkbook()
                        Dim ws = wb.Worksheets.Add("Sheet1")

                        ' Title row
                        ws.Cell(1, 1).Value = Title & " As of " & withDate
                        ws.Range(1, 1, 1, visibleColumns.Count).Merge()
                        ws.Cell(1, 1).Style.Font.Bold = True
                        ws.Cell(1, 1).Style.Font.FontSize = 14
                        ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                        ' Insert data
                        ws.Cell(2, 1).InsertTable(dt, False)

                        ' Format
                        ws.Columns().AdjustToContents()
                        ws.Row(2).Style.Font.Bold = True

                        wb.SaveAs(sfd.FileName)
                    End Using

                    MessageBox.Show("Data successfully exported to Excel.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module
