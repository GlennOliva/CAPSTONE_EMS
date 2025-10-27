Imports MySql.Data.MySqlClient

Public Class Login

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        ' Connect to database
        If dbcon() = False Then
            MsgBox("Database connection failed!", vbExclamation)
            Exit Sub
        End If

        Dim username As String = txt_username.Text.Trim()
        Dim password As String = txt_password.Text.Trim()

        ' Basic input validation
        If username = "" Or password = "" Then
            MsgBox("Please enter username and password.", vbExclamation)
            Exit Sub
        End If

        Try
            ' === Try login as Admin first ===
            Dim queryAdmin As String =
                "SELECT * FROM tbl_admin WHERE username=@username AND password=@password"
            Using cmd As New MySqlCommand(queryAdmin, con)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                If dr.Read() Then
                    ' Check if admin account is inactive
                    If dr("status").ToString().ToLower() = "inactive" Then
                        dr.Close()
                        MsgBox("⚠️ Your admin account is inactive. Please contact the system administrator.", vbExclamation)
                        Exit Sub
                    End If

                    ' Proceed if active
                    LoggedInAdminId = Convert.ToInt32(dr("id"))
                    LoggedInStaffId = 0
                    LoggedInRole = "Admin"
                    LoggedInUsername = username
                    dr.Close()

                    MsgBox("✅ Welcome Admin " & username & "!", vbInformation)
                    Dim adminForm As New AdminDashboard()
                    adminForm.Show()
                    Me.Hide()
                    Exit Sub
                End If

                dr.Close()
            End Using

            ' === Try login as Staff ===
            Dim queryStaff As String =
                "SELECT * FROM tbl_staff WHERE username=@username AND password=@password"
            Using cmd As New MySqlCommand(queryStaff, con)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                If dr.Read() Then
                    ' Check if staff account is inactive
                    If dr("status").ToString().ToLower() = "inactive" Then
                        dr.Close()
                        MsgBox("⚠️ Your staff account is inactive. Please contact the administrator.", vbExclamation)
                        Exit Sub
                    End If

                    ' Proceed if active
                    LoggedInStaffId = Convert.ToInt32(dr("id"))
                    LoggedInAdminId = 0
                    LoggedInRole = "Staff"
                    LoggedInUsername = username
                    dr.Close()

                    MsgBox("👩‍💼 Welcome Staff " & username & "!", vbInformation)
                    Dim staffForm As New StaffDashboard()
                    staffForm.Show()
                    Me.Hide()
                    Exit Sub
                End If

                dr.Close()
            End Using

            MsgBox("❌ Invalid username or password!", vbCritical)

        Catch ex As Exception
            MsgBox("Error during login: " & ex.Message, vbCritical)
        Finally
            con.Close()
        End Try
    End Sub

End Class
