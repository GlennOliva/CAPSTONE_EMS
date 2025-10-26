Imports MySql.Data.MySqlClient

Public Class AdminEditStudentModal

    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"
    Public StudentId As Integer

    Private Sub AdminEditStudentModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSubjects()

        ' When editing, load existing student data
        If StudentId > 0 Then
            LoadStudentData(StudentId)

            ' Refresh subject details after data load
            RefreshAllSubjectDetails()

            ' ✅ Automatically calculate total after loading
            CalculateTotal()
        End If
    End Sub

    ' ✅ Auto-calculate total fees
    Private Sub CalculateTotal()
        Dim total As Decimal = 0D

        Dim fees() As TextBox = {
            txt_tuition, txt_reg, txt_med, txt_science, txt_cultural, txt_comlab,
            txt_guidance, txt_library, txt_athletic, txt_entrance, txt_college,
            txt_lab, txt_org, txt_other
        }

        For Each feeBox As TextBox In fees
            Dim amount As Decimal
            If Decimal.TryParse(feeBox.Text, amount) Then
                total += amount
            End If
        Next

        txt_total.Text = total.ToString("N2")
    End Sub

    ' ✅ Recalculate total whenever any fee textbox changes
    Private Sub FeeTextBox_TextChanged(sender As Object, e As EventArgs) _
        Handles txt_tuition.TextChanged, txt_reg.TextChanged, txt_med.TextChanged, txt_science.TextChanged,
                txt_cultural.TextChanged, txt_comlab.TextChanged, txt_guidance.TextChanged, txt_library.TextChanged,
                txt_athletic.TextChanged, txt_entrance.TextChanged, txt_college.TextChanged, txt_lab.TextChanged,
                txt_org.TextChanged, txt_other.TextChanged
        CalculateTotal()
    End Sub

    ' ✅ Load subjects into comboboxes
    Private Sub LoadSubjects()
        Dim query As String = "SELECT id, course_name FROM tbl_courses"
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim dt As New DataTable()
                    dt.Load(cmd.ExecuteReader())

                    For Each cb As ComboBox In {cb_subject_one, cb_subject_two, cb_subject_three, cb_subject_fourth,
                                                cb_subject_fifth, cb_subject_sixth, cb_subject_seventh, cb_subject_eight}
                        cb.DataSource = dt.Copy()
                        cb.DisplayMember = "course_name"
                        cb.ValueMember = "id"
                        cb.SelectedIndex = -1
                    Next
                Catch ex As Exception
                    MessageBox.Show("Error loading subjects: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' ✅ Triggered when changing subject selection
    Private Sub Subject_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cb_subject_one.SelectedIndexChanged, cb_subject_two.SelectedIndexChanged, cb_subject_three.SelectedIndexChanged,
                cb_subject_fourth.SelectedIndexChanged, cb_subject_fifth.SelectedIndexChanged, cb_subject_sixth.SelectedIndexChanged,
                cb_subject_seventh.SelectedIndexChanged, cb_subject_eight.SelectedIndexChanged

        Dim cb = DirectCast(sender, ComboBox)
        If cb.SelectedIndex = -1 Then Exit Sub
        If TypeOf cb.SelectedValue Is DataRowView Then Exit Sub

        Dim courseId = Convert.ToInt32(cb.SelectedValue)
        LoadCourseDetailsToFields(cb.Name, courseId)
    End Sub

    ' ✅ Load course details into corresponding text fields
    Private Sub LoadCourseDetailsToFields(comboName As String, courseId As Integer)
        Dim query = "SELECT units, days, time, room FROM tbl_courses WHERE id = @id"

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", courseId)
                Try
                    conn.Open()
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim units = reader("units").ToString()
                            Dim days = reader("days").ToString()
                            Dim time = reader("time").ToString()
                            Dim room = reader("room").ToString()

                            Select Case comboName
                                Case "cb_subject_one"
                                    txt_units_one.Text = units
                                    txt_days_one.Text = days
                                    txt_time_one.Text = time
                                    txt_room_one.Text = room
                                Case "cb_subject_two"
                                    txt_units_two.Text = units
                                    txt_days_two.Text = days
                                    txt_time_two.Text = time
                                    txt_room_two.Text = room
                                Case "cb_subject_three"
                                    txt_units_three.Text = units
                                    txt_days_three.Text = days
                                    txt_time_three.Text = time
                                    txt_room_three.Text = room
                                Case "cb_subject_fourth"
                                    txt_units_fourth.Text = units
                                    txt_days_fourth.Text = days
                                    txt_time_fourth.Text = time
                                    txt_room_fourth.Text = room
                                Case "cb_subject_fifth"
                                    txt_units_fifth.Text = units
                                    txt_days_fifth.Text = days
                                    txt_time_fifth.Text = time
                                    txt_room_fifth.Text = room
                                Case "cb_subject_sixth"
                                    txt_units_sixth.Text = units
                                    txt_days_sixth.Text = days
                                    txt_time_sixth.Text = time
                                    txt_room_sixth.Text = room
                                Case "cb_subject_seventh"
                                    txt_units_seventh.Text = units
                                    txt_days_seventh.Text = days
                                    txt_time_seventh.Text = time
                                    txt_room_seventh.Text = room
                                Case "cb_subject_eight"
                                    txt_units_eigth.Text = units
                                    txt_days_eigth.Text = days
                                    txt_time_eight.Text = time
                                    txt_room_eight.Text = room
                            End Select
                        End If
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error loading subject details: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' ✅ Load student and subjects
    Private Sub LoadStudentData(studentId As Integer)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            Dim query As String = "SELECT * FROM tbl_students WHERE id = @id"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", studentId)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        txt_idnumber.Text = reader("id_number").ToString()
                        txt_surname.Text = reader("last_name").ToString()
                        txt_firstname.Text = reader("first_name").ToString()
                        txt_middle.Text = reader("middle_initial").ToString()
                        txt_year.Text = reader("year_section").ToString()
                        dtp_dob.Value = Convert.ToDateTime(reader("dob"))
                        txt_contact.Text = reader("contact").ToString()
                        txt_address.Text = reader("address").ToString()
                        txt_tuition.Text = reader("tuition").ToString()
                        txt_reg.Text = reader("registration").ToString()
                        txt_med.Text = reader("medical").ToString()
                        txt_science.Text = reader("science_lab").ToString()
                        txt_comlab.Text = reader("computer_lab").ToString()
                        txt_guidance.Text = reader("guidance").ToString()
                        txt_library.Text = reader("library").ToString()
                        txt_athletic.Text = reader("athletic").ToString()
                        txt_cultural.Text = reader("cultural_dev").ToString()
                        txt_college.Text = reader("college_paper").ToString()
                        txt_lab.Text = reader("late_registration").ToString()
                        txt_org.Text = reader("org").ToString()
                        txt_entrance.Text = reader("entrance_fee").ToString()
                        txt_other.Text = reader("others").ToString()
                        txt_total.Text = reader("total").ToString()
                    End If
                End Using
            End Using
        End Using
    End Sub

    ' ✅ Refresh all subject details after loading
    Private Sub RefreshAllSubjectDetails()
        For Each cb As ComboBox In {cb_subject_one, cb_subject_two, cb_subject_three, cb_subject_fourth,
                                    cb_subject_fifth, cb_subject_sixth, cb_subject_seventh, cb_subject_eight}
            If cb.SelectedValue IsNot Nothing AndAlso Not TypeOf cb.SelectedValue Is DataRowView Then
                LoadCourseDetailsToFields(cb.Name, Convert.ToInt32(cb.SelectedValue))
            End If
        Next
    End Sub

    ' ✅ Save updates to DB
    Private Sub btn_edit_student_Click(sender As Object, e As EventArgs) Handles btn_edit_student.Click
        If StudentId <= 0 Then
            MessageBox.Show("Invalid student selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim trans = conn.BeginTransaction
            Try
                ' Update student info
                Dim updateQuery As String = "UPDATE tbl_students SET 
                    id_number=@id_number, last_name=@last_name, first_name=@first_name, middle_initial=@middle_initial,
                    dob=@dob, year_section=@year_section, address=@address, contact=@contact, tuition=@tuition, 
                    registration=@registration, medical=@medical, science_lab=@science_lab, computer_lab=@computer_lab, 
                    guidance=@guidance, library=@library, athletic=@athletic, cultural_dev=@cultural_dev, 
                    college_paper=@college_paper, late_registration=@late_registration, org=@org, 
                    entrance_fee=@entrance_fee, others=@others, total=@total WHERE id=@id"

                Dim cmd As New MySqlCommand(updateQuery, conn, trans)
                cmd.Parameters.AddWithValue("@id_number", txt_idnumber.Text.Trim())
                cmd.Parameters.AddWithValue("@last_name", txt_surname.Text.Trim())
                cmd.Parameters.AddWithValue("@first_name", txt_firstname.Text.Trim())
                cmd.Parameters.AddWithValue("@middle_initial", txt_middle.Text.Trim())
                cmd.Parameters.AddWithValue("@dob", dtp_dob.Value)
                cmd.Parameters.AddWithValue("@year_section", txt_year.Text.Trim())
                cmd.Parameters.AddWithValue("@address", txt_address.Text.Trim())
                cmd.Parameters.AddWithValue("@contact", txt_contact.Text.Trim())
                cmd.Parameters.AddWithValue("@tuition", Val(txt_tuition.Text))
                cmd.Parameters.AddWithValue("@registration", Val(txt_reg.Text))
                cmd.Parameters.AddWithValue("@medical", Val(txt_med.Text))
                cmd.Parameters.AddWithValue("@science_lab", Val(txt_science.Text))
                cmd.Parameters.AddWithValue("@computer_lab", Val(txt_comlab.Text))
                cmd.Parameters.AddWithValue("@guidance", Val(txt_guidance.Text))
                cmd.Parameters.AddWithValue("@library", Val(txt_library.Text))
                cmd.Parameters.AddWithValue("@athletic", Val(txt_athletic.Text))
                cmd.Parameters.AddWithValue("@cultural_dev", Val(txt_cultural.Text))
                cmd.Parameters.AddWithValue("@college_paper", Val(txt_college.Text))
                cmd.Parameters.AddWithValue("@late_registration", Val(txt_lab.Text))
                cmd.Parameters.AddWithValue("@org", Val(txt_org.Text))
                cmd.Parameters.AddWithValue("@entrance_fee", Val(txt_entrance.Text))
                cmd.Parameters.AddWithValue("@others", Val(txt_other.Text))
                cmd.Parameters.AddWithValue("@total", Val(txt_total.Text))
                cmd.Parameters.AddWithValue("@id", StudentId)

                cmd.ExecuteNonQuery()
                trans.Commit()
                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error updating student: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim managestudent As New AdminStudent()
        managestudent.Show()
        Me.Hide()
    End Sub

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
