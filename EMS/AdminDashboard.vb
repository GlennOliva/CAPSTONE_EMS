Imports MySql.Data.MySqlClient

Public Class AdminDashboard

    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Show who is logged in
        If LoggedInRole = "Admin" Then
            MsgBox("Admin ID: " & LoggedInAdminId)
        ElseIf LoggedInRole = "Staff" Then
            MsgBox("Staff ID: " & LoggedInStaffId)
        End If

        LoadDashboardCounts()
    End Sub


    ' ✅ Load total counts for Students, Staff, Courses, and Bills
    Private Sub LoadDashboardCounts()
        Try
            If dbcon() = False Then
                MsgBox("Database connection failed!", vbExclamation)
                Exit Sub
            End If

            Dim studentCount As Integer = ExecuteScalar("SELECT COUNT(*) FROM tbl_students WHERE Status <> 'Inactive'")
            Dim staffCount As Integer = ExecuteScalar("SELECT COUNT(*) FROM tbl_staff WHERE Status <> 'Inactive'")
            Dim courseCount As Integer = ExecuteScalar("SELECT COUNT(*) FROM tbl_courses")
            Dim billCount As Integer = ExecuteScalar("SELECT COUNT(*) FROM tbl_bill")

            lbl_students.Text = studentCount.ToString()
            lbl_staff.Text = staffCount.ToString()
            lbl_course.Text = courseCount.ToString()
            lbl_bills.Text = billCount.ToString()

        Catch ex As Exception
            MsgBox("Error loading dashboard counts: " & ex.Message, vbCritical)
        Finally
            con.Close()
        End Try
    End Sub

    ' ✅ Helper function for scalar queries
    Private Function ExecuteScalar(query As String) As Integer
        Using cmd As New MySqlCommand(query, con)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso IsNumeric(result) Then
                Return Convert.ToInt32(result)
            End If
        End Using
        Return 0
    End Function



    ' ========================
    ' 📌 NAVIGATION HANDLERS
    ' ========================
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        Dim studentForm As New AdminStudent()
        studentForm.ShowDialog()
        Me.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboardForm As New AdminDashboard()
        dashboardForm.Show()
        Me.Hide()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim managestaff As New AdminStaff()
        managestaff.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim managecourse As New AdminCourse()
        managecourse.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim managebill As New AdminBill()
        managebill.Show()
        Me.Hide()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim managereports As New AdminReports()
        managereports.Show()
        Me.Hide()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Dim manageProfile As New AdminEditProfileModal()

        If LoggedInRole = "Admin" AndAlso LoggedInAdminId > 0 Then
            manageProfile.AdminID = LoggedInAdminId
            manageProfile.ShowDialog()
        Else
            MsgBox("No valid logged-in user found.", vbExclamation)
        End If
    End Sub
End Class
