
Option Strict Off
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System
Imports System.Configuration
Imports System.Data.SqlClient

Public Class FormCredentials

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        If TextBoxPassword.Text <> "" And TextBoxUserName.Text <> "" Then
            'OpenConnectionMySql(TextBoxhost.Text, TextBoxDatabase.Text, "root", "bitron")
            Dim  builder As  New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings("Morpheus").ConnectionString
            OpenConnectionMySql(builder("host"), builder("database") , builder("username"), builder("password"))

            If MySqlconnection.State = ConnectionState.Open Then

                strFtpServerUser = ParameterTable("MorpheusFtpUser")
                strFtpServerPsw = ParameterTable("MorpheusFtpPsw")


                Dim Adapter As New MySqlDataAdapter("SELECT * FROM credentials where username='" & TextBoxUserName.Text & "' and password='" & TextBoxPassword.Text & "'", MySqlconnection)
                Dim ds As New DataSet
                

                Adapter.Fill(ds)
                Dim tblCredentials As DataTable = ds.Tables(0)
                If tblCredentials.Rows.Count = 1 Then
                    Dim  connStr As  New Common.DbConnectionStringBuilder()
                    connStr.ConnectionString = ConfigurationManager.ConnectionStrings("Morpheus").ConnectionString

                    CreAccount.strUserName = LCase(TextBoxUserName.Text)
                    CreAccount.strPassword = LCase(TextBoxPassword.Text)                    
                    CreAccount.strHost = connStr("host")
                    CreAccount.strDatabase = connStr("database")
                    CreAccount.strSign = tblCredentials.Rows(0)("sign")
                    CreAccount.intId = tblCredentials.Rows(0)("id")
                    tblCredentials.Dispose()
                    Adapter.Dispose()
                    ds.Dispose()
                    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM ErrorTable", MySqlconnection)
                    AdapterProd.Fill(DsError, "ErrorTable")
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
        Else
            MsgBox("Fill in all fields!")
        End If

    End Sub

    Private Sub FormCredentials_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub FormCredentials_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim  builder As  New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings("Morpheus").ConnectionString

        TextBoxUserName.Text = ""
        'TextBoxhost.Text = "10.140.13.164"
        TextBoxPassword.Text = ""
        LabelHost.Text = "Host: " & builder("host")

    End Sub

   
    Private Sub TextBoxPassword_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxPassword.KeyPress
        If e.KeyChar = vbCr Then
            Button1_Click(Me, e)
        End If
    End Sub

End Class