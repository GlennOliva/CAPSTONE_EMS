<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminStudent

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
        Label6 = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        PictureBox1 = New PictureBox()
        Label15 = New Label()
        Students = New DataGridView()
        txt_search = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        Button3 = New Button()
        Label9 = New Label()
        Panel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(Students, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Gainsboro
        Panel2.Controls.Add(Label8)
        Panel2.Controls.Add(Label7)
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(Label5)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(Label3)
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
        Label8.Location = New Point(12, 546)
        Label8.Name = "Label8"
        Label8.Size = New Size(60, 20)
        Label8.TabIndex = 8
        Label8.Text = "Logout"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Times New Roman", 10.8F)
        Label7.Location = New Point(12, 490)
        Label7.Name = "Label7"
        Label7.Size = New Size(119, 20)
        Label7.TabIndex = 7
        Label7.Text = "Manage Profile"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Times New Roman", 10.8F)
        Label6.Location = New Point(12, 435)
        Label6.Name = "Label6"
        Label6.Size = New Size(127, 20)
        Label6.TabIndex = 6
        Label6.Text = "Manage Reports"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Times New Roman", 10.8F)
        Label5.Location = New Point(12, 382)
        Label5.Name = "Label5"
        Label5.Size = New Size(95, 20)
        Label5.TabIndex = 5
        Label5.Text = "Manage Bill"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Times New Roman", 10.8F)
        Label4.Location = New Point(12, 323)
        Label4.Name = "Label4"
        Label4.Size = New Size(129, 20)
        Label4.TabIndex = 4
        Label4.Text = "Manage Courses"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Times New Roman", 10.8F)
        Label3.Location = New Point(12, 272)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 20)
        Label3.TabIndex = 3
        Label3.Text = "Manage Staffs"
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
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Times New Roman", 10.8F)
        Label15.Location = New Point(246, 236)
        Label15.Name = "Label15"
        Label15.Size = New Size(79, 20)
        Label15.TabIndex = 2
        Label15.Text = "SEARCH"
        ' 
        ' Students
        ' 
        Students.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Students.Location = New Point(251, 271)
        Students.Name = "Students"
        Students.RowHeadersWidth = 51
        Students.Size = New Size(1455, 423)
        Students.TabIndex = 5
        ' 
        ' txt_search
        ' 
        txt_search.Font = New Font("Times New Roman", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txt_search.Location = New Point(331, 228)
        txt_search.Name = "txt_search"
        txt_search.Size = New Size(226, 28)
        txt_search.TabIndex = 6
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Green
        Button1.ForeColor = Color.White
        Button1.Location = New Point(1245, 229)
        Button1.Name = "Button1"
        Button1.Size = New Size(141, 35)
        Button1.TabIndex = 7
        Button1.Text = "CREATE STUDENT"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Yellow
        Button2.ForeColor = Color.Black
        Button2.Location = New Point(1403, 229)
        Button2.Name = "Button2"
        Button2.Size = New Size(141, 33)
        Button2.TabIndex = 8
        Button2.Text = "EDIT STUDENT"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Button3
        ' 
        Button3.BackColor = Color.Red
        Button3.ForeColor = Color.Transparent
        Button3.Location = New Point(1562, 230)
        Button3.Name = "Button3"
        Button3.Size = New Size(144, 33)
        Button3.TabIndex = 9
        Button3.Text = "DELETE STUDENT"
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Times New Roman", 24F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label9.Location = New Point(231, 42)
        Label9.Name = "Label9"
        Label9.Size = New Size(406, 46)
        Label9.TabIndex = 10
        Label9.Text = "MANAGE STUDENTS"
        ' 
        ' AdminStudent
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1902, 940)
        Controls.Add(Label9)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(txt_search)
        Controls.Add(Label15)
        Controls.Add(Students)
        Controls.Add(Panel2)
        MaximizeBox = False
        Name = "AdminStudent"
        StartPosition = FormStartPosition.CenterScreen
        Text = "AdminStudent"
        WindowState = FormWindowState.Maximized
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(Students, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Students As DataGridView
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_search As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label9 As Label
End Class
