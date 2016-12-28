Option Explicit On
Option Compare Text
Imports System.Configuration
Imports MySql.Data.MySqlClient


Public Class FormOpenIssue

    Dim tblProd As DataTable
    Dim DsProd As New DataSet

    Sub fillList()

        ListViewGRU.Clear()
        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "DEPARTMENT"
        h.Width = 160
        h2.Text = "OPEN ISSUE DESCRIPTION"
        h2.Width = 800
        ListViewGRU.Columns.Clear()
        ListViewGRU.Columns.Add(h)
        ListViewGRU.Columns.Add(h2)
        ListViewGRU.Items.Clear()

        If OpenIssue <> "" Then
            Dim str(2) As String
            Dim K = 1
            Dim i As Integer = InStr(OpenIssue, "[", CompareMethod.Text)
            Dim j As Integer = InStr(OpenIssue, "]", CompareMethod.Text)
            While j > 0
                str(0) = Mid(OpenIssue, K, i - K)
                str(1) = Mid(OpenIssue, i + 1, j - 1 - i)
                Dim ii As New ListViewItem(str)
                ListViewGRU.Items.Add(ii)
                K = j + 2
                i = InStr(j, OpenIssue, "[", CompareMethod.Text)
                j = InStr(j + 1, OpenIssue, "]", CompareMethod.Text)
            End While
        End If

        Dim column As ColumnHeader
        For Each column In ListViewGRU.Columns
            column.Width = -2
        Next

    End Sub

    Private Sub FormGroup_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        FormGroup.ComboBoxGroup.Sorted = True
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
                AdapterProd.Fill(DsProd, "product")
                tblProd = DsProd.Tables("product")
            End Using
        End Using
        fillList()
        TextBoxOpenIssueDescription.Text = ""
        ComboBoxGroup.SelectedIndex = 0
        ButtonUpdate.Enabled = False
    End Sub

    Private Sub ComboBoxGroup_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxGroup.SelectedIndexChanged

        Dim i As Integer
        tblProd.Clear()
        DsProd.Clear()
        Dim  builder As  New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	        Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
		        AdapterProd.Fill(DsProd, "product")
                tblProd = DsProd.Tables("product")
	        End Using
        End Using

        Try
            If ListViewGRU.SelectedItems.Count = 0 Then
                Dim result As DataRow() = tblProd.Select("bitronpn = '" & ProdOpenIssue & "'")
            End If
        Catch ex As Exception
            MsgBox("ERROR TO INTERPRET THE STRING")
        End Try

    End Sub

    Private Sub ButtonAddMch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click

        Dim sql As String
        If TextBoxOpenIssueDescription.Text <> "" And ComboBoxGroup.Text <> "" Then
            OpenIssue = Replace(OpenIssue, ComboBoxGroup.Text & "[" & TextBoxOpenIssueDescription.Text & "];", "")
            OpenIssue = OpenIssue & ComboBoxGroup.Text & "[" & Now.Day & "/" & Now.Month & "/" & Now.Year & "(d/m/y) ; " & TextBoxOpenIssueDescription.Text & "];"
            Try
                Dim  builder As  New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	                sql = "UPDATE `" & DBName & "`.`product` SET `OpenIssue` = '" & UCase(OpenIssue) & "' WHERE `product`.`BitronPN` = '" & Replace(Replace(Trim(FormProduct.TextBoxProduct.Text), ";", ","), "R&D", "R & D") & "' ;"
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
            End Try
        End If
        TextBoxOpenIssueDescription.Text = ""
        ComboBoxGroup.SelectedIndex = 0
        ButtonUpdate.Enabled = False
        fillList()
        ComboBoxGroup_TextChanged(Me, e)
        Dim column As ColumnHeader
        For Each column In ListViewGRU.Columns
            column.Width = -2
        Next
    End Sub

    Dim dateFromDescription As String
    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click

        Dim sql As String
        Dim oldOpenIssue As String = OpenIssue
        Dim dept = ""
        Dim opi = ""

        If ListViewGRU.SelectedItems.Count = 1 Then

            dept = ListViewGRU.SelectedItems.Item(0).SubItems(0).Text
            opi = ListViewGRU.SelectedItems.Item(0).SubItems(1).Text

            OpenIssue = Replace(OpenIssue, ListViewGRU.SelectedItems.Item(0).SubItems(0).Text & "[" & ListViewGRU.SelectedItems.Item(0).SubItems(1).Text & "];", "", , , CompareMethod.Text)
            Try
                Dim  builder As  New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	                sql = "UPDATE `" & DBName & "`.`product` SET `OpenIssue` = '" & OpenIssue &
                        "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using
                selectedIndex = -1
            Catch ex As Exception
                MsgBox("Deletion failed!")
            End Try
        Else
            MsgBox("Select an Open Issue!")
        End If
        TextBoxOpenIssueDescription.Text = ""
        ComboBoxGroup.SelectedIndex = 0
        ButtonUpdate.Enabled = False
        fillList()

        If oldOpenIssue <> OpenIssue Then
            MsgBox("Deleted Issue : " & dept & "[" & opi & "]")
        End If
        Dim column As ColumnHeader
        For Each column In ListViewGRU.Columns
            column.Width = -2
        Next
    End Sub

    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click
        Dim sql As String
        Dim oldOpenIssue As String = OpenIssue
        Dim dept = ""
        Dim opi = ""

        If ListViewGRU.SelectedItems.Count = 1 Then

            dept = ComboBoxGroup.Text
            opi = dateFromDescription & TextBoxOpenIssueDescription.Text

            OpenIssue = Replace(OpenIssue, ListViewGRU.SelectedItems.Item(0).SubItems(0).Text & "[" & ListViewGRU.SelectedItems.Item(0).SubItems(1).Text & "];", dept & "[" & opi & "];", , , CompareMethod.Text)
            Try
                Dim  builder As  New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	                sql = "UPDATE `" & DBName & "`.`product` SET `OpenIssue` = '" & OpenIssue &
                        "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using
                TextBoxOpenIssueDescription.Text = ""
                ComboBoxGroup.SelectedIndex = 0
                ButtonUpdate.Enabled = False
            Catch ex As Exception
                MsgBox("Update failed!")
            End Try
        Else
            MsgBox("Select an Open Issue!")
        End If

        fillList()

        If Len(oldOpenIssue) <> Len(OpenIssue) Then
            MsgBox("Updated Issue : " & dept & "[" & opi & "]")
        End If
        Dim column As ColumnHeader
        For Each column In ListViewGRU.Columns
            column.Width = -2
        Next

    End Sub
    Dim selectedIndex As Integer = -1
    Dim removeSelection As Boolean 
    

    Private Sub ListViewGRU_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewGRU.SelectedIndexChanged
        If removeSelection Then ListViewGRU.SelectedItems.Clear()
    End Sub

    Private Sub ListViewGRU_MouseUp(sender As Object, e As MouseEventArgs) Handles ListViewGRU.MouseUp
        Dim description As String
        If ListViewGRU.FocusedItem.Index = selectedIndex and selectedIndex <> -1 then
            ListViewGRU.Items(selectedIndex).Selected = True
            if ListViewGRU.items(selectedIndex).Selected = True Then
                dateFromDescription = ""
                TextBoxOpenIssueDescription.Text = ""
                ComboBoxGroup.SelectedIndex = 0
                ButtonUpdate.Enabled = False
                ListViewGRU.Items(ListViewGRU.FocusedItem.Index).Selected = False
                selectedIndex = -1
                removeSelection = true
            End if
        else
            removeSelection = false
            ListViewGRU.Items(ListViewGRU.FocusedItem.Index).Selected = true
            selectedIndex = ListViewGRU.FocusedItem.Index
            description = ListViewGRU.SelectedItems.Item(0).SubItems(1).Text.ToString()
            dateFromDescription = description.Substring(0, InStr(1, description, ";") + 1)
            ComboBoxGroup.Text = ListViewGRU.SelectedItems.Item(0).SubItems(0).Text.ToString()
            TextBoxOpenIssueDescription.Text = description.Substring(InStr(1, description, ";") + 1)
            ButtonUpdate.Enabled = True
        End If
    End Sub
    
End Class