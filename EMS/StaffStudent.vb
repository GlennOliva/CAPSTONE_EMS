Imports MySql.Data.MySqlClient

Public Class StaffStudent

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        ' When the "Dashboard" label is clicked, show AdminDashboard and hide this form
        Dim dashboard As New StaffDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        LoadStudentsData(txt_search.Text.Trim())
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Hide the current form
        Me.Hide()

        ' Create the AdminCreateStudentModal
        Dim studentModal As New StaffCreateStudentModal()

        ' Show the modal form
        studentModal.ShowDialog()

        ' When the student modal closes, show the current form again
        Me.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Ensure a student is selected
        If Students.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Get the selected student's ID
        Dim selectedRow As DataGridViewRow = Students.SelectedRows(0)
        Dim studentId As Integer

        ' Assume the ID is in the first column (adjust if different)
        If Not Integer.TryParse(selectedRow.Cells("id").Value.ToString(), studentId) Then
            MessageBox.Show("Invalid student selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Hide the current form
        Me.Hide()

        ' Open the edit modal
        Dim editForm As New StaffEditStudentModal()
        editForm.StudentId = studentId
        editForm.ShowDialog()

        ' Refresh the DataGridView if needed
        ' LoadStudentsData() ' uncomment if you have a method to reload data

        ' Show the current form again
        Me.Show()
    End Sub

    Private Sub LoadStudentsData(Optional ByVal searchTerm As String = "")
        Using conn As New MySqlConnection("server=localhost;userid=root;password=;database=enrollment_management_system")
            Try
                conn.Open()
                Dim query As String = "SELECT id, id_number, last_name, first_name, middle_initial, sex, dob, year_section, status 
                                   FROM tbl_students 
                                   WHERE status = 'Active' 
                                   AND (first_name LIKE @search OR last_name LIKE @search OR id_number LIKE @search)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    Dim dt As New DataTable()
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                    Students.DataSource = dt
                    Students.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading students: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub StaffStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentsData()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Ensure a student is selected
        If Students.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student to deactivate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Get the selected student's ID
        Dim selectedRow As DataGridViewRow = Students.SelectedRows(0)
        Dim studentId As String = selectedRow.Cells("id").Value.ToString()

        ' Confirm before setting inactive
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to set this student as inactive?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub

        ' Update status in database
        Using conn As New MySqlConnection("server=localhost;userid=root;password=;database=enrollment_management_system")
            Try
                conn.Open()
                Dim query As String = "UPDATE tbl_students SET status = 'Inactive' WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", studentId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Student has been set to inactive successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadStudentsData()
                    Else
                        MessageBox.Show("No record was updated. Please check the student ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating student status: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim managestudent As New StaffStudent()
        managestudent.Show()
        Me.Hide()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub




    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Dim manageProfile As New StaffEditProfileModal()

        If LoggedInRole = "Staff" AndAlso LoggedInStaffId > 0 Then
            manageProfile.StaffID = LoggedInStaffId
            manageProfile.Show()
        Else
            MsgBox("No valid logged-in user found.", vbExclamation)
        End If
    End Sub
End Class
