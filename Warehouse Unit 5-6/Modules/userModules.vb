Imports MySql.Data.MySqlClient

Module userModules
    Public user_IDno As String
    Public user_Username As String
    Public user_Password As String
    Public user_pc As String

    Public Function isLogin(IDno As String, pass As String) As Boolean
        Dim query As String = "SELECT IDno,Firstname,password FROM logistics_user WHERE IDno = @IDno AND password = @password"


        Using cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@IDno", IDno)
            cmd.Parameters.AddWithValue("@password", pass)
            con.Close()
            con.Open()
            dr = cmd.ExecuteReader
            If dr.Read = True Then

                user_IDno = dr.GetString("IDno")
                user_Username = dr.GetString("Firstname")
                user_Password = dr.GetString("password")
                user_pc = Environment.MachineName
                Return 1
            Else
                Return 0
            End If
        End Using

    End Function


    Public Function isAdmin() As Boolean
        Dim query As String = "SELECT admin FROM logistics_user WHERE IDno = @IDno"


        Using cmd As New MySqlCommand(query, con)
            cmd.Parameters.AddWithValue("@IDno", user_IDno)

            con.Close()
            con.Open()
            dr = cmd.ExecuteReader
            If dr.Read = True Then
                Return dr.GetBoolean(0)
            Else
                Return 0
            End If
        End Using

    End Function




End Module
