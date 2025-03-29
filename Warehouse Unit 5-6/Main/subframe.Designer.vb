<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class subframe
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(subframe))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btn_menu = New Guna.UI2.WinForms.Guna2Button()
        Me.btnmenu_strip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RecievingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeliveryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnablesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecievingToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeliveryOUTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReturnBoxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockMonitoringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btn_profile = New Guna.UI2.WinForms.Guna2Button()
        Me.profile_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.btn_user = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btn_administrator = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.btnmenu_strip.SuspendLayout()
        Me.profile_menu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 51)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1168, 624)
        Me.Panel1.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.btn_menu)
        Me.Panel2.Controls.Add(Me.btn_profile)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1168, 51)
        Me.Panel2.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(78, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 21)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Warehouse Unit 5-6"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'btn_menu
        '
        Me.btn_menu.BackColor = System.Drawing.Color.Transparent
        Me.btn_menu.ContextMenuStrip = Me.btnmenu_strip
        Me.btn_menu.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_menu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_menu.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_menu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_menu.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_menu.FillColor = System.Drawing.SystemColors.ControlLight
        Me.btn_menu.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btn_menu.ForeColor = System.Drawing.Color.DimGray
        Me.btn_menu.Image = CType(resources.GetObject("btn_menu.Image"), System.Drawing.Image)
        Me.btn_menu.ImageSize = New System.Drawing.Size(24, 24)
        Me.btn_menu.Location = New System.Drawing.Point(1044, 0)
        Me.btn_menu.Name = "btn_menu"
        Me.btn_menu.Size = New System.Drawing.Size(62, 51)
        Me.btn_menu.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btn_menu, "Menu")
        Me.btn_menu.UseTransparentBackground = True
        '
        'btnmenu_strip
        '
        Me.btnmenu_strip.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnmenu_strip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecievingToolStripMenuItem, Me.DeliveryToolStripMenuItem, Me.ReturnablesToolStripMenuItem, Me.ViewRecordsToolStripMenuItem, Me.StockMonitoringToolStripMenuItem})
        Me.btnmenu_strip.Name = "ContextMenuStrip1"
        Me.btnmenu_strip.Size = New System.Drawing.Size(200, 156)
        '
        'RecievingToolStripMenuItem
        '
        Me.RecievingToolStripMenuItem.Name = "RecievingToolStripMenuItem"
        Me.RecievingToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.RecievingToolStripMenuItem.Text = "INCOMING"
        '
        'DeliveryToolStripMenuItem
        '
        Me.DeliveryToolStripMenuItem.Name = "DeliveryToolStripMenuItem"
        Me.DeliveryToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.DeliveryToolStripMenuItem.Text = "OUTGOING"
        '
        'ReturnablesToolStripMenuItem
        '
        Me.ReturnablesToolStripMenuItem.Name = "ReturnablesToolStripMenuItem"
        Me.ReturnablesToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.ReturnablesToolStripMenuItem.Text = "RETURN BOX"
        '
        'ViewRecordsToolStripMenuItem
        '
        Me.ViewRecordsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RecievingToolStripMenuItem1, Me.DeliveryOUTToolStripMenuItem, Me.ReturnBoxToolStripMenuItem})
        Me.ViewRecordsToolStripMenuItem.Name = "ViewRecordsToolStripMenuItem"
        Me.ViewRecordsToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.ViewRecordsToolStripMenuItem.Text = "View Records"
        '
        'RecievingToolStripMenuItem1
        '
        Me.RecievingToolStripMenuItem1.Name = "RecievingToolStripMenuItem1"
        Me.RecievingToolStripMenuItem1.Size = New System.Drawing.Size(156, 26)
        Me.RecievingToolStripMenuItem1.Text = "Incoming"
        '
        'DeliveryOUTToolStripMenuItem
        '
        Me.DeliveryOUTToolStripMenuItem.Name = "DeliveryOUTToolStripMenuItem"
        Me.DeliveryOUTToolStripMenuItem.Size = New System.Drawing.Size(156, 26)
        Me.DeliveryOUTToolStripMenuItem.Text = "Outgoing"
        '
        'ReturnBoxToolStripMenuItem
        '
        Me.ReturnBoxToolStripMenuItem.Name = "ReturnBoxToolStripMenuItem"
        Me.ReturnBoxToolStripMenuItem.Size = New System.Drawing.Size(156, 26)
        Me.ReturnBoxToolStripMenuItem.Text = "Return Box"
        '
        'StockMonitoringToolStripMenuItem
        '
        Me.StockMonitoringToolStripMenuItem.Name = "StockMonitoringToolStripMenuItem"
        Me.StockMonitoringToolStripMenuItem.Size = New System.Drawing.Size(199, 26)
        Me.StockMonitoringToolStripMenuItem.Text = "Stock Monitoring"
        '
        'btn_profile
        '
        Me.btn_profile.BackColor = System.Drawing.Color.Transparent
        Me.btn_profile.ContextMenuStrip = Me.profile_menu
        Me.btn_profile.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btn_profile.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btn_profile.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btn_profile.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btn_profile.Dock = System.Windows.Forms.DockStyle.Right
        Me.btn_profile.FillColor = System.Drawing.SystemColors.ControlLight
        Me.btn_profile.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btn_profile.ForeColor = System.Drawing.Color.DimGray
        Me.btn_profile.Image = CType(resources.GetObject("btn_profile.Image"), System.Drawing.Image)
        Me.btn_profile.ImageSize = New System.Drawing.Size(32, 32)
        Me.btn_profile.Location = New System.Drawing.Point(1106, 0)
        Me.btn_profile.Name = "btn_profile"
        Me.btn_profile.Size = New System.Drawing.Size(62, 51)
        Me.btn_profile.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btn_profile, "Profile")
        '
        'profile_menu
        '
        Me.profile_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_user, Me.ToolStripSeparator1, Me.btn_administrator, Me.LogoutToolStripMenuItem})
        Me.profile_menu.Name = "ContextMenuStrip1"
        Me.profile_menu.Size = New System.Drawing.Size(163, 100)
        '
        'btn_user
        '
        Me.btn_user.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_user.ForeColor = System.Drawing.Color.Black
        Me.btn_user.Image = CType(resources.GetObject("btn_user.Image"), System.Drawing.Image)
        Me.btn_user.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btn_user.Name = "btn_user"
        Me.btn_user.Size = New System.Drawing.Size(162, 30)
        Me.btn_user.Text = "User"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(159, 6)
        '
        'btn_administrator
        '
        Me.btn_administrator.BackColor = System.Drawing.Color.MistyRose
        Me.btn_administrator.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_administrator.ForeColor = System.Drawing.Color.DimGray
        Me.btn_administrator.Image = CType(resources.GetObject("btn_administrator.Image"), System.Drawing.Image)
        Me.btn_administrator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btn_administrator.Name = "btn_administrator"
        Me.btn_administrator.Size = New System.Drawing.Size(162, 30)
        Me.btn_administrator.Text = "Administrator"
        Me.btn_administrator.Visible = False
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LogoutToolStripMenuItem.ForeColor = System.Drawing.Color.DimGray
        Me.LogoutToolStripMenuItem.Image = CType(resources.GetObject("LogoutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LogoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(162, 30)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'subframe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1168, 675)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "subframe"
        Me.Text = "subframe"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.btnmenu_strip.ResumeLayout(False)
        Me.profile_menu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btn_menu As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btn_profile As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents profile_menu As ContextMenuStrip
    Friend WithEvents btn_administrator As ToolStripMenuItem
    Friend WithEvents LogoutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnmenu_strip As ContextMenuStrip
    Friend WithEvents RecievingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeliveryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReturnablesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btn_user As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents StockMonitoringToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewRecordsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecievingToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DeliveryOUTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label1 As Label
    Friend WithEvents ReturnBoxToolStripMenuItem As ToolStripMenuItem
End Class
