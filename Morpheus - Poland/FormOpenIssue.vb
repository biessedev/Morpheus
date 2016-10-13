Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Globalization


Public Class FormOpenIssue


    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet

    Sub fillList()

        Dim i As Integer, j As Integer, K As Integer
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
            K = 1
            i = InStr(OpenIssue, "[", CompareMethod.Text)
            j = InStr(OpenIssue, "]", CompareMethod.Text)
            While j > 0
                str(0) = Mid(OpenIssue, k, i - k)
                str(1) = Mid(OpenIssue, i + 1, j - 1 - i)

                Dim ii As New ListViewItem(str)
                ListViewGRU.Items.Add(ii)
                K = j + 2
                i = InStr(j, OpenIssue, "[", CompareMethod.Text)
                j = InStr(j + 1, OpenIssue, "]", CompareMethod.Text)
            End While
        End If
    End Sub

    Private Sub FormGroup_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        FormGroup.ComboBoxGroup.Sorted = True
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        fillList()
    End Sub

    Private Sub ComboBoxGroup_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxGroup.TextChanged

        Dim i As Integer, result As DataRow(), k As Integer, n As Integer, j As Integer
        tblProd.Clear()
        DsProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")

        Try

            ComboBoxName.Text = ""
            result = tblProd.Select("bitronpn = '" & ProdOpenIssue & "'")
            ComboBoxName.Items.Clear()

            If result.Length > 0 Then
                k = InStr(1, result(i).Item("OpenIssue").ToString, ComboBoxGroup.Text)
                While k > 0
                    If k > 0 Then
                        n = InStr(k + 1, result(i).Item("OpenIssue").ToString, "]")
                        j = InStr(k + 1, result(i).Item("OpenIssue").ToString, "[")
                        ComboBoxName.Items.Add(Mid(result(i).Item("OpenIssue").ToString, j + 1, n - j - 1))
                    End If
                    k = InStr(k + 1, result(i).Item("OpenIssue").ToString, ComboBoxGroup.Text)
                End While
            End If

            ComboBoxName.Text = ""
        Catch ex As Exception
            MsgBox("ERROR TO INTERPRET THE STRING")
        End Try

    End Sub
  
    Private Sub ButtonAddMch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click

        Dim sql As String, cmd As MySqlCommand
        If ComboBoxName.Text <> "" And ComboBoxGroup.Text <> "" Then
            OpenIssue = Replace(OpenIssue, ComboBoxGroup.Text & "[" & ComboBoxName.Text & "];", "")
            OpenIssue = OpenIssue & ComboBoxGroup.Text & "[" & Now.Day & "/" & Now.Month & "/" & Now.Year & "(d/m/y) ; " & ComboBoxName.Text & "];"
            Try
                sql = "UPDATE `srvdoc`.`product` SET `OpenIssue` = '" & UCase(OpenIssue) & _
                "' WHERE `product`.`BitronPN` = '" & Replace(Replace(Trim(FormProduct.TextBoxProduct.Text), ";", ","), "R&D", "R & D") & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
        End If
        fillList()
        ComboBoxGroup_TextChanged(Me, e)
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click

        Dim sql As String, cmd As MySqlCommand, oldOpenIssue As String, dept As String, opi As String
        oldOpenIssue = OpenIssue
        dept = ""
        opi = ""

        If ListViewGRU.SelectedItems.Count = 1 Then

            dept = ListViewGRU.SelectedItems.Item(0).SubItems(0).Text
            opi = ListViewGRU.SelectedItems.Item(0).SubItems(1).Text

            OpenIssue = Replace(OpenIssue, ListViewGRU.SelectedItems.Item(0).SubItems(0).Text & "[" & ListViewGRU.SelectedItems.Item(0).SubItems(1).Text & "];", "", , , CompareMethod.Text)
            Try
                sql = "UPDATE `srvdoc`.`product` SET `OpenIssue` = '" & OpenIssue & _
                "' WHERE `product`.`BitronPN` = '" & Trim(FormProduct.TextBoxProduct.Text) & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Deletion failed!")
            End Try
        Else
            MsgBox("Select an Open Issue!")
        End If

        fillList()

        If Len(oldOpenIssue) <> Len(OpenIssue) Then
            MsgBox("Deleted Issue : " & dept & "[" & opi & "]")
        End If

    End Sub

    Private Sub ComboBoxGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxGroup.SelectedIndexChanged

    End Sub
End Class