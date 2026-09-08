Imports ClosedXML.Excel
Imports System.Windows.Forms
Imports System.IO
Imports Guna.UI2.WinForms

Module ExportToFile

    Public Sub ToExcel(grid As Guna2DataGridView, fileName As String)

        If grid Is Nothing OrElse grid.Rows.Count = 0 Then
            MessageBox.Show("No records to export.",
                            "Export",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Exit Sub
        End If

        Using sfd As New SaveFileDialog()

            sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
            sfd.FileName = $"{fileName}_{Date.Now:yyyyMMdd_HHmmss}.xlsx"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Using wb As New XLWorkbook()

                'Worksheet names cannot exceed 31 characters
                Dim sheetName = Path.GetFileNameWithoutExtension(fileName)
                If sheetName.Length > 31 Then
                    sheetName = sheetName.Substring(0, 31)
                End If

                Dim ws = wb.Worksheets.Add(sheetName)

                'Headers
                For c As Integer = 0 To grid.Columns.Count - 1

                    ws.Cell(1, c + 1).Value = grid.Columns(c).HeaderText
                    ws.Cell(1, c + 1).Style.Font.Bold = True
                    ws.Cell(1, c + 1).Style.Fill.BackgroundColor = XLColor.LightGray

                Next

                'Data
                Dim excelRow As Integer = 2

                For Each row As DataGridViewRow In grid.Rows

                    If row.IsNewRow Then Continue For

                    For c As Integer = 0 To grid.Columns.Count - 1

                        Dim cellValue = row.Cells(c).Value

                        If cellValue Is Nothing OrElse IsDBNull(cellValue) Then
                            ws.Cell(excelRow, c + 1).Value = ""
                        Else
                            ws.Cell(excelRow, c + 1).Value = cellValue.ToString()
                        End If

                    Next

                    excelRow += 1

                Next

                ws.Columns().AdjustToContents()
                ws.SheetView.FreezeRows(1)

                wb.SaveAs(sfd.FileName)

            End Using

            MessageBox.Show("Export completed successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

        End Using

    End Sub

End Module