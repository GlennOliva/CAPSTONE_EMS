Imports MySql.Data.MySqlClient

Public Class AdminCreateStaffModal
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

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

    Private Sub btn_create_staff_Click(sender As Object, e As EventArgs) Handles btn_create_staff.Click
        ' Validate required fields
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
                ' ✅ Correct column order and parameters
                Dim query As String =
"INSERT INTO tbl_staff (username, password, first_name, last_name, position, email, contact_number, address, sex)
 VALUES (@username, @password, @first_name, @last_name, @position, @email, @contact_number, @address, @sex);"

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

                    ' ✅ Execute the query
                    cmd.ExecuteNonQuery()
                End Using

                ' ✅ Commit the transaction
                trans.Commit()
                MessageBox.Show("Staff successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error inserting staff: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
