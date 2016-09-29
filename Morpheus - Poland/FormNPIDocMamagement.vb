'Option Explicit On
'Option Compare Text
'Imports MySql.Data.MySqlClient

'Public Class FormNPIDocMamagement

'    Dim AdapterDocNPI As New MySqlDataAdapter("SELECT * FROM Doc where header like '%R_PRO_ECR'", MySqlconnection)
'    'Dim AdapterDocNPI As New MySqlDataAdapter("SELECT * FROM Doc", MySqlconnection)
'    Dim tblDocNPI As DataTable
'    Dim DsDocNPI As New DataSet

'    Private Sub FormNPIDocMamagement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing


'        FormSamples.Show()
'        FormSamples.Focus()

'    End Sub


'    Private Sub FormNPIDocMamagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        'tblDocNPI.Clear()
'        'DsDocNPI.Clear()
'        AdapterDocNPI.Fill(DsDocNPI, "TableNPIDoc")
'        tblDocNPI = DsDocNPI.Tables("TableNPIDoc")
'        Call Btn_TypeDocFill()

'    End Sub

'    Private Sub Btn_TypeDocFill()

'        Cob_TypeDoc.Items.Clear()
'        Cob_TypeDoc.Items.Add("pdf")
'        Cob_TypeDoc.Items.Add("doc OR docx")
'        Cob_TypeDoc.Items.Add("xls OR xlsx")
'        Cob_TypeDoc.Items.Add("ppt OR pptx")
'        Cob_TypeDoc.Text = ""

'    End Sub

'    Private Sub Cob_TypeDoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cob_TypeDoc.TextChanged


'        Dim i As Integer, result As DataRow()
'        Try

'            Cob_NameDoc.Text = ""


'            Select Case Trim(Cob_TypeDoc.Text)
'                Case ""
'                    result = tblDocNPI.Select()

'                    Cob_NameDoc.Items.Clear()

'                    For i = 0 To result.Length - 1

'                        Cob_NameDoc.Items.Add(result(i).Item("FileName").ToString & "_" & result(i).Item("rev").ToString & "." & result(i).Item("Extension").ToString)

'                    Next

'                Case "pdf"
'                    result = tblDocNPI.Select("Extension = 'pdf'")
'                    Cob_NameDoc.Items.Clear()

'                    For i = 0 To result.Length - 1

'                        Cob_NameDoc.Items.Add(result(i).Item("FileName").ToString & "_" & result(i).Item("rev").ToString & "." & result(i).Item("Extension").ToString)

'                    Next

'                Case "doc OR docx"
'                    result = tblDocNPI.Select("Extension = 'doc' or Extension = 'docx'")
'                    Cob_NameDoc.Items.Clear()

'                    For i = 0 To result.Length - 1

'                        Cob_NameDoc.Items.Add(result(i).Item("FileName").ToString & "_" & result(i).Item("rev").ToString & "." & result(i).Item("Extension").ToString)

'                    Next

'                Case "xls OR xlsx"
'                    result = tblDocNPI.Select("Extension = 'xls' or Extension = 'xlsx'")
'                    Cob_NameDoc.Items.Clear()

'                    For i = 0 To result.Length - 1

'                        Cob_NameDoc.Items.Add(result(i).Item("FileName").ToString & "_" & result(i).Item("rev").ToString & "." & result(i).Item("Extension").ToString)

'                    Next

'                Case "ppt OR pptx"
'                    result = tblDocNPI.Select("Extension = 'ppt' or Extension = 'pptx'")
'                    Cob_NameDoc.Items.Clear()

'                    For i = 0 To result.Length - 1

'                        Cob_NameDoc.Items.Add(result(i).Item("FileName").ToString & "_" & result(i).Item("rev").ToString & "." & result(i).Item("Extension").ToString)

'                    Next

'            End Select

'        Catch ex As Exception

'        End Try


'    End Sub

'    Private Sub Cob_NameDoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cob_NameDoc.TextChanged

'        Dim DR As DataRow()
'        Dim i As Integer
'        Dim m As Integer = InStrRev(Cob_NameDoc.Text, "_")
'        Dim n As Integer = InStrRev(Cob_NameDoc.Text, ".")
'        Dim FileName As String = Mid(Cob_NameDoc.Text, 1, m - 1)
'        Dim Rev As String = Mid(Cob_NameDoc.Text, m + 1, n - m - 1)
'        DR = tblDocNPI.Select("FileName =  '" & FileName & "' And rev =  '" & Rev & "'")

'        With ListViewNPI
'            .Clear()
'            .View = View.Details
'            .FullRowSelect = True
'            .Columns.Add("Header", 200)
'            .Columns.Add("FileName", 200)
'            .Columns.Add("Version", 100)
'            .Columns.Add("Extension", 100)
'            .Columns.Add("Editor", 200)

'        End With

'        For i = 0 To DR.Length - 1

'            ListViewNPI.Items.Add(DR(i).Item("header").ToString)
'            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("FileName").ToString)
'            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("rev").ToString)
'            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("Extension").ToString)
'            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("Editor").ToString)
'        Next


'    End Sub

'    Private Sub Btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Add.Click
'        Dim Sql As String
'        Dim cmd As New MySqlCommand()

'        Me.Hide()

'        FormSamples.Txt_FilePath.Text = Cob_NameDoc.Text

'        Sql = "UPDATE npi_openissue  SET FilePath ='" & FormSamples.Txt_FilePath.Text & "' WHERE ID = '" & FormSamples.Txt_Index.Text & "'"

'        cmd = New MySqlCommand(Sql, MySqlconnection)
'        cmd.ExecuteNonQuery()
'        Call FormSamples.issuefunction(0)
'        MsgBox("File upload successed")

'        FormSamples.Show()
'        FormSamples.Focus()



'    End Sub

'    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
'        Me.Hide()
'        FormSamples.Show()
'        FormSamples.Focus()
'    End Sub

'    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

'    End Sub

'    Private Sub Cob_NameDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_NameDoc.SelectedIndexChanged

'    End Sub

'    Private Sub Cob_TypeDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_TypeDoc.SelectedIndexChanged

'    End Sub

'    Private Sub ListViewNPI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewNPI.SelectedIndexChanged

'    End Sub
'End Class

'Simona
Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient

Public Class FormNPIDocMamagement

    Dim AdapterDocNPI As New MySqlDataAdapter("SELECT * FROM Doc where header like '%_NPI_OPI'", MySqlconnection)
    'Dim AdapterDocNPI As New MySqlDataAdapter("SELECT * FROM Doc", MySqlconnection)
    Dim tblDocNPI As DataTable
    Dim DsDocNPI As New DataSet

    Dim AdapterDocType As New MySqlDataAdapter("SELECT * FROM Doctype where header like '%_NPI_OPI'", MySqlconnection)
    Dim tblDocType As DataTable
    Dim DsDocType As New DataSet

    Dim AdapterNPI As New MySqlDataAdapter("SELECT * FROM npi_openissue", MySqlconnection)
    Dim tblNPI As New DataTable
    Dim DsNPI As New DataSet


    Private Sub FormNPIDocMamagement_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        FormSamples.Show()
        FormSamples.Focus()

    End Sub

    Private Sub FormNPIDocMamagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'tblDocNPI.Clear()
        'DsDocNPI.Clear()
        AdapterDocNPI.Fill(DsDocNPI, "TableNPIDoc")
        tblDocNPI = DsDocNPI.Tables("TableNPIDoc")

        AdapterDocType.Fill(DsDocType, "DocType")
        tblDocType = DsDocType.Tables("DocType")

        Call Btn_TypeDocFill()

    End Sub

    'Private Sub Btn_TypeDocFill()

    '    Cob_TypeDoc.Items.Clear()
    '    Cob_TypeDoc.Items.Add("pdf")
    '    Cob_TypeDoc.Items.Add("doc OR docx")
    '    Cob_TypeDoc.Items.Add("xls OR xlsx")
    '    Cob_TypeDoc.Items.Add("ppt OR pptx")
    '    Cob_TypeDoc.Text = ""

    'End Sub

    Private Sub Btn_TypeDocFill()

        Dim returnValue As DataRow()

        Cob_TypeDoc.Items.Clear()
        Cob_TypeDoc.Text = ""

        returnValue = tblDocType.Select()
        For Each row In returnValue
            Cob_TypeDoc.Items.Add(row("header").ToString)
        Next

        'resultdoc = tblDocNPI.Select("header = '" & Cob_TypeDoc.Text & "'")

        'For i = 0 To resultdoc.Length - 1
        '    Cob_NameDoc.Items.Add(resultdoc(i).Item("FileName").ToString & "_" & resultdoc(i).Item("rev").ToString & "." & resultdoc(i).Item("Extension").ToString)
        'Next

        'Cob_TypeDoc.Items.Add("ppt OR pptx")

    End Sub

    Private Sub Cob_TypeDoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cob_TypeDoc.TextChanged

        Dim i As Integer, resultdoc As DataRow(), returnValue As DataRow()
        Try

            Cob_NameDoc.Items.Clear()

            'returnValue = tblDocType.Select()
            'For Each row In returnValue
            '    Cob_TypeDoc.Items.Add(row("header").ToString)
            'Next

            resultdoc = tblDocNPI.Select("header = '" & Cob_TypeDoc.Text & "'")

            For i = 0 To resultdoc.Length - 1
                Cob_NameDoc.Items.Add(resultdoc(i).Item("FileName").ToString & "_" & resultdoc(i).Item("rev").ToString & "." & resultdoc(i).Item("Extension").ToString)
            Next


            Cob_NameDoc.Text = ""

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Cob_NameDoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cob_NameDoc.TextChanged

        Dim DR As DataRow()
        Dim i As Integer
        Dim m As Integer = InStrRev(Cob_NameDoc.Text, "_")
        Dim n As Integer = InStrRev(Cob_NameDoc.Text, ".")
        Dim FileName As String = Mid(Cob_NameDoc.Text, 1, m - 1)
        Dim Rev As String = Mid(Cob_NameDoc.Text, m + 1, n - m - 1)
        DR = tblDocNPI.Select("FileName =  '" & FileName & "' And rev =  '" & Rev & "'")

        With ListViewNPI
            .Clear()
            .View = View.Details
            .FullRowSelect = True
            .Columns.Add("Header", 200)
            .Columns.Add("FileName", 200)
            .Columns.Add("Version", 100)
            .Columns.Add("Extension", 100)
            .Columns.Add("Editor", 200)

        End With

        For i = 0 To DR.Length - 1

            ListViewNPI.Items.Add(DR(i).Item("header").ToString)
            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("FileName").ToString)
            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("rev").ToString)
            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("Extension").ToString)
            ListViewNPI.Items(0).SubItems.Add(DR(i).Item("Editor").ToString)
        Next

    End Sub

    Private Sub Btn_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Add.Click
        Dim Sql As String
        Dim cmd As New MySqlCommand()

        Me.Hide()

        If controlRight(Mid(Cob_TypeDoc.Text, 3, 1)) >= 2 Then

            FormSamples.Txt_FilePath.Text = Cob_TypeDoc.Text & "_" & Cob_NameDoc.Text

            Sql = "UPDATE npi_openissue  SET FilePath ='" & FormSamples.Txt_FilePath.Text & "' WHERE ID = '" & FormSamples.Txt_Index.Text & "'"

            cmd = New MySqlCommand(Sql, MySqlconnection)
            cmd.ExecuteNonQuery()
            '  Call FormSamples.issuefunction(0)

            MsgBox("Successfully uploaded file")
        Else
            MsgBox("No enough right to load a file")
        End If

        FormSamples.Show()
        FormSamples.Focus()

    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Hide()
        FormSamples.Show()
        FormSamples.Focus()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Cob_NameDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_NameDoc.SelectedIndexChanged

    End Sub

    Private Sub Cob_TypeDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cob_TypeDoc.SelectedIndexChanged

    End Sub

    Private Sub ListViewNPI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewNPI.SelectedIndexChanged

    End Sub
End Class