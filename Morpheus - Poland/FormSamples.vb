Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Linq
Imports System.Configuration

Public Class FormSamples
    Dim DsDocComp As New DataSet
    Dim tblDocComp As New DataTable
    Dim index As Long = 1
    Dim tblProd As DataTable
    Dim DsProd As New DataSet
    Dim currentActivityID As Integer = -1
    Dim currentProductCode As String
    Dim XmlTree As New TreeViewToFromXml
    Dim OpenSession As Boolean
    Dim tblBomOff As DataTable
    Dim DsBomOff As New DataSet
    Dim tblSigip As DataTable
    Dim DsSigip As New DataSet
    Dim tblOff As DataTable
    Dim DsOff As New DataSet
    Dim tblPfp As DataTable
    Dim DsPfp As New DataSet
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet
    Dim tblCredentials As DataTable
    Dim DsCredentials As New DataSet
    Dim tblNPI As New DataTable
    Dim DsNPI As New DataSet
    Dim tblTP As DataTable
    Dim DsTP As New DataSet
    Dim ConnectionStringOrcad As String
    Dim AdapterSql As SqlDataAdapter
    Dim TblSql As New DataTable
    Dim DsSql As New DataSet
    Dim RdaInfo As String, OrderInfo As String
    Dim DateStart As New Date
    Dim DateClosed As New Date
    Dim cSelectedID As String
    Dim firstLoad As Boolean = True

    Private Sub FormSamples_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))

            If InStr(TabControlNPI.SelectedTab.Text, "Task") > 0 Then

                If currentActivityID > 0 Then
                    If OpenSession Then
                        If vbYes = MsgBox("Session open! Do you want to save?", MsgBoxStyle.YesNo) Then
                            ButtonSave_Click(Me, e)
                        Else
                            Dim tblProd As DataTable
                            Dim DsProd As New DataSet
                            Dim rowShow As DataRow()

                            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                                AdapterProd.Fill(DsProd, "Product")
                            End Using
                            tblProd = DsProd.Tables("Product")
                            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")

                            For Each row In rowShow
                                session("product", row("Id").ToString, False)
                            Next
                            OpenSession = False
                            TimerTask.Stop()
                            ButtonSave.BackColor = Color.Green
                            TextBoxBomTime.Text = ""
                        End If
                    Else
                        TimerTask.Stop()
                        Dim tblProd As DataTable
                        Dim DsProd As New DataSet
                        Dim rowShow As DataRow()
                        If currentActivityID > 0 Then
                            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                                AdapterProd.Fill(DsProd, "Product")
                            End Using
                            tblProd = DsProd.Tables("Product")
                            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
                            For Each row In rowShow
                                session("Product", row("Id").ToString, False)
                            Next
                        End If

                    End If
                End If
            End If

            Using AdapterOff As New MySqlDataAdapter("SELECT * FROM offer", con)
                AdapterOff.Fill(DsBomOff, "Offer")
            End Using
            tblOff = DsOff.Tables("Offer")

            Using AdapterSigip As New MySqlDataAdapter("SELECT * FROM sigip", con)
                AdapterSigip.Fill(DsSigip, "sigip")
            End Using
            tblSigip = DsSigip.Tables("sigip")
        End Using
        TreeViewTask.HideSelection = False
        TreeViewActivity.HideSelection = False
        If IsNeedUpdate(cSelectedID) Then
            Dim msgBoxResult As MsgBoxResult
            msgBoxResult = MsgBox("Do you want to save the changes?", vbYesNo)
            If msgBoxResult = MsgBoxResult.Yes Then
                SaveUpdates(cSelectedID)
            End If
        End If
        FormNPIDocMamagement.Close()

    End Sub

    Private Sub FormSamples_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))

            Using AdapterCredentials As New MySqlDataAdapter("SELECT * FROM credentials", con)
                AdapterCredentials.Fill(DsCredentials, "Credentials")
            End Using
            tblCredentials = DsCredentials.Tables("credentials")

            Try
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
                tblProd = DsProd.Tables("Product")
                ComboBoxActivityStatus.Items.Add("")
                ComboBoxActivityStatus.Items.Add("SENT")
                ComboBoxActivityStatus.Items.Add("STANDBY")
                ComboBoxActivityStatus.Items.Add("OPEN")
                ComboBoxActivityStatus.Items.Add("CLOSED")
                ComboBoxActivityStatus.Text = ""
                UpdateTreeSample()
                UpdateActivityID()
                TextBoxUser.Text = CreAccount.strUserName
                FillTaskStatus()
                FillTaskType()

                Cob_StatusFill()
                Cob_FilterStatusFill()
                FillCobOwnerContent()
                FillCobFilterContent()
                CobFilterBSFill()
                CobFilterBitronPNFill()

                If controlRight("R") >= 2 And controlRight("J") >= 2 Then
                    ButtonSaveDefault.Enabled = True
                Else
                    ButtonSaveDefault.Enabled = False
                End If

                If controlRight("R") >= 2 Then

                    ' NPI Activity
                    ButtonUpdateMagBox.Enabled = True
                    Button1.Enabled = True
                    Buttonrefresh.Enabled = True

                    ' NPI Task List
                    ButtonNew.Enabled = True
                    ButtonDelete.Enabled = True
                    ButtonSave.Enabled = True
                    ButtonReset.Enabled = True
                    ButtonUpdate.Enabled = True
                Else

                    ' NPI Activity
                    ButtonUpdateMagBox.Enabled = False
                    Button1.Enabled = False
                    Buttonrefresh.Enabled = True

                    ' NPI Task List
                    ButtonNew.Enabled = False
                    ButtonDelete.Enabled = False
                    ButtonSave.Enabled = False
                    ButtonReset.Enabled = False
                    ButtonUpdate.Enabled = False

                End If

                If controlRight("W") >= 2 Then                    ' NPI OpenIssue

                    Btn_Add.Enabled = True
                    Btn_Del.Enabled = True
                    Btn_Save.Enabled = True
                    Btn_UpLoadFile.Enabled = True

                Else

                    Btn_Add.Enabled = False
                    Btn_Del.Enabled = False
                    Btn_Save.Enabled = False
                    Btn_UpLoadFile.Enabled = False

                End If
                Using AdapterPfp As New MySqlDataAdapter("SELECT * FROM Pfp", con)
                    AdapterPfp.Fill(DsPfp, "pfp")
                End Using
                tblPfp = DsPfp.Tables("pfp")

                Try
                    DsDoc.Clear()
                    tblDoc.Clear()
                Catch ex As Exception

                End Try
                Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC;", con)
                    AdapterDoc.Fill(DsDoc, "doc")
                End Using

                tblDoc = DsDoc.Tables("doc")

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Using
        Me.DTP_Date.CustomFormat = "yyyy-MM-dd"
        DTP_Date.Format = DateTimePickerFormat.Custom
        Me.DTP_Date.Value = DateTime.Now

        Me.DTP_PlanCloseDate.CustomFormat = "yyyy-MM-dd"
        DTP_PlanCloseDate.Format = DateTimePickerFormat.Custom
        Me.DTP_PlanCloseDate.Value = DateTime.Now
        Try

            Call issuefunction(0)
            DGV_NPI.Sort(DGV_NPI.Columns("PlanedClosedDate"), System.ComponentModel.ListSortDirection.Ascending)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        firstLoad = False
    End Sub

    ' update the tree viewer
    Sub UpdateTreeSample()
        TreeViewActivity.BeginUpdate()
        TreeViewActivity.Font = New Font("Courier New", 12, FontStyle.Regular)
        TreeViewActivity.Nodes.Clear()
        TreeViewActivity.BackColor = Color.White
        Dim rootNode As TreeNode, activity As Integer
        Dim rootChildren1 As TreeNode
        DsProd.Clear()
        tblProd.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        tblProd = DsProd.Tables("Product")
        If Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " ")) = "" Then
            activity = 0
        ElseIf Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))) = "0" Then
            activity = 0
        Else
            activity = Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))))
        End If
        Dim rowShow As DataRow() = tblProd.Select(IIf(CheckBoxOpenProduct.Checked, "((status='SOP_SAMPLE') or (statusActivity='OPEN')) ", "status LIKE '*'") &
                                                  IIf(ComboBoxActivityID.Text <> "", " AND idactivity = " & activity, "") &
                                                  IIf(ComboBoxActivityStatus.Text <> "", " AND (statusActivity='" & ComboBoxActivityStatus.Text & "') ", IIf(CheckBoxClosed.Checked, " AND statusActivity LIKE 'CLOSED'", " AND statusActivity LIKE '*'")),
                                                  IIf(CheckBoxOrderByDate.Checked, " etd desc, customer, idActivity ", IIf(CheckBoxCustomer.Checked = True, "customer, idActivity ,etd", "idActivity,customer  ,etd")))
        Dim customer As String = ""
        activity = -1
        For Each row In rowShow
            If CheckBoxOrderByDate.Checked = True Then
                TreeViewActivity.Font = New Font("Courier New", 10, FontStyle.Bold)
                rootNode = New TreeNode(row("etd").ToString & Mid("__________", 1, 10 - Len(row("etd").ToString)) & " -- " & row("idactivity").ToString _
                & " - " & row("statusactivity").ToString & Mid("_______", 1, 7 - Len(row("statusactivity").ToString)) & " -- " & row("npieces").ToString &
                Mid("___________", 1, 6 - Len(Str(row("npieces").ToString))) & " pcs -- [" & row("bitronpn").ToString & "]  " & row("name").ToString)

                If row("statusactivity") = "OPEN" And row("etd").ToString <> "" Then
                    If DateDiff("d", Now, string_to_date(row("etd").ToString)) > 7 Then rootNode.ForeColor = Color.DarkGreen
                    If DateDiff("d", Today, Today) < 0 Then rootNode.ForeColor = Color.Red
                    If DateDiff("d", Now, string_to_date(row("etd").ToString).ToString) < 7 And DateDiff("d", Now, string_to_date(row("etd").ToString)) >= 0 Then rootNode.ForeColor = Color.Orange
                    If Val(row("npieces").ToString) = 0 Then rootNode.ForeColor = Color.LimeGreen
                ElseIf row("statusactivity").ToString = "CLOSED" Then
                    rootNode.BackColor = Color.Gray
                ElseIf row("statusactivity").ToString = "STANDBY" Then
                    rootNode.BackColor = Color.LightBlue
                ElseIf row("statusactivity").ToString = "SENT" Then
                    rootNode.BackColor = Color.LimeGreen
                ElseIf row("statusactivity").ToString = "" Then
                    rootNode.BackColor = Color.LightGray
                End If

                TreeViewActivity.Nodes.Add(rootNode)
            Else
                TreeViewActivity.Font = New Font("Courier New", 12, FontStyle.Bold)
                If customer <> row("customer").ToString Then
                    rootNode = New TreeNode("-- " & row("customer").ToString)

                    rootNode.NodeFont = New Font("Courier New", 12, FontStyle.Bold)
                    If CheckBoxCustomer.Checked Then TreeViewActivity.Nodes.Add(rootNode)
                    customer = row("customer").ToString
                    activity = -1
                End If
                If activity <> Val(row("idactivity").ToString) Then
                    rootChildren1 = New TreeNode("<> " & row("idactivity").ToString & " -- " & row("statusactivity").ToString & Mid("_______", 1, 7 - Len(row("statusactivity").ToString)) & " -- " & IIf(row("idactivity").ToString <> 0, row("NameActivity").ToString, "NOT ASSIGNED"))
                    rootChildren1.NodeFont = New Font("Courier New", 12, FontStyle.Italic)

                    If row("statusactivity").ToString = "CLOSED" Then

                        rootChildren1.BackColor = Color.Gray
                    End If

                    If row("statusactivity").ToString = "STANDBY" Then
                        rootChildren1.BackColor = Color.LightBlue
                    End If

                    If row("statusactivity").ToString = "SENT" Then
                        rootChildren1.BackColor = Color.LimeGreen
                    End If

                    If row("statusactivity").ToString = "" Then
                        rootChildren1.BackColor = Color.LightGray
                    End If

                    If Not CheckBoxCustomer.Checked Then
                        TreeViewActivity.Nodes.Add(rootChildren1)
                    Else
                        rootNode.Nodes.Add(rootChildren1)
                    End If

                    activity = Val(row("idactivity").ToString)
                End If

                Dim rootChildren2 As TreeNode = New TreeNode(row("etd").ToString & Mid("__________", 1, 10 - Len(row("etd"))) & " -- " & row("npieces") & Mid("___________", 1, 6 - Len(Str(row("npieces")))) & " pcs -- [" & row("bitronpn") & "]  " & row("name"))

                rootChildren2.NodeFont = New Font("Courier New", 12, FontStyle.Bold)
                If row("statusactivity").ToString = "OPEN" And row("etd").ToString <> "" Then
                    If DateDiff("d", Now, string_to_date(row("etd").ToString)) > 7 Then
                        rootChildren2.ForeColor = Color.DarkGreen
                    End If

                    If DateDiff("d", Now, string_to_date(row("etd").ToString)) < 0 Then
                        rootChildren2.ForeColor = Color.Red
                    End If

                    If DateDiff("d", Now, string_to_date(row("etd").ToString)) < 7 And DateDiff("d", Now, string_to_date(row("etd").ToString)) >= 0 Then
                        rootChildren2.ForeColor = Color.Orange
                    End If

                    If Val(row("npieces").ToString) = 0 Then rootChildren2.ForeColor = Color.LimeGreen

                ElseIf row("statusactivity").ToString <> "OPEN" And row("statusactivity").ToString <> "" Then
                    rootChildren2.ForeColor = Color.LightGray
                Else
                End If
                rootChildren1.Nodes.Add(rootChildren2)
            End If
            If CheckBoxCustomer.Checked Then rootNode.Expand()
        Next
        TextBoxProductStatus.Text = ""
        TextBoxProduct.Text = ""
        TextBoxProductQt.Text = ""
        TextBoxETD.Text = ""
        TreeViewActivity.EndUpdate()
    End Sub

    Private Sub CheckBoxOrderActivity_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        UpdateTreeSample()
    End Sub

    Private Sub TreeViewActivity_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewActivity.AfterSelect
        If Mid(TreeViewActivity.SelectedNode.Text, 1, 2) = "<>" Then
            currentActivityID = Int(Trim(Mid(TreeViewActivity.SelectedNode.Text, 4, InStr(TreeViewActivity.SelectedNode.Text, "--") - 5)))
            currentProductCode = ""
        ElseIf Mid(TreeViewActivity.SelectedNode.Text, 1, 2) <> "--" Then
            currentActivityID = -1
            currentProductCode = Val(Mid(TreeViewActivity.SelectedNode.Text, 1 + InStr(TreeViewActivity.SelectedNode.Text, "["), InStr(TreeViewActivity.SelectedNode.Text, "]") - InStr(TreeViewActivity.SelectedNode.Text, "[")))
            ComboBoxActivityID.Text = ""
        Else
            currentActivityID = -1
            currentProductCode = ""
            ComboBoxActivityID.Text = ""
            ComboBoxActivityStatus.Text = ""
            TextBoxProductStatus.Text = ""
            TextBoxProduct.Text = ""
            TextBoxProductQt.Text = ""
            TextBoxETD.Text = ""
        End If
        Try
            UpdateCurrent()
        Catch ex As Exception
            Stop
        End Try

    End Sub

    Sub UpdateCurrent()
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")

        TextBoxProductQt.Enabled = False
        ComboBoxBomLocation.Enabled = False

        If currentProductCode <> "" Then
            rowShow = tblProd.Select("bitronpn='" & currentProductCode & "'", "etd desc")
            If rowShow.Length > 0 Then
                TextBoxProductStatus.Text = rowShow(0).Item("status").ToString
                TextBoxProduct.Text = Replace(Replace(Mid(TreeViewActivity.SelectedNode.Text, InStr(TreeViewActivity.SelectedNode.Text, "[")), "[", ""), "]", "")
                TextBoxProductQt.Text = rowShow(0).Item("npieces").ToString
                TextBoxETD.Text = rowShow(0).Item("etd").ToString
                ComboBoxBomLocationAddAvaiable(currentProductCode)
                ComboBoxBomLocation.Text = (rowShow(0).Item("BomLocation").ToString)

                ComboBoxActivityID.Text = rowShow(0).Item("idactivity").ToString & " -- " & rowShow(0).Item("Nameactivity").ToString
                ComboBoxActivityStatus.Text = rowShow(0).Item("Statusactivity").ToString
            End If

            TextBoxProductQt.Enabled = True
            ComboBoxBomLocation.Enabled = True
        ElseIf currentActivityID >= 0 And Mid(TreeViewActivity.SelectedNode.Text, 1, 2) <> "--" Then
            rowShow = tblProd.Select("idactivity=" & currentActivityID & "")
            ComboBoxActivityID.Text = rowShow(0).Item("idactivity").ToString & " -- " & rowShow(0).Item("Nameactivity").ToString
            ComboBoxActivityStatus.Text = rowShow(0).Item("Statusactivity").ToString
            If currentActivityID = 0 Then ComboBoxActivityID.Text = "0 -- NOT ASSIGNED"
            TextBoxProductStatus.Text = ""
            TextBoxProduct.Text = ""
            TextBoxProductQt.Text = ""
            TextBoxETD.Text = ""
        Else
            ComboBoxActivityID.Text = ""
            ComboBoxActivityStatus.Text = ""
            TextBoxProductStatus.Text = ""
            TextBoxProduct.Text = ""
            TextBoxProductQt.Text = ""
            TextBoxETD.Text = ""

        End If

    End Sub

    ' search if there is a product with bom in offer and in sigip bom
    Sub ComboBoxBomLocationAddAvaiable(ByVal mycurrentProductCode As String)
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        ComboBoxBomLocation.Items.Clear()
        ComboBoxBomLocation.Items.Add("")
        If currentProductCode <> "" Then
            tblProd.Select("bitronpn='" & mycurrentProductCode & "'", "etd desc")
            ComboBoxBomLocation.Items.Add("SIGIP")
            'ComboBoxBomLocation.Items.Add("BEQS")
        End If
    End Sub

    Sub UpdateActivityID()
        Dim DsProd As New DataSet
        Dim activityid = 0
        ComboBoxActivityID.Items.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select(IIf(CheckBoxClosed.Checked = False, "NOT statusActivity = 'CLOSED'", "bitronpn like '*'"), "idactivity")
        ComboBoxActivityID.Items.Add("")
        ComboBoxActivityID.Items.Add("0 -- NOT ASSIGNED")
        For Each row In rowShow
            If (Val(row("idactivity")) <> activityid) Then
                If Val(row("idactivity")) > 0 Then ComboBoxActivityID.Items.Add(row("idactivity") & " -- " & row("NameActivity"))
                activityid = row("idactivity")
            End If
        Next
    End Sub

    Private Sub DateTimePickerETD_CloseUp(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerETD.CloseUp
        TextBoxETD.Text = DateTimePickerETD.Text
    End Sub

    Private Sub TextBoxETD_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TextBoxETD.MouseDoubleClick
        TextBoxETD.Text = ""
    End Sub

    Private Sub Buttonrefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Buttonrefresh.Click
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using

        UpdateTreeSample()
    End Sub

    Private Sub ButtonLink_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLink.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet, canDelete
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            If TextBoxProduct.Text <> "" And ComboBoxActivityID.Text <> "" And Len(TextBoxProductQt.Text) <= 6 Then

                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using

                tblProd = DsProd.Tables("Product")
                Dim rowShow As DataRow() = tblProd.Select("bitronpn='" & currentProductCode & "'")
                If rowShow.Length = 1 Then
                    ComboBoxActivityStatus.Text = rowShow(0).Item("Statusactivity")
                    Dim sql As String
                    If Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " ")))) <> Int(rowShow(0).Item("idactivity")) And
                        NumberProduct(Int(rowShow(0).Item("idactivity"))) = 1 Then
                        canDelete = MsgBox("This is the last product for this activity. If you delete this product you will delete also the activity and all linked tasks! Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes
                    Else
                        canDelete = True
                    End If
                    If canDelete Then
                        Try
                            sql = "UPDATE `" & DBName & "`.`product` SET " &
                            " `etd` = '" & TextBoxETD.Text &
                            "', `statusActivity` = '" & IIf(NumberProduct(Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))))) >= 1,
                            ActivityStatus(Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))))), "") &
                            "',`npieces` = " & Int(TextBoxProductQt.Text) &
                            ",`BomLocation` = '" & (ComboBoxBomLocation.Text) &
                            "',`idactivity` = " & Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " ")))) &
                            ",`nameactivity` = '" & Replace(Mid(ComboBoxActivityID.Text, InStr(ComboBoxActivityID.Text, " -- ") + 4), "NOT ASSIGNED", "") &
                            "' WHERE `product`.`bitronpn` = '" & Trim(Mid(TextBoxProduct.Text, 1, InStr(TextBoxProduct.Text, " "))) & "' ;"
                            Dim cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                            MsgBox("Successful update!")

                            If NumberProduct(Int(rowShow(0).Item("idactivity"))) = 1 Then
                                UpdateActivityID()
                            End If

                        Catch ex As Exception
                            MsgBox("Mysql update query error!")
                        End Try
                    Else
                        MsgBox("Failed update!")
                    End If
                Else
                    MsgBox("More products selected!")
                End If
            Else
                MsgBox("Need to set the product and the activity before pushing Save!")
            End If
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
            tblProd = DsProd.Tables("Product")
        End Using
        UpdateTreeSample()

    End Sub

    Private Sub TextBoxProduct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxProduct.TextChanged
        If TextBoxProduct.Text <> "" And ComboBoxActivityID.Text <> "" Then
            If controlRight("R") >= 2 Then ButtonLink.Enabled = True
            If controlRight("R") >= 2 Then ButtonNewCommit.Enabled = True
        Else
            ButtonLink.Enabled = False
            ButtonNewCommit.Enabled = False
        End If

        If TextBoxProduct.Text <> "" Then
            If controlRight("R") >= 2 Then ButtonNewCommit.Enabled = True
        Else
            ButtonNewCommit.Enabled = False
        End If
    End Sub

    Private Sub ComboBoxActivityID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxActivityID.TextChanged
        If ComboBoxActivityID.Text <> "" Then
            currentActivityID = Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))))
        Else
            currentActivityID = -1
        End If

        If TextBoxProduct.Text <> "" And ComboBoxActivityID.Text <> "" Then
            If controlRight("R") >= 2 Then ButtonLink.Enabled = True
        Else
            ButtonLink.Enabled = False
        End If

        If ComboBoxActivityID.Text <> "" And Mid(ComboBoxActivityID.Text, 1, 1) <> "0" Then
            ButtonFolder.Enabled = True
        Else
            ButtonFolder.Enabled = False
        End If

        If ComboBoxActivityID.Text <> "" And ComboBoxActivityStatus.Text <> "" Then
            If controlRight("R") >= 2 Then ButtonUpdateStatus.Enabled = True
        Else
            ButtonUpdateStatus.Enabled = False
        End If

        If Mid(ComboBoxActivityID.Text, 1, 1) = "0" Then
            ComboBoxActivityStatus.Text = ""
            ComboBoxActivityStatus.Enabled = False
        Else
            ComboBoxActivityStatus.Enabled = True
        End If

    End Sub

    Private Sub ButtonCollapse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCollapse.Click
        TreeViewActivity.CollapseAll()
    End Sub

    Private Sub ButtonUncollapse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUncollapse.Click
        TreeViewActivity.ExpandAll()
    End Sub

    Private Sub ButtonUpdateStatus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUpdateStatus.Click
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            If ComboBoxActivityID.Text <> "" And ComboBoxActivityStatus.Text <> "" Then
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
                Dim tblProd As DataTable = DsProd.Tables("Product")
                Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & Val(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))) & "")
                For Each row In rowShow
                    If Val(row("idactivity")) <> 0 Then

                        Try
                            Dim sql As String = "UPDATE `" & DBName & "`.`product` SET " &
                                                "`Statusactivity` = '" & ComboBoxActivityStatus.Text &
                                                "', `delay` = '" & IIf(ComboBoxActivityStatus.Text = "CLOSED", InputBox("Insert the closing activity delay (day):"), "") &
                                                "' WHERE `product`.`bitronpn` = '" & row("bitronpn") & "' ;"
                            Dim cmd As MySqlCommand = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()

                        Catch ex As Exception
                            MsgBox("Mysql update query error!")
                        End Try
                    Else

                    End If
                Next
                MsgBox("Status updated!")
            Else
                MsgBox("Need to fill activity and status!")
            End If
        End Using

    End Sub

    Private Sub ButtonNewCommit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonNewCommit.Click
        If TextBoxProduct.Text <> "" And productActivity(currentProductCode) = 0 Then
            Dim strActiv As String = InputBox("Please insert the name of new activity : " & vbCrLf & "PCB1 -- PCB2 -- PCB Description")
            If Regex.IsMatch(strActiv, "^[0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Then
                Try
                    Dim builder As New Common.DbConnectionStringBuilder()
                    builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                    Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                        Dim sql As String = "UPDATE `" & DBName & "`.`product` SET " &
                                            "`Statusactivity` = 'OPEN'" &
                                            ", `idactivity` = " & LastIDActivity() + 1 &
                                            ", `nameactivity` = '" & Trim(ReplaceChar(UCase(strActiv))) &
                                            "' WHERE `product`.`bitronpn` = '" & currentProductCode & "' ;"
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    End Using
                    MsgBox("Activity created with ID:" & LastIDActivity())
                Catch ex As Exception
                    MsgBox("Mysql update query error!")
                End Try
            Else
                MsgBox("Need to insert regulare name for expression!")
            End If
        Else
            MsgBox("Need to fill in product fields or product has already an activity!")
        End If
        UpdateActivityID()
    End Sub

    Function LastIDActivity() As Integer
        Dim DsProd As New DataSet
        Dim activityid = 0
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select("bitronpn like '*' ")
        For Each row In rowShow
            If row("idactivity") > activityid Then
                activityid = row("idactivity")
            End If
        Next
        LastIDActivity = activityid
    End Function

    Function NumberProduct(ByVal idactivity As Integer) As Integer
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select("idactivity = " & idactivity)
        NumberProduct = rowShow.Length
    End Function

    ' productActivity
    Function productActivity(ByVal productpn As String) As Integer
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select("bitronpn = " & productpn)
        If rowShow.Length > 0 Then
            productActivity = rowShow(0).Item("idactivity").ToString
        Else
            productActivity = 0
        End If
    End Function

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonFolder.Click
        If ComboBoxActivityID.Text <> "" And Mid(ComboBoxActivityID.Text, 1, 1) <> "0" Then
            Try
                If Directory.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text) Then

                Else
                    MkDir(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)
                End If


                If Directory.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text) Then

                Else
                    MkDir(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)
                End If


                If File.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text & "\" & ParameterTable("plant") & "R_PRO_ASR_" & ComboBoxActivityID.Text & "_0.xlsx") Then

                Else
                    File.Copy(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ParameterTable("PathFileASR") & ParameterTable("FileASR"), ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text & "\" & ParameterTable("plant") & "R_PRO_ASR_" & ComboBoxActivityID.Text & "_0.xlsx")
                End If

                Process.Start("explorer.exe", ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)

            Catch ex As Exception
                MsgBox("Directory creation error!" & ex.ToString)
            End Try
        End If
    End Sub

    Function ActivityStatus(ByVal id As Integer) As String
        Dim DsProd As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select("idactivity=" & id & "")
        ActivityStatus = ""
        If id Then
            If rowShow.Length Then ActivityStatus = rowShow(0).Item("Statusactivity").ToString
        End If
    End Function

    Private Sub ComboBoxActivityStatus_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxActivityStatus.TextChanged
        If CheckBoxClosed.Checked = True And ComboBoxActivityStatus.SelectedIndex <> 0 Then CheckBoxClosed.Checked = False
        If ComboBoxActivityID.Text <> "" And ComboBoxActivityStatus.Text <> "" Then
            If controlRight("R") >= 2 Then ButtonUpdateStatus.Enabled = True
        Else
            ButtonUpdateStatus.Enabled = False
        End If
    End Sub

    Sub SaveTree(ByVal myTree As TreeNode, ByVal path As String)
        If path <> "" Then
            For Each node In myTree.Nodes
                WriteTxtFile(path, Replace(node.ToString, "TreeNode:", ""), True)
                SaveTree(node, path)
            Next
        End If
    End Sub

#Region "task"

    Private Sub TabControlNPI_TabIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles _
                TabControlNPI.SelectedIndexChanged, ComboBoxType.SelectedIndexChanged
        If InStr(TabControlNPI.SelectedTab.Text, "Task") > 0 Then
            TreeViewTask.Font = New Font("Courier New", 11, FontStyle.Regular)
            If currentActivityID > 0 Then
                LabelActivityTask.Text = ComboBoxActivityID.Text
                XmlTree.SetTreeView(TreeViewTask)
                UpdateTreeTask()
                If controlRight("R") >= 2 Then ButtonSave.Enabled = True
                If controlRight("R") >= 2 Then ButtonUpdate.Enabled = True
                If controlRight("R") >= 2 Then ButtonNew.Enabled = True
                If controlRight("R") >= 2 Then ButtonDelete.Enabled = True
                TextBoxTaskHeader.Enabled = True
                TextBoxTaskNote.Enabled = True
                ComboBoxTaskStatus.Enabled = True
                ComboBoxType.Enabled = True
                If controlRight("R") >= 2 Then ButtonReset.Enabled = True
                If controlRight("R") >= 2 And controlRight("J") >= 2 Then
                    ButtonSaveDefault.Enabled = True
                Else
                    ButtonSaveDefault.Enabled = False
                End If
            Else
                LabelActivityTask.Text = ""
                TreeViewTask.Nodes.Clear()
                ButtonSave.Enabled = False
                ButtonUpdate.Enabled = False
                ButtonNew.Enabled = False
                ButtonDelete.Enabled = False
                TextBoxTaskHeader.Enabled = False
                TextBoxTaskNote.Enabled = False
                ComboBoxTaskStatus.Enabled = False
                ComboBoxType.Enabled = False
            End If
        Else
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                If currentActivityID > 0 Then
                    If OpenSession Then
                        If vbYes = MsgBox("Session open! Do you want to save?", MsgBoxStyle.YesNo) Then
                            ButtonSave_Click(Me, e)
                        Else
                            Dim tblProd As DataTable
                            Dim DsProd As New DataSet
                            Dim rowShow As DataRow()

                            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                                AdapterProd.Fill(DsProd, "Product")
                            End Using
                            tblProd = DsProd.Tables("Product")
                            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")

                            For Each row In rowShow
                                session("product", row("Id").ToString, False)
                            Next
                            OpenSession = False
                            TimerTask.Stop()
                            ButtonSave.BackColor = Color.Green
                            TextBoxBomTime.Text = ""
                        End If
                    Else
                        TimerTask.Stop()
                        Dim tblProd As DataTable
                        Dim DsProd As New DataSet
                        Dim rowShow As DataRow()
                        If currentActivityID > 0 Then
                            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                                AdapterProd.Fill(DsProd, "Product")
                            End Using
                            tblProd = DsProd.Tables("Product")
                            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
                            For Each row In rowShow
                                session("Product", row("Id").ToString, False)
                            Next
                        End If

                    End If
                End If
            End Using
            If InStr(TabControlNPI.SelectedTab.Text, "OpenIssue") > 0 Then
                If controlRight("W") >= 2 Then
                    Btn_Add.Enabled = True
                    Btn_Del.Enabled = True
                    Btn_Save.Enabled = True
                    Btn_UpLoadFile.Enabled = True
                End If
            End If
        End If
        If Not firstLoad Then
            DeselectRows()
            firstLoad = False
        End If
    End Sub

    Sub UpdateTreeTask()
        Dim DsProd As New DataSet
        If currentActivityID > 0 Then
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "", ComboBoxType.Text & " desc")
            If rowShow.Length > 0 Then
                If rowShow(0).Item(ComboBoxType.Text).ToString <> "" Then
                    XmlTree.Import(rowShow(0).Item(ComboBoxType.Text).ToString)
                    Dim rootNode As TreeNode = TreeViewTask.Nodes.Item(0)
                    TreeViewTask.Nodes.Clear()
                    For Each node In rootNode.Nodes
                        TreeViewTask.Nodes.Add(node)
                        colorNode(node)
                    Next
                Else
                    TreeViewTask.Nodes.Clear()
                End If
            Else

            End If
            ComboBoxTaskStatus.Text = ("    ")
            TextBoxTaskHeader.Text = ""
            TextBoxTaskNote.Text = ""
        End If
        TreeViewTask.HideSelection = False
        TreeViewActivity.HideSelection = False
    End Sub

    Sub colorNode(ByRef mynode As TreeNode)
        mynode.BackColor = Color.White
        mynode.NodeFont = New Font("Courier New", 11, FontStyle.Regular)
        If mynode.Level = 0 Then
            mynode.NodeFont = New Font("Courier New", 12, FontStyle.Bold)
            mynode.Text = percent(mynode) & Mid(mynode.Text, 5)
        End If
        If Mid(mynode.Text, 1, 4) = "NA  " Then mynode.BackColor = Color.Gray
        If Mid(mynode.Text, 1, 4) = "POST" Then mynode.BackColor = Color.Aquamarine
        If Mid(mynode.Text, 1, 4) = "100%" Then mynode.BackColor = Color.LimeGreen
        For Each nn In mynode.Nodes
            colorNode(nn)
        Next
    End Sub

    Function percent(ByVal node As TreeNode) As String
        Dim per As Integer, count As Integer
        For Each n In node.Nodes
            Try
                per = per + Int(Trim(Replace(Mid(n.Text, 1, 3), "%", "")))
            Catch ex As Exception
            End Try
            If Mid(n.Text, 1, 2) <> "NA" Then count = count + 1
        Next
        If node.Nodes.Count > 0 Then percent = Mid(Int(per / count) & "%   ", 1, 4)
    End Function

    Sub FillTaskStatus()
        ComboBoxTaskStatus.Items.Clear()
        ComboBoxTaskStatus.Items.Add("0%  ")
        ComboBoxTaskStatus.Items.Add("100%")
        ComboBoxTaskStatus.Items.Add("50% ")
        ComboBoxTaskStatus.Items.Add("NA  ")
        ComboBoxTaskStatus.Items.Add("POST")
        ComboBoxTaskStatus.Text = ("0%  ")
    End Sub

    Sub FillTaskType()
        ComboBoxType.Items.Clear()
        ComboBoxType.Items.Add("SOP_TASK")
        ComboBoxType.Items.Add("PROD_TASK")
        ComboBoxType.Items.Add("MOULD_TASK")
        ComboBoxType.Text = ("SOP_TASK")
    End Sub

    Sub SaveTreeTask(ByVal S As String)
        If currentActivityID > 0 Then
            Try
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Dim sql As String = "UPDATE `" & DBName & "`.`product` SET " &
                                        "`" & ComboBoxType.Text & "` = '" & S &
                                        "' WHERE `product`.`idactivity` = " & currentActivityID & " ;"
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MsgBox("Mysql update query error!")
            End Try

            MsgBox("Tasks saved!")
            ButtonSave.BackColor = Color.Green
        Else
            MsgBox("Need to fill in activity and status!")
        End If
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSave.Click
        Dim DsProd As New DataSet
        Dim CanSetReset As Boolean
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
        End Using
        Dim tblProd As DataTable = DsProd.Tables("Product")
        Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
        If rowShow.Length > 0 Then CanSetReset = True
        For Each row In rowShow
            CanSetReset = CanSetReset And (DeltaSessionTime("product", row("id").ToString) < 30) And (session("product", row("Id").ToString, False) = "RESET")
        Next
        If CanSetReset Then

            SaveTreeTask(XmlTree.ExportToString)
            For Each node In TreeViewTask.Nodes
                colorNode(node)
            Next
            OpenSession = False
            TimerTask.Stop()
            TextBoxBomTime.Text = ""
        Else
            For Each row In rowShow
                MsgBox("Section USED " & session("product", row("Id").ToString, False))
            Next
        End If
    End Sub

    Private Sub ButtonReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonReset.Click
        If currentActivityID > 0 Then
            Dim DsProd As New DataSet
            Dim CanSet As Boolean
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
            If rowShow.Length > 0 Then CanSet = True
            For Each row In rowShow
                CanSet = CanSet And (session("Product", row("Id").ToString, True) = "SET")
            Next

            If CanSet Then  ' valid session
                TextBoxBomTime.Text = "30"
                TimerTask.Interval = 60000
                TimerTask.Start()
                OpenSession = True
                ButtonSave.BackColor = Color.Orange
                XmlTree.Import(ParameterTable(ComboBoxType.Text))
                Dim n As TreeNode = TreeViewTask.Nodes.Item(0)
                TreeViewTask.Nodes.Clear()
                For Each node In n.Nodes
                    TreeViewTask.Nodes.Add(node)
                Next
            Else
                For Each row In rowShow
                    MsgBox("Section USED " & session("Product", row("Id").ToString, False))
                Next
            End If
        End If

    End Sub

    Private Sub ButtonNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonNew.Click
        If currentActivityID > 0 Then
            Dim DsProd As New DataSet
            Dim CanSet As Boolean

            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
            If rowShow.Length > 0 Then CanSet = True
            For Each row In rowShow
                CanSet = CanSet And (session("Product", row("Id").ToString, True) = "SET")
            Next

            If CanSet Then  ' valid session
                TextBoxBomTime.Text = "30"
                TimerTask.Interval = 60000
                TimerTask.Start()
                OpenSession = True

                If Not IsNothing(TreeViewTask.SelectedNode) Then
                    Dim rootNode As New TreeNode
                    rootNode.Text = (Mid("0%", 1, 4) & " - " &
                    UCase(Mid(" * * new * *" & "__________________________", 1, 25)) & " - " &
                    Mid(" * * new * *", 1))
                    TreeViewTask.SelectedNode.Nodes.Add(rootNode)
                    ButtonSave.BackColor = Color.Orange
                Else
                    Dim rootNode As New TreeNode
                    rootNode.Text = (Mid("0%", 1, 4) & " - " &
                    UCase(Mid(" * * new * *" & "__________________________", 1, 25)) & " - " &
                    Mid(" * * new * *", 1))
                    TreeViewTask.Nodes.Add(rootNode)
                    ButtonSave.BackColor = Color.Orange
                End If
            Else

                For Each row In rowShow
                    MsgBox("Section USED " & session("Product", row("Id").ToString, False))
                Next
            End If
        End If
    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDelete.Click
        If currentActivityID > 0 Then
            Dim DsProd As New DataSet
            Dim CanSet As Boolean

            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
            If rowShow.Length > 0 Then CanSet = True
            For Each row In rowShow
                CanSet = CanSet And (session("Product", row("Id").ToString, True) = "SET")
            Next

            If CanSet Then  ' valid session
                TextBoxBomTime.Text = "30"
                TimerTask.Interval = 60000
                TimerTask.Start()
                OpenSession = True

                Try
                    If vbYes = MsgBox("Are you sure to delete this node?", MsgBoxStyle.YesNo) Then TreeViewTask.SelectedNode.Remove()
                Catch ex As Exception
                    MsgBox("Error during deleting! " & ex.Message)
                End Try
                ButtonSave.BackColor = Color.Orange
            Else

                For Each row In rowShow
                    MsgBox("Section USED " & session("Product", row("Id").ToString, False))
                Next
            End If
        End If
    End Sub

    Private Sub TreeViewTask_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewTask.AfterSelect
        Try
            If controlRight("R") >= 2 Then ButtonNew.Enabled = True
            If controlRight("R") >= 2 Then ButtonDelete.Enabled = True
            If controlRight("R") >= 2 Then TextBoxTaskHeader.Enabled = True
            If controlRight("R") >= 2 Then TextBoxTaskNote.Enabled = True
            TextBoxTaskHeader.Text = UCase(Mid(TreeViewTask.SelectedNode.Text, 8, 25))
            ComboBoxTaskStatus.Text = UCase(Mid(TreeViewTask.SelectedNode.Text, 1, 4))
            TextBoxTaskNote.Text = (Mid(TreeViewTask.SelectedNode.Text, 36))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ButtonUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUpdate.Click
        If Not IsNothing(TreeViewTask.SelectedNode) And currentActivityID > 0 Then
            Dim DsProd As New DataSet
            Dim CanSet As Boolean
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                    AdapterProd.Fill(DsProd, "Product")
                End Using
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
            If rowShow.Length > 0 Then CanSet = True
            For Each row In rowShow
                CanSet = CanSet And (session("Product", row("Id").ToString, True) = "SET")
            Next

            If CanSet Then  ' valid session
                TextBoxBomTime.Text = "30"
                TimerTask.Interval = 60000
                TimerTask.Start()
                OpenSession = True

                TreeViewTask.SelectedNode.Text = UCase(Mid(ComboBoxTaskStatus.Text, 1, 4) & " - " &
                UCase(Mid(TextBoxTaskHeader.Text & "__________________________", 1, 25)) & " - " &
                Mid(TextBoxTaskNote.Text, 1))
                ButtonSave.BackColor = Color.Orange
            Else

                For Each row In rowShow
                    MsgBox("Section USED " & session("Product", row("Id").ToString, False))
                Next
            End If
        End If
    End Sub

    Private Sub ButtonTaskCollapse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTaskCollapse.Click
        TreeViewTask.CollapseAll()
    End Sub

    Private Sub ButtonExpand_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonExpand.Click
        TreeViewTask.ExpandAll()
    End Sub

#End Region  ' Task 
    Sub PrintNode(ByVal FileName As String, ByVal node As TreeNode)
        For Each n In node.Nodes
            WriteTxtFile(FileName, n.ToString, True)
            PrintNode(FileName, n)
        Next
    End Sub

    Private Sub ButtonExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonExport.Click
        SaveFileDialog1.DefaultExt = "txt"
        SaveFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog1.ShowDialog()
        Try
            WriteTxtFile(SaveFileDialog1.FileName, ("Product: " & Now), False)
            For Each node In TreeViewActivity.Nodes
                WriteTxtFile(SaveFileDialog1.FileName, node.ToString, True)
                PrintNode(SaveFileDialog1.FileName, node)
            Next
            WriteTxtFile(SaveFileDialog1.FileName, "", True)
            WriteTxtFile(SaveFileDialog1.FileName, "", True)

            WriteTxtFile(SaveFileDialog1.FileName, ("Task: " & Now), True)
            For Each node In TreeViewTask.Nodes
                WriteTxtFile(SaveFileDialog1.FileName, node.ToString, True)
                PrintNode(SaveFileDialog1.FileName, node)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TimerTask_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerTask.Tick
        If Val(TextBoxBomTime.Text) > 1 Then
            TextBoxBomTime.Text = Val(TextBoxBomTime.Text) - 1
        Else
            OpenSession = False
            TimerTask.Stop()
            ButtonSave.BackColor = Color.Green
            TextBoxBomTime.Text = ""
            Dim DsProd As New DataSet
            If currentActivityID > 0 Then
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                        AdapterProd.Fill(DsProd, "Product")
                    End Using
                End Using
                Dim tblProd As DataTable = DsProd.Tables("Product")
                Dim rowShow As DataRow() = tblProd.Select(" idactivity =" & currentActivityID & "")
                For Each row In rowShow
                    session("Product", row("Id").ToString, False)
                Next
                UpdateTreeTask()
            End If
            MsgBox("Session expired!")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        TextBoxETD.Text = ""
    End Sub

    Private Sub Update_Pfp()
        CollectProcess()
        'open Parti_Fornitori_Prezzi.xls
        Dim xlsApp As Object = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelPfp"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")
        'empty the PFP table
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Dim commandMySql As MySqlCommand = New MySqlCommand("TRUNCATE TABLE `" & DBName & "`.`pfp`", con)
            commandMySql.ExecuteNonQuery()

            'save the .xls file in .csv format
            Dim tempPath = Path.GetTempPath() & "temp.csv"
            Try
                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If
                xlsWorkbook.SaveAs(tempPath, 6)
                xlsWorkbook.Close(True)
                xlsApp.Quit()
                Dim generation As Integer = GC.GetGeneration(xlsApp)
                GC.Collect(generation)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'copy data from excel to `pfp`
            Dim sql As String = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `pfp` CHARACTER SET latin1 fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`pfidf`,`pepre`,`peval`,`pfpaf`,`pfpan`,`pfpad`,`pelot`,`pedin`,`pedfi`,`pefor`,`forsc`)"
            commandMySql = New MySqlCommand(sql, con)
            commandMySql.ExecuteNonQuery()
        End Using
        KillLastExcel()
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

    Private Sub ButtonUpdateMagBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonUpdateMagBox.Click
        Dim DsProd As New DataSet
        Dim BomName = ""
        Dim i = 0
        Dim commandMySql As New MySqlCommand
        ButtonUpdateMagBox.Text = "Wait ....."
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import RDA ....."
        Application.DoEvents()
        Import_Rda()
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import Order ....."
        Application.DoEvents()
        Import_Order()
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import Warehouse Stock ....."
        Application.DoEvents()
        Import_WH_Stock()
        ButtonUpdateMagBox.Text = "Import PFP ....."
        Application.DoEvents()
        Update_Pfp()
        ButtonUpdateMagBox.Text = "Update Material Request ....."
        Application.DoEvents()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))

            If IsNothing(tblSigip) Then
                Using AdapterSigip As New MySqlDataAdapter("SELECT * FROM sigip", con)
                    AdapterSigip.Fill(DsSigip, "sigip")
                End Using
                tblSigip = DsSigip.Tables("sigip")
            End If

            If IsNothing(tblOff) Then
                Using AdapterOff As New MySqlDataAdapter("SELECT * FROM offer", con)
                    AdapterOff.Fill(DsOff, "offer")
                End Using
                tblOff = DsOff.Tables("offer")
            End If
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity ,etd", con)
                AdapterProd.Fill(DsProd, "Product")
            End Using
            Dim tblProd As DataTable = DsProd.Tables("Product")
            Dim rowShow As DataRow() = tblProd.Select("statusactivity = 'OPEN'")
            Dim dsMySql As New DataSet
            Dim sql = "SELECT * FROM `materialrequest` "
            Dim adapterMySql = New MySqlDataAdapter(sql, con)
            adapterMySql.Fill(dsMySql, "materialrequest")
            Dim tblMySql As DataTable = dsMySql.Tables("materialrequest")
            ButtonUpdateMagBox.Text = "Deleting data and shift....."
            Application.DoEvents()
            For Each rowShowMy In tblMySql.Rows

                sql = "UPDATE `materialrequest` SET `warehouse3d`='',`RequestQt`=0, `BomList`='',`delta`='',`DeltaUsedFlag`=''," &
                        "`RequestQt_1`=" & rowShowMy("RequestQt").ToString & "," &
                        "`RequestQt_2`=" & rowShowMy("RequestQt_1").ToString & "," &
                        "`RequestQt_3`=" & rowShowMy("RequestQt_2").ToString & "," &
                        "`RequestQt_4`=" & rowShowMy("RequestQt_3").ToString & "," &
                        "`RequestQt_5`=" & rowShowMy("RequestQt_4").ToString & "," &
                        "`ProductionUsed`='' where bitronpn = '" & rowShowMy("bitronpn") & "'"

                Try
                    commandMySql = New MySqlCommand(sql, con)
                    commandMySql.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("Error in DB... please reset material request!")
                End Try
            Next

            ButtonUpdateMagBox.Text = "Deleting data and shift ....."
            Application.DoEvents()
            ButtonUpdateMagBox.Text = "Load Orcad Data....."
            Application.DoEvents()
            tblDocComp.Clear()
            DsDocComp.Clear()
            Dim orcadBuilder As New Common.DbConnectionStringBuilder()
            orcadBuilder.ConnectionString = ConfigurationManager.ConnectionStrings("Orcad").ConnectionString
            Using orcadCon = NewOpenConnectionMySqlOrcad(orcadBuilder("host"), orcadBuilder("database"), orcadBuilder("username"), orcadBuilder("password"))
                Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') ", orcadCon)
                AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
                tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")
            End Using
            Try
                DsDoc.Clear()
                tblDoc.Clear()
            Catch ex As Exception

            End Try
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC;", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")

            ButtonUpdateMagBox.Text = "Start calculation ..."
            Application.DoEvents()
            Dim beqsVersions As String = ""
            Dim dictionaryVersionsQuatity As New Dictionary(Of String, Integer) ' do not delete this comment
            For Each row In rowShow
                i = i + 1
                If Val(row("NPIECES").ToString) > 0 Then
                    If row("bomlocation").ToString = "SIGIP" Then
                        Dim rowShowSigip As DataRow() = tblSigip.Select("bom ='" & row("bitronpn").ToString & "' and (acq_fab = 'acq' Or acq_fab = 'acv')")
                        If rowShowSigip.Length = 0 Then MsgBox("Bom not found in SIGIP: " & row("bitronpn").ToString & BomName)
                        For Each rowSigip In rowShowSigip
                            ButtonUpdateMagBox.Text = "Udpate: " & Math.Round(100 * i / rowShow.Length, 0) & "%"
                            Application.DoEvents()
                            If Val(rowSigip("qt").ToString) * Val(row("npieces").ToString) > 0 Then AddRequest(rowSigip("bitron_pn").ToString, rowSigip("des_pn").ToString, rowSigip("qt").ToString, row("npieces").ToString, rowSigip("bom").ToString, rowSigip("bom").ToString & " - " & rowSigip("des_bom").ToString, , , rowSigip("doc").ToString)
                        Next
                    ElseIf row("bomlocation").ToString() = "BEQS" Then
                        ' TODO: Add business logic
                        dictionaryVersionsQuatity.Add(row("bitronpn"), row("npieces"))  ' do not delete this comment
                    Else
                        MsgBox("For this product BOM not assigned! " & row("bitronpn").ToString & "  " & row("name").ToString)
                    End If
                End If
            Next

            If dictionaryVersionsQuatity.Count > 0 Then ' do Not delete this comment
                FormBomOffer.ShowForm(dictionaryVersionsQuatity) ' Do Not delete this comment
            End If ' Do Not delete this comment


            sql = "DELETE FROM `" & DBName & "`.`materialRequest` WHERE `materialRequest`.`REQUESTQT` = 0 AND `materialRequest`.`REQUESTQT_1` = 0 AND `materialRequest`.`REQUESTQT_2` = 0 AND  `materialRequest`.`REQUESTQT_3` = 0 AND `materialRequest`.`REQUESTQT_4` = 0 AND  `materialRequest`.`REQUESTQT_5` = 0"

            Try
                commandMySql = New MySqlCommand(sql, con)
                commandMySql.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Error in DB... please reset material request!")
            End Try

            Dim InOrder As Single = order("", True)
            Dim InRda As Single = Rda("", True)

            rowShow = tblMySql.Select("delta < 0 ")
            For Each row In rowShow
                RdaInfo = ""
                OrderInfo = ""
                Dim NeedRda As String = ""
                If Mid(row("bitronpn").ToString, 1, 2) <> "Q_" And Mid(row("bitronpn").ToString, 1, 2) <> "18" Then
                    InOrder = order(row("bitronpn").ToString, False)
                    InRda = Rda(row("bitronpn").ToString, False)
                    If -Val(row("delta").ToString) > (InOrder + InRda) Then
                        NeedRda = "NEED_RDA[" & Val(Val(row("delta").ToString) + (InOrder + InRda)) & "];"
                    End If

                    sql = "UPDATE `materialrequest` SET `status`='" & RdaInfo & OrderInfo & NeedRda & "' where bitronpn = '" & row("bitronpn") & "'"

                    Try
                        commandMySql = New MySqlCommand(sql, con)
                        commandMySql.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox("Error in DB... please reset material request!")
                    End Try
                End If
                Application.DoEvents()
            Next

            rowShow = tblMySql.Select("delta >= 0 ")
            For Each row In rowShow
                sql = "UPDATE `materialrequest` SET `status`='" & "' where bitronpn = '" & row("bitronpn") & "'"

                Try
                    commandMySql = New MySqlCommand(sql, con)
                    commandMySql.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("Error in DB... please reset material request!")
                End Try
            Next
            tblMySql.Dispose()
            tblMySql.Dispose()
        End Using
        ButtonUpdateMagBox.Text = "Udpate Material Request"
    End Sub

    Function order(ByVal bitronpn As String, ByVal refrash As Boolean) As Single
        Static tblOrder As DataTable
        Static DsOrder As New DataSet
        Dim rowShow As DataRow()
        order = 0

        If refrash = True Then
            Try
                tblOrder.Clear()
                DsOrder.Clear()
            Catch ex As Exception

            End Try
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterOrder As New MySqlDataAdapter("SELECT * FROM `order`", con)
                    AdapterOrder.Fill(DsOrder, "order")
                End Using
            End Using
            tblOrder = DsOrder.Tables("order")
        Else
            rowShow = tblOrder.Select("identif ='" & bitronpn & "' and stato_item ='0'")
            For Each row In rowShow
                order = order + Val(row("qta_ord").ToString)
                OrderInfo = "ORDER_" & row("ordine").ToString & "[" & row("qta_ord").ToString & "];" & OrderInfo
            Next
        End If
    End Function

    Function Rda(ByVal bitronpn As String, ByVal refrash As Boolean) As Single
        Static tblRda As DataTable
        Static DsRda As New DataSet
        Dim rowShow As DataRow()
        Rda = 0

        If refrash = True Then
            Try
                tblRda.Clear()
                DsRda.Clear()
            Catch ex As Exception
            End Try
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterRda As New MySqlDataAdapter("SELECT * FROM Rda", con)
                    AdapterRda.Fill(DsRda, "Rda")
                End Using
            End Using
            tblRda = DsRda.Tables("Rda")
        Else
            Dim prodPlant As String = ParameterTable("plant")
            rowShow = tblRda.Select("RAIDF ='" & bitronpn & "' and RASTB ='" & prodPlant & "' AND ( RASTA ='I' OR  RASTA ='L' OR RASTA ='A' OR RASTA ='C' )")
            For Each row In rowShow
                Rda = Rda + Val(row("RAQT1").ToString) + Val(row("RAQT2").ToString) + Val(row("RAQT3").ToString) + Val(row("RAQT4").ToString) + Val(row("RAQT5").ToString)
                RdaInfo = "RDA_" & row("ranum").ToString & "_" & row("RASTA").ToString & "[" & Val(row("RAQT1").ToString) + Val(row("RAQT2").ToString) + Val(row("RAQT3").ToString) + Val(row("RAQT4").ToString) + Val(row("RAQT5").ToString) & "];" & RdaInfo
            Next
        End If
    End Function

    Function SigipUsed(ByVal bitronpn As String) As String
        SigipUsed = ""
        Dim rowShowSigip As DataRow() = tblSigip.Select("bitron_pn ='" & bitronpn & "' and (active = 'yes')")

        For Each rowSigip In rowShowSigip
            SigipUsed = SigipUsed & rowSigip.Item("bom").ToString & " - " & rowSigip.Item("des_bom").ToString & "[" & Val(rowSigip.Item("qt").ToString) & "];"
        Next
    End Function

    Sub AddRequest(ByVal bitronPN As String, ByVal des_PN As String, ByVal qt As String, ByVal npieces As String, ByVal Bom As String, ByVal des_bom As String, Optional ByVal brand As String = "", Optional ByVal brandAlt As String = "", Optional ByVal Doc As String = "")
        Dim strQt As String
        Dim strBomList
        Dim dsMySql As New DataSet
        Dim sql As String = "SELECT * FROM `materialrequest` WHERE `bitronpn`='" & bitronPN & "'"
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Dim adapterMySql = New MySqlDataAdapter(sql, con)
            adapterMySql.Fill(dsMySql, "materialrequest")
            Dim tblMySql As DataTable = dsMySql.Tables("materialrequest")
            Dim stockvalue As Double = Str(Stock(bitronPN))
            If tblMySql.Rows.Count < 1 Then
                strBomList = des_bom & "[" & Trim(Str(IIf(qt = Int(qt), Int(qt), Math.Round(Val(qt), 5)))) & "]"
                sql = "INSERT INTO `" & DBName & "`.`materialrequest` (`DeltaUsedFlag`,`ProductionUsed`,`bitronPN`,`des_pn`,`Brand`,`BrandALT`,`pfp`,`warehouse3d`,`Delta`,`RequestQt`,`BomList`,`doc`) VALUES ('" & IIf(SigipUsed(bitronPN) <> "" Or
                (stockvalue - Val(strQt)) < Val(strQt) * 0.1, "YES", "NO") & "','" & SigipUsed(bitronPN) & "','" & bitronPN & "','" & des_PN & "','" & brand & "','" & brandAlt & "','" & pfp(bitronPN) & "','" & stockvalue & "','" &
                 stockvalue - Val(Str(Val(qt) * Val(npieces))) & "','" & Trim(Str(Val(qt) * Val(npieces))) & "','" & strBomList & "','" & Doc & "')"
            Else
                strQt = Trim(Str(Val(tblMySql.Rows.Item(0)("requestqt")) + Str(Val(qt) * Val(npieces))))
                strBomList = tblMySql.Rows.Item(0)("BomList").ToString
                If strBomList.Contains(Bom) And strBomList <> "" Then
                    Dim i As Integer = InStr(strBomList, Bom, CompareMethod.Text)
                    i = InStr(i + 1, strBomList, "[", CompareMethod.Text)
                    Dim j As Integer = InStr(i, strBomList, "]", CompareMethod.Text)
                    strBomList = Mid(strBomList, 1, i) & Trim(Str(Val(Mid(strBomList, i + 1, j - 1 - i)) + Val(qt))) & Mid(strBomList, j)
                Else
                    strBomList = tblMySql.Rows.Item(0)("BomList") & ";" & des_bom & "[" & Trim(Str(IIf(qt = Int(qt), Int(qt), Math.Round(Val(qt), 5)))) & "]"
                    If Mid(strBomList, 1, 1) = ";" Then strBomList = Mid(strBomList, 2)
                End If
                sql = "UPDATE `materialrequest` SET `w_warehouse`=" & Val(Stock_W(bitronPN)) & ", `RequestQt`='" & strQt & "',`BomList`='" & strBomList & "',`brandALT`='" & brandAlt & "',`brand`='" & brand & "',`pfp`='" & pfp(bitronPN) & "',`doc`='" & Doc & "',`warehouse3d`='" & stockvalue & "',`Delta`='" & stockvalue - Val(strQt) & "',`ProductionUsed`='" & SigipUsed(bitronPN) & "',`DeltaUsedFlag`='" & IIf(SigipUsed(bitronPN) <> "" Or (stockvalue - Val(strQt)) < Val(strQt) * 0.1, "YES", "NO") & "' WHERE `bitronpn`='" & bitronPN & "'"
            End If
            Try
                Dim commandMySql = New MySqlCommand(sql, con)
                commandMySql.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Error in DB... please reset material request!")
            End Try
        End Using

    End Sub
    Private Sub ButtonSaveDefault_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSaveDefault.Click
        If MsgBox("Are you sure to save and change current configuration?", MsgBoxStyle.YesNo) = vbYes Then
            XmlTree.SetTreeView(TreeViewTask)
            ParameterTableWrite("sop_task", XmlTree.ExportToString)
        End If
    End Sub

    Private Sub TextBoxTaskHeader_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxTaskHeader.TextChanged
        If Mid(TextBoxTaskHeader.Text, 1, 1) = "[" Or Mid(TextBoxTaskHeader.Text, 1, 1) = "{" Then
            TextBoxTaskHeader.Enabled = False
        Else
            TextBoxTaskHeader.Enabled = True
        End If
        If Mid(TextBoxTaskHeader.Text, 1, 1) = "{" Then
            TextBoxTaskNote.Enabled = False
        Else
            TextBoxTaskNote.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxClosed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxClosed.CheckedChanged
        UpdateActivityID()
        If CheckBoxClosed.Checked = True Then
            CheckBoxOpenProduct.Checked = False
            ComboBoxActivityStatus.SelectedIndex = 0
        End If
    End Sub

    Public Sub Import_Order()
        Dim xlsApp As Object = CreateObject("Excel.Application")
        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelSigipOrder"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")

        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            'empty the PFP table
            Dim commandMySql As MySqlCommand = New MySqlCommand("TRUNCATE TABLE `" & DBName & "`.`order`", con)
            commandMySql.ExecuteNonQuery()

            'save the .xls file in .csv format
            Dim tempPath = Path.GetTempPath() & "temp.csv"
            Try
                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If
                xlsWorkbook.SaveAs(tempPath, 6)
                xlsWorkbook.Close(True)
                xlsApp.Quit()
                Dim generation As Integer = GC.GetGeneration(xlsApp)
                GC.Collect(generation)
                'copy data from excel to `pfp`
                Dim sql As String = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `order` CHARACTER SET latin1 fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`stab`,`ordine`,`tipoOrd`,`forn`,`RagSoc`,`Stato_Ord`,`Data_Inserimento`,`Acquisitore`,`Num_Item`,`Num_RDA`,`Identif`,`Descr`,`Stato_Item`,`Qta_Ord`,`Qta_Consegnata`,`Qta_Scartata`)"
                commandMySql = New MySqlCommand(sql, con)
                commandMySql.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub

    Public Sub Import_WH_Stock()
        Dim xlsWorkbook As Object

        'open Saldi_per_ubicazione.xls
        Dim xlsApp As Object = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Try
            xlsWorkbook = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelStock"))
            xlsWorkbook.Activate()
            Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
            xlsWorksheet.Activate()
            xlsWorksheet.Cells.Replace(What:=",", Replacement:="")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))

            'empty the PFP table
            Dim commandMySql As MySqlCommand = New MySqlCommand("TRUNCATE TABLE `" & DBName & "`.`spu`", con)
            commandMySql.ExecuteNonQuery()

            'save the .xls file in .csv format
            Dim tempPath = Path.GetTempPath() & "temp.csv"
            Try
                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If

                xlsWorkbook.SaveAs(tempPath, 6)
                xlsWorkbook.Close(True)
                xlsApp.Quit()
                Dim generation As Integer = GC.GetGeneration(xlsApp)
                GC.Collect(generation)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'copy data from excel to `pfp`
            Dim sql As String = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `spu` CHARACTER SET latin1 fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`bitronpn`,`pades`,`sagia`,`samgz`,`saubc`,`paumt`,`pmppa`,`pmcmm`,`paclm`)"
            commandMySql = New MySqlCommand(sql, con)
            commandMySql.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub Import_Rda()
        'open rda_per_ubicazione.xls
        Dim xlsApp As Object = CreateObject("Excel.Application")
        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelSigipRda"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            'empty the rda table
            Dim commandMySql = New MySqlCommand("TRUNCATE TABLE `" & DBName & "`.`rda`", con)
            commandMySql.ExecuteNonQuery()

            'save the .xls file in .csv format
            Dim tempPath = Path.GetTempPath() & "temp.csv"
            Try
                If File.Exists(tempPath) Then
                    File.Delete(tempPath)
                End If

                xlsWorkbook.SaveAs(tempPath, 6)
                xlsWorkbook.Close(True)
                xlsApp.Quit()
                Dim generation As Integer = GC.GetGeneration(xlsApp)
                GC.Collect(generation)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'copy data from excel to `rda`
            Dim sql As String = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `rda` CHARACTER SET latin1 fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`RATRK`,`RASTB`,`RANUM`,`RATIF`,`RAITE`,`RADES`,`RACMA`,`RAIDF`,`RAVSM`,`RAVAL`,`RAUNC`,`RAQT1`,`RAQT2`,`RAQT3`,`RAQT4`,`RAQT5`,`RADT1`,`RADT2`,`RADT3`,`RADT4`,`RADT5`,`RAQTO`,`RAVSC`,`RACOM`,`RAFOR`,`RACDI`,`RACDR`,`RAUSE`,`RADTE`,`RADCV`,`RASTA`,`RAOA1`,`RAECO`,`RALOT`,`RAPGM`,`RABUY`,`RAUAG`,`RAORA`)"
            commandMySql = New MySqlCommand(sql, con)
            commandMySql.ExecuteNonQuery()
        End Using
    End Sub

    Public Function Stock(ByVal bitronpn As String) As Double
        Dim dsMySql As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using adapterMySql As New MySqlDataAdapter("SELECT SUM(`sagia`) AS sum FROM `" & DBName & "`.`spu` WHERE (samgz='D' or samgz='8') and `bitronpn`='" & bitronpn & "'", con)
                adapterMySql.Fill(dsMySql, "spu")
            End Using
        End Using
        Dim tblMySql As DataTable = dsMySql.Tables("spu")
        Return Val(tblMySql.Rows(0).Item("sum").ToString)
    End Function

    Public Function Stock_W(ByVal bitronpn As String) As Double
        Dim dsMySql As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using adapterMySql As New MySqlDataAdapter(String.Format("SELECT SUM(`sagia`) AS sum FROM `{0}`.`spu` WHERE (" & ParameterTable("StockFunctionWhereClause") & " ) and `bitronpn`='{1}'", DBName, bitronpn), con)
                adapterMySql.Fill(dsMySql, "spu")
            End Using
        End Using
        Dim tblMySql As DataTable = dsMySql.Tables("spu")
        Return Val(tblMySql.Rows(0).Item("sum").ToString)
    End Function

    Function pfp(ByVal bitronpn As String) As String
        pfp = ""
        Dim rowShow As DataRow() = tblPfp.Select("pfidf = '" & Replace(ReplaceChar(bitronpn), "E", "") & "' and  pedfi = '0'", "pfpan desc, pfpaf desc, pedin, pfpad")
        Dim ass As Integer = 0
        For Each row In rowShow
            If Val(row("pfpad")) > 0 Then
                If Val(row("pfpan")) > 0 Then
                    pfp = pfp & " " & (ConvPrice(row("pepre"), row("pelot")) & row("peval") & " Date: " & row("pedin")) & " Share: " & row("pfpan") & "; "
                    ass = ass + Val(row("pfpan"))
                End If
                If ass >= 100 Then Exit For
            ElseIf Val(row("pfpad")) = 0 Then
                If Val(row("pfpaf")) > 0 Then
                    pfp = pfp & " " & (ConvPrice(row("pepre"), row("pelot")) & row("peval") & " Date: " & row("pedin")) & " Share: " & row("pfpaf") & "; "
                    ass = ass + Val(row("pfpaf"))
                End If
                If ass >= 100 Then Exit For
            End If
        Next
        If (ass < 100 Or ass > 100) And rowShow.Length > 0 Then
            pfp = pfp & " " & ("Error in PFP recognize of p/n " & Replace(bitronpn, "E", ""))
        End If
    End Function

    Function ConvPrice(ByVal Price As String, ByVal batch As String) As String
        If batch = "TH" Then
            ConvPrice = Math.Round(Val(Price / 1000), 5)
        ElseIf batch = "EA" Then
            ConvPrice = Math.Round(Val(Price / 1000), 5)
        Else
            ConvPrice = 0
            MsgBox("Conversion error for batch " & batch)
        End If
    End Function

    Function GetOrcadSupplier(ByVal BitronPN As String) As String
        GetOrcadSupplier = ""
        Try
            Dim orcadBuilder As New Common.DbConnectionStringBuilder()
            orcadBuilder.ConnectionString = ConfigurationManager.ConnectionStrings("Orcad").ConnectionString
            Using orcadCon = NewOpenConnectionMySqlOrcad(orcadBuilder("host"), orcadBuilder("database"), orcadBuilder("username"), orcadBuilder("password"))
                Dim AdapterSql As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') and codice_bitron = '" & BitronPN & "'", orcadCon)
                TblSql.Clear()
                DsSql.Clear()
                AdapterSql.Fill(DsSql, "orcadw.T_orcadcis")
                TblSql = DsSql.Tables("orcadw.T_orcadcis")
            End Using
            If TblSql.Rows.Count > 0 Then
                For i = 2 To 9
                    GetOrcadSupplier = GetOrcadSupplier & IIf(TblSql.Rows.Item(0)("costruttore" & i).ToString <> "", TblSql.Rows.Item(0)("costruttore" & i).ToString & "[" & TblSql.Rows.Item(0)("orderingcode" & i).ToString & "];", "")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Function

    Function ReplaceChar(ByVal s As String) As String
        ReplaceChar = s
        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 32 Or Asc(Mid(s, i, 1)) = 93 Or Asc(Mid(s, i, 1)) = 91 Or Asc(Mid(s, i, 1)) = 59 Or Asc(Mid(s, i, 1)) = 46 Or Asc(Mid(s, i, 1)) = 37 Then
            Else
                s = Replace(s, Mid(s, i, 1), "-")
            End If
            ReplaceChar = s
        Next
    End Function

    Function OrcadDoc(ByVal bitronPN As String) As String
        Dim rowShow As DataRow(), rowHC As DataRow()
        rowShow = tblDoc.Select("filename like '" & bitronPN & " - *' or filename like '" & bitronPN & "'", "rev DESC")
        If rowShow.Length > 0 And Mid(bitronPN, 1, 2) <> "15" Then
            OrcadDoc = "SRV_DOC - " & rowShow(0)("header").ToString & "_" & rowShow(0)("filename").ToString & "_" & rowShow(0)("rev").ToString & "." & rowShow(0)("extension").ToString
        Else
            rowHC = tblDocComp.Select("codice_bitron = '" & bitronPN & "'", "valido")
            If rowHC.Length = 1 Then
                OrcadDoc = "HC-" & rowHC(0)("cod_comp").ToString
            ElseIf rowHC.Length > 1 Then

                rowHC = tblDocComp.Select("codice_bitron = '" & bitronPN & "' and valido = 'valido'", "valido")
                If rowHC.Length = 1 Then
                    OrcadDoc = "HC-" & rowHC(0)("cod_comp").ToString
                Else
                    MsgBox("HC with two valid sheet! " & rowHC(0)("codice_bitron").ToString)
                    OrcadDoc = "ERROR"
                End If
            Else
                OrcadDoc = "NO"
            End If
        End If
    End Function

    Private Sub Cob_StatusFill()
        Cob_Status.Items.Clear()
        Cob_Status.Items.Add("OPEN")
        Cob_Status.Items.Add("ONGOING")
        Cob_Status.Items.Add("CLOSED")
        Cob_Status.Text = ""
    End Sub

    Private Sub Cob_FilterStatusFill()
        Cob_FilterStatus.Items.Clear()
        Cob_FilterStatus.Items.Add("")
        Cob_FilterStatus.Items.Add("OPEN")
        Cob_FilterStatus.Items.Add("ONGOING")
        Cob_FilterStatus.Items.Add("CLOSED")
        Cob_FilterStatus.Text = ""
    End Sub

    Private Sub DataBangding(ByVal selectrowNo As Integer)
        Dim objCurrencyManager As CurrencyManager
        objCurrencyManager = CType(Me.BindingContext(tblNPI), CurrencyManager)
        Txt_Index.DataBindings.Clear()
        Txt_BitronPN.DataBindings.Clear()
        Txt_description.DataBindings.Clear()
        Cob_Owner.DataBindings.Clear()
        Txt_Area.DataBindings.Clear()
        Cob_Status.DataBindings.Clear()
        DTP_Date.DataBindings.Clear()
        DTP_PlanCloseDate.DataBindings.Clear()
        Txt_IssueDescription.DataBindings.Clear()
        Txt_TempCorrectAction.DataBindings.Clear()
        Txt_FinalCorrectAction.DataBindings.Clear()
        Txt_FilePath.DataBindings.Clear()
        DGV_NPI.Update()
        objCurrencyManager.Position = selectrowNo
        Txt_Index.DataBindings.Add("Text", tblNPI, "ID")
        Txt_BitronPN.DataBindings.Add("Text", tblNPI, "Bitron_PN")
        Txt_description.DataBindings.Add("Text", tblNPI, "BS")
        Txt_IssueDescription.DataBindings.Add("Text", tblNPI, "Issue_description")
        Txt_Area.DataBindings.Add("Text", tblNPI, "Area")
        Cob_Status.DataBindings.Add("Text", tblNPI, "Status")
        Txt_TempCorrectAction.DataBindings.Add("Text", tblNPI, "Temp_corr_action")
        Txt_FinalCorrectAction.DataBindings.Add("Text", tblNPI, "Final_corr_action")
        Cob_Owner.DataBindings.Add("Text", tblNPI, "Owner")
        Txt_FilePath.DataBindings.Add("Text", tblNPI, "FilePath")
    End Sub

    Private Sub Btn_Add_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Add.Click
        If Trim(Txt_BitronPN.Text) <> "" Then
            Try

                DateStart = DTP_Date.Value.Date
                DateClosed = DTP_PlanCloseDate.Value.Date
                Dim Sql As String = "INSERT INTO npi_openissue (BS,DATE,Issue_description,Bitron_PN,Area,Owner,Temp_corr_action,Final_corr_action,ETC,Status,FilePath ) VALUES ('" &
                                    Txt_description.Text & "','" & DateStart.ToString("yyyy-MM-dd") & "','" & Txt_IssueDescription.Text & "','" & Txt_BitronPN.Text & "','" & Txt_Area.Text & "','" &
                                    Cob_Owner.Text & "','" & Txt_TempCorrectAction.Text & "','" & Txt_FinalCorrectAction.Text & "','" & DateClosed.ToString("yyyy-MM-dd") & "','" & Cob_Status.Text & "','" & Txt_FilePath.Text & "');"
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Dim cmd As MySqlCommand = New MySqlCommand(Sql, con)
                    cmd.ExecuteNonQuery()
                    Call issuefunction(0)
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Bitron Product Code can't be empty")
        End If
        CobFilterBitronPNFill()
        DeselectRows()
    End Sub

    Private Sub Btn_Del_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Del.Click
        Dim selectrowNo As Integer = DGV_NPI.CurrentRow.Index
        If MsgBox("Are you sure to delete this issue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Dim sql As String = " DELETE  From npi_openissue WHERE ID = " & Txt_Index.Text

                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    Call issuefunction(selectrowNo - 1)
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
        CobFilterBitronPNFill()
        DeselectRows()
    End Sub

    Private Sub Btn_Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Save.Click
        Dim selectrowNo As Integer = DGV_NPI.CurrentRow.Index
        If Trim(Txt_BitronPN.Text) <> "" Then
            Try
                DateStart = DTP_Date.Value.Date
                DateClosed = DTP_PlanCloseDate.Value.Date

                Dim sql As String = "UPDATE npi_openissue SET BS = '" & Txt_description.Text & "',DATE = '" & DateStart.ToString("yyyy-MM-dd") & "',Issue_description ='" &
                                    Txt_IssueDescription.Text & "',Bitron_PN = '" & Txt_BitronPN.Text & "',Area = '" & Txt_Area.Text & "',Owner = '" & Cob_Owner.Text & "',Temp_corr_action = '" &
                                    Txt_TempCorrectAction.Text & "',Final_corr_action = '" & Txt_FinalCorrectAction.Text & "',ETC = '" & DateClosed.ToString("yyyy-MM-dd") & "',Status = '" &
                                    Cob_Status.Text & "',FilePath ='" & Txt_FilePath.Text & "' WHERE ID = '" & Txt_Index.Text & "'"
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    Call issuefunction(selectrowNo)
                End Using
                MsgBox("Successful update")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Bitron Product Code can't be empty")
        End If
        CobFilterBitronPNFill()
        DeselectRows()
    End Sub

    Private Sub Btn_Search_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Search.Click
        Call DGV_Fill()
        DeselectRows()
    End Sub

    Private Sub DGV_Fill()
        Dim Sql = "SELECT * FROM npi_openissue WHERE ID > 0 "
        If (Cob_FilterOwner.Text <> "") Then
            Sql += "And Owner='" & Cob_FilterOwner.Text & "'"
        End If

        If (Cob_FilterBS.Text <> "") Then
            Sql += "And BS='" & Cob_FilterBS.Text & "'"
        End If

        If Cob_FilterBitronPN.Text <> "" Then
            Sql += "And Bitron_PN='" & Cob_FilterBitronPN.Text & "'"

        End If
        If Cob_FilterStatus.Text <> "" Then
            Sql += "And Status='" & Cob_FilterStatus.Text & "'"
        End If

        Sql += "order by ID desc"
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Try
                DsNPI.Clear()
                tblNPI.Clear()
                Using AdapterNPICob As New MySqlDataAdapter(Sql, con)
                    AdapterNPICob.Fill(DsNPI, "NPI")
                End Using
                tblNPI = DsNPI.Tables("NPI")
                DGV_NPI.DataSource = tblNPI
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
    End Sub

    Public Sub issuefunction(ByVal selectrowNo As Integer)
        DsNPI.Clear()
        tblNPI.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterNPI As New MySqlDataAdapter("SELECT * FROM npi_openissue", con)
                AdapterNPI.Fill(DsNPI, "NPI")
            End Using
        End Using
        tblNPI = DsNPI.Tables("NPI")
        DGV_NPI.DataSource = tblNPI
        If tblNPI.Rows.Count > 0 And selectrowNo > -1 Then

            DGV_NPI.Rows(selectrowNo).Selected = True
            Call DataBangding(selectrowNo)
        End If
    End Sub

    Private Sub DGV_NPI_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles DGV_NPI.MouseDoubleClick
        If DGV_NPI.SelectedRows.Count = 1 Then
            If controlRight("W") >= 1 Then 'BEC: controlRight("R") >= 1
                Dim fileOpen As String
                fileOpen = downloadFileWinPath(Txt_FilePath.Text)
                Application.DoEvents()
                If fileOpen <> "" Then
                    Process.Start(fileOpen)
                    Application.DoEvents()
                End If
            Else
                MsgBox("No enough right to check the file")
            End If
        End If
    End Sub


    Private Sub Btn_UpLoadFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_UpLoadFile.Click
        If DGV_NPI.Rows.Count > 0 Then
            FormNPIDocMamagement.Show()
            FormNPIDocMamagement.Focus()
        Else
            MsgBox("Create an open issue and then link a document!")
        End If
    End Sub

    Sub FillCobFilterContent()
        Cob_FilterOwner.Items.Clear()
        Cob_FilterOwner.Items.Add("")

        For Each row In tblCredentials.Rows
            Cob_FilterOwner.Items.Add(UCase(row("username").ToString))
        Next
        Cob_FilterOwner.Sorted = True
    End Sub

    Sub FillCobOwnerContent()
        Cob_Owner.Items.Clear()
        Cob_Owner.Items.Add("")
        For Each row In tblCredentials.Rows
            Cob_Owner.Items.Add(UCase(row("username").ToString))
        Next
        Cob_Owner.Sorted = True
    End Sub


    Function downloadFileWinPath(ByVal fileName As String) As String
        Dim objFtp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""

        If fileName <> "" Then
            Try
                Dim strPathFtp As String = Mid(fileName, 1, 3) & "/" & Mid(fileName, 1, 11) & "/"  '"/"("65R/65R_PRO_ECR/")

                objFtp.DownloadFile(strPathFtp, Path.GetTempPath, fileName) ' download successfull
                downloadFileWinPath = Path.GetTempPath & fileName
            Catch ex As Exception

            End Try
        Else
            MsgBox("FilePath does not exist")
        End If
    End Function

    Private Sub CobFilterBitronPNFill()
        Cob_FilterBitronPN.Items.Clear()
        Cob_FilterBitronPN.Items.Add("")

        DsNPI.Clear()
        tblNPI.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterNPI As New MySqlDataAdapter("SELECT * FROM npi_openissue", con)
                AdapterNPI.Fill(DsNPI, "NPI")
            End Using
        End Using
        tblNPI = DsNPI.Tables("NPI")

        Dim rowResults As DataRow() = tblNPI.Select()
        For Each row In rowResults
            If Cob_FilterBitronPN.Items.Contains(UCase(row("Bitron_PN").ToString)) = False Then Cob_FilterBitronPN.Items.Add(UCase(row("Bitron_PN").ToString))
        Next
        Cob_FilterBitronPN.Sorted = True
    End Sub

    Private Sub CobFilterBSFill()
        Cob_FilterBS.Items.Clear()
        Cob_FilterBS.Items.Add("")
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterNPI As New MySqlDataAdapter("SELECT * FROM npi_openissue", con)
                AdapterNPI.Fill(DsNPI, "NPI")
            End Using
        End Using
        tblNPI = DsNPI.Tables("NPI")

        Dim rowResults As DataRow() = tblNPI.Select()
        For Each row In rowResults
            If Cob_FilterBS.Items.Contains(UCase(row("BS").ToString)) = False Then Cob_FilterBS.Items.Add(UCase(row("BS").ToString))
        Next
        Cob_FilterBS.Sorted = True
    End Sub

    Private Sub DTP_Date_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DTP_Date.ValueChanged
        DateStart = DTP_Date.Value.Date
    End Sub

    Private Sub CheckBoxOpenProduct_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOpenProduct.CheckedChanged
        If CheckBoxOpenProduct.Checked = True Then
            CheckBoxClosed.Checked = False
        End If
    End Sub

    Private Function IsNeedUpdate(ByVal id As String) As Boolean
        Dim update As Boolean = False
        Dim dt As DataTable
        Dim rowDB As DataRow
        Dim rowId As Integer
        Try
            Dim Sql = "SELECT * FROM npi_openissue WHERE ID = " & id
            Dim dtSet As New DataSet
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            dtSet.Clear()
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterNPICob As New MySqlDataAdapter(Sql, con)
                    AdapterNPICob.Fill(dtSet, "NPI")
                End Using
            End Using
            dt = dtSet.Tables("NPI")
            rowDB = dt.Select("id = " & id & "").FirstOrDefault()

            For Each xRow As DataGridViewRow In DGV_NPI.Rows
                If xRow.Cells("ID").Value = id Then
                    rowId = xRow.Index
                    Exit For
                End If
            Next
            If DGV_NPI.Rows(rowId).Cells("ID").Value.ToString() = rowDB("ID").ToString() Then
                If DGV_NPI.Rows(rowId).Cells("BS").Value = rowDB("BS").ToString() And
                    DGV_NPI.Rows(rowId).Cells("StartDate").Value = rowDB("DATE") And
                    DGV_NPI.Rows(rowId).Cells("IssueDescription").Value = rowDB("Issue_description").ToString() And
                    DGV_NPI.Rows(rowId).Cells("BitronPN").Value = rowDB("Bitron_PN").ToString() And
                    DGV_NPI.Rows(rowId).Cells("Area").Value = rowDB("Area").ToString() And
                    DGV_NPI.Rows(rowId).Cells("Owner").Value = rowDB("Owner").ToString() And
                    DGV_NPI.Rows(rowId).Cells("TEMPCorrectAction").Value = rowDB("Temp_corr_action").ToString() And
                    DGV_NPI.Rows(rowId).Cells("FinalCorrectAction").Value = rowDB("Final_corr_action").ToString() And
                    DGV_NPI.Rows(rowId).Cells("PlanedClosedDate").Value = rowDB("ETC") And
                    DGV_NPI.Rows(rowId).Cells("Status").Value = rowDB("Status").ToString() And
                    DGV_NPI.Rows(rowId).Cells("FilePath").Value = rowDB("FilePath").ToString() Then
                    update = False
                Else
                    update = True
                End If
            End If
        Catch ex As Exception
        End Try
        Return update
    End Function


    Sub SaveUpdates(ByVal cSelectedID As String)
        Dim rowId As Integer
        For Each xRow As DataGridViewRow In DGV_NPI.Rows
            If xRow.Cells("ID").Value = cSelectedID Then
                rowId = xRow.Index
                Exit For
            End If
        Next
        If Trim(DGV_NPI.Rows(rowId).Cells("BitronPN").Value) <> "" Then
            Try
                Dim startDate = DGV_NPI.Rows(rowId).Cells("StartDate").Value
                Dim etc = DGV_NPI.Rows(rowId).Cells("PlanedClosedDate").Value
                Dim sql As String = "UPDATE npi_openissue SET BS = '" & DGV_NPI.Rows(rowId).Cells("BS").Value &
                                    "',DATE = '" & startDate.Year & "-" & startDate.Month & "-" & startDate.Day &
                                    "',Issue_description ='" & DGV_NPI.Rows(rowId).Cells("IssueDescription").Value &
                                    "',Bitron_PN = '" & DGV_NPI.Rows(rowId).Cells("BitronPN").Value &
                                    "',Area = '" & DGV_NPI.Rows(rowId).Cells("Area").Value &
                                    "',Owner = '" & Cob_Owner.Text &
                                    "',Temp_corr_action = '" & DGV_NPI.Rows(rowId).Cells("TEMPCorrectAction").Value &
                                    "',Final_corr_action = '" & DGV_NPI.Rows(rowId).Cells("FinalCorrectAction").Value &
                                    "',ETC = '" & etc.Year & "-" & etc.Month & "-" & etc.Day &
                                    "',Status = '" & DGV_NPI.Rows(rowId).Cells("Status").Value &
                                    "',FilePath ='" & DGV_NPI.Rows(rowId).Cells("FilePath").Value &
                                    "' WHERE ID = '" & cSelectedID & "'"
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Dim cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                End Using

                MsgBox("Successful update")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Bitron Product Code can't be empty")
        End If
    End Sub

    Dim saveUpdate As Boolean = True

    Private Sub ClearDataBindings()
        Txt_Index.DataBindings.Clear()
        Txt_BitronPN.DataBindings.Clear()
        Txt_description.DataBindings.Clear()
        Cob_Owner.DataBindings.Clear()
        Txt_Area.DataBindings.Clear()
        Cob_Status.DataBindings.Clear()
        DTP_Date.DataBindings.Clear()
        DTP_PlanCloseDate.DataBindings.Clear()
        Txt_IssueDescription.DataBindings.Clear()
        Txt_TempCorrectAction.DataBindings.Clear()
        Txt_FinalCorrectAction.DataBindings.Clear()
        Txt_FilePath.DataBindings.Clear()

        Txt_Index.Text = ""
        Txt_BitronPN.Text = ""
        Txt_description.Text = ""
        Cob_Owner.Text = ""
        Txt_Area.Text = ""
        Cob_Status.Text = ""
        DTP_Date.Text = ""
        DTP_PlanCloseDate.Text = ""
        Txt_IssueDescription.Text = ""
        Txt_TempCorrectAction.Text = ""
        Txt_FinalCorrectAction.Text = ""
        Txt_FilePath.Text = ""

        Txt_Area.SelectedIndex = 0
        Cob_Status.SelectedIndex = 0
    End Sub

    Dim selectedIndex As Integer = 0

    Private Sub DeselectRows()
        selectedIndex = -1
        DGV_NPI.ClearSelection()
        ClearDataBindings()
        Btn_Del.Enabled = False
        Btn_Save.Enabled = False
        Btn_UpLoadFile.Enabled = False
    End Sub

    Private Sub SelectRow()
        Dim newSelectedId As String
        If cSelectedID = Nothing Then
            cSelectedID = Me.DGV_NPI.Item(DGV_NPI.Columns("ID").Index, DGV_NPI.SelectedRows(0).Index).Value.ToString()
        End If
        selectedIndex = DGV_NPI.SelectedRows(0).Index
        DataBangding(selectedIndex)

        Btn_Del.Enabled = True
        Btn_Save.Enabled = True
        Btn_UpLoadFile.Enabled = True

        newSelectedId = Me.DGV_NPI.Item(DGV_NPI.Columns("ID").Index, selectedIndex).Value
        If newSelectedId <> cSelectedID And IsNeedUpdate(cSelectedID) Then
            Dim msgBoxResult As MsgBoxResult
            msgBoxResult = MsgBox("Do you want to save the changes?", vbYesNo)
            If msgBoxResult = MsgBoxResult.Yes Then
                SaveUpdates(cSelectedID)
            ElseIf msgBoxResult = MsgBoxResult.No Then
                CobFilterBitronPNFill()
                newSelectedId = Me.DGV_NPI.Item(DGV_NPI.Columns("ID").Index, Me.DGV_NPI.CurrentRow.Index).Value.ToString()
            End If
        End If
        cSelectedID = newSelectedId
    End Sub

    Private Sub DGV_NPI_MouseUp(sender As Object, e As MouseEventArgs) Handles DGV_NPI.MouseUp
        Try
            Dim hitInfo As DataGridView.HitTestInfo = DGV_NPI.HitTest(e.X, e.Y)
            'Click to column Header 
            If hitInfo.RowIndex = -1 Then
                Return
            End If
            If selectedIndex = DGV_NPI.SelectedCells(0).RowIndex And selectedIndex <> -1 Then
                If DGV_NPI.Rows(selectedIndex).Selected = True Then
                    DeselectRows()
                End If
            Else
                SelectRow()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGV_NPI_Sorted(sender As Object, e As EventArgs) Handles DGV_NPI.Sorted
        If selectedIndex = -1 Then
            DeselectRows()
            Return
        End If
    End Sub

    Private Sub DGV_NPI_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV_NPI.KeyDown
        If (e.KeyCode.Equals(Keys.Up)) Then
            moveUp()
        ElseIf (e.KeyCode.Equals(Keys.Down)) Then
            moveDown()
        End If
        e.Handled = True
    End Sub

    Private Sub moveUp()
        If (DGV_NPI.RowCount > 0) Then
            If (DGV_NPI.SelectedRows.Count > 0) Then
                Dim rowCount = DGV_NPI.Rows.Count
                Dim index = DGV_NPI.SelectedRows(0).Index

                If (index = 0) Then
                    Return
                End If
                Dim rows As DataGridViewRowCollection = DGV_NPI.Rows

                DGV_NPI.Rows(index - 1).Selected = True
                SelectRow()
            End If
        End If
    End Sub

    Private Sub moveDown()
        If (DGV_NPI.RowCount > 0) Then
            If (DGV_NPI.SelectedRows.Count > 0) Then
                Dim rowCount = DGV_NPI.Rows.Count
                Dim index = DGV_NPI.SelectedRows(0).Index

                If (index = (rowCount - 1)) Then
                    Return
                End If
                Dim rows As DataGridViewRowCollection = DGV_NPI.Rows

                DGV_NPI.Rows(index + 1).Selected = True
                SelectRow()
            End If
        End If
    End Sub

    Private Sub DTP_PlanCloseDate_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DTP_PlanCloseDate.ValueChanged
        DateClosed = DTP_PlanCloseDate.Value.Date
    End Sub
End Class