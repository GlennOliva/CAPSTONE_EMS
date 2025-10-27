<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StaffDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Panel2 = New Panel()
        Label8 = New Label()
        Label7 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        Panel1 = New Panel()
        lbl_students = New Label()
        Label9 = New Label()
        Panel4 = New Panel()
        lbl_course = New Label()
        Label13 = New Label()
        lbl_bills = New Label()
        Label12 = New Label()
        Panel5 = New Panel()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel5.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(Label7)
        Panel2.Controls.Add(Label2)
        Panel2.Controls.Add(Label1)
        Panel2.Controls.Add(PictureBox1)
        Panel2.Location = New Point(0, -1)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(200, 1035)
        Panel2.TabIndex = 1
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Times New Roman", 10.8F)
        Label8.Location = New Point(12, 315)
        Label8.Name = "Label8"
        Label8.Size = New Size(60, 20)
        Label8.TabIndex = 8
        Label8.Text = "Logout"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Times New Roman", 10.8F)
        Label7.Location = New Point(12, 267)
        Label7.Name = "Label7"
        Label7.Size = New Size(119, 20)
        Label7.TabIndex = 7
        Label7.Text = "Manage Profile"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Times New Roman", 10.8F)
        Label2.Location = New Point(12, 215)
        Label2.Name = "Label2"
        Label2.Size = New Size(133, 20)
        Label2.TabIndex = 2
        Label2.Text = "Manage Students"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Times New Roman", 10.8F)
        Label1.Location = New Point(12, 163)
        Label1.Name = "Label1"
        Label1.Size = New Size(87, 20)
        Label1.TabIndex = 1
        Label1.Text = "Dashboard"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BackColor = Color.Transparent
        PictureBox1.BackgroundImage = My.Resources.Resources.logo
        PictureBox1.BackgroundImageLayout = ImageLayout.Zoom
        PictureBox1.Location = New Point(3, 0)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(194, 127)
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Gainsboro
        Panel1.Controls.Add(lbl_students)
        Panel1.Controls.Add(Label9)
        Panel1.Location = New Point(257, 90)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(417, 144)
        Panel1.TabIndex = 3
        ' 
        ' lbl_students
        ' 
        lbl_students.AutoSize = True
        lbl_students.Font = New Font("Times New Roman", 10.8F)
        lbl_students.Location = New Point(353, 66)
        lbl_students.Name = "lbl_students"
        lbl_students.Size = New Size(36, 20)
        lbl_students.TabIndex = 1
        lbl_students.Text = "100"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Times New Roman", 10.8F)
        Label9.Location = New Point(13, 66)
        Label9.Name = "Label9"
        Label9.Size = New Size(134, 20)
        Label9.TabIndex = 0
        Label9.Text = "NO STUDENTS"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Gainsboro
        Panel4.Controls.Add(lbl_course)
        Panel4.Controls.Add(Label13)
        Panel4.Location = New Point(1279, 90)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(433, 144)
        Panel4.TabIndex = 4
        ' 
        ' lbl_course
        ' 
        lbl_course.AutoSize = True
        lbl_course.Font = New Font("Times New Roman", 10.8F)
        lbl_course.Location = New Point(368, 66)
        lbl_course.Name = "lbl_course"
        lbl_course.Size = New Size(36, 20)
        lbl_course.TabIndex = 3
        lbl_course.Text = "100"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Times New Roman", 10.8F)
        Label13.Location = New Point(18, 66)
        Label13.Name = "Label13"
        Label13.Size = New Size(121, 20)
        Label13.TabIndex = 3
        Label13.Text = "NO COURSES"
        ' 
        ' lbl_bills
        ' 
        lbl_bills.AutoSize = True
        lbl_bills.Font = New Font("Times New Roman", 10.8F)
        lbl_bills.Location = New Point(395, 66)
        lbl_bills.Name = "lbl_bills"
        lbl_bills.Size = New Size(36, 20)
        lbl_bills.TabIndex = 1
        lbl_bills.Text = "100"
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Font = New Font("Times New Roman", 10.8F)
        Label12.Location = New Point(13, 66)
        Label12.Name = "Label12"
        Label12.Size = New Size(90, 20)
        Label12.TabIndex = 0
        Label12.Text = "NO BILLS"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.Gainsboro
        Panel5.Controls.Add(lbl_bills)
        Panel5.Controls.Add(Label12)
        Panel5.Location = New Point(735, 90)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(448, 144)
        Panel5.TabIndex = 5
        ' 
        ' StaffDashboard
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1902, 940)
        Controls.Add(Panel5)
        Controls.Add(Panel4)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        MaximizeBox = False
        Name = "StaffDashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "StaffDashboard"
        WindowState = FormWindowState.Maximized
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_students As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lbl_course As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lbl_bills As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel5 As Panel
End Class
