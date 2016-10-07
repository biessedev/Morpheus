
Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.Net
Imports System.IO

Public Class FormAdministration
    Dim closeform As Boolean
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim AdapterDocType As New MySqlDataAdapter("SELECT * FROM Doctype", MySqlconnection)
    Dim AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr", MySqlconnection)
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM product", MySqlconnection)
    Dim Adaptermail As New MySqlDataAdapter("SELECT * FROM mail", MySqlconnection)
    Dim dep As New List(Of String)
    Dim CultureInfo_ja_JP As New System.Globalization.CultureInfo("ja-JP", False)
    Dim MailSent As Boolean
    Dim tblDoc As DataTable, tblDocType As DataTable, tblEcr As DataTable, tblProd As DataTable, tblmail As DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsEcr As New DataSet, DsProd As New DataSet, Dsmail As New DataSet
    Dim userDep3 As String
    Dim cmd As New MySqlCommand()

    Private Sub FormAdministration_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        FormStart.Show()
    End Sub

    Private Sub FormAdministration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AdapterEcr.SelectCommand = New MySqlCommand("SELECT * FROM ecr;", MySqlconnection)
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        AdapterProd.SelectCommand = New MySqlCommand("SELECT * FROM product;", MySqlconnection)
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")

        Adaptermail.SelectCommand = New MySqlCommand("SELECT * FROM mail;", MySqlconnection)
        Adaptermail.Fill(Dsmail, "mail")
        tblmail = Dsmail.Tables("mail")

        dep.Add("P")
        dep.Add("U")
        dep.Add("E")
        dep.Add("Q")
        dep.Add("N")
        dep.Add("L")
        dep.Add("C")
        dep.Add("F")
        dep.Add("B")

    End Sub
    Private Sub TimerECR_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerECR.Tick

        FormStart.Hide()
        ParameterTableWrite("SYSTEM_SCHEDULE", "RUN")
        Dim dt As New DateTime

        'ECR
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            'TimerECR.Stop()
            OpenConnectionMySql(FormCredentials.TextBoxhost.Text, FormCredentials.TextBoxDatabase.Text, "root", "bitron")
            TextBoxEcr.Text = date_to_string(Now) & " Start ECR"
            Application.DoEvents()
            UpdateEcrTable()
            Application.DoEvents()
            ecrDocSign()
            Application.DoEvents()
            ecrDocConfirm()
            Application.DoEvents()
            EcrMailScheduler()
            Application.DoEvents()
            ecrDocApprove()                                           'added by johnson
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish ECR"
            'TimerECR.Start()
        End If

        ' Status
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            'TimerECR.Stop()
            OpenConnectionMySql(FormCredentials.TextBoxhost.Text, FormCredentials.TextBoxDatabase.Text, "root", "bitron")
            TextBoxEcr.Text = date_to_string(Now) & " Start STATUS"
            Application.DoEvents()
            StatusMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish STATUS"
            'TimerECR.Start()
        End If

        ' TCR
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            'TimerECR.Stop()
            OpenConnectionMySql(FormCredentials.TextBoxhost.Text, FormCredentials.TextBoxDatabase.Text, "root", "bitron")
            TextBoxEcr.Text = date_to_string(Now) & " Start TCR"
            Application.DoEvents()
            TCRMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish TCR"
            'TimerECR.Start()
        End If

        ' DOC
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then

            OpenConnectionMySql(FormCredentials.TextBoxhost.Text, FormCredentials.TextBoxDatabase.Text, "root", "bitron")
            TextBoxEcr.Text = date_to_string(Now) & " Start Doc Changes"
            Application.DoEvents()
            DocMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish Doc Changes"

        End If

        Adaptermail.SelectCommand = New MySqlCommand("SELECT * FROM mail;", MySqlconnection)
        Adaptermail.Fill(Dsmail, "mail")
        tblmail = Dsmail.Tables("mail")

        Dim RowSearch As DataRow(), i As Integer, j As Integer
        RowSearch = tblmail.Select("name like '*'")
        For Each row In RowSearch
            j = Len(row("freq").ToString)
            If j > 1000 Then
                i = InStrRev(row("freq").ToString, "]", j - 1000, CompareMethod.Text)
                If i > 1 Then
                    WriteField("freq", Mid(row("freq").ToString, i + 1), row("id").ToString)
                End If
            End If
        Next

        ParameterTableWrite("LAST_AUTOMATIC_SCHEDULER", date_to_string(Today))
        ParameterTableWrite("SYSTEM_SCHEDULE", "HOLD")
    End Sub

    Sub TCRMailScheduler()
        Dim oi As String
        tblDoc.Clear()
        DsDoc.Clear()

        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        Dim RowSearchDoc As DataRow(), sql As String
        RowSearchDoc = tblDoc.Select("sign = '' and HEADER='" & ParameterTable("plant") & "R_PRO_TCR'")
        For Each row In RowSearchDoc

            oi = Trim(Mid(row("filename").ToString, 1, InStr(row("filename").ToString, "-") - 1))
            Dim fileOpen As String = ""
            fileOpen = downloadFileWinPath(ParameterTable("plant") & "R_PRO_TCR_" & row("filename").ToString & "_" & row("rev").ToString & "." & row("extension").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_TCR/")
            Try
                If mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                               "Please CHECK the TCR : " & " " & row("filename").ToString & " " & vbCrLf & vbCrLf & "Best Regard", "TCR Sign Notification  " & " " &
                               row("filename").ToString, "T_" & oi, False, fileOpen) Then
                    sql = "UPDATE `" & DBName & "`.`doc` SET `sign` = 'System[" & date_to_string(Now) & "]' WHERE `doc`.`id` = " & row("id").ToString & " ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                Else
                    MsgBox("Error sending email for TCR!")
                End If

            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try

        Next

    End Sub

    Sub DocMailScheduler()
        Dim listFile As String = ""
        tblDoc.Clear()
        DsDoc.Clear()

        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        Dim RowSearchDoc As DataRow(), sql As String
        RowSearchDoc = tblDoc.Select("notification = '' and sign = ''")
        For Each row In RowSearchDoc
            listFile = listFile & " " & vbCrLf & row("header").ToString & "_" & row("FileName").ToString & "_" & row("rev").ToString & "." & row("Extension").ToString & " " & vbCrLf
        Next
        Try
            MailSent = False
            If listFile <> "" Then
                mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                           vbCrLf & "Please CHECK the new file / version in the server : " & " " & vbCrLf & vbCrLf & listFile & vbCrLf & vbCrLf & "Best Regard", "File changes notification  " &
                           date_to_string(Now), "DAILY", True)
            End If
        Catch ex As Exception
            ComunicationLog("5050") ' Mysql update query error 
        End Try

        For Each row In RowSearchDoc
            Try
                sql = "UPDATE `" & DBName & "`.`doc` SET `notification` = 'SENT' WHERE `notification` = '' and sign ='';"
                If MailSent = True Then
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                End If
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try
        Next

    End Sub

    Sub StatusMailScheduler()
        Dim oi As String
        tblProd.Clear()
        DsProd.Clear()

        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")

        Dim RowSearchProduct As DataRow(), sql As String
        RowSearchProduct = tblProd.Select("")
        For Each row In RowSearchProduct
            oi = Replace(row("openissue").ToString, "];", "]" & vbCrLf)
            If oi = "" Then oi = "No Open Issue"

            If (row("Status").ToString = "MPA_APPROVED" Or row("Status").ToString = "MPA_STOPPED") And row("mail").ToString <> "SENT" Then
                Try
                    sql = "UPDATE `" & DBName & "`.`product` SET `mail` = 'SENT' WHERE `product`.`BitronPN` = '" & row("BITRONPN").ToString & "' ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    If mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                               "Please CHECK the Status of Product : " & " " & row("bitronpn").ToString & " " & row("name").ToString & vbCrLf &
                               vbCrLf & "Open Issue:" & vbCrLf & oi & vbCrLf & vbCrLf & "Best Regard", "Product Status Notification " & row("STATUS").ToString & " " &
                               row("bitronpn").ToString & " " & row("name").ToString, "S_" & row("bitronpn").ToString, False) Then
                        sql = "UPDATE `" & DBName & "`.`product` SET `mail` = 'SENT' WHERE `product`.`BitronPN` = '" & row("BITRONPN").ToString & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Else
                        MsgBox("mail sent error ECR confirm!")
                    End If

                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error 
                End Try

                oi = Replace(row("openissue").ToString, "];", "]" & vbCrLf)
                If oi = "" Then oi = "No Open Issue at this moment"
            End If

            For Each SS In dep

                'If SS = "E" And row("Status").ToString = "PURCHASING_APPROVED" Then Stop
                If prevStatus(SS) = row("Status").ToString Or (row("Status").ToString = "MPA_STOPPED" And SS = "N") Then
                    Try

                        mailSender("STATUS_" & SS & "_SignTo", "STATUS_" & SS & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                                   "Please Update the Status of Product : " & " " & row("bitronpn").ToString & " " & row("name").ToString & vbCrLf &
                                   vbCrLf & "Current Status:  " & row("Status").ToString & vbCrLf &
                                   vbCrLf & "Open Issue:" & vbCrLf & vbCrLf & oi & vbCrLf & vbCrLf & "Best Regard", "Product Status Update Request " & " " &
                                   row("bitronpn").ToString & " " & row("name").ToString, SS & "_" & row("bitronpn").ToString)
                    Catch ex As Exception
                        ComunicationLog("5050") ' Mysql update query error 
                    End Try
                End If
            Next

        Next

    End Sub

    Function prevStatus(ByVal dep As String) As String
        If dep = "L" Then prevStatus = "R&D_APPROVED"
        If dep = "C" Then prevStatus = "LOGISTIC_APPROVED"
        If dep = "U" Then prevStatus = "CUSTOMER_APPROVED"
        If dep = "P" Then prevStatus = "PURCHASING_APPROVED"
        If dep = "Q" Then prevStatus = "PRODUCTION_APPROVED"
        If dep = "E" Then prevStatus = "TIME&MOTION_APPROVED"
        If dep = "B" Then prevStatus = "TESTING_ENG_APPROVED"
        If dep = "F" Then prevStatus = "PROCESS_ENG_APPROVED"
        If dep = "N" Then prevStatus = "FINANCIAL_APPROVED"
    End Function


    Private Sub ButtonCompare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCompare.Click
        Dim RowSearch As DataRow()
        Dim objFtp As ftp = New ftp(), i As Long, sql As String
        Dim strPathFtp As String, strListDir As String
        Dim strRes As String

        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd

        TimerECR.Stop()
        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC ", MySqlconnection)
        DsDoc.Clear()
        tblDoc.Clear()
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        RowSearch = tblDoc.Select("filename like '*'")
        i = 0
        For Each row In RowSearch
            Try

                strPathFtp = (Mid(row("header").ToString, 1, 3) & "/" & row("header").ToString)
                Application.DoEvents()

                strListDir = row("header").ToString & "_" & row("filename").ToString _
                    & "_" & row("rev").ToString & "." & row("extension").ToString

                strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)

                If strRes <> "5000" Then
                    strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)
                End If

                If strRes <> "5000" Then
                    strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)
                End If

                If strRes <> "5000" Then
                    strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)
                End If

                If strRes <> "5000" Then
                    strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)
                End If

                If strRes <> "5000" Then
                    strRes = objFtp.ListDirectory(strPathFtp & "/", strListDir)
                End If


                If strRes <> "5000" Then

                    If CheckBoxDeleteRecord.Checked = True Then
                        Try

                            If MsgBox("Do you want to delete the record: " & row("header").ToString & "_" & row("filename").ToString &
                            "_" & row("rev").ToString & "." & row("extension").ToString, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                sql = "DELETE FROM `" & DBName & "`.`doc` WHERE `doc`.`id` = " & row("id").ToString
                                cmd = New MySqlCommand(sql, MySqlconnection)
                                cmd.ExecuteNonQuery()
                                ComunicationLog("5074") ' record deleted
                            End If

                        Catch ex As Exception

                        End Try
                    End If

                Else
                    ' tutto ok
                End If

                ButtonCompare.Text = Format(100 * (i / RowSearch.Length), "#.#")
                i = i + 1
                Application.DoEvents()

            Catch ex As Exception
                ComunicationLog("5078")
            End Try
        Next

        ExploreFile("/")
        ComunicationLog("5075")
        ButtonCompare.Text = "Compare File DB"
        TimerECR.Start()
    End Sub

    Private Sub ButtonClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClose.Click

        If controlRight("Z") >= 3 And InputBox("Please insert psw for this account : ", "Password Request") = CreAccount.strPassword Then
            FormStart.Show()
            closeform = True
            Me.Close()
        Else
            ComunicationLog("0043")
        End If
    End Sub

    Private Sub ButtonDelDup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDelDup.Click
        DelDuplicate()
    End Sub

    Sub UpdateEcrTable()

        Dim RowEcr As DataRow(), pos As Integer
        Dim EcrN As Integer, sql As String, filename As String, data As String
        Dim RowSearchDoc As DataRow()

        RowSearchDoc = tblDoc.Select("header = '" & ParameterTable("plant") & "R_PRO_ECR'")

        For Each row In RowSearchDoc
            AdapterEcr.SelectCommand = New MySqlCommand("SELECT * FROM ecr;", MySqlconnection)
            tblEcr.Clear()
            DsEcr.Clear()
            AdapterEcr.Fill(DsEcr, "ecr")
            tblEcr = DsEcr.Tables("ecr")

            pos = InStr(1, row("filename").ToString, "-", CompareMethod.Text)
            EcrN = Val(Mid(row("filename").ToString, 1, pos))
            RowEcr = tblEcr.Select("number=" & EcrN)
            If EcrN > 0 And RowEcr.Length = 0 And InStr(row("filename").ToString, "template", CompareMethod.Text) <= 0 Then   ' not manage becouse primary key on number
                Try
                    filename = row("filename").ToString & "_" & row("rev").ToString & "." & row("extension").ToString
                    data = Mid(row("editor").ToString, Len(row("editor").ToString) - 9, 9)
                    sql = "INSERT INTO `" & DBName & "`.`ecr` (`nnote` ,`number` ,`description` ,`date`,`Usign`,`nsign`,`Lsign`,`Asign`,`Qsign`,`Esign`,`Rsign`,`Psign`,`Bsign`,`DocInvalid`,`IdDoc`) VALUES (" &
                    Replace("'{\rtf1\fbidis\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}{\f1\fswiss\fprq2\fcharset0 Calibri;}}{\colortbl ;\red23\green54\blue93;}\viewkind4\uc1\pard\ltrpar\sl360\slmult1\cf1\lang1040\f0\fs22\par\par\par\par\ul\b\i\f1 Confirmation AREA\par\lang1033\ulnone\b0\i0 Time and First serial number / Fiche:\par\par\par\parOther Annotation:\f0\par\pard\ltrpar\cf0\lang1040\fs24\par\par\par\par}', ", "\", "\\") _
                    & EcrN & ", '" & filename & "', '" & "01/01/2000" & "', 'NOT CHECKED' , 'NOT CHECKED', 'NOT CHECKED', 'System[automatic]', 'NOT CHECKED', 'NOT CHECKED', 'NOT CHECKED', 'NOT CHECKED','NOT CHECKED', 'NO',  " & row("id").ToString & " );"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error, check if bitron p/n is already in db
                End Try

            Else  ' no need add

            End If
        Next

    End Sub

    Sub ecrDocSign()
        Dim RowSearchEcr As DataRow(), sql As String, refresh As Boolean = True
        RowSearchEcr = tblEcr.Select("docInvalid = 'NO'", "number")
        For Each row In RowSearchEcr
            Dim i As Integer
            i = Int(row("number").ToString)

            If row("sign").ToString = "" And InStr(row("Nsign").ToString & row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Qsign").ToString & row("Esign").ToString & row("Psign").ToString & row("asign").ToString & row("Bsign").ToString, "APPROVED", CompareMethod.Text) <= 0 And readDocSign(Int(row("iddoc").ToString), refresh) = "" And InStr(row("Nsign").ToString & row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Qsign").ToString & row("Esign").ToString & row("Psign").ToString & row("asign").ToString & row("Bsign").ToString, "CHECKED", CompareMethod.Text) <= 0 Then
                Try
                    Dim fileOpen As String = ""
                    fileOpen = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                    If mailSender("ECR_SignTo", "ECR_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                               vbCrLf & row("description").ToString & " -- > (Result: signed, released & implemented) " &
                               vbCrLf & "Closed Data : " & row("date").ToString & "( yyyy/mm/dd )" & vbCrLf &
                               vbCrLf & vbCrLf & "R&D Note: " & rtfTrans(row("rnote").ToString) & vbCrLf &
                               vbCrLf & "Purchasing Note: " & rtfTrans(row("unote").ToString) & vbCrLf &
                               vbCrLf & "Logistic Note: " & rtfTrans(row("lnote").ToString) & vbCrLf &
                               vbCrLf & "Testing Engineering Note: " & rtfTrans(row("enote").ToString) & vbCrLf &
                               vbCrLf & "Time & Methods Note: " & rtfTrans(row("qnote").ToString) & vbCrLf &
                               vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                               vbCrLf & "Process Engineering Note: " & rtfTrans(row("Bnote").ToString) & vbCrLf &
                               vbCrLf & "Production Note: " & rtfTrans(row("pnote").ToString) & vbCrLf &
                               vbCrLf & "Administration Note: " & rtfTrans(row("anote").ToString) & vbCrLf &
                               vbCrLf & "For all details please download ECR from server SrvDoc. ", "ECR Sign Notification:   " & " " & row("description").ToString, "SS" & row("number").ToString, False, fileOpen) Then
                        sql = "UPDATE `" & DBName & "`.`ecr` SET `sign` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `ecr`.`sign` ='' and `ecr`.`number` = '" & i & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        sql = "UPDATE `" & DBName & "`.`doc` SET `sign` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `doc`.`sign` ='' and `doc`.`id` = '" & row("iddoc").ToString & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Else
                        MsgBox("Error sending email ECR signature!")
                    End If

                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
            Application.DoEvents()
            refresh = False
        Next

    End Sub

    Function rtfTrans(ByVal rtf As String) As String
        Try
            RichTextBoxConv.Rtf = rtf
            rtfTrans = RichTextBoxConv.Text
        Catch ex As Exception
            rtfTrans = ""
        End Try
    End Function

    Sub ecrDocConfirm()
        Dim RowSearchEcr As DataRow(), sql As String, refresh As Boolean = True
        RowSearchEcr = tblEcr.Select("docInvalid = 'NO'")

        For Each row In RowSearchEcr
            If InStr(row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Qsign").ToString & row("Esign").ToString & row("Psign").ToString & row("Asign").ToString & row("Bsign").ToString, "APPROVED", CompareMethod.Text) <= 0 And readDocSign(row("iddoc").ToString, refresh) <> "" And
                row("confirm").ToString = "CONFIRMED" Then

                Dim fileOpen As String = ""
                fileOpen = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                Try
                    If mailSender("Status_SignTo", "Status_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                               vbCrLf & row("description").ToString & " -- > (Result: ECR Confirm of Introduction) " & vbCrLf & vbCrLf &
                               vbCrLf & "Validate Data :" & row("date").ToString & " (yyyy/mm/dd)" & vbCrLf &
                               vbCrLf & vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                               vbCrLf & vbCrLf & vbCrLf & "For all detailed info please download it from server SrvDoc.", "ECR Confirm of Introduction:   " & " " & row("description").ToString, "C" & row("number").ToString, False, fileOpen) Then
                        sql = "UPDATE `" & DBName & "`.`ECR` SET `confirm` = 'SENT_CONFIRMED' WHERE `ECR`.`id` = " & row("id").ToString & " ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Else
                        MsgBox("mail sent error ECR confirm!")
                    End If

                    sql = "UPDATE `" & DBName & "`.`ECR` SET `confirm` = 'SENT_CONFIRMED' WHERE `ECR`.`id` = " & row("id").ToString & " ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
            refresh = False
        Next

    End Sub

    Function readDocSign(ByVal docId As Long, ByVal refresh As Boolean) As String
        Dim Res As DataRow()
        Static Dim tblDoc As DataTable
        Static Dim DsDoc As New DataSet

        If refresh = True Then
            AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC", MySqlconnection)
            AdapterDoc.Fill(DsDoc, "doc")
            tblDoc = DsDoc.Tables("doc")
        End If

        Res = tblDoc.Select("id = " & docId)
        If Res.Length > 0 Then
            readDocSign = Res(0).Item("sign").ToString
        Else
            MsgBox("Document not please delete find for ECR" & docId)
        End If

    End Function

    Sub EcrMailScheduler()

        Dim RowSearchEcr As DataRow(), us As String, dt As Date, refresh As Boolean = True
        RowSearchEcr = tblEcr.Select("")
        For Each row In RowSearchEcr

            If readDocSign(row("iddoc").ToString, refresh) = "" Then

                us = "R"
                If row("ecrcheck").ToString <> "YES" Then
                    mailSender("ECR_" & "VerifyTo", "ECR_" & "VerifyCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please VERIFY the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "R"
                If ((row(us & "sign").ToString = "NOT CHECKED") And (row("ecrcheck").ToString = "YES")) Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "L"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "U"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "B"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "E"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "N"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" And
                    row("Bsign").ToString <> "NOT CHECKED" And
                    row("Esign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "P"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" And
                    row("Bsign").ToString <> "NOT CHECKED" And
                    row("Esign").ToString <> "NOT CHECKED" And
                    row("Nsign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "Q"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" And
                    row("Bsign").ToString <> "NOT CHECKED" And
                    row("Esign").ToString <> "NOT CHECKED" And
                    row("Nsign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                us = "A"
                If row(us & "sign").ToString = "NOT CHECKED" And
                    row("ecrcheck").ToString = "YES" And
                    row("Rsign").ToString <> "NOT CHECKED" And
                    row("Lsign").ToString <> "NOT CHECKED" And
                    row("Usign").ToString <> "NOT CHECKED" And
                    row("Bsign").ToString <> "NOT CHECKED" And
                    row("Esign").ToString <> "NOT CHECKED" And
                    row("Nsign").ToString <> "NOT CHECKED" And
                    row("Psign").ToString <> "NOT CHECKED" And
                    row("Qsign").ToString <> "NOT CHECKED" Then
                    mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the Ecr: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                dt = string_to_date((row("date").ToString))

                If row("Rsign").ToString <> "NOT CHECKED" And
                row("Lsign").ToString <> "NOT CHECKED" And
                row("Usign").ToString <> "NOT CHECKED" And
                row("Bsign").ToString <> "NOT CHECKED" And
                row("Esign").ToString <> "NOT CHECKED" And
                row("Nsign").ToString <> "NOT CHECKED" And
                row("Psign").ToString <> "NOT CHECKED" And
                row("Qsign").ToString <> "NOT CHECKED" And
                DateDiff(DateInterval.Day, Now, dt) < 2 Then

                    us = "A"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "R"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "L"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "U"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "B"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "E"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "N"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "P"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If
                    us = "Q"
                    If row(us & "sign").ToString = "CHECKED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                    End If

                End If

                If InStr(row("Asign").ToString & row("Rsign").ToString & row("Lsign").ToString & row("Usign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString, "CHECKED", CompareMethod.Text) <= 0 And
                    DateDiff(DateInterval.Day, Now, dt) < 2 Then

                    us = "A"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "R"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "L"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "U"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "B"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "E"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "N"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "P"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                    us = "Q"
                    If row(us & "sign").ToString = "APPROVED" Then
                        mailSender("ECR_" & us & "_SignTo", "ECR_" & us & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                    End If
                End If
            End If
            refresh = False
        Next
    End Sub

    Function mailSender(ByVal AddlistTo As String, ByVal AddlistCopy As String, ByVal bodyText As String, ByVal SubText As String, ByVal Necr As String, Optional ByVal freq As Boolean = True, Optional ByVal ATTACH As String = "") As Boolean
        mailSender = False
        Dim dt As New DateTime, freqTo As String = "", freqcc As String = ""
        dt = Now
        tblmail.Clear()
        Dsmail.Clear()
        mailSender = False
        Adaptermail.SelectCommand = New MySqlCommand("SELECT * FROM mail;", MySqlconnection)
        Adaptermail.Fill(Dsmail, "mail")
        tblmail = Dsmail.Tables("mail")

        Dim client As New SmtpClient(ParameterTable("SMTP"), ParameterTable("SMTP_PORT"))
        client.EnableSsl = ParameterTable("MAIL_SSL") = "YES"
        client.Credentials = New NetworkCredential(ParameterTable("MAIL_SENDER_CREDENTIAL_USER"), ParameterTable("MAIL_SENDER_CREDENTIAL_PSW"))

        Dim msg As New MailMessage(ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"), ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"))
        Dim RowSearchMail As DataRow()
        RowSearchMail = tblmail.Select("list = '" & AddlistTo & "'")
        msg.To.Clear()
        msg.CC.Clear()

        For Each row In RowSearchMail
            msg.To.Add(row("name").ToString)
            freqTo = row("freq").ToString
        Next
        RowSearchMail = tblmail.Select("list = '" & AddlistCopy & "'")
        For Each row In RowSearchMail
            msg.CC.Add(row("name").ToString)
            freqcc = row("freq").ToString
        Next
        If ATTACH <> "" Then
            Dim Allegato = New Attachment(ATTACH)
            If My.Computer.FileSystem.GetFileInfo(ATTACH).Length < Val(ParameterTable("MAX_SIZE_FILE_MAIL")) Then
                msg.Attachments.Add(Allegato)
                msg.Body = bodyText
            Else
                msg.Body = "ATTENTION FILE NOT SENT BY MAIL FOR EXCESSIVE DIMENSION. PLEASE DOWNLOAD FROM SERVER!!!" & vbCrLf & vbCrLf & bodyText
            End If
        Else
            msg.Body = bodyText
        End If

        msg.Subject = SubText

        If freq = False Then
            freqcc = ""
            freqTo = ""
        End If

        Try
            If DayOfWeek.Saturday <> dt.DayOfWeek And DayOfWeek.Sunday <> dt.DayOfWeek Then
                'If (InStr(freqTo, "[" & Necr & "]", CompareMethod.Text) <= 0) And Necr <> "DAILY" Or ((dt.Hour = 9) And (dt.Minute() >= 0 And dt.Minute() < 59)) Then
                If (InStr(freqTo, "[" & Necr & "]", CompareMethod.Text) <= 0) And Necr <> "DAILY" Or (dt.Hour > 8 And dt.Hour < 20) Then
                    client.Send(msg)
                    MailSent = True
                    ListBoxLog.Items.Add("E mail sent: " & SubText & "  " & Mid(msg.To.Item(0).ToString, 1, 45) & " ....")
                    mailSender = True
                    Application.DoEvents()
                    Application.DoEvents()
                    RowSearchMail = tblmail.Select("list = '" & AddlistTo & "'")
                    For Each row In RowSearchMail
                        WriteField("freq", row("freq").ToString & "[" & Necr & "]", row("id").ToString)
                    Next
                    RowSearchMail = tblmail.Select("list = '" & AddlistCopy & "'")
                    For Each row In RowSearchMail
                        WriteField("freq", row("freq").ToString & "[" & Necr & "]", row("id").ToString)
                    Next
                End If
            End If

        Catch ex As Exception
            ListBoxLog.Items.Add("Mail not sent...!!!")
        End Try
        Application.DoEvents()
    End Function


    Sub ExploreFile(ByVal strDir As String)

        Dim objFtp As ftp = New ftp()
        Dim strList As String, posI As Long, posL As Long, strRes As String, strRec As String
        Dim RowSearch As DataRow()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd

        strList = "*.*"
        strRes = objFtp.ListDirectory(strDir, strList)
        posI = 0
        posL = InStr(1, strList, vbCrLf, CompareMethod.Text)
        While posL > 0 And strRes = "5000"
            ' discover all directories present in the department directory
            strRec = Mid(strList, posI + 1, posL - posI)
            If Mid(strRec, 1, 1) = "d" Then ' directory

                ExploreFile(strDir & Mid(strRec, 56, Len(strRec) - 56) & "/")
            Else 'file

                RowSearch = tblDoc.Select("header='" & Mid(Mid(strRec, 56), 1, 11) &
                "' and FileName='" & Mid(Mid(strRec, 56), 13, InStrRev(Mid(strRec, 56), "_", -1) - 13) &
                "' and rev=" & Mid(Mid(strRec, 56), InStrRev(Mid(strRec, 56), "_", -1) + 1, InStrRev(Mid(strRec, 56), ".", -1) - InStrRev(Mid(strRec, 56), "_", -1) - 1) &
                " and Extension='" & Mid(Mid(strRec, 56), InStrRev(Mid(strRec, 56), ".", -1) + 1, Len(Mid(Mid(strRec, 56), InStrRev(Mid(strRec, 56), ".", -1) + 1)) - 1) & "' ")

                If RowSearch.Length = 1 Then

                ElseIf RowSearch.Length > 1 Then ' db error
                    ComunicationLog("0052")
                Else  ' record not find
                    If CheckBoxDeleteFile.Checked = True Then

                        If MsgBox("Do you want to delete the file: " & Mid(strRec, 56, Len(strRec) - 56), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            strRes = objFtp.DeleteFile(strDir, Mid(strRec, 56, Len(strRec) - 56))
                            If strRes = "5000" Then
                                ComunicationLog("5073")
                            Else
                                ComunicationLog("0056")
                            End If
                        End If
                    End If
                End If
            End If
            posI = posL + 1
            posL = InStr(posL + 1, strList, vbCrLf, CompareMethod.Text)
            ButtonCompare.Text = posL
            Application.DoEvents()
        End While

    End Sub

    Function downloadFileWinPath(ByVal fileName As String, ByVal strPathFtp As String) As String
        Dim objFtp As ftp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""


        Dim cmd As New MySqlCommand()
        If fileName <> "" Then
            Try

                ComunicationLog(objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, fileName)) ' download successfull
                downloadFileWinPath = System.IO.Path.GetTempPath & fileName
            Catch ex As Exception
                ComunicationLog("0049") ' Error in ecr Download
            End Try
        Else
            ComunicationLog("5061") ' fill path
        End If

    End Function
    Function RemotePresence(ByVal link As String, ByVal header As String, ByVal FileName As String, ByVal Extension As String, ByVal rev As Integer) As String

        Try

            My.Computer.Network.DownloadFile(link, "C:\DocumentsTMP\" & header & "_" & FileName & "_" & rev & "." & Extension, "", "", True, 3000, True, FileIO.UICancelOption.DoNothing)
            Application.DoEvents()
            RemotePresence = "OK"
            Application.DoEvents()
            Try
                If rev > 0 Then My.Computer.FileSystem.DeleteFile("C:\DocumentsTMP\" & header & "_" & FileName & "_" & (rev - 1) & "." & Extension)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            RemotePresence = "FAIL"
            My.Computer.FileSystem.DeleteFile("C:\DocumentsTMP\" & header & "_" & FileName & "_" & (rev) & "." & Extension)
        End Try

    End Function
    Function LocalRevision(ByVal header As String, ByVal FileName As String, ByVal Extension As String) As Integer

        Dim returnValue As DataRow()
        Try

            returnValue = tblDoc.Select("header='" & header & "' and FileName='" & FileName & "' and Extension='" & Extension & "' ", "rev DESC")
            If returnValue.Length >= 1 Then
                LocalRevision = returnValue(0).Item("rev").ToString
            ElseIf returnValue.Length = 0 Then ' no file in DB
                LocalRevision = -1 ' file not find
            End If
        Catch ex As Exception
            LocalRevision = -2 ' error
        End Try

    End Function


    ' Elimina i record duplicati nel DB
    Sub DelDuplicate()
        tblDoc.Clear()
        DsDoc.Clear()
        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC", MySqlconnection)
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        Dim returnValue As DataRow(), returnsel As DataRow(), sql As String, First As Integer, i As Long
        returnValue = tblDoc.Select("header like '*'")
        For Each row In returnValue
            returnsel = tblDoc.Select("header='" & row("header").ToString & "' and FileName='" & row("FileName").ToString & "' and Extension='" & row("Extension").ToString & "' and rev ='" & row("rev").ToString & "'", "rev DESC")
            If returnsel.Length > 1 Then
                First = 1
                For Each rows In returnsel
                    If First = 0 Then
                        Try
                            If MsgBox("Do you want to delete " & rows("header").ToString & " - " & rows("FileName").ToString & "_" & rows("rev").ToString & "." & rows("Extension").ToString & "  records?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                sql = "UPDATE `" & DBName & "`.`doc` SET `control` = 'CANCEL'" &
                                " WHERE `doc`.`id` = " & rows("id").ToString & " ;"
                                WriteCheckTable("Delete Duplicate : " & rows("header").ToString & " - " & rows("FileName").ToString)
                                cmd = New MySqlCommand(sql, MySqlconnection)
                                cmd.ExecuteNonQuery()
                            End If
                        Catch ex As Exception
                            ComunicationLog("5050") ' Mysql update query error 
                        End Try
                    End If
                    First = 0
                Next
            End If
            ButtonDelDup.Text = Format(100 * (i / returnValue.Length), "#.#")
            i = i + 1
            Application.DoEvents()
        Next
        returnsel = tblDoc.Select("control='CANCEL'")
        If returnsel.Length > 0 Then
            If MsgBox("Do you want to delete " & returnsel.Length & "  records?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                sql = "DELETE FROM `" & DBName & "`.`doc` WHERE `doc`.`control` = 'CANCEL'"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                ButtonDelDup.Text = "Delete record Duplicate"
            End If
        End If
        ButtonDelDup.Text = "Finish Execution"
    End Sub

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        e.Cancel = True
        If closeform = True Then e.Cancel = False 'keeps form from closing

    End Sub

    Sub WriteField(ByVal field As String, ByVal v As String, ByVal list As String)
        Dim SQL As String
        Try
            SQL = "UPDATE `" & DBName & "`.`mail` SET `" & field & "` = '" & v & "' WHERE `mail`.`id` = " & list & " ;"
            cmd = New MySqlCommand(SQL, MySqlconnection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ComunicationLog("0052") 'db operation error
        End Try
    End Sub
    Sub ComunicationLog(ByVal ComCode As String)

        Dim rsResult As DataRow()
        rsResult = tblError.Select("code='" & ComCode & "'")
        If rsResult.Length = 0 Then
            ComCode = "0051"
            rsResult = tblError.Select("code='" & ComCode & "'")
        End If

        ListBoxLog.Items.Add(ComCode & " -> " & rsResult(0).Item("en").ToString)

        If Val(ComCode) >= 5000 Then
            If ListBoxLog.BackColor = Color.OrangeRed Then
            Else
                ListBoxLog.BackColor = Color.LightGreen
            End If
        ElseIf Val(ComCode) < 5000 Then
            ListBoxLog.BackColor = Color.OrangeRed
        End If

    End Sub


    Private Sub ButtonSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSchedule.Click

        TimerECR.Enabled = False
        TimerECR_Tick(Me, e)
        TimerECR.Enabled = True
    End Sub



    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

        Try

            Me.Show()

            Me.WindowState = FormWindowState.Normal

            NotifyIcon1.Visible = False



        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub



    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize

        Try

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Minimized
                NotifyIcon1.Visible = True
                Me.Hide()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Sub ecrDocApprove()
        Dim RowSearchEcr As DataRow(), sql As String, refresh As Boolean = True
        RowSearchEcr = tblEcr.Select("docInvalid = 'NO'", "number")
        For Each row In RowSearchEcr
            Dim i As Integer
            i = Int(row("number").ToString)

            If InStr(row("Nsign").ToString & row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Qsign").ToString & row("Esign").ToString & row("Psign").ToString & row("Asign").ToString & row("Bsign").ToString, "CHECKED", CompareMethod.Text) <= 0 And row("approve").ToString = "" Then
                Try


                    Dim fileOpen As String = ""
                    fileOpen = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                    If mailSender("ECR_SignTo", "ECR_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                               vbCrLf & row("description").ToString & " -- > (Result: Approved) " &
                               vbCrLf & "Approval Data : " & row("date").ToString & "( yyyy/mm/dd )" & vbCrLf &
                               vbCrLf & vbCrLf & "R&D Note: " & rtfTrans(row("rnote").ToString) & vbCrLf &
                               vbCrLf & "Purchasing Note: " & rtfTrans(row("unote").ToString) & vbCrLf &
                               vbCrLf & "Logistic Note: " & rtfTrans(row("lnote").ToString) & vbCrLf &
                               vbCrLf & "Testing Engineering Note: " & rtfTrans(row("enote").ToString) & vbCrLf &
                               vbCrLf & "Time & Methods Note: " & rtfTrans(row("qnote").ToString) & vbCrLf &
                               vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                               vbCrLf & "Process Engineering Note: " & rtfTrans(row("Bnote").ToString) & vbCrLf &
                               vbCrLf & "Production Note: " & rtfTrans(row("pnote").ToString) & vbCrLf &
                               vbCrLf & "Admin Note: " & rtfTrans(row("anote").ToString) & vbCrLf &
                               vbCrLf & "For all details please download it from server SrvDoc. ", "ECR APPROVE Notification:   " & " " & row("description").ToString, "SS" & row("number").ToString, False, fileOpen) Then
                        sql = "UPDATE `" & DBName & "`.`ecr` SET `approve` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `ecr`.`approve` ='' and `ecr`.`number` = '" & i & "' ;"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Else
                        MsgBox("Error sending email ECR approval!")
                    End If

                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
            Application.DoEvents()
            refresh = False
        Next


    End Sub

    Private Sub TextBoxEcr_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEcr.TextChanged

    End Sub

    Private Sub TextBoxService_TextChanged(sender As Object, e As EventArgs) Handles TextBoxService.TextChanged

    End Sub
End Class