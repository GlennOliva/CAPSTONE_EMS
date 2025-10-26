Imports MySql.Data.MySqlClient

Public Class AdminCreateCourseModal
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    Private Sub ClearFields()
        txt_codenumber.Clear()
        txt_units.Clear()
        txt_time.Clear()
        txt_coursename.Clear()
        txt_days.Clear()
        txt_description.Clear()
        txt_room.Clear()
    End Sub

    Private Sub btn_create_course_Click(sender As Object, e As EventArgs) Handles btn_create_course.Click
        ' Validate required fields
        If String.IsNullOrWhiteSpace(txt_codenumber.Text) OrElse
       String.IsNullOrWhiteSpace(txt_coursename.Text) OrElse
       String.IsNullOrWhiteSpace(txt_units.Text) OrElse
       String.IsNullOrWhiteSpace(txt_time.Text) OrElse
       String.IsNullOrWhiteSpace(txt_days.Text) OrElse
       String.IsNullOrWhiteSpace(txt_room.Text) OrElse
       String.IsNullOrWhiteSpace(txt_description.Text) Then

            MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction()

            Try
                ' ✅ Insert new course record
                Dim query As String =
                "INSERT INTO tbl_courses (course_code, course_name, units, time, days, room, description, status)
                 VALUES (@course_code, @course_name, @units, @time, @days, @room, @description, 'Active');"

                Using cmd As New MySqlCommand(query, conn, trans)
                    cmd.Parameters.AddWithValue("@course_code", txt_codenumber.Text.Trim())
                    cmd.Parameters.AddWithValue("@course_name", txt_coursename.Text.Trim())
                    cmd.Parameters.AddWithValue("@units", txt_units.Text.Trim())
                    cmd.Parameters.AddWithValue("@time", txt_time.Text.Trim())
                    cmd.Parameters.AddWithValue("@days", txt_days.Text.Trim())
                    cmd.Parameters.AddWithValue("@room", txt_room.Text.Trim())
                    cmd.Parameters.AddWithValue("@description", txt_description.Text.Trim())

                    cmd.ExecuteNonQuery()
                End Using

                trans.Commit()

                ' ✅ Show success message
                MessageBox.Show("Course successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' ✅ Redirect to AdminCourse form
                Dim managecourse As New AdminCourse()
                managecourse.Show()
                Me.Close() ' Close this modal

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error inserting course: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim managecourse As New AdminCourse()
        managecourse.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim manageStudent As New AdminStudent()
        manageStudent.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim manageStaff As New AdminStaff()
        manageStaff.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim manageBill As New AdminBill()
        manageBill.Show()
        Me.Hide()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Dim manageReports As New AdminReports()
        manageReports.Show()
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

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub
End Class
