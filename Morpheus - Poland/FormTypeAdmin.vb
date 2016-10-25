Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class FormTypeAdmin
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim AdapterType As New MySqlDataAdapter("SELECT * FROM doctype", MySqlconnection)
    Dim DsType As New DataSet
    Dim tblDocType As DataTable, tblDoc As DataTable
    Dim DsDoc As New DataSet
    Dim builder As MySqlCommandBuilder = New MySqlCommandBuilder(AdapterType)

    Private Sub FormDownload_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()
        tblDocType.Dispose()
        DsType.Dispose()
        AdapterType.Dispose()
    End Sub

    Private Sub FormTypeAdmin_load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FormStart.Hide()
        AdapterType.Fill(DsType, "doctype")
        tblDocType = DsType.Tables("doctype")
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")
        FillComboFirstType()
        TextBoxPropriety.Text = "S?R?P?Y?C?"
    End Sub

    Private Sub ComboBoxFirstType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxFirstType.TextChanged
        Dim strOld = ""

        ComboBoxSecondType.Items.Clear()

        Dim returnValue As DataRow() = tblDocType.Select("FirstType='" & ComboBoxFirstType.Text & "'", "SecondType DESC")
        For Each row In returnValue
            If StrComp(Mid(strOld, 1, 3), Mid(row("SecondType").ToString, 1, 3)) <> 0 Then
                strOld = row("SecondType").ToString
                ComboBoxSecondType.Items.Add(row("SecondType"))
            End If
        Next
        ComboBoxSecondType.Sorted = True
        ComboBoxSecondType.Text = ""
        ComboBoxThirdType.Text = ""
        TextBoxPropriety.Text = "S?R?P?Y?C?"
        TextBoxExtension.Text = ""

    End Sub

    Private Sub ComboBoxSecondType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxSecondType.TextChanged
        Dim strOld = ""
        ComboBoxThirdType.Items.Clear()
        Dim returnValue As DataRow() = tblDocType.Select("FirstType='" & ComboBoxFirstType.Text & "' and SecondType='" & ComboBoxSecondType.Text & "'", "SecondType DESC")
        For Each row In returnValue
            If StrComp(Mid(strOld, 1, 3), Mid(row("ThirdType").ToString, 1, 3)) <> 0 Then
                strOld = row("ThirdType").ToString
                ComboBoxThirdType.Items.Add(row("ThirdType"))
            End If
        Next
        ComboBoxThirdType.Sorted = True
        ComboBoxThirdType.Text = ""
        TextBoxPropriety.Text = "S?R?P?Y?C?"
        TextBoxExtension.Text = ""

    End Sub
    Private Sub ComboBoxThirdType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxThirdType.TextChanged

        TextBoxPropriety.Text = "S?R?P?Y?C?"
        TextBoxExtension.Text = ""

    End Sub
    ' Function to create new type

    Private Sub ButtonTypeAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTypeAdd.Click
        Dim AllOk = False

        DisableControl()
        ComboBoxFirstType.Text = Trim(ComboBoxFirstType.Text)
        ComboBoxSecondType.Text = Trim(ComboBoxSecondType.Text)
        ComboBoxThirdType.Text = Trim(ComboBoxThirdType.Text)

        If CheckFieldType(ComboBoxFirstType.Text) And ComboBoxFirstType.Text <> "" Then
            If CheckFieldType(ComboBoxSecondType.Text) And CheckFieldType(ComboBoxThirdType.Text) Then
                If Len(ComboBoxThirdType.Text) > 1 Then
                    If Len(ComboBoxSecondType.Text) > 1 Then
                        If Regex.IsMatch(TextBoxPropriety.Text, "^S[01]R[01]P[01]Y[0-9A-Z]C[012]$") Then
                            AllOk = True
                        End If
                    End If
                End If
            End If
        End If

        If AllOk Then
            If controlRight("T") >= 3 And controlRight(Mid(ComboBoxFirstType.Text, 3, 1)) >= 2 Then
                Dim returnValue As DataRow() = tblDocType.Select("header='" & HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text) & "'")
                If returnValue.Length = 1 Then
                    ComunicationLog("0039") '("This type is already present in the database. No record added!")
                ElseIf returnValue.Length > 1 Then
                    ComunicationLog("0040") '("Error of data, more fild present in the Database for this type. No record Added!")
                Else

                    If TextBoxExtension.Text <> "" Then

                        Dim myrow As DataRow = tblDocType.NewRow
                        myrow.Item("FirstType") = Trim(cap7(ComboBoxFirstType.Text))
                        myrow.Item("SecondType") = Trim(cap7(ComboBoxSecondType.Text))
                        myrow.Item("ThirdType") = Trim(cap7(ComboBoxThirdType.Text))
                        myrow.Item("header") = UCase(Trim(HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text)))
                        myrow.Item("Control") = TextBoxPropriety.Text
                        myrow.Item("extension") = TextBoxExtension.Text

                        tblDocType.Rows.Add(myrow)
                        builder.GetUpdateCommand()
                        AdapterType.Update(tblDocType)
                        ComunicationLog("5041") '("Record inserted in database")
                        resetCont()
                        FillComboFirstType()
                    Else
                        ComunicationLog("0009") '("Extension is missing!")
                    End If

                End If
            Else
                ComunicationLog("0043") 'no enough right
            End If
        Else
            ComunicationLog("0038") 'Sintax error 
        End If
        EnableControl()

    End Sub

    Private Sub ButtonRefresh_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonRefresh.Click
        UpdatePropriety()
    End Sub

    Private Sub ButtonDelete_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDelete.Click
        If controlRight("T") >= 3 And controlRight(Mid(ComboBoxFirstType.Text, 3, 1)) >= 2 Then
            If vbYes = MsgBox(StrSettingRead("0035"), MsgBoxStyle.YesNo) Then
                Dim returnValue As DataRow() = tblDoc.Select("header='" & HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text) & "'")
                If returnValue.Length > 0 Then
                    MsgBox(StrSettingRead("0036"), MsgBoxStyle.Critical)
                Else
                    returnValue = tblDocType.Select("header='" & HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text) & "'")
                    If returnValue.Length > 0 Then

                        Dim sql As String = String.Format("DELETE FROM `{0}`.`doctype` WHERE `doctype`.`header` ='{1}'", DBName, HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text))
                        Dim cmd As MySqlCommand = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        ComunicationLog("5034") 'Record deleted from database
                        resetCont()
                    Else
                        MsgBox(StrSettingRead("0037"))
                    End If
                End If
            End If
        Else
            ComunicationLog("0043") 'no enough right
        End If
    End Sub

    ' Fill the first type combo box

    Sub FillComboFirstType()
        ComboBoxFirstType.Items.Clear()
        Dim strOld = ""
        Dim row As DataRow
        Dim result As DataRow() = tblDocType.Select("FirstType like '*'", "firstType")
        For Each row In result
            Dim strNew As Object = (row("FirstType").ToString)
            If StrComp(Mid(strOld, 1, 3), Mid(strNew, 1, 3)) <> 0 Then
                strOld = strNew
                ComboBoxFirstType.Items.Add(strNew)
            End If
        Next
        ComboBoxFirstType.Sorted = True
        ComboBoxSecondType.Text = ""
        ComboBoxThirdType.Text = ""

    End Sub

    ' calculation of the three header

    Function HeaderCalc(ByVal cf As String, ByVal cs As String, ByVal ct As String) As String
        HeaderCalc = Mid(cf, 1, 3)
        If cs <> "-" Then
            HeaderCalc = HeaderCalc & "_" & Mid(cs, 1, 3)
            If cs <> "-" Then
                HeaderCalc = HeaderCalc & "_" & Mid(ct, 1, 3)
            Else
                HeaderCalc = HeaderCalc & "_-"
            End If
        Else
            HeaderCalc = HeaderCalc & "_-_-"
        End If
    End Function

    Function CheckFieldType(ByVal s As String) As Boolean
        Dim Boofilled As Boolean

        If s <> "" Then Boofilled = True
        ' NoNumeric(s) ' can use also numeric
        Dim BooTratSpace As Boolean = TratPositionSpace(s)
        CheckFieldType = BooTratSpace And BooTratSpace And Boofilled

    End Function

    ' check if all letters isnt numeric

    Function NoNumeric(ByVal s As String) As Boolean
        Dim i As Integer
        NoNumeric = True
        For i = 1 To Len(s)
            If IsNumeric(Mid(s, i, 1)) = True Then NoNumeric = False
        Next
    End Function

    ' Check header position space

    Function TratPositionSpace(ByVal s As String) As Boolean
        TratPositionSpace = False
        If Len(s) > 1 Then
            If InStr(s, " - ", CompareMethod.Text) = 4 Then TratPositionSpace = True
        Else
            If InStr(s, "-", CompareMethod.Text) = 1 Then TratPositionSpace = True
        End If
    End Function

    'Enable all control

    Sub EnableControl()
        Dim ct As Control
        For Each ct In Me.Controls
            ct.Enabled = True
        Next
    End Sub

    'Disable all control

    Sub DisableControl()
        Dim ct As Control
        For Each ct In Me.Controls
            ct.Enabled = False
        Next
    End Sub

    ' Find the control properties and extension linked with a specific document type

    Sub UpdatePropriety()
        tblDocType.Clear()
        DsType.Clear()
        AdapterType.Fill(DsType, "doctype")
        tblDocType = DsType.Tables("doctype")

        Dim returnValue As DataRow() = tblDocType.Select("header='" & HeaderCalc(ComboBoxFirstType.Text, ComboBoxSecondType.Text, ComboBoxThirdType.Text) & "'")
        If returnValue.Length <= 1 Then
            returnValue = tblDocType.Select("FirstType='" & ComboBoxFirstType.Text & "' and SecondType='" & ComboBoxSecondType.Text & "' and ThirdType='" & ComboBoxThirdType.Text & "'", "SecondType DESC")
            If returnValue.Length <= 1 Then
                For Each row In returnValue

                    TextBoxPropriety.Text = row("control").ToString
                    TextBoxExtension.Text = row("extension").ToString
                    ComunicationLog("5030") ' Record found in database
                Next
                If returnValue.Length = 0 Then ComunicationLog("0027") ' Record NOT found in database
            Else
                ComunicationLog("0031") ' There are more records for the same type in the database. Please contact the administrator!
            End If
        Else
            ComunicationLog("0032") ' There are more types of document with same header!
        End If
    End Sub

    ' comunication function

    Sub ComunicationLog(ByVal ComCode As String)
        Dim rsResult As DataRow() = tblError.Select("code='" & ComCode & "'")
        ListBoxLog.Items.Add(ComCode & " -> " & rsResult(0).Item("en").ToString)
        'ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1

        If Val(ComCode) >= 5000 Then
            ListBoxLog.BackColor = Color.LightGreen
        ElseIf Val(ComCode) < 5000 Then
            ListBoxLog.BackColor = Color.OrangeRed
        End If
    End Sub

    Sub resetCont()
        ComboBoxFirstType.Text = ""
        ComboBoxSecondType.Text = ""
        ComboBoxThirdType.Text = ""
        TextBoxPropriety.Text = "S?R?P?Y?C?"
        TextBoxExtension.Text = ""

    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        MsgBox("First Type: 3 letters - Description" & vbCrLf & _
                "Second Type: 3 letters - Description" & vbCrLf & _
                "Third Type: 3 letters - Description" & " (Example: LAB - Label Specification)" & vbCrLf & _
                vbCrLf & _
                "Please fill in the document properties: " & vbCrLf & _
                "S{X} X=0 Sign not requested, X=1 Sign requested, " & vbCrLf & _
                "R{X} X=0 Revision not requested, X=1 Revision requested, " & vbCrLf & _
                "P{X} X=0 No product file, X=1 Product file, " & vbCrLf & _
                "Y{X} X=0 File not required, X=1 File always required, X is a letter defining a process" & vbCrLf & _
                "C{X} X=0 Free naming (General_Description), X=1 BitronCode, X=2 BitronCode - Description " & vbCrLf & _
                "Example: S1R1P1Y1C0" & vbCrLf & _
                vbCrLf & _
                "Please fill in the file extension whith possible extensions of document:" & vbCrLf & _
                "Example:pdf;docx;doc;xls;xlsx;zip;" & vbCrLf & _
                "Each file extension is followed by ';' and no SPACE are allowed between them. " & vbCrLf)
    End Sub

End Class