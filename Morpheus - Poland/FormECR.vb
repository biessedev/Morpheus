
Option Explicit On
Option Compare Text

Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net
Imports System.Configuration
Imports System.Dynamic
Imports System.Linq

Public Class FormECR
    Dim tblDoc As DataTable, tblDocType As DataTable, tblEcr As DataTable, tblProd As DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsEcr As New DataSet, DsProd As New DataSet
    Dim userDep3 As String
    Dim cmd As New MySqlCommand
    Dim CultureInfo_ja_JP As New CultureInfo("ja-JP", False)
    Dim needSave As Boolean = False
    Dim Dsmail As New DataSet
    Dim tblmail As DataTable
    Dim MailSent As Boolean

    Private Sub FormECR_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()
    End Sub

    Private Sub FormECR_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If needSave = True Then
            Dim feedback As MsgBoxResult = MsgBox("Do you want to save changes before closing?", MsgBoxStyle.YesNoCancel)
            If feedback = MsgBoxResult.Yes Then
                ButtonSave_Click(Me, EventArgs.Empty)
                ButtonSave.BackColor = Color.Green
                ButtonSaveSend.BackColor = Color.Green
                needSave = False
            ElseIf feedback = MsgBoxResult.Cancel Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub FormECR_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FormStart.Hide()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM ecr ORDER BY NUMBER;", con)
                AdapterEcr.Fill(DsEcr, "ecr")
            End Using
            tblEcr = DsEcr.Tables("ecr")

            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC;", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")

            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM product order by id;", con)
                AdapterProd.Fill(DsProd, "product")
            End Using
            tblProd = DsProd.Tables("product")
        End Using

        ComboProductFill()
        userDep3 = user()

        If userDep3 <> "A" And userDep3 <> "" Then Me.Controls("Button" & userDep3 & "L").Enabled = True

        If userDep3 = "R" And Not AllSign() Then
            ComboBoxPay.Enabled = True
        Else
            ComboBoxPay.Enabled = False
        End If

        ListViewProd.Clear()

        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "ProductPN"
        h.Width = 100
        ListViewProd.Columns.Add(h)
        h2.Text = "Description"
        h2.Width = 370
        ListViewProd.Columns.Add(h2)
        fillEcrComboTable()
        If ComboBoxEcr.Items.Count > 0 Then ComboBoxEcr.Text = ComboBoxEcr.Items(ComboBoxEcr.Items.Count - 1) 'Si aspetta sempre almeno una ECR

        ColorButton(userDep3)
        UpdateField()
        ButtonSave.BackColor = Color.Green
        ButtonSaveSend.BackColor = Color.Green
        If userDep3 = "" Then
            ButtonR_Click(Me, e)
        End If
        CheckBoxOpen.Checked = True
        CheckBoxCLCV.Enabled = If(controlRight("R") = 3, True, False)
        UpdateComboDepartmentsNumbers()

        'enabling correspondent department for LeadTime Combobox
        Select Case userDep3
            Case "R"
                ComboBoxR.Enabled = True
            Case "U"
                ComboBoxU.Enabled = True
            Case "L"
                ComboBoxL.Enabled = True
            Case "B"
                ComboBoxB.Enabled = True
            Case "E"
                ComboBoxE.Enabled = True
            Case "N"
                ComboBoxN.Enabled = True
            Case "P"
                ComboBoxP.Enabled = True
            Case "Q"
                ComboBoxQ.Enabled = True
            Case "S"
                ComboBoxS.Enabled = True
        End Select

        setLeadTimeAvailability()
    End Sub

    Sub setLeadTimeAvailability()
        Me.Controls("ComboBox" & userDep3).Enabled =Not Me.Controls("Button" & userDep3).Text.Contains("NOT CHECKED")
    End Sub

    ' Fill the ECR combo with all ECR yet open
    Sub fillEcrComboTable()
        ComboBoxEcr.Items.Clear()
        Dim DsEcr As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr where `number`> 0 and `Description` not like '%template%'", con)
                AdapterEcr.Fill(DsEcr, "ecr")
            End Using
        End Using
        Dim tblEcr As DataTable = DsEcr.Tables("ecr")

        Try
            Dim rowshow As DataRow() = tblEcr.Select("description like '*' ", "number")
            For Each row In rowshow
                If CheckBoxOpen.Checked = True Then
                    If Not AllSign(row("number").ToString) Then
                        ComboBoxEcr.Items.Add(row("description").ToString)
                    End If
                Else
                    ComboBoxEcr.Items.Add(row("description").ToString)
                End If
            Next
            If ComboBoxEcr.Items.Count > 0 Then
                ComboBoxEcr.Text = ComboBoxEcr.Items(ComboBoxEcr.Items.Count - 1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub UpdateField()
        If needSave = True Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ButtonSave_Click(Me, EventArgs.Empty)
                ButtonSave.BackColor = Color.Green
                ButtonSaveSend.BackColor = Color.Green
                needSave = False
            Else
                ButtonSave.BackColor = Color.Green
                ButtonSaveSend.BackColor = Color.Green
                needSave = False
            End If
        End If
        Dim pos As Integer, EcrN As Integer, prod As String, Result As DataRow()

        tblEcr.Clear()
        DsEcr.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM ecr;", con)
                AdapterEcr.Fill(DsEcr, "ecr")
            End Using
            tblEcr = DsEcr.Tables("ecr")
        End Using

        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))

        Result = tblEcr.Select("number = " & EcrN)

        If readField("EcrCheck", Val(Mid(ComboBoxEcr.Text, 1, pos))) = "YES" Then
            ButtonR.Enabled = True
            ButtonU.Enabled = True
            ButtonL.Enabled = True
            ButtonB.Enabled = True
            ButtonE.Enabled = True
            ButtonN.Enabled = True
            ButtonP.Enabled = True
            ButtonQ.Enabled = True
            ButtonS.Enabled = True
            ButtonA.Enabled = True
        Else
            ButtonR.Enabled = False
            ButtonU.Enabled = False
            ButtonL.Enabled = False
            ButtonB.Enabled = False
            ButtonE.Enabled = False
            ButtonN.Enabled = False
            ButtonP.Enabled = False
            ButtonQ.Enabled = False
            ButtonS.Enabled = False
            ButtonA.Enabled = False

        End If

        If Result.Length > 0 Then
            ButtonR.Text = Result(0).Item("Rsign")
            ButtonU.Text = Result(0).Item("Usign")
            ButtonL.Text = Result(0).Item("Lsign")
            ButtonB.Text = Result(0).Item("Bsign")
            ButtonE.Text = Result(0).Item("Esign")
            ButtonN.Text = Result(0).Item("Nsign")
            ButtonP.Text = Result(0).Item("Psign")
            ButtonQ.Text = Result(0).Item("Qsign")
            ButtonS.Text = Result(0).Item("Ssign")
            ButtonA.Text = Result(0).Item("Asign")

            If userDep3 = "R" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Rnote")
            If userDep3 = "U" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Unote")
            If userDep3 = "L" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Lnote")
            If userDep3 = "B" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Bnote")
            If userDep3 = "E" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Enote")
            If userDep3 = "N" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Nnote")
            If userDep3 = "P" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Pnote")
            If userDep3 = "Q" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Qnote")
            If userDep3 = "S" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Snote")
            If userDep3 = "A" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Anote")

            If userDep3 = "R" Then TextBoxStepCost.Text = Result(0).Item("RCost")
            If userDep3 = "U" Then TextBoxStepCost.Text = Result(0).Item("UCost")
            If userDep3 = "L" Then TextBoxStepCost.Text = Result(0).Item("LCost")
            If userDep3 = "B" Then TextBoxStepCost.Text = Result(0).Item("BCost")
            If userDep3 = "E" Then TextBoxStepCost.Text = Result(0).Item("ECost")
            If userDep3 = "N" Then TextBoxStepCost.Text = Result(0).Item("NCost")
            If userDep3 = "P" Then TextBoxStepCost.Text = Result(0).Item("PCost")
            If userDep3 = "Q" Then TextBoxStepCost.Text = Result(0).Item("QCost")
            If userDep3 = "S" Then TextBoxStepCost.Text = Result(0).Item("SCost")

            If Result(0).Item("EcrCheck").ToString = "YES" Then
                ButtonEcrCheck.BackColor = Color.Green
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> YES"
            Else
                ButtonEcrCheck.BackColor = Color.Red
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> NO"
            End If

            TextBoxTotalCost.Text = Int(Val(Result(0).Item("Rcost")) + Val(Result(0).Item("Ucost")) + Val(Result(0).Item("Lcost")) + Val(Result(0).Item("Bcost")) + Val(Result(0).Item("Ecost")) + Val(Result(0).Item("Ncost")) + Val(Result(0).Item("Pcost")) + Val(Result(0).Item("Qcost")) + Val(Result(0).Item("Scost")))
            Dim valuecost As Double = Val(TextBoxTotalCost.Text)
            TextBoxTotalCost.Text = valuecost.ToString("0,0", CultureInfo.InvariantCulture)
            ComboBoxPay.Text = Result(0).Item("cuspay")

            ' Product fill
            prod = Result(0).Item("prod")

            Dim str(2) As String
            ListViewProd.Items.Clear()
            For i = 0 To Int(Len(prod) / 60) - 1
                str(0) = Trim(Mid(prod, i * 60 + 1, 20))
                str(1) = Trim(Mid(prod, i * 60 + 21, 40))
                Dim ii As New ListViewItem(str)
                ListViewProd.Items.Add(ii)
            Next
            If InStr(Result(0).Item("confirm").ToString, "CONFIRMED") > 0 Then
                CheckConfirm.Checked = True
                CheckConfirm.Visible = False
                LabelConfirm.Visible = True
                LabelConfirm.ForeColor = Color.Green
                LabelConfirm.Text = Replace(Result(0).Item("confirm").ToString, "SENT_", "")
            Else
                If userDep3 = "N" Then
                    CheckConfirm.Visible = True
                    CheckConfirm.Enabled = True
                    LabelConfirm.Visible = False
                    CheckConfirm.Checked = False
                Else
                    LabelConfirm.Visible = True
                    CheckConfirm.Visible = False
                    CheckConfirm.Checked = False
                    LabelConfirm.ForeColor = Color.Red
                    LabelConfirm.Text = "NOT_CONFIRMED"
                End If
            End If

            'set initial ButtonScheduledDate Text
            ButtonData.Text = Result(0).Item("date")

            'update dates (with DB values) on every Date Button
            ButtonRL.Text = If(ButtonR.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateR"))
            ButtonUL.Text = If(ButtonU.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateU"))
            ButtonLL.Text = If(ButtonL.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateL"))
            ButtonBL.Text = If(ButtonB.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateB"))
            ButtonEL.Text = If(ButtonE.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateE"))
            ButtonPL.Text = If(ButtonP.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateP"))
            ButtonNL.Text = If(ButtonN.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateN"))
            ButtonQL.Text = If(ButtonQ.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateQ"))
            ButtonSL.Text = If(ButtonS.Text.Trim() = "NOT CHECKED", "", Result(0).Item("dateS"))
        End If

        Try
            If Not AllSign() Then
                ComboBoxPay.Enabled = True
                If userDep3 <> "A" Then Me.Controls("Button" & userDep3 & "L").Enabled = True

            Else
                ComboBoxPay.Enabled = False
                If userDep3 <> "A" Then Me.Controls("DateTimePicker" & userDep3).Visible = False
                If userDep3 <> "A" Then Me.Controls("Button" & userDep3 & "L").Enabled = False
            End If

        Catch ex As Exception

        End Try

        If Not AllSign() Then
            RichTextBoxStep.ReadOnly = False
            TextBoxStepCost.ReadOnly = False
            ButtonCalc.Enabled = True
            ButtonData.BackColor = Color.Yellow
            LabelApproved.ForeColor = Color.Red
            LabelApproved.Text = "NOT_APPROVED"
        Else
            TextBoxStepCost.ReadOnly = True
            ButtonData.BackColor = Color.Green
            ButtonCalc.Enabled = False
            LabelApproved.ForeColor = Color.Green
            LabelApproved.Text = "APPROVED"
        End If

        If userDep3 = "A" Then
            TextBoxStepCost.ReadOnly = True
            ButtonCalc.Enabled = False
        End If

        ButtonSave.BackColor = Color.Green
        ButtonSaveSend.BackColor = Color.Green
        needSave = False

        'update CLCV checkbox
        CheckBoxCLCV.Checked = If(readField("CLCV", EcrN) = "YES", True, False)
    End Sub

    Function AllSign(Optional ByVal EcrNumber As Integer = 0) As Boolean
        Dim pos As Integer, EcrN As Integer
        If EcrNumber = 0 Then
            pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
            EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
        Else
            EcrN = EcrNumber
        End If

        AllSign = True
        If InStr(1, readField("Rsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Esign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Ssign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Ssign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Then
            AllSign = False
        End If
    End Function

    Function AllApproved(Optional ByVal EcrNumber As Integer = 0) As Boolean
        Dim pos As Integer, EcrN As Integer
        If EcrNumber = 0 Then
            pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
            EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
        Else
            EcrN = EcrNumber
        End If

        AllApproved = True
        If InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Then
            AllApproved = False
        End If
    End Function

    Sub ColorButton(ByVal US As String)
        ResetColorButton()

        If US = "R" Then ButtonR.BackColor = Color.LightGreen
        If US = "U" Then ButtonU.BackColor = Color.LightGreen
        If US = "L" Then ButtonL.BackColor = Color.LightGreen
        If US = "B" Then ButtonB.BackColor = Color.LightGreen
        If US = "E" Then ButtonE.BackColor = Color.LightGreen
        If US = "N" Then ButtonN.BackColor = Color.LightGreen
        If US = "P" Then ButtonP.BackColor = Color.LightGreen
        If US = "Q" Then ButtonQ.BackColor = Color.LightGreen
        If US = "S" Then ButtonS.BackColor = Color.LightGreen
        If US = "A" Then ButtonA.BackColor = Color.LightGreen

        If userDep3 = "R" Then ButtonRL.BackColor = Color.LightGreen
        If userDep3 = "U" Then ButtonUL.BackColor = Color.LightGreen
        If userDep3 = "L" Then ButtonLL.BackColor = Color.LightGreen
        If userDep3 = "B" Then ButtonBL.BackColor = Color.LightGreen
        If userDep3 = "E" Then ButtonEL.BackColor = Color.LightGreen
        If userDep3 = "N" Then ButtonNL.BackColor = Color.LightGreen
        If userDep3 = "P" Then ButtonPL.BackColor = Color.LightGreen
        If userDep3 = "Q" Then ButtonQL.BackColor = Color.LightGreen
        If userDep3 = "S" Then ButtonSL.BackColor = Color.LightGreen

        If controlRight("R") = 3 And controlRight("J") = 3 Then
            ButtonRemove.Enabled = True
            ButtonAdd.Enabled = True
            ComboBoxProd.Enabled = True
        Else
            ButtonRemove.Enabled = False
            ButtonAdd.Enabled = False
            ComboBoxProd.Enabled = False
        End If
    End Sub

    Function readField(ByVal field As String, ByVal EcrN As Integer) As String
        Dim result As DataRow()
        readField = ""

        If IsNothing(tblEcr) Then
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr", con)
                    AdapterEcr.Fill(DsEcr, "ecr")
                    tblEcr = DsEcr.Tables("ecr")
                End Using
            End Using
        End If
        Try
            If EcrN > 0 Then
                result = tblEcr.Select("number =" & EcrN)
                readField = result(0).Item(field).ToString
            End If
        Catch ex As Exception
            MsgBox("Error in the reading of ECR:" & EcrN)
        End Try
    End Function

    Sub WriteField(ByVal field As String, ByVal v As String)
        Dim SQL As String
        Dim pos As Integer, EcrN As Integer
        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
        Try
            SQL = "UPDATE `" & DBName & "`.`ecr` SET `" & field & "` = '" & v & "' WHERE `ecr`.`number` = " & EcrN & " ;"
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                cmd = New MySqlCommand(SQL, con)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ComunicationLog("0052") 'db operation error
        End Try
    End Sub

    ' comunication function
    Sub ComunicationLog(ByVal ComCode As String)

        Dim rsResult As DataRow()
        rsResult = tblError.Select("code='" & ComCode & "'")
        If rsResult.Length = 0 Then
            ComCode = "0051"
            rsResult = tblError.Select("code='" & ComCode & "'")
        End If
        ListBoxLog.Items.Add(ComCode & " -> " & rsResult(0).Item("en").ToString)

        If Val(ComCode) >= 5000 Then
            ListBoxLog.BackColor = Color.LightGreen
        ElseIf Val(ComCode) < 5000 Then
            ListBoxLog.BackColor = Color.OrangeRed
        End If
    End Sub
    Private Sub ButtonR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonR.Click
        ManagePushButton("R")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonU_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonU.Click
        ManagePushButton("U")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonL.Click
        ManagePushButton("L")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonB.Click
        ManagePushButton("B")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonE.Click
        ManagePushButton("E")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonN.Click
        ManagePushButton("N")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonP.Click
        ManagePushButton("P")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub
    Private Sub ButtonQ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonQ.Click
        ManagePushButton("Q")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub

    Private Sub ButtonS_Click(sender As Object, e As EventArgs) Handles ButtonS.Click
        ManagePushButton("S")
        CheckScheduledDateShouldChange()
        WriteField("date", ButtonData.Text)
    End Sub

    Private Sub ButtonA_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonA.Click
        ManagePushButton("A")
    End Sub

    Sub ManagePushButton(ByVal but As String)
        Dim pos As Integer, EcrN As Integer
        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))

        Dim datepresence As Boolean
        checkSave()
        If userDep3 = but Then
            ButtonSave.Visible = True
            ButtonSaveSend.Visible = True
        Else
            ButtonSave.Visible = False
            ButtonSaveSend.Visible = False
        End If

        If userDep3 = "" Then
        Else
            If userDep3 = but And Me.Controls("Button" & userDep3).BackColor = Color.LightGreen Then
                ButtonSave.Enabled = True
                ButtonSaveSend.Enabled = True
                If Me.Controls("Button" & but).Text = "APPROVED" Then
                    If MsgBox("Do you want to sign this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        datepresence = True
                        If SomeNoApproved() = False Then
                            If datepresence Then
                                Me.Controls("Button" & but).Text = CreAccount.strUserName
                                WriteField(but & "sign", Me.Controls("Button" & but).Text)
                                WriteField("date" & but, date_to_string(Now))
                            Else
                                MsgBox("Please fill in the data!")
                            End If
                        Else
                            MsgBox("It is not possible to sign if there is some dept. that has not yet approved!")
                            If MsgBox("Do you want to remove your approval?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                                If Not AllApproved() Then
                                    WriteField(but & "sign", "CHECKED")
                                    Me.Controls("Button" & but).Text = "CHECKED"
                                    WriteField("date" & but, date_to_string(Now))
                                Else
                                    ListBoxLog.Items.Add("You can't remove your approval anymore!")
                                End If
                            End If
                        End If
                    ElseIf MsgBox("Do you want to remove the approval?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        If Not AllApproved() Then
                            WriteField(but & "sign", "CHECKED")
                            Me.Controls("Button" & but).Text = "CHECKED"
                            WriteField("date" & but, date_to_string(Now))
                        Else
                            ListBoxLog.Items.Add("You can't remove your approval anymore!")
                        End If
                    End If

                ElseIf Me.Controls("Button" & but).Text = "CHECKED" Then
                    If MsgBox("Do you want to approve this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then

                        datepresence = True
                        'If SomeNoChecked() = False Then
                        If datepresence Then
                            Me.Controls("Button" & but).Text = "APPROVED"
                            WriteField(but & "sign", Me.Controls("Button" & but).Text)
                            WriteField("date" & but, date_to_string(Now))
                        Else
                            MsgBox("Please fill in the data!")
                        End If
                        'Else
                        '    MsgBox("It is not possible to approve if there is some dept. that has not yet CHECKED!")
                        '    If MsgBox("Do you want to remove your CHECKED?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        '        WriteField(but & "sign", "NOT CHECKED")
                        '        Me.Controls("Button" & but).Text = "NOT CHECKED"
                        '        WriteField("date" & but, date_to_string(Now))
                        '    End If
                        'End If 
                    ElseIf MsgBox("Do you want to remove the status CHECKED?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        WriteField(but & "sign", "NOT CHECKED")
                        WriteField("leadTime" & but, "0")
                        Me.Controls("ComboBox" & but).Text = 0
                        Me.Controls("Button" & but).Text = "NOT CHECKED"
                        WriteField("date" & but, "")
                        If (ButtonR.Text.Trim() = "NOT CHECKED" And ButtonL.Text.Trim() = "NOT CHECKED" And ButtonU.Text.Trim() = "NOT CHECKED" And ButtonB.Text.Trim() = "NOT CHECKED" And ButtonE.Text.Trim() = "NOT CHECKED" And ButtonN.Text.Trim() = "NOT CHECKED" And ButtonP.Text.Trim() = "NOT CHECKED" And ButtonQ.Text.Trim() = "NOT CHECKED" And ButtonS.Text.Trim() = "NOT CHECKED") Then
                            ButtonData.Text = "01/01/2000"
                            WriteField("date", "01/01/2000")
                        End If
                    End If

                ElseIf Me.Controls("Button" & but).Text = "NOT CHECKED" Then

                    datepresence = True
                    If datepresence Then
                        If MsgBox("Do you want to mark as 'CHECKED' this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                            Me.Controls("Button" & but).Text = "CHECKED"
                            WriteField(but & "sign", "CHECKED")
                            WriteField("date" & but, date_to_string(Now))
                        End If
                    Else
                        MsgBox("Please fill in the data!")
                    End If

                ElseIf readDocSign(readField("iddoc", EcrN)) = "" And ParameterTable("SYSTEM_SCHEDULE") <> "RUN" Then   ' signed
                    If MsgBox("Do you want to remove your signature?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        If Not AllSign() Then
                            WriteField(but & "sign", "NOT CHECKED")
                            WriteField("leadTime" & but, "0")
                            Me.Controls("Button" & but).Text = "NOT CHECKED"
                            Me.Controls("ComboBox" & but).Text = 0
                            WriteField("date" & but, "")
                            If (ButtonR.Text.Trim() = "NOT CHECKED" And ButtonL.Text.Trim() = "NOT CHECKED" And ButtonU.Text.Trim() = "NOT CHECKED" And ButtonB.Text.Trim() = "NOT CHECKED" And ButtonE.Text.Trim() = "NOT CHECKED" And ButtonN.Text.Trim() = "NOT CHECKED" And ButtonP.Text.Trim() = "NOT CHECKED" And ButtonQ.Text.Trim() = "NOT CHECKED" And ButtonS.Text.Trim() = "NOT CHECKED") Then
                                ButtonData.Text = "01/01/2000"
                                WriteField("date", "01/01/2000")
                            End If
                        Else
                            ListBoxLog.Items.Add("You can't remove your signature anymore!")
                        End If
                    End If
                Else
                    MsgBox("ThiS ECR has been already signed from all departments, then it is not possible to remove it! Please contact the IT Dept. in case of need.")
                End If
            Else
            End If

            If userDep3 = but And Not AllSign() Then
                RichTextBoxStep.ReadOnly = False
                TextBoxStepCost.ReadOnly = False
                ButtonCalc.Enabled = True
            Else
                RichTextBoxStep.ReadOnly = True
                TextBoxStepCost.ReadOnly = True

            End If
            If userDep3 = "A" Then
                TextBoxStepCost.ReadOnly = True
                ButtonCalc.Enabled = False
            End If

            If userDep3 = "N" Then
                TextBoxStepCost.ReadOnly = False
                RichTextBoxStep.ReadOnly = False
            End If

        End If

        ColorButton(but)
        tblEcr.Clear()
        DsEcr.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM ecr;", con)
                AdapterEcr.Fill(DsEcr, "ecr")
            End Using
            tblEcr = DsEcr.Tables("ecr")
        End Using

        RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & readField(but & "note", EcrN)
        TextBoxStepCost.Text = readField(but & "cost", EcrN)
        'UpdateScheduledDateDate()

        'update dates (with DB values) on every Date Button
        ButtonRL.Text = If(ButtonR.Text.Trim() = "NOT CHECKED", "", readField("dateR", EcrN))
        ButtonUL.Text = If(ButtonU.Text.Trim() = "NOT CHECKED", "", readField("dateU", EcrN))
        ButtonLL.Text = If(ButtonL.Text.Trim() = "NOT CHECKED", "", readField("dateL", EcrN))
        ButtonBL.Text = If(ButtonB.Text.Trim() = "NOT CHECKED", "", readField("dateB", EcrN))
        ButtonEL.Text = If(ButtonE.Text.Trim() = "NOT CHECKED", "", readField("dateE", EcrN))
        ButtonPL.Text = If(ButtonP.Text.Trim() = "NOT CHECKED", "", readField("dateP", EcrN))
        ButtonNL.Text = If(ButtonN.Text.Trim() = "NOT CHECKED", "", readField("dateN", EcrN))
        ButtonQL.Text = If(ButtonQ.Text.Trim() = "NOT CHECKED", "", readField("dateQ", EcrN))
        ButtonSL.Text = If(ButtonS.Text.Trim() = "NOT CHECKED", "", readField("dateS", EcrN))


        ButtonSave.BackColor = Color.Green
        ButtonSaveSend.BackColor = Color.Green
        needSave = False
        setLeadTimeAvailability()
    End Sub
    Sub ResetColorButton()
        ButtonR.BackColor = Color.LightGray
        ButtonU.BackColor = Color.LightGray
        ButtonL.BackColor = Color.LightGray
        ButtonB.BackColor = Color.LightGray
        ButtonE.BackColor = Color.LightGray
        ButtonN.BackColor = Color.LightGray
        ButtonP.BackColor = Color.LightGray
        ButtonQ.BackColor = Color.LightGray
        ButtonS.BackColor = Color.LightGray
        ButtonA.BackColor = Color.LightGray
    End Sub

    Sub ComboProductFill()
        ComboBoxProd.Items.Clear()
        For i = 0 To tblProd.Rows.Count - 1
            ComboBoxProd.Items.Add(tblProd.Rows(i).Item("bitronpn").ToString & " - " & tblProd.Rows(i).Item("name").ToString)
        Next
        ComboBoxProd.Sorted = True
    End Sub

    Sub UpdateComboDepartmentsNumbers()
        Dim pos As Integer, EcrN As Integer
        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))

        ComboBoxR.SelectedItem = readField("leadTimeR", EcrN)
        ComboBoxL.SelectedItem = readField("leadTimeL", EcrN)
        ComboBoxU.SelectedItem = readField("leadTimeU", EcrN)
        ComboBoxB.SelectedItem = readField("leadTimeB", EcrN)
        ComboBoxE.SelectedItem = readField("leadTimeE", EcrN)
        ComboBoxN.SelectedItem = readField("leadTimeN", EcrN)
        ComboBoxP.SelectedItem = readField("leadTimeP", EcrN)
        ComboBoxQ.SelectedItem = readField("leadTimeQ", EcrN)
        ComboBoxS.SelectedItem = readField("leadTimeS", EcrN)
    End Sub

    Sub CheckScheduledDateShouldChange()
        If (ButtonR.Text.Trim() <> "NOT CHECKED" Or ButtonL.Text.Trim() <> "NOT CHECKED" Or ButtonU.Text.Trim() <> "NOT CHECKED" Or ButtonB.Text.Trim() <> "NOT CHECKED" Or ButtonE.Text.Trim() <> "NOT CHECKED" Or ButtonN.Text.Trim() <> "NOT CHECKED" Or ButtonP.Text.Trim() <> "NOT CHECKED" Or ButtonQ.Text.Trim() <> "NOT CHECKED" Or ButtonS.Text.Trim() <> "NOT CHECKED") Then
            UpdateScheduledDateDate()
        ElseIf (ButtonR.Text.Trim() = "CHECKED" And ButtonL.Text.Trim() = "CHECKED" And ButtonU.Text.Trim() = "CHECKED" And ButtonB.Text.Trim() = "CHECKED" And ButtonE.Text.Trim() = "CHECKED" And ButtonN.Text.Trim() = "CHECKED" And ButtonP.Text.Trim() = "CHECKED" And ButtonQ.Text.Trim() = "CHECKED" And ButtonS.Text.Trim() = "CHECKED") Then
            UpdateScheduledDateDate()
        ElseIf ButtonR.Text.Trim() = "APPROVED" Or ButtonL.Text.Trim() = "APPROVED" Or ButtonU.Text.Trim() = "APPROVED" Or ButtonB.Text.Trim() = "APPROVED" Or ButtonE.Text.Trim() = "APPROVED" Or ButtonN.Text.Trim() = "APPROVED" Or ButtonP.Text.Trim() = "APPROVED" Or ButtonQ.Text.Trim() = "APPROVED" Or ButtonS.Text.Trim() = "APPROVED" Then
            UpdateScheduledDateDate()
        ElseIf (ButtonR.Text.Trim() <> "NOT CHECKED" And ButtonR.Text.Trim() <> "CHECKED" And ButtonR.Text.Trim() <> "APPROVED") Or
            (ButtonL.Text.Trim() <> "NOT CHECKED" And ButtonL.Text.Trim() <> "CHECKED" And ButtonL.Text.Trim() <> "APPROVED") Or
            (ButtonU.Text.Trim() <> "NOT CHECKED" And ButtonU.Text.Trim() <> "CHECKED" And ButtonU.Text.Trim() <> "APPROVED") Or
            (ButtonB.Text.Trim() <> "NOT CHECKED" And ButtonB.Text.Trim() <> "CHECKED" And ButtonB.Text.Trim() <> "APPROVED") Or
            (ButtonE.Text.Trim() <> "NOT CHECKED" And ButtonE.Text.Trim() <> "CHECKED" And ButtonE.Text.Trim() <> "APPROVED") Or
            (ButtonN.Text.Trim() <> "NOT CHECKED" And ButtonN.Text.Trim() <> "CHECKED" And ButtonN.Text.Trim() <> "APPROVED") Or
            (ButtonP.Text.Trim() <> "NOT CHECKED" And ButtonP.Text.Trim() <> "CHECKED" And ButtonP.Text.Trim() <> "APPROVED") Or
            (ButtonQ.Text.Trim() <> "NOT CHECKED" And ButtonQ.Text.Trim() <> "CHECKED" And ButtonQ.Text.Trim() <> "APPROVED") Or
            (ButtonS.Text.Trim() <> "NOT CHECKED" And ButtonS.Text.Trim() <> "CHECKED" And ButtonS.Text.Trim() <> "APPROVED") Then
            UpdateScheduledDateDate()
        Else
            LabelComputeScheduledDate.Text = ""
        End If
    End Sub

    Sub CheckPendingSave(ByVal formValue As String, DBValue As String)
        Dim pos As Integer = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        Dim EcrN As Integer = Val(Mid(ComboBoxEcr.Text, 1, pos))

        If readField(formValue, EcrN) <> DBValue Then
            ButtonSave.BackColor = Color.Red
            ButtonSaveSend.BackColor = Color.Red
            needSave = True
        End If
    End Sub

    Private Sub ComboBoxEcr_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxEcr.SelectedValueChanged
        UpdateField()
        UpdateComboDepartmentsNumbers()
        CheckScheduledDateShouldChange()
    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAdd.Click
        Dim pos As Integer, exist As Boolean

        If ComboBoxProd.Text <> "" Then
            If controlRight("R") = 3 And controlRight("J") = 3 Then
                If ListViewProd.Items.Count > 0 Then
                    For i = 0 To ListViewProd.Items.Count - 1
                        If Trim(ListViewProd.Items(i).SubItems(0).Text) = Mid(Trim(ComboBoxProd.Text), 1, InStr(Trim(ComboBoxProd.Text), "-", CompareMethod.Text) - 2) Then
                            exist = True
                            ComunicationLog("5070") ' product exist in list
                        End If
                    Next
                End If
                If Not exist And ComboBoxEcr.Text <> "" Then
                    pos = InStr(ComboBoxProd.Text, "-", CompareMethod.Text)
                    Dim str(2) As String
                    str(0) = Mid(ComboBoxProd.Text, 1, pos - 2)
                    str(1) = Mid(ComboBoxProd.Text, pos + 2)
                    Dim ii As New ListViewItem(str)
                    ListViewProd.Items.Add(ii)
                    invalidationProd(Mid(ComboBoxProd.Text, 1, pos - 2), Mid(ComboBoxEcr.Text, 1, InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text) - 2))

                    Dim prod = ""
                    For i = 0 To ListViewProd.Items.Count - 1
                        prod = prod & StrDup(20 - Len(Mid(ListViewProd.Items(i).SubItems(0).Text(), 1, 20)), " ") & Mid(ListViewProd.Items(i).SubItems(0).Text, 1, 40)
                        prod = prod & StrDup(40 - Len(Mid(ListViewProd.Items(i).SubItems(1).Text(), 1, 40)), " ") & Mid(ListViewProd.Items(i).SubItems(1).Text, 1, 40)
                    Next
                    WriteField("prod", prod)

                End If

            Else
                ComunicationLog("0046") 'Now cant can modifiy
            End If

        Else
            ComunicationLog("0045") 'Please select a product
        End If
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRemove.Click
        Dim i As Integer
        If ListViewProd.Items.Count > 0 Then
            If ComboBoxEcr.Text <> "" Then
                DeinvalidationProd(ListViewProd.Items(ListViewProd.Items.Count - 1).SubItems(0).Text,
                Mid(ComboBoxEcr.Text, 1, InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text) - 2))
                For i = ListViewProd.CheckedItems.Count - 1 To 0 Step -1
                    ListViewProd.CheckedItems(i).Remove()
                Next
                Dim prod = ""
                For i = 0 To ListViewProd.Items.Count - 1
                    prod = prod & StrDup(20 - Len(Mid(ListViewProd.Items(i).SubItems(0).Text(), 1, 20)), " ") & Mid(ListViewProd.Items(i).SubItems(0).Text, 1, 40)
                    prod = prod & StrDup(40 - Len(Mid(ListViewProd.Items(i).SubItems(1).Text(), 1, 40)), " ") & Mid(ListViewProd.Items(i).SubItems(1).Text, 1, 40)
                Next
                WriteField("prod", prod)

            Else
                ComunicationLog("0046") 'Now can't modifiy
            End If
        End If
    End Sub

    Function NoChecked() As Boolean
        If ButtonA.Text = "NOT CHECKED" And
        ButtonR.Text = "NOT CHECKED" And
        ButtonU.Text = "NOT CHECKED" And
        ButtonL.Text = "NOT CHECKED" And
        ButtonB.Text = "NOT CHECKED" And
        ButtonE.Text = "NOT CHECKED" And
        ButtonN.Text = "NOT CHECKED" And
        ButtonP.Text = "NOT CHECKED" And
        ButtonQ.Text = "NOT CHECKED" And
        ButtonS.Text = "NOT CHECKED" Then
            NoChecked = True
        End If
    End Function

    Function SomeNoApproved() As Boolean
        SomeNoApproved = False
        If ButtonA.Text = "NOT CHECKED" Or
        ButtonR.Text = "NOT CHECKED" Or
        ButtonU.Text = "NOT CHECKED" Or
        ButtonL.Text = "NOT CHECKED" Or
        ButtonB.Text = "NOT CHECKED" Or
        ButtonE.Text = "NOT CHECKED" Or
        ButtonN.Text = "NOT CHECKED" Or
        ButtonP.Text = "NOT CHECKED" Or
        ButtonQ.Text = "NOT CHECKED" Or
        ButtonS.Text = "NOT CHECKED" Or
        ButtonA.Text = "CHECKED" Or
        ButtonR.Text = "CHECKED" Or
        ButtonU.Text = "CHECKED" Or
        ButtonL.Text = "CHECKED" Or
        ButtonB.Text = "CHECKED" Or
        ButtonE.Text = "CHECKED" Or
        ButtonN.Text = "CHECKED" Or
        ButtonP.Text = "CHECKED" Or
        ButtonQ.Text = "CHECKED" Or
        ButtonS.Text = "CHECKED" Then
            SomeNoApproved = True
        End If
    End Function

    Function SomeNoChecked() As Boolean
        SomeNoChecked = False
        If ButtonA.Text = "NOT CHECKED" Or
        ButtonR.Text = "NOT CHECKED" Or
        ButtonU.Text = "NOT CHECKED" Or
        ButtonL.Text = "NOT CHECKED" Or
        ButtonB.Text = "NOT CHECKED" Or
        ButtonE.Text = "NOT CHECKED" Or
        ButtonN.Text = "NOT CHECKED" Or
        ButtonP.Text = "NOT CHECKED" Or
        ButtonQ.Text = "NOT CHECKED" Or
        ButtonS.Text = "NOT CHECKED" Then
            SomeNoChecked = True
        End If
    End Function

    Function NoCheckedOthers(ByVal but As String) As Boolean
        NoCheckedOthers = True
        If but <> "R" Then NoCheckedOthers = NoCheckedOthers And ButtonR.Text = "NOT CHECKED"
        If but <> "U" Then NoCheckedOthers = NoCheckedOthers And ButtonU.Text = "NOT CHECKED"
        If but <> "L" Then NoCheckedOthers = NoCheckedOthers And ButtonL.Text = "NOT CHECKED"
        If but <> "B" Then NoCheckedOthers = NoCheckedOthers And ButtonB.Text = "NOT CHECKED"
        If but <> "N" Then NoCheckedOthers = NoCheckedOthers And ButtonN.Text = "NOT CHECKED"
        If but <> "E" Then NoCheckedOthers = NoCheckedOthers And ButtonE.Text = "NOT CHECKED"
        If but <> "P" Then NoCheckedOthers = NoCheckedOthers And ButtonP.Text = "NOT CHECKED"
        If but <> "Q" Then NoCheckedOthers = NoCheckedOthers And ButtonQ.Text = "NOT CHECKED"
        If but <> "S" Then NoCheckedOthers = NoCheckedOthers And ButtonS.Text = "NOT CHECKED"
        If but <> "A" Then NoCheckedOthers = NoCheckedOthers And ButtonA.Text = "NOT CHECKED"
    End Function

    Sub invalidationProd(ByVal prod As String, ByVal ecrN As Integer)
        Dim RowSearchDoc As DataRow()
        Dim RowSearchProd As DataRow()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC;", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")
        End Using
        RowSearchProd = tblProd.Select("bitronpn = '" & Trim(prod) & "'")
        RowSearchDoc = tblDoc.Select("(filename ='" & RowSearchProd(0).Item("bitronpn").ToString & "' or filename ='" &
        RowSearchProd(0).Item("pcbcode").ToString & "' or filename ='" & RowSearchProd(0).Item("piastracode").ToString & "')")

        For Each row In RowSearchDoc

            If InStr(1, row("Ecrpending").ToString, "[" & ecrN & "]", CompareMethod.Text) <= 0 Then
                Dim SQL As String
                Dim pos As Integer
                pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
                ecrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
                Try
                    Dim conBuilder As New Common.DbConnectionStringBuilder()
                    builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                    Using con = NewConnectionMySql(conBuilder("host"), conBuilder("database"), conBuilder("username"), conBuilder("password"))
                        SQL = "UPDATE `" & DBName & "`.`doc` SET `ecrpending` = '" & row("ecrpending") & "[" & ecrN & "]" & "' WHERE `doc`.`id` = '" & row("id").ToString & "' ;"
                        cmd = New MySqlCommand(SQL, con)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
        Next
    End Sub

    Sub DeinvalidationProd(ByVal prod As String, ByVal ecrN As Integer)
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC;", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")

            Dim RowSearchDoc As DataRow()
            Dim RowSearchProd As DataRow()
            RowSearchProd = tblProd.Select("bitronpn = '" & Trim(prod) & "'")
            RowSearchDoc = tblDoc.Select("(filename ='" & RowSearchProd(0).Item("bitronpn").ToString & "' or filename ='" &
            RowSearchProd(0).Item("pcbcode").ToString & "' or filename ='" & RowSearchProd(0).Item("piastracode").ToString & "')")

            For Each row In RowSearchDoc
                If InStr(1, row("Ecrpending").ToString, "[" & ecrN & "]", CompareMethod.Text) > 0 Then
                    Dim SQL As String
                    Dim pos As Integer
                    pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
                    ecrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
                    Try
                        SQL = "UPDATE `" & DBName & "`.`doc` SET `ecrpending` = '" & Replace(row("ecrpending"), "[" & ecrN & "]", "") & "' WHERE `doc`.`id` = '" & row("id").ToString & "' ;"
                        cmd = New MySqlCommand(SQL, con)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        ComunicationLog("0052") 'db operation error
                    End Try
                End If
            Next
        End Using
    End Sub

    Function downloadFileWinPath(ByVal fileName As String) As String
        Dim strPathFtp As String
        Dim objFtp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""

        If fileName <> "" Then
            Try
                strPathFtp = (ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                ComunicationLog(objFtp.DownloadFile(strPathFtp, IO.Path.GetTempPath, ParameterTable("plant") & "R_PRO_ECR_" & fileName)) ' download successfull
                downloadFileWinPath = IO.Path.GetTempPath & ParameterTable("plant") & "R_PRO_ECR_" & fileName
            Catch ex As Exception
                ComunicationLog("0049") ' Error in ecr Download
            End Try
        Else
            ComunicationLog("5061") ' fill path
        End If
    End Function

    Private Sub ButtonOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonOpen.Click
        Dim fileOpen As String
        fileOpen = downloadFileWinPath(ComboBoxEcr.Text)
        If ComboBoxEcr.Text <> "" Then Process.Start(fileOpen)
    End Sub

    Sub UpdateScheduledDateDate() Handles DateTimePickerL.ValueChanged, DateTimePickerU.ValueChanged, DateTimePickerE.ValueChanged, DateTimePickerQ.ValueChanged, DateTimePickerp.ValueChanged, DateTimePickerR.ValueChanged, DateTimePickerS.ValueChanged
        Dim dateRL As String = ButtonRL.Text.Trim()
        Dim dateLL As String = ButtonLL.Text.Trim()
        Dim dateUL As String = ButtonUL.Text.Trim()
        Dim dateBL As String = ButtonBL.Text.Trim()
        Dim dateEL As String = ButtonEL.Text.Trim()
        Dim dateNL As String = ButtonNL.Text.Trim()
        Dim datePL As String = ButtonPL.Text.Trim()
        Dim dateQL As String = ButtonQL.Text.Trim()
        Dim dateSL As String = ButtonSL.Text.Trim()

        Dim dates As List(Of DateTime) = New List(Of DateTime)
        If dateRL <> "" Then dates.Add(Convert.ToDateTime(dateRL))
        If dateLL <> "" Then dates.Add(Convert.ToDateTime(dateLL))
        If dateUL <> "" Then dates.Add(Convert.ToDateTime(dateUL))
        If dateBL <> "" Then dates.Add(Convert.ToDateTime(dateBL))
        If dateEL <> "" Then dates.Add(Convert.ToDateTime(dateEL))
        If dateNL <> "" Then dates.Add(Convert.ToDateTime(dateNL))
        If datePL <> "" Then dates.Add(Convert.ToDateTime(datePL))
        If dateQL <> "" Then dates.Add(Convert.ToDateTime(dateQL))
        If dateSL <> "" Then dates.Add(Convert.ToDateTime(dateSL))

        If dates.Count > 0 Then
            Dim maxDate = dates.Max()
            Dim weeksToAdd As Integer() = {ComboBoxR.SelectedItem, ComboBoxL.SelectedItem, ComboBoxU.SelectedItem, ComboBoxB.SelectedItem, ComboBoxE.SelectedItem, ComboBoxN.SelectedItem, ComboBoxP.SelectedItem, ComboBoxQ.SelectedItem, ComboBoxS.SelectedItem}
            Dim maxLeadTime = weeksToAdd.Max()

            ButtonData.Text = date_to_string(maxDate.AddDays(maxLeadTime * 7))
            LabelComputeScheduledDate.Text = date_to_string(maxDate) & " + " & maxLeadTime & " weeks" & Environment.NewLine & "           (" & (maxLeadTime * 7) & " days)"
        Else
            ButtonData.Text = ""
            LabelComputeScheduledDate.Text = ""
        End If
    End Sub

    Private Sub ComboBoxPay_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxPay.LostFocus
        WriteField("cusPay", ComboBoxPay.Text)
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSave.Click
        SaveData()
        UpdateField()
    End Sub

    Private Function GetDepartamentName(right As String) As String
        Dim departament = ""
        If ButtonRL.BackColor = Color.LightGreen Then departament = "R&D"
        If ButtonUL.BackColor = Color.LightGreen Then departament = "Purchasing"
        If ButtonLL.BackColor = Color.LightGreen Then departament = "Logistic"
        If ButtonBL.BackColor = Color.LightGreen Then departament = "Process Engineering"
        If ButtonEL.BackColor = Color.LightGreen Then departament = "Testing Engineering"
        If ButtonNL.BackColor = Color.LightGreen Then departament = "Quality"
        If ButtonPL.BackColor = Color.LightGreen Then departament = "Production"
        If ButtonQL.BackColor = Color.LightGreen Then departament = "Time And Methods"
        If ButtonSL.BackColor = Color.LightGreen Then departament = "Environment And Safety"
        Return departament
    End Function

    Private Sub ButtonSaveSend_Click(sender As Object, e As EventArgs) Handles ButtonSaveSend.Click
        SaveData()
        UpdateField()

        Dim bodyText As String, subject As String
        bodyText = "Automatic SrvDoc Message:" & vbLf & vbLf & GetDepartamentName(userDep3) & vbLf & "LeadTime: " & Me.Controls("ComboBox" & userDep3).Text & vbLf & "Note: " & RichTextBoxStep.Text
        subject = "ECR Note Change Notification:    " & ComboBoxEcr.Text
        SendMail("ECR_VerifyTo; ECR_R_SignTo; ECR_U_SignTo; ECR_L_SignTo; ECR_B_SignTo; ECR_E_SignTo; ECR_N_SignTo; ECR_P_SignTo; ECR_Q_SignTo; ECR_S_SignTo",
                 "ECR_VerifyCopy; ECR_R_SignCopy; ECR_U_SignCopy; ECR_L_SignCopy; ECR_B_SignCopy; ECR_E_SignCopy; ECR_N_SignCopy; ECR_P_SignCopy; ECR_Q_SignCopy; ECR_S_SignCopy;",
                 bodyText, subject)

    End Sub

    Private Sub SaveData()

        WriteField(userDep3 & "cost", TextBoxStepCost.Text)
        WriteField(userDep3 & "note", Replace(Replace(RichTextBoxStep.Rtf, "\", "\\"), "'", ""))

        Dim pos As Integer = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        Dim EcrN As Integer = Val(Mid(ComboBoxEcr.Text, 1, pos))
        If readField("date", EcrN).Trim() <> ButtonData.Text.Trim() Then WriteField("date", ButtonData.Text.Trim())

        WriteField("leadTimeR", Integer.Parse(ComboBoxR.SelectedItem))
        WriteField("leadTimeU", Integer.Parse(ComboBoxU.SelectedItem))
        WriteField("leadTimeL", Integer.Parse(ComboBoxL.SelectedItem))
        WriteField("leadTimeB", Integer.Parse(ComboBoxB.SelectedItem))
        WriteField("leadTimeE", Integer.Parse(ComboBoxE.SelectedItem))
        WriteField("leadTimeN", Integer.Parse(ComboBoxN.SelectedItem))
        WriteField("leadTimeP", Integer.Parse(ComboBoxP.SelectedItem))
        WriteField("leadTimeQ", Integer.Parse(ComboBoxQ.SelectedItem))
        WriteField("leadTimeS", Integer.Parse(ComboBoxS.SelectedItem))
        If controlRight("R") = 3 Then WriteField("CLCV", If(CheckBoxCLCV.Checked, "YES", "NO"))

        needSave = False
        ButtonSave.BackColor = Color.Green
    End Sub

    Function GetEmails(ByVal emailsStr As String) As String
        Dim listOfEmail As String = ""
        For Each item In emailsStr.Split(";")
            If listOfEmail.Contains(item.Trim) = False Then listOfEmail += "'" & item.Trim() & "',"
        Next
        If listOfEmail = "" Then
            listOfEmail += "'" & emailsStr.Trim() & "'"
        Else
            listOfEmail = listOfEmail.Remove(listOfEmail.Length - 1, 1)
        End If
        Return listOfEmail
    End Function

    Function SendMail(ByVal AddlistTo As String, ByVal AddlistCopy As String, ByVal bodyText As String, ByVal SubText As String, Optional ByVal ATTACH As String = "") As Boolean
        Dim dt As Date = Now
        SendMail = False
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using Adaptermail As New MySqlDataAdapter("SELECT * FROM mail;", con)
                Adaptermail.Fill(Dsmail, "mail")
            End Using
            tblmail = Dsmail.Tables("mail")
        End Using

        Dim client As New SmtpClient(ParameterTable("SMTP"), ParameterTable("SMTP_PORT"))
        client.EnableSsl = IIf(ParameterTable("MAIL_SSL") = "YES", True, False)
        If ParameterTable("MAIL_SENDER_CREDENTIAL_PSW") = "" Then
            client.Credentials = New NetworkCredential(ParameterTable("MAIL_SENDER_CREDENTIAL_USER"), vbNull)
        Else
            client.Credentials = New NetworkCredential(ParameterTable("MAIL_SENDER_CREDENTIAL_USER"), ParameterTable("MAIL_SENDER_CREDENTIAL_PSW"))

        End If
        Dim msg As New MailMessage(ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"), ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"))

        Dim RowSearchMail As DataRow() = tblmail.Select("list in (" & GetEmails(AddlistTo) & ")")

        msg.To.Clear()
        msg.CC.Clear()

        For Each row In RowSearchMail
            Dim mailAddress As New MailAddress(row("name").ToString.Replace(Environment.NewLine, ""))
            If msg.To.Contains(mailAddress) = False Then
                msg.To.Add(row("name").ToString.Replace(Environment.NewLine, ""))
            End If
        Next

        RowSearchMail = tblmail.Select("list in (" & GetEmails(AddlistCopy) & ")")
        For Each row In RowSearchMail
            Dim mailAddress As New MailAddress(row("name").ToString.Replace(Environment.NewLine, ""))
            If msg.CC.Contains(mailAddress) = False Then
                msg.CC.Add(mailAddress)
            End If
        Next

        If ATTACH <> "" Then
            Dim Allegato = New Attachment(ATTACH)
            If My.Computer.FileSystem.GetFileInfo(ATTACH).Length < Val(ParameterTable("MAX_SIZE_FILE_MAIL")) Then
                msg.Attachments.Add(Allegato)
                msg.Body = bodyText
            Else
                msg.Body = "ATTENTION...FILE NOT SENT BY EMAIL FOR EXCESSIVE DIMENSION. PLEASE DOWNLOAD FROM SERVER!!!" & vbCrLf & vbCrLf & bodyText
            End If
        Else
            msg.Body = bodyText
        End If

        msg.Subject = SubText
        Try
            client.Send(msg)
            MailSent = True
            MessageBox.Show("Email has been sent successfully!")
        Catch ex As Exception
            ListBoxLog.Items.Add("Email has not been sent!")
        End Try
    End Function

    Private Sub ComboBoxR_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxR.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeR", ComboBoxR.SelectedItem)
    End Sub

    Private Sub ComboBoxU_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxU.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeU", ComboBoxU.SelectedItem)
    End Sub

    Private Sub ComboBoxL_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxL.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeL", ComboBoxL.SelectedItem)
    End Sub

    Private Sub ComboBoxB_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxB.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeB", ComboBoxB.SelectedItem)
    End Sub

    Private Sub ComboBoxE_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxE.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeE", ComboBoxE.SelectedItem)
    End Sub

    Private Sub ComboBoxN_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxN.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeN", ComboBoxN.SelectedItem)
    End Sub

    Private Sub ComboBoxP_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxP.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeP", ComboBoxP.SelectedItem)
    End Sub

    Private Sub ComboBoxQ_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxQ.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeQ", ComboBoxQ.SelectedItem)
    End Sub

    Private Sub ComboBoxS_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles ComboBoxS.SelectionChangeCommitted
        CheckScheduledDateShouldChange()
        CheckPendingSave("leadTimeS", ComboBoxS.SelectedItem)
    End Sub

    Private Sub CheckBoxCLCV_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCLCV.CheckedChanged
        Dim pos As Integer = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        Dim EcrN As Integer = Val(Mid(ComboBoxEcr.Text, 1, pos))

        If readField("CLCV", EcrN) <> If(CheckBoxCLCV.Checked, "YES", "NO") Then
            ButtonSave.BackColor = Color.Red
            needSave = True
        End If
    End Sub

    Private Sub ButtonData_TextChanged(sender As Object, e As EventArgs) Handles ButtonData.TextChanged

    End Sub

    Private Sub CheckConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CheckConfirm.Click
        If CheckConfirm.Checked = True Then
            If AllSign() Then
                If vbYes = MsgBox("Are you sure to confirm the ECR? After the automatic notification you can't stop it.", MsgBoxStyle.YesNo, "Confirmation of ECR introduction") Then
                    WriteField("Confirm", "CONFIRMED")
                    CheckConfirm.Visible = False
                Else
                    CheckConfirm.Checked = False
                End If
            Else
                MsgBox("ECR needs to be signed!")
            End If
        End If
        UpdateField()
    End Sub

    Private Sub CheckBoxOpen_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxOpen.CheckedChanged
        fillEcrComboTable()
    End Sub

    Private Sub RichTextBoxStep_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RichTextBoxStep.TextChanged
        ButtonSave.BackColor = Color.Red
        ButtonSaveSend.BackColor = Color.Red
        needSave = True
    End Sub

    Sub checkSave()
        If needSave = True Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ButtonSave_Click(Me, EventArgs.Empty)
                ButtonSave.BackColor = Color.Green
                ButtonSaveSend.BackColor = Color.Green
                needSave = False
            Else
                ButtonSave.BackColor = Color.Green
                ButtonSaveSend.BackColor = Color.Green
                needSave = False
            End If
        End If
    End Sub

    Function readDocSign(ByVal docId As Long) As String
        Dim DsDoc As New DataSet
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
        End Using
        Dim tblDoc As DataTable = DsDoc.Tables("doc")
        Dim Res As DataRow() = tblDoc.Select("id = " & docId)
        If Res.Length > 0 Then
            readDocSign = Res(0).Item("sign").ToString
        Else
            MsgBox("ECR document not found: " & docId)
        End If
    End Function

    Private Sub ButtonEcrCheck_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEcrCheck.Click

        If controlRight("R") = 3 And controlRight("J") = 3 And ComboBoxEcr.Text <> "" Then

            If ButtonEcrCheck.BackColor = Color.Green Then
                If MsgBoxResult.Yes = MsgBox("Do you want to remove the approval?", vbYesNo) Then
                    If InStr(ButtonR.Text, "CHECK", ) > 0 Then
                        ButtonEcrCheck.BackColor = Color.Red
                        ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> NO"
                        WriteField("EcrCheck", "NO")
                    Else
                        MsgBox("To remove the approval first need to remove the 'CHECK' for R&D!", MsgBoxStyle.Information)
                    End If

                End If
            Else
                If MsgBoxResult.Yes = MsgBox("Do you want to give the approval?", vbYesNo) Then
                    If InStr(ButtonR.Text, "CHECK", ) > 0 Then
                        ButtonEcrCheck.BackColor = Color.Green
                        ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> YES"
                        WriteField("EcrCheck", "YES")
                    End If
                End If
            End If
        Else
            MsgBox("To approve need to have rights as R&D (R3) and supervisor (J3)! and need to select one ECR!", MsgBoxStyle.Information)
        End If
    End Sub
End Class