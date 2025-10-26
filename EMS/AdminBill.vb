Imports MySql.Data.MySqlClient

Public Class AdminBill
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' 🔹 Navigate to Dashboard
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    ' 🔹 Search students by name or ID number
    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        LoadStudentBilling(txt_search.Text.Trim())
    End Sub

    ' 🔹 Load student billing records
    Private Sub LoadStudentBilling(Optional ByVal searchTerm As String = "")
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()

                Dim query As String = "
                    SELECT 
                        s.id, 
                        s.id_number, 
                        s.last_name, 
                        s.first_name, 
                        s.sex, 
                        s.dob, 
                        s.year_section, 
                        s.total, 
                        IFNULL(b.amount_paid, 0) AS amount_paid,
                        IFNULL(b.balance, s.total) AS balance,
                        IFNULL(b.status, 'Unpaid') AS billing_status
                    FROM tbl_students s
                    LEFT JOIN tbl_bill b ON s.id = b.student_id
                    WHERE s.status = 'Active'
                    AND (s.first_name LIKE @search OR s.last_name LIKE @search OR s.id_number LIKE @search)
                "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    Dim dt As New DataTable()
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)

                    Bills.DataSource = dt
                    Bills.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading student billing data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' 🔹 Form Load
    Private Sub AdminBill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudentBilling()
    End Sub

    ' 🔹 Logout
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub

    ' 🔹 Mark as Paid (Full Payment)
    Private Sub btn_paid_Click(sender As Object, e As EventArgs) Handles btn_paid.Click
        If Bills.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim studentId As Integer = Convert.ToInt32(Bills.SelectedRows(0).Cells("id").Value)
        Dim totalAmount As Decimal = Convert.ToDecimal(Bills.SelectedRows(0).Cells("total").Value)

        SaveOrUpdateBill(studentId, totalAmount, totalAmount, 0, "Paid")
    End Sub

    ' 🔹 Mark as Unpaid (No Payment)
    Private Sub btn_unpaid_Click(sender As Object, e As EventArgs) Handles btn_unpaid.Click
        If Bills.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim studentId As Integer = Convert.ToInt32(Bills.SelectedRows(0).Cells("id").Value)
        Dim totalAmount As Decimal = Convert.ToDecimal(Bills.SelectedRows(0).Cells("total").Value)

        SaveOrUpdateBill(studentId, 0, 0, totalAmount, "Unpaid")
    End Sub

    ' 🔹 Partial Payment (Admin enters amount)
    Private Sub btn_partial_Click(sender As Object, e As EventArgs) Handles btn_partial.Click
        If Bills.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a student first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim studentId As Integer = Convert.ToInt32(Bills.SelectedRows(0).Cells("id").Value)
        Dim totalAmount As Decimal = Convert.ToDecimal(Bills.SelectedRows(0).Cells("total").Value)

        Dim input As String = InputBox("Enter amount paid by student:", "Partial Payment", "0")
        Dim amountPaid As Decimal

        If Not Decimal.TryParse(input, amountPaid) OrElse amountPaid < 0 Then
            MessageBox.Show("Invalid amount entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim balance As Decimal = totalAmount - amountPaid
        If balance < 0 Then balance = 0

        Dim status As String = If(balance = 0, "Paid", "Unpaid")

        SaveOrUpdateBill(studentId, amountPaid, amountPaid, balance, status)
    End Sub

    ' 🔹 Save or Update Billing Record
    Private Sub SaveOrUpdateBill(studentId As Integer, amountPaid As Decimal, totalPaid As Decimal, balance As Decimal, status As String)
        Using conn As New MySqlConnection(connectionString)
            conn.Open()

            Dim checkQuery As String = "SELECT COUNT(*) FROM tbl_bill WHERE student_id = @student_id"
            Using checkCmd As New MySqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@student_id", studentId)
                Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                Dim query As String
                If exists > 0 Then
                    query = "UPDATE tbl_bill 
                             SET amount_paid=@amount_paid, balance=@balance, status=@status, payment_date=CURDATE() 
                             WHERE student_id=@student_id"
                Else
                    query = "INSERT INTO tbl_bill (student_id, amount_paid, balance, payment_date, status)
                             VALUES (@student_id, @amount_paid, @balance, CURDATE(), @status)"
                End If

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@student_id", studentId)
                    cmd.Parameters.AddWithValue("@amount_paid", amountPaid)
                    cmd.Parameters.AddWithValue("@balance", balance)
                    cmd.Parameters.AddWithValue("@status", status)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Using

        MessageBox.Show("Billing updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LoadStudentBilling()
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
End Class
