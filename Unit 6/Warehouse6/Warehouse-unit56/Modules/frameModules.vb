Module frameModules

    Public Sub display_inMain(newForm As Form)

        With newForm
            .TopLevel = False
            Mainframe.Panel1.Controls.Clear()
            Mainframe.Panel1.Controls.Add(newForm)
            .BringToFront()
            .Show()
        End With

    End Sub


    Public Sub display_inSub(newForm As Form)

        With newForm
            .TopLevel = False

            subframe.Panel1.Controls.Add(newForm)
            .BringToFront()
            .Show()
        End With
    End Sub


End Module
