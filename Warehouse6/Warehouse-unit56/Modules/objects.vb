Imports Guna.UI2.WinForms
Module objects


    Public Sub addobject_group(parentPanel As Panel, partname As String, partcode As String, qty As Integer, count As Integer)
        ' Create a new panel
        Dim childPanel As New Guna2Panel()

        ' Set properties for the child panel
        With childPanel

            .Width = 370
            .Height = 120
            .FillColor = Color.White
            .Margin = New Padding(10)
            .BorderThickness = 1
            .BorderRadius = 5
            .BorderColor = Color.MidnightBlue
        End With

        Dim partnameLabel As New Label()
        With partnameLabel
            .Font = New Font("Segoe UI", 14, FontStyle.Bold)
            .BackColor = Color.White
            .ForeColor = Color.Black
            .AutoSize = True
            .Text = partname

            .Location = New Point(10, 20)
        End With

        Dim partcodeLabel As New Label()
        With partcodeLabel
            .Font = New Font("Segoe UI", 14)
            .BackColor = Color.White
            .ForeColor = Color.Black
            .AutoSize = True
            .Text = partcode

            .Location = New Point(10, 40)
        End With

        Dim qtyLabel As New Label()
        With qtyLabel
            .Font = New Font("Segoe UI", 15)
            .BackColor = Color.White
            .ForeColor = Color.Black
            .AutoSize = True
            .Text = "Total : " & qty.ToString
            .Location = New Point(15, 75)
        End With

        Dim countLabel As New Label()
        With countLabel
            .Font = New Font("Segoe UI", 15)
            .BackColor = Color.White
            .ForeColor = Color.Black
            .AutoSize = True
            .Text = "Box Count : " & count.ToString
            .Location = New Point(200, 75)
        End With

        childPanel.Controls.Add(partnameLabel)
        childPanel.Controls.Add(partcodeLabel)
        childPanel.Controls.Add(qtyLabel)
        childPanel.Controls.Add(countLabel)

        parentPanel.Controls.Add(childPanel)
    End Sub


End Module
