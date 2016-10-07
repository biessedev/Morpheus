Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class FormTime
    Dim CultureInfo_ja_JP As New System.Globalization.CultureInfo("ja-JP", False)
    Dim XmlTree As New TreeViewToFromXml

    Dim AdapterTP As New MySqlDataAdapter("SELECT * FROM TimeProject", MySqlconnection)
    Dim tblTP As DataTable
    Dim DsTP As New DataSet

    Dim OpenSession As Boolean, UpdatigTree As Boolean = True
    Dim AdapterCus As New MySqlDataAdapter("SELECT * FROM Customer", MySqlconnection)
    Dim tblCus As DataTable
    Dim DsCus As New DataSet
    Dim tbltp_static As DataTable
    Dim Dstp_static As New DataSet
    Dim NodeSelect As Integer
    Dim CurrentNodeIndex As Integer
    Dim yelloDelay As Integer = 5
    Private Sub FormTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AdapterTP.Fill(DsTP, "TimeProject")
        tblTP = DsTP.Tables("TimeProject")


        FillcomboQualityCompleated()
        ComboBoxStatusFilter.Items.Add("")
        ComboBoxStatusFilter.Items.Add("OPEN")
        ComboBoxStatusFilter.Items.Add("CLOSED")
        ComboBoxStatusFilter.Items.Add("STANDBY")
        ComboBoxCompleatedFilter.Items.Add("")
        ComboBoxCompleatedFilter.Items.Add("100")

        FillCustomerCombo()
        FillAreaCombo()
        FillResponsibleCombo()
        FillTaskLeaderCombo()
        UpdateTreeTPList(True)


        If controlRight("J") >= 2 Then ButtonAddProject.Visible = True
        If controlRight("J") >= 2 Then ButtonSave.Visible = True
        If controlRight("J") >= 2 Then ButtonDelProject.Visible = True
        If controlRight("J") >= 2 Then ButtonDuplicate.Visible = True


        ComboBoxResponsible.Sorted = True

    End Sub

    Sub FillcomboQualityCompleated()

        For i = 0 To 100 Step 2
            ComboBoxQuality.Items.Add(i)
            ComboBoxCompleated.Items.Add(i)
        Next


    End Sub

    Function DelayAdvanceTime(ByVal id As String, ByVal refresh As Boolean) As Integer
        Dim totaltime As Integer = DateDiff("d", string_to_date(ProjectStart(id, False)), string_to_date(ProjectEnd(id, False)))

        Try
            DelayAdvanceTime = DateDiff("d", Today, DateAdd("d", Int(totaltime * (ProjectCompleated(id, False)) / 100), string_to_date(ProjectStart(id, False))))

        Catch ex As Exception
            DelayAdvanceTime = 0
        End Try
    End Function

    Function DelayAdvanceTimeTask(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As Integer

        Dim totaltime As Integer = DateDiff("d", string_to_date(TaskStart(id, idp, True)), string_to_date(TaskEnd(id, idp, True)))
        DelayAdvanceTimeTask = DateDiff("d", Today, DateAdd("d", Int(totaltime * Val(TaskCompleated(id, idp, False)) / 100), string_to_date(TaskStart(id, idp, False))))
    End Function

    Sub UpdateTreeTPList(ByVal refresh As Boolean)


        Dim rootNode As TreeNode, Project As String, projectStatusStr As String
        Dim rootChildren1 As TreeNode
        Dim rowShow As DataRow(), sql As String
        TreeViewTP.Font = New Font("Courier New", 12, FontStyle.Bold)
        TreeViewTP.Nodes.Clear()
        TreeViewTP.BackColor = Color.White

        If refresh Then
            DsTP.Clear()
            tblTP.Clear()
            AdapterTP.Fill(DsTP, "TimeProject")
            tblTP = DsTP.Tables("TimeProject")
        End If
        sql = IIf(ComboBoxAreaFilter.Text = "", "area like '*' and ", "area = '" & ComboBoxAreaFilter.Text & "' and ") &
                               IIf(ComboBoxStatusFilter.Text = "", "status like '*'  and ", "status = '" & ComboBoxStatusFilter.Text & "'  and ") &
                               IIf(ComboBoxCustomerFilter.Text = "", "customer like '*'  and ", "customer = '" & ComboBoxCustomerFilter.Text & "'  and ") &
                               IIf(ComboBoxCompleatedFilter.Text = "", "compleated like '*'  and ", "compleated = '" & ComboBoxCompleatedFilter.Text & "'  and ") &
                               IIf(CheckBoxTemplate.Checked = False, "not project like '*template*'  and ", "") &
                               IIf(ComboBoxtaskFilter.Text = "", "", " taskleader = '" & ComboBoxtaskFilter.Text & "'  and ") &
                               IIf(ComboBoxResponsibleFilter.Text = "", "ProjectLeader like '*'  ", "projectleader = '" & ComboBoxResponsibleFilter.Text & "'  ")
        rowShow = tblTP.Select(sql, "project, id")

        Project = ""
        UpdatigTree = True
        projectStatusStr = ""
        For Each row In rowShow

            If row("project").ToString <> Project Then
                rootNode = New TreeNode(row("project").ToString)

                TreeViewTP.BeginUpdate()
                TreeViewTP.Nodes.Add(rootNode)
                TreeViewTP.EndUpdate()
                TreeViewTP.ResumeLayout()
                Project = row("project").ToString
                projectStatusStr = ProjectStatus(row("project").ToString, refresh)
                If projectStatusStr = "ONTIME" Then rootNode.ForeColor = Color.Green
                If projectStatusStr = "DELAY" Then rootNode.ForeColor = Color.Red
                If projectStatusStr = "CLOSED" Then rootNode.ForeColor = Color.Gray
                If projectStatusStr = "STANDBY" Then rootNode.ForeColor = Color.Blue
                If projectStatusStr = "CRITIC" Then rootNode.ForeColor = Color.Orange

                If row("projectleader").ToString = "" Or
                    row("area").ToString = "" Then

                    rootNode.ForeColor = Color.DarkMagenta

                End If



            End If

            ' If row("taskleader").ToString = "BAXI" Then Stop
            rootChildren1 = New TreeNode(row("taskname").ToString)
            rootChildren1.NodeFont = New Font("Courier New", 10, FontStyle.Bold)

            If ProjectStatusOpenClosed(row("project").ToString, True) = "OPEN" Then
                If row("status").ToString = ("CLOSED") Then rootChildren1.ForeColor = Color.Gray
                If row("status").ToString = ("STANDBY") Then rootChildren1.ForeColor = Color.Blue

                If Len(row("start").ToString) <> 10 And Len(row("end").ToString) <> 10 Then

                ElseIf TimingTS(row("start").ToString, row("end").ToString, row("compleated").ToString, row("status").ToString) = ("ONTIME") Then
                    rootChildren1.ForeColor = Color.Green
                ElseIf DelayAdvanceTimeTask(row("taskname").ToString, row("project").ToString, True) > -yelloDelay Then
                    rootChildren1.ForeColor = Color.Orange
                Else
                    rootChildren1.ForeColor = Color.Red
                End If

            End If

            rootNode.Nodes.Add(rootChildren1)
            TreeViewTP.ResumeLayout()

            refresh = False

            If row("taskleader").ToString = "" Then

                rootChildren1.ForeColor = Color.DarkMagenta
                rootNode.ForeColor = Color.DarkMagenta
            End If


        Next


        If IsNothing(TreeViewTP.SelectedNode) Then

            ButtonDuplicate.Enabled = False
            ButtonSave.Enabled = False
            ButtonExportGantt.Enabled = False
            ButtonDelProject.Enabled = False
            TextBoxFinish.Text = ""
            TextBoxNote.Text = ""
            TextBoxProject.Text = ""
            TextBoxStart.Text = ""
            TextBoxTask.Text = ""
            ComboBoxArea.Text = ""
            ComboBoxCompleated.Text = ""
            ComboBoxQuality.Text = ""
            ComboBoxResponsible.Text = ""
            ComboBoxStatus.Text = ""
            TextBoxTask.Visible = True

        Else

            ButtonDuplicate.Enabled = True
            ButtonSave.Enabled = True
            ButtonExportGantt.Enabled = True
            ButtonDelProject.Enabled = True

        End If

        UpdatigTree = False
    End Sub

    Function ProjectStatus(ByVal id As String, ByVal refresh As Boolean) As String
        Static Dim tbltp As DataTable
        Static Dim Dstp As New DataSet
        Dim timingTStr As String = ""
        Dim rowShow As DataRow(), i As Integer
        ProjectStatus = "MISSING"
        Try
            i = tbltp.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp, "TimeProject")
            tbltp = Dstp.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp.Clear()
            tbltp.Clear()
            AdapterTP.Fill(Dstp, "TimeProject")
            tbltp = Dstp.Tables("TimeProject")
        End If

        rowShow = tbltp.Select("Project = '" & id & "'")

        For Each row In rowShow

            timingTStr = TimingTS(row("start").ToString, row("end").ToString, row("compleated").ToString, row("status").ToString)
            Application.DoEvents()
            If timingTStr = "ONTIME" And (ProjectStatus = "ONTIME" Or ProjectStatus = "CLOSED" Or ProjectStatus = "MISSING") Then
                ProjectStatus = "ONTIME"
            ElseIf timingTStr = "DELAY" Or ProjectStatus = "CRITIC" Then
                If (ProjectStatus <> "DELAY") And DelayAdvanceTimeTask(row("taskname").ToString, row("project").ToString, True) > -yelloDelay Then
                    ProjectStatus = "CRITIC"
                Else
                    ProjectStatus = "DELAY"
                    Exit For
                End If
            ElseIf timingTStr = "STANDBY" Then
                ProjectStatus = "STANDBY"
                Exit For
            ElseIf timingTStr = "CLOSED" And (ProjectStatus = "CLOSED" Or ProjectStatus = "MISSING") Then
                ProjectStatus = "CLOSED"
            Else
                ProjectStatus = ""
            End If
        Next

    End Function

    Function TimingTS(ByVal Taskstart As String, ByVal Taskend As String, ByVal compleated As String, ByVal TaskStatus As String) As String
        Dim TotalTimeTask As Integer
        Dim EquivalentTime As Date
        TotalTimeTask = 0
        If Len(Taskstart) = 10 And Len(Taskend) = 10 Then
            TimingTS = "ONTIME"
            TotalTimeTask = DateDiff("d", string_to_date(Taskstart), string_to_date(Taskend))
            EquivalentTime = DateAdd("d", Int(Val(compleated) * TotalTimeTask / 100), string_to_date(Taskstart))
            If DateDiff("d", Today, EquivalentTime) < 0 Then
                TimingTS = "DELAY"

            End If

        Else
            TimingTS = ""
        End If
        If compleated = "100" Then TimingTS = "ONTIME"
        If TaskStatus = "STANDBY" Then TimingTS = "STANDBY"
    End Function

    Sub FillCustomerCombo()
        Dim rowResults As DataRow(), customer As String = ""

        ComboBoxCustomerFilter.Items.Clear()
        ComboBoxCustomerFilter.Items.Add("")

        rowResults = tblTP.Select("project like '*'", "customer")
        For Each row In rowResults
            If customer <> row("customer").ToString Then
                ComboBoxCustomerFilter.Items.Add(UCase(row("customer").ToString))
                If ComboBoxCustomer.Items.Contains(UCase(row("customer").ToString)) = False Then ComboBoxCustomer.Items.Add(UCase(row("customer").ToString))
            End If

            customer = row("customer").ToString
        Next
        ComboBoxCustomerFilter.Sorted = True
    End Sub

    Sub FillAreaCombo()
        Dim rowResults As DataRow(), Area As String = ""

        ComboBoxAreaFilter.Items.Clear()
        ComboBoxAreaFilter.Items.Add("")

        rowResults = tblTP.Select("project like '*'", "area")
        For Each row In rowResults
            If Area <> row("Area").ToString Then
                ComboBoxAreaFilter.Items.Add(UCase(row("Area").ToString))

                If ComboBoxArea.Items.Contains(UCase(row("Area").ToString)) = False Then ComboBoxArea.Items.Add(UCase(row("Area").ToString))
            End If

            Area = row("Area").ToString
        Next
        ComboBoxAreaFilter.Sorted = True
    End Sub

    Sub FillTaskLeaderCombo()
        Dim rowResults As DataRow(), Area As String = ""

        ComboBoxtaskFilter.Items.Clear()
        ComboBoxtaskFilter.Items.Add("")

        rowResults = tblTP.Select("project like '*'", "taskleader")
        For Each row In rowResults
            If Area <> row("taskleader").ToString Then
                ComboBoxtaskFilter.Items.Add(UCase(row("taskleader").ToString))
                If ComboBoxResponsible.Items.Contains(UCase(row("taskleader").ToString)) = False Then ComboBoxResponsible.Items.Add(UCase(row("taskleader").ToString))
            End If

            Area = row("taskleader").ToString
        Next
        ComboBoxtaskFilter.Sorted = True
    End Sub

    Sub FillResponsibleCombo()
        Dim rowResults As DataRow(), ProjectLeader As String = ""

        ComboBoxResponsibleFilter.Items.Clear()
        ComboBoxResponsibleFilter.Items.Add("")

        rowResults = tblTP.Select("project like '*'", "projectleader")
        For Each row In rowResults
            If ProjectLeader <> row("projectleader").ToString Then
                ComboBoxResponsibleFilter.Items.Add(UCase(row("projectleader").ToString))
                If ComboBoxResponsible.Items.Contains(UCase(row("projectleader").ToString)) = False Then ComboBoxResponsible.Items.Add(UCase(row("projectleader").ToString))



            End If

            ProjectLeader = row("projectleader").ToString
        Next
        ComboBoxResponsibleFilter.Sorted = True
    End Sub

    Sub myNodeSelect(ByVal read As Boolean, Optional ByVal parent As Boolean = False)
        Try
            Static Dim selNode As TreeNode
            Static Dim selNodeParent As TreeNode
            If read Then
                selNode = TreeViewTP.SelectedNode
                If selNode.Level = 1 Then selNodeParent = TreeViewTP.SelectedNode.Parent

                TreeViewTP.Select()

            ElseIf Not IsNothing(selNode) Then
                TreeViewTP.Visible = False
                Dim idSelected As String = selNode.Text
                Dim id As String
                TreeViewTP.HideSelection = False
                For Each node In TreeViewTP.Nodes
                    For Each nn In node.Nodes
                        Try
                            id = nn.Text
                        Catch ex As Exception

                        End Try


                        If idSelected = id Then
                            If selNode.Level = 1 Then
                                If node.text = selNodeParent.Text Then
                                    selNode = nn
                                End If
                            Else
                                selNode = nn
                            End If
                        End If
                    Next
                Next

                TreeViewTP.SelectedNode = selNode
                TreeViewTP.Focus()
                TreeViewTP.Visible = True
            End If
        Catch ex As Exception

        End Try

    End Sub

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

    Private Sub DateTimePickerFinish_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerFinish.CloseUp
        If CheckBoxFixedEnd.Checked = False Then

            TextBoxFinish.Text = date_to_string(DateTimePickerFinish.Text)
        Else
            MsgBox("Need to unlock the F")
        End If

    End Sub

    Private Sub DateTimePickerStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerStart.CloseUp
        If CheckBoxFixedStart.Checked = False Then
            TextBoxStart.Text = date_to_string(DateTimePickerStart.Text)
        Else
            MsgBox("Need to unlock the F")
        End If

    End Sub

    Private Sub ButtonDelProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelProject.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        If controlRight("J") >= 2 And (controlRight(Mid(ComboBoxArea.Text, 1, 1)) > 2 Or ComboBoxArea.Text = "") Then
            If vbYes = MsgBox("Do you want delete this Item?", MsgBoxStyle.YesNo) Then
                If OpenSession = False And TreeViewTP.SelectedNode.Level = 1 Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`TimeProject` WHERE `TimeProject`.`Project` = '" & TreeViewTP.SelectedNode.Parent.Text & "' and `TimeProject`.`TaskName` = '" & TreeViewTP.SelectedNode.Text & "'"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Milestone deleted!")
                        UpdatigTree = True
                        TreeViewTP.SelectedNode.Remove()
                        UpdatigTree = False
                        Application.DoEvents()

                    Catch ex As Exception
                        MsgBox("Milestone deleting error " & ex.Message)
                    End Try
                Else
                    MsgBox("Can cancel a milestone and only in closed session! ")
                End If
            End If
        Else
            MsgBox("No enought right for this operation! ")
        End If
        UpdateTreeTPList(True)
    End Sub

    Private Sub Controls_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                ComboBoxArea.TextChanged, ComboBoxCustomer.TextChanged, ComboBoxQuality.TextChanged, ComboBoxResponsible.TextChanged,
                ComboBoxStatus.TextChanged, TextBoxFinish.TextChanged, TextBoxStart.TextChanged, TextBoxNote.TextChanged, ComboBoxCompleated.TextChanged,
                CheckBoxFixedEnd.CheckedChanged, CheckBoxFixedStart.CheckedChanged

        If UpdatigTree = False Then
            OpenSession = True
            ButtonSave.BackColor = Color.OrangeRed
        End If
    End Sub

    Private Sub TreeViewTP_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewTP.BeforeSelect
        If UpdatigTree = False And Not IsNothing(TreeViewTP.SelectedNode) Then

            If OpenSession = True Then
                If vbYes = MsgBox("Session Open do you want Save?", MsgBoxStyle.YesNo) Then
                    ButtonSave_Click(Me, e)
                Else
                    OpenSession = False
                    ButtonSave.BackColor = Color.Green
                End If
            End If
        End If
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click

        Dim cmd As New MySqlCommand(), nodeparent As String = ""
        Dim sql As String, datevalid As Boolean
        If OpenSession = True And Not IsNothing(TreeViewTP.SelectedNode) And (Trim(UCase(ReplaceChar(ComboBoxArea.Text))) = "" Or
           (user() = Mid(Trim(UCase(ReplaceChar(ComboBoxArea.Text))), 1, 1) And
           Mid(Trim(UCase(ReplaceChar(ComboBoxArea.Text))), 2, 1) = "-" And Len(Trim(UCase(ReplaceChar(ComboBoxArea.Text)))) >= 4)) Then
            myNodeSelect(True)
            If Len(TextBoxFinish.Text) = 10 And Len(TextBoxStart.Text) = 10 Then
                datevalid = DateDiff("d", string_to_date(TextBoxFinish.Text), string_to_date(TextBoxStart.Text)) < 0
            Else
                datevalid = True
            End If


            If datevalid Then
                Try
                    Try
                        nodeparent = " `TimeProject`.`project` = '" & TreeViewTP.SelectedNode.Parent.Text & "' "
                    Catch ex As Exception
                        nodeparent = " `TimeProject`.`project` like '*'"
                    End Try
                    sql = "UPDATE `" & DBName & "`.`TimeProject` SET " &
                            IIf(TreeViewTP.SelectedNode.Level = 0, "`area` = '" & Replace(Trim(UCase(ReplaceChar(ComboBoxArea.Text))), " ", "") & "',", "") &
                            IIf(TreeViewTP.SelectedNode.Level = 0, "`customer` = '" & Trim(UCase(ReplaceChar(ComboBoxCustomer.Text))), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 0, "',`ProjectLeader` = '" & Trim(UCase(ReplaceChar(ComboBoxResponsible.Text))), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "`taskLeader` = '" & Trim(UCase(ReplaceChar(ComboBoxResponsible.Text))), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`quality` = '" & ComboBoxQuality.Text, "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`Compleated` = '" & ComboBoxCompleated.Text, "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`start` = '" & TextBoxStart.Text, "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`end` = '" & TextBoxFinish.Text, "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`FixedEnd` = '" & IIf(CheckBoxFixedEnd.Checked, "YES", ""), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`FixedStart` = '" & IIf(CheckBoxFixedStart.Checked, "YES", ""), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`note` = '" & Trim(TextBoxNote.Text), "") &
                            IIf(TreeViewTP.SelectedNode.Level = 1, "',`status` = '" & UCase(ComboBoxStatus.Text) & "'", "' ") &
                            " WHERE " & IIf(TreeViewTP.SelectedNode.Level = 1, "`TimeProject`.`taskname` = '" & TreeViewTP.SelectedNode.Text & "' and " & nodeparent, "`TimeProject`.`project` = '" & TreeViewTP.SelectedNode.Text & "'")
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    myNodeSelect(True, True)
                    UpdateTreeTPList(True)
                    myNodeSelect(False, True)
                    TreeViewTP.HideSelection = False
                    TreeViewTP.Focus()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                ButtonSave.BackColor = Color.Green
                OpenSession = False
            Else
                MsgBox("The start date need before End date")
            End If
            myNodeSelect(False, True)
        Else
            MsgBox("Session not open or invalida area name!")
        End If


    End Sub

    Private Sub TreeViewTP_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewTP.AfterSelect

        Dim UpdatigTreePrev As Boolean = UpdatigTree

        If IsNothing(TreeViewTP.SelectedNode) Then

            ButtonDuplicate.Enabled = False
            ButtonSave.Enabled = False
            ButtonExportGantt.Enabled = False
            ButtonDelProject.Enabled = False
            TextBoxFinish.Text = ""
            TextBoxNote.Text = ""
            TextBoxProject.Text = ""
            TextBoxStart.Text = ""
            TextBoxTask.Text = ""
            ComboBoxArea.Text = ""
            ComboBoxCompleated.Text = ""
            ComboBoxQuality.Text = ""
            ComboBoxResponsible.Text = ""
            ComboBoxStatus.Text = ""


        Else


            ButtonSave.Enabled = True
            ButtonExportGantt.Enabled = True
            ButtonDelProject.Enabled = True

        End If
        UpdatigTree = True

        If TreeViewTP.SelectedNode.Level = 0 Then
            ButtonDuplicate.Enabled = True
            TextBoxProject.Text = TreeViewTP.SelectedNode.Text
            LabelTask.Visible = False
            DateTimePickerFinish.Enabled = False
            DateTimePickerStart.Enabled = False
            TextBoxFinish.Text = ProjectEnd(TreeViewTP.SelectedNode.Text, True)
            TextBoxStart.Text = ProjectStart(TreeViewTP.SelectedNode.Text, True)
            TextBoxFinish.Enabled = False
            TextBoxStart.Enabled = False
            ComboBoxStatus.Items.Clear()
            ComboBoxStatus.Items.Add("")
            ComboBoxStatus.Items.Add("OPEN")
            ComboBoxStatus.Items.Add("CLOSED")
            ComboBoxStatus.Items.Add("STANDBY")
            ComboBoxStatus.Items.Add(ProjectStatusOpenClosed(TreeViewTP.SelectedNode.Text, False))
            ComboBoxStatus.Enabled = False
            ComboBoxStatus.Text = ProjectStatusOpenClosed(TreeViewTP.SelectedNode.Text, False)
            ComboBoxCompleated.Enabled = False
            ComboBoxCompleated.Text = ProjectCompleated(TreeViewTP.SelectedNode.Text, False)
            ComboBoxCustomer.Text = customer(TreeViewTP.SelectedNode.Text, False)
            LabelLeader.Text = "Project Leader"
            ComboBoxCustomer.Enabled = True
            ComboBoxArea.Enabled = True
            ComboBoxResponsible.Text = ProjectLeader(TreeViewTP.SelectedNode.Text, False)
            ComboBoxArea.Text = area(TreeViewTP.SelectedNode.Text, False)
            TextBoxNote.Visible = False
            ComboBoxQuality.Text = quality(TreeViewTP.SelectedNode.Text, False)
            ComboBoxQuality.Enabled = False
            LabelNote.Visible = False
            CheckBoxFixedEnd.Visible = False
            CheckBoxFixedStart.Visible = False

            If controlRight("J") >= 3 And controlRight(Mid(ComboBoxArea.Text, 1, 1)) > 2 Or ComboBoxArea.Text = "" Then
                ComboBoxCustomer.Enabled = True
                ComboBoxArea.Enabled = True
                ButtonSave.Enabled = True
            Else
                ComboBoxCustomer.Enabled = False
                ComboBoxArea.Enabled = False
                ButtonSave.Enabled = False
            End If

            Try
                LabelDurance.Text = "Total Time (d): " & DateDiff("d", string_to_date(TextBoxStart.Text), string_to_date(TextBoxFinish.Text))
            Catch ex As Exception

            End Try

            Try
                LabelDelayAdvance.Text = "Delay/Advance (d): " & DelayAdvanceTime(TreeViewTP.SelectedNode.Text, False)
            Catch ex As Exception

            End Try

        Else
            TextBoxProject.Text = TreeViewTP.SelectedNode.Parent.Text
            TextBoxTask.Visible = True
            TextBoxTask.Text = taskname(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            LabelTask.Visible = True

            TextBoxFinish.Enabled = False
            TextBoxStart.Enabled = False
            TextBoxStart.Text = TaskStart(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            TextBoxFinish.Text = TaskEnd(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxStatus.Items.Clear()
            ComboBoxStatus.Items.Add("")
            ComboBoxStatus.Items.Add("OPEN")
            ComboBoxStatus.Items.Add("CLOSED")
            ComboBoxStatus.Items.Add("STANDBY")
            ComboBoxStatus.Items.Add(TaskStatus(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False))
            ComboBoxStatus.Text = TaskStatus(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxCompleated.Text = TaskCompleated(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxCustomer.Text = customer(TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxCustomer.Enabled = False
            LabelLeader.Text = "Milestome Responsible"
            ComboBoxResponsible.Text = TaskLeader(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxArea.Text = area(TreeViewTP.SelectedNode.Parent.Text, False)
            ComboBoxArea.Enabled = False
            ComboBoxQuality.Text = TaskQuality(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            TextBoxNote.Text = note(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            TextBoxNote.Visible = True
            ButtonDuplicate.Enabled = False
            CheckBoxFixedEnd.Checked = IIf(TaskFixedEnd(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False) = "YES", True, False)
            CheckBoxFixedStart.Checked = IIf(TaskFixedStart(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False) = "YES", True, False)
            CheckBoxFixedEnd.Visible = True
            CheckBoxFixedStart.Visible = True


            If controlRight("J") >= 2 And controlRight(Mid(ComboBoxArea.Text, 1, 1)) > 2 Or ComboBoxArea.Text = "" Then

                DateTimePickerFinish.Enabled = True
                DateTimePickerStart.Enabled = True

                ComboBoxStatus.Enabled = True
                ComboBoxCompleated.Enabled = True
                ComboBoxQuality.Enabled = True
                ButtonSave.Enabled = True
                ComboBoxResponsible.Enabled = True
            Else

                DateTimePickerFinish.Enabled = False
                DateTimePickerStart.Enabled = False

                ComboBoxStatus.Enabled = False
                ComboBoxCompleated.Enabled = False
                ComboBoxQuality.Enabled = False
                ButtonSave.Enabled = False
                ComboBoxResponsible.Enabled = False
            End If
            If controlRight("J") >= 3 And controlRight(Mid(ComboBoxArea.Text, 1, 1)) > 2 Or ComboBoxArea.Text = "" Then
                CheckBoxFixedEnd.Enabled = True
                CheckBoxFixedStart.Enabled = True

            Else
                CheckBoxFixedEnd.Enabled = False
                CheckBoxFixedStart.Enabled = False

            End If
            Try
                LabelDurance.Text = "Total Time (d): " & DateDiff("d", string_to_date(TextBoxStart.Text), string_to_date(TextBoxFinish.Text))
            Catch ex As Exception

            End Try

            Try
                LabelDelayAdvance.Text = "Delay/Advance (d): " & DelayAdvanceTimeTask(TreeViewTP.SelectedNode.Text, TreeViewTP.SelectedNode.Parent.Text, False)
            Catch ex As Exception

            End Try

        End If


        UpdatigTree = UpdatigTreePrev

        If controlRight(Mid(ComboBoxArea.Text, 1, 1)) >= 2 Then ButtonDuplicate.Visible = True

    End Sub

    Function ProjectCompleated(ByVal id As String, ByVal refresh As Boolean) As Integer

        Dim rowShow As DataRow(), i As Integer, TotalTime As Integer
        ProjectCompleated = 0
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        TotalTime = 0
        rowShow = tbltp_static.Select("Project = '" & id & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                TotalTime = TotalTime + DateDiff("d", string_to_date(row("start").ToString), string_to_date(row("end").ToString))
                ProjectCompleated = ProjectCompleated + Val(row("Compleated").ToString) * DateDiff("d", string_to_date(row("start").ToString), string_to_date(row("end").ToString))
            Next

            If TotalTime > 0 Then ProjectCompleated = ProjectCompleated / TotalTime
        Else
            ProjectCompleated = 100
        End If

    End Function

    Function ProjectStart(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        ProjectStart = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If


        rowShow = tbltp_static.Select("Project = '" & id & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                If ProjectStart = "" Or DateDiff("d", string_to_date(ProjectStart), string_to_date(row("start").ToString)) < 0 Then
                    ProjectStart = row("start").ToString
                End If
            Next
        End If

    End Function

    Function ProjectEnd(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        ProjectEnd = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If


        rowShow = tbltp_static.Select("Project = '" & id & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                If ProjectEnd = "" Or DateDiff("d", string_to_date(ProjectEnd), string_to_date(row("end").ToString)) > 0 Then
                    ProjectEnd = row("end").ToString
                End If
            Next
        End If

    End Function

    Function quality(ByVal id As String, ByVal refresh As Boolean) As Integer

        Dim rowShow As DataRow(), i As Integer
        quality = 0
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        quality = 0
        rowShow = tbltp_static.Select("Project = '" & id & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                quality = quality + Val(row("quality").ToString)
            Next

            quality = quality / rowShow.Length
        Else
            quality = 100
        End If

    End Function

    Function customer(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        customer = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("Project = '" & id & "'")

        For Each row In rowShow
            If row("customer").ToString <> "" Then customer = row("customer").ToString
        Next

    End Function

    Function TaskCompleated(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskCompleated = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskCompleated = row("Compleated").ToString
        Next

    End Function

    Function TaskFixedStart(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskFixedStart = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskFixedStart = row("FixedStart").ToString
        Next

    End Function

    Function TaskFixedEnd(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskFixedEnd = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskFixedEnd = row("FixedEnd").ToString
        Next

    End Function

    Function TaskStatus(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskStatus = "MISSING"
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                TaskStatus = row("status").ToString
            Next
        End If
    End Function

    Function ProjectLeader(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        ProjectLeader = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("Project = '" & id & "'")

        For Each row In rowShow
            ProjectLeader = row("ProjectLeader").ToString
        Next

    End Function

    Function TaskEnd(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskEnd = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskEnd = row("End").ToString
        Next

    End Function

    Function TaskQuality(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskQuality = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskQuality = row("quality").ToString
        Next

    End Function

    Function TaskStart(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        TaskStart = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If


        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")
        For Each row In rowShow
            TaskStart = row("Start").ToString
        Next

    End Function

    Function TaskLeader(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String


        Dim rowShow As DataRow(), i As Integer
        TaskLeader = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            TaskLeader = row("TaskLeader").ToString
        Next

    End Function

    Function area(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        area = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("Project = '" & id & "'")

        For Each row In rowShow
            If row("area").ToString <> "" Then area = row("area").ToString
        Next

    End Function

    Function taskname(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        taskname = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            taskname = row("taskname").ToString
        Next

    End Function

    Function note(ByVal id As String, ByVal idp As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        note = ""
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("taskname = '" & id & "' and project = '" & idp & "'")

        For Each row In rowShow
            note = row("note").ToString
        Next

    End Function

    Function ProjectStatusOpenClosed(ByVal id As String, ByVal refresh As Boolean) As String

        Dim rowShow As DataRow(), i As Integer
        ProjectStatusOpenClosed = "CLOSED"
        Try
            i = tbltp_static.Rows.Count
        Catch ex As Exception
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tbltp_static = Dstp_static.Tables("TimeProject")
        End Try

        If refresh Then
            Dstp_static.Clear()
            tbltp_static.Clear()
            AdapterTP.Fill(Dstp_static, "TimeProject")
            tblTP = Dstp_static.Tables("TimeProject")
        End If

        rowShow = tbltp_static.Select("Project = '" & id & "'")
        If rowShow.Length > 0 Then
            For Each row In rowShow
                If "OPEN" = row("status").ToString And ProjectStatusOpenClosed = "CLOSED" Then ProjectStatusOpenClosed = "OPEN"
                If "STANDBY" = row("status").ToString And ProjectStatusOpenClosed = "CLOSED" Then ProjectStatusOpenClosed = "STANDBY"
            Next

        End If

    End Function

    Private Sub ButtonAddTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDuplicate.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String, newName As String
        DsTP.Clear()
        tblTP.Clear()

        AdapterTP.Fill(DsTP, "timeproject")
        tblTP = DsTP.Tables("timeproject")
        myNodeSelect(True, True)
        Dim rowShow As DataRow()
        If TreeViewTP.SelectedNode.Text <> "" Then
            newName = ReplaceChar(InputBox("Insert new Project name: "))
            If newName <> "" And ProjectStatus(newName, True) = "MISSING" Then

                rowShow = tblTP.Select("Project = '" & TreeViewTP.SelectedNode.Text & "'")

                For Each row In rowShow
                    Try
                        sql = "INSERT INTO `" & DBName & "`.`timeproject` (`Project`,`TaskName` ) VALUES ( '" & newName & "' , '" & row("Taskname").ToString & "'" & ");"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Next
                UpdateTreeTPList(True)

            Else
                MsgBox("Need fill the Project Name and Milestone description!")
            End If
        Else
            MsgBox("The project already exist!")
        End If

        myNodeSelect(False, True)
    End Sub

    Private Sub ButtonAddProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddProject.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String, needAdd As Boolean
        myNodeSelect(True, True)
        If controlRight("J") >= 2 And (controlRight(Mid(ComboBoxArea.Text, 1, 1)) > 2 Or ComboBoxArea.Text = "") Or ProjectStatus(TextBoxProject.Text, True) = "MISSING" Then
            If TextBoxProject.Text <> "" And TextBoxTask.Text <> "" Then
                If ProjectStatus(TextBoxProject.Text, True) = "MISSING" Then
                    needAdd = (vbYes = MsgBox("Project name different, do you want add a new Project?", vbYesNo))
                ElseIf ProjectStatus(TextBoxProject.Text, True) <> "MISSING" And TaskStatus(TextBoxTask.Text, TextBoxProject.Text, True) <> "MISSING" Then
                    MsgBox("No possible insert two task whit same name in the same project!")
                Else
                    needAdd = True
                End If
                If needAdd Then

                    Try
                        sql = "INSERT INTO `" & DBName & "`.`timeproject` (`Project`,`TaskName` ) VALUES ( '" & TextBoxProject.Text & "' , '" & TextBoxTask.Text & "'" & ");"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    UpdateTreeTPList(True)

                Else

                End If
            Else
                MsgBox("Need fill the Project Name and Milestone description!")
            End If

            myNodeSelect(False, True)
        Else
            MsgBox("No enought right for this operation!")
        End If
    End Sub

    Private Sub ButtonRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRefresh.Click
        myNodeSelect(True, True)
        UpdateTreeTPList(True)
        myNodeSelect(False, True)
        TreeViewTP.Focus()
    End Sub

    Private Sub ButtonExportGantt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExportGantt.Click
        Dim Project As String, i As Integer
        Dim PresenceTmp As Integer
        Dim rowShow As DataRow(), rowExcel As Integer, sql As String


        DsTP.Clear()
        tblTP.Clear()
        AdapterTP.Fill(DsTP, "TimeProject")
        tblTP = DsTP.Tables("TimeProject")

        sql = IIf(ComboBoxAreaFilter.Text = "", "area like '*' and ", "area = '" & ComboBoxAreaFilter.Text & "' and ") &
                               IIf(ComboBoxStatusFilter.Text = "", "status like '*'  and ", "status = '" & ComboBoxCustomerFilter.Text & "'  and ") &
                               IIf(ComboBoxCustomerFilter.Text = "", "customer like '*'  and ", "customer = '" & ComboBoxCustomerFilter.Text & "'  and ") &
                               IIf(ComboBoxCompleatedFilter.Text = "", "compleated like '*'  and ", "compleated = '" & ComboBoxCompleatedFilter.Text & "'  and ") &
                               IIf(CheckBoxTemplate.Checked = False, "not project like '*template*'  and ", "") &
                               IIf(ComboBoxtaskFilter.Text = "", "", " taskleader = '" & ComboBoxtaskFilter.Text & "'  and ") &
                               IIf(ComboBoxResponsibleFilter.Text = "", "ProjectLeader like '*'  ", "projectleader = '" & ComboBoxResponsibleFilter.Text & "'  ")
        rowShow = tblTP.Select(sql, "project, id")

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        CollectProcess()
        'open the offer template
        Dim excelApp As New Object
        excelApp = CreateObject("Excel.Application")

        Dim excelWorkbook As Object
        Dim excelSheet As Object
        'Try
        excelWorkbook = excelApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathPicture") & ParameterTable("plant") & "R_GEN_GTT_Template_0.xlsx")
        excelWorkbook.Activate()
        excelSheet = excelWorkbook.Worksheets("GANTT")
        excelSheet.Activate()
        Dim cc As New ColorConverter
        excelApp.Visible = True
        rowExcel = 1
        Project = ""
        For Each row In rowShow

            If row("project").ToString <> Project Then
                rowExcel = rowExcel + 1
                Project = row("project").ToString
                excelApp.Cells(rowExcel, 1) = Project
                excelApp.Cells(rowExcel, 1).Font.Bold = True
                excelApp.Rows(rowExcel & ":" & rowExcel).Font.Bold = True
                excelApp.Cells(rowExcel, 2) = row("projectleader").ToString
                excelApp.Cells(rowExcel, 3) = ProjectStatus(row("project").ToString, True)
                excelApp.Cells(rowExcel, 4) = customer(row("project").ToString, True)
                excelApp.Cells(rowExcel, 5) = ProjectCompleated(row("project").ToString, True)
                excelApp.Cells(rowExcel, 6) = ProjectStart(row("project").ToString, True)
                excelApp.Cells(rowExcel, 7) = ProjectEnd(row("project").ToString, True)
                excelApp.Cells(rowExcel, 9) = quality(row("project").ToString, True)
                i = 10
                While excelApp.Cells(1, i).text <> ""
                    PresenceTmp = PresenceWeek(excelApp.Cells(1, i).text, excelApp.Cells(rowExcel, 6).text, excelApp.Cells(rowExcel, 7).text)
                    If PresenceTmp > 0 Then

                        If excelApp.Cells(rowExcel, 3).text = "DELAY" Then
                            excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Red)
                            excelApp.Cells(rowExcel, i) = PresenceTmp
                        End If


                        If excelApp.Cells(rowExcel, 3).text = "CRITIC" Then
                            excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Orange)
                            excelApp.Cells(rowExcel, i) = PresenceTmp
                        End If

                        If excelApp.Cells(rowExcel, 3).text = "ONTIME" Then
                            excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.LightGreen)
                            excelApp.Cells(rowExcel, i) = PresenceTmp
                        End If

                        If excelApp.Cells(rowExcel, 3).text = "STANDBY" Then
                            excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Blue)
                            excelApp.Cells(rowExcel, i) = PresenceTmp
                        End If

                        If excelApp.Cells(rowExcel, 3).text = "CLOSED" Then
                            excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Gray)
                            excelApp.Cells(rowExcel, i) = PresenceTmp
                        End If

                    End If
                    i = i + 1
                End While
                rowExcel = rowExcel + 1
            End If

            ' TASK
            excelApp.Cells(rowExcel, 1) = "    " & row("taskname").ToString
            excelApp.Cells(rowExcel, 2) = row("taskleader").ToString
            excelApp.Cells(rowExcel, 3) = TimingTS(row("start").ToString, row("end").ToString, row("compleated").ToString, row("status").ToString)
            excelApp.Cells(rowExcel, 4) = ""
            excelApp.Cells(rowExcel, 5) = TaskCompleated(row("taskname").ToString, Project, True)
            excelApp.Cells(rowExcel, 6) = TaskStart(row("taskname").ToString, Project, True)
            excelApp.Cells(rowExcel, 7) = TaskEnd(row("taskname").ToString, Project, True)
            excelApp.Cells(rowExcel, 9) = TaskQuality(row("taskname").ToString, Project, True)
            i = 10
            While excelApp.Cells(1, i).text <> ""
                PresenceTmp = PresenceWeek(excelApp.Cells(1, i).text, excelApp.Cells(rowExcel, 6).text, excelApp.Cells(rowExcel, 7).text)
                If PresenceTmp > 0 Then
                    If excelApp.Cells(rowExcel, 3).text = "DELAY" Then
                        excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Red)
                        excelApp.Cells(rowExcel, i) = PresenceTmp
                    End If

                    If excelApp.Cells(rowExcel, 3).text = "ONTIME" Then
                        excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.LightGreen)
                        excelApp.Cells(rowExcel, i) = PresenceTmp
                    End If

                    If excelApp.Cells(rowExcel, 3).text = "CRITIC" Then
                        excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Orange)
                        excelApp.Cells(rowExcel, i) = PresenceTmp
                    End If

                    If excelApp.Cells(rowExcel, 3).text = "STANDBY" Then
                        excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Blue)
                        excelApp.Cells(rowExcel, i) = PresenceTmp
                    End If

                    If excelApp.Cells(rowExcel, 3).text = "CLOSED" Then
                        excelApp.Cells(rowExcel, i).interior.color = ColorTranslator.ToOle(Color.Gray)
                        excelApp.Cells(rowExcel, i) = PresenceTmp
                    End If
                End If
                i = i + 1
            End While
            rowExcel = rowExcel + 1
        Next


        Try
            SaveFileDialog1.FileName = ParameterTable("plant") & "R_GEN_GTT_" & Replace(Today, "/", "_") & "_0.xlsx"
            SaveFileDialog1.ShowDialog()
            excelWorkbook.SaveAs(SaveFileDialog1.FileName)
            excelWorkbook.Close(True)
            excelApp.Quit()
            Dim generation As Integer = GC.GetGeneration(excelApp)
            GC.Collect(generation)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        Me.Focus()
        Me.Show()
        KillLastExcel()
        Application.DoEvents()
    End Sub

    Function PresenceWeek(ByVal timestr As String, ByVal start As String, ByVal finish As String) As Integer
        Dim myWeek As Integer = Int(Mid(timestr, 7))
        Dim myYear As Integer = Int(Mid(timestr, 1, 4))
        Dim MydateMond As Date

        MydateMond = DateAdd("d", 7 * (myWeek - 1) + 1 - Weekday(string_to_date(myYear & "/01/01"), FirstDayOfWeek.Monday), string_to_date(myYear & "/01/01"))
        If DateDiff("d", string_to_date(start), MydateMond) >= -4 And DateDiff("d", string_to_date(start), MydateMond) <= 0 Then
            PresenceWeek = 5 + DateDiff("d", string_to_date(start), MydateMond)
        ElseIf DateDiff("d", string_to_date(finish), MydateMond) >= -4 And DateDiff("d", string_to_date(finish), MydateMond) <= 0 Then
            PresenceWeek = -DateDiff("d", string_to_date(finish), MydateMond) + 1
        ElseIf DateDiff("d", string_to_date(start), MydateMond) > 0 And DateDiff("d", string_to_date(finish), MydateMond) < 0 Then
            PresenceWeek = 5
        Else
            PresenceWeek = 0
        End If


    End Function

    Private Sub ButtonHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHelp.Click
        FormHelpHime.Show()
        FormHelpHime.Focus()
        FormHelpHime.Text = "Quality problem severity" & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Sub CollectProcess()
        MemProcess = ""
        For Each prog As Process In Process.GetProcesses
            MemProcess = MemProcess & ";" & prog.Id

        Next
    End Sub

    Sub KillLastExcel()
        MemProcess = ""
        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "EXCEL" And InStr(MemProcess, prog.Id) <= 0 Then
                prog.Kill()
            End If
        Next
    End Sub

    Private Sub ComboBoxQuality_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxQuality.TextChanged
        If Not IsNumeric(ComboBoxQuality.Text) Or Val(ComboBoxQuality.Text) > 100 Then ComboBoxQuality.Text = ""
    End Sub

    Private Sub ComboBoxCompleated_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCompleated.TextChanged
        If Not IsNumeric(ComboBoxQuality.Text) Or Val(ComboBoxQuality.Text) > 100 Then ComboBoxQuality.Text = ""
    End Sub


End Class