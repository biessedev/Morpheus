Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient


Public Class FormGroup

    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet

    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet

    Dim dictionaryForProd As Dictionary(Of Integer, String)


    Sub fillList()
        Dim i As Integer, j As Integer
        ListViewGRU.Clear()
        If dictionaryForProd.Count > 0 Then
            Dim hname As New ColumnHeader
            hname.Text = "BitronPN"
            hname.Width = 100
            ListViewGRU.Columns.Add(hname)
        End If
        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "TYPE"
        h.Width = 150
        h2.Text = "NAME"
        h2.Width = 200
        ListViewGRU.Columns.Add(h)
        ListViewGRU.Columns.Add(h2)
        Dim productNr As String
        Dim group As String
        ListViewGRU.Items.Clear()
        If dictionaryForProd.Count > 0 Then
            For Each product In dictionaryForProd
                productNr = product.Key
                group = product.Value
                If group <> "" Then
                    Dim str(3) As String
                    i = 1
                    j = InStr(group, "]", CompareMethod.Text)
                    While j > 0
                        str(0) = productNr
                        str(1) = Mid(group, i, 11)
                        str(2) = Mid(group, i + 12, j - 12 - i)
                        Dim ii As New ListViewItem(str)
                        ListViewGRU.Items.Add(ii)
                        i = j + 2
                        j = InStr(i + 1, group, "]", CompareMethod.Text)
                    End While
                End If
            Next
        End If

    End Sub

    Private Sub FormGroup_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")


        ComboBoxGroup.Text = StrComboBoxGroup
        ComboBoxName.Text = ""
        FillProductList()

    End Sub

    'procedure for product list
    '
    Sub FillProductList()
        Dim rowShow As DataRow()
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Update(DsProd, "product")
        AdapterProd.Fill(DsProd, "product")

        tblProd = DsProd.Tables("product")
        rowShow = tblProd.Select("bitronpn like '*'", "bitronpn asc")

        Dim Widht(tblProd.Columns.Count - 1) As Integer
        Widht(0) = 0  ' 
        Widht(1) = 0  ' 
        Widht(2) = 0
        Widht(3) = 140
        Widht(4) = 170
        Widht(5) = 0
        Widht(6) = 160
        Widht(7) = 160
        Widht(8) = 0
        Widht(9) = 0
        Widht(10) = 0
        Widht(11) = 0
        Widht(12) = 0
        Widht(13) = 0
        Widht(14) = 0
        Widht(15) = 0
        Widht(16) = 0  ' ecr
        Widht(17) = 0   ' ls
        Widht(18) = 100   ' bom value
        Widht(19) = 100   ' bom ratio
        Widht(20) = 0
        Widht(21) = 50
        Widht(22) = 50
        Widht(23) = 130  ' etd
        Widht(24) = 70
        Widht(25) = 0
        Widht(26) = 300  ' name activity
        Widht(27) = 0
        Widht(28) = 0
        Widht(29) = 0

        Dim c As DataColumn, i As Integer, strPrevDoc As String
        ListViewForProducts.Clear()
        i = 0
        For Each c In tblProd.Columns

            'adding names of columns as Listview columns				
            Dim h As New ColumnHeader
            h.Text = c.ColumnName
            h.Width = Widht(i)
            ListViewForProducts.Columns.Add(h)
            i = i + 1
        Next

        Dim str(tblProd.Columns.Count - 1) As String
        'adding Datarows as listview Grids
        strPrevDoc = ""
        For i = 0 To rowShow.Length - 1
            For col As Integer = 0 To tblProd.Columns.Count - 1
                str(col) = UCase(rowShow(i).ItemArray(col).ToString())
            Next
            Dim ii As New ListViewItem(str)
            ListViewForProducts.Items.Add(ii)

            ListViewForProducts.Items(ListViewForProducts.Items.Count - 1).BackColor = Color.White

            If ListViewForProducts.Items(ListViewForProducts.Items.Count - 1).SubItems(14).Text <> "" Then
                ListViewForProducts.Items(ListViewForProducts.Items.Count - 1).BackColor = Color.LightCoral
            End If

        Next
        ListViewForProducts.Refresh()

    End Sub

    Private Sub ComboBoxGroup_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxGroup.TextChanged
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

    Private Sub ButtonAddMch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click
        Dim sql As String, cmd As MySqlCommand
        If ListViewForProducts.SelectedItems.Count > 0 Then
            If ComboBoxName.Text <> "" And ComboBoxGroup.Text <> "" Then

                Using trans = MySqlconnection.BeginTransaction(IsolationLevel.ReadCommitted)
                    For Each product In dictionaryForProd
                        Dim group = product.Value
                        group = Replace(group, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "")
                        Dim newGroupList = group & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];"
                        group = newGroupList
                        Try
                            sql = "UPDATE `product` SET `grouplist` = '" & UCase(group) &
                        "' WHERE `product`.`BitronPN` = '" & product.Key & "' ;"
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                        End Try
                    Next
                    trans.Commit()
                End Using
            End If
            Me.DsProd.Reset()
            tblProd.Reset()
            Me.AdapterProd.Fill(Me.tblProd)
            Me.dictionaryForProd.Clear()
            For Each productItem As ListViewItem In ListViewForProducts.SelectedItems
                Dim item = productItem.SubItems.Item(0)
                Dim result = tblProd.Select("id = '" & item.Text & "'")
                Dim lastResult = result.Length - 1
                Me.dictionaryForProd.Add(result(lastResult).Item("BitronPn"), result(lastResult).Item("groupList"))
            Next
            ListViewGRU.Clear()
            fillList()
        Else
            MsgBox("Select a product!")
        End If
        ComboBoxGroup.Text = StrComboBoxGroup

    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click

        Dim sql As String, cmd As MySqlCommand, oldGroupList As String, productNumber As String, type As String, filename As String
        oldGroupList = GroupList
        productNumber = ""
        Type = ""
        filename = ""
        If ListViewGRU.SelectedItems.Count > 0 Then
            Dim i = 0
            Using trans = MySqlconnection.BeginTransaction(IsolationLevel.ReadCommitted)
                For Each itemFromList In ListViewGRU.SelectedItems
                    productNumber = ListViewGRU.SelectedItems.Item(i).SubItems(0).Text
                    type = ListViewGRU.SelectedItems.Item(i).SubItems(1).Text
                    filename = ListViewGRU.SelectedItems.Item(i).SubItems(2).Text
                    Dim valueOfGroupList = dictionaryForProd.Item(productNumber)
                    GroupList = Replace(valueOfGroupList, type & "[" & filename & "];", "", , , CompareMethod.Text)

                    Try
                        sql = "UPDATE `product` SET `grouplist` = '" & GroupList &
                "' WHERE `product`.`BitronPN` = '" & productNumber & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        dictionaryForProd.Item(productNumber) = GroupList
                    Catch ex As Exception
                        MsgBox("Deletion failed!")
                    End Try
                    i += 1
                Next
                trans.Commit()
            End Using
            Me.DsProd.Reset()
            tblProd.Reset()
            Me.AdapterProd.Fill(Me.tblProd)
            Me.dictionaryForProd.Clear()
            For Each productItem As ListViewItem In ListViewForProducts.SelectedItems
                Dim item = productItem.SubItems.Item(0)
                Dim result = tblProd.Select("id = '" & item.Text & "'")
                Dim lastResult = result.Length - 1
                Me.dictionaryForProd.Add(result(lastResult).Item("BitronPn"), result(lastResult).Item("groupList"))
            Next
            ListViewGRU.Clear()
            fillList()
        Else
            MsgBox("Select a document!")
        End If

    End Sub

    Private Sub ListViewForProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewForProducts.SelectedIndexChanged
        If ListViewForProducts.SelectedItems.Count > 0 Then
            Me.dictionaryForProd = New Dictionary(Of Integer, String)
            Me.DsProd.Reset()
            tblProd.Reset()
            Me.AdapterProd.Fill(Me.tblProd)
            For Each productItem As ListViewItem In ListViewForProducts.SelectedItems
                Dim item = productItem.SubItems.Item(0)
                Dim result = tblProd.Select("id = '" & item.Text & "'")
                Dim lastResult = result.Length - 1
                Me.dictionaryForProd.Add(result(lastResult).Item("BitronPn"), result(lastResult).Item("groupList"))
                ListViewGRU.Clear()
                fillList()
            Next
        End If
    End Sub
End Class