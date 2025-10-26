<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
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
        Panel1 = New Panel()
        btn_login = New Button()
        txt_password = New TextBox()
        txt_username = New TextBox()
        Label2 = New Label()
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        Panel1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Gainsboro
        Panel1.BackgroundImageLayout = ImageLayout.Center
        Panel1.Controls.Add(btn_login)
        Panel1.Controls.Add(txt_password)
        Panel1.Controls.Add(txt_username)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(PictureBox1)
        Panel1.Location = New Point(-1, -1)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1904, 1034)
        Panel1.TabIndex = 0
        ' 
        ' btn_login
        ' 
        btn_login.Location = New Point(824, 494)
        btn_login.Name = "btn_login"
        btn_login.Size = New Size(244, 40)
        btn_login.TabIndex = 5
        btn_login.Text = "LOGIN"
        btn_login.UseVisualStyleBackColor = True
        ' 
        ' txt_password
        ' 
        txt_password.Font = New Font("Times New Roman", 10.8F)
        txt_password.Location = New Point(824, 440)
        txt_password.Name = "txt_password"
        txt_password.PasswordChar = "*"c
        txt_password.Size = New Size(244, 28)
        txt_password.TabIndex = 4
        ' 
        ' txt_username
        ' 
        txt_username.Font = New Font("Times New Roman", 10.8F)
        txt_username.Location = New Point(824, 385)
        txt_username.Name = "txt_username"
        txt_username.Size = New Size(244, 28)
        txt_username.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Times New Roman", 10.8F)
        Label2.Location = New Point(698, 448)
        Label2.Name = "Label2"
        Label2.Size = New Size(109, 20)
        Label2.TabIndex = 2
        Label2.Text = "PASSWORD:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Times New Roman", 10.8F)
        Label1.Location = New Point(698, 393)
        Label1.Name = "Label1"
        Label1.Size = New Size(111, 20)
        Label1.TabIndex = 1
        Label1.Text = "USERNAME:"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.BackgroundImage = My.Resources.Resources.logo
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        PictureBox1.Location = New Point(710, 18)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(448, 334)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Login
        ' 
        AccessibleRole = AccessibleRole.None
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1902, 1033)
        Controls.Add(Panel1)
        MaximizeBox = False
        MaximumSize = New Size(1920, 1080)
        MinimumSize = New Size(1918, 1018)
        Name = "Login"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login"
        WindowState = FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_login As Button
    Friend WithEvents txt_password As TextBox
    Friend WithEvents txt_username As TextBox
End Class
