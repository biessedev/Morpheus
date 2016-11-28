
Option Strict Off
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System
Imports System.Configuration
Imports System.Data.SqlClient

Public Class FormCredentials

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click       
        If TextBoxPassword.Text <> "" And TextBoxUserName.Text <> "" Then
            Dim  builder As  New Common.DbConnectionStringBuilder()

            hostName = ComboBoxHost.Text.Substring(0,InStr(ComboBoxHost.Text, " - ") - 1)
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            'OpenConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))            
                If con.State = ConnectionState.Open Then

                    strFtpServerUser = ParameterTable("MorpheusFtpUser")
                    strFtpServerPsw = ParameterTable("MorpheusFtpPsw")
                    
                    Dim ds As New DataSet
                    Using Adapter As New MySqlDataAdapter("SELECT * FROM credentials where username='" & TextBoxUserName.Text & "' and password='" & TextBoxPassword.Text & "'", con)
		                Adapter.Fill(ds)
	                End Using
                
                    Dim tblCredentials As DataTable = ds.Tables(0)
                    If tblCredentials.Rows.Count = 1 Then
                        Dim  connStr As  New Common.DbConnectionStringBuilder()
                        connStr.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString

                        CreAccount.strUserName = LCase(TextBoxUserName.Text)
                        CreAccount.strPassword = LCase(TextBoxPassword.Text)                    
                        CreAccount.strHost = connStr("host")
                        CreAccount.strDatabase = connStr("database")
                        CreAccount.strSign = tblCredentials.Rows(0)("sign")
                        CreAccount.intId = tblCredentials.Rows(0)("id")
                        tblCredentials.Dispose()
                        ds.Dispose()
                        Using Adapter As New MySqlDataAdapter("SELECT * FROM ErrorTable", con)
		                    Adapter.Fill(DsError, "ErrorTable")
	                    End Using
                        tblError = DsError.Tables("ErrorTable")
                        Me.Hide()
                        FormStart.Show()
                        FormStart.Focus()
                        DBName = UCase(connStr("database"))
                        strFtpServerAdd = ParameterTable("PathDocument") & DBName & "/"
                    Else
                        MsgBox("Database account error, check password and username")
                    End If
                End If
            End Using
        Else
            MsgBox("Fill in all fields!")
        End If

    End Sub

    Private Sub FormCredentials_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub FormCredentials_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim  builder As  New Common.DbConnectionStringBuilder()
        'builder.ConnectionString = ConfigurationManager.ConnectionStrings("Morpheus").ConnectionString
        'ComboBoxHost.Add("")
        For Each conn  As ConnectionStringSettings in ConfigurationManager.ConnectionStrings
            builder.Clear()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(conn.Name).ConnectionString
            If builder("connectionType") = "MainConnections" Then
                'builder.Clear()
                'builder.ConnectionString = ConfigurationManager.ConnectionStrings(conn.Name).ConnectionString
                ComboBoxHost.Items.Add(conn.Name & " - " & builder("host"))    
            End If            
        Next
        ComboBoxHost.SelectedIndex = 0
        TextBoxUserName.Text = ""
        'TextBoxhost.Text = "10.140.13.164"
        TextBoxPassword.Text = ""
        LabelHost.Text = "Host: " '& builder("host")

    End Sub

   
    Private Sub TextBoxPassword_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxPassword.KeyPress
        If e.KeyChar = vbCr Then
            Button1_Click(Me, e)
        End If
    End Sub

End Class