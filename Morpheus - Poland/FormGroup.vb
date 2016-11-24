Option Explicit On
Option Compare Text
Imports System.Configuration
Imports MySql.Data.MySqlClient


Public Class FormGroup

    'Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet

    'Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet

    Dim dtSelectedColumns As DataTable


    Dim dictionaryForProd As Dictionary(Of Integer, String)


    Sub fillList()
        ListViewGRU.Clear()
        If dictionaryForProd.Count > 0 Then
            Dim hname As New ColumnHeader
            hname.Text = "Product Bitron Code"
            hname.Width = 110
            ListViewGRU.Columns.Add(hname)
        End If
        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "Doc Type"
        h.Width = 110
        h2.Text = "File Name"
        h2.Width = 190
        ListViewGRU.Columns.Add(h)
        ListViewGRU.Columns.Add(h2)
        ListViewGRU.Items.Clear()
        If dictionaryForProd.Count > 0 Then
            For Each product In dictionaryForProd
                Dim productNr As String = product.Key
                Dim group As String = product.Value
                If group <> "" Then
                    Dim str(3) As String
                    Dim i As Integer = 1
                    Dim j As Integer = InStr(group, "]", CompareMethod.Text)
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
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
                AdapterProd.Fill(DsProd, "product")
                tblProd = DsProd.Tables("product")
            End Using
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", con)
		        AdapterDoc.Fill(DsDoc, "doc")
                tblDoc = DsDoc.Tables("doc")
	        End Using
        End Using
        ComboBoxGroup.Text = StrComboBoxGroup
        ComboBoxName.Text = ""
        FillProductList()

    End Sub

    Sub FillProductList()
        DsProd.Clear()
        tblProd.Clear()
        Dim  builder As  New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	        Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
		        AdapterProd.Update(DsProd, "product")
                AdapterProd.Fill(DsProd, "product")
	        End Using
        End Using
        tblProd = DsProd.Tables("product")
        Dim rowShow As DataRow() = tblProd.Select("bitronpn like '*'", "bitronpn asc")
        Dim Widht(tblProd.Columns.Count - 1) As Integer
        Widht(0) = 0  ' 
        Widht(1) = 0  ' 
        Widht(2) = 0
        Widht(3) = 110
        Widht(4) = 450
        Widht(5) = 0
        Widht(6) = 0
        Widht(7) = 0
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
        Widht(18) = 0   ' bom value
        Widht(19) = 0   ' bom ratio
        Widht(20) = 0
        Widht(21) = 0
        Widht(22) = 0
        Widht(23) = 0  ' etd
        Widht(24) = 0
        Widht(25) = 0
        Widht(26) = 0  ' name activity
        Widht(27) = 0
        Widht(28) = 0
        Widht(29) = 0

        Dim c As DataColumn
        ListViewForProducts.Clear()
        Dim i As Integer = 0
        For Each c In tblProd.Columns
            'adding names of columns as Listview columns				
            Dim h As New ColumnHeader
            If i = 3 Then
                h.Text = "Product Bitron Code"
            ElseIf i = 4 Then
                h.Text = "Description"
            Else
                h.Text = c.ColumnName
            End If
            h.Width = Widht(i)
            ListViewForProducts.Columns.Add(h)
            i = i + 1
        Next

        Dim str(tblProd.Columns.Count - 1) As String
        'adding Datarows as listview Grids
        For i = 0 To rowShow.Length - 1
            For col = 0 To tblProd.Columns.Count - 1
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
        PopulateComboBoxName()
    End Sub

    Private Sub ButtonAddMch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click
        Dim sql As String, cmd As MySqlCommand
        Dim  builder As  New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            If ListViewForProducts.SelectedItems.Count > 0 Then
                If ComboBoxName.Text <> "" And ComboBoxGroup.Text <> "" Then
                    Using trans = con.BeginTransaction(IsolationLevel.ReadCommitted)
                        For Each product In dictionaryForProd
                            Dim group = product.Value
                            group = Replace(group, Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];", "")
                            Dim newGroupList = group & Mid(ComboBoxGroup.Text, 1, 11) & "[" & ComboBoxName.Text & "];"
                            group = newGroupList
                            Try
                                sql = "UPDATE `product` SET `grouplist` = '" & UCase(group) &
                            "' WHERE `product`.`BitronPN` = '" & product.Key & "' ;"
                                cmd = New MySqlCommand(sql, con)
                                cmd.ExecuteNonQuery()
                            Catch ex As Exception
                            End Try
                        Next
                        trans.Commit()
                    End Using
                End If
                Me.DsProd.Reset()
                tblProd.Reset()
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
		            AdapterProd.Fill(Me.tblProd)
	            End Using
                
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
        End Using
        ComboBoxGroup.Text = StrComboBoxGroup
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click
        Dim  builder As  New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            If ListViewGRU.SelectedItems.Count > 0 Then
                Dim i = 0
                Using trans = con.BeginTransaction(IsolationLevel.ReadCommitted)
                    For Each itemFromList In ListViewGRU.SelectedItems
                        Dim productNumber As Object = ListViewGRU.SelectedItems.Item(i).SubItems(0).Text
                        Dim type As Object = ListViewGRU.SelectedItems.Item(i).SubItems(1).Text
                        Dim filename As Object = ListViewGRU.SelectedItems.Item(i).SubItems(2).Text
                        Dim valueOfGroupList = dictionaryForProd.Item(productNumber)
                        GroupList = Replace(valueOfGroupList, type & "[" & filename & "];", "", , , CompareMethod.Text)
                        Try
                            Dim sql As String = "UPDATE `product` SET `grouplist` = '" & GroupList &
                                                "' WHERE `product`.`BitronPN` = '" & productNumber & "' ;"
                            Dim cmd As MySqlCommand = New MySqlCommand(sql, con)
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
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
		            AdapterProd.Fill(Me.tblProd)
	            End Using
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
        End Using
    End Sub

    Private Sub ListViewForProducts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewForProducts.SelectedIndexChanged
        If ListViewForProducts.SelectedItems.Count > 0 Then
            Me.dictionaryForProd = New Dictionary(Of Integer, String)
            Me.DsProd.Reset()
            tblProd.Reset()
            Dim  builder As  New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
	            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", con)
		            AdapterProd.Fill(Me.tblProd)
	            End Using
            End Using
            For Each productItem As ListViewItem In ListViewForProducts.SelectedItems
                Dim item = productItem.SubItems.Item(0)
                Dim result = tblProd.Select("id = '" & item.Text & "'")
                Dim lastResult = result.Length - 1
                Me.dictionaryForProd.Add(result(lastResult).Item("BitronPn"), result(lastResult).Item("groupList"))
                ListViewGRU.Clear()
                fillList()
            Next
        End If
        PopulateComboBoxName()
    End Sub

    Private Sub PopulateComboBoxName()
        Try
            Dim i As Integer
            ComboBoxName.Text = ""
            ComboBoxName.Items.Clear()
            Dim resultdoc As DataRow() = tblDoc.Select("header = '" & Mid(ComboBoxGroup.Text, 1, 11) & "'")
            For Each item In ListViewForProducts.SelectedItems

            Next
            Dim fileDocExist As String
            Dim fileName As String
            For i = 0 To resultdoc.Length - 1
                fileDocExist = true
                fileName = resultdoc(i).Item("filename").ToString
                For Each product In dictionaryForProd
                    If InStr(product.Value.ToString , fileName) = 0 Then fileDocExist = false
                Next
                If fileDocExist = False Then ComboBoxName.Items.Add(resultdoc(i).Item("filename").ToString)
            Next
        Catch ex As Exception
        End Try
    End Sub

End Class