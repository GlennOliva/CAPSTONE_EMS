Imports MySql.Data.MySqlClient

Public Class AdminCourse
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' 🔹 Navigate to Dashboard
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    ' 🔹 Search course by name or code
    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        LoadCourseData(txt_search.Text.Trim())
    End Sub

    ' 🔹 Add new course
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim courseModal As New AdminCreateCourseModal()
        courseModal.ShowDialog()
        Me.Hide()
        LoadCourseData()
    End Sub

    ' 🔹 Edit course
    Private Sub btn_editcourse_Click(sender As Object, e As EventArgs) Handles btn_editcourse.Click
        If Courses.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a course to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Get selected course ID
        Dim selectedRow As DataGridViewRow = Courses.SelectedRows(0)
        Dim selectedCourseId As Integer

        If Not Integer.TryParse(selectedRow.Cells("id").Value.ToString(), selectedCourseId) Then
            MessageBox.Show("Invalid course selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.Hide()

        ' ✅ Pass course ID to edit form
        Dim editForm As New AdminEditCourseModal()
        editForm.StaffId = selectedCourseId
        editForm.ShowDialog()

        LoadCourseData()
        Me.Show()
    End Sub

    ' 🔹 Load course records
    Private Sub LoadCourseData(Optional ByVal searchTerm As String = "")
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "
                    SELECT id, course_code, course_name, description, status
                    FROM tbl_courses
                    WHERE status = 'Active'
                    AND (course_name LIKE @search OR course_code LIKE @search)
                "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    Dim dt As New DataTable()
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                    Courses.DataSource = dt
                    Courses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading courses: " & ex.Message)
            End Try
        End Using
    End Sub

    ' 🔹 Form Load
    Private Sub AdminCourse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourseData()
    End Sub

    ' 🔹 Deactivate course
    Private Sub btn_delete_course_Click(sender As Object, e As EventArgs) Handles btn_delete_course.Click
        If Courses.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a course to deactivate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedRow As DataGridViewRow = Courses.SelectedRows(0)
        Dim courseId As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to set this course as inactive?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub

        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "UPDATE tbl_courses SET status = 'Inactive' WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", courseId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Course has been set to inactive successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadCourseData()
                    Else
                        MessageBox.Show("No record was updated. Please check the course ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating course status: " & ex.Message)
            End Try
        End Using
    End Sub

    ' 🔹 Navigation to Manage Students
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim manageStudent As New AdminStudent()
        manageStudent.Show()
        Me.Hide()
    End Sub

    ' 🔹 Logout
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        ' Open Edit Profile depending on the user role
        Dim manageProfile As New AdminEditProfileModal()

        If LoggedInRole = "Admin" AndAlso LoggedInAdminId > 0 Then
            manageProfile.AdminID = LoggedInAdminId
            manageProfile.ShowDialog()
        Else
            MsgBox("No valid logged-in user found.", vbExclamation)
        End If
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
End Class
