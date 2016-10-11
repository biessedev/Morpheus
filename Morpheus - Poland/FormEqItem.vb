Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic

Public Class FormEqItem

    Public CurrentActivityId As String
    Dim AdapterEQ As New MySqlDataAdapter("SELECT * FROM EQUIPMENTS", MySqlconnection)
    Dim tblEQ As DataTable
    Dim DsEQ As New DataSet
    Dim AdapterEQAsset As New MySqlDataAdapter("SELECT * FROM EqAsset", MySqlconnection)
    Dim tblEQAsset As DataTable
    Dim DsEQAsset As New DataSet
    Dim UpdatingAuto As Boolean

    Private Sub FormEqItem_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        UpdatingAuto = True
        GroupBoxItem.Enabled = False
        TreeViewEQAsset.HideSelection = False
        Me.Text = Me.Text & "    Welcome : " & CreAccount.strUserName
        If controlRight("E") <= 2 Then ComboBoxResponsible.DropDownStyle = ComboBoxStyle.DropDownList
        If controlRight("E") <= 2 Then ButtonClosedDate.Enabled = False
        TextBoxActivity.Text = CurrentActivityId
        AdapterEQ.Fill(DsEQ, "EQUIPMENTS")
        tblEQ = DsEQ.Tables("EQUIPMENTS")
        ComboBoxRDA.Items.Add("NEED_PRICE")
        AdapterEQAsset.Fill(DsEQAsset, "EqAsset")
        tblEQAsset = DsEQAsset.Tables("EqAsset")
        FillTreeViewEQAsset()
        TextBoxTotalCost.Text = CostRecalculation()
        loadComboDai()
        loadComboResponsible()
        UpdatingAuto = False
        ButtonSave.BackColor = Color.Green
    End Sub



    Function CompareDatabase(ByVal id As Integer) As String
        Dim rowResults As DataRow()
        Dim DBString As String
        Dim FormString As String
        CompareDatabase = False
        If CurrentAssetId() > 0 Then



            rowResults = tblEQAsset.Select("id = " & CurrentAssetId() & "")

            DBString = ""
            DBString = DBString & rowResults(0)("description")
            DBString = DBString & rowResults(0)("IdAsset")
            DBString = DBString & rowResults(0)("Responsible")
            DBString = DBString & rowResults(0)("Supplier")
            DBString = DBString & rowResults(0)("EstimatedDate")
            DBString = DBString & rowResults(0)("OpenDate")
            DBString = DBString & rowResults(0)("ClosedDate")
            DBString = DBString & rowResults(0)("RDA")
            DBString = DBString & rowResults(0)("Order")
            DBString = DBString & rowResults(0)("Dai")
            DBString = DBString & rowResults(0)("Cost")



            Try
                DsEQAsset.Clear()
                tblEQAsset.Clear()
            Catch ex As Exception

            End Try


            AdapterEQAsset.Fill(DsEQAsset, "EqAsset")
            tblEQAsset = DsEQAsset.Tables("EqAsset")

            rowResults = tblEQAsset.Select("id = " & CurrentAssetId() & "")

            FormString = ""
            FormString = FormString & rowResults(0)("description")
            FormString = FormString & rowResults(0)("IdAsset")
            FormString = FormString & rowResults(0)("Responsible")
            FormString = FormString & rowResults(0)("Supplier")
            FormString = FormString & rowResults(0)("EstimatedDate")
            FormString = FormString & rowResults(0)("OpenDate")
            FormString = FormString & rowResults(0)("ClosedDate")
            FormString = FormString & rowResults(0)("RDA")
            FormString = FormString & rowResults(0)("Order")
            FormString = FormString & rowResults(0)("Dai")
            FormString = FormString & rowResults(0)("Cost")


            If FormString = DBString Then CompareDatabase = True
        End If
    End Function


    Sub FillTreeViewEQAsset()
        Try


            Static Dim tblEQ As DataTable
            Static Dim DsEQ As New DataSet
            Dim rootNode As TreeNode
            Dim rootChildren1 As TreeNode
            Dim rowShow As DataRow()
            Dim rowShowAsset As DataRow()
            TreeViewEQAsset.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            TreeViewEQAsset.Nodes.Clear()
            TreeViewEQAsset.BackColor = Color.White
            Try
                DsEQ.Clear()
                tblEQ.Clear()
            Catch ex As Exception

            End Try

            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")

            Try
                DsEQAsset.Clear()
                tblEQAsset.Clear()
            Catch ex As Exception

            End Try


            AdapterEQAsset.Fill(DsEQAsset, "EQAsset")
            tblEQAsset = DsEQAsset.Tables("EQAsset")

            rowShow = tblEQ.Select("idactivity ='" & TextBoxActivity.Text & "'", "Id")

            For Each row In rowShow


                rootNode = New TreeNode(row("Id").ToString & " - " & row("Toolname").ToString)

                TreeViewEQAsset.BeginUpdate()
                TreeViewEQAsset.Nodes.Add(rootNode)
                TreeViewEQAsset.EndUpdate()
                TreeViewEQAsset.ResumeLayout()

                If row("status").ToString = ("CLOSED") Then rootNode.ForeColor = Color.Green
                If row("status").ToString = ("NONEED") Then rootNode.ForeColor = Color.Gray
                If row("status").ToString = ("STANDBY") Then rootNode.ForeColor = Color.Blue

                If row("status").ToString = ("OPEN") Then
                    If TimingEQ(row("id").ToString, tblEQ) = ("ONTIME") Then
                        rootNode.ForeColor = Color.Green
                    ElseIf TimingEQ(row("id").ToString, tblEQ) = ("DELAY") Then
                        rootNode.ForeColor = Color.Red
                    Else

                    End If
                End If

                rowShowAsset = tblEQAsset.Select("idtool ='" & row("Id").ToString & "'", "Id")

                For Each rowAsset In rowShowAsset
                    rootChildren1 = New TreeNode(rowAsset("id").ToString & " - " & rowAsset("name").ToString)
                    rootChildren1.NodeFont = New Font("Segoe UI", 10, FontStyle.Regular)
                    rootChildren1.ForeColor = Color.Green
                    If rowAsset("closeddate").ToString = ("OPEN") Then rootChildren1.ForeColor = Color.Navy
                    TreeViewEQAsset.BeginUpdate()
                    rootNode.Nodes.Add(rootChildren1)
                    TreeViewEQAsset.EndUpdate()
                    TreeViewEQAsset.ResumeLayout()
                Next

            Next
        Catch ex As Exception
            MsgBox("Problem in Item load!")
        End Try
    End Sub


    Function TimingEQ(ByVal id As Long, ByVal tblEQ As DataTable) As String
        ' If id = 33 Then Stop

        Dim rowShow As DataRow()

        rowShow = tblEQ.Select("id = " & id & "")

        If rowShow(0).Item("status").ToString = "CLOSED" Then
            TimingEQ = "CLOSED"
        ElseIf Len(rowShow(0).Item("START").ToString) = 10 And Len(rowShow(0).Item("end").ToString) = 10 Then
            TimingEQ = "ONTIME"

            If Len(rowShow(0).Item("HWDoc").ToString) = 10 Then
                If DateDiff("d", Today, string_to_date(rowShow(0).Item("HWDoc").ToString)) < 0 Then
                    TimingEQ = "DELAY"
                End If
            End If

            If Len(rowShow(0).Item("END").ToString) = 10 Then
                If DateDiff("d", Today, string_to_date(rowShow(0).Item("END").ToString)) < 0 Then
                    TimingEQ = "DELAY"
                End If
            End If

            If Len(rowShow(0).Item("HWDebug").ToString) = 10 Then
                If DateDiff("d", Today, string_to_date(rowShow(0).Item("HWDebug").ToString)) < 0 Then
                    TimingEQ = "DELAY"
                End If
            End If

            If Len(rowShow(0).Item("SWDebug").ToString) = 10 Then
                If DateDiff("d", Today, string_to_date(rowShow(0).Item("SWDebug").ToString)) < 0 Then
                    TimingEQ = "DELAY"
                End If
            End If

            If Len(rowShow(0).Item("HWBuilding").ToString) = 10 Then
                If DateDiff("d", Today, string_to_date(rowShow(0).Item("HWBuilding").ToString)) < 0 Then
                    TimingEQ = "DELAY"
                End If
            End If

        Else
            TimingEQ = ""
        End If

    End Function


    Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonRemove.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String
        If CurrentAssetId() > 0 Then
            If vbYes = MsgBox("Do you want delete this Asset?", MsgBoxStyle.YesNo) Then
                If CompareDatabase(CurrentAssetId()) Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`eqAsset` WHERE `eqAsset`.`id` = " & CurrentAssetId()
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Asset deleted!")
                        TreeViewEQAsset.SelectedNode.Remove()
                        Application.DoEvents()

                    Catch ex As Exception
                        MsgBox("Asset deleting error " & ex.Message)
                    End Try

                Else
                    MsgBox("session open from others user, plese try later! ")
                End If

            End If
        Else
            MsgBox("Please select a valid Asset!")
        End If

    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonAdd.Click

        Dim rootNode As TreeNode, rootNodeChild As TreeNode
        Dim cmd As New MySqlCommand()
        Dim sql As String
        myNodeSelect(True)

        If Not IsNothing(TreeViewEQAsset.SelectedNode) Then
            If Not IsNothing(TreeViewEQAsset.SelectedNode.Parent) Then
                rootNode = TreeViewEQAsset.SelectedNode.Parent
            Else
                rootNode = TreeViewEQAsset.SelectedNode
            End If


            rootNodeChild = New TreeNode("---- New Tool ----")

            TreeViewEQAsset.BeginUpdate()
            rootNode.Nodes.Add(rootNodeChild)

            TreeViewEQAsset.EndUpdate()
            TreeViewEQAsset.ResumeLayout()

            Try
                sql = "INSERT INTO `" & DBName & "`.`eqasset` (`Name`,`idtool` ,`closeddate`,`rda`) VALUES ( '" & Trim(UCase(Replace(InputBox("Insert the item name: "), "'", ""))) & "' , '" & CurrentToolId() & "'" & ", 'OPEN','NEED_PRICE') ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            FillTreeViewEQAsset()

            rootNode.Expand()



        End If

        myNodeSelect(False)


    End Sub


    Function CurrentAssetId() As Integer
        Try
            CurrentAssetId = 0
            If TreeViewEQAsset.SelectedNode.Level = 1 Then
                CurrentAssetId = Mid(TreeViewEQAsset.SelectedNode.Text, 1, InStr(TreeViewEQAsset.SelectedNode.Text, " - ") - 1)
            End If
        Catch ex As Exception
            CurrentAssetId = 0
        End Try

    End Function
    Function CurrentAssetName() As String
        Try
            CurrentAssetName = ""
            If TreeViewEQAsset.SelectedNode.Level = 1 Then
                CurrentAssetName = Trim(Mid(TreeViewEQAsset.SelectedNode.Text, InStr(TreeViewEQAsset.SelectedNode.Text, " - ") + 1))
            End If
        Catch ex As Exception
            CurrentAssetName = ""
        End Try

    End Function

    Function CurrentToolId() As Integer
        Try
            CurrentToolId = 0
            If CurrentAssetId() = 0 Then
                CurrentToolId = Mid(TreeViewEQAsset.SelectedNode.Text, 1, InStr(TreeViewEQAsset.SelectedNode.Text, " - ") - 1)
            Else
                CurrentToolId = Mid(TreeViewEQAsset.SelectedNode.Parent.Text, 1, InStr(TreeViewEQAsset.SelectedNode.Parent.Text, " - ") - 1)
            End If
        Catch ex As Exception
            CurrentToolId = 0
        End Try

    End Function
    Function CurrentToolName() As String
        Try
            CurrentToolName = ""
            If CurrentAssetId() = 0 Then
                CurrentToolName = Trim(Mid(TreeViewEQAsset.SelectedNode.Text, InStr(TreeViewEQAsset.SelectedNode.Text, " - ") + 1))
            Else
                CurrentToolName = Trim(Mid(TreeViewEQAsset.SelectedNode.Parent.Text, InStr(TreeViewEQAsset.SelectedNode.Parent.Text, " - ") + 1))
            End If
        Catch ex As Exception
            CurrentToolName = ""
        End Try

    End Function



    Sub myNodeSelect(ByVal read As Boolean)

        Static Dim selNode As TreeNode

        If read Then
            selNode = TreeViewEQAsset.SelectedNode
            TreeViewEQAsset.Select()

        Else
            TreeViewEQAsset.Visible = False
            Dim idSelected As Integer = Trim(Mid(selNode.Text, 1, InStr(selNode.Text, " - ") - 1))
            Dim id As Integer
            TreeViewEQAsset.HideSelection = False
            For Each node In TreeViewEQAsset.Nodes
                For Each nn In node.Nodes
                    Try
                        id = Mid(nn.Text, 1, InStr(nn.Text, " - ") - 1)
                    Catch ex As Exception

                    End Try


                    If idSelected = id Then
                        selNode = nn
                    End If
                Next
            Next

            TreeViewEQAsset.SelectedNode = selNode
            'TreeViewEQAsset.TopNode = TreeViewEQAsset.SelectedNode.Parent
            TreeViewEQAsset.Focus()
            TreeViewEQAsset.Visible = True
        End If

    End Sub



    Private Sub DateTimePickerED_CloseUp(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerED.CloseUp
        ComboBoxEstimatedClosed.Items.Clear()
        ComboBoxEstimatedClosed.Items.Add("")
        ComboBoxEstimatedClosed.Items.Add(date_to_string(DateTimePickerED.Text))
        ComboBoxEstimatedClosed.Text = date_to_string(DateTimePickerED.Text)
    End Sub


    Private Sub DateTimePickerOD_CloseUp(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerOD.CloseUp
        ComboBoxOpenDate.Items.Clear()
        ComboBoxOpenDate.Items.Add("")
        ComboBoxOpenDate.Items.Add(date_to_string(DateTimePickerOD.Text))
        ComboBoxOpenDate.Text = date_to_string(DateTimePickerOD.Text)
    End Sub

    Private Sub ComboBoxEstimatedClosed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles ComboBoxEstimatedClosed.TextChanged, ButtonClosedDate.TextChanged
        If (ComboBoxEstimatedClosed.Text <> "") Then
            TextBoxDelay.Text = DateDiff("d", string_to_date(ComboBoxEstimatedClosed.Text), string_to_date(IIf(ButtonClosedDate.Text = "OPEN", date_to_string(Today), ButtonClosedDate.Text)))
            If Val(TextBoxDelay.Text) > 0 Then TextBoxDelay.BackColor = Color.Tomato
            If Val(TextBoxDelay.Text) <= 0 Then TextBoxDelay.BackColor = Color.LightGreen
        Else
            TextBoxDelay.Text = ""
            TextBoxDelay.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub ButtonClosedDate_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonClosedDate.Click
        If ButtonClosedDate.Text = "OPEN" Then
            If vbYes = MsgBox("Do you want close today this Item?", vbYesNo) Then
                ButtonClosedDate.Text = date_to_string(Today)
                ButtonClosedDate.BackColor = Color.LightGreen
                ComboBoxEstimatedClosed.Enabled = False
                DateTimePickerED.Enabled = False
                SaveDelay()
            End If
        Else
            If vbYes = MsgBox("Do you want remove the close status of this Item?", vbYesNo) Then
                ButtonClosedDate.Text = "OPEN"
                ButtonClosedDate.BackColor = Color.Tomato
                ComboBoxEstimatedClosed.Enabled = True
                DateTimePickerED.Enabled = True
            End If
        End If
    End Sub


    Private Sub TreeViewEQAsset_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewEQAsset.AfterSelect
        If ButtonSave.BackColor = Color.Red Then
            If MsgBox("Session open, do you want SAVE?", vbYesNo) = vbYes Then
                ButtonSave_Click(Me, e)
            Else
                ButtonSave.BackColor = Color.Green
            End If
        End If



        If toolAsset(CurrentToolId()) <> "" Then
            ButtonAssetImport.Enabled = True
        Else
            ButtonAssetImport.Enabled = False
        End If

        Dim rowShow As DataRow()
        UpdatingAuto = True

        If CurrentAssetId() > 0 Then
            GroupBoxItem.Enabled = True



            rowShow = tblEQAsset.Select("id = " & CurrentAssetId() & "")


            If rowShow.Length > 0 Then

                RichTextBoxNote.Text = rowShow(0)("description").ToString
                TextBoxAssetID.Text = rowShow(0)("IdAsset").ToString
                ComboBoxResponsible.Text = rowShow(0)("Responsible").ToString
                TextBoxSupplier.Text = rowShow(0)("Supplier").ToString
                ComboBoxEstimatedClosed.Items.Add(rowShow(0)("EstimatedDate").ToString)
                ComboBoxEstimatedClosed.Text = rowShow(0)("EstimatedDate").ToString
                ComboBoxOpenDate.Items.Add(IIf(rowShow(0)("OpenDate").ToString <> "", rowShow(0)("OpenDate").ToString, date_to_string(Today)))
                ComboBoxOpenDate.Text = IIf(rowShow(0)("OpenDate").ToString <> "", rowShow(0)("OpenDate").ToString, date_to_string(Today))

                ButtonClosedDate.Text = IIf(rowShow(0)("ClosedDate").ToString <> "", rowShow(0)("ClosedDate").ToString, "OPEN")
                ComboBoxRDA.Text = rowShow(0)("RDA").ToString
                TextBoxOrder.Text = rowShow(0)("Order").ToString
                ComboBoxDai.Text = rowShow(0)("Dai").ToString
                TextBoxCost.Text = Replace(Replace(Replace(rowShow(0)("Cost").ToString, "'", ""), ",", ""), ".", "")
                TextBoxDS.Text = rowShow(0)("DS").ToString
            End If

        Else
            GroupBoxItem.Enabled = False
            RichTextBoxNote.Text = ""
            TextBoxAssetID.Text = ""
            TextBoxCost.Text = ""
            ComboBoxResponsible.Text = ""
            TextBoxSupplier.Text = ""
            ComboBoxEstimatedClosed.Text = ""
            ComboBoxOpenDate.Text = ""
            ButtonClosedDate.Text = ""
            ComboBoxRDA.Text = ""
            TextBoxOrder.Text = ""
            ComboBoxDai.Text = ""
            RichTextBoxNote.Text = ""
            TextBoxDS.Text = ""
        End If




        If UpdatingAuto And IsNumeric(TextBoxCost.Text) Then
            Dim MyInt As Integer = Val(TextBoxCost.Text)
            Dim MyCulture As New Globalization.CultureInfo("zh-CN")
            TextBoxCost.Text = MyInt.ToString("C0", MyCulture)
        End If
        If IsNumeric(TextBoxTotalCost.Text) Then
            Dim MyInt As Integer = Val(TextBoxTotalCost.Text)
            Dim MyCulture As New Globalization.CultureInfo("zh-CN")
            TextBoxTotalCost.Text = MyInt.ToString("C0", MyCulture)
        End If

        UpdatingAuto = False
        ButtonSave.BackColor = Color.Green
    End Sub



    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonSave.Click

        Application.DoEvents()
        ButtonSave.Enabled = False
        If CurrentAssetId() > 0 And CompareDatabase(CurrentAssetId()) Then
            If IsNumeric(Replace(Replace(IIf(Mid(TextBoxCost.Text, 2) <> "", Mid(TextBoxCost.Text, 2), "0"), ",", ""), ".", "")) Then

                Dim cmd As New MySqlCommand()
                Dim sql As String

                Try

                    sql = "UPDATE `" & DBName & "`.`EqAsset` SET " & _
                    "`description` = '" & Replace(Replace(RichTextBoxNote.Text, "\", "\\"), "'", "") & _
                    "',`Idasset` = '" & TextBoxAssetID.Text & _
                    "',`responsible` = '" & Trim(UCase(ComboBoxResponsible.Text)) & _
                    "',`EstimatedDate` = '" & ComboBoxEstimatedClosed.Text & _
                    "',`OpenDate` = '" & ComboBoxOpenDate.Text & _
                    "',`Supplier` = '" & TextBoxSupplier.Text & _
                    "',`ds` = '" & Replace((TextBoxDS.Text), "\", "\\") & _
                    "',`ClosedDate` = '" & ButtonClosedDate.Text & _
                    "',`rda` = '" & ComboBoxRDA.Text & _
                    "',`order` = '" & TextBoxOrder.Text & _
                    "',`dai` = '" & UCase(ComboBoxDai.Text) & _
                    "',`cost` = " & Replace(Replace(IIf(Mid(TextBoxCost.Text, 2) <> "", Mid(TextBoxCost.Text, 2), "0"), ",", ""), ".", "") & _
                    ",`name` = '" & Trim(Mid(TreeViewEQAsset.SelectedNode.Text, InStr(TreeViewEQAsset.SelectedNode.Text, "-") + 1)) & _
                    "' WHERE `eqasset`.`id` = " & CurrentAssetId() & " ;"


                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()


                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                ButtonSave.BackColor = Color.Green
                Try
                    DsEQAsset.Clear()
                    tblEQAsset.Clear()
                Catch ex As Exception

                End Try


                AdapterEQAsset.Fill(DsEQAsset, "EQAsset")
                tblEQAsset = DsEQAsset.Tables("EQAsset")
            Else
                MsgBox("Cost and RDA need be a number, DAI should be 8 char with ""K"" as start!")
            End If
        Else
            MsgBox("Section USED or Item not selected!")
        End If
        TextBoxTotalCost.Text = CostRecalculation()
        ButtonSave.Enabled = True
    End Sub

    Function SaveDelay()
        If CurrentAssetId() > 0 And CompareDatabase(CurrentAssetId()) Then

            Dim cmd As New MySqlCommand()
            Dim sql As String
            If TextBoxDelay.Text <> "" Then
                Try

                    sql = "UPDATE `" & DBName & "`.`EqAsset` SET " & _
                    "`delay` = " & TextBoxDelay.Text & _
                    " WHERE `eqasset`.`id` = " & CurrentAssetId() & " ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()


                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If

        Else
            MsgBox("Error in dave delay!")
        End If
    End Function

    Private Sub ButtonAssetImport_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonAssetImport.Click
        TextBoxAssetID.Text = toolAsset(CurrentToolId())
    End Sub

    Function toolAsset(ByVal toolId As Integer) As String
        Dim rowShow As DataRow()
        If toolId <> 0 Then
            rowShow = tblEQ.Select("id =" & toolId, "Id")
            If rowShow.Length > 0 Then
                toolAsset = rowShow(0)("asset_id").ToString()
            Else
                MsgBox("Error to recognize the Item tools Id " & toolId)
                toolAsset = ""
            End If
        Else
            toolAsset = ""
        End If


    End Function



    Sub loadComboResponsible()
        Dim rowResults As DataRow()
        Try
            ComboBoxResponsible.Items.Clear()
            ComboBoxResponsible.Items.Add("")

            rowResults = tblEQAsset.Select("name like '*'", "name")
            For Each row In rowResults
                If Not ComboBoxResponsible.Items.Contains(row("responsible").ToString) Then ComboBoxResponsible.Items.Add(row("responsible").ToString)
            Next
            ComboBoxResponsible.Sorted = True
        Catch ex As Exception

        End Try


    End Sub


    Sub loadComboDai()
        Dim rowResults As DataRow()
        Try
            ComboBoxDai.Items.Clear()
            ComboBoxDai.Items.Add("")
            ComboBoxDai.Items.Add("NO_DAI")
            rowResults = tblEQ.Select("IDACTIVITY = '" & TextBoxActivity.Text & "'")

            For Each row In rowResults
                If Not ComboBoxDai.Items.Contains(row("mpa").ToString) Then ComboBoxDai.Items.Add(row("mpa").ToString)
            Next
            ComboBoxDai.Sorted = True
        Catch ex As Exception

        End Try


    End Sub


    Private Sub TreeViewEQAsset_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles TreeViewEQAsset.DoubleClick
        Dim name = ""
        If CurrentAssetId() > 0 Then
            name = Trim(UCase(Replace(InputBox("Change the bame the item name: ", ), "'", "")))
            If name <> "" Then
                TreeViewEQAsset.SelectedNode.Text = Trim(Mid(TreeViewEQAsset.SelectedNode.Text, 1, InStr(TreeViewEQAsset.SelectedNode.Text, " - ") - 1)) & " - " & name
                ButtonSave_Click(Me, e)
            End If
        End If
    End Sub


    Private Sub TextBoxCost_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxCost.LostFocus
        Dim MyInt As Integer = TextBoxCost.Text
        Dim MyCulture As New Globalization.CultureInfo("zh-CN")
        TextBoxCost.Text = MyInt.ToString("C0", MyCulture)
    End Sub


    Sub FieldChange() Handles TextBoxAssetID.KeyUp, RichTextBoxNote.KeyUp, ComboBoxResponsible.KeyUp, _
        TextBoxSupplier.KeyUp, ComboBoxEstimatedClosed.TextChanged, ComboBoxOpenDate.TextChanged, _
        ButtonClosedDate.Click, ComboBoxRDA.KeyUp, TextBoxOrder.KeyUp, ComboBoxDai.KeyUp, TextBoxTotalCost.KeyUp, TextBoxDS.TextChanged, TextBoxCost.TextChanged
        If UpdatingAuto = False Then
            ButtonSave.BackColor = Color.Red
        End If
    End Sub



    Private Sub TextBoxTotalCost_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxTotalCost.TextChanged
        If UpdatingAuto And IsNumeric(TextBoxTotalCost.Text) Then
            Dim MyInt As Integer = TextBoxTotalCost.Text
            Dim MyCulture As New Globalization.CultureInfo("zh-CN")
            TextBoxTotalCost.Text = MyInt.ToString("C0", MyCulture)
        End If
    End Sub

    Function CostRecalculation() As Integer
        Dim rowResults As DataRow()
        Dim rowShowAsset As DataRow()
        CostRecalculation = 0
        Try
            DsEQ.Clear()
            tblEQ.Clear()
        Catch ex As Exception

        End Try

        AdapterEQ.Fill(DsEQ, "equipment")
        tblEQ = DsEQ.Tables("equipment")

        Try
            DsEQAsset.Clear()
            tblEQAsset.Clear()
        Catch ex As Exception

        End Try


        AdapterEQAsset.Fill(DsEQAsset, "EQAsset")
        tblEQAsset = DsEQAsset.Tables("EQAsset")

        rowResults = tblEQ.Select("idactivity ='" & TextBoxActivity.Text & "'", "Id")

        For Each row In rowResults

            rowShowAsset = tblEQAsset.Select("idtool ='" & row("Id").ToString & "'", "Id")

            For Each rowshow In rowShowAsset
                CostRecalculation = CostRecalculation + Int(rowshow("cost").ToString)
            Next

        Next

    End Function


    Private Sub TextBoxCost_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxCost.TextChanged

        If UpdatingAuto And IsNumeric(TextBoxCost.Text) Then
            Dim MyInt As Integer = Val(TextBoxCost.Text)
            Dim MyCulture As New Globalization.CultureInfo("zh-CN")
            TextBoxCost.Text = MyInt.ToString("C0", MyCulture)
        End If

    End Sub

    Private Sub ButtonComponentDelLink_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonComponentDelLink.Click
        TextBoxDS.Text = ""
    End Sub

    Private Sub ButtonComponentOpenDs_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonComponentOpenDs.Click
        Try
            Process.Start(TextBoxDS.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonComponetAddDs_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ButtonComponetAddDs.Click
        OpenFileDialog1.Filter = "All File (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        If CurrentAssetId() > 0 Then
            If OpenFileDialog1.CheckFileExists And InStr(OpenFileDialog1.FileName, "'", CompareMethod.Text) <= 0 Then
                Try
                    MkDir(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("DataSheetEQFolder"))
                Catch ex As Exception

                End Try
                Try
                    FileCopy(OpenFileDialog1.FileName, ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("DataSheetEQFolder") & CurrentAssetId() & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")))
                    MsgBox("File copied!   " & vbCrLf & vbCrLf & ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("DataSheetEQFolder") & CurrentAssetId() & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")), MsgBoxStyle.Information)
                    TextBoxDS.Text = ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("DataSheetEQFolder") & CurrentAssetId() & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, "."))

                Catch ex As Exception
                    MsgBox("File exist or others file problem!" & ex.Message)
                End Try
            End If
        Else
            MsgBox("Please select a component!")
        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Button1.Click
        FillTreeViewEQAsset()
    End Sub

    Private Sub GroupBoxItem_Enter(ByVal sender As System.Object, ByVal e As EventArgs) Handles GroupBoxItem.Enter

    End Sub
End Class