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
            strSintaxCheck = ""
            strRevCheck = ""

            strSintaxCheck = PathSintaxAnalysis()
            'ComunicationLog(strSintaxCheck)

            strRevCheck = RevisionExtract(intLastRev)
            If intLastRev >= 0 Then
                TextBoxLastRevision.Text = Str(intLastRev)
            Else
                TextBoxLastRevision.Text = "Not Found"
            End If

            If strSintaxCheck = "5008" And controlType("E") = 1 Then
                If EnumerateCheck(CreFile.Header) = -1 Then
                    ComunicationLog("1007") ' exist plase carefull
                    EcrControl = True
                ElseIf EnumerateCheck(CreFile.Header) = 2 Then
                    ComunicationLog("1006") ' ecr progression error
                    EcrControl = False
                ElseIf EnumerateCheck(CreFile.Header) = 1 Then
                    ComunicationLog("5071") ' ecr progression ok
                    EcrControl = True
                Else
                    ComunicationLog("0043") ' db error
                    EcrControl = False
                End If
            End If

            'ComunicationLog(strRevCheck)

        End If
    End Sub

    Private Sub ButtonLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoad.Click
        Dim strLoaded As String, tmp As String

        'strRevCheck = RevisionExtract(intLastRev)

        If controlRight(Mid(CreFile.Header, 3, 1)) >= 2 Then
            'Dim returnValue As DataRow()
            'returnValue = tblType.Select("header = '" & CreFile.Header & "'")
            'If returnValue.Length > 0 Then
            If strSintaxCheck = "5008" And strRevCheck = "5029" Then
                If EcrControl Or controlType("E") = 0 Then
                    If intLastRev = -1 Then      ' file not found in DB
                        If CreFile.Rev = 0 Then
                            strLoaded = loadCreFile(False)
                            ComunicationLog(strLoaded)
                        ElseIf controlType("R") = 1 Then
                            If MsgBox("The file is not in the server. Do you want to load it with revision index greater than 0?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                strLoaded = loadCreFile(False)
                                ComunicationLog(strLoaded)
                            Else
                                ComunicationLog("1009") ' File not uploaded
                            End If
                        ElseIf controlType("R") = 0 Then
                            ComunicationLog("1008") ' Revision progression not requested...revision index must be 0!
                        End If
                    ElseIf intLastRev >= 0 Then  ' file found in db
                        If CreFile.Rev = intLastRev + 1 Then
                            strLoaded = loadCreFile(False)
                            ComunicationLog(strLoaded)
                        ElseIf CreFile.Rev = intLastRev Then
                            If MsgBox("The file is already in the server. Do you want to replace it?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                strLoaded = SignExtract(tmp)
                                If tmp = "" And strLoaded = "5069" Or controlType("S") = 0 Then
                                    ReplaceNameFileC2()
                                    strLoaded = loadCreFile(True)
                                    ComunicationLog(strLoaded)
                                Else
                                    ComunicationLog("0044") ' File already signed
                                End If
                            Else
                                ComunicationLog("0002") ' File already present in server
                            End If
                        ElseIf (CreFile.Rev > intLastRev) And controlType("R") = 0 Then

                            ComunicationLog("1010")

                        Else

                            ComunicationLog("0015") ' "Revision progression error!"

                        End If
                    End If
                End If
            End If
            'Else
            '   ComunicationLog("0055") ' this type not exist
            'End If
        Else
            ComunicationLog("0043") ' right not enough
        End If
    End Sub
    ' Load the crefile in the server

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
                loadCreFile = "0003" ' Directory creation error
            Else
                If Val(CreFile.Rev) <> 0 And ComboBoxRevNote.Text = "" Then
                    loadCreFile = "0011" ' Fill in the revision note
                Else
                    strRes = objFtp.ListDirectory(strPathFtp & "/" & Mid(TextBoxDocName.Text, intPos + 1), strList)

                    If strRes <> "5000" Or strRes = "5000" And ReplaceOnly Then

                    Else
                        ListBoxLog.Items.Add("File is present in the server, the system will rewrite it!")
                        ReplaceOnly = True
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
                                myrow.Item("revNote") = CstrRevNoteCreation '  "File creation"
                            ElseIf ComboBoxRevNote.Text <> "" Then
                                myrow.Item("revNote") = ComboBoxRevNote.Text
                            End If

                            tblDoc.Rows.Add(myrow)
                            builder.GetUpdateCommand()
                            AdapterDoc.Update(tblDoc)

                        End If

                        loadCreFile = "5027" ' File uploaded 

                    Else
                        loadCreFile = "0001" ' Upload file error
                    End If

                End If
            End If

        Else
            loadCreFile = "0043" 'You don't have right enough for this operation
        End If

    End Function
    ' Check the sintax of file

    Function PathSintaxAnalysis() As String

        Dim strNomeFile As String
        Dim Header1 As String
        Dim intPos As Integer
        Dim strRev As String
        Dim HeaderCheck As Integer, FileNameCheck As Integer, RevCheck As Integer, ExtCheck As Integer
        Dim BooFileName As Boolean = False
        Dim returnValue As DataRow()

        HeaderCheck = 0
        FileNameCheck = 0
        RevCheck = 0
        ExtCheck = 0

        Try

            If TextBoxDocName.Text <> "" Then

                intPos = InStrRev(TextBoxDocName.Text, "\", -1, CompareMethod.Text)
                If intPos > 0 Then
                    strNomeFile = Mid(TextBoxDocName.Text, intPos + 1)

                    CreFile.Header = UCase(Mid(strNomeFile, 1, 11))
                    Header1 = UCase(Mid(strNomeFile, 1, 12))
                    If Regex.IsMatch(Header1, "^[0-9][0-9][a-zA-Z]_([a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]_){2}$", RegexOptions.IgnoreCase) Then 'Check su sintassi dell'header
                        HeaderCheck = 1
                        returnValue = tblType.Select("header = '" & CreFile.Header & "'")
                        If returnValue.Length = 0 Then ' header not defined
                            HeaderCheck = 2
                        End If
                    End If

                    strRev = Regex.Match(strNomeFile, "(?<=_)\d+(?=.\w+$)").ToString
                    If IsNumeric(strRev) And (Mid(strRev, 1, 1) <> "0" Or strRev = "0") Then   'Check su sintassi della revisione
                        CreFile.Rev = Str(strRev)
                        RevCheck = 1
                    End If

                    If HeaderCheck = 1 Then

                        CreFile.Extension = Regex.Match(strNomeFile, "(?<=.)\w+$").ToString
                        If InStr(1, ";" & FileExtensionAllowed(CreFile.Header) & ";", ";" & CreFile.Extension & ";") > 0 Then 'Check sull'estensione
                            ExtCheck = 1
                        End If

                        If Not Regex.IsMatch(strNomeFile, "__", RegexOptions.IgnoreCase) Then
                            CreFile.FileName = Mid(strNomeFile, 13, InStrRev(strNomeFile, "_", -1, CompareMethod.Text) - 13)
                            Select Case CreFile.Header

                                Case ParameterTable("plant") & "R_PRO_ECR"                     'Ecr 

                                    If CreFile.Rev = 0 Then
                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+$", RegexOptions.IgnoreCase)
                                    Else
                                        ComunicationLog("0034")
                                    End If

                                Case ParameterTable("plant") & "R_PRO_TCR"                     'Ecr 

                                    If CreFile.Rev = 0 Then
                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+$", RegexOptions.IgnoreCase)
                                    Else
                                        ComunicationLog("0034")
                                    End If

                                Case ParameterTable("plant") & "R_PRO_EOR"                     'Ordini EQ

                                    If CreFile.Rev = 0 Then
                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^\d+ - \w+$", RegexOptions.IgnoreCase)
                                    Else
                                        ComunicationLog("0034")
                                    End If

                                Case Else
                                    If controlType("C") = 2 Then ' Filename type "15002320 - pcb  ....."

                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^[0-9]{8} - \w+$", RegexOptions.IgnoreCase) Or Regex.IsMatch(CreFile.FileName, "^[0-9]{11} - \w+$", RegexOptions.IgnoreCase)

                                    ElseIf controlType("C") = 1 Then

                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^[0-9]{8}$", RegexOptions.IgnoreCase) Or Regex.IsMatch(CreFile.FileName, "^[0-9]{11}$", RegexOptions.IgnoreCase)

                                    ElseIf controlType("C") = 0 Then

                                        BooFileName = Regex.IsMatch(CreFile.FileName, "^\w+", RegexOptions.IgnoreCase)

                                    End If

                            End Select

                            FileNameCheck = IIf(BooFileName, 1, 0)

                        End If

                    Else
                        ExtCheck = 2
                        FileNameCheck = 2
                    End If

                    If HeaderCheck = 1 And FileNameCheck = 1 And RevCheck = 1 And ExtCheck = 1 Then

                        TextBoxHeader.Text = CreFile.Header
                        TextBoxExtension.Text = CreFile.Extension
                        TextBoxFileName.Text = CreFile.FileName
                        TextBoxRev.Text = Str(CreFile.Rev)
                        PathSintaxAnalysis = ("5008")  ' Sintax ok
                        ComunicationLog("5008")
                    Else
                        PathSintaxAnalysis = ("0020") ' Sintax Error
                    End If

                    If PathSintaxAnalysis = "0020" Then
                        If HeaderCheck = 0 Then
                            ComunicationLog("1001")  ' Header syntax error
                            'ElseIf HeaderCheck = 1 Then
                            '   ComunicationLog("6001")  ' Header syntax ok
                        ElseIf HeaderCheck = 2 Then
                            ComunicationLog("0055")  ' Header not defined
                        End If

                        If FileNameCheck = 0 Then
                            ComunicationLog("1002")  ' File name syntax error
                            'ElseIf FileNameCheck = 1 Then
                            '   ComunicationLog("6002")  ' File name syntax ok
                            'ElseIf FileNameCheck = 2 Then
                            '   ComunicationLog("1005")  ' Check after header
                        End If

                        If RevCheck = 0 Then
                            ComunicationLog("1003")  ' Revision syntax error
                            'ElseIf RevCheck = 1 Then
                            '   ComunicationLog("6003")  ' Revision syntax ok
                        End If

                        If ExtCheck = 0 Then
                            ComunicationLog("1004")  ' Ext syntax error
                            'ElseIf ExtCheck = 1 Then
                            '   ComunicationLog("6004")  ' Ext syntax ok
                        End If
                    End If
                Else
                    PathSintaxAnalysis = ("0004") ' Path error
                    ComunicationLog("0004")
                End If

            Else
                PathSintaxAnalysis = ("0022") ' Please select a file
                ComunicationLog("0022")
            End If

        Catch ex As Exception
            PathSintaxAnalysis = ("0025") ' Generic exception
            ComunicationLog("0025")
        End Try

    End Function

    ' Find the last revision in the server of the current file
    ' If not exist return ""
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
            If strSintaxCheck = ("5008") Then
                RevisionExtract = ("5029") ' Revision extract ok
                'If controlType("C") = 2 Then
                'returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName like '" & Regex.Match(CreFile.FileName, "^\w+").ToString & " - *' and Extension='" & CreFile.Extension & "' ", "rev DESC")
                'Else
                returnValue = tblDoc.Select("header='" & CreFile.Header & "' and FileName='" & CreFile.FileName & "' and Extension='" & CreFile.Extension & "' ", "rev DESC")
                'End If

                If returnValue.Length >= 1 Then
                    rev = returnValue(0).Item("rev")
                ElseIf returnValue.Length = 0 Then ' No file in DB
                    rev = -1 ' File not found
                End If
            Else
                RevisionExtract = ("0019") ' Syntax error....revision not extracted!
            End If
        Catch ex As Exception
            RevisionExtract = "0013"
            ComunicationLog("0013") ' Generic exception
        End Try

    End Function
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

    ' Find sign
    ' If not exist return ""
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

    Sub ComunicationLog(ByVal ComCode As String)

        Dim rsResult As DataRow()
        rsResult = tblError.Select("code='" & ComCode & "'")
        If rsResult.Length = 0 Then
            ComCode = "0051"
            rsResult = tblError.Select("code='" & ComCode & "'")
        End If

        ListBoxLog.Items.Add(ComCode & " -> " & rsResult(0).Item("en").ToString)

        If Val(ComCode) = 5000 Or Val(ComCode) = 5027 Then
            ListBoxLog.BackColor = Color.LightGreen
        ElseIf Val(ComCode) > 5000 Then
            If ListBoxLog.BackColor = Color.OrangeRed Then
            Else
                ListBoxLog.BackColor = Color.LightGreen
            End If
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