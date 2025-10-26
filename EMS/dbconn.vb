Imports MySql.Data.MySqlClient
Imports System.IO

Module dbconn
    Public con As New MySqlConnection
    Public result As Boolean
    Public command_handler As MySqlCommand
    Public datareader As MySqlDataReader
    Public datadapter As MySqlDataAdapter
    Public i As Integer

    Public Function dbcon() As Boolean
        Try
            If con.State = ConnectionState.Closed Then
                con.ConnectionString = "server=localhost;user=root;password=;database=enrollment_management_system;port=3306;"
                con.Open()
                result = True
            End If
        Catch ex As Exception
            result = False
            MsgBox("❌ Server not connected. Error: " & ex.Message, vbExclamation)
        End Try
        Return result
    End Function
End Module
