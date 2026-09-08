Imports System.Collections.Generic

Public Module QRParser

    Public Structure QRData
        Public PartCode As String
        Public LotNumber As String
        Public Qty As String
        Public Remarks As String
        Public Serial As String
        Public Supplier As String
    End Structure

    Public Function ParseQR(qrString As String) As Nullable(Of QRData)

        Dim data As New QRData()

        Try
            ' Split by "|"
            Dim parts() As String = qrString.Split("|"c)

            ' Store tag-value pairs
            Dim dict As New Dictionary(Of String, String)

            For Each p As String In parts

                If p.Length > 2 Then

                    Dim tag As String = p.Substring(0, 2)   ' Z1, Z2, etc.
                    Dim value As String = p.Substring(2)

                    dict(tag) = value

                End If

            Next

            ' Validate required keys
            If dict.ContainsKey("Z1") AndAlso
               dict.ContainsKey("Z2") AndAlso
               dict.ContainsKey("Z3") AndAlso
               dict.ContainsKey("Z4") AndAlso
               dict.ContainsKey("Z5") AndAlso
               dict.ContainsKey("Z7") Then

                data.PartCode = dict("Z1")
                data.LotNumber = dict("Z2")
                data.Qty = dict("Z3")
                data.Remarks = dict("Z4")
                data.Serial = dict("Z5")
                data.Supplier = dict("Z7")

                Return data

            Else

                ' Missing required fields
                Return Nothing

            End If

        Catch
            Return Nothing
        End Try

    End Function

End Module