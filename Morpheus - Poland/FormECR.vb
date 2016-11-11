
Option Explicit On
Option Compare Text

Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net

Public Class FormECR
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim AdapterDocType As New MySqlDataAdapter("SELECT * FROM Doctype", MySqlconnection)
    Dim AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr", MySqlconnection)
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM product", MySqlconnection)
    Dim tblDoc As DataTable, tblDocType As DataTable, tblEcr As DataTable, tblProd As DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsEcr As New DataSet, DsProd As New DataSet
    Dim userDep3 As String
    Dim cmd As New MySqlCommand
    Dim CultureInfo_ja_JP As New CultureInfo("ja-JP", False)
    Dim needSave As Boolean = False
    Dim Adaptermail As New MySqlDataAdapter("SELECT * FROM mail", MySqlconnection)
    Dim Dsmail As New DataSet
    Dim tblmail As DataTable
    Dim MailSent As Boolean

    Private Sub FormECR_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        checkSave()

        FormStart.Show()
    End Sub

    Private Sub FormECR_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FormStart.Hide()
        AdapterEcr.SelectCommand = New MySqlCommand("SELECT * FROM ecr ORDER BY NUMBER;", MySqlconnection)
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        AdapterProd.SelectCommand = New MySqlCommand("SELECT * FROM product order by id;", MySqlconnection)
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")

        ComboProductFill()
        userDep3 = user()

        'If userDep3 <> "A" And userDep3 <> "" Then Me.Controls("DateTimePicker" & userDep3).Visible = True        ‘edited by johnson

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

    End Sub

    ' Fill the ECR combo with all ECR yet open

    Sub fillEcrComboTable()
        ComboBoxEcr.Items.Clear()
        Dim DsEcr As New DataSet
        AdapterEcr.Fill(DsEcr, "ecr")
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
            'ComboBoxEcr.Sorted = True
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
        AdapterEcr.SelectCommand = New MySqlCommand("SELECT * FROM ecr;", MySqlconnection)
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")

        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))

        Result = tblEcr.Select("number = " & EcrN)

        If readField("EcrCheck", Val(Mid(ComboBoxEcr.Text, 1, pos))) = "YES" Then
            ButtonE.Enabled = True
            ButtonP.Enabled = True
            ButtonQ.Enabled = True
            ButtonR.Enabled = True
            ButtonU.Enabled = True
            ButtonA.Enabled = True
            ButtonN.Enabled = True
            ButtonL.Enabled = True
            ButtonB.Enabled = True
        Else
            ButtonE.Enabled = False
            ButtonL.Enabled = False
            ButtonP.Enabled = False
            ButtonQ.Enabled = False
            ButtonR.Enabled = False
            ButtonU.Enabled = False
            ButtonA.Enabled = False
            ButtonN.Enabled = False
            ButtonB.Enabled = False
        End If

        If Result.Length > 0 Then
            ButtonE.Text = Result(0).Item("Esign")
            ButtonL.Text = Result(0).Item("Lsign")
            ButtonP.Text = Result(0).Item("Psign")
            ButtonQ.Text = Result(0).Item("Qsign")
            ButtonR.Text = Result(0).Item("Rsign")
            ButtonU.Text = Result(0).Item("Usign")
            ButtonA.Text = Result(0).Item("Asign")
            ButtonN.Text = Result(0).Item("Nsign")
            ButtonB.Text = Result(0).Item("Bsign")

            If userDep3 = "E" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Enote")
            If userDep3 = "L" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Lnote")
            If userDep3 = "P" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Pnote")
            If userDep3 = "Q" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Qnote")
            If userDep3 = "R" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Rnote")
            If userDep3 = "U" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Unote")
            If userDep3 = "A" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Anote")
            If userDep3 = "N" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("nnote")
            If userDep3 = "B" Then RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & Result(0).Item("Bnote")

            If userDep3 = "E" Then TextBoxStepCost.Text = Result(0).Item("ECost")
            If userDep3 = "L" Then TextBoxStepCost.Text = Result(0).Item("LCost")
            If userDep3 = "P" Then TextBoxStepCost.Text = Result(0).Item("PCost")
            If userDep3 = "Q" Then TextBoxStepCost.Text = Result(0).Item("QCost")
            If userDep3 = "R" Then TextBoxStepCost.Text = Result(0).Item("RCost")
            If userDep3 = "U" Then TextBoxStepCost.Text = Result(0).Item("UCost")
            If userDep3 = "N" Then TextBoxStepCost.Text = Result(0).Item("NCost")
            If userDep3 = "B" Then TextBoxStepCost.Text = Result(0).Item("BCost")

            If Result(0).Item("EcrCheck").ToString = "YES" Then

                ButtonEcrCheck.BackColor = Color.Green
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> YES"

            Else

                ButtonEcrCheck.BackColor = Color.Red
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Alignment    ---> NO"
            End If

            TextBoxTotalCost.Text = Int(Val(Result(0).Item("ECost")) + Val(Result(0).Item("LCost")) +
            Val(Result(0).Item("PCost")) + Val(Result(0).Item("NCost")) + Val(Result(0).Item("QCost")) + Val(Result(0).Item("RCost")) +
            Val(Result(0).Item("UCost")) + Val(Result(0).Item("BCost")))
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

            ButtonData.Text = Result(0).Item("date")
            ButtonRL.Text = Result(0).Item("dater")
            ButtonUL.Text = Result(0).Item("dateu")
            ButtonQL.Text = Result(0).Item("dateq")
            ButtonEL.Text = Result(0).Item("datee")
            ButtonLL.Text = Result(0).Item("datel")
            ButtonPL.Text = Result(0).Item("datep")
            ButtonNL.Text = Result(0).Item("dateN")
            ButtonBL.Text = Result(0).Item("dateB")

        End If
        If Not AllSign() Then UpdateDate()
        Try
            If Not AllSign() Then

                ComboBoxPay.Enabled = True
                'If userDep3 <> "A" Then Me.Controls("DateTimePicker" & userDep3).Visible = True
                If userDep3 <> "A" Then Me.Controls("Button" & userDep3 & "L").Enabled = True

            Else
                ComboBoxPay.Enabled = False
                If userDep3 <> "A" Then Me.Controls("DateTimePicker" & userDep3).Visible = False
                If userDep3 <> "A" Then Me.Controls("Button" & userDep3 & "L").Enabled = False
            End If
        Catch ex As Exception

        End Try

        'If userDep3 = "N" And CheckConfirm.Checked = False Then Me.Controls("DateTimePicker" & userDep3).Visible = True

        If Not AllSign() Then
            RichTextBoxStep.ReadOnly = False
            TextBoxStepCost.ReadOnly = False
            ButtonCalc.Enabled = True
            ButtonData.BackColor = Color.Yellow
            LabelApproved.ForeColor = Color.Red
            LabelApproved.Text = "NOT_APPROVED"
        Else
            'RichTextBoxStep.ReadOnly = True
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
        If InStr(1, readField("Esign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Rsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "CHECKED", CompareMethod.Text) > 0 Then
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
        If InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or
            InStr(1, readField("Usign", EcrN), "CHECKED", CompareMethod.Text) > 0 Then
            AllApproved = False
        End If
    End Function

    Sub ColorButton(ByVal US As String)

        ResetColorButton()

        If US = "E" Then ButtonE.BackColor = Color.LightGreen
        If US = "L" Then ButtonL.BackColor = Color.LightGreen
        If US = "P" Then ButtonP.BackColor = Color.LightGreen
        If US = "Q" Then ButtonQ.BackColor = Color.LightGreen
        If US = "R" Then ButtonR.BackColor = Color.LightGreen
        If US = "U" Then ButtonU.BackColor = Color.LightGreen
        If US = "A" Then ButtonA.BackColor = Color.LightGreen
        If US = "N" Then ButtonN.BackColor = Color.LightGreen
        If US = "B" Then ButtonB.BackColor = Color.LightGreen

        If userDep3 = "E" Then ButtonEL.BackColor = Color.LightGreen
        If userDep3 = "L" Then ButtonLL.BackColor = Color.LightGreen
        If userDep3 = "P" Then ButtonPL.BackColor = Color.LightGreen
        If userDep3 = "Q" Then ButtonQL.BackColor = Color.LightGreen
        If userDep3 = "R" Then ButtonRL.BackColor = Color.LightGreen
        If userDep3 = "U" Then ButtonUL.BackColor = Color.LightGreen
        If userDep3 = "N" Then ButtonNL.BackColor = Color.LightGreen
        If userDep3 = "B" Then ButtonBL.BackColor = Color.LightGreen

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
            AdapterEcr.Fill(DsEcr, "ecr")
            tblEcr = DsEcr.Tables("ecr")
        End If

        Try
            If EcrN > 0 Then
                result = tblEcr.Select("number =" & EcrN)
                readField = result(0).Item(field).ToString
            End If
        Catch ex As Exception
            MsgBox("Error reading ECR:" & EcrN)
        End Try

    End Function

    Sub WriteField(ByVal field As String, ByVal v As String)
        Dim SQL As String
        Dim pos As Integer, EcrN As Integer
        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))
        Try
            SQL = "UPDATE `" & DBName & "`.`ecr` SET `" & field & "` = '" & v & "' WHERE `ecr`.`number` = " & EcrN & " ;"
            cmd = New MySqlCommand(SQL, MySqlconnection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ComunicationLog("0052") 'db operation error
        End Try
    End Sub

    ' comunication function
    '
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

    Private Sub ButtonN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonN.Click
        ManagePushButton("N")
    End Sub
    Private Sub ButtonP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonP.Click
        ManagePushButton("P")
    End Sub
    Private Sub ButtonL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonL.Click
        ManagePushButton("L")
    End Sub
    Private Sub ButtonA_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonA.Click
        ManagePushButton("A")
    End Sub
    Private Sub ButtonR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonR.Click
        ManagePushButton("R")
    End Sub
    Private Sub ButtonE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonE.Click
        ManagePushButton("E")
    End Sub
    Private Sub ButtonQ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonQ.Click
        ManagePushButton("Q")
    End Sub
    Private Sub ButtonU_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonU.Click
        ManagePushButton("U")
    End Sub
    Private Sub ButtonB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonB.Click
        ManagePushButton("B")
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

                        If but <> "A" Then
                            'datepresence = Me.Controls("Button" & but & "L").Text <> ""
                            datepresence = True
                        Else
                            datepresence = True
                        End If
                        If SomeNoApproved() = False Then
                            If datepresence Then
                                Me.Controls("Button" & but).Text = CreAccount.strUserName & "[" & date_to_string(Now) & "]"
                                WriteField(but & "sign", Me.Controls("Button" & but).Text)
                                WriteField("date" & but, date_to_string(Now))

                                'UpdateDate()

                            Else
                                MsgBox("Please fill the data!")
                            End If
                        Else
                            MsgBox("It is not possible to sign if there is some dept that has not yet APPROVED!")
                            If MsgBox("Do you want to remove your approval?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                                If Not AllApproved() Then
                                    WriteField(but & "sign", "CHECKED")
                                    Me.Controls("Button" & but).Text = "CHECKED"
                                    WriteField("date" & but, date_to_string(Now))

                                Else
                                    ListBoxLog.Items.Add("You can't remove your APPROVE anymore!")
                                End If
                            End If
                        End If
                    End If

                ElseIf Me.Controls("Button" & but).Text = "CHECKED" Then
                    If MsgBox("Do you want to approve this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then

                        If but <> "A" Then
                            'datepresence = Me.Controls("Button" & but & "L").Text <> ""
                            datepresence = True
                        Else
                            datepresence = True
                        End If
                        If SomeNoChecked() = False Then
                            If datepresence Then
                                Me.Controls("Button" & but).Text = "APPROVED"
                                WriteField(but & "sign", Me.Controls("Button" & but).Text)
                                WriteField("date" & but, date_to_string(Now))

                            Else
                                MsgBox("Please fill in the data!")
                            End If
                        Else
                            MsgBox("It is not possible to approve if there is some dept. that has not yet CHECKED!")
                            If MsgBox("Do you want to remove your CHECKED?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                                'If Not AllApproved() Then
                                WriteField(but & "sign", "NOT CHECKED")
                                Me.Controls("Button" & but).Text = "NOT CHECKED"
                                WriteField("date" & but, date_to_string(Now))
                                'Else
                                '    ListBoxLog.Items.Add("Now, all approved, you cant can remove your approve ")
                                'End If
                            End If
                        End If
                    End If

                ElseIf Me.Controls("Button" & but).Text = "NOT CHECKED" Then

                    If but <> "A" Then
                        'datepresence = Me.Controls("Button" & but & "L").Text <> ""
                        datepresence = True
                    Else
                        datepresence = True
                    End If

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
                    If MsgBox("Do you want to remove your sign?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        If Not AllSign() Then
                            WriteField(but & "sign", "NOT CHECKED")
                            Me.Controls("Button" & but).Text = "NOT CHECKED"
                            WriteField("date" & but, date_to_string(Now))


                        Else
                            ListBoxLog.Items.Add("You can't remove your sign anymore!")
                        End If
                    End If
                Else
                    MsgBox("Already all signed for this ECR, so it is not possible to remove it! Please contact the IT Dept. in case of need.")
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
        AdapterEcr.SelectCommand = New MySqlCommand("SELECT * FROM ecr;", MySqlconnection)
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")

        RichTextBoxStep.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}" & readField(but & "note", EcrN)
        TextBoxStepCost.Text = readField(but & "cost", EcrN)
        UpdateDate()
        ButtonRL.Text = readField("dater", EcrN)
        ButtonUL.Text = readField("dateu", EcrN)
        ButtonQL.Text = readField("dateq", EcrN)
        ButtonEL.Text = readField("datee", EcrN)
        ButtonLL.Text = readField("datel", EcrN)
        ButtonPL.Text = readField("datep", EcrN)
        ButtonNL.Text = readField("dateN", EcrN)
        ButtonBL.Text = readField("dateB", EcrN)
        ButtonSave.BackColor = Color.Green
        ButtonSaveSend.BackColor = Color.Green
        needSave = False
    End Sub

    Sub ResetColorButton()
        ButtonE.BackColor = Color.LightGray
        ButtonL.BackColor = Color.LightGray
        ButtonP.BackColor = Color.LightGray
        ButtonQ.BackColor = Color.LightGray
        ButtonR.BackColor = Color.LightGray
        ButtonU.BackColor = Color.LightGray
        ButtonA.BackColor = Color.LightGray
        ButtonN.BackColor = Color.LightGray
        ButtonB.BackColor = Color.LightGray
    End Sub

    Sub ComboProductFill()
        ComboBoxProd.Items.Clear()

        For i = 0 To tblProd.Rows.Count - 1
            ComboBoxProd.Items.Add(tblProd.Rows(i).Item("bitronpn").ToString & " - " & tblProd.Rows(i).Item("name").ToString)

        Next
        ComboBoxProd.Sorted = True
    End Sub


    Private Sub ComboBoxEcr_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxEcr.SelectedValueChanged
        UpdateField()
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
        ButtonU.Text = "NOT CHECKED" And
        ButtonR.Text = "NOT CHECKED" And
        ButtonL.Text = "NOT CHECKED" And
        ButtonQ.Text = "NOT CHECKED" And
        ButtonN.Text = "NOT CHECKED" And
        ButtonE.Text = "NOT CHECKED" And
        ButtonB.Text = "NOT CHECKED" And
        ButtonP.Text = "NOT CHECKED" Then
            NoChecked = True
        End If
    End Function
    Function SomeNoApproved() As Boolean
        SomeNoApproved = False
        If ButtonA.Text = "NOT CHECKED" Or
        ButtonU.Text = "NOT CHECKED" Or
        ButtonR.Text = "NOT CHECKED" Or
        ButtonL.Text = "NOT CHECKED" Or
        ButtonQ.Text = "NOT CHECKED" Or
        ButtonN.Text = "NOT CHECKED" Or
        ButtonE.Text = "NOT CHECKED" Or
        ButtonP.Text = "NOT CHECKED" Or
        ButtonB.Text = "NOT CHECKED" Or
        ButtonA.Text = "CHECKED" Or
        ButtonU.Text = "CHECKED" Or
        ButtonR.Text = "CHECKED" Or
        ButtonL.Text = "CHECKED" Or
        ButtonQ.Text = "CHECKED" Or
        ButtonN.Text = "CHECKED" Or
        ButtonE.Text = "CHECKED" Or
        ButtonB.Text = "CHECKED" Or
        ButtonP.Text = "CHECKED" Then

            SomeNoApproved = True
        End If
    End Function

    Function SomeNoChecked() As Boolean
        SomeNoChecked = False
        If ButtonA.Text = "NOT CHECKED" Or
        ButtonU.Text = "NOT CHECKED" Or
        ButtonR.Text = "NOT CHECKED" Or
        ButtonL.Text = "NOT CHECKED" Or
        ButtonQ.Text = "NOT CHECKED" Or
        ButtonN.Text = "NOT CHECKED" Or
        ButtonE.Text = "NOT CHECKED" Or
        ButtonB.Text = "NOT CHECKED" Or
        ButtonP.Text = "NOT CHECKED" Then
            SomeNoChecked = True
        End If
    End Function

    Function NoCheckedOthers(ByVal but As String) As Boolean
        NoCheckedOthers = True
        If but <> "R" Then NoCheckedOthers = NoCheckedOthers And ButtonR.Text = "NOT CHECKED"
        If but <> "E" Then NoCheckedOthers = NoCheckedOthers And ButtonE.Text = "NOT CHECKED"
        If but <> "U" Then NoCheckedOthers = NoCheckedOthers And ButtonU.Text = "NOT CHECKED"
        If but <> "P" Then NoCheckedOthers = NoCheckedOthers And ButtonP.Text = "NOT CHECKED"
        If but <> "Q" Then NoCheckedOthers = NoCheckedOthers And ButtonQ.Text = "NOT CHECKED"
        If but <> "L" Then NoCheckedOthers = NoCheckedOthers And ButtonL.Text = "NOT CHECKED"
        If but <> "A" Then NoCheckedOthers = NoCheckedOthers And ButtonA.Text = "NOT CHECKED"
        If but <> "N" Then NoCheckedOthers = NoCheckedOthers And ButtonN.Text = "NOT CHECKED"
        If but <> "B" Then NoCheckedOthers = NoCheckedOthers And ButtonB.Text = "NOT CHECKED"
    End Function

    Sub invalidationProd(ByVal prod As String, ByVal ecrN As Integer)

        Dim RowSearchDoc As DataRow()
        Dim RowSearchProd As DataRow()
        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")
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
                    SQL = "UPDATE `" & DBName & "`.`doc` SET `ecrpending` = '" & row("ecrpending") & "[" & ecrN & "]" & "' WHERE `doc`.`id` = '" & row("id").ToString & "' ;"
                    cmd = New MySqlCommand(SQL, MySqlconnection)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If


        Next
    End Sub

    Sub DeinvalidationProd(ByVal prod As String, ByVal ecrN As Integer)

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
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
                    cmd = New MySqlCommand(SQL, MySqlconnection)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
        Next

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
                'strPathFtp = ("65R/65R_PRO_ECR/")
                strPathFtp = (ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                'ComunicationLog(objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, "65R_PRO_ECR_" & fileName)) ' download successfull
                ComunicationLog(objFtp.DownloadFile(strPathFtp, IO.Path.GetTempPath, ParameterTable("plant") & "R_PRO_ECR_" & fileName)) ' download successfull
                'downloadFileWinPath = System.IO.Path.GetTempPath & "65R_PRO_ECR_" & fileName
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

    'Private Sub DateTimePickerN_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickern.CloseUp
    '    ButtonNL.Text = DateTimePickern.Text
    '    WriteField("dateN", DateTimePickern.Text)
    '    UpdateDate()
    'End Sub

    'Private Sub ButtonNL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNL.Click
    '    If ButtonNL.Text = "" Then
    '        ButtonNL.Text = ""
    '        WriteField("dateN", "")
    '        UpdateDate()
    '    End If
    'End Sub

    'Private Sub DateTimePickerR_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerR.CloseUp
    '   ButtonRL.Text = DateTimePickerR.Text
    '  WriteField("dateR", DateTimePickerR.Text)
    '   UpdateDate()
    ' End Sub
    'Private Sub ButtonRL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRL.Click
    '     If ButtonRL.Text = "" Then
    '        ButtonRL.Text = ""
    '        WriteField("dateR", "")
    '       UpdateDate()
    '   End If
    ' End Sub
    'Private Sub DateTimePickeru_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerU.CloseUp
    '    ButtonUL.Text = DateTimePickerU.Text
    '    WriteField("dateu", DateTimePickerU.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub Buttonul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUL.Click
    '    If ButtonUL.Text = "" Then
    '        ButtonUL.Text = ""
    '        WriteField("dateu", "")
    '        UpdateDate()
    '    End If
    'End Sub

    'Private Sub DateTimePickerq_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerQ.CloseUp
    '    ButtonQL.Text = DateTimePickerQ.Text
    '    WriteField("dateq", DateTimePickerQ.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub Buttonql_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonQL.Click
    '    If ButtonQL.Text = "" Then
    '        ButtonQL.Text = ""
    '        WriteField("dateq", "")
    '        UpdateDate()
    '    End If
    'End Sub

    'Private Sub DateTimePickere_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerE.CloseUp
    '    ButtonEL.Text = DateTimePickerE.Text
    '    WriteField("datee", DateTimePickerE.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub Buttonel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEL.Click
    '    If ButtonEL.Text = "" Then
    '        ButtonEL.Text = ""
    '        WriteField("datee", "")
    '        UpdateDate()
    '    End If
    'End Sub

    'Private Sub DateTimePickerl_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerL.CloseUp
    '    ButtonLL.Text = DateTimePickerL.Text
    '    WriteField("datel", DateTimePickerL.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub Buttonll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonLL.Click
    '    If ButtonLL.Text = "" Then
    '        ButtonLL.Text = ""
    '        WriteField("datel", "")
    '        UpdateDate()
    '    End If
    'End Sub
    'Private Sub DateTimePickerp_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerp.CloseUp
    '    ButtonPL.Text = DateTimePickerp.Text
    '    WriteField("datep", DateTimePickerp.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub Buttonpl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPL.Click
    '    If ButtonLL.Text = "" Then
    '        ButtonPL.Text = ""
    '        WriteField("datep", "")
    '        UpdateDate()
    '    End If
    'End Sub
    Sub UpdateDate() Handles DateTimePickerL.ValueChanged, DateTimePickerU.ValueChanged, DateTimePickerE.ValueChanged, DateTimePickerQ.ValueChanged, DateTimePickerp.ValueChanged, DateTimePickerR.ValueChanged

        Dim maxVal As Date = string_to_date("2000/01/01")
        If ButtonRL.Text <> "" Then If maxVal < string_to_date(ButtonRL.Text) Then maxVal = ButtonRL.Text
        If ButtonUL.Text <> "" Then If maxVal < string_to_date(ButtonUL.Text) Then maxVal = ButtonUL.Text
        If ButtonLL.Text <> "" Then If maxVal < string_to_date(ButtonLL.Text) Then maxVal = ButtonLL.Text
        If ButtonQL.Text <> "" Then If maxVal < string_to_date(ButtonQL.Text) Then maxVal = ButtonQL.Text
        If ButtonEL.Text <> "" Then If maxVal < string_to_date(ButtonEL.Text) Then maxVal = ButtonEL.Text
        If ButtonPL.Text <> "" Then If maxVal < string_to_date(ButtonPL.Text) Then maxVal = ButtonPL.Text
        If ButtonNL.Text <> "" Then If maxVal < string_to_date(ButtonNL.Text) Then maxVal = ButtonNL.Text
        If ButtonBL.Text <> "" Then If maxVal < string_to_date(ButtonBL.Text) Then maxVal = ButtonBL.Text
        If maxVal < Now Then maxVal = Now

        If maxVal <> string_to_date("2000/01/01") Then
            ButtonData.Text = date_to_string(maxVal)
            WriteField("date", date_to_string(maxVal))
        Else
            ButtonData.Text = ""
        End If

    End Sub

    Private Sub ComboBoxPay_LostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxPay.LostFocus
        WriteField("cusPay", ComboBoxPay.Text)
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSave.Click
        WriteField(userDep3 & "cost", TextBoxStepCost.Text)
        WriteField(userDep3 & "note", Replace(Replace(RichTextBoxStep.Rtf, "\", "\\"), "'", ""))

        needSave = False
        ButtonSave.BackColor = Color.Green
        UpdateField()
    End Sub

    Private Function GetDepartamentName(right as String) as String
        Dim departament = ""
        If ButtonRL.BackColor = Color.LightGreen then departament =  "R&L"
        If ButtonLL.BackColor = Color.LightGreen then departament =  "Logistic"
        If ButtonUL.BackColor = Color.LightGreen then departament =  "Purchasing"
        If ButtonBL.BackColor = Color.LightGreen then departament =  "Process Engineering"
        If ButtonEL.BackColor = Color.LightGreen then departament =  "Testing Engineering"
        If ButtonNL.BackColor = Color.LightGreen then departament =  "Quality"
        If ButtonPL.BackColor = Color.LightGreen then departament =  "Production"
        If ButtonQL.BackColor = Color.LightGreen then departament =  "Time and Methods"
        return departament
    End Function


    Private Sub ButtonSaveSend_Click(sender As Object, e As EventArgs) Handles ButtonSaveSend.Click
        WriteField(userDep3 & "cost", TextBoxStepCost.Text)
        WriteField(userDep3 & "note", Replace(Replace(RichTextBoxStep.Rtf, "\", "\\"), "'", ""))

        needSave = False
        ButtonSave.BackColor = Color.Green
        UpdateField()

        Dim bodyText as String, subject as String
        bodyText = "Automatic SrvDoc Message:" & vbLf & vbLf & GetDepartamentName(userDep3) & " Note: " & RichTextBoxStep.Text
        subject = "ECR Note Change Notification:    " & ComboBoxEcr.Text
        SendMail("ECR_VerifyTo; ECR_R_SignTo; ECR_U_SignTo; ECR_L_SignTo; ECR_B_SignTo; ECR_E_SignTo; ECR_N_SignTo; ECR_P_SignTo; ECR_Q_SignTo", _
                 "ECR_VerifyCopy; ECR_R_SignCopy; ECR_U_SignCopy; ECR_L_SignCopy; ECR_B_SignCopy; ECR_E_SignCopy; ECR_N_SignCopy; ECR_P_SignCopy; ECR_Q_SignCopy;", _
                 bodyText, subject)

    End Sub

    Function GetEmails(ByVal emailsStr as String) as String
        Dim listOfEmail as String = ""
        For Each item in emailsStr.Split(";")
            if listOfEmail.Contains(item.Trim) = false then listOfEmail += "'" & item.Trim() & "',"
        Next
        If listOfEmail = "" then
            listOfEmail += "'" & emailsStr.Trim() & "'"
        else
            listOfEmail = listOfEmail.Remove(listOfEmail.Length - 1,1)
        End If  
        Return listOfEmail
    End Function

    Function SendMail(ByVal AddlistTo As String, ByVal AddlistCopy As String, ByVal bodyText As String, ByVal SubText As String, Optional ByVal ATTACH As String = "") As Boolean
        Dim dt As Date = Now
        SendMail = False
        Adaptermail.SelectCommand = New MySqlCommand("SELECT * FROM mail;", MySqlconnection)
        Adaptermail.Fill(Dsmail, "mail")
        tblmail = Dsmail.Tables("mail")

        Dim client As New SmtpClient(ParameterTable("SMTP"), ParameterTable("SMTP_PORT"))
        client.EnableSsl = IIf(ParameterTable("MAIL_SSL") = "YES", True, False)
        client.Credentials = New NetworkCredential(ParameterTable("MAIL_SENDER_CREDENTIAL_USER"), ParameterTable("MAIL_SENDER_CREDENTIAL_PSW"))

        Dim msg As New MailMessage(ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"), ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"))

        
        Dim RowSearchMail As DataRow() = tblmail.Select("list in (" & GetEmails(AddlistTo) & ")")

        msg.To.Clear()
        msg.CC.Clear()

        For Each row In RowSearchMail      
            Dim mailAddress as new  MailAddress(row("name").ToString.Replace(Environment.NewLine,""))    
            if msg.To.Contains(mailAddress) = false then   
                msg.To.Add(row("name").ToString.Replace(Environment.NewLine,""))
            End If
        Next
        
        RowSearchMail = tblmail.Select("list in (" & GetEmails(AddlistCopy) & ")")
        For Each row In RowSearchMail
            Dim mailAddress as new  MailAddress(row("name").ToString.Replace(Environment.NewLine,""))
            if msg.CC.Contains(mailAddress) = false then 
                msg.CC.Add(mailAddress)            
            End If
            
        Next

        If ATTACH <> "" Then
            Dim Allegato = New Attachment(ATTACH)
            If My.Computer.FileSystem.GetFileInfo(ATTACH).Length < Val(ParameterTable("MAX_SIZE_FILE_MAIL")) Then
                msg.Attachments.Add(Allegato)
                msg.Body = bodyText
            Else
                msg.Body = "ATTENTION... FILE NOT SENT BY MAIL FOR EXCESSIVE DIMENSION. PLEASE DOWNLOAD FROM SERVER!!!" & vbCrLf & vbCrLf & bodyText
            End If
        Else
            msg.Body = bodyText
        End If

        msg.Subject = SubText
        Try
            client.Send(msg)
            MailSent = True         
        Catch ex As Exception
            ListBoxLog.Items.Add("Mail not sent...!!!")
        End Try
        'Application.DoEvents()
    End Function


    Private Sub CheckConfirm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CheckConfirm.Click
        If CheckConfirm.Checked = True Then
            If AllSign() Then
                If vbYes = MsgBox("Are you sure to confirm the ECR? After the automatic notification you can't stop it.", MsgBoxStyle.YesNo, "SrvDoc ECR confirm of Introducing") Then
                    WriteField("Confirm", "CONFIRMED")
                    CheckConfirm.Visible = False
                Else
                    CheckConfirm.Checked = False
                End If
            Else
                MsgBox("Need that ECR is signed!")
            End If
        End If
        UpdateField()
    End Sub

    Private Sub CheckBoxOpen_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBoxOpen.CheckedChanged
        fillEcrComboTable()
    End Sub

    Private Sub RichTextBoxStep_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RichTextBoxStep.TextChanged
        ButtonSave.BackColor = Color.OrangeRed
        ButtonSaveSend.BackColor = Color.OrangeRed
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

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        Dim tblDoc As DataTable = DsDoc.Tables("doc")
        Dim Res As DataRow() = tblDoc.Select("id = " & docId)
        If Res.Length > 0 Then
            readDocSign = Res(0).Item("sign").ToString
        Else
            MsgBox("Document not found for ECR" & docId)
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
                        MsgBox("For removing the approval first need to remove the EcrSign for R&D", MsgBoxStyle.Information)
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
            MsgBox("For approval need to have rights as R&D (R3) and supervisor (J3)! and need to select one ECR!", MsgBoxStyle.Information)
        End If

    End Sub

End Class