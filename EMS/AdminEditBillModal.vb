Imports MySql.Data.MySqlClient

Public Class AdminEditBillModal
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' Public property to receive the selected Course ID from AdminCourse form
    Public StaffId As Integer

    Private Sub ClearFields()
        txt_codenumber.Clear()
        txt_units.Clear()
        txt_time.Clear()
        txt_coursename.Clear()
        txt_days.Clear()
        txt_description.Clear()
        txt_room.Clear()
    End Sub

    ' ✅ Load course data when form opens
    Private Sub AdminEditBillModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If StaffId > 0 Then
            LoadCourseData(StaffId)
        End If
    End Sub

    Private Sub LoadCourseData(courseId As Integer)
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM tbl_courses WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", courseId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txt_codenumber.Text = reader("course_code").ToString()
                            txt_coursename.Text = reader("course_name").ToString()
                            txt_description.Text = reader("description").ToString()
                            txt_units.Text = reader("units").ToString()
                            txt_time.Text = reader("time").ToString()
                            txt_days.Text = reader("days").ToString()
                            txt_room.Text = reader("room").ToString()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading course data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ✅ Update button
    Private Sub btn_edit_course_Click(sender As Object, e As EventArgs) Handles btn_edit_course.Click
        If StaffId <= 0 Then
            MessageBox.Show("Invalid course selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Validate required fields
        If String.IsNullOrWhiteSpace(txt_codenumber.Text) OrElse
           String.IsNullOrWhiteSpace(txt_coursename.Text) OrElse
           String.IsNullOrWhiteSpace(txt_description.Text) OrElse
           String.IsNullOrWhiteSpace(txt_units.Text) OrElse
           String.IsNullOrWhiteSpace(txt_time.Text) OrElse
           String.IsNullOrWhiteSpace(txt_days.Text) OrElse
           String.IsNullOrWhiteSpace(txt_room.Text) Then

            MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction()

            Try
                ' ✅ Correct update query for tbl_courses
                Dim query As String = "
                    UPDATE tbl_courses
                    SET 
                        course_code = @course_code,
                        course_name = @course_name,
                        description = @description,
                        units = @units,
                        time = @time,
                        days = @days,
                        room = @room
                    WHERE id = @id;
                "

                Using cmd As New MySqlCommand(query, conn, trans)
                    cmd.Parameters.AddWithValue("@course_code", txt_codenumber.Text.Trim())
                    cmd.Parameters.AddWithValue("@course_name", txt_coursename.Text.Trim())
                    cmd.Parameters.AddWithValue("@description", txt_description.Text.Trim())
                    cmd.Parameters.AddWithValue("@units", txt_units.Text.Trim())
                    cmd.Parameters.AddWithValue("@time", txt_time.Text.Trim())
                    cmd.Parameters.AddWithValue("@days", txt_days.Text.Trim())
                    cmd.Parameters.AddWithValue("@room", txt_room.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", StaffId)

                    cmd.ExecuteNonQuery()
                End Using

                trans.Commit()
                MessageBox.Show("Course record successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                Me.Close()

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error updating course: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
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

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim manageCourse As New AdminCourse()
        manageCourse.Show()
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
