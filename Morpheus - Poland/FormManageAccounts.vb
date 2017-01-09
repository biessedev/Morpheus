Imports System.Configuration
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class FormManageAccounts
    Dim tblCred As DataTable
    Dim DsCred As New DataSet

    Private Sub FormChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToParent()
        Me.AcceptButton = ButtonSave
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterCred As New MySqlDataAdapter("SELECT * FROM Credentials ORDER BY username asc", con)
                AdapterCred.Fill(DsCred, "credential")
                tblCred = DsCred.Tables("credential")
            End Using
        End Using
        fillList()
    End Sub

    Private Sub fillList()
        DsCred.Clear()
        tblCred.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterCred As New MySqlDataAdapter("SELECT * FROM Credentials ORDER BY username asc", con)
                AdapterCred.Update(DsCred, "credential")
                AdapterCred.Fill(DsCred, "credential")
            End Using
        End Using
        tblCred = DsCred.Tables("credential")
        Dim users = tblCred.Select()
        ListViewForUsers.Refresh()
        Dim h1 As New ColumnHeader
        Dim h2 As New ColumnHeader
        Dim h3 As New ColumnHeader
        h1.Text = "USERNAME"
        h1.Width = 125
        h2.Text = "PASSWORD"
        h2.Width = 125
        h3.Text = "SIGN"
        h3.Width = 250
        ListViewForUsers.Columns.Clear()
        ListViewForUsers.Columns.Add(h1)
        ListViewForUsers.Columns.Add(h2)
        ListViewForUsers.Columns.Add(h3)
        ListViewForUsers.Items.Clear()

        For Each usr In users
            Dim str(3) As String
            str(0) = usr(0)
            str(1) = usr(1)
            str(2) = usr(2)
            Dim ii As New ListViewItem(str)
            ListViewForUsers.Items.Add(ii)
        Next
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Dim sql As String
        If TextBoxForUsername.Text <> "" And TextBoxForPassword.Text <> "" And TextBoxForSign.Text <> "" And ListViewForUsers.SelectedItems.Count > 0 Then
            Try
                Dim returnValue As Boolean
                returnValue = Regex.IsMatch(TextBoxForSign.Text.ToUpper.Trim, "R[0-9]J[0-9]E[0-9]B[0-9]Q[0-9]N[0-9]P[0-9]U[0-9]F[0-9]L[0-9]C[0-9]I[0-9]A[0-9]T[0-9]W[0-9]Z[0-9]$")
                If returnValue = True Then
                    Dim builder As New Common.DbConnectionStringBuilder()
                    builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                    Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                        sql = "UPDATE `" & DBName & "`.`credentials` SET `username` = '" & TextBoxForUsername.Text & "', `password` = '" & TextBoxForPassword.Text & "', `sign` = '" & TextBoxForSign.Text & "' WHERE `username` = '" & ListViewForUsers.SelectedItems.Item(0).SubItems(0).Text & "' AND `password` = '" & ListViewForUsers.SelectedItems.Item(0).SubItems(1).Text & "' ;"
                        Dim cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    End Using
                    MsgBox("Profile has been successfully updated!", vbOKOnly)
                    fillList()
                    TextBoxForUsername.Text = ""
                    TextBoxForPassword.Text = ""
                    TextBoxForSign.Text = ""
                Else
                    MsgBox("The 'Sign' is not valid", vbOKOnly)
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub ListViewForUsers_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListViewForUsers.ItemSelectionChanged
        If ListViewForUsers.SelectedItems.Count = 1 Then
            TextBoxForUsername.Text = ListViewForUsers.SelectedItems.Item(0).SubItems(0).Text.ToString()
            TextBoxForPassword.Text = ListViewForUsers.SelectedItems.Item(0).SubItems(1).Text.ToString()
            TextBoxForSign.Text = ListViewForUsers.SelectedItems.Item(0).SubItems(2).Text.ToString()
        End If
    End Sub

    Private Sub ButtonRemove_Click(sender As Object, e As EventArgs) Handles ButtonRemove.Click
        Dim sql As String
        If ListViewForUsers.SelectedItems.Count = 1 Then
            Try
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    sql = "DELETE FROM `" & DBName & "`.`credentials` WHERE `Username` = '" & TextBoxForUsername.Text &
                        "' and `password` = '" & TextBoxForPassword.Text & "' ;"
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using
                MsgBox("User has been successfully deleted!", vbOKOnly)
            Catch ex As Exception
                MsgBox("Deletion failed!")
            End Try
        Else
            MsgBox("Select an User!")
        End If

        fillList()
        TextBoxForUsername.Text = ""
        TextBoxForPassword.Text = ""
        TextBoxForSign.Text = ""
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim sql
        If TextBoxForUsername.Text <> "" And TextBoxForPassword.Text <> "" And TextBoxForSign.Text <> "" Then
            Try
                Dim returnValue As Boolean
                returnValue = Regex.IsMatch(TextBoxForSign.Text.ToUpper.Trim, "R[0-9]J[0-9]E[0-9]B[0-9]Q[0-9]N[0-9]P[0-9]U[0-9]F[0-9]L[0-9]C[0-9]I[0-9]A[0-9]T[0-9]W[0-9]Z[0-9]$")
                If returnValue = True Then
                    If IsUserExist(TextBoxForUsername.Text.Trim.ToLower) = False Then
                        Dim builder As New Common.DbConnectionStringBuilder()
                        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                            sql = "INSERT INTO `" & DBName & "`.`credentials` (username, password, sign) VALUES ('" & TextBoxForUsername.Text & "','" & TextBoxForPassword.Text & "','" & TextBoxForSign.Text & "')"
                            Dim cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        End Using
                        fillList()
                        TextBoxForUsername.Text = ""
                        TextBoxForPassword.Text = ""
                        TextBoxForSign.Text = ""
                    Else
                        MsgBox("Username already exist!", vbOKOnly)
                    End If
                Else
                    MsgBox("The 'Sign' is not valid", vbOKOnly)
                End If
            Catch ex As Exception
            End Try
        Else
            MsgBox("All fields must be completed", vbOKOnly)
        End If
    End Sub

    Private Function IsUserExist(userName As String) As Boolean
        Dim returnValue As Boolean = False
        Dim strQuery As String

        strQuery = "SELECT COUNT(*)"
        strQuery &= "FROM credentials "
        strQuery &= "WHERE trim(lower(username)) = '" & userName.Trim.ToLower & "'"

        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using xComm As New MySqlCommand()
                With xComm
                    .Connection = con
                    .CommandText = strQuery
                    .CommandType = CommandType.Text
                End With
                Try
                    If CInt(xComm.ExecuteScalar()) > 0 Then
                        returnValue = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                    returnValue = False
                End Try
            End Using
        End Using
        Return returnValue
    End Function
End Class