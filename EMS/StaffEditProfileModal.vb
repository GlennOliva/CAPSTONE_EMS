Imports MySql.Data.MySqlClient
Imports System.ComponentModel

Public Class StaffEditProfileModal
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' Public property to receive the selected Admin ID from Admin form
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property StaffID As Integer

    Private Sub ClearFields()
        txt_username.Clear()
        txt_firstname.Clear()
        txt_email.Clear()
        txt_password.Clear()
        txt_last.Clear()
        txt_contact.Clear()
        txt_address.Clear()
        txt_sex.Clear()
        txt_position.Clear()
    End Sub

    ' ✅ Load admin data when form opens
    Private Sub StaffEditProfileModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If StaffID > 0 Then
            LoadStaffData(StaffID)
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
                            txt_last.Text = reader("last_name").ToString()
                            txt_email.Text = reader("email").ToString()
                            txt_contact.Text = reader("contact_number").ToString()
                            txt_position.Text = reader("position").ToString()
                            txt_address.Text = reader("address").ToString()
                            txt_sex.Text = reader("sex").ToString
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading admin data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ✅ Update admin profile
    Private Sub btn_edit_profile_Click(sender As Object, e As EventArgs) Handles btn_edit_profile.Click
        If StaffID <= 0 Then
            MessageBox.Show("Invalid admin selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Validate required fields
        If String.IsNullOrWhiteSpace(txt_username.Text) OrElse
           String.IsNullOrWhiteSpace(txt_password.Text) OrElse
           String.IsNullOrWhiteSpace(txt_firstname.Text) OrElse
           String.IsNullOrWhiteSpace(txt_last.Text) OrElse
           String.IsNullOrWhiteSpace(txt_email.Text) OrElse
           String.IsNullOrWhiteSpace(txt_address.Text) OrElse
           String.IsNullOrWhiteSpace(txt_position.Text) OrElse
           String.IsNullOrWhiteSpace(txt_sex.Text) OrElse
           String.IsNullOrWhiteSpace(txt_contact.Text) Then

            MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction()

            Try
                ' ✅ Correct update query for tbl_admin
                Dim query As String = "
                    UPDATE tbl_staff
                    SET 
                        username = @username,
                        password = @password,
                        first_name = @first_name,
                        last_name = @last_name,
                        email = @email,
                        contact_number = @contact_number,
                        position = @position,
                        address = @address,
                        sex = @sex
                    WHERE id = @id;
                "

                Using cmd As New MySqlCommand(query, conn, trans)
                    cmd.Parameters.AddWithValue("@username", txt_username.Text.Trim())
                    cmd.Parameters.AddWithValue("@password", txt_password.Text.Trim())
                    cmd.Parameters.AddWithValue("@first_name", txt_firstname.Text.Trim())
                    cmd.Parameters.AddWithValue("@last_name", txt_last.Text.Trim())
                    cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim())
                    cmd.Parameters.AddWithValue("@contact_number", txt_contact.Text.Trim())
                    cmd.Parameters.AddWithValue("@position", txt_position.Text.Trim())
                    cmd.Parameters.AddWithValue("@address", txt_address.Text.Trim())
                    cmd.Parameters.AddWithValue("@sex", txt_sex.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", StaffID)

                    cmd.ExecuteNonQuery()
                End Using

                trans.Commit()
                MessageBox.Show("Staff profile successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                Me.Close()

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error updating staff profile: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New StaffDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim manageStudent As New StaffStudent()
        manageStudent.Show()
        Me.Hide()
    End Sub



    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Dim manageProfile As New StaffEditProfileModal()

        If LoggedInRole = "Staff" AndAlso LoggedInStaffId > 0 Then
            manageProfile.StaffID = LoggedInStaffId
            manageProfile.Show()
            Me.Hide()
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
