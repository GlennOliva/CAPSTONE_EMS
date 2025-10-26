Imports MySql.Data.MySqlClient

Public Class AdminCreateStudentModal

    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"



    Private Sub AdminCreateStudentModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSubjects()
    End Sub

    ' ✅ Load all subjects into ComboBoxes
    Private Sub LoadSubjects()
        Dim query As String = "SELECT id, course_name FROM tbl_courses"

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    Dim dt As New DataTable()
                    dt.Load(cmd.ExecuteReader())

                    ' ✅ Bind all subject ComboBoxes
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

    ' ✅ Display subject details (units, days, time, room) when selected
    Private Sub Subject_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cb_subject_one.SelectedIndexChanged, cb_subject_two.SelectedIndexChanged, cb_subject_three.SelectedIndexChanged, cb_subject_fourth.SelectedIndexChanged, cb_subject_fifth.SelectedIndexChanged, cb_subject_sixth.SelectedIndexChanged, cb_subject_seventh.SelectedIndexChanged, cb_subject_eight.SelectedIndexChanged

        Dim cb = DirectCast(sender, ComboBox)
        If cb.SelectedIndex = -1 Then Exit Sub

        If TypeOf cb.SelectedValue Is DataRowView Then Exit Sub
        Dim courseId = Convert.ToInt32(cb.SelectedValue)

        Dim query = "SELECT units, days, time, room FROM tbl_courses WHERE id = @id"

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", courseId)
                Try
                    conn.Open
                    Using reader = cmd.ExecuteReader
                        If reader.Read Then
                            Dim units = reader("units").ToString
                            Dim days = reader("days").ToString
                            Dim time = reader("time").ToString
                            Dim room = reader("room").ToString

                            Select Case cb.Name
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


    ' ✅ Automatically calculates total fees
    Private Sub CalculateTotal()
        Dim total As Decimal = 0

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

    ' ✅ Recalculate when any fee changes
    Private Sub FeeTextBox_TextChanged(sender As Object, e As EventArgs) _
        Handles txt_tuition.TextChanged, txt_reg.TextChanged, txt_med.TextChanged, txt_science.TextChanged,
                txt_cultural.TextChanged, txt_comlab.TextChanged, txt_guidance.TextChanged, txt_library.TextChanged,
                txt_athletic.TextChanged, txt_entrance.TextChanged, txt_college.TextChanged, txt_lab.TextChanged,
                txt_org.TextChanged, txt_other.TextChanged
        CalculateTotal()
    End Sub



    ' ✅ Insert new student record
    ' ✅ Insert new student record
    ' ✅ Insert new student record
    Private Sub btn_create_student_Click(sender As Object, e As EventArgs) Handles btn_create_student.Click
        If String.IsNullOrWhiteSpace(txt_firstname.Text) OrElse
       String.IsNullOrWhiteSpace(txt_surname.Text) OrElse
       String.IsNullOrWhiteSpace(txt_middle.Text) OrElse
       String.IsNullOrWhiteSpace(txt_year.Text) Then
            MessageBox.Show("Please fill in all required fields (Name, Middle Initial, Year).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using conn As New MySqlConnection(connectionString)
            conn.Open
            Dim trans = conn.BeginTransaction

            Try
                ' --- Insert Student ---
                Dim studentQuery As String =
"INSERT INTO tbl_students (
    id_number, sex, last_name, first_name, middle_initial, dob, year_section, address, contact,
    tuition, registration, medical, science_lab, computer_lab, guidance,
    library, athletic, cultural_dev, college_paper, late_registration,
    org, entrance_fee, others, total
) VALUES (
    @id_number, @sex, @last_name, @first_name, @middle_initial, @dob, @year_section, @address,@contact,
    @tuition, @registration, @medical, @science_lab, @computer_lab, @guidance,
    @library, @athletic, @cultural_dev, @college_paper, @late_registration,
    @org, @entrance_fee, @others, @total
);
SELECT LAST_INSERT_ID();"

                Dim studentCmd As New MySqlCommand(studentQuery, conn, trans)
                studentCmd.Parameters.AddWithValue("@id_number", txt_idnumber.Text.Trim())
                studentCmd.Parameters.AddWithValue("@sex", txt_sex.Text.Trim())
                studentCmd.Parameters.AddWithValue("@last_name", txt_surname.Text.Trim())
                studentCmd.Parameters.AddWithValue("@first_name", txt_firstname.Text.Trim())
                studentCmd.Parameters.AddWithValue("@middle_initial", txt_middle.Text.Trim())
                studentCmd.Parameters.AddWithValue("@dob", dtp_dob.Value)
                studentCmd.Parameters.AddWithValue("@address", txt_address.Text.Trim())
                studentCmd.Parameters.AddWithValue("@contact", txt_contact.Text.Trim())
                studentCmd.Parameters.AddWithValue("@year_section", txt_year.Text.Trim())

                ' Billing values
                studentCmd.Parameters.AddWithValue("@tuition", Val(txt_tuition.Text))
                studentCmd.Parameters.AddWithValue("@registration", Val(txt_reg.Text))
                studentCmd.Parameters.AddWithValue("@medical", Val(txt_med.Text))
                studentCmd.Parameters.AddWithValue("@science_lab", Val(txt_science.Text))
                studentCmd.Parameters.AddWithValue("@computer_lab", Val(txt_comlab.Text))
                studentCmd.Parameters.AddWithValue("@guidance", Val(txt_guidance.Text))
                studentCmd.Parameters.AddWithValue("@library", Val(txt_library.Text))
                studentCmd.Parameters.AddWithValue("@athletic", Val(txt_athletic.Text))
                studentCmd.Parameters.AddWithValue("@cultural_dev", Val(txt_cultural.Text))
                studentCmd.Parameters.AddWithValue("@college_paper", Val(txt_college.Text))
                studentCmd.Parameters.AddWithValue("@late_registration", Val(txt_lab.Text))
                studentCmd.Parameters.AddWithValue("@org", Val(txt_org.Text))
                studentCmd.Parameters.AddWithValue("@entrance_fee", Val(txt_entrance.Text))
                studentCmd.Parameters.AddWithValue("@others", Val(txt_other.Text))
                studentCmd.Parameters.AddWithValue("@total", Val(txt_total.Text))



                Dim newStudentId = Convert.ToInt32(studentCmd.ExecuteScalar)

                ' --- Collect all selected course IDs ---
                ' --- Collect all selected course IDs ---
                Dim subjectBoxes = {cb_subject_one, cb_subject_two, cb_subject_three, cb_subject_fourth,
                              cb_subject_fifth, cb_subject_sixth, cb_subject_seventh, cb_subject_eight}

                ' ✅ Use INSERT IGNORE to skip duplicates safely
                Dim insertCourseQuery = "INSERT IGNORE INTO tbl_student_courses (student_id, course_id) VALUES (@student_id, @course_id)"

                For Each cb In subjectBoxes
                    If cb.SelectedValue IsNot Nothing AndAlso Not TypeOf cb.SelectedValue Is DataRowView Then
                        Dim courseId = Convert.ToInt32(cb.SelectedValue)
                        Dim courseCmd As New MySqlCommand(insertCourseQuery, conn, trans)
                        courseCmd.Parameters.AddWithValue("@student_id", newStudentId)
                        courseCmd.Parameters.AddWithValue("@course_id", courseId)
                        Try
                            courseCmd.ExecuteNonQuery
                        Catch ex As MySqlException
                            ' ✅ Ignore duplicate entry errors (error code 1062)
                            If ex.Number <> 1062 Then
                                Throw
                            End If
                        End Try
                    End If
                Next


                ' Commit the transaction
                trans.Commit
                MessageBox.Show("Student and selected courses successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields

            Catch ex As Exception
                trans.Rollback
                MessageBox.Show("Error inserting student and courses: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub



    Private Sub ClearFields()
        ' --- Personal info ---
        txt_surname.Clear()
        txt_firstname.Clear()
        txt_middle.Clear()
        txt_address.Clear()
        txt_contact.Clear()
        txt_year.Clear()
        txt_id.Clear()

        ' --- Dates ---
        dtp_dob.Value = DateTime.Now

        ' --- Subjects ---
        Dim subjectCbs = {cb_subject_one, cb_subject_two, cb_subject_three, cb_subject_fourth,
                           cb_subject_fifth, cb_subject_sixth, cb_subject_seventh, cb_subject_eight}

        For Each cb As ComboBox In subjectCbs
            cb.SelectedIndex = -1
        Next

        ' --- Units ---
        Dim unitTxts = {txt_units_one, txt_units_two, txt_units_three, txt_units_fourth,
                        txt_units_fifth, txt_units_sixth, txt_units_seventh, txt_units_eigth}
        For Each txt As TextBox In unitTxts
            txt.Clear()
        Next

        ' --- Days ---
        Dim daysTxts = {txt_days_one, txt_days_two, txt_days_three, txt_days_fourth,
                        txt_days_fifth, txt_days_sixth, txt_days_seventh, txt_days_eigth}
        For Each txt As TextBox In daysTxts
            txt.Clear()
        Next

        ' --- Times ---
        Dim timeTxts = {txt_time_one, txt_time_two, txt_time_three, txt_time_fourth,
                        txt_time_fifth, txt_time_sixth, txt_time_seventh, txt_time_eight}
        For Each txt As TextBox In timeTxts
            txt.Clear()
        Next

        ' --- Rooms ---
        Dim roomTxts = {txt_room_one, txt_room_two, txt_room_three, txt_room_fourth,
                        txt_room_fifth, txt_room_sixth, txt_room_seventh, txt_room_eight}
        For Each txt As TextBox In roomTxts
            txt.Clear()
        Next

        ' --- Fees ---
        Dim feeTxts = {txt_tuition, txt_reg, txt_med, txt_science, txt_cultural, txt_comlab,
                       txt_guidance, txt_library, txt_athletic, txt_entrance, txt_college,
                       txt_lab, txt_org, txt_other}
        For Each txt As TextBox In feeTxts
            txt.Clear()
        Next

        ' --- Total ---
        txt_total.Text = "0.00"
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
