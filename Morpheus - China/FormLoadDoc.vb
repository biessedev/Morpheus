Option Explicit On
Option Compare Text
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class FormLoadDoc

    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc order by rev desc", MySqlconnection)
    Dim AdapterRevNote As New MySqlDataAdapter("SELECT * FROM revNote", MySqlconnection)
    Dim AdapterType As New MySqlDataAdapter("SELECT * FROM doctype", MySqlconnection)

    Dim tblDoc As DataTable, tblRevNote As DataTable, tblType As DataTable
    Dim DsDoc As New DataSet, DsRevNote As New DataSet, DsType As New DataSet
    Dim builder As MySqlCommandBuilder = New MySqlCommandBuilder(AdapterDoc)
    Dim strRuleCheck As String
    Dim strSintaxCheck As String
    Dim strRevCheck As String
    Dim intLastRev As Integer
    Dim EcrControl As Boolean

    Private Sub FormLoadDoc_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()

    End Sub

    Private Sub FormLoadDoc_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            FormStart.Hide()

            AdapterDoc.Fill(DsDoc, "doc")
            tblDoc = DsDoc.Tables("doc")
            AdapterType.Fill(DsType, "doctype")
            tblType = DsType.Tables("docType")
            Me.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try


    End Sub

    Private Sub ButtonBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBrowse.Click

        If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            ComunicationLog("5000")  ' Sistem ready
            TextBoxDocName.Text = OpenFileDialog1.FileName
            FillComboRevNote()
            ListBoxLog.Items.Clear()
            intLastRev = 0
            strRuleCheck = ""
            strSintaxCheck = ""
            strRevCheck = ""
            strSintaxCheck = PathSintaxAnalysis()
            ComunicationLog(strSintaxCheck)
            strRuleCheck = NameFileExstensionHeaderRuleCheck()
            ComunicationLog(strRuleCheck)

            strRevCheck = RevisionExtract(intLastRev)
            If intLastRev >= 0 Then
                TextBoxLastRevision.Text = Str(intLastRev)
            Else
                TextBoxLastRevision.Text = "Not Find"
            End If

            If strSintaxCheck = "5008" And controlType("E") = 1 Then
                If EnumerateCheck(CreFile.Header) = -1 Then
                    ComunicationLog("5069") ' exist plase carefull
                    EcrControl = True
                ElseIf EnumerateCheck(CreFile.Header) = 2 Then
                    ComunicationLog("5065") ' ecr progression error
                    EcrControl = False
                ElseIf EnumerateCheck(CreFile.Header) = 1 Then
                    ComunicationLog("5071") ' ecr progression ok
                    EcrControl = True
                Else
                    ComunicationLog("0043") ' db erro
                    EcrControl = False
                End If
            End If

            ComunicationLog(strRevCheck)

        End If
    End Sub

    Function EnumerateCheck(ByVal typeEcrTcr As String) As Integer
        Dim rsResult As DataRow(), pos As Integer, i As Integer, maxN As Integer = -1
        If controlType("E") = 1 Then ' enumerate the ECR, TCR and EOR for example
            rsResult = tblDoc.Select("header='" & typeEcrTcr & "'")
            For i = 0 To rsResult.Length - 1
                pos = InStr(1, rsResult(i).Item("filename").ToString, "-", CompareMethod.Text)
                If pos > 0 Then
                    If Val(Trim(Mid(rsResult(i).Item("filename").ToString, 1, pos - 1))) > maxN Then
                        maxN = Val(Trim(Mid(rsResult(i).Item("filename").ToString, 1, pos - 1)))
                    End If
                End If
            Next
            pos = InStr(1, CreFile.FileName, "-", CompareMethod.Text)
            Try
                If Val(Trim(Mid(CreFile.FileName, 1, pos - 1))) = maxN + 1 Then
                    EnumerateCheck = +1
                ElseIf Val(Trim(Mid(CreFile.FileName, 1, pos - 1))) <= maxN Then
                    EnumerateCheck = -1
                Else
                    EnumerateCheck = +2
                End If
            Catch ex As Exception
                EnumerateCheck = +2
            End Try

        Else
            EnumerateCheck = -2
        End If
    End Function


    Private Sub ButtonLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoad.Click
        Dim strLoaded As String, tmp As String
        strRevCheck = RevisionExtract(intLastRev)
        If controlRight(Mid(CreFile.Header, 3, 1)) >= 2 Then
            Dim returnValue As DataRow()
            returnValue = tblType.Select("header = '" & CreFile.Header & "'")
            If returnValue.Length > 0 Then
                If strSintaxCheck = "5008" And strRuleCheck = "5026" And strRevCheck = "5029" Then
                    If EcrControl Or controlType("E") = 0 Then
                        If intLastRev = -1 Then      ' file not find in db" Then
                            If 0 = CreFile.Rev Then
                                strLoaded = loadCreFile(False)
                                ComunicationLog(strLoaded)
                            Else
                                If MsgBox("The file there is not in SrvDoc. Want you put it in a revision more of 0?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    strLoaded = loadCreFile(False)
                                    ComunicationLog(strLoaded)
                                Else
                                    ComunicationLog("0015") ' "Revision progression error!"
                                End If
                            End If
                        ElseIf intLastRev >= 0 Then  ' file find in db
                            If CreFile.Rev = intLastRev + 1 Then
                                strLoaded = loadCreFile(False)
                                ComunicationLog(strLoaded)
                            Else
                                If CreFile.Rev = intLastRev Then
                                    If MsgBox("The file is already present in SrvDoc. Want you replace it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                        strLoaded = SignExtract(tmp)
                                        If tmp = "" And strLoaded = "5069" Or controlType("S") = 0 Then
                                            REplaceNameFileC2()
                                            strLoaded = loadCreFile(True)
                                            ComunicationLog(strLoaded)
                                        Else
                                            ComunicationLog("0044") ' File already signed

                                        End If
                                    Else
                                        ComunicationLog("0002") ' File already present in server
                                    End If
                                ElseIf (CreFile.Rev > intLastRev) And controlType("R") = 0 Then

                                    ListBoxLog.Items.Add("Revision not request, but error in progression!")

                                Else

                                    ComunicationLog("0015") ' "Revision progression error!"

                                End If
                            End If
                        End If
                    End If
                End If
                Else
                    ComunicationLog("0055") ' this type not exist
                End If
            Else
                ComunicationLog("0043") ' right not enough
            End If
    End Sub

    ' Load the crefile in the server
    '
    Function loadCreFile(ByVal ReplaceOnly As Boolean) As String

        Dim myrow As DataRow
        Dim intPos As Integer
        Dim objFtp As ftp = New ftp()
        Dim strRes As String
        Dim strPathFtp As String
        Dim strList As String = ""

        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        If controlRight(Mid(CreFile.Header, 3, 1)) >= 2 Then 'editor()
            intPos = InStrRev(TextBoxDocName.Text, "\", -1, CompareMethod.Text)
            strPathFtp = ("/" & Mid(CreFile.Header, 1, 3) & "/" & CreFile.Header)
            strRes = objFtp.CreateDir("/" & Mid(CreFile.Header, 1, 3))
            strRes = objFtp.CreateDir(strPathFtp)
            strRes = objFtp.ListDirectory(strPathFtp, strList)

            If strRes <> "5000" Then
                loadCreFile = "0003" 'Error Create directory"
            Else
                If Val(CreFile.Rev) <> 0 And ComboBoxRevNote.Text = "" Then
                    loadCreFile = "5011" ' please fill the revision note or select a value
                Else
                    strRes = objFtp.ListDirectory(strPathFtp & "/" & Mid(TextBoxDocName.Text, intPos + 1), strList)

                    If strRes <> "5000" Or strRes = "5000" And ReplaceOnly Then
                    Else
                        ListBoxLog.Items.Add("File present in the server, The system rewrite it!")
                    End If
                    strRes = objFtp.UploadFile(strPathFtp & "/", Mid(TextBoxDocName.Text, 1, intPos - 1), Mid(TextBoxDocName.Text, intPos + 1))

                    If strRes = "5000" Then
                        strRes = objFtp.ListDirectory(strPathFtp & "/" & Mid(TextBoxDocName.Text, intPos + 1), strList)
                        If Not ReplaceOnly Then
                            myrow = tblDoc.NewRow
                            myrow.Item("FileName") = CreFile.FileName
                            myrow.Item("header") = CreFile.Header
                            myrow.Item("rev") = CreFile.Rev
                            myrow.Item("Editor") = CreAccount.strUserName & "[" & Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year & "]"
                            If controlType("S") = 0 Then myrow.Item("sign") = "NoSignReq[" & Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year & "]"
                            myrow.Item("Extension") = CreFile.Extension

                            If Val(CreFile.Rev) = 0 Then
                                myrow.Item("revNote") = CstrRevNoteCreation '  "File Creation"
                            ElseIf ComboBoxRevNote.Text <> "" Then
                                myrow.Item("revNote") = ComboBoxRevNote.Text
                            End If

                            tblDoc.Rows.Add(myrow)
                            builder.GetUpdateCommand()
                            AdapterDoc.Update(tblDoc)

                        End If

                        loadCreFile = "5027" ' file loaded 
                    Else
                        loadCreFile = "0001" 'Error happend in the upload file"
                    End If

                End If
                End If
        Else
                loadCreFile = "0043" 'you don't have right enough for this operation
            End If
    End Function

    ' find the last revision in the Server of the current file
    ' if not exist return ""
    Function RevisionExtract(ByRef rev As Integer) As String

        Try
            DsDoc.Clear()

        Catch ex As Exception

        End Try
        Try

            tblDoc.Clear()
        Catch ex As Exception

        End Try

        Try
            AdapterDoc.Fill(DsDoc, "doc")
            tblDoc = DsDoc.Tables("doc")
        Catch ex As Exception

        End Try

        Dim returnValue As DataRow()
        Try
            RevisionExtract = ("5029") ' revision extract ok
            If controlType("C") = 2 Then
                returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName like '" & Regex.Match(CreFile.FileName, "^\w+").ToString & " - *' and Extension='" & CreFile.Extension & "' ", "rev DESC")
            Else
                returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName='" & CreFile.FileName & "' and Extension='" & CreFile.Extension & "' ", "rev DESC")
            End If

            If returnValue.Length >= 1 Then
                rev = returnValue(0).Item("rev")
            ElseIf returnValue.Length = 0 Then ' no file in DB
                rev = -1 ' file not find
            End If
        Catch ex As Exception
            RevisionExtract = ("0013") ' "Error in revision extract
        End Try

    End Function

    ' find sign
    ' if not exist return ""
    Function SignExtract(ByRef sign As String) As String

        Dim returnValue As DataRow()
        Try
            SignExtract = ("5069") ' Sign extract ok
            returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName='" & CreFile.FileName & _
            "' and Extension='" & CreFile.Extension & "' and rev = " & CreFile.Rev, "rev DESC")
            If returnValue.Length >= 1 Then
                sign = returnValue(0).Item("sign")
            ElseIf returnValue.Length = 0 Then ' no file in DB
                sign = "" ' file not found
            End If
        Catch ex As Exception
            SignExtract = ("0041") ' "Generic exception
        End Try

    End Function

    ' check the sintax of path file
    ''
    Function PathSintaxAnalysis() As String

        Dim strNomeFile As String
        Dim intPos As Integer
        Dim strRev As String
        Try

            If TextBoxDocName.Text <> "" Then

                intPos = InStrRev(TextBoxDocName.Text, "\", -1, CompareMethod.Text)
                If intPos > 0 Then
                    strNomeFile = Mid(TextBoxDocName.Text, intPos + 1)
                    CreFile.Header = UCase(Mid(strNomeFile, 1, 11))
                    If Not Regex.IsMatch(strNomeFile, "__", RegexOptions.IgnoreCase) And Regex.IsMatch(CreFile.Header, "^[0-9][0-9][a-zA-Z0-9](_[a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]){2}$", RegexOptions.IgnoreCase) Then
                        CreFile.FileName = Mid(strNomeFile, 13, InStrRev(strNomeFile, "_", -1, CompareMethod.Text) - 13)
                        If (controlType("C") <> 2 Or Len(CreFile.FileName) >= 13 Or InStr(CreFile.FileName, "template", vbTextCompare) > 0) And (Len(Regex.Match(CreFile.FileName, "^[a-zA-Z0-9_]*", RegexOptions.IgnoreCase).ToString) >= 1 Or controlType("E") = 1) And Regex.IsMatch(strNomeFile, "^[0-9][0-9][a-zA-Z0-9]_([a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]_){2}[a-zA-Z0-9 -_]+\.\w\w\w\w*$", RegexOptions.IgnoreCase) Then
                            'If (controlType("C") <> 2 Or Len(CreFile.FileName) >= 13 Or InStr(CreFile.FileName, "template", vbTextCompare) > 0) And (Len(Regex.Match(CreFile.FileName, "^[a-zA-Z0-9_]*", RegexOptions.IgnoreCase).ToString) >= 1 Or controlType("E") = 1) Then 'And Regex.IsMatch(strNomeFile, "^[0-9][0-9][a-zA-Z0-9]_([a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]_){2}[a-zA-Z0-9 -_]+\.\w\w\w\w*$", RegexOptions.IgnoreCase) Then

                            strRev = Regex.Match(strNomeFile, "(?<=_)\d+(?=.\w+$)").ToString
                            If IsNumeric(strRev) And (Mid(strRev, 1, 1) <> "0" Or strRev = "0") Then
                                CreFile.Rev = Str(strRev)
                                CreFile.Extension = Regex.Match(strNomeFile, "(?<=.)\w+$").ToString
                                If CreFile.Extension <> "" Then
                                    TextBoxHeader.Text = CreFile.Header
                                    TextBoxExtension.Text = CreFile.Extension
                                    TextBoxFileName.Text = CreFile.FileName
                                    TextBoxRev.Text = Str(CreFile.Rev)
                                    PathSintaxAnalysis = ("5008")  ' "Sintax OK"
                                Else
                                    PathSintaxAnalysis = ("0018") ' "Extension Sintax Error!"

                                End If
                            Else
                                PathSintaxAnalysis = ("0019") ' "rev Sintax Error!"

                            End If
                        Else
                            PathSintaxAnalysis = ("0020") ' "NomeFile Sintax Error or too short < 8 char!"
                        End If
                    Else
                        PathSintaxAnalysis = ("0021") ' "Header Sintax Error!"
                    End If
                Else
                    PathSintaxAnalysis = ("5008") ' "Sintax Error!"
                End If

            Else
                PathSintaxAnalysis = ("0022") ' "Please select File!"

            End If

        Catch ex As Exception
            PathSintaxAnalysis = ("0025") ' "Sintax Error in path analisys!"
        End Try

    End Function

    ' Check the rule on fileName 
    ' The rule are based on the header
    Function NameFileExstensionHeaderRuleCheck() As String
        Dim BooAux As Boolean = False

        Try
            Select Case CreFile.Header

                Case "65R_PRO_ECR"                     'Ecr 


                    If CreFile.Rev = 0 Then
                        BooAux = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+[a-zA-Z0-9]$", RegexOptions.IgnoreCase)
                    Else
                        ComunicationLog("0034") ' only revision 0 for this type
                    End If



                Case "65R_PRO_TCR"                     'Ecr 

                    If CreFile.Rev = 0 Then
                        BooAux = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+[a-zA-Z0-9]$", RegexOptions.IgnoreCase)
                    Else
                        ComunicationLog("0034") ' only revision 0 for this type
                    End If

                Case "65R_PRO_EOR"                     'Ordini EQ

                    If CreFile.Rev = 0 Then
                        BooAux = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+[a-zA-Z0-9]$", RegexOptions.IgnoreCase)
                    Else
                        ComunicationLog("0034") ' only revision 0 for this type
                    End If


                Case Else
                    BooAux = True

            End Select


            ' propriety naming check


            If controlType("C") = 2 Then ' Filename type "15002320 - pcb  ....."

                BooAux = BooAux And Regex.IsMatch(CreFile.FileName, "^\w+ - \w+[a-zA-Z0-9]$", RegexOptions.IgnoreCase)

            Else
                BooAux = BooAux And Regex.IsMatch(CreFile.FileName, "^\w+", RegexOptions.IgnoreCase)

            End If


            BooAux = BooAux And (InStr(1, ";;" & FileExtensionAllowed(CreFile.Header) & ";", ";" & CreFile.Extension & ";") > 0)


            NameFileExstensionHeaderRuleCheck = IIf(BooAux, "5026", "0024") ' Header file name extesion match ok /error

        Catch ex As Exception
            NameFileExstensionHeaderRuleCheck = "0023" ' Generic error in heder filename extenzsion check
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
            ListBoxLog.BackColor = Color.LightGreen
        ElseIf Val(ComCode) < 5000 Then
            ListBoxLog.BackColor = Color.OrangeRed
        End If

    End Sub

    ' Fill the combo of revision note

    Sub FillComboRevNote()
        Dim row As DataRow
        AdapterRevNote = New MySqlDataAdapter("SELECT * FROM RevNote", MySqlconnection)
        AdapterRevNote.Fill(DsRevNote, "RevNote")
        tblRevNote = DsRevNote.Tables("RevNote")
        ComboBoxRevNote.Items.Clear()
        For Each row In tblRevNote.Rows
            ComboBoxRevNote.Items.Add(row("revnote").ToString)
        Next
        ComboBoxRevNote.Sorted = True
    End Sub

    ' Check the control type of file
    ' If type not find give -1
    Function controlType(ByVal header As String) As Integer
        Dim intpos As Integer
        controlType = -1 ' type not find
        Dim row As DataRow()
        row = tblType.Select("header = '" & CreFile.Header & "'")
        If row.Length > 0 Then
            intpos = InStr(1, row(0).Item("control").ToString, header, CompareMethod.Text)
            If intpos > 0 Then
                controlType = Val(Mid(row(0).Item("Control").ToString, intpos + 1, 1))
            Else
                controlType = 0
            End If
        End If
    End Function

    Function FileExtensionAllowed(ByVal header As String) As String
        FileExtensionAllowed = "" 'extension not find
        Dim row As DataRow()
        row = tblType.Select("header = '" & CreFile.Header & "'")
        If row.Length > 0 Then
            FileExtensionAllowed = row(0).Item("extension").ToString
        End If
    End Function

    Sub ReplaceNameFileC2()

        Dim objFtp As ftp = New ftp()
        Dim strRes As String
        Dim strPathFtp As String
        Dim strList As String = ""

        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd

        strPathFtp = (Mid(CreFile.Header, 1, 3) & "/" & CreFile.Header)
        strRes = objFtp.ListDirectory(strPathFtp, strList)

        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim returnValue As DataRow()
        Try

            If controlType("C") = 2 Then
                returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName like '" & Regex.Match(CreFile.FileName, "^\w+").ToString & "*' and Extension='" & CreFile.Extension & "' ", "rev DESC")

                For Each row In returnValue
                    Try
                        strRes = objFtp.DeleteFile(strPathFtp & "/", row("header").ToString & "_" & row("filename").ToString & "_" & row("rev").ToString & "." & row("extension").ToString)
                        If strRes = "5000" Then
                            sql = "UPDATE `" & DBName & "`.`doc` SET " & _
                            "`sign` = '', `filename` = '" & CreFile.FileName & "' WHERE `doc`.`id` = " & row("id").ToString & " ;"
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                        End If
                    Catch ex As Exception
                        MsgBox("Mysql update query error!")
                    End Try
                Next

            Else

            End If

        Catch ex As Exception
            MsgBox("Replace C2 file name error!")
        End Try

    End Sub

End Class