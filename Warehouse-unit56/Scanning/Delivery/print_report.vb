Imports MySql.Data.MySqlClient
Public Class print_report

    Dim dt As New DataTable
    Private Sub print_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub




    Public Sub viewdata(dateout As String, user As String, batchout As String)
        Dim myrpt As New delivery_report
        dt.Clear()


        con.Close()
        con.Open()
        Dim showreport As New MySqlCommand("SELECT mo.id,
                                                    mo.qrcode,
                                                    mo.partcode,
                                                    mm.partname,
                                                    mo.lotnumber AS lotno,
                                                    mo.qty,
                                                    mo.remarks,
                                                    mo.boxno,
                                                    mo.batchOUT,
                                                    CONCAT(mu.Firstname,' ',mu.Lastname) AS fullname,
                                                    mo.timeOUT,
                                                    mo.dateOUT
                                                    FROM logistics_unit56 mo
                                                    INNER JOIN trc_user.logistics_user_unit56 mu ON mo.userout = mu.IDno
                                                    INNER JOIN logistics_masterlist mm ON mm.partcode=mo.partcode

                                                     WHERE `dateout`='" & dateout & "' and CONCAT(Firstname,' ',Lastname)='" & user & "' AND batchOUT='" & batchout & "'





", con)
        Dim da As New MySqlDataAdapter(showreport)
        da.Fill(dt)
        con.Close()

        myrpt.Database.Tables("outgoing_delivery").SetDataSource(dt)
        CrystalReportViewer1.ReportSource = Nothing
        CrystalReportViewer1.ReportSource = myrpt

    End Sub
End Class