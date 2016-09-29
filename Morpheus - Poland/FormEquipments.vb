
Option Explicit Off
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class FormEquipments

    Dim XmlTree As New TreeViewToFromXml
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by idactivity", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet
    Dim AdapterEQ As New MySqlDataAdapter("SELECT * FROM EQUIPMENTS", MySqlconnection)
    Dim tblEQ As DataTable
    Dim DsEQ As New DataSet
    Dim OpenSession As Boolean, UpdatigTree As Boolean = True
    Dim AdapterCus As New MySqlDataAdapter("SELECT * FROM Customer", MySqlconnection)
    Dim tblCus As DataTable
    Dim DsCus As New DataSet
    Dim NodeSelect As Integer
    Dim CurrentNodeIndex As Integer



    Private Sub FormEquipments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdatigTree = True
        ComboBoxActivityId.Items.Add("")
        ComboBoxActivityId.Items.Add("GBES - Grugliasco Build Equipment Service")
        FillCustomerCombo()


        ComboBoxmpa.Items.Add("")


        ComboBoxStatus.Items.Add("")
        ComboBoxStatus.Items.Add("OPEN")
        ComboBoxStatus.Items.Add("CLOSED")
        ComboBoxStatus.Items.Add("STANDBY")
        ComboBoxStatus.Items.Add("NONEED")

        ComboBoxToolsType.Items.Add("")
        ComboBoxToolsType.Items.Add("STENCIL")
        ComboBoxToolsType.Items.Add("CARRIER")
        ComboBoxToolsType.Items.Add("ICT")
        ComboBoxToolsType.Items.Add("FVT")
        ComboBoxToolsType.Items.Add("JIG")
        ComboBoxToolsType.Items.Add("SAMPLES")
        ComboBoxToolsType.Items.Add("POTTING_MACH")
        ComboBoxToolsType.Items.Add("SCREW_MACH")
        ComboBoxToolsType.Items.Add("DEPANELING")
        ComboBoxToolsType.Items.Add("MOULD")
        ComboBoxToolsType.Items.Add("OTHER")


        ComboBoxHWBuilding.Items.Add("")
        ComboBoxHWDebug.Items.Add("")
        ComboBoxHWDoc.Items.Add("")
        ComboBoxSWDebug.Items.Add("")
        ComboBoxHWBuilding.Items.Add("")
        ComboBoxStart.Items.Add("")
        ComboBoxEnd.Items.Add("")
        ComboBoxmpa.Items.Add("")
        ComboBoxSop.Items.Add("")

        ComboBoxHWBuilding.Items.Add("NA")
        ComboBoxHWDebug.Items.Add("NA")
        ComboBoxHWDoc.Items.Add("NA")
        ComboBoxSWDebug.Items.Add("NA")
        ComboBoxHWBuilding.Items.Add("NA")
        '  ComboBoxStart.Items.Add("NA")
        '  ComboBoxEnd.Items.Add("NA")

        ComboBoxHWBuilding.Items.Add("DONE")
        ComboBoxHWDebug.Items.Add("DONE")
        ComboBoxHWDoc.Items.Add("DONE")
        ComboBoxSWDebug.Items.Add("DONE")
        ComboBoxHWBuilding.Items.Add("DONE")
        ComboBoxStart.Items.Add("DONE")
        ComboBoxEnd.Items.Add("DONE")

        UpdateTreeEQList(False)
        UpdatigTree = True
        TreeViewEQ.HideSelection = False

        ComboBoxRange.Text = 60
        'ChartEq.ChartAreas(0).AxisX.LabelStyle.Angle = 90
        TreeViewEQ.Visible = True
        UpdatigTree = False
        ComboBoxToolsType.Text = ""
    End Sub

    Sub FillCustomerCombo()
        Dim rowResults As DataRow()

        ComboBoxCustomer.Items.Clear()
        ComboBoxCustomer.Items.Add("")
        Try
            DsCus.Clear()
            tblCus.Clear()
        Catch ex As Exception

        End Try

        AdapterCus.Fill(DsCus, "Customer")
        tblCus = DsCus.Tables("Customer")
        rowResults = tblCus.Select("name like '*'", "name")
        For Each row In rowResults
            ComboBoxCustomer.Items.Add(row("name").ToString)
        Next
        ComboBoxCustomer.Sorted = True
    End Sub

    'Sub DrawSimultaneusTools()

    '    Dim tblEQ As DataTable
    '    Dim DsEQ As New DataSet
    '    Dim rowShow As DataRow(), openTypeTool As Integer
    '    AdapterEQ.Fill(DsEQ, "equipment")
    '    tblEQ = DsEQ.Tables("equipment")
    '    Dim limit As Integer = Val(ComboBoxRange.Text)

    '    Dim xPoint(limit) As String
    '    Dim yPoint(limit) As Double
    '    Dim yPointT(limit) As Double
    '    'ChartEq.Series(0).Points.Clear()
    '    'ChartEq.Series(1).Points.Clear()

    '    rowShow = tblEQ.Select("type = '" & ComboBoxToolsType.Text & "' and status = 'OPEN' ")

    '    openTypeTool = 0
    '    For i = 1 To limit
    '        openTypeTool = 0
    '        For Each row In rowShow
    '            If Len(row("start").ToString) = 10 And Len(row("start").ToString) = 10 Then
    '                If string_to_date(row("start").ToString) < DateAdd("d", i, Today) _
    '                    And string_to_date(row("end").ToString) > DateAdd("d", i, Today) Then
    '                    openTypeTool = openTypeTool + 1
    '                End If
    '            End If
    '        Next


    '        xPoint(i) = (Format(DateAdd("d", i, Today), "MMM/dd"))
    '        yPoint(i) = openTypeTool


    '    Next
    '    Dim integral As Double = 0
    '    For i = 1 To limit
    '        integral = yPoint(i) + integral
    '    Next
    '    For i = 1 To limit
    '        yPointT(i) = (integral / limit)
    '    Next
    '    'ChartEq.Series(0).Points.DataBindXY(xPoint, yPoint)
    '    'ChartEq.Series(1).Points.DataBindXY(xPoint, yPointT)

    'End Sub

    Sub ResetComboData()


        ComboBoxHWBuilding.Items.Clear()
        ComboBoxHWDebug.Items.Clear()
        ComboBoxHWDoc.Items.Clear()
        ComboBoxSWDebug.Items.Clear()
        ComboBoxStart.Items.Clear()
        ComboBoxEnd.Items.Clear()
        ComboBoxmpa.Items.Clear()
        ComboBoxSop.Items.Clear()

        ComboBoxHWBuilding.Items.Add("")
        ComboBoxHWDebug.Items.Add("")
        ComboBoxHWDoc.Items.Add("")
        ComboBoxSWDebug.Items.Add("")
        ComboBoxStart.Items.Add("")
        ComboBoxEnd.Items.Add("")
        ComboBoxmpa.Items.Add("")
        ComboBoxSop.Items.Add("")

        ComboBoxHWDebug.Items.Add("NA")
        ComboBoxHWDoc.Items.Add("NA")
        ComboBoxSWDebug.Items.Add("NA")
        ComboBoxHWBuilding.Items.Add("NA")
        'ComboBoxEnd.Items.Add("NA")
        'ComboBoxStart.Items.Add("NA")

        ComboBoxmpa.Items.Add("")


        ComboBoxSWDebug.Items.Add("DONE")
        ComboBoxHWBuilding.Items.Add("DONE")
        ComboBoxHWDebug.Items.Add("DONE")
        ComboBoxHWDoc.Items.Add("DONE")
        ComboBoxEnd.Items.Add("DONE")

    End Sub

    Sub UpdateTreeEQList(ByVal refresh As Boolean)

        Static Dim tblEQ As DataTable
        Static Dim DsEQ As New DataSet
        Dim rootNode As TreeNode, activity As String
        Dim rootChildren1 As TreeNode
        Dim rowShow As DataRow(), i As Integer
        TreeViewEQ.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        TreeViewEQ.Nodes.Clear()
        TreeViewEQ.BackColor = Color.White
        Try
            i = tblEQ.Rows.Count
            AdapterEQ.Update(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        Catch ex As Exception
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End Try

        If refresh Then
            DsEQ.Clear()
            tblEQ.Clear()
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End If

        rowShow = tblEQ.Select(IIf(ComboBoxToolsType.Text = "", "type like '*' and ", "type = '" & ComboBoxToolsType.Text & "' and ") & IIf(ComboBoxStatus.Text = "", "status like '*' ", "status = '" & ComboBoxStatus.Text & "'"), "IdActivity, status")

        activity = ""
        UpdatigTree = True
        For Each row In rowShow

            If row("IdActivity").ToString <> activity And IIf(CheckBoxOpen.Checked, Not ActivityStatusClosed(row("IdActivity").ToString, refresh), True) Then
                rootNode = New TreeNode(row("IdActivity").ToString)

                TreeViewEQ.BeginUpdate()
                TreeViewEQ.Nodes.Add(rootNode)
                TreeViewEQ.EndUpdate()
                TreeViewEQ.ResumeLayout()
                activity = row("IdActivity").ToString
                If ActivityStatusOnTime(row("IdActivity").ToString, refresh) Then rootNode.ForeColor = Color.Green
                If ActivityStatusDelay(row("IdActivity").ToString, refresh) Then rootNode.ForeColor = Color.Red
                If ActivityStatusClosed(row("IdActivity").ToString, refresh) Then rootNode.ForeColor = Color.Gray
                If ActivityStatusStandby(row("IdActivity").ToString, refresh) Then rootNode.ForeColor = Color.Blue
            End If

            If IIf(CheckBoxOpen.Checked, Not ActivityStatusClosed(row("IdActivity").ToString, refresh), True) Then
                rootChildren1 = New TreeNode(row("id").ToString & " - " & IIf(row("type").ToString <> "", row("type").ToString, "?") & " - " & row("ToolName").ToString)
                rootChildren1.NodeFont = New Font("Segoe UI", 10, FontStyle.Regular)
                If row("status").ToString = ("CLOSED") Then rootChildren1.ForeColor = Color.Gray
                If row("status").ToString = ("STANDBY") Then rootChildren1.ForeColor = Color.Blue
                If row("status").ToString = ("NONEED") Then rootChildren1.ForeColor = Color.Gray
                If row("status").ToString = ("OPEN") Then
                    If TimingEQ(row("id").ToString, tblEQ) = ("ONTIME") Then
                        rootChildren1.ForeColor = Color.Green
                    ElseIf TimingEQ(row("id").ToString, tblEQ) = ("DELAY") Then
                        rootChildren1.ForeColor = Color.Red
                    Else

                    End If
                End If
                'TreeViewEQ.BeginUpdate()
                rootNode.Nodes.Add(rootChildren1)
                'TreeViewEQ.EndUpdate()
                TreeViewEQ.ResumeLayout()


            End If
            refresh = False
        Next
        'ResumeLayout()
        UpdatigTree = False
    End Sub

    Function ActivityStatusStandby(ByVal act As String, ByVal refresh As Boolean) As Boolean
        Static Dim tblEQ As DataTable
        Static Dim DsEQ As New DataSet, i As Integer

        Dim rowShow As DataRow()
        ActivityStatusStandby = False

        Try
            i = tblEQ.Rows.Count
        Catch ex As Exception
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End Try

        If refresh Then
            DsEQ.Clear()
            tblEQ.Clear()
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End If

        rowShow = tblEQ.Select("idactivity = '" & act & "' and status = 'STANDBY'")

        If rowShow.Length > 0 Then
            ActivityStatusStandby = True
        End If


    End Function

    Function ActivityStatusClosed(ByVal act As String, ByVal refresh As Boolean) As Boolean
        Static Dim tblEQ As DataTable
        Static Dim DsEQ As New DataSet, i As Integer

        Dim rowShow As DataRow()
        ActivityStatusClosed = True

        Try
            i = tblEQ.Rows.Count
        Catch ex As Exception
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End Try

        If refresh Then
            DsEQ.Clear()
            tblEQ.Clear()
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End If


        rowShow = tblEQ.Select("idactivity = '" & act & "' and not status like 'CLOSED' and not status like 'NONEED'")

        If rowShow.Length > 0 Then
            ActivityStatusClosed = False
        End If

    End Function

    Function ActivityStatusDelay(ByVal act As String, ByVal refresh As Boolean) As Boolean
        Static Dim tblEQ As DataTable
        Static Dim DsEQ As New DataSet, i As Integer

        Dim rowShow As DataRow()
        ActivityStatusDelay = False

        Try
            i = tblEQ.Rows.Count
        Catch ex As Exception
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End Try

        If refresh Then
            DsEQ.Clear()
            tblEQ.Clear()
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End If

        rowShow = tblEQ.Select("idactivity = '" & act & "'")
        For Each row In rowShow
            If TimingEQ(row("id").ToString, tblEQ) = "DELAY" Then ActivityStatusDelay = True
        Next

    End Function

    Function ActivityStatusOnTime(ByVal act As String, ByVal refresh As Boolean) As Boolean
        Static Dim tblEQ As DataTable
        Static Dim DsEQ As New DataSet
        Dim timing As String = ""
        Dim rowShow As DataRow(), i As Integer
        ActivityStatusOnTime = True

        Try
            i = tblEQ.Rows.Count
        Catch ex As Exception
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End Try

        If refresh Then
            DsEQ.Clear()
            tblEQ.Clear()
            AdapterEQ.Fill(DsEQ, "equipment")
            tblEQ = DsEQ.Tables("equipment")
        End If


        rowShow = tblEQ.Select("idactivity = '" & act & "'")

        For Each row In rowShow

            If TimingEQ(row("id").ToString, tblEQ) = "ONTIME" Or TimingEQ(row("id").ToString, tblEQ) = "CLOSED" Then
                timing = "ONTIME"
            Else
                timing = ""
            End If
        Next
        If timing = "DELAY" Or timing = "" Then ActivityStatusOnTime = False
    End Function

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

    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click

        Dim rootNode As TreeNode, rootNodeChild As TreeNode
        Dim cmd As New MySqlCommand()
        Dim sql As String
        myNodeSelect(True)

        If Not IsNothing(TreeViewEQ.SelectedNode) Then
            rootNode = TreeViewEQ.SelectedNode.Parent
            rootNodeChild = New TreeNode("---- New Tool ----")

            TreeViewEQ.BeginUpdate()
            rootNode.Nodes.Add(rootNodeChild)

            TreeViewEQ.EndUpdate()
            TreeViewEQ.ResumeLayout()

            Try
                sql = "INSERT INTO `" & DBName & "`.`equipments` (`ToolName`,`idactivity` ) VALUES ( '" & "---- New Tool ----" & "' , '" & ComboBoxActivityId.Text & "'" & ");"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            UpdateTreeEQList(True)

            rootNode.Expand()



        End If

        myNodeSelect(False)

    End Sub

    Private Sub TreeViewEQ_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewEQ.AfterSelect
        Dim tblEQ As DataTable
        Dim DsEQ As New DataSet
        Dim rowShow As DataRow(), id As Integer = 0
        Dim UpdatigTreePrev As Boolean
        AdapterEQ.Fill(DsEQ, "equipments")
        tblEQ = DsEQ.Tables("equipments")
        UpdatigTreePrev = UpdatigTree
        id = CurrentID()

        If currentActivityID() > 0 Then
            resetField()
            TextBoxToolName.Visible = False
            ButtonRemove.Visible = False
            ButtonAdd.Visible = False
            rowShow = tblEQ.Select("IDactivity='" & TreeViewEQ.SelectedNode.Text & "'")
            UpdatigTree = True
            ComboBoxActivityId.Text = TreeViewEQ.SelectedNode.Text
            UpdatigTree = UpdatigTreePrev
            SetControl("ACTIVITY")
            ComboBoxmpa.Enabled = True
            ComboBoxSop.Enabled = True
            ComboBoxCustomer.Enabled = True
            TextBoxWorkHours.Enabled = False

            DateTimePickerSop.Enabled = True
            TextBoxToolId.Visible = False
            Label9.Visible = False
            ButtonAdd.Enabled = False
            ButtonRemove.Enabled = False
        ElseIf id > 0 Then
            TextBoxToolName.Visible = True
            rowShow = tblEQ.Select("ID=" & id)
            TextBoxWorkHours.Enabled = True
            SetControl("TOOL")
            ComboBoxmpa.Enabled = False
            ComboBoxSop.Enabled = False
            ComboBoxCustomer.Enabled = False
            DateTimePickerSop.Enabled = False
            TextBoxToolId.Visible = True
            Label9.Visible = True
            ButtonAdd.Enabled = True
            ButtonRemove.Enabled = True
        Else
            MsgBox("Not correct selection!")
            Exit Sub
        End If



        If rowShow.Length > 0 Then

            UpdatigTree = True

            ComboBoxToolsType.Text = rowShow(0).Item("type").ToString
            ComboBoxStatus.Text = rowShow(0).Item("Status").ToString

            If id > 0 Then
                Try
                    RichTextBoxNote.Text = rowShow(0).Item("note").ToString
                Catch ex As Exception

                End Try
            ElseIf currentActivityID() > 0 Then
                Try
                    RichTextBoxNote.Text = rowShow(0).Item("noteactivity").ToString
                Catch ex As Exception

                End Try
            End If

            TextBoxToolName.Text = rowShow(0).Item("ToolName").ToString
            If Not currentActivityID() > 0 Then TextBoxWorkHours.Text = rowShow(0).Item("WorkHour").ToString
            ComboBoxActivityId.Items.Clear()
            ComboBoxActivityId.Items.Add(rowShow(0).Item("idactivity").ToString)
            ComboBoxActivityId.Text = rowShow(0).Item("idactivity").ToString
            TextBoxToolId.Text = rowShow(0).Item("asset_ID").ToString
            'If ComboBoxActivityId.Text <> "" Then Clipboard.SetText(ComboBoxActivityId.Text)
            Application.DoEvents()
            ResetComboData()
            If currentActivityID() > 0 Then TextBoxWorkHours.Text = sumHour(ComboBoxActivityId.Text)


            ComboBoxSop.Items.Add(rowShow(0).Item("sop").ToString)
            ComboBoxmpa.Items.Add(rowShow(0).Item("mpa").ToString)
            ComboBoxHWBuilding.Items.Add(rowShow(0).Item("HWBuilding").ToString)
            ComboBoxHWDebug.Items.Add(rowShow(0).Item("HwDebug").ToString)
            ComboBoxHWDoc.Items.Add(rowShow(0).Item("HWDoc").ToString)
            ComboBoxSWDebug.Items.Add(rowShow(0).Item("SWDebug").ToString)
            ComboBoxStart.Items.Add(rowShow(0).Item("start").ToString)
            ComboBoxEnd.Items.Add(rowShow(0).Item("end").ToString)


            ComboBoxSop.Text = rowShow(0).Item("sop").ToString
            ComboBoxmpa.Text = rowShow(0).Item("mpa").ToString
            ComboBoxHWBuilding.Text = rowShow(0).Item("HWBuilding").ToString
            ComboBoxHWDebug.Text = rowShow(0).Item("HwDebug").ToString
            ComboBoxHWDoc.Text = rowShow(0).Item("HWDoc").ToString
            ComboBoxSWDebug.Text = rowShow(0).Item("SWDebug").ToString
            ComboBoxStart.Text = rowShow(0).Item("start").ToString
            ComboBoxEnd.Text = rowShow(0).Item("end").ToString
            ComboBoxCustomer.Text = rowShow(0).Item("customer").ToString
            UpdatigTree = UpdatigTreePrev
        End If

    End Sub

    Function sumHour(ByVal act As String) As Integer
        Dim tblEQ As DataTable
        Dim DsEQ As New DataSet
        Dim timing As String = ""
        Dim rowShow As DataRow(), i As Integer

        sumHour = 0

        AdapterEQ.Fill(DsEQ, "equipment")
        tblEQ = DsEQ.Tables("equipment")


        rowShow = tblEQ.Select("idactivity = '" & act & "'")

        For Each row In rowShow
            i = row("id").ToString
            sumHour = sumHour + Val(row("workhour").ToString)
        Next
    End Function

    Private Sub ButtonBomSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Dim id As Integer, n As TreeNode, idSelected As Integer
        id = CurrentID()
        If id > 0 Then
            If (ComboBoxStatus.Text <> "" And ComboBoxToolsType.Text <> "") Or (ComboBoxStatus.Text = "NONEED") Then
                If DeltaSessionTime("equipments", id) < 30 And session("equipments", id, False) = "RESET" Then
                    TextBoxTime.Text = ""
                    Dim cmd As New MySqlCommand()
                    Dim sql As String

                    Try

                        sql = "UPDATE `" & DBName & "`.`equipments` SET " & _
                        "`start` = '" & ComboBoxStart.Text & _
                        "',`IdActivity` = '" & ComboBoxActivityId.Text & _
                        "',`toolname` = '" & TextBoxToolName.Text & _
                        "',`type` = '" & ComboBoxToolsType.Text & _
                        "',`sop` = '" & ComboBoxSop.Text & _
                        "',`mpa` = '" & ComboBoxmpa.Text & _
                        "',`start` = '" & ComboBoxStart.Text & _
                        "',`Asset_ID` = '" & TextBoxToolId.Text & _
                        "',`HWDebug` = '" & ComboBoxHWDebug.Text & _
                        "',`HWDoc` = '" & ComboBoxHWDoc.Text & _
                        "',`HWBuilding` = '" & ComboBoxHWBuilding.Text & _
                        "',`workhour` = " & Int(Val(TextBoxWorkHours.Text)) & _
                        ",`SWDebug` = '" & ComboBoxSWDebug.Text & _
                        "',`note` = '" & Replace(Replace(RichTextBoxNote.Text, "'", ""), "'", "") & _
                        "',`end` = '" & ComboBoxEnd.Text & _
                        "',`status` = '" & ComboBoxStatus.Text & "' WHERE `equipments`.`id` = " & id & " ;"


                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        idSelected = CurrentID()
                        TreeViewEQ.HideSelection = False
                        TreeViewEQ.Focus()
                        TreeViewEQ.SelectedNode.Text = Mid(TreeViewEQ.SelectedNode.Text, 1, InStr(TreeViewEQ.SelectedNode.Text, " - ") - 1) & " - " & IIf(ComboBoxToolsType.Text <> "", ComboBoxToolsType.Text, "?") & " - " & TextBoxToolName.Text



                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try



                    ButtonSave.BackColor = Color.Green
                    OpenSession = False
                    TimerRecord.Stop()


                Else
                    MsgBox("Section USED " & session("BomOffer", id, False))
                End If
            Else
                MsgBox(" Please fill the toolsType, status, start and end!", MsgBoxStyle.Information)
            End If
        ElseIf currentActivityID() > 0 Then

            Dim cmd As New MySqlCommand()
            Dim sql As String

            Try

                sql = "UPDATE `" & DBName & "`.`equipments` SET " & _
                "`sop` = '" & ComboBoxSop.Text & _
                "',`mpa` = '" & ComboBoxmpa.Text & _
                 "',`customer` = '" & ComboBoxCustomer.Text & _
                "',`NoteActivity` = '" & Replace(Replace(RichTextBoxNote.Text, "'", ""), "'", "") & _
                "',`start` = '" & ComboBoxStart.Text & "' WHERE `equipments`.`idactivity` = '" & TreeViewEQ.SelectedNode.Text & "' ;"


                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                ButtonSave.BackColor = Color.Green

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else
            MsgBox("Section USED " & session("BomOffer", id, False))
        End If

    End Sub

    Sub myNodeSelect(ByVal read As Boolean)
        Static Dim selNode As TreeNode

        If read Then
            selNode = TreeViewEQ.SelectedNode
            TreeViewEQ.Select()

        Else
            TreeViewEQ.Visible = False
            Dim idSelected As Integer = Mid(selNode.Text, 1, InStr(selNode.Text, " - ") - 1)
            Dim id As Integer
            TreeViewEQ.HideSelection = False
            For Each node In TreeViewEQ.Nodes
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

            TreeViewEQ.SelectedNode = selNode
            TreeViewEQ.TopNode = TreeViewEQ.SelectedNode.Parent
            TreeViewEQ.Focus()
            TreeViewEQ.Visible = True
        End If

    End Sub

    Function currentActivityID() As Integer

        currentActivityID = 0
        Try
            currentActivityID = Mid(TreeViewEQ.SelectedNode.Text, 1, InStr(TreeViewEQ.SelectedNode.Text, " - ") - 1)
        Catch ex As Exception

        End Try
        Try
            If TreeViewEQ.SelectedNode.Level = 1 Then
                currentActivityID = 0
            Else

            End If
        Catch ex As Exception

        End Try


    End Function

    Private Sub Controls_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
    ComboBoxEnd.TextChanged, ComboBoxHWDoc.TextChanged, ComboBoxHWDebug.TextChanged, ComboBoxHWBuilding.TextChanged, _
    ComboBoxStart.TextChanged, ComboBoxSWDebug.TextChanged, ComboBoxCustomer.TextChanged, TextBoxToolId.TextChanged, ComboBoxmpa.TextChanged, _
    ComboBoxSop.TextChanged, RichTextBoxNote.TextChanged, TextBoxToolName.TextChanged, TextBoxWorkHours.TextChanged

        'If ChartEq.Visible = False Then
        Dim id As Integer
        id = CurrentID()

        If UpdatigTree = False And id > 0 Then

            ButtonSave.BackColor = Color.OrangeRed

            If OpenSession = True Then

            Else

                If session("Offer", id, True) = "SET" Then  ' valid session
                    TextBoxTime.Text = "30"
                    TimerRecord.Interval = 60000
                    TimerRecord.Start()
                    OpenSession = True

                Else
                    MsgBox("Section USED " & session("equipments", id, False))

                End If
            End If
        ElseIf currentActivityID() > 0 And UpdatigTree = False Then
            ButtonSave.BackColor = Color.OrangeRed
        End If
        'End If
    End Sub

    Sub resetField()
        Dim tmpUpdate As Boolean
        tmpUpdate = UpdatigTree
        UpdatigTree = True
        ComboBoxToolsType.Text = ""
        ComboBoxStatus.Text = ""
        RichTextBoxNote.Text = ""
        TextBoxToolName.Text = ""
        ComboBoxActivityId.Text = ""
        ComboBoxSop.Text = ""
        ComboBoxmpa.Text = ""
        ComboBoxHWBuilding.Text = ""
        ComboBoxHWDebug.Text = ""
        ComboBoxHWDoc.Text = ""
        ComboBoxSWDebug.Text = ""
        ComboBoxHWBuilding.Text = ""
        ComboBoxStart.Text = ""
        ComboBoxEnd.Text = ""
        ComboBoxActivityId.Items.Add("")
        ComboBoxActivityId.Text = ""
        UpdatigTree = False
        UpdatigTree = tmpUpdate
    End Sub

    Private Sub TimerRecord_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerRecord.Tick
        Dim id As Integer
        id = CurrentID()
        If id = 0 Then
            OpenSession = False
            TextBoxTime.Text = ""
        End If
        If id > 0 Then
            If Val(TextBoxTime.Text) > 1 Then
                TextBoxTime.Text = Val(TextBoxTime.Text) - 1
            Else
                OpenSession = False
                TextBoxTime.Text = ""
                MsgBox("Session Bom expired!")
                TimerRecord.Stop()
                session("equipments", id, False)
                UpdateTreeEQList(True)
            End If
        End If
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String, id As Integer


        id = CurrentID()
        If id = 0 Then
            OpenSession = False
            TextBoxTime.Text = ""
        End If



        If vbYes = MsgBox("Do you want delete this Tool?", MsgBoxStyle.YesNo) Then
            If id > 0 Then
                If (session("equipments", id, False) = "RESET") Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`equipments` WHERE `equipments`.`id` = " & id
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Bom deleted!")
                        'UpdateTreeEQList(False)    
                        UpdatigTree = True
                        TreeViewEQ.SelectedNode.Remove()
                        UpdatigTree = False
                        Application.DoEvents()

                    Catch ex As Exception
                        MsgBox("Bom deleting error " & ex.Message)
                    End Try

                Else
                    MsgBox("session open! " & session("bomoffer", id, False))
                End If
            Else
                MsgBox("Please select a valid Tool!")
            End If
        End If

    End Sub

    Private Sub DateTimePickerSop_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerSop.CloseUp

        ComboBoxSop.Items.Clear()
        ComboBoxSop.Items.Add("")
        ComboBoxSop.Items.Add(date_to_string(DateTimePickerSop.Text))
        ComboBoxSop.Text = date_to_string(DateTimePickerSop.Text)
    End Sub

    Private Sub DateTimePickerStart_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerStart.CloseUp
        ComboBoxStart.Items.Clear()
        ComboBoxStart.Items.Add("")
        'ComboBoxStart.Items.Add("NA")
        ComboBoxStart.Items.Add("DONE")
        ComboBoxStart.Items.Add(date_to_string(DateTimePickerStart.Text))
        ComboBoxStart.Text = (date_to_string(DateTimePickerStart.Text))
    End Sub

    Private Sub DateTimePickerHWDoc_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerHWDoc.CloseUp
        ComboBoxHWDoc.Items.Clear()
        ComboBoxHWDoc.Items.Add("")
        ComboBoxHWDoc.Items.Add("NA")
        ComboBoxHWDoc.Items.Add("DONE")
        ComboBoxHWDoc.Items.Add(date_to_string(DateTimePickerHWDoc.Text))
        ComboBoxHWDoc.Text = date_to_string(DateTimePickerHWDoc.Text)
    End Sub

    Private Sub DateTimePickerHWBuilding_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerHWBuilding.CloseUp
        ComboBoxHWBuilding.Items.Clear()
        ComboBoxHWBuilding.Items.Add("")
        ComboBoxHWBuilding.Items.Add("NA")
        ComboBoxHWBuilding.Items.Add("DONE")
        ComboBoxHWBuilding.Items.Add(date_to_string(DateTimePickerHWBuilding.Text))
        ComboBoxHWBuilding.Text = date_to_string(DateTimePickerHWBuilding.Text)
    End Sub

    Private Sub DateTimePickerHWDebug_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerHWDebug.CloseUp

        ComboBoxHWDebug.Items.Clear()
        ComboBoxHWDebug.Items.Add("")
        ComboBoxHWDebug.Items.Add("NA")
        ComboBoxHWDebug.Items.Add("DONE")
        ComboBoxHWDebug.Items.Add(date_to_string(DateTimePickerHWDebug.Text))



        ComboBoxHWDebug.Text = date_to_string(DateTimePickerHWDebug.Text)
    End Sub

    Private Sub DateTimePickerSWDebug_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerSWDebug.CloseUp

        ComboBoxSWDebug.Items.Clear()
        ComboBoxSWDebug.Items.Add("")
        ComboBoxSWDebug.Items.Add("NA")
        ComboBoxSWDebug.Items.Add("DONE")
        ComboBoxSWDebug.Items.Add(date_to_string(DateTimePickerSWDebug.Text))


        ComboBoxSWDebug.Text = date_to_string(DateTimePickerSWDebug.Text)
    End Sub

    Private Sub DateTimePickerEnd_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerEnd.CloseUp

        ComboBoxEnd.Items.Clear()
        ComboBoxEnd.Items.Add("")
        ' ComboBoxEnd.Items.Add("NA")
        ComboBoxEnd.Items.Add("DONE")
        ComboBoxEnd.Items.Add(date_to_string(DateTimePickerEnd.Text))


        ComboBoxEnd.Text = date_to_string(DateTimePickerEnd.Text)
    End Sub

    Function CurrentID() As Integer

        CurrentID = 0
        Try
            CurrentID = Mid(TreeViewEQ.SelectedNode.Text, 1, InStr(TreeViewEQ.SelectedNode.Text, " - ") - 1)
        Catch ex As Exception

        End Try
        Try
            If TreeViewEQ.SelectedNode.Level = 0 Then
                CurrentID = 0
            Else

            End If
        Catch ex As Exception

        End Try




    End Function

    Private Sub ButtonAddActivity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddActivity.Click
        Dim nameActivity As String, i As Integer
        nameActivity = InputBox("Insert name activity in the format:  acronim - description" & vbCrLf & vbCrLf & "Example ""GBES - Grugliasco Build Equipment Service""")
        Try
            i = InStr(nameActivity, "-")
        Catch ex As Exception

        End Try

        If i > 0 Then
            ComboBoxActivityId.Items.Add(nameActivity)
        Else
            MsgBox("Please check the sintax")
        End If

    End Sub

    Private Sub ButtonLoadTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadTools.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        Dim currentIdActivity As Integer = 0, rowShow As DataRow(), activityname As String
        Dim TOOLNAME As String
        rowShow = tblProd.Select("statusActivity = 'open' or statusActivity = 'sent'")

        For Each row In rowShow
            If row("idactivity").ToString <> currentIdActivity And row("sop_task").ToString <> "" Then

                currentIdActivity = row("idactivity").ToString
                XmlTree.SetTreeView(TreeViewEQ)
                XmlTree.Import(row("sop_task").ToString)

                For Each node In TreeViewEQ.Nodes(0).Nodes

                    Dim dr As String = node.ToString
                    If InStr(node.ToString, "EQUIPMENTS") > 0 Then
                        Dim cmd As New MySqlCommand()
                        Dim sql As String
                        For Each n In node.Nodes
                            TOOLNAME = Replace((Mid(n.ToString, InStr(n.ToString, "-") + 1, InStr(InStr(n.ToString, "-") + 1, n.ToString, "-") - InStr(n.ToString, "-") - 1)), "_", " ")
                            activityname = row("idactivity").ToString & " - " & row("NAMEactivity").ToString

                            If Not ExistEq(TOOLNAME, activityname) Then
                                Try
                                    sql = "INSERT INTO `" & DBName & "`.`equipments` (`ToolName`,`idactivity` ) VALUES ( '" & TOOLNAME & "' , '" & activityname & " '" & ");"
                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()

                                Catch ex As Exception
                                    MsgBox(ex.Message)
                                End Try
                            End If
                        Next
                    End If
                Next
            Else

            End If
        Next
        TreeViewEQ.HideSelection = False
        UpdateTreeEQList(True)
        MsgBox("Equipments loaded!", vbInformation)
    End Sub

    Function ExistEq(ByVal toolname As String, ByVal activityname As String) As Boolean
        Dim tblEQ As DataTable
        Dim DsEQ As New DataSet
        Dim rowShow As DataRow()
        AdapterEQ.Fill(DsEQ, "equipments")
        tblEQ = DsEQ.Tables("equipments")
        rowShow = tblEQ.Select("toolname='" & toolname & "' and idactivity='" & activityname & "'", "IdActivity")
        If rowShow.Length > 0 Then
            ExistEq = True
        Else
            ExistEq = False
        End If

    End Function

    Private Sub TreeViewEQ_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewEQ.BeforeSelect
        If UpdatigTree = False And Not IsNothing(TreeViewEQ.SelectedNode) Then

            If OpenSession = True Then
                If vbYes = MsgBox("Session Open do you want Save?", MsgBoxStyle.YesNo) Then
                    ButtonBomSave_Click(Me, e)
                Else
                    OpenSession = False
                    ButtonSave.BackColor = Color.Green
                    TimerRecord.Stop()
                    TextBoxTime.Text = ""
                    session("equipments", CurrentID, False)
                End If
            End If
        End If
    End Sub

    Private Sub ButtonCollapsExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCollapsExpand.Click
        If ButtonCollapsExpand.Text = "C" Then
            ButtonCollapsExpand.Text = "E"
            TreeViewEQ.CollapseAll()
        Else
            ButtonCollapsExpand.Text = "C"
            TreeViewEQ.ExpandAll()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        UpdateTreeEQList(True)
    End Sub

    'Private Sub ButtonEqGr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEqGr.Click
    '    TreeViewEQ.Visible = False
    '    'ChartEq.Visible = False
    '    If ButtonEqGr.Text = "Equipments List" Then
    '        ButtonEqGr.Text = "Tools Schedule"
    '    Else
    '        ButtonEqGr.Text = "Equipments List"
    '    End If


    '    If ButtonEqGr.Text = "Equipments List" Then
    '        TreeViewEQ.Visible = True
    '        Label1Range.Visible = False
    '        ComboBoxRange.Visible = False
    '    End If

    '    If ButtonEqGr.Text = "Tools Schedule" Then
    '        'ChartEq.Visible = True
    '        Label1Range.Visible = True
    '        ComboBoxRange.Visible = True
    '        SetControl("ACTIVITY")
    '        DrawSimultaneusTools()
    '    End If



    'End Sub

    Function ReplaceChar(ByVal s As String) As String
        ReplaceChar = s
        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 93 Or Asc(Mid(s, i, 1)) = 91 Or Asc(Mid(s, i, 1)) = 59 Or Asc(Mid(s, i, 1)) = 46 Or Asc(Mid(s, i, 1)) = 37 Or Asc(Mid(s, i, 1)) = 32 Then
            Else
                s = Replace(s, Mid(s, i, 1), "-")
            End If
            ReplaceChar = s
        Next

    End Function

    Private Sub ComboBoxEnd_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEnd.SelectedIndexChanged
        If ComboBoxEnd.Text = "DONE" And UpdatigTree = False Then
            ComboBoxHWBuilding.Text = "DONE"
            ComboBoxHWDebug.Text = "DONE"
            ComboBoxHWDoc.Text = "DONE"
            ComboBoxSWDebug.Text = "DONE"
        End If
        If (ComboBoxEnd.Text = "NA" Or ComboBoxStart.Text = "NA") And UpdatigTree = False Then
            ComboBoxEnd.Text = "NA"
            ComboBoxStart.Text = "NA"
            ComboBoxHWBuilding.Text = "NA"
            ComboBoxHWDebug.Text = "NA"
            ComboBoxHWDoc.Text = "NA"
            ComboBoxSWDebug.Text = "NA"
        End If
    End Sub

    Sub SetControl(ByVal activity As String)

        If activity = "TOOL" Then

            ButtonAdd.Visible = True
            ButtonRemove.Visible = True
            GroupBoxDate.Visible = True
        Else

            ButtonAdd.Visible = False
            ButtonRemove.Visible = False
            GroupBoxDate.Visible = False
        End If

    End Sub

    Private Sub TreeViewEQ_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewEQ.DoubleClick
        FormEqItem.CurrentActivityId = ComboBoxActivityId.Text
        FormEqItem.Show()

    End Sub


End Class