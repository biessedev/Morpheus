
Option Strict Off
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System

Public Class FormCredentials

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        If TextBoxPassword.Text <> "" And TextBoxUserName.Text <> "" Then
            OpenConnectionMySql(TextBoxhost.Text, TextBoxDatabase.Text, "root", "bitron")

            If MySqlconnection.State = ConnectionState.Open Then

                strFtpServerUser = ParameterTable("MorpheusFtpUser")
                strFtpServerPsw = ParameterTable("MorpheusFtpPsw")

                Dim Adapter As New MySqlDataAdapter("SELECT * FROM credentials where username='" & TextBoxUserName.Text & "' and password='" & TextBoxPassword.Text & "'", MySqlconnection)
                Dim ds As New DataSet
                Adapter.Fill(ds)
                Dim tblCredentials As DataTable = ds.Tables(0)
                If tblCredentials.Rows.Count = 1 Then
                    CreAccount.strUserName = LCase(TextBoxUserName.Text)
                    CreAccount.strPassword = LCase(TextBoxPassword.Text)
                    CreAccount.strHost = TextBoxhost.Text
                    CreAccount.strDatabase = TextBoxDatabase.Text
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
                    DBName = UCase(TextBoxDatabase.Text)
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

        TextBoxUserName.Text = ""
        TextBoxhost.Text = "10.140.13.164"
        TextBoxPassword.Text = ""

    End Sub

    Private Sub TextBoxPassword_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles TextBoxPassword.KeyPress
        If e.KeyChar = vbCr Then
            Button1_Click(Me, e)
        End If
    End Sub

    Private Sub TextBoxUserName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxUserName.TextChanged
        If TextBoxUserName.Text = "demo" Then
            TextBoxDatabase.Text = "demo"
        End If
    End Sub

End Class