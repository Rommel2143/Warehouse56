Module CheckUpdateModule

    Dim updatePath As String = "\\ptif1-ds\SystemServer\Warehouse-Sunbo\setup.exe"
    Public Sub CheckUpdate()
        Try
            ' Define the update path and current version

            Dim localPath As String = Application.StartupPath & "\setup.exe"
            Dim result As DialogResult

            ' Check if update file exists in the shared folder
            If System.IO.File.Exists(updatePath) Then
                ' Compare the last write time to decide if it's a newer version
                Dim remoteVersionDate = System.IO.File.GetLastWriteTime(updatePath)
                Dim currentVersionDate = System.IO.File.GetLastWriteTime(Application.ExecutablePath)

                If remoteVersionDate > currentVersionDate Then
                    result = MessageBox.Show("A new update is available. Do you want to install it now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If result = DialogResult.Yes Then
                        ' Optionally copy it locally or just run it
                        Process.Start(updatePath)
                        Application.Exit()
                    End If
                Else
                    MessageBox.Show("You are already using the latest version.", "No Updates", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Update file not found in the shared directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred while checking for updates: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CheckUpdate(notify As Boolean)
        Try
            ' Define the update path and current version

            Dim localPath As String = Application.StartupPath & "\setup.exe"
            Dim result As DialogResult

            ' Check if update file exists in the shared folder
            If System.IO.File.Exists(updatePath) Then
                ' Compare the last write time to decide if it's a newer version
                Dim remoteVersionDate = System.IO.File.GetLastWriteTime(updatePath)
                Dim currentVersionDate = System.IO.File.GetLastWriteTime(Application.ExecutablePath)

                If remoteVersionDate > currentVersionDate Then
                    result = MessageBox.Show("A new update is available. Do you want to install it now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If result = DialogResult.Yes Then
                        ' Optionally copy it locally or just run it
                        Process.Start(updatePath)
                        Application.Exit()
                    End If
                Else
                    If notify = True Then
                        MessageBox.Show("You are already using the latest version.", "No Updates", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End If
                    Else
                MessageBox.Show("Update file not found in the shared directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred while checking for updates: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




End Module
