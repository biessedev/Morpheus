Option Explicit On

Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class FormSamples

    Dim DsDocComp As New DataSet
    Dim tblDocComp As New DataTable
    Dim index As Long = 1
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product order by customer, statusActivity, etd", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet
    Dim currentActivityID As Integer = -1
    Dim currentProductCode As String
    Dim XmlTree As New TreeViewToFromXml
    Dim OpenSession As Boolean
    Dim AdapterBomOff As New MySqlDataAdapter("SELECT * FROM Bomoffer", MySqlconnection)
    Dim tblBomOff As DataTable
    Dim DsBomOff As New DataSet
    Dim AdapterSigip As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
    Dim tblSigip As DataTable
    Dim DsSigip As New DataSet
    Dim AdapterOff As New MySqlDataAdapter("SELECT * FROM offer", MySqlconnection)
    Dim tblOff As DataTable
    Dim DsOff As New DataSet
    Dim AdapterPfp As New MySqlDataAdapter("SELECT * FROM Pfp", MySqlconnection)
    Dim tblPfp As DataTable
    Dim DsPfp As New DataSet
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet

    Dim AdapterNPI As New MySqlDataAdapter("SELECT * FROM npi_openissue", MySqlconnection)
    Dim tblNPI As New DataTable
    Dim DsNPI As New DataSet

    Dim AdapterTP As New MySqlDataAdapter("SELECT * FROM TimeProject", MySqlconnection)
    Dim tblTP As DataTable
    Dim DsTP As New DataSet


    Dim AdapterCredentials As New MySqlDataAdapter("SELECT * FROM credentials", MySqlconnection)
    Dim tblCredentials As DataTable
    Dim DsCredentials As New DataSet


    Dim ConnectionStringOrcad As String
    Dim SqlconnectionOrcad As New SqlConnection
    Dim info As String
    Dim AdapterSql As SqlDataAdapter
    Dim TblSql As New DataTable
    Dim DsSql As New DataSet
    Dim RdaInfo As String, OrderInfo As String

    Dim DateStart As New Date
    Dim DateClosed As New Date


    Private Sub FormSamples_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If InStr(TabControlNPI.SelectedTab.Text, "Task") > 0 Then

            If currentActivityID > 0 Then
                If OpenSession Then
                    If vbYes = MsgBox("Session open! Do you want to save?", MsgBoxStyle.YesNo) Then
                        ButtonSave_Click(Me, e)
                    Else
                        Dim tblProd As DataTable
                        Dim DsProd As New DataSet
                        Dim rowShow As DataRow()

                        AdapterProd.Fill(DsProd, "Product")
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
                        AdapterProd.Fill(DsProd, "Product")
                        tblProd = DsProd.Tables("Product")
                        rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
                        For Each row In rowShow
                            session("Product", row("Id").ToString, False)
                        Next
                    End If

                End If
            End If
        End If

        AdapterOff.Fill(DsBomOff, "Offer")
        tblOff = DsOff.Tables("Offer")

        AdapterSigip.Fill(DsSigip, "sigip")
        tblSigip = DsSigip.Tables("sigip")

        TreeViewTask.HideSelection = False
        TreeViewActivity.HideSelection = False

        FormNPIDocMamagement.Close()

    End Sub

    Private Sub FormSamples_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        AdapterCredentials.Fill(DsCredentials, "Credentials")
        tblCredentials = DsCredentials.Tables("credentials")

        Try
            AdapterProd.Fill(DsProd, "Product")
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

            AdapterPfp.Fill(DsPfp, "pfp")
            tblPfp = DsPfp.Tables("pfp")

            AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
            Try
                DsDoc.Clear()
                tblDoc.Clear()
            Catch ex As Exception

            End Try

            AdapterDoc.Fill(DsDoc, "doc")
            tblDoc = DsDoc.Tables("doc")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

        Me.DTP_Date.CustomFormat = "yyyy-MM-dd"
        DTP_Date.Format = DateTimePickerFormat.Custom

        Me.DTP_PlanCloseDate.CustomFormat = "yyyy-MM-dd"
        DTP_PlanCloseDate.Format = DateTimePickerFormat.Custom
        Try

            Call issuefunction(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    ' update the tree viewer
    Sub UpdateTreeSample()
        TreeViewActivity.BeginUpdate()
        TreeViewActivity.Font = New Font("Courier New", 12, FontStyle.Regular)
        TreeViewActivity.Nodes.Clear()
        TreeViewActivity.BackColor = Color.White
        Dim rootNode As TreeNode, customer As String, activity As Integer
        Dim rootChildren1 As TreeNode, rootChildren2 As TreeNode
        Dim rowShow As DataRow(), currentColor As Color
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        If Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " ")) = "" Then
            activity = 0
        ElseIf Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))) = "0" Then
            activity = 0
        Else
            activity = Int(Trim(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))))
        End If
        rowShow = tblProd.Select(IIf(CheckBoxOpenProduct.Checked, "((status='SOP_SAMPLE') or (statusActivity='OPEN')) ", "status LIKE '*'") &
                  IIf(ComboBoxActivityID.Text <> "", " AND idactivity = " & activity, "") &
                  IIf(ComboBoxActivityStatus.Text <> "", " AND (statusActivity='" & ComboBoxActivityStatus.Text & "') ", IIf(CheckBoxClosed.Checked, " AND statusActivity LIKE '*'", " AND statusActivity <> 'CLOSED'")),
                  IIf(CheckBoxOrderByDate.Checked, " etd desc, customer, idActivity ", IIf(CheckBoxCustomer.Checked = True, "customer, idActivity ,etd", "idActivity,customer  ,etd")))
        customer = ""
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
                    currentColor = Color.Black
                    rootChildren1 = New TreeNode("<> " & row("idactivity").ToString & " -- " & row("statusactivity").ToString & Mid("_______", 1, 7 - Len(row("statusactivity").ToString)) & " -- " & IIf(row("idactivity").ToString <> 0, row("NameActivity").ToString, "NOT ASSIGNED"))
                    rootChildren1.NodeFont = New Font("Courier New", 12, FontStyle.Italic)



                    If row("statusactivity").ToString = "CLOSED" Then

                        rootChildren1.BackColor = Color.Gray
                        currentColor = Color.Gray
                    End If


                    If row("statusactivity").ToString = "STANDBY" Then
                        rootChildren1.BackColor = Color.LightBlue
                        currentColor = Color.LightBlue
                    End If

                    If row("statusactivity").ToString = "SENT" Then
                        rootChildren1.BackColor = Color.LimeGreen
                        currentColor = Color.LimeGreen
                    End If

                    If row("statusactivity").ToString = "" Then
                        rootChildren1.BackColor = Color.LightGray
                        currentColor = Color.LightGray
                    End If

                    If Not CheckBoxCustomer.Checked Then
                        TreeViewActivity.Nodes.Add(rootChildren1)
                    Else
                        rootNode.Nodes.Add(rootChildren1)
                    End If

                    activity = Val(row("idactivity").ToString)
                End If

                rootChildren2 = New TreeNode(row("etd").ToString & Mid("__________", 1, 10 - Len(row("etd"))) & " -- " & row("npieces") & Mid("___________", 1, 6 - Len(Str(row("npieces")))) & " pcs -- [" & row("bitronpn") & "]  " & row("name"))

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
                    'rootChildren2.ForeColor = currentColor
                End If
                rootChildren1.Nodes.Add(rootChildren2)
            End If
            If CheckBoxCustomer.Checked Then rootNode.Expand()
        Next
        TextBoxProductStatus.Text = ""
        TextBoxProduct.Text = ""
        TextBoxProductQt.Text = ""
        TextBoxETD.Text = ""
        ' TreeViewActivity.SelectedNode = TreeViewActivity.Nodes.Item(0)


        'SaveFileDialog1.ShowDialog()
        'Dim path As String = SaveFileDialog1.FileName
        'WriteTxtFile(path, "Export " & Now, False)

        'Dim XmlTree As New TreeViewToFromXml, s As String
        'XmlTree.SetTreeView(TreeViewActivity)
        's = XmlTree.ExportToString()
        'TreeViewActivity.Nodes.Clear()
        'XmlTree.Import(s)

        'SaveTree(TreeViewActivity.Nodes.Item(0), path)
        'rootNode.StateImageKey = 1
        'rootNode = TreeViewActivity.Nodes.Item(0)
        'TreeViewActivity.Nodes.Clear()
        'For Each node In rootNode.Nodes
        '    TreeViewActivity.Nodes.Add(node)
        'Next
        TreeViewActivity.EndUpdate()
    End Sub

    Private Sub CheckBoxOrderActivity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UpdateTreeSample()
    End Sub

    Private Sub TreeViewActivity_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewActivity.AfterSelect
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
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")

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
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        ComboBoxBomLocation.Items.Clear()
        ComboBoxBomLocation.Items.Add("")
        If currentProductCode <> "" Then
            rowShow = tblProd.Select("bitronpn='" & mycurrentProductCode & "'", "etd desc")
            ComboBoxBomLocation.Items.Add("SIGIP")
        End If

        'Dim DsBomOff As New DataSet
        ' Dim tblBomOff As DataTable
        'AdapterBomOff.Fill(DsBomOff, "BomOffer")
        ' tblBomOff = DsBomOff.Tables("BomOffer")
        ' rowShow = tblBomOff.Select("var1 = '" & mycurrentProductCode & "' or " & _
        '             "var2 = '" & mycurrentProductCode & "' or " & _
        '        "var4 = '" & mycurrentProductCode & "' or " & _
        '         "var3 = '" & mycurrentProductCode & "' or " & _
        '          "var5 = '" & mycurrentProductCode & "' or " & _
        '          "var6 = '" & mycurrentProductCode & "'")
        ' If rowShow.Length = 1 Then
        'ComboBoxBomLocation.Items.Add("OFFER")
        ' ElseIf rowShow.Length > 1 Then
        ' MsgBox("Error in Offer bom association, more of one bom with p/n:" & mycurrentProductCode)
        ' End If
    End Sub

    Sub UpdateActivityID()
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        ComboBoxActivityID.Items.Clear()
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select(IIf(CheckBoxClosed.Checked = False, "NOT statusActivity = 'CLOSED'", "bitronpn like '*'"), "idactivity")
        ComboBoxActivityID.Items.Add("")
        ComboBoxActivityID.Items.Add("0 -- NOT ASSIGNED")
        For Each row In rowShow
            If (Val(row("idactivity")) <> activityid) Then
                If Val(row("idactivity")) > 0 Then ComboBoxActivityID.Items.Add(row("idactivity") & " -- " & row("NameActivity"))
                activityid = row("idactivity")
            End If
        Next
    End Sub

    Private Sub DateTimePickerETD_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerETD.CloseUp
        TextBoxETD.Text = DateTimePickerETD.Text
    End Sub

    Private Sub TextBoxETD_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBoxETD.MouseDoubleClick
        TextBoxETD.Text = ""
    End Sub

    Private Sub Buttonrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buttonrefresh.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        UpdateTreeSample()
    End Sub

    Private Sub ButtonLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLink.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet, canDelete As Boolean = False
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        If TextBoxProduct.Text <> "" And ComboBoxActivityID.Text <> "" And Len(TextBoxProductQt.Text) <= 6 Then
            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select("bitronpn='" & currentProductCode & "'")
            If rowShow.Length = 1 Then
                ComboBoxActivityStatus.Text = rowShow(0).Item("Statusactivity")
                Dim cmd As New MySqlCommand()
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
                        cmd = New MySqlCommand(sql, MySqlconnection)
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

        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        UpdateTreeSample()


    End Sub

    Private Sub TextBoxProduct_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxProduct.TextChanged
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

    Private Sub ComboBoxActivityID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxActivityID.TextChanged


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

    Private Sub ButtonCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCollapse.Click
        TreeViewActivity.CollapseAll()
    End Sub

    Private Sub ButtonUncollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUncollapse.Click
        TreeViewActivity.ExpandAll()
    End Sub

    Private Sub ButtonUpdateStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdateStatus.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        If ComboBoxActivityID.Text <> "" And ComboBoxActivityStatus.Text <> "" Then
            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & Val(Mid(ComboBoxActivityID.Text, 1, InStr(ComboBoxActivityID.Text, " "))) & "")
            For Each row In rowShow
                If Val(row("idactivity")) <> 0 Then
                    Dim cmd As New MySqlCommand()
                    Dim sql As String

                    Try
                        sql = "UPDATE `" & DBName & "`.`product` SET " &
                        "`Statusactivity` = '" & ComboBoxActivityStatus.Text &
                        "', `delay` = '" & IIf(ComboBoxActivityStatus.Text = "CLOSED", InputBox("Insert the closing activity delay (day):"), "") &
                        "' WHERE `product`.`bitronpn` = '" & row("bitronpn") & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
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

    End Sub

    '' replace the not ascii char
    'Function ReplaceChar(ByVal s As String) As String
    '    ReplaceChar = s
    '    For i = 1 To Len(s)
    '        If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
    '         Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
    '         Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 32 Or Asc(Mid(s, i, 1)) = 45 Or Asc(Mid(s, i, 1)) = 95 Then
    '        Else
    '            s = Replace(s, Mid(s, i, 1), "-")
    '        End If
    '        ReplaceChar = s
    '    Next

    'End Function

    Private Sub ButtonNewCommit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewCommit.Click

        Dim DsProd As New DataSet
        Dim strActiv As String
        Dim activityid As Integer = 0
        Dim cmd As New MySqlCommand()
        Dim sql As String
        If TextBoxProduct.Text <> "" And productActivity(currentProductCode) = 0 Then

            strActiv = InputBox("Please insert the name of new activity : " & vbCrLf & "PCB_1 -- PCB_ 2 -- Activity Description")

            If Regex.IsMatch(strActiv, "^[0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Or
                Regex.IsMatch(strActiv, "^[0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- [0-9]{8} -- \w+$", RegexOptions.IgnoreCase) Then

                Try
                    sql = "UPDATE `" & DBName & "`.`product` SET " &
                    "`Statusactivity` = 'OPEN'" &
                    ", `idactivity` = " & LastIDActivity() + 1 &
                    ", `nameactivity` = '" & Trim(ReplaceChar(UCase(strActiv))) &
                    "' WHERE `product`.`bitronpn` = '" & currentProductCode & "' ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
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

        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select("bitronpn like '*' ")
        For Each row In rowShow
            If row("idactivity") > activityid Then
                activityid = row("idactivity")
            End If
        Next

        LastIDActivity = activityid
    End Function

    Function NumberProduct(ByVal idactivity As Integer) As Integer
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select("idactivity = " & idactivity)
        NumberProduct = rowShow.Length
    End Function

    ' productActivity
    Function productActivity(ByVal productpn As String) As Integer
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select("bitronpn = " & productpn)
        If rowShow.Length > 0 Then
            productActivity = rowShow(0).Item("idactivity").ToString
        Else
            productActivity = 0
        End If
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFolder.Click
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
                '   Process.Start("explorer.exe", ParameterTable("NPI_SHARE_DIR"))

            Catch ex As Exception
                MsgBox("Directory creation error!" & ex.ToString)
            End Try

        End If


    End Sub
    ' Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFolder.Click
    '    If ComboBoxActivityID.Text <> "" And Mid(ComboBoxActivityID.Text, 1, 1) <> "0" Then
    '       Try
    '          If Directory.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text) Then

    '            Else
    '               MkDir(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)
    '          End If


    '            If Directory.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text) Then

    '           Else
    '              MkDir(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)
    '         End If


    '            If File.Exists(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text & "\" & ParameterTable("plant") & "R_PRO_ASR_" & ComboBoxActivityID.Text & "_0.xlsx") Then

    '           Else
    '             File.Copy(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ParameterTable("PathFileASR") & ParameterTable("FileASR"), ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text & "\" & ParameterTable("plant") & "R_PRO_ASR_" & ComboBoxActivityID.Text & "_0.xlsx")
    '          End If

    '        Process.Start("explorer.exe", ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("PathActivityDoc") & ComboBoxActivityID.Text)

    '      Catch ex As Exception
    '           MsgBox("Directory search creation error!" & ex.ToString)
    '      End Try

    '    End If

    '    End Sub

    Function ActivityStatus(ByVal id As Integer) As String
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select("idactivity=" & id & "")
        ActivityStatus = ""
        If id Then
            If rowShow.Length Then ActivityStatus = rowShow(0).Item("Statusactivity").ToString
        End If

    End Function

    Private Sub ComboBoxActivityStatus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxActivityStatus.TextChanged
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

    Private Sub TabControlNPI_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
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

            If currentActivityID > 0 Then
                If OpenSession Then
                    If vbYes = MsgBox("Session open! Do you want to save?", MsgBoxStyle.YesNo) Then
                        ButtonSave_Click(Me, e)
                    Else
                        Dim tblProd As DataTable
                        Dim DsProd As New DataSet
                        Dim rowShow As DataRow()

                        AdapterProd.Fill(DsProd, "Product")
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
                        AdapterProd.Fill(DsProd, "Product")
                        tblProd = DsProd.Tables("Product")
                        rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
                        For Each row In rowShow
                            session("Product", row("Id").ToString, False)
                        Next
                    End If

                End If
            End If

            If InStr(TabControlNPI.SelectedTab.Text, "OpenIssue") > 0 Then
                'If controlRight("R") < 3 Then
                If controlRight("W") >= 2 Then
                    Btn_Add.Enabled = True
                    Btn_Del.Enabled = True
                    Btn_Save.Enabled = True
                    Btn_UpLoadFile.Enabled = True

                End If

            End If


        End If
    End Sub

    Sub UpdateTreeTask()
        Dim tblProd As DataTable
        Dim DsProd As New DataSet, rootNode As New TreeNode
        Dim rowShow As DataRow()
        Dim activityid As Integer = 0
        If currentActivityID > 0 Then

            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "", ComboBoxType.Text & " desc")
            If rowShow.Length > 0 Then
                If rowShow(0).Item(ComboBoxType.Text).ToString <> "" Then
                    XmlTree.Import(rowShow(0).Item(ComboBoxType.Text).ToString)
                    rootNode = TreeViewTask.Nodes.Item(0)
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
        'If Mid(mynode.Text, 8, 4) = "note" Then
        '    mynode.BackColor = Color.Yellow
        'End If

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

        Dim activityid As Integer = 0
        If currentActivityID > 0 Then
            Dim cmd As New MySqlCommand()
            Dim sql As String
            Try
                sql = "UPDATE `" & DBName & "`.`product` SET " &
                "`" & ComboBoxType.Text & "` = '" & S &
                "' WHERE `product`.`idactivity` = " & currentActivityID & " ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Mysql update query error!")
            End Try

            MsgBox("Tasks saved!")
            ButtonSave.BackColor = Color.Green
        Else
            MsgBox("Need to fill in activity and status!")
        End If
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim CanSetReset As Boolean

        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
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

    Private Sub ButtonReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReset.Click

        If currentActivityID > 0 Then
            Dim tblProd As DataTable
            Dim DsProd As New DataSet
            Dim rowShow As DataRow()
            Dim CanSet As Boolean

            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
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

    Private Sub ButtonNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNew.Click
        If currentActivityID > 0 Then
            Dim tblProd As DataTable
            Dim DsProd As New DataSet
            Dim rowShow As DataRow()
            Dim CanSet As Boolean

            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
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

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        If currentActivityID > 0 Then
            Dim tblProd As DataTable
            Dim DsProd As New DataSet
            Dim rowShow As DataRow()
            Dim CanSet As Boolean

            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
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

    Private Sub TreeViewTask_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewTask.AfterSelect
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

    Private Sub ButtonUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click
        If Not IsNothing(TreeViewTask.SelectedNode) And currentActivityID > 0 Then
            Dim tblProd As DataTable
            Dim DsProd As New DataSet
            Dim rowShow As DataRow()
            Dim CanSet As Boolean

            AdapterProd.Fill(DsProd, "Product")
            tblProd = DsProd.Tables("Product")
            rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
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

    Private Sub ButtonTaskCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTaskCollapse.Click
        TreeViewTask.CollapseAll()
    End Sub

    Private Sub ButtonExpand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExpand.Click
        TreeViewTask.ExpandAll()
    End Sub

#End Region  ' Task 

    Sub PrintNode(ByVal FileName As String, ByVal node As TreeNode)

        For Each n In node.Nodes
            WriteTxtFile(FileName, n.ToString, True)
            PrintNode(FileName, n)
        Next

    End Sub

    Private Sub ButtonExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExport.Click
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

    Private Sub TimerTask_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerTask.Tick
        If Val(TextBoxBomTime.Text) > 1 Then
            TextBoxBomTime.Text = Val(TextBoxBomTime.Text) - 1
        Else
            OpenSession = False
            TimerTask.Stop()
            ButtonSave.BackColor = Color.Green
            TextBoxBomTime.Text = ""
            Dim tblProd As DataTable
            Dim DsProd As New DataSet
            Dim rowShow As DataRow()
            If currentActivityID > 0 Then
                AdapterProd.Fill(DsProd, "Product")
                tblProd = DsProd.Tables("Product")
                rowShow = tblProd.Select(" idactivity =" & currentActivityID & "")
                For Each row In rowShow
                    session("Product", row("Id").ToString, False)
                Next
                UpdateTreeTask()
            End If
            MsgBox("Session expired!")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBoxETD.Text = ""
    End Sub

    Private Sub Update_Pfp()
        Dim sql As String
        Dim commandMySql As MySqlCommand
        CollectProcess()
        'open Parti_Fornitori_Prezzi.xls
        Dim xlsApp As New Object
        xlsApp = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelPfp"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")
        'empty the PFP table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`pfp`", MySqlconnection)
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
        sql = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `pfp` fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`pfidf`,`pepre`,`peval`,`pfpaf`,`pfpan`,`pfpad`,`pelot`,`pedin`,`pedfi`,`pefor`,`forsc`)"
        commandMySql = New MySqlCommand(sql, MySqlconnection)
        commandMySql.ExecuteNonQuery()
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

    Private Sub ButtonUpdateMagBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdateMagBox.Click

        Dim tblProd As DataTable
        Dim DsProd As New DataSet
        Dim rowShow As DataRow()
        Dim rowShowSigip As DataRow()
        Dim BomName As String = "", verN As Integer = 0
        Dim sql As String
        Dim i As Integer = 0
        Dim commandMySql As New MySqlCommand
        Dim adapterMySql As New MySqlDataAdapter
        ButtonUpdateMagBox.Text = "Wait........."
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import Rda....."
        Application.DoEvents()
        Import_Rda()
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import Order....."
        Application.DoEvents()
        Import_Order()
        Application.DoEvents()
        ButtonUpdateMagBox.Text = "Import Warehouse stock....."
        Application.DoEvents()
        Import_WH_Stock()
        ButtonUpdateMagBox.Text = "Import PFP ....."
        Application.DoEvents()
        Update_Pfp()
        ButtonUpdateMagBox.Text = "Make Table ....."
        Application.DoEvents()

        If IsNothing(tblSigip) Then
            AdapterSigip.Fill(DsSigip, "sigip")
            tblSigip = DsSigip.Tables("sigip")
        End If

        If IsNothing(tblOff) Then
            AdapterOff.Fill(DsOff, "offer")
            tblOff = DsOff.Tables("offer")
        End If

        AdapterProd.Fill(DsProd, "Product")
        tblProd = DsProd.Tables("Product")
        rowShow = tblProd.Select(" statusactivity = 'OPEN'")


        Dim tblMySql As New DataTable
        Dim dsMySql As New DataSet


        sql = "SELECT * FROM `materialrequest` "
        adapterMySql = New MySqlDataAdapter(sql, MySqlconnection)
        adapterMySql.Fill(dsMySql, "materialrequest")
        tblMySql = dsMySql.Tables("materialrequest")
        ButtonUpdateMagBox.Text = "Deleting data and shift"
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
                commandMySql = New MySqlCommand(sql, MySqlconnection)
                commandMySql.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Error in DB, reset material request")
            End Try

        Next
        ButtonUpdateMagBox.Text = "Deleting data and shift"
        Application.DoEvents()
        Try
            OpenConnectionSqlOrcad("10.10.10.36", "Orcad1", "orcadw", "orcadw")
            'OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
        Catch ex As Exception
            CloseConnectionSqlOrcad()
            OpenConnectionSqlOrcad("10.10.10.36", "Orcad1", "orcadw", "orcadw")
            'OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
        End Try

        ButtonUpdateMagBox.Text = "Load Orcad data"
        Application.DoEvents()
        Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') ", SqlconnectionOrcad)
        tblDocComp.Clear()
        DsDocComp.Clear()
        AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
        tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        Try
            DsDoc.Clear()
            tblDoc.Clear()
        Catch ex As Exception

        End Try

        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        ButtonUpdateMagBox.Text = "Start calculation.."
        Application.DoEvents()

        For Each row In rowShow
            i = i + 1
            If Val(row("NPIECES").ToString) > 0 Then
                If row("bomlocation").ToString = "SIGIP" Then
                    rowShowSigip = tblSigip.Select("bom ='" & row("bitronpn").ToString & "' and (acq_fab = 'acq' Or acq_fab = 'acv')")
                    If rowShowSigip.Length = 0 Then MsgBox("Bom not found in SIGIP!" & row("bitronpn").ToString & BomName)
                    For Each rowSigip In rowShowSigip
                        ButtonUpdateMagBox.Text = "Udpate : " & Math.Round(100 * i / rowShow.Length, 0) & "%"
                        Application.DoEvents()
                        If Val(rowSigip("qt").ToString) * Val(row("npieces").ToString) > 0 Then AddRequest(rowSigip("bitron_pn").ToString, rowSigip("des_pn").ToString, rowSigip("qt").ToString, row("npieces").ToString, rowSigip("bom").ToString, rowSigip("bom").ToString & " - " & rowSigip("des_bom").ToString, , , rowSigip("doc").ToString)
                    Next

                    'ElseIf row("bomlocation").ToString = "OFFER" Then
                    '    If versionOffer(row("bitronpn").ToString, BomName, verN) = 1 Then
                    '        rowShowOffer = tblOff.Select("name ='" & BomName & "'")
                    '        If rowShowOffer.Length = 0 Then MsgBox("Bom not found in OFFER!" & row("bitronpn").ToString & BomName)
                    '        For Each rowOffer In rowShowOffer
                    '            Dim BitronPNCode As String
                    '            If rowOffer("bitronpn").ToString <> "" Then
                    '                BitronPNCode = rowOffer("bitronpn").ToString
                    '            Else
                    '                BitronPNCode = "Q_" & rowOffer("name").ToString & "_" & rowOffer("id").ToString
                    '            End If

                    '            ButtonUpdateMagBox.Text = "Udpate : " & Math.Round(100 * i / rowShow.Length, 0) & "%"
                    '            Application.DoEvents()
                    '            If Val(rowOffer("qt_v" & verN).ToString) * Val(row("npieces").ToString) > 0 Then AddRequest(BitronPNCode, rowOffer("description").ToString, rowOffer("qt_v" & verN).ToString, row("npieces").ToString, row("bitronpn").ToString, row("bitronpn").ToString & " - " & BomName, rowOffer("brand").ToString, rowOffer("brandAlt").ToString, OrcadDoc(rowOffer("bitronpn").ToString))
                    '        Next
                    '    Else
                    '        MsgBox("Bom not found in OFFER!" & row("bitronpn").ToString & BomName)
                    '    End If
                Else
                    MsgBox("For this product BOM not assigned! " & row("bitronpn").ToString & "  " & row("name").ToString)
                End If
            End If
        Next

        sql = "DELETE FROM `" & DBName & "`.`materialRequest` WHERE `materialRequest`.`REQUESTQT` = 0 AND `materialRequest`.`REQUESTQT_1` = 0 AND `materialRequest`.`REQUESTQT_2` = 0 AND  `materialRequest`.`REQUESTQT_3` = 0 AND `materialRequest`.`REQUESTQT_4` = 0 AND  `materialRequest`.`REQUESTQT_5` = 0"

        Try
            commandMySql = New MySqlCommand(sql, MySqlconnection)
            commandMySql.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error in DB, reset material request")
        End Try

        ' rda/order elaboration

        Dim InOrder As Single
        Dim InRda As Single
        InOrder = order("", True)
        InRda = Rda("", True)
        Dim NeedRda As String

        rowShow = tblMySql.Select("delta < 0 ")
        For Each row In rowShow
            RdaInfo = ""
            OrderInfo = ""
            NeedRda = ""
            'If "15001152" = row("bitronpn").ToString Then Stop
            If Mid(row("bitronpn").ToString, 1, 2) <> "Q_" And Mid(row("bitronpn").ToString, 1, 2) <> "18" Then
                InOrder = order(row("bitronpn").ToString, False)
                InRda = Rda(row("bitronpn").ToString, False)
                If -Val(row("delta").ToString) > (InOrder + InRda) Then
                    NeedRda = "NEED_RDA[" & Val(Val(row("delta").ToString) + (InOrder + InRda)) & "];"
                End If

                sql = "UPDATE `materialrequest` SET `status`='" & RdaInfo & OrderInfo & NeedRda & "' where bitronpn = '" & row("bitronpn") & "'"

                Try
                    commandMySql = New MySqlCommand(sql, MySqlconnection)
                    commandMySql.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("Error in DB, reset material request")
                End Try
            End If
            Application.DoEvents()
        Next

        rowShow = tblMySql.Select("delta >= 0 ")
        For Each row In rowShow
            sql = "UPDATE `materialrequest` SET `status`='" & "' where bitronpn = '" & row("bitronpn") & "'"

            Try
                commandMySql = New MySqlCommand(sql, MySqlconnection)
                commandMySql.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Error in DB, reset material request")
            End Try
        Next

        tblMySql.Dispose()
        tblMySql.Dispose()
        ButtonUpdateMagBox.Text = "Udpate Material Request"
    End Sub

    Function order(ByVal bitronpn As String, ByVal refrash As Boolean) As Single
        Static AdapterOrder As New MySqlDataAdapter("SELECT * FROM `order`", MySqlconnection)
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

            AdapterOrder.Fill(DsOrder, "order")
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
        Static AdapterRda As New MySqlDataAdapter("SELECT * FROM Rda", MySqlconnection)
        Static tblRda As DataTable
        Static DsRda As New DataSet
        Dim rowShow As DataRow()
        Dim prodPlant As String
        Rda = 0

        If refrash = True Then
            Try
                tblRda.Clear()
                DsRda.Clear()
            Catch ex As Exception

            End Try

            AdapterRda.Fill(DsRda, "Rda")
            tblRda = DsRda.Tables("Rda")

        Else
            prodPlant = ParameterTable("plant")
            rowShow = tblRda.Select("RAIDF ='" & bitronpn & "' and RASTB ='" & prodPlant & "' AND ( RASTA ='I' OR  RASTA ='L' OR RASTA ='A' OR RASTA ='C' )")
            For Each row In rowShow
                Rda = Rda + Val(row("RAQT1").ToString) + Val(row("RAQT2").ToString) + Val(row("RAQT3").ToString) + Val(row("RAQT4").ToString) + Val(row("RAQT5").ToString)
                RdaInfo = "RDA_" & row("ranum").ToString & "_" & row("RASTA").ToString & "[" & Val(row("RAQT1").ToString) + Val(row("RAQT2").ToString) + Val(row("RAQT3").ToString) + Val(row("RAQT4").ToString) + Val(row("RAQT5").ToString) & "];" & RdaInfo
            Next

        End If

    End Function

    Function SigipUsed(ByVal bitronpn As String) As String

        Dim rowShowSigip As DataRow()
        SigipUsed = ""
        rowShowSigip = tblSigip.Select("bitron_pn ='" & bitronpn & "' and (active = 'yes')")

        For Each rowSigip In rowShowSigip
            SigipUsed = SigipUsed & rowSigip.Item("bom").ToString & " - " & rowSigip.Item("des_bom").ToString & "[" & Val(rowSigip.Item("qt").ToString) & "];"
        Next


    End Function

    Sub AddRequest(ByVal bitronPN As String, ByVal des_PN As String, ByVal qt As String, ByVal npieces As String, ByVal Bom As String, ByVal des_bom As String, Optional ByVal brand As String = "", Optional ByVal brandAlt As String = "", Optional ByVal Doc As String = "")

        Dim sql As String
        Dim strQt As String
        Dim strBomList As String = ""
        Dim commandMySql As New MySqlCommand
        Dim adapterMySql As New MySqlDataAdapter
        Dim tblMySql As New DataTable
        Dim dsMySql As New DataSet
        Dim i As Integer, j As Integer, stockvalue As Double
        sql = "SELECT * FROM `materialrequest` WHERE `bitronpn`='" & bitronPN & "'"
        adapterMySql = New MySqlDataAdapter(sql, MySqlconnection)
        adapterMySql.Fill(dsMySql, "materialrequest")
        tblMySql = dsMySql.Tables("materialrequest")
        stockvalue = Str(Stock(bitronPN))
        If tblMySql.Rows.Count < 1 Then
            strBomList = des_bom & "[" & Trim(Str(IIf(qt = Int(qt), Int(qt), Math.Round(Val(qt), 5)))) & "]"
            sql = "INSERT INTO `srvdoc`.`materialrequest` (`DeltaUsedFlag`,`ProductionUsed`,`bitronPN`,`des_pn`,`Brand`,`BrandALT`,`pfp`,`warehouse3d`,`Delta`,`RequestQt`,`BomList`,`doc`) VALUES ('" & IIf(SigipUsed(bitronPN) <> "" Or
            (stockvalue - Val(strQt)) < Val(strQt) * 0.1, "YES", "NO") & "','" & SigipUsed(bitronPN) & "','" & bitronPN & "','" & des_PN & "','" & brand & "','" & brandAlt & "','" & pfp(bitronPN) & "','" & stockvalue & "','" &
             stockvalue - Val(Str(Val(qt) * Val(npieces))) & "','" & Trim(Str(Val(qt) * Val(npieces))) & "','" & strBomList & "','" & Doc & "')"
        Else
            strQt = Trim(Str(Val(tblMySql.Rows.Item(0)("requestqt")) + Str(Val(qt) * Val(npieces))))
            strBomList = tblMySql.Rows.Item(0)("BomList").ToString
            If strBomList.Contains(Bom) And strBomList <> "" Then
                i = InStr(strBomList, Bom, CompareMethod.Text)
                i = InStr(i + 1, strBomList, "[", CompareMethod.Text)
                j = InStr(i, strBomList, "]", CompareMethod.Text)
                strBomList = Mid(strBomList, 1, i) & Trim(Str(Val(Mid(strBomList, i + 1, j - 1 - i)) + Val(qt))) & Mid(strBomList, j)
            Else
                strBomList = tblMySql.Rows.Item(0)("BomList") & ";" & des_bom & "[" & Trim(Str(IIf(qt = Int(qt), Int(qt), Math.Round(Val(qt), 5)))) & "]"
                If Mid(strBomList, 1, 1) = ";" Then strBomList = Mid(strBomList, 2)
            End If
            sql = "UPDATE `materialrequest` SET `w_warehouse`=" & Val(Stock_W(bitronPN)) & ", `RequestQt`='" & strQt & "',`BomList`='" & strBomList & "',`brandALT`='" & brandAlt & "',`brand`='" & brand & "',`pfp`='" & pfp(bitronPN) & "',`doc`='" & Doc & "',`warehouse3d`='" & stockvalue & "',`Delta`='" & stockvalue - Val(strQt) & "',`ProductionUsed`='" & SigipUsed(bitronPN) & "',`DeltaUsedFlag`='" & IIf(SigipUsed(bitronPN) <> "" Or (stockvalue - Val(strQt)) < Val(strQt) * 0.1, "YES", "NO") & "' WHERE `bitronpn`='" & bitronPN & "'"
        End If
        Try
            commandMySql = New MySqlCommand(sql, MySqlconnection)
            commandMySql.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Error in DB update/insert material request")
        End Try


    End Sub

    'Sub ResetMaterial()
    '    Dim Sql As String
    '    Dim cmd As New MySqlCommand()

    '    Try
    '        Sql = "DELETE FROM `" & materialrequest & "`.`materialRequest` WHERE `materialRequest`.`bitronpn` like '*'"
    '        cmd = New MySqlCommand(Sql, MySqlconnection)
    '        cmd.ExecuteNonQuery()
    '        Application.DoEvents()
    '    Catch ex As Exception
    '        MsgBox("Error in  delete material")
    '    End Try

    'End Sub

    'Function versionOffer(ByVal versionName As String, ByRef BomName As String, ByRef verN As Integer) As Integer
    '    Dim rowShow As DataRow()
    '    Dim DsBomOff As New DataSet
    '    Dim tblBomOff As DataTable
    '    AdapterBomOff.Fill(DsBomOff, "BomOffer")
    '    tblBomOff = DsBomOff.Tables("BomOffer")
    '    rowShow = tblBomOff.Select("var1 = '" & versionName & "' or " & _
    '                               "var2 = '" & versionName & "' or " & _
    '                               "var4 = '" & versionName & "' or " & _
    '                               "var3 = '" & versionName & "' or " & _
    '                               "var5 = '" & versionName & "' or " & _
    '                               "var6 = '" & versionName & "'")


    '    If rowShow.Length = 1 Then

    '        If rowShow(0).Item("var1").ToString = versionName Then
    '            verN = 1
    '            BomName = rowShow(0).Item("name").ToString
    '        End If
    '        If rowShow(0).Item("var2").ToString = versionName Then
    '            verN = 2
    '            BomName = rowShow(0).Item("name").ToString
    '        End If
    '        If rowShow(0).Item("var3").ToString = versionName Then
    '            verN = 3
    '            BomName = rowShow(0).Item("name").ToString
    '        End If
    '        If rowShow(0).Item("var4").ToString = versionName Then
    '            verN = 4
    '            BomName = rowShow(0).Item("name").ToString
    '        End If
    '        If rowShow(0).Item("var5").ToString = versionName Then
    '            verN = 5
    '            BomName = rowShow(0).Item("name").ToString
    '        End If
    '        If rowShow(0).Item("var6").ToString = versionName Then
    '            verN = 6
    '            BomName = rowShow(0).Item("name").ToString
    '        End If


    '    ElseIf rowShow.Length > 1 Then
    '        MsgBox("Error in Offer bom association, more of one bom with p/n:" & versionName)
    '    End If
    '    versionOffer = rowShow.Length
    'End Function

    Private Sub ButtonSaveDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSaveDefault.Click
        If MsgBox("Are you sure to save and change current configuration?", MsgBoxStyle.YesNo) = vbYes Then
            XmlTree.SetTreeView(TreeViewTask)
            ParameterTableWrite("sop_task", XmlTree.ExportToString)
        End If
    End Sub

    Private Sub TextBoxTaskHeader_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxTaskHeader.TextChanged
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

    Private Sub CheckBoxClosed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxClosed.CheckedChanged
        UpdateActivityID()
    End Sub

    Public Sub Import_Order()

        Dim sql As String
        Dim commandMySql As MySqlCommand

        'open Saldi_per_ubicazione.xls
        Dim xlsApp As New Object
        xlsApp = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelSigipOrder"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")


        'empty the PFP table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`order`", MySqlconnection)
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
            'Catch ex As Exception
            'MsgBox(ex.Message)
            'End Try

            'copy data from excel to `pfp`
            sql = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `order` fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`stab`,`ordine`,`tipoOrd`,`forn`,`RagSoc`,`Stato_Ord`,`Data_Inserimento`,`Acquisitore`,`Num_Item`,`Num_RDA`,`Identif`,`Descr`,`Stato_Item`,`Qta_Ord`,`Qta_Consegnata`,`Qta_Scartata`)"
            commandMySql = New MySqlCommand(sql, MySqlconnection)
            commandMySql.ExecuteNonQuery()

            'sql = "DELETE FROM `" & DBName & "`.`order` WHERE `order`.`stab` = '62'"
            sql = "DELETE FROM `" & DBName & "`.`order` WHERE `order`.`stab` = " & ParameterTable("plant")
            commandMySql = New MySqlCommand(sql, MySqlconnection)
            commandMySql.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Import_WH_Stock()

        Dim sql As String
        Dim commandMySql As MySqlCommand
        Dim xlsWorkbook As Object

        'open Saldi_per_ubicazione.xls
        Dim xlsApp As New Object
        xlsApp = CreateObject("Excel.Application")

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


        'empty the PFP table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`spu`", MySqlconnection)
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
        sql = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `spu` fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`bitronpn`,`pades`,`sagia`,`samgz`,`saubc`,`paumt`,`pmppa`,`pmcmm`,`paclm`)"
        commandMySql = New MySqlCommand(sql, MySqlconnection)
        commandMySql.ExecuteNonQuery()

    End Sub

    Public Sub Import_Rda()

        Dim sql As String
        Dim commandMySql As MySqlCommand

        'open rda_per_ubicazione.xls
        Dim xlsApp As New Object
        xlsApp = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelSigipRda"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()
        xlsWorksheet.Cells.Replace(What:=",", Replacement:="")



        'empty the rda table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`rda`", MySqlconnection)
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
        sql = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `rda` fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`RATRK`,`RASTB`,`RANUM`,`RATIF`,`RAITE`,`RADES`,`RACMA`,`RAIDF`,`RAVSM`,`RAVAL`,`RAUNC`,`RAQT1`,`RAQT2`,`RAQT3`,`RAQT4`,`RAQT5`,`RADT1`,`RADT2`,`RADT3`,`RADT4`,`RADT5`,`RAQTO`,`RAVSC`,`RACOM`,`RAFOR`,`RACDI`,`RACDR`,`RAUSE`,`RADTE`,`RADCV`,`RASTA`,`RAOA1`,`RAECO`,`RALOT`,`RAPGM`,`RABUY`,`RAUAG`,`RAORA`)"
        commandMySql = New MySqlCommand(sql, MySqlconnection)
        commandMySql.ExecuteNonQuery()

    End Sub

    Public Function Stock(ByVal bitronpn As String) As Double

        Dim adapterMySql As MySqlDataAdapter
        Dim tblMySql As New DataTable
        Dim dsMySql As New DataSet

        'adapterMySql = New MySqlDataAdapter("SELECT SUM(`sagia`) AS sum FROM `srvdoc`.`spu` WHERE (samgz='A' or samgz='D' or samgz='G' or samgz='P' ) and `bitronpn`='" & bitronpn & "'", MySqlconnection)
        adapterMySql = New MySqlDataAdapter("SELECT SUM(`sagia`) AS sum FROM `srvdoc`.`spu` WHERE (samgz='D' or samgz='8') and `bitronpn`='" & bitronpn & "'", MySqlconnection)
        adapterMySql.Fill(dsMySql, "spu")
        tblMySql = dsMySql.Tables("spu")



        Return Val(tblMySql.Rows(0).Item("sum").ToString)

    End Function

    Public Function Stock_W(ByVal bitronpn As String) As Double

        Dim adapterMySql As MySqlDataAdapter
        Dim tblMySql As New DataTable
        Dim dsMySql As New DataSet

        'adapterMySql = New MySqlDataAdapter("SELECT SUM(`sagia`) AS sum FROM `srvdoc`.`spu` WHERE (samgz='D' or samgz='G' ) and `bitronpn`='" & bitronpn & "'", MySqlconnection)
        adapterMySql = New MySqlDataAdapter("SELECT SUM(`sagia`) AS sum FROM `srvdoc`.`spu` WHERE (samgz='D' or samgz='8' ) and `bitronpn`='" & bitronpn & "'", MySqlconnection)
        adapterMySql.Fill(dsMySql, "spu")
        tblMySql = dsMySql.Tables("spu")

        Return Val(tblMySql.Rows(0).Item("sum").ToString)

    End Function

    Function pfp(ByVal bitronpn As String) As String
        pfp = ""
        Dim rowShow As DataRow(), ass As Integer
        rowShow = tblPfp.Select("pfidf = '" & Replace(ReplaceChar(bitronpn), "E", "") & "' and  pedfi = '0'", "pfpan desc, pfpaf desc, pedin, pfpad")
        ass = 0
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
            'For Each row In rowShow
            '    pfp = pfp & " - " & (ConvPrice(row("pepre"), row("pelot")) & " - " & row("peval") & " Date " & row("pedin"))
            '    ass = ass + Val(row("pfpan")) + Val(row("pfpaf"))
            '    If ass >= 100 Then Exit For
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

        Try
            OpenConnectionSqlOrcad("10.10.10.36", "Orcad1", "orcadw", "orcadw")
            'OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
        Catch ex As Exception
            CloseConnectionSqlOrcad()
            OpenConnectionSqlOrcad("10.10.10.36", "Orcad1", "orcadw", "orcadw")
            'OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
        End Try

        GetOrcadSupplier = ""
        Try
            Dim AdapterSql As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') and codice_bitron = '" & BitronPN & "'", SqlconnectionOrcad)
            TblSql.Clear()
            DsSql.Clear()
            AdapterSql.Fill(DsSql, "orcadw.T_orcadcis")
            TblSql = DsSql.Tables("orcadw.T_orcadcis")

            If TblSql.Rows.Count > 0 Then
                info = IIf(TblSql.Rows.Item(0)("costruttore").ToString <> "", TblSql.Rows.Item(0)("costruttore").ToString & "[" & TblSql.Rows.Item(0)("orderingcode").ToString & "];", "")
                For i = 2 To 9
                    GetOrcadSupplier = GetOrcadSupplier & IIf(TblSql.Rows.Item(0)("costruttore" & i).ToString <> "", TblSql.Rows.Item(0)("costruttore" & i).ToString & "[" & TblSql.Rows.Item(0)("orderingcode" & i).ToString & "];", "")
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Function
    Sub OpenConnectionSqlOrcad(ByVal strHost As String, ByVal strDatabase As String, ByVal strUserName As String, ByVal strPassword As String)

        Try
            ConnectionStringOrcad = "server=" & strHost & ";user id=" & strUserName & ";" & "pwd=" & strPassword & ";" & "database=" & strDatabase & ";Connect Timeout=10;"
            SqlconnectionOrcad = New SqlConnection(ConnectionStringOrcad)
            If SqlconnectionOrcad.State = ConnectionState.Closed Then
                SqlconnectionOrcad.Open()
            End If
        Catch ex As Exception

            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    Sub CloseConnectionSqlOrcad()

        Try
            If SqlconnectionOrcad.State = ConnectionState.Closed Then
                SqlconnectionOrcad.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

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
        ' If "15002423" = bitronPN Then Stop
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

        'objCurrencyManager.Position = 0

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
        'DTP_Date.DataBindings.Add("Text", tblNPI, "DATE")
        'DTP_PlanCloseDate.DataBindings.Add("Text", tblNPI, "ETC")
        Txt_FilePath.DataBindings.Add("Text", tblNPI, "FilePath")

    End Sub

    Private Sub Btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Add.Click
        Dim cmd As New MySqlCommand()
        Dim Sql As String
        Dim selectrowNo As Integer

        If DGV_NPI.Rows.Count > 0 Then
            selectrowNo = DGV_NPI.CurrentRow.Index
        Else
            selectrowNo = 0
        End If

        'Try
        '    selectrowNo = DGV_NPI.CurrentRow.Index
        'Catch ex As Exception
        '    selectrowNo = 0
        'End Try

        'If controlRight("W") >= 2 Then           
        'BEC: If controlRight("R") >= 2 Then

        If Trim(Txt_BitronPN.Text) <> "" Then
            Try

                Sql = "INSERT INTO npi_openissue (BS,DATE,Issue_description,Bitron_PN,Area,Owner,Temp_corr_action,Final_corr_action,ETC,Status,FilePath ) VALUES ('" &
                Txt_description.Text & "','" & DateStart & "','" & Txt_IssueDescription.Text & "','" & Txt_BitronPN.Text & "','" & Txt_Area.Text & "','" &
                Cob_Owner.Text & "','" & Txt_TempCorrectAction.Text & "','" & Txt_FinalCorrectAction.Text & "','" & DateClosed & "','" & Cob_Status.Text & "','" & Txt_FilePath.Text & "');"

                cmd = New MySqlCommand(Sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                Call issuefunction(0)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Bitron Product Code can't be empty")

        End If
        ' Else
        ' MsgBox("No enough right to add this table")
        'End If

    End Sub

    Private Sub Btn_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Del.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim selectrowNo As Integer = DGV_NPI.CurrentRow.Index
        'If controlRight("W") >= 2 Then
        'BEC: If controlRight("R") >= 2 Then

        If MsgBox("Are you sure to delete this issue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                sql = " DELETE  From npi_openissue WHERE ID = " & Txt_Index.Text

                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                Call issuefunction(selectrowNo - 1)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
        'Else
        ' MsgBox("no enough right to delete this table")
        'End If

    End Sub

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String

        'DGV_NPI.Focus()
        'DGV_NPI.Rows(1).Selected = True

        Dim selectrowNo As Integer = DGV_NPI.CurrentRow.Index


        'If controlRight("W") >= 2 Then
        'BEC: If controlRight("R") >= 2 Then

        If Trim(Txt_BitronPN.Text) <> "" Then
            Try

                sql = "UPDATE npi_openissue SET BS = '" & Txt_description.Text & "',DATE = '" & DateStart & "',Issue_description ='" &
                Txt_IssueDescription.Text & "',Bitron_PN = '" & Txt_BitronPN.Text & "',Area = '" & Txt_Area.Text & "',Owner = '" & Cob_Owner.Text & "',Temp_corr_action = '" &
                Txt_TempCorrectAction.Text & "',Final_corr_action = '" & Txt_FinalCorrectAction.Text & "',ETC = '" & DateClosed & "',Status = '" &
                Cob_Status.Text & "',FilePath ='" & Txt_FilePath.Text & "' WHERE ID = '" & Txt_Index.Text & "'"

                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                Call issuefunction(selectrowNo)


                MsgBox("Successful update")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MsgBox("Bitron Product Code can't be empty")
        End If
        'Else
        'MsgBox("No enough right to update this table")
        'End If

    End Sub

    Private Sub Btn_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Search.Click

        Call DGV_Fill()

    End Sub

    Private Sub DGV_Fill()

        'Dim AdapterNPICob As New MySqlDataAdapter("SELECT * FROM npi_openissue WHERE BS = " & Trim(Cob_FilterBS.Text), MySqlconnection)
        'Dim AdapterNPICob As New MySqlDataAdapter("SELECT * FROM npi_openissue WHERE Owner = 'BIC'", MySqlconnection)

        Dim Sql As String = "SELECT * FROM npi_openissue WHERE ID > 0 "
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

        Sql += "order by ID"

        Dim AdapterNPICob As New MySqlDataAdapter(Sql, MySqlconnection)
        Try
            DsNPI.Clear()
            tblNPI.Clear()
            AdapterNPICob.Fill(DsNPI, "NPI")
            tblNPI = DsNPI.Tables("NPI")

            DGV_NPI.DataSource = tblNPI
            Call DataBangding(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub issuefunction(ByVal selectrowNo As Integer)

        DsNPI.Clear()
        tblNPI.Clear()
        AdapterNPI.Fill(DsNPI, "NPI")
        tblNPI = DsNPI.Tables("NPI")

        DGV_NPI.DataSource = tblNPI

        If tblNPI.Rows.Count > 0 And selectrowNo > -1 Then

            DGV_NPI.Rows(selectrowNo).Selected = True
            Call DataBangding(selectrowNo)

        End If

    End Sub

    Private Sub DGV_NPI_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGV_NPI.MouseDoubleClick

        If DGV_NPI.SelectedRows.Count = 1 Then
            If controlRight("W") >= 1 Then 'BEC: controlRight("R") >= 1

                Dim fileOpen As String

                'fileOpen = downloadFileWinPath(ParameterTable("plant") & "R_NPI_OPI_" & Txt_FilePath.Text)
                fileOpen = downloadFileWinPath(Txt_FilePath.Text)
                Application.DoEvents()
                Process.Start(fileOpen)
                Application.DoEvents()
            Else
                MsgBox("No enough right to check the file")
            End If

        End If

    End Sub

    'Private Sub DGV_NPI_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGV_NPI.SelectionChanged

    '    Try
    '        DTP_Date.Text = DGV_NPI.CurrentRow.Cells("StartDate").Value().ToString
    '        DTP_PlanCloseDate.Text = DGV_NPI.CurrentRow.Cells("PlanedClosedDate").Value().ToString
    '    Catch ex As Exception
    '        DTP_Date.Text = DGV_NPI.Rows(1).Cells("StartDate").Value().ToString
    '        DTP_PlanCloseDate.Text = DGV_NPI.Rows(1).Cells("PlanedClosedDate").Value().ToString
    '    End Try


    'End Sub

    Private Sub Btn_UpLoadFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btn_UpLoadFile.Click

        Dim selectrowNo As Integer = DGV_NPI.CurrentRow.Index

        'If controlRight("W") >= 2 Then
        'BEC: If controlRight("R") >= 2 Then

        FormNPIDocMamagement.Show()
        FormNPIDocMamagement.Focus()

        ' Else
        ' MsgBox("No enough right to update this table")
        ' End If

        'Call issuefunction(selectrowNo)

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


    'Private Sub Btn_ToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_ToExcel.Click



    '    Dim DRshow As DataRow()
    '    Dim rowExcel As Integer
    '    Dim project As String




    '    DsNPI.Clear()
    '    tblNPI.Clear()
    '    AdapterNPI.Fill(DsNPI, "Openissue")
    '    tblNPI = DsNPI.Tables("Openissue")
    '    Dim sql As String = IIf(Cob_FilterBS.Text = "", "BS like '*' and ", "BS = '" & Cob_FilterBS.Text & "'  and ") & _
    '                                  IIf(Cob_FilterOwner.Text = "", "Owner like '*'  and ", "Owner = '" & Cob_FilterOwner.Text & "'  and ") & _
    '                                  IIf(Cob_FilterStatus.Text = "", "Status like '*'  and ", "Status = '" & Cob_FilterStatus.Text & "'  and ") & _
    '                                  IIf(Cob_FilterBitronPN.Text = "", "Bitron_PN <> 0 ", "Bitron_PN = " & Cob_FilterBitronPN.Text)


    '    DRshow = tblNPI.Select(sql, "id")

    '    Me.Hide()

    '    CollectProcess()
    '    Dim excelApp As New Object
    '    excelApp = CreateObject("Excel.Application")


    '    Dim excelWorkbook As Object
    '    Dim excelSheet As Object



    '    excelWorkbook = excelApp.Workbooks.Open(ParameterTable("PathPicture") & "\65R_GEN_NPI_Issue_Template_0.xlsx")



    '    excelWorkbook.Activate()
    '    excelSheet = excelWorkbook.Worksheets("NPI_Issue")
    '    excelSheet.Activate()
    '    Dim cc As New ColorConverter
    '    excelApp.Visible = True
    '    rowExcel = 1
    '    project = ""
    '    For Each row In DRshow


    '        If row("BS").ToString <> project Then

    '            rowExcel = rowExcel + 1
    '            project = row("BS").ToString
    '            excelApp.Cells(rowExcel, 1) = project
    '            excelApp.Cells(rowExcel, 1).Font.Bold = True
    '            excelApp.Rows(rowExcel & ":" & rowExcel).Font.Bold = True
    '            excelApp.Cells(rowExcel, 2) = row("Bitron_PN").ToString
    '            excelApp.Cells(rowExcel, 3) = row("Issue_description").ToString
    '            excelApp.Cells(rowExcel, 4) = row("Area").ToString
    '            excelApp.Cells(rowExcel, 5) = row("Owner").ToString
    '            excelApp.Cells(rowExcel, 6) = row("DATE").ToString
    '            excelApp.Cells(rowExcel, 7) = row("Temp_corr_action").ToString
    '            excelApp.Cells(rowExcel, 8) = row("Final_corr_action").ToString
    '            excelApp.Cells(rowExcel, 9) = row("ETC").ToString
    '            excelApp.Cells(rowExcel, 10) = row("Status").ToString

    '        End If

    '    Next
    '    Try
    '        SaveFileDialog1.FileName = "65R_GEN_NPI_Issue_" & Replace(Today, "/", "_") & "_0.xlsx"
    '        SaveFileDialog1.ShowDialog()
    '        excelWorkbook.SaveAs(SaveFileDialog1.FileName)
    '        excelWorkbook.Close(True)
    '        excelApp.Quit()
    '        Dim generation As Integer = GC.GetGeneration(excelApp)
    '        GC.Collect(generation)
    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try
    '    'Catch ex As Exception
    '    '    MsgBox(ex.Message)
    '    'End Try
    '    Me.Focus()
    '    Me.Show()
    '    KillLastExcel()
    '    Application.DoEvents()

    '    Call DGV_Fill()

    'End Sub
    Function downloadFileWinPath(ByVal fileName As String) As String
        Dim strPathFtp As String
        Dim objFtp As ftp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""


        Dim cmd As New MySqlCommand()
        If fileName <> "" Then
            Try
                strPathFtp = Mid(fileName, 1, 3) & "/" & Mid(fileName, 1, 11) & "/"  '"/"("65R/65R_PRO_ECR/")

                objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, fileName) ' download successfull
                downloadFileWinPath = System.IO.Path.GetTempPath & fileName
            Catch ex As Exception
                'ComunicationLog("0049") ' Error in ecr Download
            End Try
        Else
            MsgBox("FilePath does not exist")
        End If

    End Function

    Private Sub CobFilterBitronPNFill()
        Dim rowResults As DataRow()

        Cob_FilterBitronPN.Items.Clear()
        Cob_FilterBitronPN.Items.Add("")

        AdapterNPI.Fill(DsNPI, "NPI")
        tblNPI = DsNPI.Tables("NPI")

        rowResults = tblNPI.Select()
        For Each row In rowResults
            If Cob_FilterBitronPN.Items.Contains(UCase(row("Bitron_PN").ToString)) = False Then Cob_FilterBitronPN.Items.Add(UCase(row("Bitron_PN").ToString))
        Next

        Cob_FilterBitronPN.Sorted = True

    End Sub

    'Private Sub CobFilterBitronPNFill()
    '    Dim rowResults As DataRow(), Area As String = ""

    '    Cob_FilterBitronPN.Items.Clear()
    '    Cob_FilterBitronPN.Items.Add("")

    '    AdapterNPI.Fill(DsNPI, "NPI")
    '    tblNPI = DsNPI.Tables("NPI")

    '    rowResults = tblNPI.Select("Area like '*'", "Bitron_PN")
    '    For Each row In rowResults
    '        If Area <> row("Bitron_PN").ToString Then
    '            Cob_FilterBitronPN.Items.Add(UCase(row("Bitron_PN").ToString))
    '            If Cob_FilterBitronPN.Items.Contains(UCase(row("Bitron_PN").ToString)) = False Then Cob_FilterBitronPN.Items.Add(UCase(row("Bitron_PN").ToString))
    '        End If

    '        Area = row("Bitron_PN").ToString
    '    Next
    '    Cob_FilterBitronPN.Sorted = True

    'End Sub

    Private Sub CobFilterBSFill()

        Dim rowResults As DataRow()

        Cob_FilterBS.Items.Clear()
        Cob_FilterBS.Items.Add("")

        AdapterNPI.Fill(DsNPI, "NPI")
        tblNPI = DsNPI.Tables("NPI")

        rowResults = tblNPI.Select()
        For Each row In rowResults
            If Cob_FilterBS.Items.Contains(UCase(row("BS").ToString)) = False Then Cob_FilterBS.Items.Add(UCase(row("BS").ToString))
        Next

        Cob_FilterBS.Sorted = True

    End Sub

    Private Sub DTP_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTP_Date.ValueChanged

        'MessageBox.Show(DTP_Date.Value.ToString)
        DateStart = DTP_Date.Value.Date
        'DateStart = DateTime.ParseExact(DateStart.ToString, "yyyy/MM/dd", CultureInfo.InvariantCulture)
        'DateStart = "#" & DTP_Date.Value.Year & "-" & DTP_Date.Value.Month & "-" & DTP_Date.Value.Day & "#"
        'MessageBox.Show(DateStart)


    End Sub

    Private Sub DTP_PlanCloseDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTP_PlanCloseDate.ValueChanged

        DateClosed = DTP_PlanCloseDate.Value.Date

    End Sub

    Private Sub TextBoxUser_TextChanged(sender As Object, e As EventArgs) Handles TextBoxUser.TextChanged

    End Sub

    Private Sub Cob_FilterBS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_FilterBS.SelectedIndexChanged

    End Sub

    Private Sub ComboBoxBomLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBomLocation.SelectedIndexChanged

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub Cob_Owner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_Owner.SelectedIndexChanged

    End Sub

    Private Sub Txt_BitronPN_TextChanged(sender As Object, e As EventArgs) Handles Txt_BitronPN.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub DGV_NPI_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV_NPI.CellContentClick

    End Sub

    Private Sub Txt_FinalCorrectAction_TextChanged(sender As Object, e As EventArgs) Handles Txt_FinalCorrectAction.TextChanged

    End Sub

    Private Sub Txt_description_TextChanged(sender As Object, e As EventArgs) Handles Txt_description.TextChanged

    End Sub

    Private Sub Txt_TempCorrectAction_TextChanged(sender As Object, e As EventArgs) Handles Txt_TempCorrectAction.TextChanged

    End Sub

    Private Sub Txt_IssueDescription_TextChanged(sender As Object, e As EventArgs) Handles Txt_IssueDescription.TextChanged

    End Sub
End Class