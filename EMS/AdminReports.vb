Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection.Metadata
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MySql.Data.MySqlClient

Public Class AdminReports
    Private connectionString As String = "server=localhost;userid=root;password=;database=enrollment_management_system"

    ' 🔹 Navigate to Dashboard
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim dashboard As New AdminDashboard()
        dashboard.Show()
        Me.Hide()
    End Sub

    ' 🔹 Search students by name or ID number
    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        LoadReports(txt_search.Text.Trim())
    End Sub

    ' 🔹 Load report (student + billing + course)
    Private Sub LoadReports(Optional ByVal searchTerm As String = "")
        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open()

                Dim query As String = "
                    SELECT 
                        s.id,
                        s.id_number,
                        CONCAT(s.last_name, ', ', s.first_name) AS full_name,
                        s.year_section,
                        s.dob,
                        IFNULL(c.course_name, 'N/A') AS course_name,
                        IFNULL(c.description, '') AS course_description,
                        IFNULL(c.days, '') AS course_days,
                        IFNULL(c.time, '') AS course_time,
                        IFNULL(c.room, '') AS course_room,
                        IFNULL(b.amount_paid, 0) AS amount_paid,
                        IFNULL(b.balance, s.total) AS balance,
                        IFNULL(b.payment_date, CURDATE()) AS payment_date,
                        IFNULL(b.status, 'Unpaid') AS billing_status
                    FROM tbl_students s
                    LEFT JOIN tbl_student_courses sc ON s.id = sc.student_id
                    LEFT JOIN tbl_courses c ON sc.course_id = c.id
                    LEFT JOIN tbl_bill b ON s.id = b.student_id
                    WHERE s.status = 'Active'
                    AND (s.first_name LIKE @search OR s.last_name LIKE @search OR s.id_number LIKE @search)
                "

                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    Dim dt As New DataTable()
                    Dim adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)

                    Reports.DataSource = dt
                    Reports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' 🔹 Form Load
    Private Sub AdminReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadReports()
    End Sub

    ' 🔹 Logout
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        Dim logout As New Login()
        logout.Show()
        Me.Hide()
    End Sub




    ' 🔹 Export to PDF
    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        If Reports.Rows.Count = 0 Then
            MessageBox.Show("No data to export!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim pdfDoc As New iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20)
        Dim filePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StudentReports.pdf")

        Try
            PdfWriter.GetInstance(pdfDoc, New FileStream(filePath, FileMode.Create))
            pdfDoc.Open()

            ' --- Fix Fonts ---
            Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            Dim titleFont As New Font(baseFont, 16, Font.Bold)
            Dim headerFont As New Font(baseFont, 10, Font.Bold)
            Dim cellFont As New Font(baseFont, 9, Font.Bold)

            ' --- Title ---
            pdfDoc.Add(New Paragraph("STUDENT BILLING REPORT", titleFont))
            pdfDoc.Add(New Paragraph("Generated on: " & DateTime.Now.ToString() & vbCrLf))

            ' --- Table ---
            Dim table As New PdfPTable(Reports.Columns.Count)
            table.WidthPercentage = 100

            ' Add headers
            For Each column As DataGridViewColumn In Reports.Columns
                Dim cell As New PdfPCell(New Phrase(column.HeaderText, headerFont))
                cell.BackgroundColor = BaseColor.LIGHT_GRAY
                table.AddCell(cell)
            Next

            ' Add rows
            For Each row As DataGridViewRow In Reports.Rows
                If Not row.IsNewRow Then
                    For Each cell As DataGridViewCell In row.Cells
                        Dim text As String = If(cell.Value IsNot Nothing, cell.Value.ToString(), "")
                        table.AddCell(New Phrase(text, cellFont))
                    Next
                End If
            Next

            pdfDoc.Add(table)
            pdfDoc.Close()

            MessageBox.Show("✅ PDF exported successfully to Desktop!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error exporting PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
