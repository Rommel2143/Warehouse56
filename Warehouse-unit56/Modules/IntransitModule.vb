
Imports MySql.Data.MySqlClient
Module IntransitModule


    Public Function Checktransit() As Integer
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT COUNT(*) FROM (SELECT 1 FROM `molding_stock_intransit` WHERE status = 1 AND (scan_from='Sunbo' OR scan_to='Sunbo') GROUP BY batchcode, date_transit, partcode) AS grouped"
            Using cmd As New MySqlCommand(query, con)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count
            End Using
        Catch ex As Exception
            ' Optional: show error only if debugging
            ' MessageBox.Show(ex.Message, "Transit Check Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Function

    Public Sub UpdateIntransit(qrcode As String)

        Try

            con.Close()
            con.Open()
            Dim query As String = "UPDATE molding_stock_intransit SET status= 0
                                    WHERE qrcode=@qrcode"

            Dim cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@qrcode", qrcode)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            show_error($"Error on updating: {ex.Message}", 1)

        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

    End Sub

End Module
