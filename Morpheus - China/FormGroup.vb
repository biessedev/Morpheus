Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient


Public Class FormGroup

    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet

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

    Private Sub FormGroup_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        fillList()
        ComboBoxGroup.Text = StrComboBoxGroup
    End Sub

    Private Sub ComboBoxGroup_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxGroup.TextChanged

        Dim i As Integer, result As DataRow(), k As Integer, n As Integer
        Try

            ComboBoxName.Text = ""
            result = tblProd.Select("")
            ComboBoxName.Items.Clear()
            For i = 0 To result.Length - 1
                k = InStr(1, result(i).Item("grouplist").ToString, Mid(ComboBoxGroup.Text, 1, 11))
                If k > 0 Then
                    n = InStr(k + 1, result(i).Item("grouplist").ToString, "]")
                    If ComboBoxName.FindString(Mid(result(i).Item("grouplist").ToString, k + 12, n - 12 - k)) < 0 Then
                        ComboBoxName.Items.Add(Mid(result(i).Item("grouplist").ToString, k + 12, n - 12 - k))
                        'Dim str2 As String = result(i).Item("grouplist").ToString
                        'Dim str1 As String = Mid(result(i).Item("grouplist").ToString, k + 12, n - 12 - k)
                    End If
                End If
            Next
            If ComboBoxName.Items.Count > 0 Then ComboBoxName.Text = ComboBoxName.Items(ComboBoxName.Items.Count - 1)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonAddMch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click
        Dim sql As String, cmd As MySqlCommand
        If ComboBoxName.Text <> "" And ComboBoxGroup.Text <> "" Then
            GroupList = Replace(GroupList, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "")
            GroupList = GroupList & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];"
            Try
                sql = "UPDATE `srvdoc`.`product` SET `grouplist` = '" & UCase(GroupList) & _
                "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
        End If
        StrComboBoxGroup = ComboBoxGroup.Text
        fillList()
        ComboBoxGroup_TextChanged(Me, e)

    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click

        Dim sql As String, cmd As MySqlCommand, oldGroupList As String
        oldGroupList = GroupList
        If ComboBoxName.Text <> "" Then

            GroupList = Replace(GroupList, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "", , , CompareMethod.Text)
            Try
                sql = "UPDATE `srvdoc`.`product` SET `grouplist` = '" & GroupList & _
                "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
        End If

        fillList()
        If Len(oldGroupList) = Len(GroupList) Then
            MsgBox("Group Not find " & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "]")
        Else
            MsgBox("Group Deleted " & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "]")
        End If

    End Sub

End Class