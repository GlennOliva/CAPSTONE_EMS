Imports MySql.Data.MySqlClient

Public Class AdminEditStaffModal
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' Public property so you can pass the selected staff ID from the main form
    Public StaffId As Integer

    Private Sub ClearFields()
        txt_surname.Clear()
        txt_firstname.Clear()
        txt_position.Clear()
        txt_username.Clear()
        txt_password.Clear()
        txt_contact.Clear()
        txt_address.Clear()
        txt_sex.Clear()
        txt_email.Clear()
    End Sub

    ' ✅ Load staff data when editing
    Private Sub AdminEditStaffModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If StaffId > 0 Then
            LoadStaffData(StaffId)
        End If
    End Sub

    Private Sub LoadStaffData(staffId As Integer)
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM tbl_staff WHERE id = @id"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@id", staffId)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txt_username.Text = reader("username").ToString()
                            txt_password.Text = reader("password").ToString()
                            txt_firstname.Text = reader("first_name").ToString()
                            txt_surname.Text = reader("last_name").ToString()
                            txt_position.Text = reader("position").ToString()
                            txt_email.Text = reader("email").ToString()
                            txt_contact.Text = reader("contact_number").ToString()
                            txt_address.Text = reader("address").ToString()
                            txt_sex.Text = reader("sex").ToString()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading staff data: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ✅ Update button
    Private Sub btn_update_staff_Click(sender As Object, e As EventArgs) Handles btn_create_staff.Click
        If StaffId <= 0 Then
            MessageBox.Show("Invalid staff selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txt_firstname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_surname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_email.Text) OrElse
           String.IsNullOrWhiteSpace(txt_sex.Text) OrElse
           String.IsNullOrWhiteSpace(txt_username.Text) OrElse
           String.IsNullOrWhiteSpace(txt_position.Text) OrElse
           String.IsNullOrWhiteSpace(txt_contact.Text) Then

            MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction()

            Try
                ' ✅ Update query
                Dim query As String =
"UPDATE tbl_staff 
SET 
    username = @username,
    password = @password,
    first_name = @first_name,
    last_name = @last_name,
    position = @position,
    email = @email,
    contact_number = @contact_number,
    address = @address,
    sex = @sex
WHERE id = @id;"

                Using cmd As New MySqlCommand(query, conn, trans)
                    cmd.Parameters.AddWithValue("@username", txt_username.Text.Trim())
                    cmd.Parameters.AddWithValue("@password", txt_password.Text.Trim())
                    cmd.Parameters.AddWithValue("@first_name", txt_firstname.Text.Trim())
                    cmd.Parameters.AddWithValue("@last_name", txt_surname.Text.Trim())
                    cmd.Parameters.AddWithValue("@position", txt_position.Text.Trim())
                    cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim())
                    cmd.Parameters.AddWithValue("@contact_number", txt_contact.Text.Trim())
                    cmd.Parameters.AddWithValue("@address", txt_address.Text.Trim())
                    cmd.Parameters.AddWithValue("@sex", txt_sex.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", StaffId)

                    cmd.ExecuteNonQuery()
                End Using

                trans.Commit()
                MessageBox.Show("Staff record successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error updating staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
