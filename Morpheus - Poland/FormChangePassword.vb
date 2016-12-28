Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Configuration

Public Class FormChangePassword

    Dim tblCred As DataTable
    Dim DsCred As New DataSet

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub FormChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToParent()
        Me.ActiveControl = TextBoxOldPass
        Me.AcceptButton = ButtonSave
        Me.Text = "Change password for user " + CreAccount.strUserName

        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using Adapter As New MySqlDataAdapter("SELECT * FROM Credentials", con)
		        Adapter.Fill(DsCred, "credential")
	        End Using
        End Using
        tblCred = DsCred.Tables("credential")
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Dim users = tblCred.Select("username = '" & CreAccount.strUserName & "'")
        If users.Length <> 0 Then

            If TextBoxOldPass.Text <> "" And TextBoxNewPass.Text.Trim <> "" And TextBoxPassCheck.Text.Trim <> "" Then

                If users(0).Item(1) = TextBoxOldPass.Text And TextBoxNewPass.Text = TextBoxPassCheck.Text Then

                    If TextBoxOldPass.Text = TextBoxNewPass.Text Then
                        LabelForValidation.ForeColor = Color.Red
                        LabelForValidation.Text = "New password is the same as old password."
                    Else
                        Dim builder As New Common.DbConnectionStringBuilder()
                        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                            Dim sqlquery = "UPDATE credentials SET password = '" + TextBoxPassCheck.Text + "' WHERE username = '" & CreAccount.strUserName & "'"
                            Dim cmd As MySqlCommand = New MySqlCommand(sqlquery, con)
                            cmd.ExecuteNonQuery()
                        End Using
                        MsgBox("Your password was successfully changed", vbOKOnly)
                        Me.Close()
                    End If
                ElseIf users(0).Item(1) <> TextBoxOldPass.Text Then
                    LabelForValidation.ForeColor = Color.Red
                    LabelForValidation.Text = "Your password does not corespond with the password from " + Environment.NewLine + "'Old password' field"
                ElseIf TextBoxNewPass.Text <> TextBoxPassCheck.Text Then
                    LabelForValidation.ForeColor = Color.Red
                    LabelForValidation.Text = "The new password must be the same in 'New password' field " + Environment.NewLine + " and in 'Retype new password'"
                End If
            Else
                LabelForValidation.ForeColor = Color.Red
                LabelForValidation.Text = "You must complete all the fields "
            End If
        End If
    End Sub


    Private Sub CheckBoxShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowPassword.CheckedChanged
        If CheckBoxShowPassword.CheckState = CheckState.Checked Then
            TextBoxNewPass.PasswordChar = ""
            TextBoxPassCheck.PasswordChar = ""
            TextBoxOldPass.PasswordChar = ""
        Else
            TextBoxNewPass.PasswordChar = "*"
            TextBoxPassCheck.PasswordChar = "*"
            TextBoxOldPass.PasswordChar = "*"
        End If
    End Sub
End Class