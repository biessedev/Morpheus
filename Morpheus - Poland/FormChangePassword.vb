Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text.RegularExpressions


Public Class FormChangePassword
    Dim AdapterCred As New MySqlDataAdapter("SELECT * FROM Credentials", MySqlconnection)
    Dim tblCred As DataTable
    Dim DsCred As New DataSet

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click 
        me.Close()
    End Sub

    Private Sub FormChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load 
        CenterToParent()
        Me.ActiveControl = TextBoxOldPass
        Me.AcceptButton = ButtonSave
        Me.Text = "Change password for user " + CreAccount.strUserName
        AdapterCred.Fill(DsCred, "credential")
        tblCred = DsCred.Tables("credential")
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click 
            
         dim users = tblCred.Select("username = '" & CreAccount.strUserName & "'")
        if users.Length <> 0
            if TextBoxOldPass.Text <> "" And TextBoxNewPass.Text.Trim <> "" and TextBoxPassCheck.Text.Trim <>""
                if users(0).Item(1) = TextBoxOldPass.Text and TextBoxNewPass.Text = TextBoxPassCheck.Text
                    If TextBoxOldPass.Text = TextBoxNewPass.Text
                        LabelForValidation.ForeColor = Color.Red
                        LabelForValidation.Text = "New password is the same as old password."
                    Else
                        Dim sqlquery = "UPDATE credentials SET password = '" + TextBoxPassCheck.Text + "' WHERE username = '" & CreAccount.strUserName & "'"
                        Dim cmd   As MySqlCommand = New MySqlCommand(sqlquery, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        'LabelForValidation.ForeColor = Color.DarkGreen
                        'LabelForValidation.Text = "Your password was changed"
                        MsgBox("Your password was successfully changed", vbOKOnly)
                        me.Close()
                    End If
                Else if users(0).Item(1) <> TextBoxOldPass.Text
                    LabelForValidation.ForeColor = Color.Red
                    LabelForValidation.Text = "Your password does not corespond with the password from " + Environment.NewLine + "'Old password' field"
                Else if TextBoxNewPass.Text <> TextBoxPassCheck.Text
                    LabelForValidation.ForeColor = Color.Red
                    LabelForValidation.Text = "The new password must be the same in 'New password' field " + Environment.NewLine + " and in 'Retype new password'"
                
                End if
            else
                LabelForValidation.ForeColor = Color.Red
                LabelForValidation.Text = "You must complete all the fields "
            end if
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