Imports MySql.Data.MySqlClient

Public Class AdminStaff
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' 🔹 Navigate to Dashboard
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    ' 🔹 Search staff by name
    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        LoadStaffData(txt_search.Text.Trim())
    End Sub

    ' 🔹 Add new staff
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Dim staffModal As New AdminCreateStaffModal()
        staffModal.ShowDialog()
        Me.Show()
        LoadStaffData()
    End Sub

    ' 🔹 Edit staff
    Private Sub btn_editstaff_Click(sender As Object, e As EventArgs) Handles btn_editstaff.Click
        If Students.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a staff to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Get selected staff ID
        Dim selectedRow As DataGridViewRow = Students.SelectedRows(0)
        Dim selectedStaffId As Integer

        If Not Integer.TryParse(selectedRow.Cells("id").Value.ToString(), selectedStaffId) Then
            MessageBox.Show("Invalid staff selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Me.Hide()

        ' ✅ Pass staff ID to edit form
        Dim editForm As New AdminEditStaffModal()
        editForm.StaffId = selectedStaffId
        editForm.ShowDialog()

        LoadStaffData()
        Me.Show()
    End Sub

    ' 🔹 Load staff records
    Private Sub LoadStaffData(Optional ByVal searchTerm As String = "")
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "
                    SELECT id, username, first_name, last_name, position, email, contact_number, address, sex, status
                    FROM tbl_staff
                    WHERE status = 'Active'
                    AND (first_name LIKE @search OR last_name LIKE @search OR username LIKE @search)
                "
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    Dim dt As New DataTable()
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                    Students.DataSource = dt
                    Students.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading staff: " & ex.Message)
            End Try
        End Using
    End Sub

    ' 🔹 Form Load
    Private Sub AdminStaff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaffData()
    End Sub

    ' 🔹 Deactivate staff
    Private Sub btn_delete_staff_Click(sender As Object, e As EventArgs) Handles btn_delete_staff.Click
        If Students.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a staff to deactivate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedRow As DataGridViewRow = Students.SelectedRows(0)
        Dim staffId As Integer = Convert.ToInt32(selectedRow.Cells("id").Value)

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to set this staff as inactive?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.No Then Exit Sub

        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "UPDATE tbl_staff SET status = 'Inactive' WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", staffId)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Staff has been set to inactive successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadStaffData()
                    Else
                        MessageBox.Show("No record was updated. Please check the staff ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error updating staff status: " & ex.Message)
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
End Class
