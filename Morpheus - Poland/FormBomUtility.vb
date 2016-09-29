
Option Explicit On
Option Compare Text

Imports System.IO
Imports System.Data.SqlClient
Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class FormBomUtility
    Dim DsDocComp As New DataSet
    Dim tblDocComp As New DataTable
    Dim AdapterPfp As New MySqlDataAdapter("SELECT * FROM Pfp", MySqlconnection)
    Dim tblPfp As DataTable
    Dim DsPfp As New DataSet

    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet
    Dim ConnectionStringOrcad As String
    Dim SqlconnectionOrcad As New SqlConnection
    Dim info As String
    Dim AdapterSql As SqlDataAdapter

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
    Private Sub ButtonOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpen.Click
        Dim pathExcel As String = ""
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        CollectProcess()

        'open the offer template
        Dim excelApp As New Object
        excelApp = CreateObject("Excel.Application")

        Dim excelWorkbook As Object
        Dim excelSheet As Object
        ComboBoxIndesit.Items.Clear()
        ComboBoxIndesit.Text = ""
        OpenFileDialogExcel.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        OpenFileDialogExcel.ShowDialog()
        LabelExcel.Text = OpenFileDialogExcel.FileName
        Try
            If OpenFileDialogExcel.CheckFileExists Then
                excelWorkbook = excelApp.Workbooks.Open(LabelExcel.Text)
                For Each excelSheet In excelWorkbook.Worksheets
                    ComboBoxIndesit.Items.Add(excelSheet.name)
                Next
                excelApp.visible = True

                excelWorkbook.close()
            End If
        Catch ex As Exception

        End Try


        excelApp.quit()
        KillLastExcel()
    End Sub


    Private Sub ButtonCompact_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCompact.Click



        Dim pathExcel As String = "", reference As String = ""
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim avl(10000, 100) As String, name(100) As String
        Dim coll(10000, 100) As String
        If LabelExcel.Text <> "****" And ComboBoxIndesit.Text <> "" Then

            Dim excelApp As New Object
            excelApp = CreateObject("Excel.Application")
            Dim excelWorkbook As Object, excelSheetNew As Object, endcol As Integer
            Dim excelSheet As Object, i As Integer, j As Integer, idColl As Integer, collRow As Integer
            Dim rowExcelCom As Integer, rowExcel As Integer
            excelWorkbook = excelApp.Workbooks.Open(LabelExcel.Text)
            excelWorkbook.Activate()
            excelSheet = excelWorkbook.Worksheets(ComboBoxIndesit.Text)

            excelSheet.Activate()
            Dim cc As New ColorConverter
            excelApp.Visible = True
            rowExcelCom = -1
            rowExcel = 7
            Dim col As Integer
            ' 1 indesit pn
            ' 2 desc
            ' 3 ref
            ' 4 classe
            ' 5 avl
            While excelSheet.Cells(rowExcel, 2).text <> ""
                If reference <> excelSheet.Cells(rowExcel, 4).text & excelSheet.Cells(rowExcel, 2).text Then rowExcelCom = rowExcelCom + 1
                reference = excelSheet.Cells(rowExcel, 4).text & excelSheet.Cells(rowExcel, 2).text
                avl(rowExcelCom, 1) = excelSheet.Cells(rowExcel, 2).text
                avl(rowExcelCom, 2) = excelSheet.Cells(rowExcel, 1).text
                avl(rowExcelCom, 3) = excelSheet.Cells(rowExcel, 4).text
                avl(rowExcelCom, 4) = excelSheet.Cells(rowExcel, 6).text
                avl(rowExcelCom, 5) = avl(rowExcelCom, 5) & ";" & excelSheet.Cells(rowExcel, 8).text & "[" & excelSheet.Cells(rowExcel, 9).text & "]"
                If Mid(avl(rowExcelCom, 5), 1, 1) = ";" Then avl(rowExcelCom, 5) = Mid(avl(rowExcelCom, 5), 2)
                Application.DoEvents()
                col = 11
                Labelindex.Text = rowExcelCom
                Application.DoEvents()
                While excelSheet.Cells(6, col).text <> ""
                    name(col) = excelSheet.Cells(6, col).text
                    avl(rowExcelCom, col) = excelSheet.Cells(rowExcel, col).text
                    col = col + 1
                End While
                endcol = col - 1
                rowExcel = rowExcel + 1
            End While

            For i = 0 To 100
                For j = 0 To 100
                    coll(i, j) = ""
                Next
            Next


            collRow = -1
            i = 0
            While avl(i, 1) <> ""

                idColl = searchid(avl(i, 1), coll)
                '
                If idColl > 0 Then

                    If coll(collRow, 5) <> avl(i, 5) Then
                        'MsgBox("avl different for code " & avl(i, 1))
                    End If

                Else
                    collRow = collRow + 1
                    idColl = collRow
                End If

                coll(idColl, 1) = avl(i, 1)
                coll(idColl, 2) = avl(i, 2)
                coll(idColl, 3) = Trim(coll(idColl, 3) & "*" & IIf(InStr(coll(idColl, 3), "*" & avl(i, 3) & "*", CompareMethod.Text) > 0, "", avl(i, 3)) & "*")
                coll(idColl, 4) = avl(i, 4)
                coll(idColl, 5) = avl(i, 5)
                col = 11

                While name(col) <> ""
                    coll(idColl, col) = Val(coll(idColl, col)) + IIf(avl(i, col) <> "", 1, 0)
                    coll(idColl, (endcol + col - 11 + 1)) = Trim(coll(idColl, (endcol + col - 11 + 1)) & "*" & IIf(InStr(coll(idColl, (endcol + col - 11 + 1)), "*" & avl(i, col) & "*", CompareMethod.Text) > 0, "", avl(i, col)) & "*")
                    col = col + 1
                End While
                Labelindex.Text = i
                Application.DoEvents()
                Labelindex.Text = rowExcelCom
                i = i + 1
            End While

            Try
                excelWorkbook.Worksheets("Collected").delete()

            Catch ex As Exception

            End Try

            excelSheetNew = excelWorkbook.Worksheets.add()
            excelSheetNew.name = "Collected"


            i = 0
            While coll(i, 1) <> ""
                coll(i, 3) = Replace(coll(i, 3), "**", " ")
                coll(i, 3) = Replace(Replace(Replace(coll(i, 3), "*", " "), "  ", " "), "  ", " ")
                coll(i, 3) = Trim(coll(i, 3))
                i = i + 1
            End While

            i = 0
            While coll(i, 1) <> ""
                j = 1
                While j <= 2 * endcol + 1


                    If j > endcol Then
                        coll(i, j) = Replace(coll(i, j), "**", " ")
                        coll(i, j) = Replace(coll(i, j), "*", " ")
                        coll(i, j) = Trim(coll(i, j))
                    End If

                    excelSheetNew.Cells(i + 2, j) = coll(i, j)
                    excelSheetNew.Cells(i + 2, j).wraptext = True
                    excelSheetNew.Cells(i + 2, j).NumberFormat = "@"

                    j = j + 1
                End While
                i = 1 + i
            End While

            j = 11

            While j <= endcol
                excelSheetNew.Cells(1, j) = name(j)
                excelSheetNew.Cells(1, j).wraptext = True
                excelSheetNew.Cells(1, j).NumberFormat = "@"
                j = j + 1
            End While

            j = endcol + 1
            While j <= 2 * (endcol)
                excelSheetNew.Cells(1, j) = name(j - (endcol - 10))
                excelSheetNew.Cells(1, j).wraptext = True
                excelSheetNew.Cells(1, j).NumberFormat = "@"
                j = j + 1
            End While

            excelSheetNew.Cells(1, 1) = "indesit pn"
            excelSheetNew.Cells(1, 2) = "desc"
            excelSheetNew.Cells(1, 3) = "ref"
            excelSheetNew.Cells(1, 4) = "classe"
            excelSheetNew.Cells(1, 5) = "avl"

            Try
                SaveFileDialog1.FileName = LabelExcel.Text
                SaveFileDialog1.ShowDialog()
                excelWorkbook.SaveAs(SaveFileDialog1.FileName)
                excelWorkbook.Close(True)
                excelApp.Quit()
                Dim generation As Integer = GC.GetGeneration(excelApp)
                GC.Collect(generation)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Me.Focus()
            Me.Show()
            KillLastExcel()

        End If
    End Sub

    Function searchid(ByVal s As String, ByVal avlm(,) As String) As Integer
        Dim i As Integer = 0
        While avlm(i, 1) <> ""
            If avlm(i, 1) = s Then
                searchid = i
                Exit While
            Else
                i = i + 1
            End If

        End While
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

    Function OrcadDoc(ByVal bitronPN As String) As String
        Dim rowShow As DataRow(), rowHC As DataRow()
        ' If "15002423" = bitronPN Then Stop
        rowShow = tblDoc.Select("filename like '" & bitronPN & " - *' or filename like '" & bitronPN & "'", "rev DESC")
        If rowShow.Length > 0 And Mid(bitronPN, 1, 2) <> "15" Then
            OrcadDoc = "SRV_DOC - " & rowShow(0)("header").ToString & "_" & rowShow(0)("filename").ToString & "_" & rowShow(0)("rev").ToString & "." & rowShow(0)("extension").ToString
        Else
            rowHC = tblDocComp.Select("codice_bitron = '" & bitronPN & "'", "valido")
            If rowHC.Length = 1 Then
                OrcadDoc = "HC-" & rowHC(0)("cod_comp").ToString & " - " & rowHC(0)("valido").ToString
            ElseIf rowHC.Length > 1 Then

                rowHC = tblDocComp.Select("codice_bitron = '" & bitronPN & "' and valido = 'valido'", "valido")
                If rowHC.Length = 1 Then
                    OrcadDoc = "HC-" & rowHC(0)("cod_comp").ToString & " - " & rowHC(0)("valido").ToString
                Else
                    MsgBox("HC with two valid sheet! " & rowHC(0)("codice_bitron").ToString)
                    OrcadDoc = "ERROR"
                End If
            Else
                OrcadDoc = "NO"
            End If

        End If

    End Function

    Private Sub ButtonMissingPf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMissingPf.Click
        Dim sql As String
        Dim i As Integer = 0
        Dim commandMySql As New MySqlCommand
        Dim OrcadDBAds = ParameterTable("OrcadDBAdr")
        Dim OrcadDBName = ParameterTable("OrcadDBName")
        Dim OrcadDBUserName = ParameterTable("OrcadDBUser")
        Dim OrcadDBPwd = ParameterTable("OrcadDBPwd")

            Try
            OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
            Catch ex As Exception
                CloseConnectionSqlOrcad()
            OpenConnectionSqlOrcad(OrcadDBAds, OrcadDBName, OrcadDBUserName, OrcadDBPwd)
            End Try

            ButtonMissingPf.Text = "load orcad data"
            Application.DoEvents()
            Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') or (valido = 'in_attesa_convalida') ", SqlconnectionOrcad)
            Try
                tblDocComp.Clear()
                DsDocComp.Clear()
            Catch ex As Exception

            End Try

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

            AdapterPfp.SelectCommand = New MySqlCommand("SELECT * FROM missing_pf;", MySqlconnection)
            Try
                DsPfp.Clear()
                tblPfp.Clear()
            Catch ex As Exception

            End Try

            AdapterPfp.Fill(DsPfp, "missing_pf")
            tblPfp = DsPfp.Tables("missing_pf")


            For Each row In tblPfp.Rows
                sql = "UPDATE `missing_pf` SET `doc`='" & OrcadDoc(row("bitron_PN").ToString) & "' WHERE `bitron_pn`='" & row("bitron_PN").ToString & "'"
                Try
                    commandMySql = New MySqlCommand(sql, MySqlconnection)
                    commandMySql.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox("Error in DB update/Insert material request")
                End Try
                ButtonMissingPf.Text = "Elaboration of : " & row("bitron_PN").ToString
                Application.DoEvents()
            Next


            ButtonMissingPf.Text = "Missing Pf update Doc"

    End Sub



    Private Sub ButtonCompactElux_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCompactElux.Click





        Dim pathExcel As String = "", reference As String = ""
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim avl(10000, 100) As String, name(100) As String
        Dim coll(10000, 100) As String
        If LabelExcel.Text <> "****" And ComboBoxIndesit.Text <> "" Then

            Dim excelApp As New Object
            excelApp = CreateObject("Excel.Application")
            Dim excelWorkbook As Object, excelSheetNew As Object, endcol As Integer
            Dim excelSheet As Object, i As Integer, j As Integer, idColl As Integer, collRow As Integer
            Dim rowExcelCom As Integer, rowExcel As Integer
            excelWorkbook = excelApp.Workbooks.Open(LabelExcel.Text)
            excelWorkbook.Activate()
            excelSheet = excelWorkbook.Worksheets(ComboBoxIndesit.Text)

            excelSheet.Activate()
            Dim cc As New ColorConverter
            excelApp.Visible = True
            rowExcelCom = -1
            rowExcel = 17
            Dim col As Integer
            ' 1 elux pn
            ' 2 desc
            ' 3 ref
            ' 4 classe
            ' 5 avl
            While excelSheet.Cells(rowExcel, 2).text <> ""
                rowExcelCom = rowExcelCom + 1
                avl(rowExcelCom, 1) = IIf(excelSheet.Cells(rowExcel, 7).text <> "", excelSheet.Cells(rowExcel, 7).text, excelSheet.Cells(rowExcel, 3).text)
                avl(rowExcelCom, 2) = excelSheet.Cells(rowExcel, 3).text
                avl(rowExcelCom, 3) = excelSheet.Cells(rowExcel, 2).text
                avl(rowExcelCom, 4) = excelSheet.Cells(rowExcel, 8).text
                avl(rowExcelCom, 5) = EluxAvl(excelSheet.Cells(rowExcel, 6).text, excelSheet.Cells(rowExcel, 5).text)


                Application.DoEvents()
                col = 13
                Labelindex.Text = rowExcelCom
                Application.DoEvents()
                While excelSheet.Cells(15, col).text <> ""
                    name(col) = excelSheet.Cells(15, col).text
                    avl(rowExcelCom, col) = IIf(excelSheet.Cells(rowExcel, col).text = "x", avl(rowExcelCom, 3), "")
                    col = col + 1
                End While
                endcol = col - 1
                rowExcel = rowExcel + 1
            End While

            For i = 0 To 100
                For j = 0 To 100
                    coll(i, j) = ""
                Next
            Next


            collRow = -1
            i = 0
            While avl(i, 1) <> ""

                idColl = searchid(avl(i, 1), coll)
                '
                If idColl > 0 Then

                    If coll(collRow, 5) <> avl(i, 5) Then
                        'MsgBox("avl different for code " & avl(i, 1))
                    End If

                Else
                    collRow = collRow + 1
                    idColl = collRow
                End If

                coll(idColl, 1) = avl(i, 1)
                coll(idColl, 2) = avl(i, 2)
                coll(idColl, 3) = Trim(coll(idColl, 3) & "*" & IIf(InStr(coll(idColl, 3), "*" & avl(i, 3) & "*", CompareMethod.Text) > 0, "", avl(i, 3)) & "*")
                coll(idColl, 4) = avl(i, 4)
                coll(idColl, 5) = avl(i, 5)
                col = 13

                While name(col) <> ""
                    coll(idColl, col) = Val(coll(idColl, col)) + IIf(avl(i, col) <> "", 1, 0)
                    coll(idColl, (endcol + col - 11 + 1)) = Trim(coll(idColl, (endcol + col - 11 + 1)) & "*" & IIf(InStr(coll(idColl, (endcol + col - 11 + 1)), "*" & avl(i, col) & "*", CompareMethod.Text) > 0, "", avl(i, col)) & "*")
                    col = col + 1
                End While
                Labelindex.Text = i
                Application.DoEvents()
                Labelindex.Text = rowExcelCom
                i = i + 1
            End While

            Try
                excelWorkbook.Worksheets("Collected").delete()

            Catch ex As Exception

            End Try


            excelSheetNew = excelWorkbook.Worksheets.add()
            excelSheetNew.name = "Collected"


            i = 1
            While coll(i, 1) <> ""
                coll(i, 3) = Replace(coll(i, 3), "**", " ")
                coll(i, 3) = Replace(Replace(Replace(coll(i, 3), "*", " "), "  ", " "), "  ", " ")
                coll(i, 3) = Trim(coll(i, 3))
                i = i + 1
            End While

            i = 0
            While coll(i, 1) <> ""
                j = 1
                While j <= 2 * endcol + 1


                    If j > endcol Then
                        coll(i, j) = Replace(coll(i, j), "**", " ")
                        coll(i, j) = Replace(coll(i, j), "*", " ")
                        coll(i, j) = Trim(coll(i, j))
                    End If

                    excelSheetNew.Cells(i + 2, j) = coll(i, j)
                    excelSheetNew.Cells(i + 2, j).wraptext = True
                    excelSheetNew.Cells(i + 2, j).NumberFormat = "@"

                    j = j + 1
                End While
                i = 1 + i
            End While

            j = 11

            While j <= endcol
                excelSheetNew.Cells(1, j) = name(j)
                excelSheetNew.Cells(1, j).wraptext = True
                excelSheetNew.Cells(1, j).NumberFormat = "@"
                j = j + 1
            End While

            j = endcol + 1
            While j <= 2 * (endcol)
                excelSheetNew.Cells(1, j) = name(j - (endcol - 10))
                excelSheetNew.Cells(1, j).wraptext = True
                excelSheetNew.Cells(1, j).NumberFormat = "@"
                j = j + 1
            End While

            excelSheetNew.Cells(1, 1) = "elux pn"
            excelSheetNew.Cells(1, 2) = "desc"
            excelSheetNew.Cells(1, 3) = "ref"
            excelSheetNew.Cells(1, 4) = "classe"
            excelSheetNew.Cells(1, 5) = "avl"

            Try
                SaveFileDialog1.FileName = LabelExcel.Text
                SaveFileDialog1.ShowDialog()
                excelWorkbook.SaveAs(SaveFileDialog1.FileName)
                excelWorkbook.Close(True)
                excelApp.Quit()
                Dim generation As Integer = GC.GetGeneration(excelApp)
                GC.Collect(generation)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            Me.Focus()
            Me.Show()
            KillLastExcel()

        End If




    End Sub


    Private Function EluxAvl(ByVal brand As String, ByVal oc As String) As String

        Dim avlElux(100, 2) As String
        Dim i As Integer = 1, j As Integer = 1, k As Integer = 1

        brand = brand & Chr(10)
        oc = oc & Chr(10)
        While j > 0
            j = InStr(j + 1, brand, Chr(10), CompareMethod.Text)
            If j > 0 Then
                avlElux(k, 1) = Trim(Mid(brand, i, j - i))
                k = k + 1
            End If
            i = j + 1
        End While

        i = 1
        k = 1
        j = 1
        While j > 0
            j = InStr(j + 1, oc, Chr(10), CompareMethod.Text)
            If j > 0 Then
                avlElux(k, 2) = Trim(Mid(oc, i, j - i))
                k = k + 1
            End If
            i = j + 1
        End While

        k = 1
        While avlElux(k, 1) <> ""
            EluxAvl = avlElux(k, 1) & "[" & avlElux(k, 2) & "]" & ";" & EluxAvl
            k = k + 1
        End While

    End Function

    Private Sub FormBomUtility_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class