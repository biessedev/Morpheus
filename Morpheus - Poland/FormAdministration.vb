﻿Option Explicit On
Option Compare Text

Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.Net
Imports System.Linq
Imports System.Configuration

Public Class FormAdministration

    Dim closeform As Boolean
    Dim dep As New List(Of String)
    Dim CultureInfo_ja_JP As New CultureInfo("ja-JP", False)
    Dim MailSent As Boolean
    Dim tblDoc As DataTable, tblDocType As DataTable, tblEcr As DataTable, tblProd As DataTable, tblmail As DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsEcr As New DataSet, DsProd As New DataSet, Dsmail As New DataSet
    Dim userDep3 As String
    Dim cmd As New MySqlCommand()
    Dim notDeletedFiles As List(Of String) = New List(Of String)

    Private Sub FormAdministration_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()
    End Sub

    Private Sub FormAdministration_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim conBuilder As New Common.DbConnectionStringBuilder()
        conBuilder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(conBuilder("host"), conBuilder("database"), conBuilder("username"), conBuilder("password"))
            Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM ecr;", con)
                AdapterEcr.Fill(DsEcr, "ecr")
            End Using
            tblEcr = DsEcr.Tables("ecr")

            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")

            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM product;", con)
                AdapterProd.Fill(DsProd, "product")
            End Using
            tblProd = DsProd.Tables("product")

            Using Adaptermail As New MySqlDataAdapter("SELECT * FROM mail;", con)
                Adaptermail.Fill(Dsmail, "mail")
            End Using
            tblmail = Dsmail.Tables("mail")
        End Using
        dep.Add("U")
        dep.Add("L")
        dep.Add("C")
        dep.Add("B")
        dep.Add("E")
        dep.Add("N")
        dep.Add("P")
        dep.Add("Q")
        dep.Add("F")
        dep.Add("S")

    End Sub

    Private Sub ButtonSchedule_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSchedule.Click
        TimerECR.Enabled = False
        TimerECR_Tick(Me, e)
        TimerECR.Enabled = True
    End Sub

    Private Sub TimerECR_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerECR.Tick

        FormStart.Hide()
        ParameterTableWrite("SYSTEM_SCHEDULE", "RUN")
        Dim builder As New Common.DbConnectionStringBuilder()

        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            Dim conBuilder As New Common.DbConnectionStringBuilder()
            conBuilder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            TextBoxEcr.Text = date_to_string(Now) & " Start ECR"
            Application.DoEvents()
            UpdateEcrTable()
            Application.DoEvents()
            EcrMailScheduler()
            Application.DoEvents()
            ecrDocConfirm()
            Application.DoEvents()
            ecrDocApprove()
            Application.DoEvents()
            ecrDocSign()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish ECR"
        End If

        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            TextBoxEcr.Text = date_to_string(Now) & " Start TCR"
            Application.DoEvents()
            TCRMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish TCR"
        End If

        ' DOC
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            TextBoxEcr.Text = date_to_string(Now) & " Start Documentation"
            Application.DoEvents()
            DocMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish Documentation"
        End If

        ' Status
        If Now.DayOfWeek <> DayOfWeek.Saturday And Now.DayOfWeek <> DayOfWeek.Sunday Then
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            TextBoxEcr.Text = date_to_string(Now) & " Start Satus Product"
            Application.DoEvents()
            StatusMailScheduler()
            Application.DoEvents()
            TextBoxEcr.Text = date_to_string(Now) & " Finish Satus Product"
        End If

        TextBoxEcr.Text = date_to_string(Now) & " Email notification completed"

        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using Adaptermail As New MySqlDataAdapter("SELECT * FROM mail;", con)
                Adaptermail.Fill(Dsmail, "mail")
            End Using
            tblmail = Dsmail.Tables("mail")
        End Using

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

    Sub UpdateEcrTable()
        Dim RowEcr As DataRow(), pos As Integer
        Dim EcrN As Integer, sql As String, filename As String
        Dim RowSearchDoc As DataRow()

        RowSearchDoc = tblDoc.Select("header = '" & ParameterTable("plant") & "R_PRO_ECR'")

        For Each row In RowSearchDoc
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                tblEcr.Clear()
                DsEcr.Clear()
                Using AdapterEcr As New MySqlDataAdapter("SELECT * FROM ecr;", con)
                    AdapterEcr.Fill(DsEcr, "ecr")
                End Using
                tblEcr = DsEcr.Tables("ecr")

                pos = InStr(1, row("filename").ToString, "-", CompareMethod.Text)
                EcrN = Val(Mid(row("filename").ToString, 1, pos))
                RowEcr = tblEcr.Select("number=" & EcrN)
                If EcrN > 0 And RowEcr.Length = 0 And InStr(row("filename").ToString, "template", CompareMethod.Text) <= 0 Then
                    Try
                        filename = row("filename").ToString & "_" & row("rev").ToString & "." & row("extension").ToString
                        sql = "INSERT INTO `" & DBName & "`.`ecr` (`nnote` ,`number` ,`description` ,`date`,`Usign`,`nsign`,`Lsign`,`Asign`,`Qsign`,`Esign`,`Rsign`,`Psign`,`Bsign`,`Ssign`,`DocInvalid`,`IdDoc`,`CLCV`) VALUES (" &
                        Replace("'{\rtf1\fbidis\ansi\ansicpg1252\deff0{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}{\f1\fswiss\fprq2\fcharset0 Calibri;}}{\colortbl ;\red23\green54\blue93;}\viewkind4\uc1\pard\ltrpar\sl360\slmult1\cf1\lang1040\f0\fs22\par\par\par\par\ul\b\i\f1 Confirmation AREA\par\lang1033\ulnone\b0\i0 Time and First serial number / Fiche:\par\par\par\parOther Annotation:\f0\par\pard\ltrpar\cf0\lang1040\fs24\par\par\par\par}', ", "\", "\\") _
                        & EcrN & ", '" & filename & "', '" & "01/01/2000" & "', 'NOT CHECKED' , 'NOT CHECKED', 'NOT CHECKED', 'System[automatic]', 'NOT CHECKED', 'NOT CHECKED', 'NOT CHECKED', 'NOT CHECKED', 'NOT CHECKED','NOT CHECKED', 'NO', " & row("id").ToString & ",'NO');"
                        cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        ComunicationLog("5050") ' Mysql update query error, check if bitron p/n is already in db
                    End Try

                End If
            End Using
        Next
    End Sub

    Sub EcrMailScheduler()
        Dim refresh = True
        Dim RowSearchEcr As DataRow() = tblEcr.Select("")
        For Each row In RowSearchEcr

            If readDocSign(row("iddoc").ToString, refresh) = "" Then

                If row("ecrcheck").ToString <> "YES" Then
                    mailSender("ECR_" & "VerifyTo", "ECR_" & "VerifyCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please VERIFY the ECR: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                End If

                Dim us As String = getAllDepartmentInitialsForAutomaticSrvDocMessage()
                For Each c As String In us
                    If ((row(c & "sign").ToString = "NOT CHECKED") And (row("ecrcheck").ToString = "YES")) Then
                        mailSender("ECR_" & c & "_SignTo", "ECR_" & c & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please CHECK the ECR: " & " " & row("description").ToString, "ECR Check Request " & " " & row("description").ToString, row("number").ToString)
                    End If
                Next

                Dim dt As Date = string_to_date((row("date").ToString))

                If row("Rsign").ToString <> "NOT CHECKED" And
                row("Usign").ToString <> "NOT CHECKED" And
                row("Lsign").ToString <> "NOT CHECKED" And
                row("Bsign").ToString <> "NOT CHECKED" And
                row("Esign").ToString <> "NOT CHECKED" And
                row("Nsign").ToString <> "NOT CHECKED" And
                row("Psign").ToString <> "NOT CHECKED" And
                row("Qsign").ToString <> "NOT CHECKED" And
                row("Ssign").ToString <> "NOT CHECKED" And
                DateDiff(DateInterval.Day, Now, dt) < 2 Then

                    us = getAllDepartmentInitialsForAutomaticSrvDocMessage()
                    For Each c As String In us
                        If row(c & "sign").ToString = "CHECKED" Then
                            mailSender("ECR_" & c & "_SignTo", "ECR_" & c & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please APPROVE the Ecr: " & " " & row("description").ToString, "ECR Approval Request " & row("description").ToString, row("number").ToString & "A")
                        End If
                    Next

                End If

                If InStr(row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString & row("Asign").ToString, "CHECKED", CompareMethod.Text) <= 0 And
                    DateDiff(DateInterval.Day, Now, dt) < 2 Then

                    us = getAllDepartmentInitialsForAutomaticSrvDocMessage()
                    For Each c As String In us
                        If row(c & "sign").ToString = "APPROVED" Then
                            mailSender("ECR_" & c & "_SignTo", "ECR_" & c & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf & "Please SIGN the Ecr: " & " " & row("description").ToString, "ECR Sign Request " & row("description").ToString, row("number").ToString & "S")
                        End If
                    Next
                End If
            End If
            refresh = False
        Next
    End Sub

    Private Function getAllDepartmentInitialsForAutomaticSrvDocMessage() As String
        Return "ARULBENPQS"
    End Function

    Sub ecrDocConfirm()
        Dim sql As String, refresh = True
        Dim RowSearchEcr As DataRow() = tblEcr.Select("docInvalid = 'NO'", "number")
        For Each row In RowSearchEcr
            If InStr(row("Rsign").ToString & row("Lsign").ToString & row("Usign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString & row("Asign").ToString & row("Ssign").ToString, "APPROVED", CompareMethod.Text) <= 0 And readDocSign(row("iddoc").ToString, refresh) <> "" And
                row("confirm").ToString = "CONFIRMED" Then

                Dim fileOpen As String = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                Try
                    Dim builder As New Common.DbConnectionStringBuilder()
                    builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                    Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))

                        If mailSender("Status_SignTo", "Status_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                                   vbCrLf & row("description").ToString & " -- > (Result: Confirmation of ECR introduction) " & vbCrLf & vbCrLf &
                                   vbCrLf & "Validate Data :" & row("date").ToString & " (yyyy/mm/dd)" & vbCrLf &
                                   vbCrLf & vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                                   vbCrLf & vbCrLf & vbCrLf & "For all details please download the ECR from server SrvDoc.", "ECR - Confirmation of introduction:   " & " " & row("description").ToString, "C" & row("number").ToString, False, fileOpen) Then
                            sql = "UPDATE `" & DBName & "`.`ECR` SET `confirm` = 'SENT_CONFIRMED' WHERE `ECR`.`id` = " & row("id").ToString & " ;"
                            cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        Else
                            MsgBox("ECR confirmation email has not been sent!")
                        End If

                        cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    End Using
                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
            refresh = False
        Next
    End Sub

    Sub ecrDocApprove()
        Dim RowSearchEcr As DataRow() = tblEcr.Select("docInvalid = 'NO'", "number")
        For Each row In RowSearchEcr
            Dim i As Integer
            i = Int(row("number").ToString)

            If InStr(row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString & row("Asign").ToString & row("Ssign").ToString, "CHECKED", CompareMethod.Text) <= 0 And row("approve").ToString = "" Then
                Try
                    Dim fileOpen As Object = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                    If mailSender("ECR_SignTo", "ECR_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                               vbCrLf & row("description").ToString & " -- > (Result: Approved) " &
                               vbCrLf & "Approval Data : " & row("date").ToString & "( yyyy/mm/dd )" & vbCrLf &
                               vbCrLf & vbCrLf & "R&D Note: " & rtfTrans(row("rnote").ToString) & vbCrLf &
                               vbCrLf & "Logistic Note: " & rtfTrans(row("lnote").ToString) & vbCrLf &
                               vbCrLf & "Purchasing Note: " & rtfTrans(row("unote").ToString) & vbCrLf &
                               vbCrLf & "Process Engineering Note: " & rtfTrans(row("Bnote").ToString) & vbCrLf &
                               vbCrLf & "Testing Engineering Note: " & rtfTrans(row("enote").ToString) & vbCrLf &
                               vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                               vbCrLf & "Production Note: " & rtfTrans(row("pnote").ToString) & vbCrLf &
                               vbCrLf & "Time & Methods Note: " & rtfTrans(row("qnote").ToString) & vbCrLf &
                               vbCrLf & "Admin Note: " & rtfTrans(row("anote").ToString) & vbCrLf &
                               vbCrLf & "Environment And Safety Note: " & rtfTrans(row("Snote").ToString) & vbCrLf &
                               vbCrLf & "For all details please download the ECR from server SrvDoc. ", "ECR Approval Notification:   " & " " & row("description").ToString, "SS" & row("number").ToString, False, fileOpen) Then
                        Dim builder As New Common.DbConnectionStringBuilder()
                        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                            Dim sql As String = "UPDATE `" & DBName & "`.`ecr` SET `approve` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `ecr`.`approve` ='' and `ecr`.`number` = '" & i & "' ;"
                            cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        MsgBox("ECR approval email has not been sent!")
                    End If

                Catch ex As Exception
                    ComunicationLog("0052") 'db operation error
                End Try
            End If
            Application.DoEvents()
        Next
    End Sub

    Sub ecrDocSign()
        Dim refresh = True
        Dim RowSearchEcr As DataRow() = tblEcr.Select("docInvalid = 'NO'", "number")
        For Each row In RowSearchEcr
            Dim i As Integer
            i = Int(row("number").ToString)

            If row("sign").ToString = "" And InStr(row("Rsign").ToString & row("Usign").ToString & row("Lsign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString & row("Asign").ToString & row("Ssign").ToString, "APPROVED", CompareMethod.Text) <= 0 And InStr(row("Rsign").ToString & row("Lsign").ToString & row("Usign").ToString & row("Bsign").ToString & row("Esign").ToString & row("Nsign").ToString & row("Psign").ToString & row("Qsign").ToString & row("asign").ToString & row("ssign").ToString, "CHECKED", CompareMethod.Text) <= 0 And readDocSign(Int(row("iddoc").ToString), refresh) = "" Then
                Try
                    Dim fileOpen As Object = downloadFileWinPath(ParameterTable("plant") & "R_PRO_ECR_" & row("DESCRIPTION").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_ECR/")
                    If mailSender("ECR_SignTo", "ECR_SignCopy", "Automatic SrvDoc Message:" & vbCrLf &
                               vbCrLf & row("description").ToString & " -- > (Result: Signed, Released & Implemented) " &
                               vbCrLf & "Closed Data : " & row("date").ToString & "( yyyy/mm/dd )" & vbCrLf &
                               vbCrLf & vbCrLf & "R&D Note: " & rtfTrans(row("rnote").ToString) & vbCrLf &
                               vbCrLf & "Logistic Note: " & rtfTrans(row("lnote").ToString) & vbCrLf &
                               vbCrLf & "Purchasing Note: " & rtfTrans(row("unote").ToString) & vbCrLf &
                               vbCrLf & "Process Engineering Note: " & rtfTrans(row("Bnote").ToString) & vbCrLf &
                               vbCrLf & "Testing Engineering Note: " & rtfTrans(row("enote").ToString) & vbCrLf &
                               vbCrLf & "Quality Note: " & rtfTrans(row("nnote").ToString) & vbCrLf &
                               vbCrLf & "Production Note: " & rtfTrans(row("pnote").ToString) & vbCrLf &
                               vbCrLf & "Time & Methods Note: " & rtfTrans(row("qnote").ToString) & vbCrLf &
                               vbCrLf & "Administration Note: " & rtfTrans(row("anote").ToString) & vbCrLf &
                               vbCrLf & "Environment And Safety Note: " & rtfTrans(row("snote").ToString) & vbCrLf &
                               vbCrLf & "For all details please download the ECR from server SrvDoc. ", "ECR Signature Notification:   " & " " & row("description").ToString, "SS" & row("number").ToString, False, fileOpen) Then
                        Dim builder As New Common.DbConnectionStringBuilder()
                        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                            Dim sql As String = "UPDATE `" & DBName & "`.`ecr` SET `sign` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `ecr`.`sign` ='' and `ecr`.`number` = '" & i & "' ;"
                            cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                            sql = "UPDATE `" & DBName & "`.`doc` SET `sign` = '" & "System" & "[" & date_to_string(Now) & "]" & "' WHERE `doc`.`sign` ='' and `doc`.`id` = '" & row("iddoc").ToString & "' ;"
                            cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        End Using
                    Else
                        MsgBox("ECR signature email has not been sent!")
                    End If

                Catch ex As Exception
                    ComunicationLog("0052") 'db operation failed
                End Try
            End If
            Application.DoEvents()
            refresh = False
        Next
    End Sub

    Sub TCRMailScheduler()
        tblDoc.Clear()
        DsDoc.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")
            Dim RowSearchDoc As DataRow() = tblDoc.Select("sign = '' and HEADER='" & ParameterTable("plant") & "R_PRO_TCR'")
            For Each row In RowSearchDoc
                Dim oi As String = Trim(Mid(row("filename").ToString, 1, InStr(row("filename").ToString, "-") - 1))
                Dim fileOpen As Object = downloadFileWinPath(ParameterTable("plant") & "R_PRO_TCR_" & row("filename").ToString & "_" & row("rev").ToString & "." & row("extension").ToString, ParameterTable("plant") & "R/" & ParameterTable("plant") & "R_PRO_TCR/")
                Try
                    If mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                                   "Please CHECK the TCR : " & " " & row("filename").ToString & " " & vbCrLf & vbCrLf & "Best Regard", "TCR Signature Notification  " & " " &
                                   row("filename").ToString, "T_" & oi, False, fileOpen) Then
                        Dim sql As String = "UPDATE `" & DBName & "`.`doc` SET `sign` = 'System[" & date_to_string(Now) & "]' WHERE `doc`.`id` = " & row("id").ToString & " ;"
                        cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    Else
                        MsgBox("Email for TCR has not been sent!")
                    End If

                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error 
                End Try
            Next
        End Using
    End Sub

    Sub DocMailScheduler()
        Dim listFile = ""
        tblDoc.Clear()
        DsDoc.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")
            Dim sql As String
            Dim RowSearchDoc = From p In tblDoc.Rows
                               Where (p("header") <> (ParameterTable("plant") & "R_PRO_ECR")) And ((p("notification") = "" And p("sign") = "") Or (p("notification") = "SENT" And p("sign") = "" And (DateTime.Now.Date - DateTime.ParseExact(p("editor").Substring(p("editor").IndexOf("[") + 1, p("editor").LastIndexOf("]") - p("editor").IndexOf("[") - 1), "d/M/yyyy", CultureInfo.CurrentCulture).Date).TotalDays > 2))
                               Select p
            For Each row In RowSearchDoc
                listFile = listFile & " " & vbCrLf & row("header").ToString & "_" & row("FileName").ToString & "_" & row("rev").ToString & "." & row("Extension").ToString & " " & vbCrLf
            Next
            Try
                MailSent = False
                If listFile <> "" Then
                    mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                               vbCrLf & "Please CHECK the new document/revision: " & " " & vbCrLf & vbCrLf & listFile & vbCrLf & vbCrLf & "Best Regard", "File changes notification  " &
                               date_to_string(Now), date_to_string(Now), True)
                End If
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try

            For Each row In RowSearchDoc
                Try
                    sql = "UPDATE `" & DBName & "`.`doc` SET `notification` = 'SENT' WHERE `notification` = '' and sign ='';"
                    If MailSent = True Then
                        cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                    End If
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error 
                End Try
            Next
        End Using
    End Sub

    Function readDocSign(ByVal docId As Long, ByVal refresh As Boolean) As String
        If refresh = True Then
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC", con)
                    AdapterDoc.Fill(DsDoc, "doc")
                End Using
                tblDoc = DsDoc.Tables("doc")
            End Using
        End If

        Dim Res As DataRow() = tblDoc.Select("id = " & docId)
        If Res.Length > 0 Then
            readDocSign = Res(0).Item("sign").ToString
        Else
            MsgBox("ECR document not found: " & docId)
        End If
    End Function

    Sub StatusMailScheduler()
        tblProd.Clear()
        DsProd.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterProd As New MySqlDataAdapter("SELECT * FROM product", con)
                AdapterProd.Fill(DsProd, "product")
            End Using
            tblProd = DsProd.Tables("product")

            Dim RowSearchProduct As DataRow(), sql As String
            RowSearchProduct = tblProd.Select("")
            For Each row In RowSearchProduct
                Dim oi As String = Replace(row("openissue").ToString, "];", "]" & vbCrLf)
                If oi = "" Then oi = "No Open Issue"

                If (row("Status").ToString = "MPA_APPROVED" Or row("Status").ToString = "MPA_STOPPED") And row("mail").ToString <> "SENT" Then
                    Try
                        sql = "UPDATE `" & DBName & "`.`product` SET `mail` = 'SENT' WHERE `product`.`BitronPN` = '" & row("BITRONPN").ToString & "' ;"
                        cmd = New MySqlCommand(sql, con)
                        cmd.ExecuteNonQuery()
                        If mailSender("STATUS" & "_SignTo", "STATUS" & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                                   "Please approve the product: " & " " & row("bitronpn").ToString & " " & row("name").ToString & vbCrLf &
                                   vbCrLf & "Open Issue:" & vbCrLf & oi & vbCrLf & vbCrLf & "Best Regards", "Product Status Notification " & row("STATUS").ToString & " " &
                                   row("bitronpn").ToString & " " & row("name").ToString, "S_" & row("bitronpn").ToString, False) Then
                            sql = "UPDATE `" & DBName & "`.`product` SET `mail` = 'SENT' WHERE `product`.`BitronPN` = '" & row("BITRONPN").ToString & "' ;"
                            cmd = New MySqlCommand(sql, con)
                            cmd.ExecuteNonQuery()
                        Else
                            MsgBox("Email for product approval has not been sent!")
                        End If

                    Catch ex As Exception
                        ComunicationLog("5050") ' Mysql update query error 
                    End Try

                    oi = Replace(row("openissue").ToString, "];", "]" & vbCrLf)
                    If oi = "" Then oi = "No Open Issue at this moment"
                End If

                For Each SS In dep
                    If prevStatus(SS) = row("Status").ToString Or (row("Status").ToString = "MPA_STOPPED" And SS = "N") Then
                        Try

                            mailSender("STATUS_" & SS & "_SignTo", "STATUS_" & SS & "_SignCopy", "Automatic SrvDoc Message:" & vbCrLf & vbCrLf &
                                       "Please update the Status of Product: " & " " & row("bitronpn").ToString & " " & row("name").ToString & vbCrLf &
                                       vbCrLf & "Current Status:  " & row("Status").ToString & vbCrLf &
                                       vbCrLf & "Open Issue:" & vbCrLf & vbCrLf & oi & vbCrLf & vbCrLf & "Best Regards", "Product Status Update Request " & " " &
                                       row("bitronpn").ToString & " " & row("name").ToString, SS & "_" & row("bitronpn").ToString)
                        Catch ex As Exception
                            ComunicationLog("5050") ' Mysql update query error 
                        End Try
                    End If
                Next

            Next
        End Using
    End Sub

    Function prevStatus(ByVal dep As String) As String
        If dep = "U" Then prevStatus = "R&D_APPROVED"
        If dep = "L" Then prevStatus = "PURCHASING_APPROVED"
        If dep = "C" Then prevStatus = "LOGISTIC_APPROVED"
        If dep = "B" Then prevStatus = "CUSTOMER_APPROVED"
        If dep = "E" Then prevStatus = "PROCESS_ENG_APPROVED"
        If dep = "P" Then prevStatus = "TESTING_ENG_APPROVED"
        If dep = "Q" Then prevStatus = "PRODUCTION_APPROVED"
        If dep = "F" Then prevStatus = "TIME&METHODS_APPROVED"
        If dep = "N" Then prevStatus = "FINANCIAL_APPROVED"
        If dep = "S" Then prevStatus = "ENVIRONMENT_AND_SAFETY"
    End Function

    Function mailSender(ByVal AddlistTo As String, ByVal AddlistCopy As String, ByVal bodyText As String, ByVal SubText As String, ByVal Necr As String, Optional ByVal freq As Boolean = True, Optional ByVal ATTACH As String = "") As Boolean
        Dim freqTo = ""
        Dim dt As Date = Now
        tblmail.Clear()
        Dsmail.Clear()
        mailSender = False
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using Adaptermail As New MySqlDataAdapter("SELECT * FROM mail;", con)
                Adaptermail.Fill(Dsmail, "mail")
            End Using
        End Using
        tblmail = Dsmail.Tables("mail")

        Dim client As New SmtpClient(ParameterTable("SMTP"), ParameterTable("SMTP_PORT"))
        client.EnableSsl = IIf(ParameterTable("MAIL_SSL") = "YES", True, False)
        client.Credentials = New NetworkCredential(ParameterTable("MAIL_SENDER_CREDENTIAL_USER"), ParameterTable("MAIL_SENDER_CREDENTIAL_PSW"))

        Dim msg As New MailMessage(ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"), ParameterTable("MAIL_SENDER_CREDENTIAL_MAIL"))

        Dim RowSearchMail As DataRow() = tblmail.Select("list = '" & AddlistTo & "'")
        msg.To.Clear()
        msg.CC.Clear()

        For Each row In RowSearchMail
            msg.To.Add(row("name").ToString)
            freqTo = row("freq").ToString
        Next

        RowSearchMail = tblmail.Select("list = '" & AddlistCopy & "'")
        For Each row In RowSearchMail
            msg.CC.Add(row("name").ToString)
        Next

        If ATTACH <> "" Then
            Dim Allegato = New Attachment(ATTACH)
            If My.Computer.FileSystem.GetFileInfo(ATTACH).Length < Val(ParameterTable("MAX_SIZE_FILE_MAIL")) Then
                msg.Attachments.Add(Allegato)
                msg.Body = bodyText
            Else
                msg.Body = "ATTENTION...FILE NOT SENT BY EMAIL FOR EXCESSIVE DIMENSION. PLEASE DOWNLOAD FROM THE SERVER!" & vbCrLf & vbCrLf & bodyText
            End If
        Else
            msg.Body = bodyText
        End If

        msg.Subject = SubText

        If freq = False Then
            freqTo = ""
        End If

        Try
            If DayOfWeek.Saturday <> dt.DayOfWeek And DayOfWeek.Sunday <> dt.DayOfWeek And (dt.Hour > 8 And dt.Hour < 20) Then
                'Dim rowEcr As DataRow() = tblEcr.Select("number = '" & Necr & "'")
                'Dim parsedDate
                'Dim dateList As New List(Of DateTime)
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("date"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateR"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateU"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateL"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateB"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateE"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateN"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateQ"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Add(If(DateTime.TryParse(rowEcr(0).Item("dateP"), parsedDate), parsedDate, Date.Parse("1/1/2012 12:00:00 AM")))
                'dateList.Sort()
                'dateList.Reverse()

                If (InStr(freqTo, "[" & Necr & "]", CompareMethod.Text) <= 0) Then
                    'Or                   ((InStr(freqTo, "[" & Necr & "]", CompareMethod.Text) > 0) And (DateDiff(DateInterval.Day, dateList.ElementAt(0), DateTime.Now) >= 3)) Then
                    client.Send(msg)
                    MailSent = True
                    ListBoxLog.Items.Add("Email sent: " & SubText & "  " & Mid(msg.To.Item(0).ToString, 1, 45) & " ....")
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
            ListBoxLog.Items.Add("Email has not been sent!")
        End Try
        Application.DoEvents()
    End Function

    Sub WriteField(ByVal field As String, ByVal v As String, ByVal list As String)
        Try
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Dim SQL As String = "UPDATE `" & DBName & "`.`mail` SET `" & field & "` = '" & v & "' WHERE `mail`.`id` = " & list & " ;"
                cmd = New MySqlCommand(SQL, con)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ComunicationLog("0052") 'db operation error
        End Try
    End Sub

    Private Sub ButtonCompare_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCompare.Click
        Dim objFtp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd

        TimerECR.Stop()
        DsDoc.Clear()
        tblDoc.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC ", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using

            tblDoc = DsDoc.Tables("doc")

            Dim RowSearch As DataRow() = tblDoc.Select("filename like '*'")
            Dim i As Long = 0
            For Each row In RowSearch
                Try

                    Dim strPathFtp As String = (Mid(row("header").ToString, 1, 3) & "/" & row("header").ToString)
                    Application.DoEvents()

                    Dim strListDir As String = row("header").ToString & "_" & row("filename").ToString _
                                               & "_" & row("rev").ToString & "." & row("extension").ToString

                    Dim strRes As String = objFtp.CheckFile(strPathFtp & "/", strListDir)

                    If strRes <> "5000" Then
                        strRes = objFtp.CheckFile(strPathFtp & "/", strListDir)
                    End If

                    If strRes <> "5000" Then
                        strRes = objFtp.CheckFile(strPathFtp & "/", strListDir)
                    End If

                    If strRes <> "5000" Then
                        strRes = objFtp.CheckFile(strPathFtp & "/", strListDir)
                    End If

                    If strRes <> "5000" Then
                        strRes = objFtp.CheckFile(strPathFtp & "/", strListDir)
                    End If

                    If strRes <> "5000" Then
                        strRes = objFtp.CheckFile(strPathFtp & "/", strListDir)
                    End If

                    If strRes <> "5000" Then

                        If CheckBoxDeleteRecord.Checked = True Then
                            Try

                                If MsgBox("Do you want to delete the record? " & row("header").ToString & "_" & row("filename").ToString &
                                "_" & row("rev").ToString & "." & row("extension").ToString, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    Dim sql As String = "DELETE FROM `" & DBName & "`.`doc` WHERE `doc`.`id` = " & row("id").ToString
                                    cmd = New MySqlCommand(sql, con)
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
        End Using

        Me.notDeletedFiles = New List(Of String)
        ExploreFile("/")
        If notDeletedFiles.Count Then
            ShowMessageBoxWithDetails(notDeletedFiles)
        End If

        ComunicationLog("5075")
        ButtonCompare.Text = "Compare File DB"
        TimerECR.Start()
    End Sub

    Private Sub ShowMessageBoxWithDetails(notDeletedFiles As List(Of String))
        Dim dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg"
        Dim dialogType = (GetType(Windows.Forms.Form)).Assembly.GetType(dialogTypeName)

        ' Create dialog instance.
        Dim dialog = TryCast(Activator.CreateInstance(dialogType, New PropertyGrid()), Form)

        ' Populate relevant properties on the dialog instance.
        Dim strNotDeletedFiles = String.Join(vbCrLf & vbCrLf, notDeletedFiles)
        dialog.Text = "File not deleted!"
        dialogType.GetProperty("Details").SetValue(dialog, strNotDeletedFiles, Nothing)
        dialogType.GetProperty("Message").SetValue(dialog, "Some unallowed file have been found." & vbCrLf & vbCrLf, Nothing)

        Dim okBtn = dialog.Controls.Find("okBtn", True)
        okBtn(0).Visible = False

        Dim cancelBtn = dialog.Controls.Find("cancelBtn", True)
        cancelBtn(0).Text = okBtn(0).Text

        ' Display dialog.
        Dim result = dialog.ShowDialog()
    End Sub

    Private Sub ButtonDelDup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDelDup.Click
        DelDuplicate()
    End Sub

    ' Elimina i record duplicati nel DB
    Sub DelDuplicate()
        tblDoc.Clear()
        DsDoc.Clear()
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM DOC", con)
                AdapterDoc.Fill(DsDoc, "doc")
            End Using
            tblDoc = DsDoc.Tables("doc")

            Dim returnsel As DataRow(), sql As String, i As Long
            Dim returnValue As DataRow() = tblDoc.Select("header like '*'")
            For Each row In returnValue
                returnsel = tblDoc.Select("header='" & row("header").ToString & "' and FileName='" & row("FileName").ToString & "' and Extension='" & row("Extension").ToString & "' and rev ='" & row("rev").ToString & "'", "rev DESC")
                If returnsel.Length > 1 Then
                    Dim First As Integer = 1
                    For Each rows In returnsel
                        If First = 0 Then
                            Try
                                If MsgBox("Do you want to delete " & rows("header").ToString & " - " & rows("FileName").ToString & "_" & rows("rev").ToString & "." & rows("Extension").ToString & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    sql = "UPDATE `" & DBName & "`.`doc` SET `control` = 'CANCEL'" &
                                    " WHERE `doc`.`id` = " & rows("id").ToString & " ;"
                                    WriteCheckTable("Delete Duplicates: " & rows("header").ToString & " - " & rows("FileName").ToString)
                                    cmd = New MySqlCommand(sql, con)
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
                If MsgBox("Do you want to delete " & returnsel.Length & " records?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    sql = "DELETE FROM `" & DBName & "`.`doc` WHERE `doc`.`control` = 'CANCEL'"
                    cmd = New MySqlCommand(sql, con)
                    cmd.ExecuteNonQuery()
                    ButtonDelDup.Text = "Delete duplicated records"
                End If
            End If
        End Using
        ButtonDelDup.Text = "Finish Execution"
    End Sub

    Function rtfTrans(ByVal rtf As String) As String
        Try
            RichTextBoxConv.Rtf = rtf
            rtfTrans = RichTextBoxConv.Text
        Catch ex As Exception
            rtfTrans = ""
        End Try
    End Function

    Sub ExploreFile(ByVal strDir As String)
        Dim objFtp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        Dim strList As String = "*.*"
        Dim strRes As String = objFtp.ListDirectory(strDir, strList)
        Dim posI As Long = 0
        Dim posL As Long = InStr(1, strList, vbCrLf, CompareMethod.Text)
        While posL > 0 And strRes = "5000"
            ' discover all directories present in the department directory
            Dim strRec As String = Mid(strList, posI + 1, posL - posI)
            If Mid(strRec, 1, 1) = "d" Then ' directory
                Dim folder = strRec.Split(" ").Last()
                If strDir = "/" Then
                    strDir = ""
                    ExploreFile(String.Format("{0}{1}", strDir, folder))
                ElseIf strDir.Length = 0 Then
                    Dim path As String = strDir + "/" + folder
                    path = path.Replace(vbCr, "").Replace(vbLf, "")
                    ExploreFile(path)
                ElseIf strDir.Substring(strDir.Length - 1) = "/" Then
                    ExploreFile(String.Format("{0}{1}", strDir, folder))
                Else
                    Dim path As String = strDir + "/" + folder
                    path = path.Replace(vbCr, "").Replace(vbLf, "")
                    ExploreFile(path)
                End If
            Else 'file

                Dim file As String = Mid(strRec, 49).Trim
                Try
                    Dim header As String = strDir.Split("/").Last
                    Dim Extension As String = file.Split(".").Last.Trim
                    Dim rev As String = file.Split("_").Last.Replace(Extension, "").Replace(".", "").Trim
                    Dim FileName As String = file.Substring(0, (file.Length - Extension.Length - rev.Length - 2))
                    FileName = If(header = "", FileName, FileName.Replace(header, ""))
                    FileName = FileName.Substring(1, FileName.Length - 1)

                    Dim RowSearch As DataRow() = tblDoc.Select("header='" & header &
                                                               "' and FileName='" & FileName &
                                                               "' and rev=" & rev &
                                                               " and Extension='" & Extension & "' ")
                    If RowSearch.Length = 1 Then

                    ElseIf RowSearch.Length > 1 Then ' db error
                        ComunicationLog("0052")
                    Else  ' record not find
                        If CheckBoxDeleteFile.Checked = True Then

                            If MsgBox("Do you want to delete the file: " & file, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                strRes = objFtp.DeleteFile(strDir, ("/" & file).Replace(vbCr, "").Replace(vbLf, "").Trim)
                                If strRes = "5000" Then
                                    ComunicationLog("5073")
                                Else
                                    ComunicationLog("0056")
                                End If
                            End If
                        End If
                    End If

                Catch ex As Exception
                    notDeletedFiles.Add(strFtpServerAdd & strDir & "/" & file)
                End Try
            End If
            posI = posL + 1
            posL = InStr(posL + 1, strList, vbCrLf, CompareMethod.Text)
            ButtonCompare.Text = posL
            Application.DoEvents()
        End While

    End Sub

    Function downloadFileWinPath(ByVal fileName As String, ByVal strPathFtp As String) As String
        Dim objFtp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        downloadFileWinPath = ""

        If fileName <> "" Then
            Try

                ComunicationLog(objFtp.DownloadFile(strPathFtp, IO.Path.GetTempPath, fileName)) ' download successfull
                downloadFileWinPath = IO.Path.GetTempPath & fileName
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
        Try
            Dim returnValue As DataRow() = tblDoc.Select("header='" & header & "' and FileName='" & FileName & "' and Extension='" & Extension & "' ", "rev DESC")
            If returnValue.Length >= 1 Then
                LocalRevision = returnValue(0).Item("rev").ToString
            ElseIf returnValue.Length = 0 Then ' no file in DB
                LocalRevision = -1 ' file not find
            End If
        Catch ex As Exception
            LocalRevision = -2 ' error
        End Try
    End Function

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

    Private Sub ButtonClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonClose.Click
        If controlRight("Z") >= 3 And InputBox("Please insert psw for this account : ", "Password Request") = CreAccount.strPassword Then
            FormStart.Show()
            closeform = True
            Me.Close()
        Else
            ComunicationLog("0043")
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

        Try
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
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

    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        e.Cancel = True
        If closeform = True Then e.Cancel = False 'keeps form from closing
    End Sub
End Class