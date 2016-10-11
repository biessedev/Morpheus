﻿Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient


Public Class FormGroup

    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet

    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet

    Sub fillList()

        Dim i As Integer, j As Integer
        ListViewGRU.Clear()
        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "TYPE"
        h.Width = 200
        h2.Text = "NAME"
        h2.Width = 485
        ListViewGRU.Columns.Add(h)
        ListViewGRU.Columns.Add(h2)

        ListViewGRU.Items.Clear()
        If GroupList <> "" Then
            Dim str(2) As String
            i = 1
            j = InStr(GroupList, "]", CompareMethod.Text)
            While j > 0
                str(0) = Mid(GroupList, i, 11)
                str(1) = Mid(GroupList, i + 12, j - 12 - i)

                Dim ii As New ListViewItem(str)
                ListViewGRU.Items.Add(ii)
                i = j + 2
                j = InStr(i + 1, GroupList, "]", CompareMethod.Text)
            End While
        End If
    End Sub

    Private Sub FormGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        fillList()
        ComboBoxGroup.Text = StrComboBoxGroup
        ComboBoxName.Text = ""

    End Sub

    Private Sub ComboBoxGroup_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxGroup.TextChanged

        Dim i As Integer, resultdoc As DataRow()
        Try

            ComboBoxName.Text = ""
            ComboBoxName.Items.Clear()

            resultdoc = tblDoc.Select("header = '" & Mid(ComboBoxGroup.Text, 1, 11) & "'")

            For i = 0 To resultdoc.Length - 1
                ComboBoxName.Items.Add(resultdoc(i).Item("filename").ToString)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonAddMch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim sql As String, cmd As MySqlCommand
        If ComboBoxName.Text <> "" And ComboBoxGroup.Text <> "" Then
            GroupList = Replace(GroupList, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "")
            GroupList = GroupList & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];"
            Try
                sql = "UPDATE `srvdoc`.`product` SET `grouplist` = '" & UCase(GroupList) &
                "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
        End If
        'StrComboBoxGroup = ComboBoxGroup.Text
        fillList()
        'ComboBoxGroup_TextChanged(Me, e)
        ComboBoxGroup.Text = StrComboBoxGroup

    End Sub

    'Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click

    '    Dim sql As String, cmd As MySqlCommand, oldGroupList As String
    '    oldGroupList = GroupList
    '    If ComboBoxName.Text <> "" Then
    '   
    '        GroupList = Replace(GroupList, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "", , , CompareMethod.Text)
    '        Try
    '            sql = "UPDATE `srvdoc`.`product` SET `grouplist` = '" & GroupList & _
    '            "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
    '            cmd = New MySqlCommand(sql, MySqlconnection)
    '            cmd.ExecuteNonQuery()
    '        Catch ex As Exception
    '        End Try
    '    End If
    '    fillList()
    '    If Len(oldGroupList) = Len(GroupList) Then
    '        MsgBox(Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "]" & "not found for selected product")
    '    Else
    '        MsgBox(Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "]" & "deleted for selected product")
    '    End If
    'End Sub

    Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click

        Dim sql As String, cmd As MySqlCommand, oldGroupList As String, header As String, filename As String
        oldGroupList = GroupList
        header = ""
        filename = ""
        If ListViewGRU.SelectedItems.Count = 1 Then
            header = ListViewGRU.SelectedItems.Item(0).SubItems(0).Text
            filename = ListViewGRU.SelectedItems.Item(0).SubItems(1).Text
            GroupList = Replace(GroupList, ListViewGRU.SelectedItems.Item(0).SubItems(0).Text & "[" & ListViewGRU.SelectedItems.Item(0).SubItems(1).Text & "];", "", , , CompareMethod.Text)
            Try
                sql = "UPDATE `srvdoc`.`product` SET `grouplist` = '" & GroupList &
                "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox("Deletion failed!")
            End Try
        Else
            MsgBox("Select a document!")
        End If

        fillList()

        If Len(oldGroupList) <> Len(GroupList) Then
            MsgBox(header & "[" & filename & "]" & " deleted for selected product!")
        End If

    End Sub

End Class