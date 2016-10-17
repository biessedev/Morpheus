Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Threading


Public Class FormDownload
    Public loadDoc As Boolean
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim AdapterDocType As New MySqlDataAdapter("SELECT * FROM Doctype", MySqlconnection)
    Dim AdapterDocProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim AdapterCust As New MySqlDataAdapter("SELECT * FROM customer", MySqlconnection)
    Dim AdapterBom As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
    Dim tblDoc As DataTable, tblDocType As DataTable, tblDocProd As DataTable, tblDocCust As DataTable, tlbDocGru As New DataTable, tlbDocGrutype As New DataTable
    Dim DsDoc As New DataSet, DsDocType As New DataSet, DsDocProd As New DataSet, DsDocCust As New DataSet, DsDocGru As New DataSet, DsDocGrutype As New DataSet
    Dim DsDocComp As New DataSet, DsBom As New DataSet
    Dim tblDocComp As New DataTable, tblBom As DataTable
    Dim BooAvvio As Boolean = True, stopEvent As Boolean
    Dim trd As Thread
    Dim Autoupdate As Boolean
    Public trdFinish As Boolean = False

    Private Sub FormDownload_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()
        If trdFinish Then CloseConnectionMySqlGru()
        CloseConnectionMySqlGru()
    End Sub
    Private Sub FormDownload_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Autoupdate = True
        trd = New Thread(AddressOf ThreadTask)
        trd.IsBackground = True
        trd.Start()

        Me.Focus()
        RadioButtonGeneralSearch.Checked = True
        RadioButtonProductSearch.Checked = False
        FillComboSign()
        FillComboRevision()

        Label1LastBomUpdate.Text = "Last bom update " & ParameterTable("LAST_SIGIP_BOM_UPDATE")
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        AdapterDocType.Fill(DsDocType, "DocType")
        tblDocType = DsDocType.Tables("DocType")

        AdapterDocProd.Fill(DsDocProd, "Product")
        tblDocProd = DsDocProd.Tables("Product")

        AdapterCust.Fill(DsDocCust, "customer")
        tblDocCust = DsDocCust.Tables("customer")

        FillComboEcrNull()
        FillComboEcrPending()

        FillComboFirstType()
        BooAvvio = False

        FillComboCust()
        FillComboProd()

        ComboBoxStatus.Items.Add("ALL - STATUS")
        ComboBoxStatus.Items.Add("SOP_SAMPLE")
        ComboBoxStatus.Items.Add("OBSOLETE")
        ComboBoxStatus.Items.Add("R&D_APPROVED")
        ComboBoxStatus.Items.Add("LOGISTIC_APPROVED")
        ComboBoxStatus.Items.Add("CUSTOMER_APPROVED")
        ComboBoxStatus.Items.Add("PURCHASING_APPROVED")
        ComboBoxStatus.Items.Add("ENGINEERING_APPROVED")
        ComboBoxStatus.Items.Add("PROCESS_ENG_APPROVED")
        'ComboBoxStatus.Items.Add("IDN_ENG_APPROVED")
        'ComboBoxStatus.Items.Add("IND_ENG_APPROVED")
        ComboBoxStatus.Items.Add("TIME&MOTION_APPROVED")
        ComboBoxStatus.Items.Add("PRODUCTION_APPROVED")
        ComboBoxStatus.Items.Add("FINANCIAL_APPROVED")
        ComboBoxStatus.Items.Add("MPA_APPROVED")


        If CheckComp.Checked = True Then

            TextBoxCompPn.Enabled = True
        Else
            TextBoxCompPn.Enabled = False
        End If

        Me.Focus()
        ListView1.Columns.Clear()
        Autoupdate = False
    End Sub
    Private Sub ComboBoxFirstType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxFirstType.TextChanged
        Dim strOld As String = ""
        ComboBoxSecondType.Items.Clear()
        Dim returnValue As DataRow()
        returnValue = tblDocType.Select("FirstType='" & ComboBoxFirstType.Text & "'", "SecondType DESC")
        For Each row In returnValue
            If StrComp(strOld, row("SecondType").ToString) <> 0 Then
                strOld = row("SecondType").ToString
                ComboBoxSecondType.Items.Add(row("SecondType"))
            End If
        Next
        ComboBoxSecondType.Items.Add("")
        ComboBoxSecondType.Sorted = True
        ComboBoxSecondType.Text = ""
        ComboBoxThirdType.Text = ""
    End Sub
    Private Sub ComboBoxSecondType_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxSecondType.TextChanged
        Dim returnValue As DataRow()
        ComboBoxThirdType.Items.Clear()
        Dim strOld As String = ""
        returnValue = tblDocType.Select("FirstType='" & ComboBoxFirstType.Text & "' and SecondType='" & ComboBoxSecondType.Text & "'", "SecondType DESC")
        For Each row In returnValue
            If StrComp(strOld, row("ThirdType").ToString) <> 0 Then
                strOld = row("ThirdType").ToString
                ComboBoxThirdType.Items.Add(row("ThirdType"))
            End If
        Next
        ComboBoxThirdType.Items.Add("")
        ComboBoxThirdType.Sorted = True
        ComboBoxThirdType.Text = ""
    End Sub

    Private Sub ButtonQuery_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonQuery.Click, ComboBoxSign.TextChanged
        Dim Inconsistent As Boolean = False
        Dim IndexNo As Integer = 0
        If Autoupdate = False Then
            Dim I As Integer, J As Integer, oldBitronPn As String
            Dim RowSearch As DataRow(), RowSearchGru As DataRow(), rowType As DataRow(), strPcbCode As String = "", strPiastraCode As String = "", SQL As String = ""
            Dim mech As String, strSoftwareCode As String = "", strLabelCode As String = "", resultProdList As DataRow(), LastRowList As Integer
            Dim dima As Boolean, sw As Boolean, listLengh As Long

            WriteFile("Info " & Now.ToString, False)
            stopEvent = False
            ListBoxLog.Items.Clear()
            ListView1.Clear()
            ListBoxLog.Items.Add("Query started.....")
            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1

            AdapterBom = New MySqlDataAdapter("SELECT * FROM sigip ", MySqlconnection)
            AdapterBom.Fill(DsBom, "sigip")
            tblBom = DsBom.Tables("sigip")
            tblBom.Clear()
            DsBom.Clear()
            If Not BooAvvio Then

                AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
                DsDoc.Clear()
                tblDoc.Clear()
                AdapterDoc.Fill(DsDoc, "doc")
                tblDoc = DsDoc.Tables("doc")

                If RadioButtonGeneralSearch.Checked Then
                    Try

                    SQL = "(ecrNull like '*" & ComboBoxEcrNull.Text & "*') and  (ecrpending like '*" & ComboBoxEcrPending.Text & "*') and  (filename like '*" & TextBoxfileName.Text & "*') and ( header like '" & IIf(Mid(ComboBoxFirstType.Text, 1, 3) = "", "*", Mid(ComboBoxFirstType.Text, 1, 3) & "_") _
                                      & IIf(Mid(ComboBoxSecondType.Text, 1, 3) = "", "*", Mid(ComboBoxSecondType.Text, 1, 3) & "_") & IIf(Mid(ComboBoxThirdType.Text, 1, 3) = "", "*", Mid(ComboBoxThirdType.Text, 1, 3)) & "')"
                    RowSearch = tblDoc.Select(SQL, "id")

                    FillListView(RowSearch)

                    Catch ex As Exception
                        MsgBox("Error in file name! please change")
                    End Try


                Else
                    Dim ProdControl As String
                    ListView1.Clear()
                    If ComboBoxProd.Text <> "" Then

                        resultProdList = tblDocProd.Select(IIf(CheckBoxObsolete.Checked = True, "(status like '*') ", " (not status = 'OBSOLETE') ") & _
                        " AND (status " & IIf(ComboBoxStatus.Text = "ALL - STATUS", "like '*')", "like '" & ComboBoxStatus.Text & "')") & "  and bitronpn LIKE '" & IIf(Mid(ComboBoxProd.Text, 1, 3) = "ALL", "*", Trim(Mid(ComboBoxProd.Text, 1, InStr(1, ComboBoxProd.Text, "-", CompareMethod.Text) - 1))) & _
                                        "' and customer like '" & IIf(ComboBoxCustomer.Text = "ALL - CUSTOMER", "*", ComboBoxCustomer.Text) & "'")
                        For Each rowPrdList In resultProdList

                            If CheckGru.Checked And Not stopEvent And MySqlconnectionGru.State = ConnectionState.Open Then
                                DsDocGru.Clear()
                                tlbDocGru.Clear()
                                Application.DoEvents()
                                Dim prodDoc As String, proddocAux As String

                                prodDoc = rowPrdList("bitronpn").ToString
                                proddocAux = rowPrdList("piastracode").ToString
                                Try
                                    Dim AdapterDocGruProd As New MySqlDataAdapter("SELECT * FROM documento where codicepf = '" & prodDoc & "' or codicepf = '" & proddocAux & "'", MySqlconnectionGru)
                                    AdapterDocGruProd.Fill(DsDocGru, "documento")
                                Catch ex As Exception
                                    'ListBoxLog.Items.Add("Connection lost, need waithing 20 second...")
                                    'CloseConnectionMySqlGru()
                                    'OpenConnectionMySqlGru("10.10.10.15", "Gestdoc", "chinadoc", "china")
                                    'If MySqlconnectionGru.State = ConnectionState.Open Then
                                    '    ListBoxLog.Items.Add("Connection enstabilish...Done!")
                                    '    Dim AdapterDocGruProd As New MySqlDataAdapter("SELECT * FROM documento where codicepf = '" & prodDoc & "' or codicepf = '" & proddocAux & "'", MySqlconnectionGru)
                                    '    AdapterDocGruProd.Fill(DsDocGru, "documento")
                                    'End If
                                End Try
                                If MySqlconnectionGru.State = ConnectionState.Open Then
                                    tlbDocGru = DsDocGru.Tables("documento")
                                    If prodDoc = "" Then prodDoc = proddocAux
                                    If proddocAux = "" Then proddocAux = prodDoc
                                    RowSearch = tlbDocGru.Select("( codicepf LIKE '" & prodDoc & "' or codicepf LIKE '" & proddocAux & "')  ")

                                    dima = False
                                    sw = False
                                    ProdControl = fControl(rowPrdList("bitronpn").ToString, strPcbCode, strPiastraCode)
                                    listLengh = ListView1.Items.Count
                                    Inconsistent = False
                                    WriteFile(rowPrdList("bitronpn").ToString & "   " & rowPrdList("name").ToString & " <<-->> " & rowPrdList("status").ToString, True)
                                    For Each row In RowSearch

                                        If (row("visto").ToString <> "" And row("data_obso").ToString = "") And (row("codicepf").ToString = prodDoc Or row("codicepf").ToString = proddocAux) Then

                                            Dim ssr(tblDoc.Columns.Count) As String
                                            ssr(0) = "GRU"
                                            If InStr(tipodoc(row("coddoc").ToString), "dima", CompareMethod.Text) > 0 Then dima = True
                                            If InStr(tipodoc(row("coddoc").ToString), "sw", CompareMethod.Text) > 0 Then sw = True
                                            ssr(1) = prodDoc & " -- " & tipodoc(row("coddoc").ToString)
                                            ssr(2) = row("allegato").ToString
                                            ssr(11) = FileNameDes(rowPrdList("BITRONPN").ToString)
                                            Dim kk As New ListViewItem(ssr)
                                            ListView1.Items.Add(kk)
                                            ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Aqua

                                            For Each rowCk In RowSearch
                                                If InStr(rowCk("allegato").ToString, "ECR", CompareMethod.Text) = 0 And _
                                                    InStr(rowCk("allegato").ToString, "RMP", CompareMethod.Text) = 0 And (rowCk("data_obso").ToString = "" And rowCk("visto").ToString <> "" And row("visto").ToString <> "" And row("data_obso").ToString = "") Then
                                                    Try
                                                        If (Mid(rowCk("allegato").ToString, 1, InStrRev(rowCk("allegato").ToString, "_") - 1) = Mid(row("allegato").ToString, 1, InStrRev(row("allegato").ToString, "_") - 1)) And _
                                                            (Mid(rowCk("allegato").ToString, InStr(rowCk("allegato").ToString, ".") + 1) = Mid(row("allegato").ToString, InStr(row("allegato").ToString, ".") + 1)) And _
                                                            rowCk("allegato").ToString <> row("allegato").ToString Then
                                                            ListBoxLog.Items.Add("Inconsistent file : " & row("allegato").ToString & "  and  " & rowCk("allegato").ToString)
                                                            WriteFile(("--> Inconsistent file : " & row("allegato").ToString & "  and  " & rowCk("allegato").ToString), True)
                                                            Inconsistent = True
                                                        End If
                                                    Catch ex As Exception

                                                    End Try
                                                End If
                                            Next

                                        End If

                                    Next
                                    dima = True

                                    If (Presence("F", ProdControl) = "1" And Not sw) Or Not dima Or listLengh = ListView1.Items.Count Or Inconsistent Then
                                    Else
                                        WriteFile(" --> All Doc OK!", True)
                                    End If
                                    If listLengh = ListView1.Items.Count Then
                                        ListBoxLog.Items.Add(rowPrdList("bitronpn").ToString & " - Document NOT found in Intranet!")
                                        WriteFile(" --> Document not Find In Intranet!", True)
                                    End If

                                    If Not dima Then
                                        'ListBoxLog.Items.Add(rowPrdList("bitronpn").ToString & " - Dima PCB not Find!")
                                        'WriteFile(" --> Dima PCB not Find!!", True)
                                    End If

                                    If Presence("F", ProdControl) = "1" Then
                                        If Not sw Then ListBoxLog.Items.Add(rowPrdList("bitronpn").ToString & " - Software NOT found in Intranet!")
                                        If Not sw Then WriteFile(" --> Software NOT found in Intranet!", True)
                                    End If
                                    If Presence("F", ProdControl) = "1" Or Not dima Or listLengh = ListView1.Items.Count Then WriteFile("", True)
                                End If
                            End If

                            If CheckComp.Checked And Not stopEvent Then

                                Dim prodDoc As String
                                prodDoc = rowPrdList("bitronpn").ToString

                                AdapterBom = New MySqlDataAdapter("SELECT * FROM sigip where bom = '" & prodDoc & "' and ACQ_FAB like 'ACQ' ", MySqlconnection)
                                AdapterBom.Fill(DsBom, "sigip")
                                tblBom = DsBom.Tables("sigip")
                                End If

                            If Not stopEvent Then

                                ProdControl = fControl(rowPrdList("bitronpn").ToString, strPcbCode, strPiastraCode)

                                RowSearch = tblDoc.Select("ecrPending like '*" & ComboBoxEcrPending.Text & "*' and ecrNull like '*" & _
                                            ComboBoxEcrNull.Text & "*' AND ( header like '" & IIf(Mid(ComboBoxFirstType.Text, 1, 3) = "", "*", Mid(ComboBoxFirstType.Text, 1, 3) & "_") & IIf(Mid(ComboBoxSecondType.Text, 1, 3) = "", "*", Mid(ComboBoxSecondType.Text, 1, 3) & "_") & IIf(Mid(ComboBoxThirdType.Text, 1, 3) = "", "*", Mid(ComboBoxThirdType.Text, 1, 3)) & "') AND(filename like '" & _
                                            ComboBoxProd.Text & "' or filename like '" & _
                                            rowPrdList("bitronpn").ToString & "')", "rev DESC")
                                FillListView(RowSearch, True)

                                ' IMMISSIONE GRUPPI
                                GroupList = rowPrdList("GROUPLIST").ToString()

                                If GroupList <> "" Then
                                    Dim str(2) As String
                                    I = 1
                                    J = InStr(GroupList, "]", CompareMethod.Text)
                                    While J > 0
                                        RowSearch = tblDoc.Select("(HEADER = '" & Mid(GroupList, I, 11) & "' AND filename = '" & Mid(GroupList, I + 12, J - 12 - I) _
                                         & "') and ( header like '" & IIf(Mid(ComboBoxFirstType.Text, 1, 3) = "", "*", Mid(ComboBoxFirstType.Text, 1, 3) & "_") _
                                        & IIf(Mid(ComboBoxSecondType.Text, 1, 3) = "", "*", Mid(ComboBoxSecondType.Text, 1, 3) & "_") _
                                        & IIf(Mid(ComboBoxThirdType.Text, 1, 3) = "", "*", Mid(ComboBoxThirdType.Text, 1, 3)) & "')")
                                        If RowSearch.Length > 0 Then
                                            FillListView(RowSearch, True, rowPrdList("BITRONPN").ToString)
                                        End If
                                        Dim ii As New ListViewItem(str)
                                        I = J + 2
                                        J = InStr(I + 1, GroupList, "]", CompareMethod.Text)
                                    End While
                                End If

                                If ProdControl <> "" Then
                                    rowType = tblDocType.Select("control like '*P*' AND ( header like '" & IIf(Mid(ComboBoxFirstType.Text, 1, 3) = "", "*", Mid(ComboBoxFirstType.Text, 1, 3) & "_") & IIf(Mid(ComboBoxSecondType.Text, 1, 3) = "", "*", Mid(ComboBoxSecondType.Text, 1, 3) & "_") & IIf(Mid(ComboBoxThirdType.Text, 1, 3) = "", "*", Mid(ComboBoxThirdType.Text, 1, 3)) & "')")
                                    Dim row As DataRow
                                    For Each row In rowType
                                        If Presence(Presence("Y", row("Control").ToString), ProdControl) = "1" Then
                                            Dim ListPresence As Boolean = False
                                            For I = LastRowList To ListView1.Items.Count - 1
                                                If ListView1.Items(I).SubItems(1).Text = row("header").ToString Then
                                                    ListPresence = True
                                                End If
                                            Next
                                            If Not ListPresence And ComboBoxSign.Text = "SIGNED" Then
                                                If row("header").ToString <> "65R_PRO_EMH" Then
                                                    Dim str(tblDoc.Columns.Count) As String
                                                    str(1) = row("header").ToString
                                                    str(2) = rowPrdList("BITRONPN").ToString & " -- File required But Missing!"
                                                    ' If row("header").ToString = "65R_PRO_DNS" Then str(2) = rowPrdList("BITRONPN").ToString & " -- File recommended But Missing!"
                                                    ' If row("header").ToString = "65R_PRO_ASR" Then str(2) = rowPrdList("BITRONPN").ToString & " -- File recommended But Missing!"
                                                    str(11) = FileNameDes(rowPrdList("bitronpn").ToString)
                                                    Dim ii As New ListViewItem(str)
                                                    ListView1.Items.Add(ii)
                                                    ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow
                                                    ' If row("header").ToString = "65R_PRO_DNS" Then ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.YellowGreen
                                                    ' If row("header").ToString = "65R_PRO_ASR" Then ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.YellowGreen
                                                End If
                                            End If
                                        End If
                                    Next

                                    ' Find mech element

                                    Dim ResultDoc As DataRow(), ResultProd As DataRow(), mchElement As String

                                    ResultProd = tblDocProd.Select("bitronpn = '" & rowPrdList("BITRONPN").ToString & "'")

                                    If "65R_PRO_EMH" = Mid(ComboBoxFirstType.Text, 1, 3) & "_" & Mid(ComboBoxSecondType.Text, 1, 3) & "_" & Mid(ComboBoxThirdType.Text, 1, 3) Or _
                                    "65R_PRO_" = Mid(ComboBoxFirstType.Text, 1, 3) & "_" & Mid(ComboBoxSecondType.Text, 1, 3) & "_" & Mid(ComboBoxThirdType.Text, 1, 3) Or _
                                    "65R__" = Mid(ComboBoxFirstType.Text, 1, 3) & "_" & Mid(ComboBoxSecondType.Text, 1, 3) & "_" & Mid(ComboBoxThirdType.Text, 1, 3) Or _
                                    "__" = Mid(ComboBoxFirstType.Text, 1, 3) & "_" & Mid(ComboBoxSecondType.Text, 1, 3) & "_" & Mid(ComboBoxThirdType.Text, 1, 3) Then
                                        mech = ResultProd(0).Item("mchelement").ToString

                                        For I = 0 To Int(Len(mech) / 60) - 1
                                            mchElement = Trim(Mid(mech, I * 60 + 1, 20))
                                            ResultDoc = tblDoc.Select("header = '65R_PRO_EMH' and filename like '" & mchElement & "*'")
                                            If ResultDoc.Length > 0 Then
                                                FillListView(ResultDoc, True, rowPrdList("BITRONPN").ToString)
                                            ElseIf ComboBoxSign.Text = "SIGNED" Then
                                                Dim str(tblDoc.Columns.Count - 1) As String
                                                str(1) = "65R_PRO_EMH"
                                                str(2) = mchElement & " -- File required But Missing!"
                                                str(11) = FileNameDes(rowPrdList("BITRONPN").ToString)
                                                Dim ii As New ListViewItem(str)
                                                ListView1.Items.Add(ii)
                                                ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow
                                            End If
                                        Next
                                    End If
                                Else
                                    ComunicationLog("5063") ' Product not found in product list

                                End If
                                LastRowList = ListView1.Items.Count
                            End If
                        Next

                    Else
                        ComunicationLog("5054")
                    End If
                End If

                If RadioButtonProductSearch.Checked And Not RadioButtonGeneralSearch.Checked Then
                RowSearch = tblBom.Select("bitron_pn like '" & IIf(TextBoxCompPn.Text = "", "*", TextBoxCompPn.Text) & "'", "bitron_pn")
                    Dim proNo = Trim(Mid(ComboBoxProd.Text, 1, InStr(1, ComboBoxProd.Text, "-", CompareMethod.Text) - 1))
                    If MySqlconnectionGru.State = ConnectionState.Open Then
                        DsDocGru.Clear()
                        tlbDocGru.Clear()
                Application.DoEvents()
                        If proNo <> "" Then
                            Try
                                Dim AdapterDocGruProd As New MySqlDataAdapter("SELECT * FROM documento where codicepf = '" & proNo & "'", MySqlconnectionGru)

                                AdapterDocGruProd.Fill(DsDocGru, "documento")
                            Catch ex As Exception
                                'ListBoxLog.Items.Add("Connection lost, need waithing 20 second...")
                                'CloseConnectionMySqlGru()
                                'OpenConnectionMySqlGru("10.10.10.15", "Gestdoc", "chinadoc", "china")
                                'If MySqlconnectionGru.State = ConnectionState.Open Then
                                '    ListBoxLog.Items.Add("Connection enstabilish...Done!")
                                '    Dim AdapterDocGruProd As New MySqlDataAdapter("SELECT * FROM documento where codicepf = '" & prodDoc & "' or codicepf = '" & proddocAux & "'", MySqlconnectionGru)
                                '    AdapterDocGruProd.Fill(DsDocGru, "documento")
                                'End If
                            End Try

                        End If
                    End If

                    tlbDocGru = DsDocGru.Tables("documento")
                    RowSearchGru = tlbDocGru.Select("( codicepf LIKE '" & proNo & "')")


                    Application.DoEvents()
                oldBitronPn = ""
                For Each row In RowSearch
                    If oldBitronPn <> row("bitron_pn").ToString Then
                        If Mid(row("bitron_pn").ToString, 1, 2) <> "18" Then
                            Dim ssr(tblDoc.Columns.Count) As String

                                If Mid(row("bitron_pn").ToString, 1, 2) = "11" Or Mid(row("bitron_pn").ToString, 1, 2) = "12" Or Mid(row("bitron_pn").ToString, 1, 2) = "13" Or Mid(row("bitron_pn").ToString, 1, 2) = "16" Then

                                    IndexNo = 1
                                    If RowSearchGru.Length <> 0 Then

                                        For Each row1 In RowSearchGru
                                            If row("bitron_pn").ToString = Mid(row1("allegato").ToString, 1, 8) Then
                                                ssr(0) = "MECH"
                                                ssr(11) = row1("allegato").ToString

                                                Exit For
                                            Else
                                                If Mid(row("doc").ToString, 1, 7) = "SRV_DOC" Then
                                                    ssr(0) = RevisionLast(row("doc").ToString)
                                                    'If ((Mid(row("doc").ToString, 11, 21) = "65R_PRO_EMH")) Then
                                                    If (InStr(row("doc").ToString, "EMH") <> 0) Then
                                                        ssr(11) = "Documentation : " & row("doc").ToString
                                                    Else
                                                        ssr(0) = "MISS"
                                                        ssr(11) = "No"

                                                    End If
                                                Else
                                                    ssr(0) = "MISS"
                                                    ssr(11) = "No"

                                                End If
                                            End If

                                        Next
                                    Else
                                        If Mid(row("doc").ToString, 1, 7) = "SRV_DOC" Then
                                            ssr(0) = RevisionLast(row("doc").ToString)
                                            'If ((Mid(row("doc").ToString, 11, 21) = "65R_PRO_EMH")) Then
                                            If (InStr(row("doc").ToString, "EMH") <> 0) Then
                                                ssr(11) = "Documentation : " & row("doc").ToString
                                            Else
                                                ssr(0) = "MISS"
                                                ssr(11) = "No"

                                            End If
                                        Else
                                            ssr(0) = "MISS"
                                            ssr(11) = "No"

                                        End If
                                    End If

                            ElseIf row("acq_fab").ToString = "FAB" Then
                                ssr(0) = "FAB"
                            ElseIf Mid(row("doc").ToString, 1, 2) = "HC" Then
                                ssr(0) = "HC"
                            ElseIf Mid(row("doc").ToString, 1, 7) = "SRV_DOC" Then
                                ssr(0) = RevisionLast(row("doc").ToString)
                            Else
                                ssr(0) = "MISS"
                            End If

                            ssr(1) = row("acq_fab").ToString
                            ssr(2) = row("bitron_pn").ToString & " - " & row("des_pn").ToString

                                If (IndexNo <> 1) Then
                            ssr(11) = "Documentation : " & row("doc").ToString
                                Else

                                End If

                                Dim kk As New ListViewItem(ssr)
                                ListView1.Items.Add(kk)
                                'If ssr(0) = "MISS" And ssr(11) = "No" Then
                                '    ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow
                                'ElseIf row("doc").ToString = "NO" And row("acq_fab").ToString = "ACQ" Then
                                '    ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow
                                'End If
                                'If row("doc").ToString = "NO" And row("acq_fab").ToString = "ACQ" Then ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow




                                If IndexNo = 1 Then

                                    If ssr(0) = "MISS" And ssr(11) = "No" Then
                                        ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow
                                        IndexNo = 0
                                    End If
                                Else

                            If row("doc").ToString = "NO" And row("acq_fab").ToString = "ACQ" Then ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.Yellow

                        End If
                    End If
                        End If
                    oldBitronPn = row("bitron_pn").ToString

                Next
                End If
            End If

            If CheckBoxSaveInfo.Checked Then
                SaveFileDialog1.FileName = System.IO.Path.GetTempPath & "SrvQueryLog.txt"
                SaveFileDialog1.ShowDialog()
                Try
                    If SaveFileDialog1.FileName <> "" Then FileCopy(System.IO.Path.GetTempPath & "SrvQueryLog.txt", SaveFileDialog1.FileName)

                Catch ex As Exception

                End Try
            End If
            ListBoxLog.Items.Add("Query Done!")
            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
        Else
            If Not trdFinish And Not Autoupdate Then MsgBox("Wait one moment!")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBrowse.Click

        FolderBrowserDialog1.ShowDialog()
        If FolderBrowserDialog1.SelectedPath <> "" Then
            TextBoxFilePath.Text = FolderBrowserDialog1.SelectedPath
        End If
        FolderBrowserDialog1.Dispose()
    End Sub

    Private Sub RadioButtonGeneralSearch_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonGeneralSearch.CheckedChanged
        If RadioButtonProductSearch.Checked And Not RadioButtonGeneralSearch.Checked Then
        Else
            RadioButtonProductSearch.Checked = Not RadioButtonGeneralSearch.Checked
        End If

        If RadioButtonProductSearch.Checked = False Then
            ComboBoxProd.Enabled = False
        Else

            ComboBoxProd.Enabled = True
        End If
        If RadioButtonGeneralSearch.Checked = True Then
            TextBoxCompPn.Text = ""
            TextBoxCompPn.Enabled = False
            CheckComp.Enabled = False
        End If
    End Sub
    Private Sub RadioButtonProductSearch_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonProductSearch.CheckedChanged
        If RadioButtonGeneralSearch.Checked And Not RadioButtonProductSearch.Checked Then
        Else
            RadioButtonGeneralSearch.Checked = Not RadioButtonProductSearch.Checked
        End If

        If RadioButtonGeneralSearch.Checked = False Then
            ComboBoxSecondType.Enabled = True
            ComboBoxFirstType.Enabled = True
            ComboBoxThirdType.Enabled = True
            TextBoxfileName.Enabled = False
            ComboBoxProd.Enabled = True
            ComboBoxCustomer.Enabled = True
            ComboBoxStatus.Enabled = True
            CheckGru.Enabled = True
            CheckBoxObsolete.Enabled = True
        Else
            ComboBoxSecondType.Enabled = True
            ComboBoxFirstType.Enabled = True
            ComboBoxThirdType.Enabled = True
            TextBoxfileName.Enabled = True
            ComboBoxProd.Enabled = False
            ComboBoxCustomer.Enabled = False
            ComboBoxProd.Text = ""
            ComboBoxCustomer.Text = ""
            ComboBoxStatus.Enabled = False
            CheckGru.Enabled = False
            CheckBoxObsolete.Enabled = False
            CheckGru.Enabled = False
        End If

        If RadioButtonProductSearch.Checked = True Then
            TextBoxCompPn.Text = ""
            TextBoxCompPn.Enabled = True
            CheckComp.Enabled = True
        End If
    End Sub
    Private Sub ButtonReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonReset.Click
        FillComboFirstType()
    End Sub
    Private Sub ButtonDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDelete.Click
        Dim strRes As String
        Dim strPathFtp As String
        Dim objFtp As ftp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd

        Dim sql As String, cmd As MySqlCommand, rev As Integer
        If MsgBox("Are you sure to delete a document?" & vbCrLf & "Please consider that when you delete a file with revisione greater than 0, automatically you will validate the " _
                  & "file in the server with previous revision index!!! Please care about this.", MsgBoxStyle.YesNo, "SRVDOC - File delete") = vbYes Then
            If ListView1.CheckedItems.Count = 1 Then
                RevisionExtract(rev, ListView1.CheckedItems(0).SubItems(1).Text(), ListView1.CheckedItems(0).SubItems(2).Text(), ListView1.CheckedItems(0).SubItems(4).Text())
                If Trim(Str(rev)) = ListView1.CheckedItems(0).SubItems(3).Text() Then
                    If controlRight(Mid(ListView1.Items(0).SubItems(1).Text, 3, 1)) >= 3 Then
                        Try
                            Try
                                strPathFtp = (Mid(ListView1.CheckedItems(0).SubItems(1).Text, 1, 3) & "/" & ListView1.CheckedItems(0).SubItems(1).Text)
                                strRes = objFtp.DeleteFile(strPathFtp & "/", ListView1.CheckedItems(0).SubItems(1).Text & "_" & ListView1.CheckedItems(0).SubItems(2).Text() & "_" & ListView1.CheckedItems(0).SubItems(3).Text() & "." & ListView1.CheckedItems(0).SubItems(4).Text())
                            Catch ex As Exception
                                ComunicationLog("0056") ' ftp operation error
                            End Try
                            strPathFtp = ("/" & Mid(ListView1.CheckedItems(0).SubItems(1).Text, 1, 3) & "/" & ListView1.CheckedItems(0).SubItems(1).Text)
                            strRes = objFtp.ListDirectory(strPathFtp & "/", ListView1.CheckedItems(0).SubItems(1).Text & "_" & ListView1.CheckedItems(0).SubItems(2).Text() & "_" & ListView1.CheckedItems(0).SubItems(3).Text() & "." & ListView1.CheckedItems(0).SubItems(4).Text())

                            sql = "DELETE FROM `" & DBName & "`.`doc` WHERE `doc`.`id` = " & ListView1.CheckedItems(0).SubItems(0).Text & ""
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()

                            ComunicationLog("5057") ' document deleted
                        Catch ex As Exception
                            ComunicationLog("0043") ' Mysql delete error 
                        End Try
                    Else
                        ComunicationLog("0043") 'no enough right
                    End If
                Else
                    ComunicationLog("5060")
                End If
            Else
                MsgBox("Only one file at a time can be deleted!")
            End If
        Else
            ComunicationLog("5058")
        End If

    End Sub

    Private Sub ButtonDownload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDownload.Click
        Dim objFtp As ftp = New ftp()
        Dim strPathFtp As String
        ListBoxLog.Items.Clear()
        Application.DoEvents()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        Dim rgPattern = "[\\\/:\*\?""'<>|]"
        Dim objRegEx As New Regex(rgPattern)
        Dim fileDetail As IO.FileInfo
        Dim i As Integer
        Dim cmd As New MySqlCommand()
        If TextBoxFilePath.Text <> "" Then
            For i = 0 To ListView1.CheckedItems.Count - 1
                Application.DoEvents()
                strPathFtp = ("/" & Mid(ListView1.CheckedItems(i).SubItems(1).Text, 1, 3) & "/" & ListView1.CheckedItems(i).SubItems(1).Text & "/")
                If "GRU" = (ListView1.CheckedItems(i).SubItems(0).Text) Then
                    Application.DoEvents()
                    Try
                        ListBoxLog.Items.Add("Download Web....")
                        ListBoxLog.Items.Add(ListView1.CheckedItems(i).SubItems(1).Text & "" & ListView1.CheckedItems(i).SubItems(2).Text() & "  " & ListView1.CheckedItems(i).SubItems(3).Text() & ListView1.CheckedItems(i).SubItems(4).Text())

                        Application.DoEvents()
                        My.Computer.Network.DownloadFile("http://10.10.10.15/grugliasco/gestdoc/attach/" & ListView1.CheckedItems(i).SubItems(2).Text, TextBoxFilePath.Text & "\" & ListView1.CheckedItems(i).SubItems(2).Text, "", "", False, 10000, True)
                        ComunicationLog("5076") ' file download from web
                        ListBoxLog.Items.Add("")
                    Catch ex As Exception
                        MsgBox("Document not present in Bitron Intranet. Error in Intranet DB")
                    End Try


                ElseIf "MECH" = (ListView1.CheckedItems(i).SubItems(0).Text) Then
                    Application.DoEvents()
                    Try
                        ListBoxLog.Items.Add("Download Web....")
                        'ListBoxLog.Items.Add(ListView1.CheckedItems(i).SubItems(1).Text & "" & ListView1.CheckedItems(i).SubItems(11).Text() & "  " & ListView1.CheckedItems(i).SubItems(3).Text() & ListView1.CheckedItems(i).SubItems(4).Text())

                        Application.DoEvents()
                        My.Computer.Network.DownloadFile("http://10.10.10.15/grugliasco/gestdoc/attach/" & ListView1.CheckedItems(i).SubItems(11).Text, TextBoxFilePath.Text & "\" & ListView1.CheckedItems(i).SubItems(11).Text, "", "", False, 10000, True)
                        ComunicationLog("5076") ' file download from web
                        ListBoxLog.Items.Add("")
                    Catch ex As Exception
                        MsgBox("Document Not present in intranet Bitron Web. Error in intranet DB")
                    End Try


                ElseIf (ListView1.CheckedItems(i).SubItems(0).Text) = "HC" Then
                    ListBoxLog.Items.Add("Download HC....")
                    ListBoxLog.Items.Add(ListView1.CheckedItems(i).SubItems(1).Text & "_" & ListView1.CheckedItems(i).SubItems(2).Text() & "_" & ListView1.CheckedItems(i).SubItems(3).Text() & "." & ListView1.CheckedItems(i).SubItems(4).Text())
                    My.Computer.Network.DownloadFile("http://10.10.10.15/orcad/carica_file_pdf.php?cod_comp=" & Mid(ListView1.CheckedItems(i).SubItems(11).Text, 20), TextBoxFilePath.Text & "\" & objRegEx.Replace(ListView1.CheckedItems(i).SubItems(2).Text, "") & ".pdf", "", "", False, 10000, True)
                    ComunicationLog("5076")
                    ListBoxLog.Items.Add("")
                ElseIf IsNumeric((ListView1.CheckedItems(i).SubItems(0).Text)) Then
                    Dim repeatDownload As Integer = 0

                    While repeatDownload < 3

                        ListBoxLog.Items.Add("Download Local....")
                        ListBoxLog.Items.Add(ListView1.CheckedItems(i).SubItems(1).Text & "_" & ListView1.CheckedItems(i).SubItems(2).Text() & "_" & ListView1.CheckedItems(i).SubItems(3).Text() & "." & ListView1.CheckedItems(i).SubItems(4).Text())
                        objFtp.DownloadFile(strPathFtp, TextBoxFilePath.Text, ListView1.CheckedItems(i).SubItems(1).Text & "_" & ListView1.CheckedItems(i).SubItems(2).Text() & "_" & ListView1.CheckedItems(i).SubItems(3).Text() & "." & ListView1.CheckedItems(i).SubItems(4).Text())
                        fileDetail = My.Computer.FileSystem.GetFileInfo(TextBoxFilePath.Text & "\" & ListView1.CheckedItems(i).SubItems(1).Text & "_" & ListView1.CheckedItems(i).SubItems(2).Text() & "_" & ListView1.CheckedItems(i).SubItems(3).Text() & "." & ListView1.CheckedItems(i).SubItems(4).Text())
                        If fileDetail.Length > 0 Then
                            repeatDownload = 5
                        End If
                        ComunicationLog("5062")
                        ListBoxLog.Items.Add("")
                        repeatDownload = repeatDownload + 1
                        If repeatDownload = 3 Then
                            ListBoxLog.Items.Add("Error download file: " & ListView1.CheckedItems(i).SubItems(1).Text & "_" & ListView1.CheckedItems(i).SubItems(2).Text() & "_" & ListView1.CheckedItems(i).SubItems(3).Text() & "." & ListView1.CheckedItems(i).SubItems(4).Text())
                        End If
                    End While
                End If
            Next
        Else
            ComunicationLog("5061") ' fill path
        End If
        ListBoxLog.Items.Add("Download Finish!")
    End Sub

    Private Sub ButtonSel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSel.Click
        Dim i As Integer
        For i = 0 To ListView1.Items.Count - 1
            ListView1.Items(i).Checked = True
        Next
    End Sub

    Private Sub ButtonEcr_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEcr.Click
        Dim i As Integer, sql As String, question As String
        Dim cmd As New MySqlCommand()
        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Checked And ListView1.Items(i).SubItems(9).Text <> "" Then
                If controlRight(Mid(ListView1.Items(i).SubItems(1).Text, 3, 1)) >= 2 Then
                    question = InputBox("Please write the same [*] number for invalidate the ECR Alarm" & vbCrLf & "EcrPending: " & ListView1.Items(i).SubItems(9).Text)
                    If Mid(question, 1) = "[" And Mid(question, Len(question), 1) = "]" And IsNumeric(Mid(question, 2, Len(question) - 2)) Then
                        Try
                            sql = "UPDATE `" & DBName & "`.`doc` SET `ecrnull` = '" & ListView1.Items(i).SubItems(9).Text & question & "' WHERE `doc`.`id` = '" & ListView1.Items(i).SubItems(0).Text & "' ;"
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                            ComunicationLog("5067") 'ecr nulled
                            ButtonQuery_Click(Me, e)
                        Catch ex As Exception
                            ComunicationLog("0052") 'db operation error
                        End Try
                    Else
                        ComunicationLog("0068") ' parser text ko
                    End If
                Else
                    ComunicationLog("0043") ' no right
                End If
            End If
        Next
    End Sub

    Private Sub ButtonSign_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSign.Click
        ListBoxLog.Items.Clear()
        Dim i As Integer, sql As String
        Dim cmd As New MySqlCommand()
        If ComboBoxSign.Text = "UNSIGNED" Then
            For i = 0 To ListView1.CheckedItems.Count - 1
                If (ListView1.CheckedItems(i).SubItems(1).Text <> "65R_PRO_ECR" Or (ListView1.CheckedItems(i).SubItems(1).Text = "65R_PRO_ECR" And InStr(1, ListView1.CheckedItems(i).SubItems(2).Text, "Template", CompareMethod.Text) > 0)) _
                And (ListView1.CheckedItems(i).SubItems(1).Text <> "65R_PRO_TCR" Or (ListView1.CheckedItems(i).SubItems(1).Text = "65R_PRO_TCR" And InStr(1, ListView1.CheckedItems(i).SubItems(2).Text, "Template", CompareMethod.Text) > 0)) Then
                    If ListView1.CheckedItems(i).Checked Then
                        If controlRight(Mid(ListView1.Items(i).SubItems(1).Text, 3, 1)) >= 3 Then
                            Try
                                sql = "UPDATE `" & DBName & "`.`doc` SET `sign` = '" & CreAccount.strUserName & "[" & Date.Today.Day & "/" & Date.Today.Month & "/" & Date.Today.Year & "]" & _
                                "' WHERE `doc`.`id` = " & ListView1.CheckedItems(i).SubItems(0).Text & " ;"
                                cmd = New MySqlCommand(sql, MySqlconnection)
                                cmd.ExecuteNonQuery()
                                ComunicationLog("5056") 'doc signed

                            Catch ex As Exception
                                ComunicationLog("0052") 'db operation error
                            End Try
                        Else
                            ComunicationLog("0043") ' no right
                        End If
                    End If
                Else
                    ComunicationLog("5066") ' Special Procedure For sign this document
                End If
            Next

        Else
            ComunicationLog("5053") ' plase go in unsigned section
        End If

    End Sub
    Private Sub ComboBoxStatus_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxStatus.TextChanged
        ComboBoxProd.Text = ""
        FillComboProd()
    End Sub
    Private Sub ComboBoxCustomer_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxCustomer.TextChanged
        ComboBoxProd.Text = ""
        FillComboProd()
    End Sub
    Private Sub ButtonStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonStop.Click
        stopEvent = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Application.DoEvents()
    End Sub

    Private Sub CheckGru_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        'ButtonQuery.Enabled = False
        'If loadDoc = False And CheckGru.Checked = True Then
        '    If loadDoc = False Then loadDoc = True
        '    ComunicationLog("5077")
        '    Application.DoEvents()
        '    Try
        '        OpenConnectionMySqlGru("10.10.10.15", "Gestdoc", "chinadoc", "china")
        '        Dim AdapterDocGruProdType As New MySqlDataAdapter("SELECT * FROM tipodoc", MySqlconnectionGru)
        '        AdapterDocGruProdType.Fill(DsDocGrutype, "tipodoc")
        '        tlbDocGrutype = DsDocGrutype.Tables("tipodoc")

        '        ComunicationLog("5080")
        '    Catch ex As Exception
        '        MsgBox("Database Account error, server Grugliasco open procedure")
        '        Me.Close()
        '    End Try
        'End If
        'ButtonQuery.Enabled = True
    End Sub
    Function RevisionExtract(ByRef rev As Integer, ByVal header As String, ByVal Filename As String, ByVal Extension As String) As String

        Dim returnValue As DataRow()
        Try
            RevisionExtract = ("5029") ' revision extract ok
            If controlType(header, "C") = 2 And Regex.Match(Filename, "^\w+ - ").ToString <> "" Then
                returnValue = tblDoc.Select("header='" & header & "' and FileName like '" & Regex.Match(Filename, "^\w+").ToString & " - *'", "rev DESC") 'and Extension='" & Extension & "' ", "rev DESC")
            Else
                returnValue = tblDoc.Select("header='" & header & "' and FileName='" & Filename & "'", "rev DESC") 'and Extension='" & Extension & "' ", "rev DESC")
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
    Function fControl(ByVal bitronPn As String, ByRef pcbCode As String, ByRef piastracode As String) As String

        Dim rsResult As DataRow()
        rsResult = tblDocProd.Select("bitronpn='" & bitronPn & "'")
        If rsResult.Length = 1 Then
            fControl = rsResult(0).Item("docFlag").ToString
            pcbCode = rsResult(0).Item("pcbcode").ToString
            piastracode = rsResult(0).Item("piastracode").ToString
            fControl = rsResult(0).Item("docflag").ToString

        ElseIf rsResult.Length > 1 Then
            ComunicationLog("0054") ' product duplicate
            fControl = ""
        Else
            fControl = ""
        End If
    End Function
    Function Presence(ByVal strFlag As String, ByVal strControl As String) As String
        Presence = ""
        If InStr(1, strControl, strFlag) > 0 Then Presence = Mid(strControl, InStr(1, strControl, strFlag) + 1, 1)
        If strFlag = "1" Then Presence = "1"
        If strFlag = "0" Then Presence = "0"
    End Function
    Function TranslateIntranetName(ByVal h As String, ByVal f As String, ByVal r As Integer, ByVal e As String) As String
        TranslateIntranetName = ""
        If h = "65R_PRO_GPN" Then TranslateIntranetName = f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_GFX" Then TranslateIntranetName = "fix_" & f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_NFB" Then TranslateIntranetName = f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_NFP" Then TranslateIntranetName = f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_PCB" Then TranslateIntranetName = "doc_" & f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_PST" Then TranslateIntranetName = f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_SPG" Then TranslateIntranetName = "ps_" & f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        If h = "65R_PRO_TDS" Then TranslateIntranetName = f & "_" & IIf(r > 10, r, "0" & r) & "." & e
        TranslateIntranetName = "http://10.10.10.15/grugliasco/gestdoc/attach/" & TranslateIntranetName
    End Function
    Function FileNameDes(ByVal filename As String) As String


        Dim rsResult As DataRow()
        rsResult = tblDocProd.Select("bitronpn='" & filename & "'")
        If rsResult.Length > 0 Then
            FileNameDes = rsResult(0).Item("name").ToString
        Else
            rsResult = tblDocProd.Select("piastracode='" & filename & "'")
            If rsResult.Length > 0 Then
                FileNameDes = "Piastra of " & rsResult(0).Item("name").ToString
            Else
                rsResult = tblDocProd.Select("pcbcode='" & filename & "'")
                If rsResult.Length > 0 Then
                    FileNameDes = "Pcb of " & rsResult(0).Item("name").ToString
                Else
                    FileNameDes = ""
                End If
            End If
        End If
    End Function
    Sub FillListView(ByVal rowShow As DataRow(), Optional ByVal Notclear As Boolean = False, Optional ByVal prod As String = "")
        ListBoxLog.BackColor = Color.LightGreen

        If Not stopEvent Then

            If Notclear Then

            Else
                ListView1.Clear()
            End If
            Dim c As DataColumn, i As Integer
            Dim Widht(tblDoc.Columns.Count - 1) As Integer
            If CheckBox1.Checked Then Widht(0) = 70 Else Widht(0) = 0
            If CheckBox2.Checked Then Widht(1) = 140 Else Widht(1) = 0
            If CheckBox3.Checked Then Widht(2) = 300 Else Widht(2) = 0
            If CheckBox4.Checked Then Widht(3) = 70 Else Widht(3) = 0
            If CheckBox5.Checked Then Widht(4) = 70 Else Widht(4) = 0
            If CheckBox6.Checked Then Widht(5) = 100 Else Widht(5) = 0
            If CheckBox7.Checked Then Widht(6) = 100 Else Widht(6) = 0
            If CheckBox8.Checked Then Widht(7) = 100 Else Widht(7) = 0
            If CheckBox9.Checked Then Widht(8) = 100 Else Widht(8) = 0
            If CheckBox10.Checked Then Widht(9) = 70 Else Widht(9) = 0
            If CheckBox11.Checked Then Widht(10) = 70 Else Widht(10) = 0
            If ListView1.Columns.Count < 11 Then
                i = 0
                For Each c In tblDoc.Columns

                    'adding names of columns as Listview columns				
                    Dim h As New ColumnHeader
                    h.Text = c.ColumnName
                    h.Width = Widht(i)
                    ListView1.Columns.Add(h)
                    i = i + 1
                Next
                ListView1.Columns.Add("Description")
                ListView1.Columns.Item(11).Name = "Description"

                If CheckBox12.Checked Then ListView1.Columns.Item(11).Width = 400 Else ListView1.Columns.Item(11).Width = 0
            End If



            Dim str(tblDoc.Columns.Count) As String, rev As Integer
            'adding Datarows as listview Grids

            For i = 0 To rowShow.Length - 1
                For col As Integer = 0 To tblDoc.Columns.Count - 1
                    str(col) = rowShow(i).ItemArray(col).ToString()
                Next
                If prod <> "" Then
                    str(11) = FileNameDes(prod)
                Else
                    str(11) = FileNameDes(str(2))
                End If

                Dim ii As New ListViewItem(str)

                RevisionExtract(rev, rowShow(i).ItemArray(1).ToString(), rowShow(i).ItemArray(2).ToString(), rowShow(i).ItemArray(4).ToString())

                If ComboBoxRevision.Text = "ALL" Or rev = Int(Val(rowShow(i).ItemArray(3).ToString())) Then
                    If ComboBoxSign.Text = "SIGNED" And Trim(rowShow(i).ItemArray(6).ToString()) <> "" Then
                        If controlRight(Mid(rowShow(i).ItemArray(1).ToString(), 3, 1)) >= 1 Then
                            ListView1.Items.Add(ii)
                        End If
                    ElseIf ComboBoxSign.Text = "UNSIGNED" And rowShow(i).ItemArray(6).ToString() = "" Then
                        If controlRight(Mid(rowShow(i).ItemArray(1).ToString(), 3, 1)) >= 2 Then
                            ListView1.Items.Add(ii)
                        End If
                    End If
                End If

            Next

        Else
            ComunicationLog("5072")
        End If
    End Sub
    Sub ComunicationLog(ByVal ComCode As String)

        Dim rsResult As DataRow()
        rsResult = tblError.Select("code='" & ComCode & "'")
        If rsResult.Length = 0 Then
            ComCode = "0051"
            rsResult = tblError.Select("code='" & ComCode & "'")
        End If
        ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
        ListBoxLog.Items.Add(ComCode & " -> " & rsResult(0).Item("en").ToString)
        WriteFile(ComCode & " -> " & rsResult(0).Item("en").ToString & vbCrLf, True)

        If Val(ComCode) >= 5000 Then
            ListBoxLog.BackColor = Color.LightGreen
        ElseIf Val(ComCode) < 5000 Then
            ListBoxLog.BackColor = Color.OrangeRed
        End If
    End Sub
    Sub FillComboRevision()
        ComboBoxRevision.Items.Add("LAST")
        ComboBoxRevision.Items.Add("ALL")
        ComboBoxRevision.Sorted = True
        ComboBoxRevision.Text = ("LAST")
    End Sub
    Sub FillComboProd()
        ComboBoxProd.Items.Clear()
        ComboBoxProd.Items.Add("ALL - PRODUCT")
        Dim returnValue As DataRow()
        returnValue = tblDocProd.Select(IIf(CheckBoxObsolete.Checked = True, "( status like '*') and ", "( NOT status like 'OBSOLETE') and ") & IIf(ComboBoxStatus.Text = "ALL - STATUS", "( status like '*')", "( status like '" & ComboBoxStatus.Text & "')") & " and customer like '" & IIf(ComboBoxCustomer.Text = "ALL - CUSTOMER", "*", ComboBoxCustomer.Text) & "'", "bitronpn DESC")
        For Each row In returnValue
            ComboBoxProd.Items.Add(row("bitronpn").ToString & " - " & row("name").ToString)
        Next
    End Sub
    Sub FillComboCust()
        ComboBoxCustomer.Items.Clear()
        ComboBoxCustomer.Items.Add("ALL - CUSTOMER")
        Dim returnValue As DataRow()
        returnValue = tblDocCust.Select("name like '*'", "name ASC")
        For Each row In returnValue
            ComboBoxCustomer.Items.Add(row("name").ToString)
        Next
    End Sub
    Sub FillComboFirstType()
        ComboBoxFirstType.Items.Clear()
        Dim strOld As String = ""
        Dim strNew As String
        Dim returnValue As DataRow()
        returnValue = tblDocType.Select("FirstType LIKE '*' ", "FirstType DESC")
        For Each row In returnValue
            strNew = (row("FirstType").ToString)
            If StrComp(strOld, strNew) <> 0 Then
                strOld = strNew
                ComboBoxFirstType.Items.Add(strNew)
            End If
        Next
        ComboBoxFirstType.Items.Add("")
        ComboBoxFirstType.Sorted = True
        ComboBoxSecondType.Items.Clear()
        ComboBoxThirdType.Items.Clear()
        ComboBoxProd.Text = ""
        ComboBoxCustomer.Text = ""
        TextBoxfileName.Text = ""
        TextBoxCompPn.Text = ""
    End Sub
    Sub FillComboEcrNull()
        ComboBoxEcrNull.Items.Clear()
        ComboBoxEcrPending.Items.Clear()
        Dim strOld As String = ""
        Dim strNew As String
        Dim returnValue As DataRow()
        returnValue = tblDoc.Select(" ecrnull LIKE '*ecr*'", "ecrnull DESC")
        For Each row In returnValue
            strNew = (row("ecrnull").ToString)
            If StrComp(strOld, strNew) <> 0 Then
                strOld = strNew
                ComboBoxEcrNull.Items.Add(strNew)
            End If
        Next
        ComboBoxEcrNull.Sorted = True
    End Sub
    Sub FillComboEcrPending()
        ComboBoxEcrPending.Items.Clear()
        Dim strOld As String = ""
        Dim strNew As String
        Dim returnValue As DataRow()
        returnValue = tblDoc.Select(" ecrPending LIKE '*ecr*'", "ecrPending DESC")
        For Each row In returnValue
            strNew = (row("ecrPending").ToString)
            If StrComp(strOld, strNew) <> 0 Then
                strOld = strNew
                ComboBoxEcrPending.Items.Add(strNew)
            End If
        Next
        ComboBoxEcrPending.Sorted = True
    End Sub
    Sub FillComboSign()
        ComboBoxSign.Items.Add("SIGNED")
        If InStr(1, CreAccount.strSign, "2", CompareMethod.Text) > 0 Or InStr(1, CreAccount.strSign, "3", CompareMethod.Text) > 0 Then ComboBoxSign.Items.Add("UNSIGNED")
        ComboBoxSign.Sorted = True
        ComboBoxSign.Text = "SIGNED"
    End Sub
    Function tipodoc(ByVal idtipo As Integer) As String
        Dim rsResult As DataRow()
        rsResult = tlbDocGrutype.Select("coddoc='" & idtipo & "'")
        If rsResult.Length = 1 Then
            tipodoc = rsResult(0).Item("tipodoc").ToString
        End If
    End Function

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick

        If ListView1.SelectedItems.Count = 1 Then
            If IsNumeric(ListView1.SelectedItems.Item(0).SubItems(0).Text) Then
                Dim fileOpen As String
                fileOpen = downloadFileWinPath(ListView1.SelectedItems.Item(0).SubItems(1).Text & "_" & _
                ListView1.SelectedItems.Item(0).SubItems(2).Text & "_" & _
                ListView1.SelectedItems.Item(0).SubItems(3).Text & "." & _
                ListView1.SelectedItems.Item(0).SubItems(4).Text)

                Application.DoEvents()
                Process.Start(fileOpen)
                Application.DoEvents()
            ElseIf Mid(ListView1.SelectedItems.Item(0).SubItems(0).Text, 1, 7) = "SRV_DOC" Then
                Dim fileOpen As String
                fileOpen = downloadFileWinPath(Mid(ListView1.SelectedItems.Item(0).SubItems(0).Text, 11))
                Application.DoEvents()
                Process.Start(fileOpen)
                Application.DoEvents()
            ElseIf ListView1.SelectedItems.Item(0).SubItems(0).Text = "GRU" Then

                Try
                    Application.DoEvents()
                    ListBoxLog.Items.Add("Download Web....")
                    Application.DoEvents()
                    My.Computer.Network.DownloadFile("http://10.10.10.15/grugliasco/gestdoc/attach/" & ListView1.SelectedItems(0).SubItems(2).Text, System.IO.Path.GetTempPath & ListView1.SelectedItems(0).SubItems(2).Text, "", "", False, 10000, True)
                    ListBoxLog.Items.Add("Download Web....Finish!")
                    Process.Start(System.IO.Path.GetTempPath & ListView1.SelectedItems(0).SubItems(2).Text)
                    Application.DoEvents()
                Catch ex As Exception
                    MsgBox("Document not present in Bitron Intranet. Error in Intranet DB")
                End Try

            ElseIf ListView1.SelectedItems.Item(0).SubItems(0).Text = "MECH" Then

                Try
                    Application.DoEvents()
                    ListBoxLog.Items.Add("Download Web....")
                    Application.DoEvents()
                    My.Computer.Network.DownloadFile("http://10.10.10.15/grugliasco/gestdoc/attach/" & ListView1.SelectedItems(0).SubItems(11).Text, System.IO.Path.GetTempPath & ListView1.SelectedItems(0).SubItems(11).Text, "", "", False, 10000, True)
                    ListBoxLog.Items.Add("Download Web....Finish!")
                    Process.Start(System.IO.Path.GetTempPath & ListView1.SelectedItems(0).SubItems(11).Text)
                    Application.DoEvents()
                Catch ex As Exception
                    MsgBox("Document Not present in intranet Bitron Web. Error in intranet DB")
                End Try

            ElseIf ListView1.SelectedItems.Item(0).SubItems(0).Text = "HC" Then
                Process.Start("IExplore.exe", "http://10.10.10.15/orcad/gest.php?cod_comp=" & Mid(ListView1.SelectedItems.Item(0).SubItems(11).Text, 20))
                Application.DoEvents()
                ' open DS
                'If vbYes = MsgBox("Do you want open all DS? ") Then
                Dim dsstr As String
                For i = 0 To 9
                    If i <> 1 Then
                        dsstr = ds(Mid(ListView1.SelectedItems.Item(0).SubItems(11).Text, 20), i)
                        Try
                            If dsstr <> "" Then Process.Start("IExplore.exe", dsstr)
                        Catch ex As Exception
                            MsgBox("Problem to open")
                        End Try
                        Application.DoEvents()
                    End If

                Next

                'End If

            End If
        End If

        ListView1.SelectedItems.Item(0).Checked = Not ListView1.SelectedItems.Item(0).Checked
    End Sub

    Function ds(ByVal comp As String, ByVal i As Integer) As String
        Dim RowHC As DataRow()
        RowHC = tblDocComp.Select("cod_comp = " & comp & " and valido = 'valido'", "valido")
        If RowHC.Length = 1 Then
            ds = Replace(Replace(RowHC(0)("datasheet" & IIf(i > 0, i, "")).ToString, "http://webserver.industrie.bitron.net/", "10.10.10.15\"), "\", "/")
        End If
    End Function

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
                strPathFtp = Mid(fileName, 1, 3) & "/" & Mid(fileName, 1, 11) & "/"

                ComunicationLog(objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, fileName)) ' download successfull
                downloadFileWinPath = System.IO.Path.GetTempPath & fileName
            Catch ex As Exception
                ComunicationLog("0049") ' Error in ecr Download
            End Try
        Else
            ComunicationLog("5061") ' fill path
        End If

    End Function

    Function NameFile(ByVal id As Long) As String

        Dim RowSearchDoc As DataRow()
        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        DsDoc.Clear()
        tblDoc.Clear()
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")
        RowSearchDoc = tblDoc.Select("id = " & id)
        NameFile = ""
        For Each ROW In RowSearchDoc
            NameFile = ROW("header").ToString & "_" & ROW("filename").ToString & "_" & ROW("rev").ToString & "." & ROW("extension").ToString
        Next
    End Function

    Private Sub ListView1_ColumnClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)
        ' Call the sort method to manually sort.
        ListView1.Sort()


    End Sub

    Sub SalvaFile(ByVal NomeFile As String)

        Using sw As New System.IO.StreamWriter(NomeFile, False, System.Text.Encoding.GetEncoding(1252))
            Dim sb As New System.Text.StringBuilder
            For i As Integer = 0 To ListView1.Columns.Count - 1
                sb.Append(ListView1.Columns(i).Text)
                If i <> ListView1.Columns.Count - 1 Then
                    sb.Append(ControlChars.Tab)
                Else
                    sw.WriteLine(sb.ToString)
                End If
            Next
            For Each it As ListViewItem In ListView1.Items
                sb = New System.Text.StringBuilder
                sb.Append(it.Text)
                If ListView1.Columns.Count > 1 Then
                    For i As Integer = 1 To ListView1.Columns.Count - 1
                        sb.Append(ControlChars.Tab)
                        sb.Append(it.SubItems(i).Text)
                    Next
                End If
                sw.WriteLine(sb.ToString)
            Next
        End Using
    End Sub

    Private Sub ButtonSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            SalvaFile(SaveFileDialog1.FileName)
        End If
    End Sub

    Private Sub ButtonExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonExport.Click
        ExportListview2Excel(ListView1)
    End Sub

    ' find the last revision in the Server of the current file
    Function RevisionLast(ByVal FILENAME As String) As String
        RevisionLast = FILENAME
        Dim returnValue As DataRow()
        Try
            returnValue = tblDoc.Select("header='" & Mid(FILENAME, 11, 11) & "' and FileName='" & Mid(FILENAME, 23, InStrRev(FILENAME, "_") - 23) & "' and Extension='" & Mid(FILENAME, InStrRev(FILENAME, ".") + 1) & "' ", "rev DESC")
            If returnValue.Length >= 1 Then
                RevisionLast = "SRV_DOC - " & returnValue(0).Item("header") & "_" & returnValue(0).Item("filename") & "_" & returnValue(0).Item("rev") & "." & returnValue(0).Item("extension")
            Else

            End If
        Catch ex As Exception
            MsgBox("Revision Extract Error")
        End Try

    End Function

    ' check the control type of file
    ' if type not find give -1
    Function controlType(ByVal header As String, ByVal controlString As String) As Integer
        Dim intpos As Integer
        controlType = -1 ' type enot find
        Dim row As DataRow()
        row = tblDocType.Select("header = '" & header & "'")
        If row.Length > 0 Then
            intpos = InStr(1, row(0).Item("control").ToString, controlString, CompareMethod.Text)
            If intpos > 0 Then
                controlType = Val(Mid(row(0).Item("Control").ToString, intpos + 1, 1))
            Else
                controlType = 0
            End If
        End If
    End Function



    Private Sub ThreadTask()
        ButtonConnection.BackColor = Color.Red

        Dim GruDBAdr = ParameterTable("GruDBAdr")
        Dim GruDBName = ParameterTable("GruDBName")
        Dim GruUserName = ParameterTable("GruDBUsername")
        Dim OrcadDBPwd = ParameterTable("GruDBUserPsw")

        Try
            'OpenConnectionMySqlGru(GruDBAdr, GruDBName, GruUserName, OrcadDBPwd)
            OpenConnectionMySqlGru("10.10.10.15", "Gestdoc", "chinadoc", "china")
            Dim AdapterDocGruProdType As New MySqlDataAdapter("SELECT * FROM tipodoc", MySqlconnectionGru)
            AdapterDocGruProdType.Fill(DsDocGrutype, "tipodoc")
            tlbDocGrutype = DsDocGrutype.Tables("tipodoc")
            ButtonConnection.BackColor = Color.Green
            Application.DoEvents()



        Catch ex As Exception
            MsgBox("Database Account error, server Grugliasco open procedure")

        End Try

        Try
            DsDocComp.Clear()
            tblDocComp.Clear()

        Catch ex As Exception

        End Try


        Try
            Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where not valido = 'no_valido'", SQLconnectionOrcad)
            AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
            tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")
        Catch ex As Exception
            Try
                CloseConnectionSqlOrcad()
                OpenConnectionMySqlOrcad("10.10.10.36", "orcad1", "orcadw", "orcadw")
                'OpenConnectionMySqlOrcad(OrcadDBAdr, OrcadDBName, OrcadDBUser, OrcadDBPwd)

                Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where not valido = 'no_valido'", SQLconnectionOrcad)
                AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
                tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")
            Catch

                MsgBox("Error in orcad Database Open! adesso - " & ex.Message)

            End Try

        End Try

        If MySqlconnectionGru.State = ConnectionState.Closed Then CheckGru.Checked = False

        Dim RowSearchDoc As DataRow()
        AdapterBom.Fill(DsBom, "sigip")
        tblBom = DsBom.Tables("sigip")
        RowSearchDoc = tblBom.Select("doc=''")
        If RowSearchDoc.Length > 0 Then
            MsgBox("Is possible missing result please refresh sigip bom import")
        End If

        trdFinish = True

    End Sub


End Class