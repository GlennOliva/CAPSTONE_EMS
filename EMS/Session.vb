Imports MySql.Data.MySqlClient

Public Module SessionData
    ' 🔹 Store session info for both Admin and Staff
    Public LoggedInAdminId As Integer = 0
    Public LoggedInStaffId As Integer = 0
    Public LoggedInRole As String = ""    ' "Admin" or "Staff"
    Public LoggedInUsername As String = ""
End Module
