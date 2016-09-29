
Option Explicit On
Option Compare Text

Imports MySql.Data.MySqlClient
Imports System.Globalization


Public Class FormECR
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim AdapterDocType As New MySqlDataAdapter("SELECT * FROM Doctype", MySqlconnection)
    Dim AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr", MySqlconnection)
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM product", MySqlconnection)
    Dim tblDoc As DataTable, tblDocType As DataTable, tblEcr As DataTable, tblProd As DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsEcr As New DataSet, DsProd As New DataSet
    Dim userDep3 As String, stepNote As String
    Dim cmd As New MySqlCommand
    Dim CultureInfo_ja_JP As New System.Globalization.CultureInfo("ja-JP", False)
    Dim needSave As Boolean = False


    Private Sub FormECR_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        checkSave()

        FormStart.Show()
    End Sub

    Private Sub FormECR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

        'If userDep3 <> "A" And userDep3 <> "" Then Me.Controls("DateTimePicker" & userDep3).Visible = True        ‘ edited by johnson
        If userDep3 <> "A" And userDep3 <> "" Then Me.Controls("Button" & userDep3 & "L").Enabled = True

        If userDep3 = "R" And Not AllSign() And userDep3 <> "" Then
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
            ComboBoxEcr.Text = ComboBoxEcr.Items(ComboBoxEcr.Items.Count - 1)
            ColorButton(userDep3)
        UpdateField()
        ButtonSave.BackColor = Color.Green
        If userDep3 = "" Then
            ButtonR_Click(Me, e)
        End If

    End Sub




    ' Fill the ECr combo with all ecr yet open, select from file 65R_PRO_ECR
    '
    Sub fillEcrComboTable()
        Dim i As Integer
        Dim rowshow As DataRow()
        ComboBoxEcr.Items.Clear()
        Dim tblEcr As DataTable
        Dim DsEcr As New DataSet
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")


        Try
            rowshow = tblEcr.Select("description like '*' ", "number")
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
            If MsgBox("Do you want save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ButtonSave_Click(Me, EventArgs.Empty)
                ButtonSave.BackColor = Color.Green
                needSave = False
            Else
                ButtonSave.BackColor = Color.Green
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



            stepNote = RichTextBoxStep.Text

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
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Allignement    ---> YES"

            Else

                ButtonEcrCheck.BackColor = Color.Red
                ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Allignement    ---> NO"
            End If



            TextBoxTotalCost.Text = Int(Val(Result(0).Item("ECost")) + Val(Result(0).Item("LCost")) + _
            Val(Result(0).Item("PCost")) + Val(Result(0).Item("NCost")) + Val(Result(0).Item("QCost")) + Val(Result(0).Item("RCost")) + _
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
        If InStr(1, readField("Esign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Lsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Psign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Asign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Qsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Rsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Nsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Usign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Bsign", EcrN), "APPROVED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
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
        If InStr(1, readField("Esign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Lsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Psign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Asign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Qsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Rsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Nsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
            InStr(1, readField("Bsign", EcrN), "CHECKED", CompareMethod.Text) > 0 Or _
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

        If userDep3 = "R" Then
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
            MsgBox("Error ECR read :" & EcrN)
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

    Private Sub ButtonN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonN.Click
        ManagePushButton("N")
    End Sub
    Private Sub ButtonP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonP.Click
        ManagePushButton("P")
    End Sub
    Private Sub ButtonL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonL.Click
        ManagePushButton("L")
    End Sub
    Private Sub ButtonA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonA.Click
        ManagePushButton("A")
    End Sub
    Private Sub ButtonR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonR.Click
        ManagePushButton("R")
    End Sub
    Private Sub ButtonE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonE.Click
        ManagePushButton("E")
    End Sub
    Private Sub ButtonQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonQ.Click
        ManagePushButton("Q")
    End Sub
    Private Sub ButtonU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonU.Click
        ManagePushButton("U")
    End Sub
    Private Sub ButtonB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonB.Click
        ManagePushButton("B")
    End Sub

    Sub ManagePushButton(ByVal but As String)
        Dim pos As Integer, EcrN As Integer
        pos = InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text)
        EcrN = Val(Mid(ComboBoxEcr.Text, 1, pos))

        Dim prevSigned As Boolean, datepresence As Boolean
        checkSave()
        If userDep3 = but Then
            ButtonSave.Visible = True
        Else
            ButtonSave.Visible = False
        End If
        If userDep3 = "" Then
        Else

            If userDep3 = but And Me.Controls("Button" & userDep3).BackColor = Color.LightGreen Then
                ButtonSave.Enabled = True
                If Me.Controls("Button" & but).Text = "APPROVED" Then
                    If MsgBox("Want you sign this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then

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
                            MsgBox("No possible sign if there are some dept that have tot yet APPROVED!")
                            If MsgBox("Want you remove your Approve?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                                If Not AllApproved() Then
                                    WriteField(but & "sign", "CHECKED")
                                    Me.Controls("Button" & but).Text = "CHECKED"
                                    WriteField("date" & but, date_to_string(Now))

                                Else
                                    ListBoxLog.Items.Add("Now, all APPROVED, you cant can remove your APPROVE")
                                End If
                            End If
                        End If
                    End If

                ElseIf Me.Controls("Button" & but).Text = "CHECKED" Then
                    If MsgBox("Want you approve this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then

                        If but <> "A" Then
                            'datepresence = Me.Controls("Button" & but & "L").Text <> ""
                            datepresence = True
                        Else
                            datepresence = True
                        End If
                        If SomeNoChecked() = False Then
                            If datepresence Then

                                If but <> "P" Then
                                    Me.Controls("Button" & but).Text = "APPROVED"
                                    WriteField(but & "sign", Me.Controls("Button" & but).Text)
                                    WriteField("date" & but, date_to_string(Now))
                                Else
                                    If AllApprovedButProduct() = True Then
                                        Me.Controls("Button" & but).Text = "APPROVED"
                                        WriteField(but & "sign", Me.Controls("Button" & but).Text)
                                        WriteField("date" & but, date_to_string(Now))
                                    Else
                                        MsgBox("No possible approve if there are some other dept that have tot yet APPROVED!")

                                    End If

                                End If

                            Else
                                    MsgBox("Please fill the data!")
                            End If
                        Else
                            MsgBox("No possible approve if there are some dept that have tot yet CHECKED!")
                            If MsgBox("Want you remove your CHECKED?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
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

                        If MsgBox("Want you mark 'CHECKED' this ECR?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                            Me.Controls("Button" & but).Text = "CHECKED"
                            WriteField(but & "sign", "CHECKED")
                            WriteField("date" & but, date_to_string(Now))
                        End If

                    Else
                        MsgBox("Please fill the data!")

                    End If

                ElseIf readDocSign(readField("iddoc", EcrN)) = "" And ParameterTable("SYSTEM_SHEDULE") <> "RUN" Then   ' signed
                    If MsgBox("Want you remove your sign?", MsgBoxStyle.YesNo, "ECR Question") = MsgBoxResult.Yes Then
                        If Not AllSign() Then
                            WriteField(but & "sign", "NOT CHECKED")
                            Me.Controls("Button" & but).Text = "NOT CHECKED"
                            WriteField("date" & but, date_to_string(Now))


                        Else
                            ListBoxLog.Items.Add("Now, all signed, you cant can remove your sign")
                        End If
                    End If
                Else
                    MsgBox("Already all sign for this ECR, so is no possible remove! please contact the IT Dept. in case need.")
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
            stepNote = RichTextBoxStep.Text
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


    Private Sub ComboBoxEcr_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxEcr.SelectedValueChanged
        UpdateField()
    End Sub



    Private Sub ButtonAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim pos As Integer, exist As Boolean


        If ComboBoxProd.Text <> "" Then
            If userDep3 = "R" Then
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

                    Dim prod As String = ""
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
            ComunicationLog("0045") 'Please select product
        End If
    End Sub

    Private Sub ButtonRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemove.Click
        Dim i As Integer
        If ListViewProd.Items.Count > 0 Then
            If ComboBoxEcr.Text <> "" Then
                DeinvalidationProd(ListViewProd.Items(ListViewProd.Items.Count - 1).SubItems(0).Text, _
                Mid(ComboBoxEcr.Text, 1, InStr(1, ComboBoxEcr.Text, "-", CompareMethod.Text) - 2))
                For i = ListViewProd.CheckedItems.Count - 1 To 0 Step -1
                    ListViewProd.CheckedItems(i).Remove()
                Next
                Dim prod As String = ""
                For i = 0 To ListViewProd.Items.Count - 1
                    prod = prod & StrDup(20 - Len(Mid(ListViewProd.Items(i).SubItems(0).Text(), 1, 20)), " ") & Mid(ListViewProd.Items(i).SubItems(0).Text, 1, 40)
                    prod = prod & StrDup(40 - Len(Mid(ListViewProd.Items(i).SubItems(1).Text(), 1, 40)), " ") & Mid(ListViewProd.Items(i).SubItems(1).Text, 1, 40)
                Next
                WriteField("prod", prod)

            Else
                ComunicationLog("0046") 'Now cant can modifiy
            End If
        End If

    End Sub
    Function NoChecked() As Boolean
        If ButtonA.Text = "NOT CHECKED" And _
        ButtonU.Text = "NOT CHECKED" And _
        ButtonR.Text = "NOT CHECKED" And _
        ButtonL.Text = "NOT CHECKED" And _
        ButtonQ.Text = "NOT CHECKED" And _
        ButtonN.Text = "NOT CHECKED" And _
        ButtonE.Text = "NOT CHECKED" And _
        ButtonB.Text = "NOT CHECKED" And _
        ButtonP.Text = "NOT CHECKED" Then
            NoChecked = True
        End If
    End Function
    Function SomeNoApproved() As Boolean
        SomeNoApproved = False
        If ButtonA.Text = "NOT CHECKED" Or _
        ButtonU.Text = "NOT CHECKED" Or _
        ButtonR.Text = "NOT CHECKED" Or _
        ButtonL.Text = "NOT CHECKED" Or _
        ButtonQ.Text = "NOT CHECKED" Or _
        ButtonN.Text = "NOT CHECKED" Or _
        ButtonE.Text = "NOT CHECKED" Or _
        ButtonP.Text = "NOT CHECKED" Or _
        ButtonB.Text = "NOT CHECKED" Or _
        ButtonA.Text = "CHECKED" Or _
        ButtonU.Text = "CHECKED" Or _
        ButtonR.Text = "CHECKED" Or _
        ButtonL.Text = "CHECKED" Or _
        ButtonQ.Text = "CHECKED" Or _
        ButtonN.Text = "CHECKED" Or _
        ButtonE.Text = "CHECKED" Or _
        ButtonB.Text = "CHECKED" Or _
        ButtonP.Text = "CHECKED" Then

            SomeNoApproved = True
        End If
    End Function

    Function SomeNoChecked() As Boolean
        SomeNoChecked = False
        If ButtonA.Text = "NOT CHECKED" Or _
        ButtonU.Text = "NOT CHECKED" Or _
        ButtonR.Text = "NOT CHECKED" Or _
        ButtonL.Text = "NOT CHECKED" Or _
        ButtonQ.Text = "NOT CHECKED" Or _
        ButtonN.Text = "NOT CHECKED" Or _
        ButtonE.Text = "NOT CHECKED" Or _
        ButtonB.Text = "NOT CHECKED" Or _
        ButtonP.Text = "NOT CHECKED" Then
            SomeNoChecked = True
        End If
    End Function
    Function AllApprovedButProduct() As Boolean
        AllApprovedButProduct = False
        If ButtonU.Text = "APPROVED" And _
        ButtonR.Text = "APPROVED" And _
        ButtonL.Text = "APPROVED" And _
        ButtonQ.Text = "APPROVED" And _
        ButtonN.Text = "APPROVED" And _
        ButtonE.Text = "APPROVED" And _
        ButtonB.Text = "APPROVED" Then
            AllApprovedButProduct = True
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
        RowSearchDoc = tblDoc.Select("(filename ='" & RowSearchProd(0).Item("bitronpn").ToString & "' or filename ='" & _
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

        RowSearchDoc = tblDoc.Select("(filename ='" & RowSearchProd(0).Item("bitronpn").ToString & "' or filename ='" & _
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
        Dim objFtp As ftp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""


        Dim cmd As New MySqlCommand()
        If fileName <> "" Then
            Try
                strPathFtp = ("65R/65R_PRO_ECR/")

                ComunicationLog(objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, "65R_PRO_ECR_" & fileName)) ' download successfull
                downloadFileWinPath = System.IO.Path.GetTempPath & "65R_PRO_ECR_" & fileName
            Catch ex As Exception
                ComunicationLog("0049") ' Error in ecr Download
            End Try
        Else
            ComunicationLog("5061") ' fill path
        End If

    End Function


    Private Sub ButtonOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOpen.Click
        Dim fileOpen As String
        fileOpen = downloadFileWinPath(ComboBoxEcr.Text)
        Process.Start(fileOpen)

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
    '    ButtonRL.Text = DateTimePickerR.Text
    '    WriteField("dateR", DateTimePickerR.Text)
    '    UpdateDate()
    'End Sub
    'Private Sub ButtonRL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRL.Click
    '    If ButtonRL.Text = "" Then
    '        ButtonRL.Text = ""
    '        WriteField("dateR", "")
    '        UpdateDate()
    '    End If
    'End Sub
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
    Private Sub ComboBoxPay_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPay.LostFocus
        WriteField("cusPay", ComboBoxPay.Text)
    End Sub
    'Private Sub ButtonCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCalc.Click
    '    FormCalcCost.Show()
    'End Sub
    Private Sub ButtonSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSave.Click
        WriteField(userDep3 & "cost", TextBoxStepCost.Text)
        WriteField(userDep3 & "note", Replace(Replace(RichTextBoxStep.Rtf, "\", "\\"), "'", ""))

        needSave = False
        ButtonSave.BackColor = Color.Green
        UpdateField()
    End Sub

    Private Sub CheckConfirm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckConfirm.Click
        If CheckConfirm.Checked = True Then
            If AllSign() Then
                If vbYes = MsgBox("Are you sure to confirm the ECR? After the notification is automatic an you can't stoop it.", MsgBoxStyle.YesNo, "SrvDoc ECR confirm of Introducing") Then
                    WriteField("Confirm", "CONFIRMED")
                    CheckConfirm.Visible = False
                Else
                    CheckConfirm.Checked = False
                End If
            Else
                MsgBox("Need the ECR is Signed!")
            End If
        End If
        UpdateField()
    End Sub



    Private Sub CheckBoxOpen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxOpen.CheckedChanged
        fillEcrComboTable()
    End Sub

    Private Sub RichTextBoxStep_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBoxStep.TextChanged
        ButtonSave.BackColor = Color.OrangeRed
        needSave = True
    End Sub



    Sub checkSave()
        If needSave = True Then
            If MsgBox("Do you want save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                ButtonSave_Click(Me, EventArgs.Empty)
                ButtonSave.BackColor = Color.Green
                needSave = False
            Else
                ButtonSave.BackColor = Color.Green
                needSave = False
            End If
        End If
    End Sub



    Function readDocSign(ByVal docId As Long) As String
        Dim Res As DataRow()
        Dim tblDoc As DataTable
        Dim DsDoc As New DataSet

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")
        Res = tblDoc.Select("id = " & docId)
        If Res.Length > 0 Then
            readDocSign = Res(0).Item("sign").ToString
        Else
            MsgBox("Document not please delete find for ECR" & docId)
        End If

    End Function



    Private Sub ButtonEcrCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEcrCheck.Click

        If userDep3 = "R" And controlRight("J") = 3 Then

            If ButtonEcrCheck.BackColor = Color.Green Then
                If MsgBoxResult.Yes = MsgBox("Do you want remove the approval?", vbYesNo) Then
                    If InStr(ButtonR.Text, "CHECK", ) > 0 Then
                        ButtonEcrCheck.BackColor = Color.Red
                        ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Allignement    ---> NO"
                        WriteField("EcrCheck", "NO")
                    Else
                        MsgBox("For remove the approval first need remove the EcrSign for R&D", MsgBoxStyle.Information)
                    End If

                End If
            Else
                If MsgBoxResult.Yes = MsgBox("Do you want give the approval?", vbYesNo) Then
                    If InStr(ButtonR.Text, "CHECK", ) > 0 Then
                        ButtonEcrCheck.BackColor = Color.Green
                        ButtonEcrCheck.Text = "Customer Doc To Bitron ECR Allignement    ---> YES"
                        WriteField("EcrCheck", "YES")
                    End If
                End If
            End If
        Else
            MsgBox("For Approve need right R&D ( R3 ) and supervisor ( J3 )!", MsgBoxStyle.Information)
        End If

    End Sub




    Private Sub ButtonQL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonQL.Click, ButtonBL.Click

    End Sub
    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click, Label17.Click

    End Sub

    Private Sub UpdateDate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerU.ValueChanged, DateTimePickerR.ValueChanged, DateTimePickerQ.ValueChanged, DateTimePickerp.ValueChanged, DateTimePickerL.ValueChanged, DateTimePickerE.ValueChanged, DateTimePickerB.ValueChanged

    End Sub
End Class