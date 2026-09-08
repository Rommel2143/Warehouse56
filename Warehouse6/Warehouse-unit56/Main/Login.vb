Public Class Login
    Dim testMode As Boolean = 0
    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Login()

    End Sub


    Private Sub Login()
        ' Get username and password from textboxes
        Dim username As String = txt_idno.Text.Trim()
        Dim password As String = txt_password.Text.Trim()

        ' Check if fields are empty
        If username = "" Then
            show_error("Access Denied.", 0)
            txt_idno.Focus()
            Exit Sub
        End If


        If isLogin(username, password) Then
            display_inMain(subframe)

            Me.Close()
        Else
            show_error("Access Denied.", 0)
            txt_idno.Focus()
        End If
    End Sub

    Private Sub txt_password_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_password.KeyDown, txt_idno.KeyDown
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If testMode = True Then
            isLogin("03200728", "1123")
            display_inMain(subframe)
        End If
    End Sub
End Class