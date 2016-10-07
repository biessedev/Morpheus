Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Data
Imports System.Data.OleDb
Imports System.Text.RegularExpressions


Public Class FormOffer
    Dim NoStatusChange As Boolean
    Dim ConnectionStringOrcad As String
    Dim SqlconnectionOrcad As New SqlConnection
    Dim info As String
    Dim AdapterSql As SqlDataAdapter
    Dim TblSql As New DataTable
    Dim DsSql As New DataSet
    Dim typefilled As String = ""
    Dim updatigComponentTBD As Boolean
    Dim AdapterCustomerPrice As New MySqlDataAdapter("SELECT * FROM CustomerPrice", MySqlconnection)
    Dim tblCustomerPrice As DataTable
    Dim DsCustomerPrice As New DataSet
    Dim AdapterBomOff As New MySqlDataAdapter("SELECT * FROM Bomoffer", MySqlconnection)
    Dim tblBomOff As DataTable
    Dim DsBomOff As New DataSet
    Dim AdapterPfp As New MySqlDataAdapter("SELECT * FROM Pfp", MySqlconnection)
    Dim tblPfp As DataTable
    Dim DsPfp As New DataSet
    Dim AdapterOff As New MySqlDataAdapter("SELECT * FROM offer", MySqlconnection)
    Dim tblOff As DataTable
    Dim DsOff As New DataSet
    Dim AdapterBrand As New MySqlDataAdapter("SELECT * FROM Brand", MySqlconnection)
    Dim tblBrand As DataTable
    Dim DsBrand As New DataSet
    Dim AdapterSigip As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
    Dim tblSigip As DataTable
    Dim DsSigip As New DataSet
    Dim AdapterClass As New MySqlDataAdapter("SELECT * FROM ComponentClass", MySqlconnection)
    Dim tblClass As DataTable
    Dim DsClass As New DataSet
    Dim AdapterCus As New MySqlDataAdapter("SELECT * FROM Customer", MySqlconnection)
    Dim tblPfpElaborated As DataTable
    Dim DsPfpElaborated As New DataSet
    Dim AdapterPfpElaborated As New MySqlDataAdapter("SELECT * FROM PfpElaborated", MySqlconnection)
    Dim tblCus As DataTable
    Dim DsCus As New DataSet, selectedNode As TreeNode
    Dim OpenSession As Boolean, updating As Boolean
    Dim CurrentComponentID As Long
    Dim ComponentSession As Boolean
    Dim BrandSession As Boolean
    Dim updatigComponent As Boolean = True
    Dim updatigBrand As Boolean = True
    Dim CurrentBrandID As Long
    Dim tblBomOffVer As DataTable
    Dim DsBomOffVer As New DataSet
    Dim firstTime As Boolean
    Dim afterSelectComp As Boolean
    Dim LastValueBrandCombo As String
    Dim onlyType As Boolean
    Dim BooLinkingChanges As Boolean
    Dim priceSensitiveChangesBrandAlt As Boolean = False
    Dim priceSensitiveChangesBrand As Boolean = False
    Dim priceSensitiveChangesBitronPN As Boolean = False
    Dim Price_modified As Boolean
    Dim a As Single = 0.1
    Dim NoInfoBomBest As Boolean = False
    Dim OrcadProblem As Boolean = False
    Dim EstimatedFirst As Boolean = True


    Private Sub FormOffer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If OpenSession = True Then
            If vbYes = MsgBox("Session Open do you want Save?", MsgBoxStyle.YesNo) Then
                ButtonBomSave_Click(Me, e)
            Else
                OpenSession = False
                session("BomOffer", currentId, False)
            End If
        Else
        End If
        Dim n As New TreeNode
        Dim eTree As New System.Windows.Forms.TreeViewCancelEventArgs(n, False, TreeViewAction.Unknown)

        If OpenSession Then TreeViewBomList_BeforeSelect(Me, eTree)
        If BrandSession Then TreeViewBrand_BeforeSelect(Me, eTree)
        If ComponentSession Then TreeViewComponent_BeforeSelect(Me, eTree)

        FormStart.Show()
        FormStart.Focus()
    End Sub


    Private Sub formOffer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        updatigComponent = True
        CheckBoxBestPrice.Enabled = True
        Me.Focus()
        EnableBrandControl()
        If controlRight("R") >= 2 Then

            ComboBoxComponentStatus.Items.Add("R&D CHEKED")
            ComboBoxComponentStatus.Items.Add("R&D MODIFIED")
            ComboBoxComponentStatus.Items.Add("UNCHECKED")
            TextBoxComponentBitronPN.ReadOnly = False
            TextBoxComponentDescription.ReadOnly = False
            TextBoxComponentCustomer.Enabled = True

            TextBoxComponentqt1.Enabled = True
            TextBoxComponentqt2.Enabled = True
            TextBoxComponentqt4.Enabled = True
            TextBoxComponentqt3.Enabled = True
            TextBoxComponentqt5.Enabled = True
            TextBoxComponentqt6.Enabled = True
            TextBoxComponentReference.Enabled = True
            TextBoxComponentRNDNote.ReadOnly = False
            ComboBoxComponentTHM.Enabled = True
            ComboBoxComponentBrand.Enabled = True
            ComboBoxComponentALTBrand.Enabled = True
            ComboBoxCompoentType.Enabled = True
            ComboBoxComponentBrand.Enabled = True
            ButtonComponetAddDs.Enabled = True
            TreeViewComponent.Enabled = True
            ButtonComponentDelLink.Enabled = True
            ButtonBomAddOffer.Enabled = True
            ButtonBomDelOffer.Enabled = True
            ButtonBomOfferOpen.Enabled = True
            ButtonUpdatePfp.Enabled = True
            ComboBoxBomResult.Enabled = True
            TextBoxBomTurnover.Enabled = True
            ButtonComponentImportOrcad.Enabled = True
            ComboBoxClass.Enabled = True

            'CheckBoxResult_On_Going.Enabled = True

        ElseIf controlRight("U") >= 2 Then

            For i = 0 To TabControl.TabPages.Item(0).Controls.Count - 1
                TabControl.TabPages.Item(0).Controls.Item(i).Enabled = False
            Next
            TreeViewBomList.Enabled = True
            TreeViewComponent.Enabled = True
            ComboBoxComponentStatus.Items.Add("PRICE OK")
            ComboBoxComponentStatus.Items.Add("PRICE ASKED")
            ComboBoxComponentStatus.Items.Add("R&D CHEKED")
            ComboBoxComponentPrice.Enabled = True
            ComboBoxComponentPriceAlt.Enabled = True
            ComboBoxComponentEstimation.Enabled = True
            TextBoxComponentPrice.Enabled = True
            TextBoxComponentPriceAlt.Enabled = True
            TextBoxComponentEstimation.Enabled = True
            TextBoxComponentPurchNote.ReadOnly = False
            TextBoxComponentBitronPN.ReadOnly = True
            ComboBoxComponentCustomerCurrency.Enabled = True
            TextBoxComponentCustomerPrice.Enabled = True
            ButtonComponentBitronPnTRF.Enabled = True
            ButtonComponentAltTFT.Enabled = True
            ButtonComponentPriceTFT.Enabled = True
            ButtonComponentCustomerTRF.Enabled = True
            ButtonBrandAddOffer.Enabled = True
            ButtonBrandDelOffer.Enabled = True
            ButtonBomBest.Enabled = True
            CheckBoxOpen.Enabled = True
            FillcomboBrandSupplier()
            ButtonBomOfferOpen.Enabled = True
            'CheckBoxResult_On_Going.Enabled = True
            ComboBoxCustomerFilter.Enabled = True
            Label31.Enabled = True
            CheckBoxEstimation.Enabled = True
            CheckBoxOrderByNumber.Enabled = True
            CheckBoxOrderByDate.Enabled = True
        Else

        End If

        DateTimePickerBom.Enabled = False
        ComboBoxBomCurrency.Enabled = False
        ComboBoxCustomer.Enabled = False
        TextBoxNameV1.Enabled = False
        TextBoxNameV2.Enabled = False
        TextBoxNameV3.Enabled = False
        TextBoxNameV4.Enabled = False
        TextBoxNameV5.Enabled = False
        TextBoxNameV6.Enabled = False
        TextBoxQt_V1.Enabled = False
        TextBoxQt_V2.Enabled = False
        TextBoxQt_V3.Enabled = False
        TextBoxQt_V4.Enabled = False
        TextBoxQt_V5.Enabled = False
        TextBoxQt_V6.Enabled = False

        TextBoxNote.Enabled = False
        DateTimePickerBom.Text = ""
        ComboBoxBomCurrency.Text = ""
        TextBoxNameV1.Text = ""
        TextBoxNameV2.Text = ""
        TextBoxNameV3.Text = ""
        TextBoxNameV4.Text = ""
        TextBoxNameV5.Text = ""
        TextBoxNameV6.Text = ""
        TextBoxQt_V1.Text = ""
        TextBoxQt_V2.Text = ""
        TextBoxQt_V3.Text = ""
        TextBoxQt_V4.Text = ""
        TextBoxQt_V5.Text = ""
        TextBoxQt_V6.Text = ""
        TextBoxBomName.Text = ""
        TextBoxNote.Text = ""
        ComboBoxCustomer.Text = ""

        AdapterBomOff.Fill(DsBomOff, "BomOffer")
        tblBomOff = DsBomOff.Tables("BomOffer")

        AdapterCus.Fill(DsCus, "Customer")
        tblCus = DsCus.Tables("Customer")

        AdapterClass.Fill(DsClass, "ComponentClass")
        tblClass = DsClass.Tables("ComponentClass")

        ' PfpElaborated()
        UpdateBomFields()
        FillcomboCustomer()
        FillcomboCurrency()
        UpdateTreeBomOffer()
        ButtonBomSave.BackColor = Color.Green

        ' COMPONENT


        ComboBoxBomResult.Items.Add("NEGATIVE")
        ComboBoxBomResult.Items.Add("SENT")
        ComboBoxBomResult.Items.Add("ANNULLED")
        ComboBoxBomResult.Items.Add("POSITIVE")
        ComboBoxBomResult.Items.Add("ON_GOING")
        ComboBoxBomResult.Items.Add("")

        ComboBoxBomStatusFilter.Items.Add("AQUIRED")
        ComboBoxBomStatusFilter.Items.Add("LOST")
        ComboBoxBomStatusFilter.Items.Add("SENT")
        ComboBoxBomStatusFilter.Items.Add("ANNULLED")
        ComboBoxBomStatusFilter.Items.Add("ON_GOING")
        ComboBoxBomStatusFilter.Items.Add("")

        ComboBoxCompoentType.Items.Add("SMD_T")
        ComboBoxCompoentType.Items.Add("SMD_B")
        ComboBoxCompoentType.Items.Add("AX")
        ComboBoxCompoentType.Items.Add("RD")
        ComboBoxCompoentType.Items.Add("P")
        ComboBoxCompoentType.Items.Add("FP")
        ComboBoxCompoentType.Items.Add("")

        ComboBoxComponentEstimation.Items.Add("EUR")
        ComboBoxComponentEstimation.Items.Add("USD")
        ComboBoxComponentEstimation.Items.Add("CNY")
        ComboBoxComponentEstimation.Items.Add("JPY")
        ComboBoxComponentEstimation.Items.Add("")

        ComboBoxComponentCustomerCurrency.Items.Add("EUR")
        ComboBoxComponentCustomerCurrency.Items.Add("USD")
        ComboBoxComponentCustomerCurrency.Items.Add("CNY")
        ComboBoxComponentCustomerCurrency.Items.Add("JPY")
        ComboBoxComponentCustomerCurrency.Items.Add("")

        ComboBoxComponentPriceAlt.Items.Add("EUR")
        ComboBoxComponentPriceAlt.Items.Add("USD")
        ComboBoxComponentPriceAlt.Items.Add("CNY")
        ComboBoxComponentPriceAlt.Items.Add("JPY")
        ComboBoxComponentPriceAlt.Items.Add("")

        ComboBoxComponentPrice.Items.Add("EUR")
        ComboBoxComponentPrice.Items.Add("USD")
        ComboBoxComponentPrice.Items.Add("CNY")
        ComboBoxComponentPrice.Items.Add("JPY")
        ComboBoxComponentPrice.Items.Add("")

        ComboBoxBrandCurrency.Items.Add("EUR")
        ComboBoxBrandCurrency.Items.Add("USD")
        ComboBoxBrandCurrency.Items.Add("CNY")
        ComboBoxBrandCurrency.Items.Add("JPY")
        ComboBoxBrandCurrency.Items.Add("")

        ComboBoxComponentTHM.Items.Add("KG")
        ComboBoxComponentTHM.Items.Add("M")
        ComboBoxComponentTHM.Items.Add("L")
        ComboBoxComponentTHM.Items.Add("EA")
        ComboBoxComponentTHM.Items.Add("")

        ComboBoxBomStatus.Items.Add("OPEN")
        ComboBoxBomStatus.Items.Add("CLOSED")

        If controlRight("R") >= 2 Then ButtonComponentDelete.Enabled = True
        If controlRight("R") >= 2 Then ButtonComponentAdd.Enabled = True
        If controlRight("R") >= 2 Or controlRight("U") >= 2 Then ButtonBrandDel.Enabled = True
        If controlRight("R") >= 2 Or controlRight("U") >= 2 Then ButtonBrandAdd.Enabled = True
        If controlRight("R") >= 2 Or controlRight("U") >= 2 Then ButtonAlTDel.Enabled = True
        If controlRight("R") >= 2 Or controlRight("U") >= 2 Then ButtonAltAdd.Enabled = True
        If controlRight("U") >= 2 Then CheckBoxResult_On_Going.Checked = True
        If controlRight("R") = 3 Then CheckBoxResult_On_Going.Checked = True
        If controlRight("R") >= 2 Then ButtonBomSaveCurrency.Enabled = True
        If controlRight("R") >= 2 Then TextBoxBomUSD_CNY.Enabled = True
        If controlRight("R") >= 2 Then TextBoxBomEUR_USD.Enabled = True
        If controlRight("R") >= 2 Then TextBoxBomEUR_JPY.Enabled = True
        If controlRight("U") >= 2 Then ComboBoxBomStatusFilter.Enabled = True
        If controlRight("U") >= 2 Then ComboBoxBomStatusFilter.Text = "ON_GOING"

        updatigComponent = False
        TreeViewComponent.HideSelection = False
        TreeViewBomList.HideSelection = False
        TreeViewBrand.HideSelection = False

        AdapterPfp.Fill(DsPfp, "pfp")
        tblPfp = DsPfp.Tables("pfp")


        TextBoxBomUSD_CNY.Text = ParameterTable("USD_CNY")
        TextBoxBomEUR_USD.Text = ParameterTable("EUR_USD")
        TextBoxBomEUR_JPY.Text = ParameterTable("EUR_JPY")
        'Fill orcad supplier

        FillComboBoxClass()

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


        AdapterBrand.Fill(DsBrand, "Brand")
        tblBrand = DsBrand.Tables("Brand")
        CheckBoxBestPrice.Enabled = True
        CheckBoxBestPrice.Visible = True
    End Sub

    ' Fill Combo Currency
    Sub FillcomboCurrency()
        ComboBoxBomCurrency.Items.Clear()
        ComboBoxBomCurrency.Items.Add("")
        ComboBoxBomCurrency.Items.Add("EUR")
        ComboBoxBomCurrency.Items.Add("CNY")
        ComboBoxBomCurrency.Items.Add("USD")
        ComboBoxBomCurrency.Items.Add("JPY")
    End Sub

    ' update the table bom offer from the data table offfer
    ' ask to delete bom offer that arent item in offer table
    Sub FillcomboCustomer()
        Dim rowResults As DataRow(), PrevCustomer As String

        ComboBoxCustomer.Items.Clear()
        ComboBoxCustomer.Items.Add("")
        ComboBoxCustomerFilter.Items.Clear()
        ComboBoxCustomerFilter.Items.Add("")
        DsBomOff.Clear()
        tblBomOff.Clear()
        AdapterBomOff.Fill(DsBomOff, "BomOffer")
        tblBomOff = DsBomOff.Tables("BomOffer")

        DsCus.Clear()
        tblCus.Clear()
        AdapterCus.Fill(DsCus, "Customer")
        tblCus = DsCus.Tables("Customer")
        PrevCustomer = ""
        Try
            rowResults = tblBomOff.Select("CUSTOMER like '*'", "CUSTOMER")
            For Each row In rowResults
                If PrevCustomer <> row("CUSTOMER").ToString Then
                    ComboBoxCustomer.Items.Add(row("CUSTOMER").ToString)
                    ComboBoxCustomerFilter.Items.Add(row("CUSTOMER").ToString)
                End If
                PrevCustomer = row("CUSTOMER").ToString
            Next
            ComboBoxCustomer.Sorted = True
            ComboBoxCustomerFilter.Sorted = True
        Catch ex As Exception

        End Try

    End Sub

   

    Sub FillComboBoxClass()
        Dim rowResults As DataRow()

        ComboBoxClass.Items.Clear()
        ComboBoxClass.Items.Add("")
        DsClass.Clear()
        tblClass.Clear()
        AdapterClass.Fill(DsClass, "componentClass")
        tblClass = DsClass.Tables("componentClass")
        rowResults = tblClass.Select("class like '*'", "class")
        For Each row In rowResults
            ComboBoxClass.Items.Add(row("class").ToString)
        Next
        ComboBoxClass.Sorted = True
    End Sub


    Sub FillcomboBrandSupplier()


        Dim tblBrand As DataTable
        Dim DsBrand As New DataSet

        Dim rowResults As DataRow()

        ComboBoxBrandSupplier.Items.Clear()
        ComboBoxBrandSupplier.Items.Add("")

        AdapterBrand.Fill(DsBrand, "brand")
        tblBrand = DsBrand.Tables("brand")
        rowResults = tblBrand.Select("not buyer = 'SystemLiking'", "supplier")
        For Each row In rowResults
            If Not ComboBoxBrandSupplier.Items.Contains(UCase(row("supplier").ToString)) And row("supplier").ToString <> "" Then ComboBoxBrandSupplier.Items.Add(UCase(row("supplier").ToString))
        Next
        ComboBoxBrandSupplier.Sorted = True
    End Sub

    ' update the tree bom offer list
    Sub UpdateTreeBomOffer()
        TreeViewBomList.BeginUpdate()
        Dim name As String, customer As String, SEL As String
        TreeViewBomList.Nodes.Clear()
        Dim rootNode As TreeNode, childNode As TreeNode
        Dim rowShow As DataRow(), n As Integer = 0
        DsBomOff.Clear()
        tblBomOff.Clear()
        AdapterBomOff.Fill(DsBomOff, "BomOffer")
        tblBomOff = DsBomOff.Tables("BomOffer")
        'SEL = "result = '' or name like '*'" & IIf(ComboBoxBomStatusFilter.Text <> "", " AND result = '" & ComboBoxBomStatusFilter.Text & "' ", "") & IIf(ComboBoxCustomerFilter.Text <> "", " AND customer = '" & ComboBoxCustomerFilter.Text & "'", "") & IIf(CheckBoxOpen.Checked, " AND STATUS = 'OPEN'", "") & IIf(CheckEstimed.Checked, " AND bitronpn like '*e*'", "")
        SEL = "name like '*'" & IIf(ComboBoxBomStatusFilter.Text <> "", " AND result = '" & ComboBoxBomStatusFilter.Text & "' ", "") & IIf(ComboBoxCustomerFilter.Text <> "", " AND customer = '" & ComboBoxCustomerFilter.Text & "'", "") & IIf(CheckBoxOpen.Checked, " AND STATUS = 'OPEN'", "") & IIf(CheckEstimed.Checked, " AND bitronpn like '*e*'", "")
        rowShow = tblBomOff.Select(SEL, IIf(CheckBoxOrderByDate.Checked, "eta ", IIf(CheckBoxOrderByNumber.Checked, "id", "customer,name")))
        name = ""
        customer = "-"
        For Each row In rowShow
            If row("customer").ToString <> customer And Not CheckBoxOrderByDate.Checked Then
                rootNode = New TreeNode(" - " & row("customer").ToString)
                TreeViewBomList.Nodes.Add(rootNode)
                customer = row("customer").ToString

            End If
            If row("name").ToString <> name Then
                childNode = New TreeNode(row("id").ToString & " - " & row("name").ToString & " - ETA: " & row("eta").ToString)
                childNode.NodeFont = New Font("times new roman", 12, FontStyle.Regular)
                'If EstimatedFirst = True Then
                If estimated(row("name").ToString) Then childNode.ForeColor = Color.Red

                'End If
                If IsNothing(rootNode) Then rootNode = New TreeNode("")
                If CheckBoxOrderByDate.Checked Then TreeViewBomList.Nodes.Add(childNode)
                If Not CheckBoxOrderByDate.Checked Then rootNode.Nodes.Add(childNode)
                name = row("name").ToString
                n = n + 1
            End If
        Next
        Label13.Text = "Bom name  -  Finded " & n & " Bom"
        TreeViewBomList.ExpandAll()
        TreeViewBomList.EndUpdate()
        EstimatedFirst = False
    End Sub

    ' is true if find estimated in the bom or "E" pr "Price_est"
    Function estimated(ByVal bomName As String) As Boolean

        estimated = False
        Dim i As Integer
        Dim rowShow As DataRow()
        Static Dim tblOff As DataTable
        Static Dim DsOff As New DataSet
        Try
            i = tblOff.Rows.Count
        Catch ex As Exception
            AdapterOff.Fill(DsOff, "Offer")
            tblOff = DsOff.Tables("offer")
        End Try

        rowShow = tblOff.Select("name ='" & bomName & "' and ( status = 'ESTIMED' ) ")
        If rowShow.Length > 0 Then
            estimated = True
        Else
            estimated = False
        End If
        'DsOff.Dispose()
        'tblOff.Dispose()

    End Function

    ' extract brand[oc] in positin n
    Function ExtractBrand(ByVal n As Integer, ByVal s As String) As String
        ExtractBrand = ""
        Dim j As Integer, i As Integer, brand As String, pos As Integer
        Try
            s = s & ";"
            i = 1
            j = InStr(s, ";", CompareMethod.Text)
            pos = 1
            While j > 0
                brand = Mid(s, i, j - i)
                If InStr(brand, "[", CompareMethod.Text) > 1 Then
                    If InStr(brand, "]", CompareMethod.Text) > 3 Then
                        If InStr(brand, "]", CompareMethod.Text) = Len(brand) Then
                            If n = pos Then
                                ExtractBrand = Mid(s, i, j - 1)
                            End If
                        Else
                            MsgBox("Error in brand / brandAlt")
                            Exit While
                        End If
                    Else
                        MsgBox("Error in brand / brandAlt id")
                        Exit While
                    End If
                Else
                    MsgBox("Error in brand / brandAlt id")
                    Exit While
                End If
                i = j
                j = InStr(s, ";", CompareMethod.Text)
            End While

        Catch ex As Exception
            MsgBox("Error in brand / brandAlt")
        End Try
    End Function

    ' check the sintax of brand[oc];brand2[oc2]
    Function CheckBrandString(ByVal s As String, ByVal id As String) As Boolean
        Dim j As Integer, i As Integer, brand As String
        CheckBrandString = False
        If s <> "" Then
            Try
                If Mid(s, Len(s), 1) <> ";" Then s = s & ";"
                i = 1
                j = InStr(s, ";", CompareMethod.Text)
                While j > 0
                    brand = Mid(s, i, j - i)
                    If InStr(brand, "[", CompareMethod.Text) > 1 Then
                        If InStr(brand, "]", CompareMethod.Text) > 3 Then
                            If InStr(brand, "]", CompareMethod.Text) = Len(brand) Then
                                CheckBrandString = True
                            Else
                                MsgBox("Error in brand / brandAlt id:" & id)
                                Exit While
                            End If
                        Else
                            MsgBox("Error in brand / brandAlt id:" & id)
                            Exit While
                        End If
                    Else
                        MsgBox("Error in brand / brandAlt id:" & id)
                        Exit While
                    End If
                    i = j
                    j = InStr(j + 1, s, ";", CompareMethod.Text)
                End While

            Catch ex As Exception
                MsgBox("Error in brand / brandAlt id:" & id)
            End Try
        Else
            CheckBrandString = True
        End If
    End Function

    ' check if there is bom name in the offer table.
    Function CheckBomExist(ByVal s As String, ByVal refresh As Boolean) As Boolean

        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("offer")


            rowShow = tblOff.Select("name = '" & s & "'")
            If rowShow.Length > 0 Then
                CheckBomExist = True
            Else
                CheckBomExist = False
            End If
            DsOff.Dispose()
            tblOff.Dispose()

    End Function

    ' replace the not ascii char
    Function ReplaceChar(ByVal s As String) As String
        ReplaceChar = s
        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 93 Or Asc(Mid(s, i, 1)) = 91 Or Asc(Mid(s, i, 1)) = 59 Or Asc(Mid(s, i, 1)) = 46 Or Asc(Mid(s, i, 1)) = 37 Then
            Else
                s = Replace(s, Mid(s, i, 1), "-")
            End If
            ReplaceChar = s
        Next

    End Function


    Function ReplaceCharBrandOC(ByVal s As String) As String

        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 91 Or Asc(Mid(s, i, 1)) = 93 Then
            Else
                s = Replace(s, Mid(s, i, 1), "-")
            End If
            ReplaceCharBrandOC = s
        Next
        ReplaceCharBrandOC = Replace(s, "-", "")

    End Function



    Private Sub TreeViewBomList_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewBomList.AfterSelect

        If currentId() > 0 Then
            selectedNode = TreeViewBomList.SelectedNode
            UpdateBomFields()
            If controlRight("R") >= 2 Then
                DateTimePickerBom.Enabled = True
                ComboBoxBomCurrency.Enabled = True
                ButtonBomImportSigipBom.Enabled = True
                ButtonLoadBom.Enabled = True
                TextBoxNameV1.Enabled = True
                TextBoxNameV2.Enabled = True
                TextBoxNameV3.Enabled = True
                TextBoxNameV4.Enabled = True
                TextBoxNameV5.Enabled = True
                TextBoxNameV6.Enabled = True
                TextBoxQt_V1.Enabled = True
                TextBoxQt_V2.Enabled = True
                TextBoxQt_V3.Enabled = True
                TextBoxQt_V4.Enabled = True
                TextBoxQt_V5.Enabled = True
                TextBoxQt_V6.Enabled = True

                TextBoxNote.Enabled = True
                ComboBoxCustomer.Enabled = True
                ButtonBomSave.Enabled = True
                ButtonLoadBom.Enabled = True
                ComboBoxBomStatus.Enabled = True
                CheckBoxOpen.Enabled = True
                TextBoxBomV1.Text = ""
                TextBoxBomV2.Text = ""
                TextBoxBomV3.Text = ""
                TextBoxBomv4.Text = ""
                TextBoxBomV5.Text = ""
                TextBoxBomv6.Text = ""
            End If
            ButtonBomSave.BackColor = Color.Green
        Else
            ComboBoxBomStatus.Enabled = False
            ComboBoxCustomer.Enabled = False
            DateTimePickerBom.Enabled = False
            ComboBoxBomCurrency.Enabled = False
            TextBoxNameV1.Enabled = False
            TextBoxNameV2.Enabled = False
            TextBoxNameV3.Enabled = False
            TextBoxNameV4.Enabled = False
            TextBoxNameV5.Enabled = False
            TextBoxNameV6.Enabled = False
            TextBoxQt_V1.Enabled = False
            TextBoxQt_V2.Enabled = False
            TextBoxQt_V3.Enabled = False
            TextBoxQt_V4.Enabled = False
            TextBoxQt_V5.Enabled = False
            TextBoxQt_V6.Enabled = False

            TextBoxNote.Enabled = False
            DateTimePickerBom.Text = ""
            ComboBoxBomCurrency.Text = ""
            TextBoxNameV1.Text = ""
            TextBoxNameV2.Text = ""
            TextBoxNameV3.Text = ""
            TextBoxNameV4.Text = ""
            TextBoxNameV5.Text = ""
            TextBoxNameV6.Text = ""
            TextBoxQt_V1.Text = ""
            TextBoxQt_V2.Text = ""
            TextBoxQt_V3.Text = ""
            TextBoxQt_V4.Text = ""
            TextBoxQt_V5.Text = ""
            TextBoxQt_V6.Text = ""
            TextBoxBomName.Text = ""
            ProgressBarBom.Value = 0
            TextBoxNote.Text = ""
            ComboBoxCustomer.Text = ""
            ButtonBomSave.Enabled = False
            ButtonBomSave.BackColor = Color.Gray
            CheckBoxOpen.Enabled = True
        End If

        If currentId() > 0 Then
            If CheckBomExist(TextBoxBomName.Text, True) Then
                ButtonLoadBom.Text = "Bom Present, Reload"
                ButtonLoadBom.BackColor = Color.Green
            Else
                ButtonLoadBom.Text = "Bom missing, Load"
                ButtonLoadBom.BackColor = Color.Yellow
            End If
        Else
            ButtonLoadBom.BackColor = Color.Gray
            ButtonLoadBom.Text = "Bom missing, Load"
        End If

        If currentId() > 0 Then

        Else
            ButtonBomImportSigipBom.BackColor = Color.Gray
            ButtonBomImportSigipBom.Text = "List missing, Load"
        End If
        CheckBoxBestPrice.Visible = True
    End Sub

    'save the date in the form
    Private Sub ButtonBomSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomSave.Click
        If Not (ComboBoxBomResult.Text = "ON_GOING" And ComboBoxBomStatus.Text = "CLOSED") Then
            If checkValue() Then
                If currentId() > 0 Then
                    If DeltaSessionTime("BomOffer", currentId) < 30 And session("BomOffer", currentId, False) = "RESET" Then
                        TextBoxBomTime.Text = ""
                        Dim cmd As New MySqlCommand()
                        Dim sql As String
                        If currentId() > 0 Then

                            Try
                                sql = "UPDATE `" & DBName & "`.`Bomoffer` SET " & _
                                "`eta` = '" & DateTimePickerBom.Text & _
                                "',`Customer` = '" & ComboBoxCustomer.Text & _
                                "',`currency` = '" & ComboBoxBomCurrency.Text & _
                                "',`var1` = '" & TextBoxNameV1.Text & _
                                "',`var2` = '" & TextBoxNameV2.Text & _
                                "',`var3` = '" & TextBoxNameV3.Text & _
                                "',`var4` = '" & TextBoxNameV4.Text & _
                                "',`var5` = '" & TextBoxNameV5.Text & _
                                "',`var6` = '" & TextBoxNameV6.Text & _
                                "',`vol1` = '" & TextBoxQt_V1.Text & _
                                "',`vol2` = '" & TextBoxQt_V2.Text & _
                                "',`vol3` = '" & TextBoxQt_V3.Text & _
                                "',`vol4` = '" & TextBoxQt_V4.Text & _
                                "',`vol5` = '" & TextBoxQt_V5.Text & _
                                "',`vol6` = '" & TextBoxQt_V6.Text & _
                                "',`name` = '" & TextBoxBomName.Text & _
                                "',`RESULT` = '" & ComboBoxBomResult.Text & _
                                "',`TURNOVER` = '" & Replace(Replace(TextBoxBomTurnover.Text, ".", ""), ",", "") & _
                                "',`linkoffer` = '" & Replace(TextBoxBomOfferLink.Text, "\", "\\") & _
                                "',`STATUS` = '" & ComboBoxBomStatus.Text & _
                                "',`note` = '" & TextBoxNote.Text & "' WHERE `bomOffer`.`id` = " & currentId() & " ;"
                                TextBoxBomTurnover.Text = Format(Val(Replace(Replace(TextBoxBomTurnover.Text, ".", ""), ",", "")), "#,0.")
                                cmd = New MySqlCommand(sql, MySqlconnection)
                                cmd.ExecuteNonQuery()
                            Catch ex As Exception
                                MsgBox("Mysql update query error!")
                            End Try

                        End If
                        ButtonBomSave.BackColor = Color.Green
                        OpenSession = False
                        TimerBomRecord.Stop()
                        UpdateTreeBomOffer()
                    Else
                        MsgBox("Section USED " & session("BomOffer", currentId, False))
                    End If
                Else
                    MsgBox("No Bom correctly selected!", MsgBoxStyle.Information)
                    ButtonBomSave.BackColor = Color.Green
                    OpenSession = False
                    TimerBomRecord.Stop()
                    UpdateTreeBomOffer()
                End If
            Else
                MsgBox("Please fill in consistent way the volumes and Quantity")
            End If
        Else
            MsgBox("Cannot close an ongoing offer!")
        End If
    End Sub

    ' is true when value consistent.
    Function checkValue() As Boolean
        If (((TextBoxQt_V1.Text) <> "") = (TextBoxQt_V1.Text <> "")) And _
      (((TextBoxQt_V1.Text) <> "") = (TextBoxQt_V1.Text <> "")) And _
      (((TextBoxQt_V2.Text) <> "") = (TextBoxQt_V2.Text <> "")) And _
      (((TextBoxQt_V3.Text) <> "") = (TextBoxQt_V3.Text <> "")) And _
      (((TextBoxQt_V4.Text) <> "") = (TextBoxQt_V4.Text <> "")) And _
      (((TextBoxQt_V5.Text) <> "") = (TextBoxQt_V5.Text <> "")) And _
      (((TextBoxQt_V6.Text) <> "") = (TextBoxQt_V6.Text <> "")) And _
      IsNumeric(TextBoxQt_V1.Text & "0") And _
      IsNumeric(TextBoxQt_V2.Text & "0") And _
      IsNumeric(TextBoxQt_V3.Text & "0") And _
      IsNumeric(TextBoxQt_V4.Text & "0") And _
      IsNumeric(TextBoxQt_V5.Text & "0") And _
      IsNumeric(TextBoxQt_V6.Text & "0") Then
            checkValue = True
        End If

    End Function

    ' value changed function
    Sub ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
             DateTimePickerBom.TextChanged, _
             ComboBoxBomCurrency.TextChanged, _
             TextBoxNameV1.TextChanged, _
             TextBoxNameV2.TextChanged, _
             TextBoxNameV3.TextChanged, _
             TextBoxNameV4.TextChanged, _
             TextBoxNameV5.TextChanged, _
             TextBoxNameV6.TextChanged, _
             TextBoxQt_V1.TextChanged, _
             TextBoxQt_V2.TextChanged, _
             TextBoxQt_V3.TextChanged, _
             TextBoxQt_V4.TextChanged, _
             TextBoxQt_V5.TextChanged, _
             TextBoxQt_V6.TextChanged, _
             TextBoxBomTurnover.TextChanged, _
             ComboBoxBomResult.TextChanged, _
             ComboBoxBomStatus.TextChanged, _
        TextBoxBomOfferLink.TextChanged, _
        TextBoxBomName.TextChanged, _
   TextBoxNote.TextChanged

        If updating = False Then
            ButtonBomSave.BackColor = Color.OrangeRed
            If OpenSession = True Then
            Else

                If session("BomOffer", currentId, True) = "SET" Then  ' valid session
                    TextBoxBomTime.Text = "30"
                    TimerBomRecord.Interval = 60000
                    TimerBomRecord.Start()
                    OpenSession = True
                Else
                    If currentId() <> 0 Then
                        MsgBox("Section USED " & session("BomOffer", currentId, False))
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TimerBomRecord_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerBomRecord.Tick
        If Val(TextBoxBomTime.Text) > 1 Then
            TextBoxBomTime.Text = Val(TextBoxBomTime.Text) - 1
        Else
            OpenSession = False
            MsgBox("Session Bom expired!")
            TimerBomRecord.Stop()
            session("BomOffer", currentId, False)
            UpdateBomFields()
        End If

    End Sub

    Function currentId() As Long
        currentId = 0
        Try
            currentId = Val(Mid(TreeViewBomList.SelectedNode.Text, 1, InStr(TreeViewBomList.SelectedNode.Text, "-") - 2))
        Catch ex As Exception
        End Try
    End Function

    ' update the field in the form bom
    Sub UpdateBomFields()

        If currentId() <> 0 Then
            updating = True
            updating = True
            Dim rowShow As DataRow()
            DsBomOff.Clear()
            tblBomOff.Clear()
            AdapterBomOff.Fill(DsBomOff, "BomOffer")
            tblBomOff = DsBomOff.Tables("BomOffer")
            rowShow = tblBomOff.Select("id = " & currentId())
            If rowShow.Length > 0 Then
                If rowShow(0).Item("eta").ToString <> "" Then
                    DateTimePickerBom.Text = DateTime.ParseExact(rowShow(0).Item("eta").ToString, "yyyy/MM/dd", CultureInfo.InvariantCulture)
                End If
                ComboBoxCustomer.Text = rowShow(0).Item("customer").ToString
                ComboBoxBomCurrency.Text = rowShow(0).Item("currency").ToString
                TextBoxNameV1.Text = rowShow(0).Item("var1").ToString
                TextBoxNameV2.Text = rowShow(0).Item("var2").ToString
                TextBoxNameV3.Text = rowShow(0).Item("var3").ToString
                TextBoxNameV4.Text = rowShow(0).Item("var4").ToString
                TextBoxNameV5.Text = rowShow(0).Item("var5").ToString
                TextBoxNameV6.Text = rowShow(0).Item("var6").ToString
                TextBoxQt_V1.Text = rowShow(0).Item("vol1").ToString
                TextBoxQt_V2.Text = rowShow(0).Item("vol2").ToString
                TextBoxQt_V3.Text = rowShow(0).Item("vol3").ToString
                TextBoxQt_V4.Text = rowShow(0).Item("vol4").ToString
                TextBoxQt_V5.Text = rowShow(0).Item("vol5").ToString
                TextBoxQt_V6.Text = rowShow(0).Item("vol6").ToString
                TextBoxBomName.Text = rowShow(0).Item("name").ToString
                TextBoxBomOfferLink.Text = rowShow(0).Item("linkoffer").ToString
                TextBoxNote.Text = rowShow(0).Item("note").ToString
                TextBoxBomTurnover.Text = Format(Val(rowShow(0).Item("TURNOVER").ToString), "#,0.")
                ComboBoxBomResult.Text = rowShow(0).Item("RESULT").ToString
                ComboBoxBomStatus.Text = rowShow(0).Item("status").ToString
                ProgressBarBom.Value = PercentComplited(rowShow(0).Item("name").ToString)
            End If
            updating = False

        End If
    End Sub

    Function PercentComplited(ByVal name As String) As Integer
        Dim rowShow As DataRow()
        Dim n As Integer
        Static Dim tblOff As DataTable
        Static Dim DsOff As New DataSet


        If IsNothing(tblOff) Then
            AdapterOff.Fill(DsOff, "Offer")
            tblOff = DsOff.Tables("offer")
        Else

            Try
                rowShow = tblOff.Select("name = '" & name & "' and (( not Brandprice = '' or not AltPrice='') or ( (not BitronpnPrice='') and not bitronpn like 'E*'))")

                n = rowShow.Length
                rowShow = tblOff.Select("name = '" & name & "'")
                If rowShow.Length > 0 Then PercentComplited = Int(100 * n / rowShow.Length)
                DsOff.Dispose()
                tblOff.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Function

    Private Sub TreeViewBomList_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewBomList.BeforeSelect
        If OpenSession = True Then
            If vbYes = MsgBox("Session Open do you want Save?", MsgBoxStyle.YesNo) Then
                ButtonBomSave_Click(Me, e)
            Else
                OpenSession = False
                TimerBomRecord.Stop()
                TextBoxBomTime.Text = ""
                session("BomOffer", currentId, False)
            End If
        End If
    End Sub

    'Private Sub TreeViewBomList_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewBomList.BeforeSelect
    '    session("BomOffer", currentId, False)
    '    TextBoxBomTime.Text = ""
    '    OpenSession = False
    '    ButtonBomSave.BackColor = Color.Green
    'End Sub

    Private Sub TreeViewBomList_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewBomList.GotFocus
        Try
            TreeViewBomList.SelectedNode.ForeColor = Color.Black
        Catch ex As Exception

        End Try

    End Sub


    Sub ImportExcel()


        'open the offer template
        Dim excelApp As New Object
        excelApp = CreateObject("Excel.Application")
        Dim excelWorkbook As Object
        Dim excelSheet As Object
        OpenFileDialog1.InitialDirectory = "c:"
        OpenFileDialog1.Filter = "Access 2007 (*.accdb)|*.accdb"
        OpenFileDialog1.ShowDialog()

        If OpenFileDialog1.CheckFileExists Then

            Try
                excelWorkbook = excelApp.Workbooks.Open(OpenFileDialog1.FileName)
                excelWorkbook.Activate()
                excelSheet = excelWorkbook.Worksheets("BOM")
                excelSheet.Activate()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            excelApp.Visible = True








        End If





    End Sub


    Private Sub ButtonLoadBom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonLoadBom.Click

        Dim replaceBom As Boolean = False

        If CheckBomExist(TextBoxBomName.Text, True) And Not IsNothing(TreeViewBomList.SelectedNode) Then
            MsgBox("The BOM is present in our system, please delete all component before reload!", MsgBoxStyle.Information)
            Application.DoEvents()
            '  OfferComponentDelete(TextBoxBomName.Text)
        Else
            replaceBom = True
        End If


        If replaceBom And currentId() > 0 Then

            Dim problem As Boolean = False, resultCheck As Boolean, firstTime As Boolean = True, qtNumeber As Integer
            Dim bomname As String, nqt As Integer
            Dim cmd As New MySqlCommand()
            Dim sql As String

            OpenFileDialog1.InitialDirectory = "c:"
            OpenFileDialog1.Filter = "Access 2007 (*.accdb)|*.accdb"
            OpenFileDialog1.ShowDialog()

            If OpenFileDialog1.CheckFileExists Then

                Dim conn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & OpenFileDialog1.FileName)
                Try
                    conn.Open()
                Catch ex As Exception
                    MsgBox("Error in connection open:" & ex.Message)
                    Exit Sub
                End Try

                Dim myadapter As New OleDbDataAdapter("SELECT * FROM BOM order by id", conn)
                Dim dtset As New DataSet
                myadapter.Fill(dtset)

                Dim tableBom As DataTable
                tableBom = dtset.Tables("table")
                Try
                    problem = False
                    ' sintax ceck
                    For Each row In tableBom.Rows

                        resultCheck = CheckBrandString(s(row("brand").ToString), s(row("id").ToString))
                        If resultCheck Then

                        Else
                            problem = True
                            MsgBox("Brand field problem: ID " & resultCheck)
                        End If
                        resultCheck = CheckBrandString(s(row("brandALT").ToString), s(row("id").ToString))
                        If resultCheck Then

                        Else
                            problem = True
                            MsgBox("Brand ALT field problem: ID " & s(row("id").ToString))
                        End If

                        nqt = 0
                        nqt = IIf(s(row("qt_v1").ToString) <> "", 1, 0) + _
                                      IIf(s(row("qt_v2").ToString) <> "", 1, 0) + _
                                      IIf(s(row("qt_v3").ToString) <> "", 1, 0) + _
                                      IIf(s(row("qt_v4").ToString) <> "", 1, 0) + _
                                      IIf(s(row("qt_v5").ToString) <> "", 1, 0) + _
                                      IIf(s(row("qt_v6").ToString) <> "", 1, 0)

                        If firstTime = True Then
                            qtNumeber = nqt
                            firstTime = False
                        Else
                            If qtNumeber <> nqt Then
                                MsgBox("some quantity not fill o fill more: id" & s(row("id").ToString))
                                problem = True
                            End If
                        End If


                        If s(row("brand").ToString) = "" And s(row("bitronpn").ToString) = "" And s(row("brandalt").ToString) = "" Then
                            MsgBox("Better fill in at least one brand, brandalt, bitron_pn : id" & s(row("id").ToString), MsgBoxStyle.Information)
                        End If

                        If s(row("description").ToString) = "" Then
                            MsgBox("Description need to fill: id" & s(row("id").ToString))
                            problem = True
                        End If
                        If problem = True Then Exit For

                    Next
                    bomname = TextBoxBomName.Text
                    ' Load in mysql
                    If CheckBomExist(bomname, True) = False Then
                        If problem = False Then
                            For Each row In tableBom.Rows
                                problem = False
                                bomname = TextBoxBomName.Text



                                Try
                                    sql = "INSERT INTO `" & DBName & "`.`offer` (`BitronPN` ,`Name` ,`CustomerPN` ,`description` ,`brand` ,`brandALT`," & _
                                    "`STATUS`,`qt_v1`,`qt_v2`,`qt_v3`,`qt_v4`,`qt_v5`,`qt_v6`,`reference`,`tum`,`noternd`,`notegeneric`,`class`,`type`) VALUES ('" & _
                                   Replace(ReplaceChar(s(row("bitronpn").ToString)), "'", "") & "', '" & _
                                    bomname & "', '" & _
                                     Replace((s(row("customerpn").ToString)), "'", "") & "', '" & _
                                     Replace((s(row("description").ToString)), "'", "") & "', '" & _
                                     Replace(ReplaceChar(s(row("brand").ToString)), "'", "") & "', '" & _
                                     Replace(ReplaceChar(s(row("brandALT").ToString)), "'", "") & "', '" & _
                                    "UNCHECKED" & "', '" & _
                                    (s(row("qt_v1").ToString)) & "', '" & _
                                    (s(row("qt_v2").ToString)) & "', '" & _
                                    (s(row("qt_v3").ToString)) & "', '" & _
                                    (s(row("qt_v4").ToString)) & "', '" & _
                                    (s(row("qt_v5").ToString)) & "', '" & _
                                    (s(row("qt_v6").ToString)) & "', '" & _
                                    Replace((s(row("reference").ToString)), "'", "") & "', '" & _
                                    IIf((s(row("tum").ToString)) <> "", s(row("tum").ToString), "EA") & "', '" & _
                                    Replace((s(row("noternd").ToString)), "'", "") & "', '" & _
                                    Replace((s(row("notegeneric").ToString)), "'", "") & "', '" & _
                                    Replace((s(row("ComponentClass").ToString)), "'", "") & "', '" & _
                                    (s(row("type").ToString)) & "'" & ");"

                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox("Insert Error during import Access DB!")
                                End Try

                            Next
                            MsgBox("Bon imported!")
                        Else
                            MsgBox("Problem heppend Bon not imported!")
                        End If
                    Else
                        MsgBox("Error : Bom name already present in DB!")
                    End If
                Catch ex As Exception
                    MsgBox("Can't access to database!")
                End Try
                myadapter.Dispose()
                conn.Close()
            End If
        End If

    End Sub

    Function StatusComponent(ByVal id As Integer, Optional ByVal Update As Boolean = False) As String
        StatusComponent = "UNCHECKED"
        Dim rowShow As DataRow()
        If Update = True Or IsNothing(tblOff) Then

            Dim tblOff As DataTable
            Dim DsOff As New DataSet
            AdapterOff.Fill(DsOff, "Offer")
            tblOff = DsOff.Tables("offer")
        End If

        rowShow = tblOff.Select("id = " & id)
        If rowShow.Length > 0 Then
            StatusComponent = rowShow(0).Item("status").ToString
        End If
        If StatusComponent = "" Then StatusComponent = "UNCHECKED"
        DsOff.Dispose()
        tblOff.Dispose()
    End Function

    Function CheckBomExistID(ByVal id As Long) As Boolean

        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("offer")
        rowShow = tblOff.Select("id =" & id & "")
        If rowShow.Length > 0 Then
            CheckBomExistID = True
        Else
            CheckBomExistID = False
        End If
        DsOff.Dispose()
        tblOff.Dispose()
    End Function

    Function PurchSign(ByVal id As Long) As String

        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("offer")
        rowShow = tblOff.Select("id =" & id & "")
        If rowShow.Length > 0 Then
            PurchSign = rowShow(0).Item("PurchSign").ToString
        End If
        DsOff.Dispose()
        tblOff.Dispose()
    End Function

    Function RndSign(ByVal id As Long) As String

        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("offer")
        rowShow = tblOff.Select("id =" & id & "")
        If rowShow.Length > 0 Then
            RndSign = rowShow(0).Item("RndSign").ToString
        End If
        DsOff.Dispose()
        tblOff.Dispose()
    End Function

    Function CheckCustomerPriceExist() As Boolean

        Dim rowShow As DataRow()
        Try
            DsCustomerPrice.Clear()
            tblCustomerPrice.Clear()
        Catch ex As Exception

        End Try
        AdapterCustomerPrice.Fill(DsCustomerPrice, "CustomerPrice")
        tblCustomerPrice = DsCustomerPrice.Tables("CustomerPrice")
        rowShow = tblCustomerPrice.Select("customer = '" & TextBoxComponentCustomer.Text & "'")
        If rowShow.Length > 0 Then
            CheckCustomerPriceExist = True
        Else
            CheckCustomerPriceExist = False
        End If

    End Function

    Private Sub ButtonNewBom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNewBom.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String, BOMNAME As String
        BOMNAME = InputBox("Insert the Bom Name Example" & vbCrLf & "Basic Digit")
        If BOMNAME <> "" And Regex.IsMatch(BOMNAME, "^\w+[a-zA-Z0-9]$", RegexOptions.IgnoreCase) Then
            Try
                sql = "INSERT INTO `" & (DBName) & "`.`BOMOFFER` (`Name`,`STATUS`) VALUES ('" & UCase(BOMNAME) & "', 'OPEN');"

                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Insert Error during import Access DB!")
            End Try

            UpdateTreeBomOffer()
        Else
            MsgBox("Name no valid!, can use only alphanumeric char or undescore")
        End If
    End Sub



    ' update the form with component info
    Sub updateComponentList()
        TreeViewComponent.Nodes.Clear()
        Dim sql As String
        Dim rowShow As DataRow()


        Try
            tblOff.Clear()
            DsOff.Clear()

        Catch ex As Exception

        End Try

        Try
            TreeViewComponent.Scrollable = True
            Dim rootNode As TreeNode
            AdapterOff.Fill(DsOff, "Offer")
            tblOff = DsOff.Tables("Offer")
            sql = ("name = '" & TextBoxBomName.Text & IIf(IsNumeric(TextBoxComponentFilter.Text), "' and ( id=" & TextBoxComponentFilter.Text & " or ", "' and ( ") & " ( description like '*" & TextBoxComponentFilter.Text & "*' )) " & _
            IIf(CheckBoxNoPrice.Checked = True, " and   ( ( Brandprice= '') and ( BitronpnPrice= '') and ( AltPrice= ''))", "") & _
            IIf(CheckBoxNO_ALTP.Checked = True, " and   ( ( AltPrice= '') and ( brandalt<> '') )", "") & _
            IIf(CheckBoxClass.Checked = True, " and  ( class= '') ", "") & _
            IIf(CheckEstimed.Checked = True, " and (( not BitronpnPrice= '' and  Brandprice= '' and  AltPrice= '' and bitronpn like 'E*')) ", "") & _
            IIf(CheckBoxNoCustomerPrice.Checked = True, " and ( customerPrice= '' ) ", ""))

            rowShow = tblOff.Select(sql, IIf(CheckBoxBestPrice.Checked, "pricesortcny DESC, ", "") & "status")
            LabelComponentFinded.Text = "Found rows: " & rowShow.Length
            TreeViewComponent.HideSelection = Not TreeViewComponent.HideSelection
            TreeViewComponent.BeginUpdate()
            For Each row In rowShow
                rootNode = New TreeNode(row("id") & " - " & LCase(row("Description")))
                rootNode.NodeFont = New Font("courier new", 10, FontStyle.Regular)

                'TreeViewComponent.ItemHeight = rootNode.NodeFont.GetHeight()
                Dim StatusComp As String = StatusComponent(row("id"))
                If StatusComp = ("PRICE OK") Then rootNode.ForeColor = Color.Green
                If StatusComp = ("R&D CHEKED") Then rootNode.ForeColor = Color.Blue
                If StatusComp = ("PRICE ASKED") Then rootNode.ForeColor = Color.Brown
                If StatusComp = ("UNCHECKED") Then rootNode.ForeColor = Color.Black
                If StatusComp = ("R&D MODIFIED") Then rootNode.ForeColor = Color.Red
                If StatusComp = ("ESTIMED") Then
                    rootNode.ForeColor = Color.Chocolate
                    rootNode.NodeFont = New Font("courier new", 11, FontStyle.Bold)
                End If




                TreeViewComponent.Nodes.Add(rootNode)



                Application.DoEvents()
            Next
            TreeViewComponent.EndUpdate()
            ResumeLayout()
            If TreeViewComponent.Nodes.Count > 0 Then TreeViewComponent.SelectedNode = TreeViewComponent.Nodes(0)
            If TreeViewComponent.Nodes.Count = 0 Then resetComponentField()
            Application.DoEvents()

        Catch ex As Exception
            TreeViewComponent.Enabled = True
            TreeViewComponent.Scrollable = True
            MsgBox("Error in display! " & ex.Message)
        End Try
        ' TreeViewComponent.EndUpdate()


    End Sub

    ' update the form with component info
    Sub updateComponent(ByVal id As Long)
        updatigComponent = True
        TreeViewComponent.HideSelection = False

        priceSensitiveChangesBitronPN = False
        priceSensitiveChangesBrand = False
        priceSensitiveChangesBrandAlt = False

        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")
        rowShow = tblOff.Select("id = " & id)
        If rowShow.Length = 1 Then

            TextBoxComponentBitronPN.Text = rowShow(0).Item("bitronpn").ToString
            TextBoxComponentCustomer.Text = rowShow(0).Item("customerpn").ToString
            TextBoxCompenentGenNote.Text = rowShow(0).Item("notegeneric").ToString
            TextBoxComponentQT.Text = CodQt(Math.Round(Val(volumes(currentId(), "vol1") * Val((rowShow(0).Item("qt_v1").ToString)) + _
                                           volumes(currentId(), "vol2") * Val((rowShow(0).Item("qt_v2").ToString)) + _
                                           volumes(currentId(), "vol3") * Val((rowShow(0).Item("qt_v3").ToString)) + _
                                           volumes(currentId(), "vol4") * Val((rowShow(0).Item("qt_v4").ToString)) + _
                                           volumes(currentId(), "vol5") * Val((rowShow(0).Item("qt_v5").ToString)) + _
                                           volumes(currentId(), "vol6") * Val((rowShow(0).Item("qt_v6").ToString))), 0))
            ComboBoxComponentTHM.Text = rowShow(0).Item("tum").ToString
            ComboBoxClass.Text = rowShow(0).Item("class").ToString
            TextBoxComponentRNDNote.Text = rowShow(0).Item("noternd").ToString
            TextBoxComponentPurchNote.Text = rowShow(0).Item("notepurchasing").ToString
            ComboBoxCompoentType.Text = rowShow(0).Item("type").ToString
            TextBoxComponentPrice.Text = rowShow(0).Item("BrandPrice").ToString
            TextBoxComponentReference.Text = rowShow(0).Item("Reference").ToString
            TextBoxComponentPriceAlt.Text = rowShow(0).Item("AltPrice").ToString
            TextBoxComponentqt1.Text = rowShow(0).Item("qt_v1").ToString
            TextBoxComponentqt2.Text = rowShow(0).Item("qt_v2").ToString
            TextBoxComponentqt3.Text = rowShow(0).Item("qt_v3").ToString
            TextBoxComponentqt4.Text = rowShow(0).Item("qt_v4").ToString
            TextBoxComponentqt5.Text = rowShow(0).Item("qt_v5").ToString
            TextBoxComponentqt6.Text = rowShow(0).Item("qt_v6").ToString
            ' label
            If firstTime = True Then
                Labelqt1.Text = VersionName(currentId, 1, True)
                Labelqt2.Text = VersionName(currentId, 2, False)
                Labelqt3.Text = VersionName(currentId, 3, False)
                Labelqt4.Text = VersionName(currentId, 4, False)
                Labelqt5.Text = VersionName(currentId, 5, False)
                Labelqt6.Text = VersionName(currentId, 6, False)
                firstTime = False
            End If
            TextBoxComponentStatus.Text = rowShow(0).Item("status").ToString
            TextBoxComponentDS.Text = rowShow(0).Item("dslink").ToString
            If Labelqt1.Text = "" Then
                TextBoxComponentqt1.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt1.Enabled = True
            End If

            If Labelqt2.Text = "" Then
                TextBoxComponentqt2.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt2.Enabled = True
            End If

            If Labelqt3.Text = "" Then
                TextBoxComponentqt3.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt3.Enabled = True
            End If

            If Labelqt4.Text = "" Then
                TextBoxComponentqt4.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt4.Enabled = True
            End If

            If Labelqt5.Text = "" Then
                TextBoxComponentqt5.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt5.Enabled = True
            End If

            If Labelqt6.Text = "" Then
                TextBoxComponentqt6.Enabled = False
            ElseIf controlRight("R") >= 2 Then
                TextBoxComponentqt6.Enabled = True
            End If

            TextBoxComponentEstimation.Text = rowShow(0).Item("BitronpnPrice").ToString
            TextBoxComponentCustomerPrice.Text = rowShow(0).Item("customerprice").ToString
            ComboBoxComponentCustomerCurrency.Text = rowShow(0).Item("customercurrency").ToString
            ComboBoxComponentPrice.Text = rowShow(0).Item("Brandcurrency").ToString
            ComboBoxComponentPriceAlt.Text = rowShow(0).Item("AltCurrency").ToString
            ComboBoxComponentEstimation.Text = rowShow(0).Item("BitronpnCurrency").ToString
            LoadComboBoxComponentBrand(UCase(rowShow(0).Item("brand").ToString))
            LoadComboBoxComponentALTBrand(UCase(rowShow(0).Item("brandAlt").ToString))
            TextBoxComponentDescription.Text = UCase(rowShow(0).Item("Description").ToString)
        Else

        End If




        updatigComponent = False
        DsOff.Dispose()
        tblOff.Dispose()
    End Sub

    Function VersionName(ByVal idBom As Integer, ByVal pos As Integer, Optional ByVal update As Boolean = True) As String
        Dim rowShow As DataRow()
        If update Then
            AdapterBomOff.Fill(DsBomOffVer, "BomOffer")
            tblBomOffVer = DsBomOffVer.Tables("BomOffer")
        End If

        rowShow = tblBomOffVer.Select("id = " & idBom)
        If rowShow.Length > 0 Then
            VersionName = rowShow(0).Item("var" & pos)
        End If
    End Function

    Function volumes(ByVal id As Long, ByVal position As String) As Long
        Dim rowShow As DataRow()
        Dim tblBomOff As DataTable
        Dim DsbomOff As New DataSet
        AdapterBomOff.Fill(DsbomOff, "bomOffer")
        tblBomOff = DsbomOff.Tables("bomoffer")
        rowShow = tblBomOff.Select("id = " & id)
        If rowShow.Length > 0 Then volumes = Val(rowShow(0).Item(position).ToString)
    End Function

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged

        If TabControl.SelectedTab.Text = "COMPONENT" Then
            If currentId() > 0 Then
                LabelBomName.Text = TextBoxBomName.Text
                firstTime = True
                updateComponentList()
                For i = 0 To TabControl.TabPages.Item(1).Controls.Count - 1
                    TabControl.TabPages.Item(1).Controls.Item(i).Visible = True
                Next
                If TreeViewComponent.Nodes.Count > 0 Then TreeViewComponent.SelectedNode = TreeViewComponent.Nodes(0)
            Else

                For i = 0 To TabControl.TabPages.Item(1).Controls.Count - 1
                    TabControl.TabPages.Item(1).Controls.Item(i).Visible = False
                Next

                LabelBomName.Text = "Please select a valid bom in the Bom List page!"
                LabelBomName.Visible = True
            End If
            UpdateLiking()
        ElseIf TabControl.SelectedTab.Text = "BRAND" Then
            updateBrandList()
        ElseIf TabControl.SelectedTab.Text = "BOM" Then
            TreeViewComponent_BeforeSelect(Me, e)
            updateBrandList()
        Else

        End If

    End Sub

    Private Sub TreeViewComponent_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewComponent.AfterSelect
        afterSelectComp = True
        TreeViewComponent.Enabled = False
        CurrentComponentID = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, " - "))
        updateComponent(CurrentComponentID)
        FillProposalPrice()
        Application.DoEvents()
        TreeViewComponent.Enabled = True
        Application.DoEvents()
        TreeViewComponent.Focus()
        afterSelectComp = False


    End Sub

    Function LoadComboBoxComponentALTBrand(ByVal s As String) As Boolean
        ComboBoxComponentALTBrand.Items.Clear()
        ComboBoxComponentALTBrand.Text = ""
        Dim j As Integer, i As Integer, brand As String
        LoadComboBoxComponentALTBrand = False
        If s <> "" Then
            Try
                If Mid(s, Len(s), 1) <> ";" Then s = s & ";"
                i = 1
                j = InStr(s, ";", CompareMethod.Text)
                While j > 0
                    brand = Mid(s, i, j - i)
                    If InStr(brand, "[", CompareMethod.Text) > 1 Then
                        If InStr(brand, "]", CompareMethod.Text) > 3 Or ((InStr(brand, "]", CompareMethod.Text) = 3) And _
                                                                         (InStr(Replace(Mid(brand, i + 1, j - 1 - i), " ", ""), "GENERALBRAND", CompareMethod.Text))) Then
                            If InStr(brand, "]", CompareMethod.Text) = Len(brand) Then
                                ComboBoxComponentALTBrand.Items.Add(Replace(Mid(s, i, j - i), ";", ""))
                            Else
                                MsgBox("Error in brand / brandAlt")
                                Exit While
                            End If
                        Else
                            MsgBox("Error in brand / brandAlt")
                            Exit While
                        End If
                    Else
                        MsgBox("Error in brand / brandAlt")
                        Exit While
                    End If
                    i = j
                    j = InStr(j + 1, s, ";", CompareMethod.Text)
                End While

            Catch ex As Exception
                MsgBox("Error in brand / brandAlt")
            End Try
        Else
            LoadComboBoxComponentALTBrand = True
        End If
        If ComboBoxComponentALTBrand.Items.Count > 0 Then ComboBoxComponentALTBrand.Text = ComboBoxComponentALTBrand.Items(0).ToString
    End Function

    Function LoadComboBoxComponentBrand(ByVal s As String) As Boolean
        ComboBoxComponentBrand.Items.Clear()
        ComboBoxComponentBrand.Text = ""
        Dim j As Integer, i As Integer, brand As String
        LoadComboBoxComponentBrand = False
        If s <> "" Then
            Try
                If Mid(s, Len(s), 1) <> ";" Then s = s & ";"
                i = 1
                j = InStr(s, ";", CompareMethod.Text)
                While j > 0
                    brand = Mid(s, i, j - i)
                    If InStr(brand, "[", CompareMethod.Text) > 1 Then
                        If InStr(brand, "]", CompareMethod.Text) > 3 Or ((InStr(brand, "]", CompareMethod.Text) = 3) And _
                                                                         (InStr(Replace(Mid(brand, i + 1, j - 1 - i), " ", ""), "GENERALBRAND", CompareMethod.Text))) Then
                            If InStr(brand, "]", CompareMethod.Text) = Len(brand) Then
                                ComboBoxComponentBrand.Items.Add(Replace(Mid(s, i, j - i), ";", ""))
                            Else
                                MsgBox("Error in brand / brandAlt")
                                Exit While
                            End If
                        Else
                            MsgBox("Error in brand / brandAlt")
                            Exit While
                        End If
                    Else
                        MsgBox("Error in brand / brandAlt")
                        Exit While
                    End If
                    i = j
                    j = InStr(j + 1, s, ";", CompareMethod.Text)
                End While

            Catch ex As Exception
                MsgBox("Error in brand / brandAlt")
            End Try
        Else
            LoadComboBoxComponentBrand = True
        End If
        If ComboBoxComponentBrand.Items.Count > 0 Then ComboBoxComponentBrand.Text = ComboBoxComponentBrand.Items(0).ToString

    End Function

    Private Sub ButtonNewComponent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentAdd.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim descr As String
        descr = InputBox("Insert Description")
        If descr <> "" Then
            Try
                sql = "INSERT INTO `" & DBName & "`.`offer` (`description` ,`Name` ) VALUES ('" & _
                descr & "', '" & TextBoxBomName.Text & "');"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()

                MsgBox("Component insert, please fill all needs info")
                updateComponentList()
                'ValueChangedComponent(Me, e)
            Catch ex As Exception
                MsgBox("Component insert error " & ex.Message)
            End Try
        Else
            MsgBox("No possible insert description empity component!")
        End If

    End Sub

    Private Sub ButtonComponentDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentDelete.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String
        If Not IsNothing(TreeViewComponent.SelectedNode) Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            If vbYes = MsgBox("Do you want delete this component?", MsgBoxStyle.YesNo) Then
                If (session("offer", id, False) = "RESET") Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`offer` WHERE `offer`.`id` = " & id
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Component deleted")
                        updateComponentList()
                        ' ValueChangedComponent(Me, e)
                    Catch ex As Exception
                        MsgBox("Component insert error " & ex.Message)
                    End Try

                Else
                    MsgBox("session open! " & session("offer", id, False))
                End If
            End If
        End If
    End Sub

    Private Sub ButtonBrandDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandDel.Click
        If ComboBoxComponentBrand.Text <> "" Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            If vbYes = MsgBox("Do you want delete this component?", MsgBoxStyle.YesNo) Then
                If (session("offer", id, True) = "SET") Then
                    ComboBoxComponentBrand.Items.RemoveAt(ComboBoxComponentBrand.SelectedIndex)
                    ComboBoxComponentBrand.Text = ""
                    ValueChangedComponent(Me, e)
                    priceSensitiveChangesBrand = True
                Else
                    MsgBox("session open! " & session("offer", id, False))
                    updateComponent(id)
                End If
            End If
        End If
    End Sub


    Private Sub ButtonBrandAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandAdd.Click
        Dim brand As String

        brand = UCase(ReplaceChar(InputBox("Please insert Brand example: ST[1n4148]")))
        If brand <> "" And CheckBrandString(brand, 0) And Not ComboBrandContain(brand) Then
            If Not ComboBrandAltContain(brand) Then
                ComboBoxComponentBrand.Items.Add(ReplaceChar(brand))
                ComboBoxComponentBrand.Text = brand
                ValueChangedComponent(Me, e)
                priceSensitiveChangesBrand = True
            Else
                MsgBox("Item already in Alternative Brand AVL!")
            End If
        End If
    End Sub

    Private Sub ButtonAltAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAltAdd.Click
        Dim brand As String
        brand = InputBox("Please insert Brand example: ST[1n4148]")
        If InStr(brand, "GENERAL", CompareMethod.Text) <= 0 Then
            brand = UCase(ReplaceChar(brand))
        Else

        End If

        If brand <> "" And CheckBrandString(brand, 0) And Not ComboBrandAltContain(brand) Then
            If Not ComboBrandContain(brand) Then
                ComboBoxComponentALTBrand.Items.Add(ReplaceChar(brand))
                ComboBoxComponentALTBrand.Text = brand
                ValueChangedComponent(Me, e)
                Proposal_TextChanged(Me, e)
                priceSensitiveChangesBrandAlt = True
            Else
                MsgBox("Item already in Brand AVL!")
            End If
        End If
    End Sub

    Private Sub ButtonAlTDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlTDel.Click
        If ComboBoxComponentALTBrand.Text <> "" Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            If vbYes = MsgBox("Do you want delete this Brand?", MsgBoxStyle.YesNo) Then
                If (session("offer", id, True) = "SET") Then
                    ComboBoxComponentALTBrand.Items.RemoveAt(ComboBoxComponentALTBrand.SelectedIndex)
                    ComboBoxComponentALTBrand.Text = ""
                    ValueChangedComponent(Me, e)
                    Proposal_TextChanged(Me, e)
                    priceSensitiveChangesBrandAlt = True
                Else
                    MsgBox("session open! " & session("offer", id, False))
                    updateComponent(id)
                End If
            End If
        End If
    End Sub

    Sub TextBoxComponentBitronPN_textchanged() Handles TextBoxComponentBitronPN.TextChanged
        If updatigComponent = False Then
            priceSensitiveChangesBitronPN = True
        End If

    End Sub



    ' value changed function
    Sub ValueChangedComponent(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxCompenentGenNote.TextChanged, _
        TextBoxComponentBitronPN.TextChanged, _
        TextBoxComponentCustomer.TextChanged, _
        TextBoxComponentDescription.TextChanged, _
        TextBoxComponentEstimation.TextChanged, _
        TextBoxComponentPrice.TextChanged, _
        TextBoxComponentCustomerPrice.TextChanged, _
        TextBoxComponentPriceAlt.TextChanged, _
        TextBoxComponentPurchNote.TextChanged, _
        TextBoxComponentRNDNote.TextChanged, _
        TextBoxComponentReference.TextChanged, _
        TextBoxComponentqt1.TextChanged, _
        TextBoxComponentqt2.TextChanged, _
        TextBoxComponentqt3.TextChanged, _
        TextBoxComponentqt4.TextChanged, _
        TextBoxComponentqt5.TextChanged, _
        TextBoxComponentqt6.TextChanged, _
        ComboBoxComponentTHM.TextChanged, _
        ComboBoxClass.TextChanged, _
        TextBoxComponentDS.TextChanged, _
        ComboBoxCompoentType.TextChanged, _
        ComboBoxComponentEstimation.TextChanged, _
        ComboBoxComponentPrice.TextChanged, _
        ComboBoxComponentPriceAlt.TextChanged, _
        ComboBoxComponentCustomerCurrency.TextChanged, _
        TextBoxComponentStatus.TextChanged
        TextBoxComponentBitronPN.Text = Replace(TextBoxComponentBitronPN.Text, "'", "")


        If updatigComponent = False And (sender.name = ComboBoxComponentPriceAlt.Name Or _
            sender.name = ComboBoxComponentPrice.Name Or _
            sender.name = TextBoxComponentPrice.Name Or _
            sender.name = TextBoxComponentPriceAlt.Name Or _
            sender.name = TextBoxComponentEstimation.Name Or _
            sender.name = ComboBoxComponentEstimation.Name) Then
            TextBoxComponentCustomerPrice.Text = ""
            ComboBoxComponentCustomerCurrency.Text = ""
            Price_modified = True
        Else
            Price_modified = False
        End If

        If Not IsNothing(TreeViewComponent.SelectedNode) Then
            ComboBoxComponentStatus.Enabled = True

        End If
        If sender.NAME = "ComboBoxCompoentType" Then
            NoStatusChange = True
        Else
            NoStatusChange = False
        End If


        If updatigComponent = False And Not IsNothing(TreeViewComponent.SelectedNode) Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            ButtonSaveComp.BackColor = Color.OrangeRed
            If sender.name = "ComboBoxCompoentType" Or sender.name = "ComboBoxClass" Then
                onlyType = True
            Else
                onlyType = False
            End If
            If ComponentSession = True Then
            Else

                If session("Offer", id, True) = "SET" Then  ' valid session
                    TextBoxComponentSession.Text = "30"
                    TimerComponents.Interval = 60000
                    TimerComponents.Start()
                    ComponentSession = True

                Else
                    MsgBox("Section USED " & session("Offer", id, False))

                End If
            End If
        End If
    End Sub

    Private Sub ComboBoxComponentStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxComponentStatus.SelectedIndexChanged

        TextBoxComponentStatus.Text = ComboBoxComponentStatus.Text
        If Not IsNothing(TreeViewComponent.SelectedNode) Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            If controlRight("R") >= 2 And (StatusComponent(id) = ("PRICE OK") Or StatusComponent(id) = ("PRICE ASKED")) Then
                If TextBoxComponentStatus.Text = ("R&D CHEKED") Then TextBoxComponentStatus.Text = ("R&D MODIFIED")
            End If
        End If

        If Not IsNothing(TreeViewComponent.SelectedNode) Then
            Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
            If controlRight("U") >= 2 And (StatusComponent(id) = ("UNCHECKED")) Then
                TextBoxComponentStatus.Text = StatusComponent(id)
            End If
        End If

    End Sub

    Private Sub CheckBoxNoPrice_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNoPrice.CheckedChanged
        updateComponentList()
    End Sub

    Private Sub CheckEstimed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckEstimed.CheckedChanged
        updateComponentList()
    End Sub


    Private Sub ButtonBomRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomRemove.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String
        If vbYes = MsgBox("Do you want delete this bom?", MsgBoxStyle.YesNo) Then
            If CheckBomExist(TextBoxBomName.Text, True) Then
                MsgBox("The bom " & TextBoxBomName.Text & " has component. Please delete all component before delete bom ")
            Else
                If (session("bomoffer", currentId, False) = "RESET") Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`bomoffer` WHERE `bomoffer`.`id` = " & currentId()
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Bom deleted!")
                        UpdateTreeBomOffer()
                        Application.DoEvents()
                        ValueChangedComponent(Me, e)
                    Catch ex As Exception
                        MsgBox("Bom deleting error " & ex.Message)
                    End Try

                Else
                    MsgBox("session open! " & session("bomoffer", currentId, False))
                End If
            End If
        End If

    End Sub




    Private Sub ButtonSaveComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSaveComp.Click

        If Not IsNothing(TreeViewComponent.SelectedNode) Then

            If priceSensitiveChangesBitronPN = True Then
                If MsgBox("BitronPN changed, do you want reset the price?", vbYesNo) = vbYes Then
                    TextBoxComponentEstimation.Text = ""
                    ComboBoxComponentEstimation.Text = ""
                    TextBoxComponentCustomerPrice.Text = ""
                    ComboBoxComponentCustomerCurrency.Text = ""
                End If
            End If
            priceSensitiveChangesBitronPN = False

            If priceSensitiveChangesBrand = True Then
                If MsgBox("BitronPN changed, do you want reset the price?", vbYesNo) = vbYes Then
                    TextBoxComponentPrice.Text = ""
                    ComboBoxComponentPrice.Text = ""
                    TextBoxComponentCustomerPrice.Text = ""
                    ComboBoxComponentCustomerCurrency.Text = ""
                End If
            End If
            priceSensitiveChangesBrand = False

            If priceSensitiveChangesBrandAlt = True Then
                If MsgBox("BitronPN changed, do you want reset the price?", vbYesNo) = vbYes Then
                    TextBoxComponentPriceAlt.Text = ""
                    ComboBoxComponentPriceAlt.Text = ""
                    TextBoxComponentCustomerPrice.Text = ""
                    ComboBoxComponentCustomerCurrency.Text = ""
                End If
            End If
            priceSensitiveChangesBrandAlt = False












            If PriceConsistance() = True Then
                Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)

                If DeltaSessionTime("Offer", id) <= 30 And session("Offer", id, False) = "RESET" Then



                    ' price association

                    If controlRight("U") >= 2 Then PriceAssociation()


                    ' price customer            
                    Dim BaseSelected As String = ""
                    If InStr(ComboBoxComponentProposalCustomer.Text, "BrandAlt") > 0 Then

                        BaseSelected = "BRANDALT"
                    ElseIf InStr(ComboBoxComponentProposalCustomer.Text, "BitronPN") > 0 And Mid(TextBoxComponentBitronPN.Text, 1, 1) <> "E" Then

                        BaseSelected = "ORCAD"
                    ElseIf InStr(ComboBoxComponentProposalCustomer.Text, "Brand") > 0 Then

                        BaseSelected = "BRAND"
                    Else

                    End If

                    TextBoxComponentSession.Text = ""
                    Dim cmd As New MySqlCommand()
                    Dim sql As String
                    Try
                        sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                        "`notegeneric` = '" & Replace(TextBoxCompenentGenNote.Text, "'", "") & _
                        "',`bitronpn` = '" & Replace(TextBoxComponentBitronPN.Text, "'", "") & _
                        "',`customerpn` = '" & Replace(TextBoxComponentCustomer.Text, "'", "") & _
                        "',`description` = '" & UCase(Replace(TextBoxComponentDescription.Text, "'", "")) & _
                        "',`BitronpnPrice` = '" & IIf(TextBoxComponentEstimation.Text <> "", Replace(Math.Round(Val(TextBoxComponentEstimation.Text), 5), ",", "."), "") & _
                        "',`Brandprice` = '" & IIf(TextBoxComponentPrice.Text <> "", Replace(Math.Round(Val(TextBoxComponentPrice.Text), 5), ",", "."), "") & _
                        "',`AltPrice` = '" & IIf(TextBoxComponentPriceAlt.Text <> "", Replace(Math.Round(Val(TextBoxComponentPriceAlt.Text), 5), ",", "."), "") & _
                        "',`customerPrice` = '" & IIf(Price_modified, "", IIf(TextBoxComponentCustomerPrice.Text <> "", Replace(Math.Round(Val(TextBoxComponentCustomerPrice.Text), 5), ",", "."), "")) & _
                        "',`notepurchasing` = '" & Replace(TextBoxComponentPurchNote.Text, "'", "") & _
                        "',`qt_v1` = '" & TextBoxComponentqt1.Text & _
                        "',`qt_v2` = '" & TextBoxComponentqt2.Text & _
                        "',`qt_v3` = '" & TextBoxComponentqt3.Text & _
                        "',`qt_v4` = '" & TextBoxComponentqt4.Text & _
                        "',`qt_v5` = '" & TextBoxComponentqt5.Text & _
                        "',`qt_v6` = '" & TextBoxComponentqt6.Text & _
                        "',`Brandcurrency` = '" & ComboBoxComponentPrice.Text & _
                        "',`class` = '" & ComboBoxClass.Text & _
                        "',`brandalt` = '" & UCase(Replace(StringBrandAlt(), "'", "")) & _
                        "',`DSlink` = '" & Replace((TextBoxComponentDS.Text), "\", "\\") & _
                        "',`brand` = '" & UCase(Replace(StringBrand(), "'", "")) & _
                        IIf(controlRight("U") >= 2, "',`purchSign` = '" & PurchSign(id) & " - " & CreAccount.strUserName & "[" & date_to_string(Today) & "]", "") & _
                        IIf(controlRight("R") >= 2, "',`rndSign` = '" & RndSign(id) & " - " & CreAccount.strUserName & "[" & date_to_string(Today) & "]", "") & _
                        "',`AltCurrency` = '" & ComboBoxComponentPriceAlt.Text & _
                        "',`CustomerCurrency` = '" & IIf(Price_modified, "", ComboBoxComponentCustomerCurrency.Text) & _
                        "',`BitronpnCurrency` = '" & ComboBoxComponentEstimation.Text & _
                        "',`reference` = '" & Replace(TextBoxComponentReference.Text, "'", "") & _
                        "',`tum` = '" & ComboBoxComponentTHM.Text & _
                        "',`CustomerPriceBased` = '" & BaseSelected & _
                        "',`type` = '" & ComboBoxCompoentType.Text & _
                        "',`status` = '" & StatusCalc() & _
                        "',`noternd` = '" & Replace(TextBoxComponentRNDNote.Text, "'", "") & "' WHERE `Offer`.`id` = " & id & " ;"

                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()


                        If TextBoxComponentStatus.Text = ("PRICE OK") Then TreeViewComponent.SelectedNode.ForeColor = Color.Green
                        If TextBoxComponentStatus.Text = ("R&D CHEKED") Then TreeViewComponent.SelectedNode.ForeColor = Color.Blue
                        If TextBoxComponentStatus.Text = ("PRICE ASKED") Then TreeViewComponent.SelectedNode.ForeColor = Color.Brown
                        If TextBoxComponentStatus.Text = ("UNCHECKED") Then TreeViewComponent.SelectedNode.ForeColor = Color.Black
                        If TextBoxComponentStatus.Text = ("R&D MODIFIED") Then TreeViewComponent.SelectedNode.ForeColor = Color.Red




                    Catch ex As Exception
                        MsgBox("Mysql update query error!" & ex.Message)
                    End Try

                    ButtonSaveComp.BackColor = Color.Green
                    ComponentSession = False
                    TimerComponents.Stop()

                Else
                    MsgBox("Section USED " & session("Offer", currentId, False))
                End If
            Else
                MsgBox("Please fill in Price and the related currency!")
            End If
        End If

        TreeViewComponent.Focus()
        If CheckBoxNoCustomerPrice.Checked = True Then
            TreeViewComponent.SelectedNode = TreeViewComponent.SelectedNode.NextNode
            ButtonComponentCustomerTRF_Click(Me, e)
        End If
    End Sub

    Function StatusCalc() As String
        Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)

        If controlRight("R") >= 2 Then

            If (Not onlyType) And (StatusComponent(id) = ("PRICE OK") Or StatusComponent(id) = ("PRICE ASKED")) And ComponentSession And _
            TextBoxComponentCustomerPrice.Text & TextBoxComponentPrice.Text & TextBoxComponentPriceAlt.Text & TextBoxComponentEstimation.Text <> "" And NoStatusChange = False Then
                If vbYes = MsgBox("Do you want change in R&D MODIFIED? if chenges is related to price is mandatory modify the status!", vbYesNo) Then
                    StatusCalc = ("R&D MODIFIED")
                    TextBoxComponentStatus.Text = ("R&D MODIFIED")
                Else
                    StatusCalc = TextBoxComponentStatus.Text
                End If
            Else
                StatusCalc = TextBoxComponentStatus.Text
            End If

        ElseIf controlRight("U") >= 2 Then

            If TextBoxComponentCustomerPrice.Text & TextBoxComponentCustomerPrice.Text & TextBoxComponentPriceAlt.Text & TextBoxComponentEstimation.Text <> "" And _
                TextBoxComponentStatus.Text = "R&D CHEKED" And StatusComponent(id) <> "R&D CHEKED" Then
                If vbYes = MsgBox("Need delete all price for perform this operation! do you want do it?", MsgBoxStyle.YesNo) Then
                    TextBoxComponentCustomerPrice.Text = ""
                    TextBoxComponentEstimation.Text = ""
                    TextBoxComponentEstimation.Text = ""
                    TextBoxComponentEstimation.Text = ""
                    ComboBoxComponentPrice.Text = ""
                    ComboBoxComponentPriceAlt.Text = ""
                    ComboBoxComponentEstimation.Text = ""
                    ComboBoxComponentCustomerCurrency.Text = ""
                    StatusCalc = "R&D CHEKED"
                    TextBoxComponentStatus.Text = "R&D CHEKED"
                Else
                    StatusCalc = StatusComponent(id)
                    TextBoxComponentStatus.Text = StatusComponent(id)
                End If

            ElseIf TextBoxComponentEstimation.Text & TextBoxComponentPrice.Text & TextBoxComponentPriceAlt.Text <> "" And TextBoxComponentStatus.Text <> ("PRICE OK") Then
                If vbYes = MsgBox("do you want approve the price and move the status in PRICE OK?", MsgBoxStyle.YesNo) Then
                    StatusCalc = "PRICE OK"
                    TextBoxComponentStatus.Text = "PRICE OK"
                Else
                    StatusCalc = TextBoxComponentStatus.Text
                End If
            ElseIf TextBoxComponentEstimation.Text & TextBoxComponentPrice.Text & TextBoxComponentPriceAlt.Text = "" And TextBoxComponentStatus.Text = ("PRICE OK") Then
                StatusCalc = StatusComponent(id)
                MsgBox("Need write some price for apply ""PRICE OK"" ")
            Else
                StatusCalc = TextBoxComponentStatus.Text
            End If
            If InStr(TextBoxComponentBitronPN.Text, "Price_Est_", CompareMethod.Text) > 0 Or (Mid(TextBoxComponentBitronPN.Text, 1, 1) = "E" And _
               TextBoxComponentPrice.Text & TextBoxComponentEstimation.Text = "") Then StatusCalc = "ESTIMED"
        Else
            ' no acces for this kind of user
        End If

    End Function





    Function PriceConsistance() As Boolean
        PriceConsistance = True
        If (ComboBoxComponentPrice.Text <> "" And TextBoxComponentPrice.Text = "") Or _
        (ComboBoxComponentPrice.Text = "" And TextBoxComponentPrice.Text <> "") Or _
        (ComboBoxComponentPriceAlt.Text <> "" And TextBoxComponentPriceAlt.Text = "") Or _
        (ComboBoxComponentPriceAlt.Text = "" And TextBoxComponentPriceAlt.Text <> "") Or _
        (ComboBoxComponentEstimation.Text <> "" And TextBoxComponentEstimation.Text = "") Or _
        (ComboBoxComponentEstimation.Text = "" And TextBoxComponentEstimation.Text <> "") Or _
        (ComboBoxComponentCustomerCurrency.Text = "" And TextBoxComponentCustomerPrice.Text <> "") Or _
        (ComboBoxComponentCustomerCurrency.Text <> "" And TextBoxComponentCustomerPrice.Text = "") Or _
        (TextBoxComponentDescription.Text = "" And ComboBoxComponentALTBrand.Text = "" And TextBoxComponentBitronPN.Text = "" And ComboBoxComponentBrand.Text = "" And TextBoxComponentPrice.Text <> "") Or _
        (ComboBoxComponentALTBrand.Text = "" And TextBoxComponentPriceAlt.Text <> "") Or _
        (TextBoxComponentPrice.Text <> "" And ComboBoxComponentBrand.Text = "") Or _
        (TextBoxComponentEstimation.Text <> "" And TextBoxComponentBitronPN.Text = "") Or _
        (TextBoxComponentPriceAlt.Text <> "" And ComboBoxComponentALTBrand.Text = "") Then
            PriceConsistance = False
        End If


    End Function

    Function StringBrand() As String
        StringBrand = ""
        For i = 1 To ComboBoxComponentBrand.Items.Count
            StringBrand = StringBrand & ComboBoxComponentBrand.Items(i - 1).ToString & ";"
        Next
    End Function

    Function StringBrandAlt() As String
        StringBrandAlt = ""
        For i = 1 To ComboBoxComponentALTBrand.Items.Count
            StringBrandAlt = StringBrandAlt & ComboBoxComponentALTBrand.Items(i - 1).ToString & ";"
        Next
    End Function

    Private Sub ButtonComponentOpenDs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentOpenDs.Click
        Try
            Process.Start(ParameterTable("DataSheetOfferFolder") & TextBoxComponentDS.Text)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ButtonComponetAddDs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponetAddDs.Click
        OpenFileDialog1.Filter = "All File (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        If Not IsNothing(TreeViewComponent.SelectedNode) And TextBoxComponentDescription.Text <> "" Then
            If OpenFileDialog1.CheckFileExists And InStr(OpenFileDialog1.FileName, "'", CompareMethod.Text) <= 0 Then
                Try
                    MkDir(ParameterTable("DataSheetOfferFolder"))
                Catch ex As Exception

                End Try
                Try
                    FileCopy(OpenFileDialog1.FileName, ParameterTable("DataSheetOfferFolder") & ReplaceChar(TreeViewComponent.SelectedNode.Text) & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")))
                    MsgBox("File copied!   " & vbCrLf & vbCrLf & ParameterTable("DataSheetOfferFolder") & ReplaceChar(TreeViewComponent.SelectedNode.Text) & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")), MsgBoxStyle.Information)
                    TextBoxComponentDS.Text = ReplaceChar(TreeViewComponent.SelectedNode.Text) & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, "."))

                Catch ex As Exception
                    MsgBox("File exist or others file problem!" & ex.Message)
                End Try
            End If
        Else
            MsgBox("Please select a component!")
        End If
    End Sub


    Sub FillProposalPrice()
        ComboBoxComponentProposalPFP.BackColor = Color.White
        ComboBoxComponentProposalBrand.Items.Clear()
        ComboBoxComponentProposalALT.Items.Clear()
        ComboBoxComponentProposalPFP.Items.Clear()
        ComboBoxComponentProposalCustomer.Items.Clear()
        Dim rowShow As DataRow()
        If ComboBoxComponentBrand.Text <> "" Or ComboBoxComponentALTBrand.Text <> "" Or TextBoxComponentBitronPN.Text <> "" Then

            Dim tblBrand As DataTable
            Dim DsBrand As New DataSet
            Dim Ass As Integer


            ' Search Brand corrispondence
            AdapterBrand.Fill(DsBrand, "Brand")
            tblBrand = DsBrand.Tables("Brand")
            rowShow = tblBrand.Select("Brand = '" & ReplaceCharBrandOC(ComboBoxComponentBrand.Text) & "'")
            For Each row In rowShow
                ComboBoxComponentProposalBrand.Items.Add(row("Price") & " - " & row("Currency") & "  Qt/y " & Math.Round(Val(row("qt")), 5) & "  Date " & row("date"))
                ComboBoxComponentProposalBrand.Text = ComboBoxComponentProposalBrand.Items(0).ToString
            Next

            ' Search ALT Brand corrispondence
            rowShow = tblBrand.Select("Brand = '" & ReplaceCharBrandOC(ComboBoxComponentALTBrand.Text) & "'")
            For Each row In rowShow
                ComboBoxComponentProposalALT.Items.Add(row("Price") & " - " & row("Currency") & "  Qt/y " & Math.Round(Val(row("qt")), 5) & "  Date " & row("date"))
                ComboBoxComponentProposalALT.Text = ComboBoxComponentProposalALT.Items(0).ToString
            Next

            ' Search PFP corrispondence
            If InStr(TextBoxComponentBitronPN.Text, "price_est_", CompareMethod.Text) <= 0 Then
                rowShow = tblPfp.Select("pfidf = '" & Replace(ReplaceChar(TextBoxComponentBitronPN.Text), "E", "") & "'  and  pedfi = '0'", "pfpan desc,  pfpaf desc, pedin")
                Ass = 0
                For Each row In rowShow
                    If (Val(row("pfpan")) + Val(row("pfpaf"))) <> 0 Then
                        ComboBoxComponentProposalPFP.Items.Add(ConvPrice(row("pepre"), row("pelot")) & " - " & row("peval") & "  Date " & row("pedin"))
                        ComboBoxComponentProposalPFP.Text = ComboBoxComponentProposalPFP.Items(0).ToString
                        Ass = Ass + IIf(Val(row("pfpan")) <> 0, Val(row("pfpan")), Val(row("pfpaf")))
                        If Ass >= 100 Then Exit For
                    End If
                Next
                If (Ass < 100 Or Ass > 100) And rowShow.Length > 0 Then
                    ComboBoxComponentProposalPFP.Items.Clear()
                    'MsgBox("Error in PFP recognize of p/n " & Replace(TextBoxComponentBitronPN.Text, "E", ""))
                    ComboBoxComponentProposalPFP.BackColor = Color.Red
                Else
                    ComboBoxComponentProposalPFP.BackColor = Color.White
                End If
                tblBrand.Dispose()
                DsBrand.Dispose()
            Else
                ComboBoxComponentProposalPFP.Items.Clear()
            End If
        End If

        ' Search Customer Price corrispondence
        Try
            DsCustomerPrice.Clear()
            tblCustomerPrice.Clear()
        Catch ex As Exception

        End Try

        AdapterCustomerPrice.Fill(DsCustomerPrice, "CustomerPrice")
        tblCustomerPrice = DsCustomerPrice.Tables("CustomerPrice")

        rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and PartNumber = '" & Replace((TextBoxComponentBitronPN.Text), "E", "") & "'", "date")
        For Each row In rowShow
            ComboBoxComponentProposalCustomer.Items.Add(row("Price") & " - " & row("currency") & "  Date " & row("date"))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        Next
        rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and PartNumber = '" & collectCombo(ComboBoxComponentBrand) & "'", "date")
        For Each row In rowShow
            ComboBoxComponentProposalCustomer.Items.Add(row("Price") & " - " & row("currency") & "  Date " & row("date"))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        Next

        rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and PartNumber = '" & (TextBoxComponentCustomer.Text) & "'", "date")
        For Each row In rowShow
            ComboBoxComponentProposalCustomer.Items.Add(row("Price") & " - " & row("currency") & "  Date " & row("date"))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        Next

        rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and PartNumber = '" & collectCombo(ComboBoxComponentALTBrand) & "'", "date")
        For Each row In rowShow
            ComboBoxComponentProposalCustomer.Items.Add(row("Price") & " - " & row("currency") & "  Date " & row("date"))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        Next


        Dim price As String = TextBoxComponentPrice.Text
        Dim currency As String = ComboBoxComponentPrice.Text
        If Val(price) > 0 And ComboBoxCustomer.Text <> "" And price <> "" And currency <> "" Then
            ComboBoxComponentProposalCustomer.Items.Add(Math.Round(Val(price) * (1 + PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency) / 100), 5) & " - " & currency & "  Brand Percentage increment: " & Math.Round(PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency), 1))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        End If

        price = TextBoxComponentPriceAlt.Text
        currency = ComboBoxComponentPriceAlt.Text
        If price <> "" And ComboBoxCustomer.Text <> "" And price <> "" And currency <> "" Then
            ComboBoxComponentProposalCustomer.Items.Add(Math.Round(Val(price) * (1 + IIf(InStr(ComboBoxComponentALTBrand.Text, "packaging", CompareMethod.Text), 2, 1) * PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency) / 100), 5) & " - " & currency & "  BrandAlt Percentage increment: " & Math.Round(IIf(InStr(ComboBoxComponentALTBrand.Text, "packaging", CompareMethod.Text), 2, 1) * PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency), 1))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        End If

        price = TextBoxComponentEstimation.Text
        currency = ComboBoxComponentEstimation.Text
        If price <> "" And ComboBoxCustomer.Text <> "" And price <> "" And currency <> "" Then
            ComboBoxComponentProposalCustomer.Items.Add(Math.Round(Val(price) * (1 + PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency) / 100), 5) & " - " & currency & "  BitronPN Percentage increment: " & Math.Round(PerCnvCustomerPrice(ComboBoxCustomer.Text, price, currency), 1))
            ComboBoxComponentProposalCustomer.Text = ComboBoxComponentProposalCustomer.Items(0).ToString
        End If

        LoadComboBoxOrcad(GetOrcadSupplier(Replace(ReplaceChar(Replace(TextBoxComponentBitronPN.Text, "PRICE_EST_", "")), "E", "")))
        Application.DoEvents()
    End Sub


    Function PerCnvCustomerPrice(ByVal cust As String, ByVal price As String, ByVal cur As String) As Double
        Try
            Dim percCust As Double = Val(Mid(ParameterTable(cust), 1, InStr(ParameterTable(cust), ";") - 1))
            Dim maxUsdVal As Double = Val(Mid(ParameterTable(cust), 1 + InStr(ParameterTable(cust), ";")))


            If usd(price, cur) >= maxUsdVal Then
                PerCnvCustomerPrice = 0
            Else
                PerCnvCustomerPrice = (percCust * (maxUsdVal - usd(price, cur)) / maxUsdVal)
            End If
        Catch ex As Exception

        End Try


    End Function


    Function ConvPrice(ByVal Price As String, ByVal batch As String) As String

        If batch = "TH" Then
            ConvPrice = Math.Round(Val(Val(Price) / 1000), 5)
        ElseIf batch = "EA" Then
            ConvPrice = Math.Round(Val(Price), 5)
        ElseIf batch = "HA" Then
            ConvPrice = Math.Round(Val(Price) / 100, 5)
        Else
            ConvPrice = 0
            MsgBox("Conversion error for batch " & batch)
        End If

    End Function

    Private Sub ButtonComponentPrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentPriceTFT.Click
        If ComboBoxComponentProposalBrand.Text <> "" Then
            ComboBoxComponentPrice.Text = Mid(ComboBoxComponentProposalBrand.Text, InStr(ComboBoxComponentProposalBrand.Text, "-") + 2, 3)
            TextBoxComponentPrice.Text = Mid(ComboBoxComponentProposalBrand.Text, 1, InStr(ComboBoxComponentProposalBrand.Text, "-") - 2)
        End If
    End Sub


    Private Sub ButtonComponentALT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentAltTFT.Click
        If ComboBoxComponentProposalALT.Text <> "" Then
            ComboBoxComponentPriceAlt.Text = Mid(ComboBoxComponentProposalALT.Text, InStr(ComboBoxComponentProposalALT.Text, "-") + 2, 3)
            TextBoxComponentPriceAlt.Text = Mid(ComboBoxComponentProposalALT.Text, 1, InStr(ComboBoxComponentProposalALT.Text, "-") - 2)
        End If
    End Sub


    Private Sub ButtonComponentBitronPnTRF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentBitronPnTRF.Click
        If ComboBoxComponentProposalPFP.Text <> "" Then
            ComboBoxComponentEstimation.Text = Mid(ComboBoxComponentProposalPFP.Text, InStr(ComboBoxComponentProposalPFP.Text, "-") + 2, 3)
            TextBoxComponentEstimation.Text = Mid(ComboBoxComponentProposalPFP.Text, 1, InStr(ComboBoxComponentProposalPFP.Text, "-") - 2)
        End If
    End Sub

    Private Sub ButtonComponentCustomerTRF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentCustomerTRF.Click
        If ComboBoxComponentProposalCustomer.Text <> "" Then
            ComboBoxComponentCustomerCurrency.Text = Mid(ComboBoxComponentProposalCustomer.Text, InStr(ComboBoxComponentProposalCustomer.Text, "-") + 2, 3)
            TextBoxComponentCustomerPrice.Text = Mid(ComboBoxComponentProposalCustomer.Text, 1, InStr(ComboBoxComponentProposalCustomer.Text, "-") - 2)
        End If
        ButtonSaveComp.Focus()
    End Sub

    Sub PriceAssociation()
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim rowShow As DataRow()
        Try
            DsBrand.Clear()
            tblBrand.Clear()
        Catch ex As Exception

        End Try

        Try
            DsBrand.Clear()
            DsCustomerPrice.Clear()
        Catch ex As Exception

        End Try



        AdapterBrand.Fill(DsBrand, "Brand")
        tblBrand = DsBrand.Tables("Brand")

        AdapterCustomerPrice.Fill(DsCustomerPrice, "CustomerPrice")
        tblCustomerPrice = DsCustomerPrice.Tables("CustomerPrice")



        If CheckBox1.Checked = False Then

            ' brand 
            If TextBoxComponentPrice.Text <> "" And ComboBoxComponentBrand.Text <> "" And InStr(ComboBoxComponentBrand.Text, "TBD") <= 0 And InStr(ComboBoxComponentBrand.Text, "PACKAGE") <= 0 Then
                rowShow = tblBrand.Select("Brand = '" & ReplaceCharBrandOC(ComboBoxComponentBrand.Text) & "'")
                If rowShow.Length = 0 Then
                    If vbYes = MsgBox("Do you want make association beetwen :" & vbCrLf & vbCrLf & ComboBoxComponentBrand.Text & vbCrLf & vbCrLf & " and price : " & vbCrLf & vbCrLf & Math.Round(Val(TextBoxComponentPrice.Text), 5) & " " & ComboBoxComponentPrice.Text, MsgBoxStyle.YesNo, "Brand Association") Then

                        Try
                            sql = "INSERT INTO `" & DBName & "`.`Brand` (`brand` ,`price`,`qt`,`date`,`buyer`,`currency` ) VALUES ('" & _
                            ReplaceCharBrandOC(ComboBoxComponentBrand.Text) & "', '" & _
                            Replace(Math.Round(Val(TextBoxComponentPrice.Text), 5), ",", ".") & "', '" & _
                            DecQt(TextBoxComponentQT.Text) & "', '" & _
                            date_to_string(Now) & "', '" & _
                            CreAccount.strUserName & "', '" & _
                            ComboBoxComponentPrice.Text & "');"
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()

                            MsgBox("Association enstabilished!")

                        Catch ex As Exception
                            MsgBox("Component insert error " & ex.Message)
                        End Try
                    End If
                ElseIf rowShow.Length = 1 Then
                    If ComboBoxComponentPrice.Text = rowShow(0).Item("CURRENCY").ToString And _
                          rowShow(0).Item("Price").ToString = Math.Round(Val(TextBoxComponentPrice.Text), 5) Then
                    Else
                        MsgBox("One Brand-Price part already present in DB, please edit it in Brand section if you want change price or add others price quantity. Association not established", MsgBoxStyle.Information)
                    End If
                ElseIf rowShow.Length > 1 Then
                    MsgBox("More of one Brand-Price part already present in DB, please edit it in Brand section if you want change price or add others price quantity. Association not established", MsgBoxStyle.Information)
                End If
            End If


            ' alternative 
            If TextBoxComponentPriceAlt.Text <> "" And ComboBoxComponentALTBrand.Text <> "" And InStr(ComboBoxComponentALTBrand.Text, "TBD") <= 0 And InStr(ComboBoxComponentALTBrand.Text, "PACKAGE") <= 0 Then
                rowShow = tblBrand.Select("Brand = '" & ReplaceCharBrandOC(ComboBoxComponentALTBrand.Text) & "' and currency ='" & ComboBoxComponentPriceAlt.Text & _
                                  "' and price = '" & Math.Round(Val(TextBoxComponentPriceAlt.Text), 5) & "'")
                If rowShow.Length = 0 Then
                    If vbYes = MsgBox("Do you want make association beetwen :" & vbCrLf & vbCrLf & ComboBoxComponentALTBrand.Text & vbCrLf & vbCrLf & " and price : " & _
                                      vbCrLf & vbCrLf & Math.Round(Val(TextBoxComponentPriceAlt.Text), 5) & " " & ComboBoxComponentPriceAlt.Text, MsgBoxStyle.YesNo, "Alternative Brand Association") Then

                        Try
                            sql = "INSERT INTO `" & DBName & "`.`Brand` (`brand` ,`price`,`qt`,`date`,`buyer`,`currency`) VALUES ('" & _
                            ReplaceCharBrandOC(ComboBoxComponentALTBrand.Text) & "', '" & _
                            Replace(Math.Round(Val(TextBoxComponentPriceAlt.Text), 5), ",", ".") & "', '" & _
                            DecQt(TextBoxComponentQT.Text) & "', '" & _
                            date_to_string(Now) & "', '" & _
                            CreAccount.strUserName & "', '" & _
                            ComboBoxComponentPriceAlt.Text & "');"
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                            MsgBox("Association enstabilished!")
                        Catch ex As Exception
                            MsgBox("Component insert error " & ex.Message)
                        End Try

                    End If
                End If
            End If


        End If

        ' price customer            
        Dim ref As String = "", BaseSelected As String = ""
        If InStr(ComboBoxComponentProposalCustomer.Text, "BrandAlt") > 0 Then
            If ref = "" Then ref = collectCombo(ComboBoxComponentALTBrand)
            BaseSelected = "BRANDALT"
        ElseIf InStr(ComboBoxComponentProposalCustomer.Text, "BitronPN") > 0 And InStr(TextBoxComponentBitronPN.Text, "E", CompareMethod.Text) <= 0 Then
            ref = TextBoxComponentBitronPN.Text
            BaseSelected = "BITRON_PN"
        ElseIf InStr(ComboBoxComponentProposalCustomer.Text, "Brand") > 0 Then
            ref = collectCombo(ComboBoxComponentBrand)
            BaseSelected = "BRAND"
        Else
            If Replace(TextBoxComponentBitronPN.Text, "E", "") <> "" And Mid(TextBoxComponentBitronPN.Text, 1, 1) <> "E" Then
                ref = Replace(TextBoxComponentBitronPN.Text, "E", "")
                BaseSelected = "BITRON_PN"
            ElseIf collectCombo(ComboBoxComponentALTBrand) <> "" Then
                If ref = "" Then ref = collectCombo(ComboBoxComponentALTBrand)
                BaseSelected = "BRANDALT"

            ElseIf collectCombo(ComboBoxComponentBrand) <> "" Then
                ref = collectCombo(ComboBoxComponentBrand)
                BaseSelected = "BRAND"
            Else
                MsgBox("Please fill at least of field for correct association of customer price! ")
            End If

        End If



            Dim price As String = TextBoxComponentCustomerPrice.Text
            Dim currency As String = ComboBoxComponentCustomerCurrency.Text
            Dim perc As Single = 0
            Try
                perc = Val(Mid(ParameterTable(ComboBoxCustomer.Text), 1, InStr(ParameterTable(ComboBoxCustomer.Text), ";") - 1))
            Catch ex As Exception

            End Try

            If price <> "" And currency <> "" And ref <> "" And InStr(ref, "TBD") <= 0 And InStr(ref, "PACKAGE") <= 0 And perc > 0 Then

                rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and currency ='" & currency & _
                                  "' and price = '" & Math.Round(Val(TextBoxComponentCustomerPrice.Text), 5) & "' and partnumber ='" & ref & "'")
                If rowShow.Length = 0 Then
                    If vbYes = MsgBox("Do you want make association beetwen :" & vbCrLf & vbCrLf & ref & vbCrLf & vbCrLf & " and price : " & _
                                      vbCrLf & vbCrLf & Math.Round(Val(price), 5) & " " & currency, MsgBoxStyle.YesNo, "Customer Price Association") Then


                        rowShow = tblCustomerPrice.Select("customer = '" & ComboBoxCustomer.Text & "' and partnumber ='" & ref & "'")
                        If rowShow.Length > 0 Then
                            If vbYes = MsgBox("Do you want remove others occurence of this p/n and this customer already associated?", MsgBoxStyle.YesNo) Then
                                Try
                                    ComboBoxComponentProposalCustomer.Items.Clear()
                                    sql = "DELETE FROM `" & DBName & "`.`customerPrice` WHERE `customerPrice`.`partnumber` = '" & ref & "'"
                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()
                                    MsgBox("delete done!")
                                Catch ex As Exception
                                    MsgBox("Component delete error " & ex.Message)
                                End Try
                            End If
                        End If

                        Try
                            sql = "INSERT INTO `" & DBName & "`.`customerPrice` (`customer` ,`price`,`currency`,`date`,`partnumber`) VALUES ('" & _
                            ComboBoxCustomer.Text & "', '" & _
                            Replace(Math.Round(Val(price), 5), ",", ".") & "', '" & _
                            currency & "', '" & _
                            date_to_string(Now) & "', '" & _
                            ref & "');"

                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                            MsgBox("Association enstabilished!")
                        Catch ex As Exception
                            MsgBox("Component insert error " & ex.Message)
                        End Try

                    End If
                End If
            End If

    End Sub

    Function collectCombo(ByVal c As ComboBox) As String
        collectCombo = ""
        For i = 0 To c.Items.Count - 1
            collectCombo = c.Items(i).ToString & ";" & collectCombo
        Next
    End Function

    Private Sub CheckBoxNoCustomerPrice_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNoCustomerPrice.CheckedChanged
        updateComponentList()
    End Sub

    Function DecQt(ByVal qt As String) As String
        DecQt = Val(qt)
        If InStr(qt, "K", CompareMethod.Text) >= 1 Then DecQt = Math.Round(Val(qt) * 1000, 0)
        If InStr(qt, "M", CompareMethod.Text) >= 1 Then DecQt = Math.Round(Val(qt) * 1000000, 0)
    End Function


    Function CodQt(ByVal qt As String) As String
        CodQt = Val(qt)
        If Int(Val(qt) / 1000) >= 1 Then CodQt = Math.Round(Val(qt) / 1000, 1) & "K"
        If Int(Val(qt) / 1000000) >= 1 Then CodQt = Math.Round(Val(qt) / 1000000, 1) & "M"

    End Function

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        FormInfoOffer.Show()
    End Sub

    ' update the form with component info
    Sub updateBrandList()

        TreeViewBrand.Nodes.Clear()
        Dim rowShow As DataRow()

        Try
            tblBrand.Clear()
            DsBrand.Clear()
        Catch ex As Exception

        End Try

        Dim rootNode As TreeNode
        AdapterBrand.Fill(DsBrand, "brand")
        tblBrand = DsBrand.Tables("brand")

        rowShow = tblBrand.Select("(not buyer = 'SystemLiking') and (brand like " & IIf(TextBoxBrandRefresh.Text = "", "'*'", "'*" & OnlyChar(TextBoxBrandRefresh.Text) & "*'") & ")", "brand")

        For Each row In rowShow
            If CheckBoxBrandBomOnly.Checked = True Then
                If ComboBrandContain(row("brand").ToString) Or ComboBrandAltContain(row("brand").ToString) Then
                    rootNode = New TreeNode(row("id") & " - " & LCase(row("Brand").ToString))
                    TreeViewBrand.Nodes.Add(rootNode)
                End If
            Else
                rootNode = New TreeNode(row("id").ToString & " - " & LCase(row("Brand").ToString))
                TreeViewBrand.Nodes.Add(rootNode)
            End If

        Next
        If TreeViewBrand.Nodes.Count > 0 Then TreeViewBrand.SelectedNode = TreeViewBrand.Nodes(0)
        updatigBrand = False
        TreeViewBrand.EndUpdate()
        FillcomboBrandSupplier()
    End Sub

    ' update the form with component info id
    Sub updateBrandListId()

        TreeViewBrand.Nodes.Clear()
        Dim rowShow As DataRow()

        Try
            tblBrand.Clear()
            DsBrand.Clear()
        Catch ex As Exception

        End Try

        Dim rootNode As TreeNode
        AdapterBrand.Fill(DsBrand, "brand")
        tblBrand = DsBrand.Tables("brand")

        rowShow = tblBrand.Select("(not buyer = 'SystemLiking') and (id = " & TextBoxBrandRefresh.Text & ")", "brand")

        For Each row In rowShow
            If CheckBoxBrandBomOnly.Checked = True Then
                If ComboBrandContain(row("brand").ToString) Or ComboBrandAltContain(row("brand").ToString) Then
                    rootNode = New TreeNode(row("id") & " - " & LCase(row("Brand").ToString))
                    TreeViewBrand.Nodes.Add(rootNode)
                End If
            Else
                rootNode = New TreeNode(row("id").ToString & " - " & LCase(row("Brand").ToString))
                TreeViewBrand.Nodes.Add(rootNode)
            End If

        Next
        If TreeViewBrand.Nodes.Count > 0 Then TreeViewBrand.SelectedNode = TreeViewBrand.Nodes(0)
        updatigBrand = False
        TreeViewBrand.EndUpdate()
        FillcomboBrandSupplier()
    End Sub
    Function ComboBrandContain(ByVal s As String) As Boolean
        ComboBrandContain = False
        For i = 0 To ComboBoxComponentBrand.Items.Count - 1
            If ComboBoxComponentBrand.Items(i).ToString = s Then ComboBrandContain = True
        Next i
    End Function
    Function ComboBrandAltContain(ByVal s As String) As Boolean
        For i = 0 To ComboBoxComponentALTBrand.Items.Count - 1
            If ComboBoxComponentALTBrand.Items(i).ToString = s Then ComboBrandAltContain = True
        Next i
    End Function


    Private Sub CheckBoxOpen_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOpen.CheckedChanged
        If updatigComponent = False Then UpdateTreeBomOffer()
    End Sub


    Sub resetComponentField()
        TextBoxCompenentGenNote.Text = ""
        TextBoxComponentBitronPN.Text = ""
        TextBoxComponentCustomer.Text = ""
        TextBoxComponentDescription.Text = ""
        TextBoxComponentEstimation.Text = ""
        TextBoxComponentPrice.Text = ""
        TextBoxComponentCustomerPrice.Text = ""
        TextBoxComponentPriceAlt.Text = ""
        TextBoxComponentPurchNote.Text = ""
        TextBoxComponentRNDNote.Text = ""
        TextBoxComponentReference.Text = ""
        TextBoxComponentqt1.Text = ""
        TextBoxComponentqt2.Text = ""
        TextBoxComponentqt3.Text = ""
        TextBoxComponentqt4.Text = ""
        TextBoxComponentqt5.Text = ""
        TextBoxComponentqt6.Text = ""
        ComboBoxComponentTHM.Text = ""
        TextBoxComponentDS.Text = ""
        ComboBoxCompoentType.Text = ""
        ComboBoxComponentEstimation.Text = ""
        ComboBoxComponentPrice.Text = ""
        ComboBoxComponentPriceAlt.Text = ""
        ComboBoxComponentCustomerCurrency.Text = ""
        TextBoxComponentStatus.Text = ""
        ComboBoxComponentProposalALT.Items.Clear()
        ComboBoxComponentProposalCustomer.Items.Clear()
        ComboBoxComponentProposalBrand.Items.Clear()
        ComboBoxComponentProposalPFP.Items.Clear()
        ComboBoxComponentBrand.Items.Clear()
        ComboBoxComponentALTBrand.Items.Clear()
    End Sub

    Private Sub CheckBoxOrderByDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOrderByDate.CheckedChanged
        UpdateTreeBomOffer()
    End Sub

    Private Sub CheckBoxBrandBomOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBrandBomOnly.CheckedChanged
        updateBrandList()
    End Sub

    Private Sub TreeViewBrand_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewBrand.AfterSelect

        updatigBrand = True
        Dim rowShow As DataRow()

        Try
            tblBrand.Clear()
            DsBrand.Clear()
        Catch ex As Exception

        End Try
        Dim id As Long = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, "-") - 2)

        AdapterBrand.Fill(DsBrand, "brand")
        tblBrand = DsBrand.Tables("brand")

        rowShow = tblBrand.Select("id =" & id, "brand")

        If rowShow.Length = 1 Then
            TextBoxBrandBuyer.Text = rowShow(0).Item("buyer").ToString
            If (rowShow(0).Item("Date").ToString) <> "" Then
                DateTimePickerBrand.Value = string_to_date(rowShow(0).Item("Date").ToString)
            Else
                DateTimePickerBrand.Text = ""
            End If
            TextBoxBrandOC.Text = rowShow(0).Item("Brand").ToString
            TextBoxBrandPrice.Text = rowShow(0).Item("Price").ToString
            ComboBoxBrandSupplier.Text = UCase(rowShow(0).Item("Supplier").ToString)
            ComboBoxBrandCurrency.Text = rowShow(0).Item("currency").ToString
            TextBoxBrandLink.Text = rowShow(0).Item("OfferLink").ToString
            TextBoxBrandQuantity.Text = IIf(rowShow(0).Item("qt").ToString <> "", CodQt(DecQt(rowShow(0).Item("qt").ToString)), "")
        Else
            MsgBox("Inconsistance for id Brand")
        End If

        updatigBrand = False

    End Sub



    Private Sub ButtonBrandAddOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandAddOffer.Click
        OpenFileDialog1.InitialDirectory = ParameterTable("SupplierOffer")
        OpenFileDialog1.Filter = "All File (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.CheckFileExists And InStr(OpenFileDialog1.FileName, "'", CompareMethod.Text) <= 0 And TextBoxBrandOC.Text <> "" And Not IsNothing(TreeViewBrand.SelectedNode) Then
            Try
                MkDir(ParameterTable("SupplierOffer"))
            Catch ex As Exception

            End Try
            Try
                FileCopy(OpenFileDialog1.FileName, ParameterTable("SupplierOffer") & TreeViewBrand.SelectedNode.Text & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")))
                MsgBox("File copied!   " & vbCrLf & vbCrLf & ParameterTable("SupplierOffer") & TreeViewBrand.SelectedNode.Text & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, ".")), MsgBoxStyle.Information)
                TextBoxBrandLink.Text = TreeViewBrand.SelectedNode.Text & Mid(OpenFileDialog1.FileName, InStrRev(OpenFileDialog1.FileName, "."))

            Catch ex As Exception
                MsgBox("File exist or others file problem!" & ex.Message)
            End Try
        End If
    End Sub



    Private Sub ButtonBrandUpdate_Click_1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandUpdate.Click

        If Not IsNothing(TreeViewBrand.SelectedNode) Then
            If IsNumeric(Replace(Replace(TextBoxBrandQuantity.Text, "M", ""), "K", "")) And CheckBrandString(TextBoxBrandOC.Text, TextBoxBrandOC.Text) _
               And ((TextBoxBrandPrice.Text <> "" And ComboBoxBrandCurrency.Text <> "") Or (ComboBoxBrandCurrency.Text = "" And TextBoxBrandPrice.Text = "")) Then
                Dim id As Long = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, "-") - 2)

                If DeltaSessionTime("Brand", id) <= 30 And session("Brand", id, False) = "RESET" Then

                    TextBoxBrandSession.Text = ""
                    Dim cmd As New MySqlCommand()
                    Dim sql As String
                    Try
                        sql = "UPDATE `" & DBName & "`.`Brand` SET " & _
                        "`buyer` = '" & Replace(IIf(TextBoxBrandBuyer.Text <> "", CreAccount.strUserName, ""), "'", "") & _
                        "',`Date` = '" & date_to_string(DateTimePickerBrand.Value) & _
                        "',`Brand` = '" & ReplaceCharBrandOC(TextBoxBrandOC.Text) & _
                        "',`Price` = '" & IIf(TextBoxBrandPrice.Text <> "", Replace(Math.Round(Val(TextBoxBrandPrice.Text), 5), ",", "."), "") & _
                        "',`Supplier` = '" & Replace(UCase(ComboBoxBrandSupplier.Text), "'", "") & _
                        "',`currency` = '" & ComboBoxBrandCurrency.Text & _
                        "',`OfferLink` = '" & Replace(TextBoxBrandLink.Text, "\", "\\") & _
                        "',`qt` = '" & IIf(TextBoxBrandQuantity.Text <> "", DecQt(TextBoxBrandQuantity.Text), "") & _
                        "' WHERE `Brand`.`id` = " & id & " ;"

                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox("Mysql update query error!" & ex.Message)
                    End Try

                    ButtonBrandUpdate.BackColor = Color.Green
                    BrandSession = False
                    TimerBrand.Stop()

                Else
                    MsgBox("Section USED " & session("Brand", id, False))
                End If
            Else
                MsgBox("Please fill in Price Qt and the related currency!")
            End If
        End If

    End Sub


    Private Sub TextBoxBrandOC_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxBrandOC.TextChanged
        TextBoxBrandOC.Text = ReplaceCharBrandOC(TextBoxBrandOC.Text)
    End Sub

    Private Sub TextBoxBrandSupplier_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ComboBoxBrandSupplier.Text = UCase(ReplaceCharBrandOC(ComboBoxBrandSupplier.Text))
    End Sub

    Private Sub ButtonBrandRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBrandRemove.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        If Not IsNothing(TreeViewBrand.SelectedNode) Then
            Dim id As Long = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, "-") - 2)
            If vbYes = MsgBox("Do you want delete this Brand[OC]?", MsgBoxStyle.YesNo) Then
                If (session("brand", id, False) = "RESET") Then
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`Brand` WHERE `Brand`.`id` = " & id
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        MsgBox("Brand deleted!")
                        updateBrandList()
                        Application.DoEvents()
                        'ValueChangedBrand(Me, e)
                    Catch ex As Exception
                        MsgBox("Bom deleting error " & ex.Message)
                    End Try
                Else
                    MsgBox("session open! " & session("brand", id, False))
                End If
            End If
        Else
            MsgBox("Select a valid item")
        End If

    End Sub

    Private Sub TextBoxBrandQuantity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxBrandQuantity.LostFocus
        TextBoxBrandQuantity.Text = CodQt(DecQt(TextBoxBrandQuantity.Text))
    End Sub

    Private Sub ValueChangedBrand(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxBrandPrice.TextChanged, _
    TextBoxBrandOC.TextChanged, _
    TextBoxBrandBuyer.TextChanged, _
    TextBoxBrandLink.TextChanged, _
    TextBoxBrandQuantity.TextChanged, _
    ComboBoxBrandSupplier.TextChanged, _
    ComboBoxBrandCurrency.TextChanged

        If updatigBrand = False And Not IsNothing(TreeViewBrand.SelectedNode) Then
            Dim id As Long = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, "-") - 2)
            ButtonBrandUpdate.BackColor = Color.OrangeRed
            If BrandSession = True Then
            Else

                If session("Brand", id, True) = "SET" Then  ' valid session
                    TextBoxBrandSession.Text = "30"
                    TimerBrand.Interval = 60000

                    TimerBrand.Start()
                    BrandSession = True
                Else
                    MsgBox("Section USED " & session("Brand", id, False))

                End If
            End If
        End If

    End Sub


    Private Sub ButtonBrandNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandNew.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim descr As String
        descr = ReplaceCharBrandOC(InputBox("Insert Brand[OrderingCode] : " & vbCrLf & vbCrLf & "For Example nxp[bav99]"))
        If CheckBrandString(descr, descr) Then
            Try
                sql = "INSERT INTO `" & DBName & "`.`Brand` (`Brand` ,`buyer` ,`date`) VALUES ('" & _
                descr & "', '" & CreAccount.strUserName & "', '" & date_to_string(Today) & "' ); "
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()

                MsgBox("Brand insert, please fill all needs info")
                updateBrandList()

            Catch ex As Exception
                MsgBox("Brand insert error !" & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub


    Private Sub TextBoxNameV1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV1.TextChanged
        TextBoxNameV1.Text = ReplaceChar(TextBoxNameV1.Text)
    End Sub

    Private Sub TextBoxNameV2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV2.TextChanged
        TextBoxNameV2.Text = ReplaceChar(TextBoxNameV2.Text)
    End Sub

    Private Sub TextBoxNameV3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV3.TextChanged
        TextBoxNameV3.Text = ReplaceChar(TextBoxNameV3.Text)
    End Sub

    Private Sub TextBoxNameV4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV4.TextChanged
        TextBoxNameV4.Text = ReplaceChar(TextBoxNameV4.Text)
    End Sub

    Private Sub TextBoxNameV5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV5.TextChanged
        TextBoxNameV5.Text = ReplaceChar(TextBoxNameV5.Text)
    End Sub

    Private Sub TextBoxNameV6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNameV6.TextChanged
        TextBoxNameV6.Text = ReplaceChar(TextBoxNameV6.Text)
    End Sub

    Private Sub TextBoxNote_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNote.TextChanged
        TextBoxNote.Text = Replace(TextBoxNote.Text, "'", "")
        TextBoxNote.Select(TextBoxNote.Text.Length, 0)
    End Sub

    Private Sub TextBoxComponentDescription_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxComponentDescription.TextChanged
        TextBoxComponentDescription.Text = Replace(TextBoxComponentDescription.Text, "'", "")
    End Sub


    Private Sub ButtonBrandOfferOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandOfferOpen.Click
        Try
            Process.Start(ParameterTable("SupplierOffer") & TextBoxBrandLink.Text)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TextBoxBrandQuantity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxBrandQuantity.TextChanged
        If IsNumeric(Replace(Replace(TextBoxBrandQuantity.Text, "M", ""), "K", "")) Then

        Else
            TextBoxBrandQuantity.Text = ""
        End If

    End Sub



    Private Sub TreeViewBrand_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewBrand.BeforeSelect
        If updatigBrand = False And Not IsNothing(TreeViewBrand.SelectedNode) Then
            CurrentBrandID = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, " - "))
            If BrandSession Then
                If vbYes = MsgBox("Session open on the last brand, do you want save it?", MsgBoxStyle.YesNo) Then
                    ButtonBrandUpdate_Click_1(Me, e)
                Else
                    session("brand", CurrentBrandID, False)
                    ButtonBrandUpdate.BackColor = Color.Green
                    BrandSession = False
                    TimerBrand.Stop()
                End If
            End If
        End If
    End Sub

    Private Sub TreeViewComponent_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeViewComponent.BeforeSelect
        If updatigComponent = False And Not IsNothing(TreeViewComponent.SelectedNode) Then
            CurrentComponentID = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, " - "))
            If ComponentSession = True Then
                If vbYes = MsgBox("Session Open do you want Save?", MsgBoxStyle.YesNo) Then
                    ButtonSaveComp_Click(Me, e)
                Else
                    ComponentSession = False
                    ButtonSaveComp.BackColor = Color.Green
                    TimerComponents.Stop()
                    TextBoxComponentSession.Text = ""
                    session("Offer", CurrentComponentID, False)
                End If
            End If
        End If
    End Sub

    Sub EnableBrandControl()

        If controlRight("U") >= 2 Then
            For i = 0 To TabControl.TabPages.Item(2).Controls.Count - 1
                TabControl.TabPages.Item(2).Controls.Item(i).Enabled = True
            Next

            TextBoxBrandLink.Enabled = False
            TextBoxBrandOC.Enabled = False
        End If
    End Sub




    Private Sub Proposal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        ComboBoxComponentBrand.SelectedIndexChanged, _
        ComboBoxComponentALTBrand.SelectedIndexChanged

        Application.DoEvents()
        If afterSelectComp = False Then FillProposalPrice()
        UpdateLiking()
    End Sub

    Private Sub Proposal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxComponentBitronPN.TextChanged

        Application.DoEvents()
        If afterSelectComp = False Then FillProposalPrice()
        UpdateLiking()
    End Sub

    Sub OfferComponentDelete(ByVal name As String)
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim rowShow As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")
        rowShow = tblOff.Select("name = " & name & "'")
        For Each row In rowShow
            If (session("offer", row("id").ToString, False) = "RESET") Then
                Try
                    sql = "DELETE FROM `" & DBName & "`.`offer` WHERE `offer`.`name` = '" & name & "'"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    Application.DoEvents()

                Catch ex As Exception
                    MsgBox("Bom deleting error " & ex.Message)
                End Try
                MsgBox("Component deleted!")
            Else
                MsgBox("session open! " & session("offer", row("id").ToString, False))
            End If
        Next
        DsOff.Dispose()
        tblOff.Dispose()

    End Sub



    Private Sub ButtonComponentRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentRefresh.Click
        updateComponentList()
    End Sub



    Private Sub ButtonExportXML_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonExportXML.Click

        SaveFileDialog1.FileName = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\" & TextBoxBomName.Text & ".xls"
        Dim tblOff As DataTable

        Dim AdapterOff As New MySqlDataAdapter("SELECT * FROM offer where name = '" & TextBoxBomName.Text & "'", MySqlconnection)
        Dim DsOff As New DataSet
        AdapterOff.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")

        If SaveFileDialog1.FileName <> "" And DialogResult.OK = SaveFileDialog1.ShowDialog() Then
            Try
                tblOff.WriteXml(SaveFileDialog1.FileName, True)
                Process.Start(SaveFileDialog1.FileName)
            Catch ex As Exception

            End Try

        End If

    End Sub


    Private Sub TextBoxComponentBitronPN_textchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxComponentBitronPN.TextChanged
        sender.Text = UCase(sender.Text)
        sender.SelectionStart = Len(sender.Text)
    End Sub




    Private Sub TextBoxComponentPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
           TextBoxComponentPrice.TextChanged, _
           TextBoxComponentCustomerPrice.TextChanged, _
           TextBoxComponentEstimation.TextChanged, _
           TextBoxComponentPriceAlt.TextChanged, _
        TextBoxComponentqt1.TextChanged, _
        TextBoxComponentqt2.TextChanged, _
        TextBoxComponentqt3.TextChanged, _
        TextBoxComponentqt4.TextChanged, _
        TextBoxComponentqt5.TextChanged, _
        TextBoxComponentqt6.TextChanged, _
        TextBoxBrandPrice.TextChanged

        If InStr(sender.Text, "'", CompareMethod.Text) > 0 Then
            sender.Text = Replace(sender.Text, ",", ".")
            sender.SelectionStart = Len(sender.Text)
        End If

        If Not IsNumeric(sender.Text) Then
            If Len(sender.Text) > 0 Then MsgBox("Please insert validd number!", MsgBoxStyle.Information)
            sender.Text = ""
        End If
    End Sub

    Private Sub TimerComponents_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerComponents.Tick
        Dim id As Long = Mid(TreeViewComponent.SelectedNode.Text, 1, InStr(TreeViewComponent.SelectedNode.Text, "-") - 2)
        If Val(TextBoxComponentSession.Text) > 1 Then
            TextBoxComponentSession.Text = Val(TextBoxComponentSession.Text) - 1
        Else
            ComponentSession = False
            MsgBox("Session Components expired!")
            TimerComponents.Stop()
            session("Offer", id, False)
            updateComponentList()
        End If
    End Sub

    Private Sub TimerBrand_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerBrand.Tick
        Dim id As Long = Mid(TreeViewBrand.SelectedNode.Text, 1, InStr(TreeViewBrand.SelectedNode.Text, "-") - 2)
        If Val(TextBoxBrandSession.Text) > 1 Then
            TextBoxBrandSession.Text = Val(TextBoxBrandSession.Text) - 1
        Else
            BrandSession = False
            MsgBox("Session Brand expired!")
            TimerBrand.Stop()
            session("brand", id, False)
            updateBrandList()
        End If
    End Sub

    Private Sub ComboBoxComponentALTBrand_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxComponentALTBrand.KeyDown, _
    ComboBoxComponentBrand.KeyDown
        If Not sender.FindStringExact(sender.Text) < 0 Then
            LastValueBrandCombo = sender.Text
        Else
            LastValueBrandCombo = ""
        End If
    End Sub

    Private Sub ComboBoxComponentALTBrand_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
        ComboBoxComponentALTBrand.KeyUp, _
        ComboBoxComponentBrand.KeyUp
        If LastValueBrandCombo = "" Then
        Else
            sender.Text = LastValueBrandCombo
        End If
    End Sub


    ' calculate the best proposal for price
    Private Sub ButtonBomBest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomBest.Click


        If TextBoxBomName.Text <> "" Then
            Dim rowShow As DataRow()
            Dim tblOff As DataTable
            Dim DsOff As New DataSet
            Dim bestCurrency As String, BestPrice As String, DeltaAVL As String, selectedPrice As String, selectedCurrency As String
            Dim cmd As New MySqlCommand(), DeltaOrcad As String = ""
            Dim sql As String, Selected As String
            Dim DisableErrorClass As Integer = 0
            typefilled = ""
            ' update the component type qt
            SumBomQt(TextBoxBomName.Text, "SMD_T")
            SumBomQt(TextBoxBomName.Text, "SMD_B")
            SumBomQt(TextBoxBomName.Text, "AX")
            SumBomQt(TextBoxBomName.Text, "RD")
            SumBomQt(TextBoxBomName.Text, "P")
            SumBomQt(TextBoxBomName.Text, "FP")
            SumBomQt(TextBoxBomName.Text, "")



            TextBoxBomV1.Text = ""
            TextBoxBomV2.Text = ""
            TextBoxBomV3.Text = ""
            TextBoxBomv4.Text = ""
            TextBoxBomV5.Text = ""
            TextBoxBomv6.Text = ""


            AdapterOff.Fill(DsOff, "Offer")
            tblOff = DsOff.Tables("offer")
            rowShow = tblOff.Select("name ='" & TextBoxBomName.Text & "'")
            For Each row In rowShow


                If NoInfoBomBest = False Then
                    If TextBoxNameV1.Text <> "" And row("qt_v1").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                    If TextBoxNameV2.Text <> "" And row("qt_v2").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                    If TextBoxNameV3.Text <> "" And row("qt_v3").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                    If TextBoxNameV4.Text <> "" And row("qt_v4").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                    If TextBoxNameV5.Text <> "" And row("qt_v5").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                    If TextBoxNameV6.Text <> "" And row("qt_v6").ToString = "" Then MsgBox("Quantity not fill for this p/n: " & row("id").ToString)
                End If

                If row("class").ToString = "" Then
                    If DisableErrorClass = 0 Then MsgBox("Class missing for id: " & row("id").ToString)
                    If DisableErrorClass = 0 Then

                        If MsgBox("Want end this error?", vbYesNo) = vbYes Then DisableErrorClass = 1
                    ElseIf DisableErrorClass = 1 Then

                    End If

                End If

                If row("type").ToString = "" Then MsgBox("Type missing for id: " & row("id").ToString)
                Try
                    DeltaOrcad = (GetOrcadSupplier(Replace(ReplaceChar(row("bitronpn").ToString), "E", "")))
                Catch ex As Exception

                End Try

                BestPrice = row("BrandPrice").ToString
                bestCurrency = row("Brandcurrency").ToString
                DeltaAVL = ""
                Selected = "BRAND"
                If Val(BestPrice) = 0 Or (Val(row("AltPrice").ToString) > 0 And Val(BestPrice) > 0) And (usd(row("BrandPrice").ToString, row("Brandcurrency").ToString) > usd(row("AltPrice").ToString, row("AltCurrency").ToString)) Then
                    BestPrice = Math.Round(Val(row("AltPrice").ToString), 5)
                    bestCurrency = row("AltCurrency").ToString
                    DeltaAVL = row("brandAlt").ToString
                    Selected = "BRANDALT"
                End If

                If Val(BestPrice) = 0 Or (Val(BestPrice) > 0 And Val(row("BitronpnPrice").ToString) > 0 And usd(BestPrice, bestCurrency) > usd(row("BitronpnPrice").ToString, row("BitronpnCurrency").ToString)) Then
                    BestPrice = Math.Round(Val(row("BitronpnPrice").ToString), 5)
                    bestCurrency = row("BitronpnCurrency").ToString
                    DeltaAVL = row("bitronpn").ToString
                    Selected = "ORCAD"
                End If

                'If BestPrice <> 0 Then
                '    If row("BrandPrice").ToString <> "" Then
                '        BestPrice = usd(BestPrice, bestCurrency)

                '    Else
                '        BestPrice = usd(BestPrice, bestCurrency)

                '    End If

                '    If bestCurrency = "USD" Then BestPrice = usd(BestPrice, "USD")
                '    If bestCurrency = "EUR" Then BestPrice = eur(BestPrice, "USD")
                '    If bestCurrency = "CNY" Then BestPrice = cny(BestPrice, "USD")
                'Else

                'End If

                If Val(BestPrice) <> 0 Then
                    selectedCurrency = bestCurrency
                    selectedPrice = BestPrice
                Else
                    selectedCurrency = row("Brandcurrency").ToString
                    selectedPrice = row("BrandPrice").ToString
                End If


                'If NoInfoBomBest = False Then If Val(selectedPrice) = 0 Then MsgBox("Selcted Price 0 for: " & row("id").ToString & " - " & row("description").ToString)


                If TextBoxNameV1.Text <> "" Then TextBoxBomV1.Text = Str(Math.Round(Val(TextBoxBomV1.Text) + Val(row("qt_v1").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))
                If TextBoxNameV2.Text <> "" Then TextBoxBomV2.Text = Str(Math.Round(Val(TextBoxBomV2.Text) + Val(row("qt_v2").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))
                If TextBoxNameV3.Text <> "" Then TextBoxBomV3.Text = Str(Math.Round(Val(TextBoxBomV3.Text) + Val(row("qt_v3").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))
                If TextBoxNameV4.Text <> "" Then TextBoxBomv4.Text = Str(Math.Round(Val(TextBoxBomv4.Text) + Val(row("qt_v4").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))
                If TextBoxNameV5.Text <> "" Then TextBoxBomV5.Text = Str(Math.Round(Val(TextBoxBomV5.Text) + Val(row("qt_v5").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))
                If TextBoxNameV6.Text <> "" Then TextBoxBomv6.Text = Str(Math.Round(Val(TextBoxBomv6.Text) + Val(row("qt_v6").ToString) * ConvertPriceCurency(ComboBoxBomCurrency.Text, selectedPrice, selectedCurrency), 2))

                Try

                    If ComboBoxBomCurrency.Text = "USD" Then BestPrice = usd(BestPrice, bestCurrency)
                    If ComboBoxBomCurrency.Text = "EUR" Then BestPrice = eur(BestPrice, bestCurrency)
                    If ComboBoxBomCurrency.Text = "CNY" Then BestPrice = cny(BestPrice, bestCurrency)
                    bestCurrency = ComboBoxBomCurrency.Text



                    sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                    "`DeltaPrice` = '" & Selected & " --> " & IIf(Val(BestPrice) <> 0, Math.Round(Val(BestPrice), 5), "") & _
                    "',`DeltaPriceCurrency` = '" & IIf(Val(BestPrice) <> 0, bestCurrency, "") & _
                    "',`DeltaAVL` = '" & IIf(Val(BestPrice) <> 0, DeltaAVL, "") & _
                    "',`DeltaOrcad` = '" & IIf(DeltaOrcad <> "", DeltaOrcad, "=""""") & _
                    "',`PriceSortCny` = '" & cny(BestPrice, bestCurrency) & _
                    "' WHERE `Offer`.`id` = " & row("id").ToString & " ;"


                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()


                Catch ex As Exception
                    MsgBox("Best update error !" & ex.Message, MsgBoxStyle.Critical)
                End Try


            Next
            MsgBox("Best Bom Price Elaborated!")
            DsOff.Dispose()
            tblOff.Dispose()
        End If
    End Sub

    Function ConvertPriceCurency(ByVal targetCurrency As String, ByVal Price As String, ByVal Currency As String) As Double
        If targetCurrency = "USD" Then ConvertPriceCurency = usd(Price, Currency)
        If targetCurrency = "EUR" Then ConvertPriceCurency = eur(Price, Currency)
        If targetCurrency = "CNY" Then ConvertPriceCurency = cny(Price, Currency)
        If targetCurrency = "JPY" Then ConvertPriceCurency = jpy(Price, Currency)
    End Function

    Private Sub ButtonBomSaveCurrency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomSaveCurrency.Click
        If IsNumeric(TextBoxBomUSD_CNY.Text) Then ParameterTableWrite("USD_CNY", Math.Round(Val(Replace(TextBoxBomUSD_CNY.Text, ",", ".")), 5))
        If IsNumeric(TextBoxBomEUR_USD.Text) Then ParameterTableWrite("EUR_USD", Math.Round(Val(Replace(TextBoxBomEUR_USD.Text, ",", ".")), 5))
        If IsNumeric(TextBoxBomEUR_JPY.Text) Then ParameterTableWrite("EUR_JPY", Math.Round(Val(Replace(TextBoxBomEUR_JPY.Text, ",", ".")), 5))
    End Sub

    Function usd(ByVal value As String, ByVal cur As String) As Double
        usd = 0
        If cur = "EUR" Then usd = (Val(value) * Val(TextBoxBomEUR_USD.Text))
        If cur = "CNY" Then usd = (Val(value) / Val(TextBoxBomUSD_CNY.Text))
        If cur = "USD" Then usd = (Val(value))
        If cur = "JPY" Then usd = (Val(value) * Val(TextBoxBomEUR_USD.Text) / Val(TextBoxBomEUR_JPY.Text))
        If usd = 0 And Val(value) <> 0 And cur <> "" Then MsgBox("Currency don't rcognize " & cur)

    End Function

    Function eur(ByVal value As String, ByVal cur As String) As Double
        eur = 0
        If cur = "EUR" Then eur = (Val(value))
        If cur = "USD" Then eur = (Val(value) / Val(TextBoxBomEUR_USD.Text))
        If cur = "CNY" Then eur = (Val(value) / Val(TextBoxBomUSD_CNY.Text) / Val(TextBoxBomEUR_USD.Text))
        If cur = "JPY" Then eur = (Val(value) / Val(TextBoxBomEUR_JPY.Text))
        If eur = 0 And Val(value) <> 0 And cur <> "" Then MsgBox("Currency don't rcognize " & cur)
    End Function

    Function cny(ByVal value As String, ByVal cur As String) As Double
        cny = 0
        If cur = "CNY" Then cny = (Val(value))
        If cur = "USD" Then cny = (Val(value) * Val(TextBoxBomUSD_CNY.Text))
        If cur = "EUR" Then cny = (Val(value) * Val(TextBoxBomUSD_CNY.Text) * Val(TextBoxBomEUR_USD.Text))
        If cur = "JPY" Then cny = (Val(value) * Val(TextBoxBomEUR_USD.Text) * Val(TextBoxBomUSD_CNY.Text) / Val(TextBoxBomEUR_JPY.Text))
        If cny = 0 And Val(value) <> 0 And cur <> "" Then MsgBox("Currency don't rcognize " & cur)
    End Function

    Function jpy(ByVal value As String, ByVal cur As String) As Double
        jpy = 0
        If cur = "JPY" Then jpy = (Val(value))
        If cur = "USD" Then jpy = (Val(value) * Val(TextBoxBomEUR_JPY.Text) / Val(TextBoxBomEUR_USD.Text))
        If cur = "EUR" Then jpy = (Val(value) * Val(TextBoxBomEUR_JPY.Text))
        If cur = "CNY" Then jpy = (Val(value) / Val(TextBoxBomEUR_USD.Text) / Val(TextBoxBomUSD_CNY.Text) * Val(TextBoxBomEUR_JPY.Text))
        If jpy = 0 And Val(value) <> 0 And cur <> "" Then MsgBox("Currency don't rcognize " & cur)
    End Function


    Private Sub ButtonBrandRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandRefresh.Click
        updateBrandList()
    End Sub

    Private Sub ButtonBrandDelOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandDelOffer.Click
        TextBoxBrandLink.Text = ""
    End Sub

    Sub OpenConnectionSqlOrcad(ByVal strHost As String, ByVal strDatabase As String, ByVal strUserName As String, ByVal strPassword As String)
        If OrcadProblem = False Then
            Try
                ConnectionStringOrcad = "server=" & strHost & ";user id=" & strUserName & ";" & "pwd=" & strPassword & ";" & "database=" & strDatabase & ";Connect Timeout=10;"
                SqlconnectionOrcad = New SqlConnection(ConnectionStringOrcad)
                If SqlconnectionOrcad.State = ConnectionState.Closed Then
                    SqlconnectionOrcad.Open()
                End If
            Catch ex As Exception
                OrcadProblem = True
                MessageBox.Show(ex.ToString())
            End Try
        Else

        End If

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

    Function GetOrcadSupplier(ByVal BitronPN As String) As String
        If OrcadProblem = False Then
            GetOrcadSupplier = ""
            Try
                Dim AdapterSql As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') and codice_bitron = '" & BitronPN & "'", SqlconnectionOrcad)
                TblSql.Clear()
                DsSql.Clear()
                AdapterSql.Fill(DsSql, "orcadw.T_orcadcis")
                TblSql = DsSql.Tables("orcadw.T_orcadcis")

                If TblSql.Rows.Count > 0 Then
                    GetOrcadSupplier = IIf(TblSql.Rows.Item(0)("costruttore").ToString <> "", Replace(Replace(TblSql.Rows.Item(0)("costruttore").ToString, "[", "-"), "]", "-") & "[" & Replace(Replace(TblSql.Rows.Item(0)("orderingcode").ToString, "[", "-"), "]", "-") & "];", "")
                    For i = 2 To 9
                        GetOrcadSupplier = GetOrcadSupplier & IIf(TblSql.Rows.Item(0)("costruttore" & i).ToString <> "", Replace(Replace(TblSql.Rows.Item(0)("costruttore" & i).ToString, "[", "-"), "]", "-") & "[" & Replace(Replace(TblSql.Rows.Item(0)("orderingcode" & i).ToString, "[", "-"), "]", "-") & "];", "")
                    Next
                End If

            Catch ex As Exception
                ' MessageBox.Show(ex.ToString())
            End Try
        Else
            GetOrcadSupplier = ""
        End If
    End Function

    Function LoadComboBoxOrcad(ByVal s As String) As Boolean
        ComboBoxComponentOrcadSupplier.Items.Clear()
        ComboBoxComponentOrcadSupplier.Text = ""
        Dim j As Integer, i As Integer, brand As String
        LoadComboBoxOrcad = False

        If s <> "" Then
            Try
                If Mid(s, Len(s), 1) <> ";" Then s = s & ";"
                i = 1
                j = InStr(s, ";", CompareMethod.Text)
                While j > 0
                    brand = Mid(s, i, j - i)
                    If InStr(brand, "[", CompareMethod.Text) > 1 Then
                        If InStr(brand, "]", CompareMethod.Text) > 3 Or ((InStr(brand, "]", CompareMethod.Text) = 3) And _
                                                                         (InStr(Replace(Mid(brand, i + 1, j - 1 - i), " ", ""), "GENERALBRAND", CompareMethod.Text))) Then
                            If InStr(brand, "]", CompareMethod.Text) = Len(brand) Then
                                ComboBoxComponentOrcadSupplier.Items.Add(Replace(Mid(s, i, j - i), ";", ""))
                            Else
                                MsgBox("Error in Orcad AVL")
                                Exit While
                            End If
                        Else
                            MsgBox("Error in Orcad AVL")
                            Exit While
                        End If
                    Else
                        MsgBox("Error in Orcad AVL")
                        Exit While
                    End If
                    i = j
                    j = InStr(j + 1, s, ";", CompareMethod.Text)
                End While

            Catch ex As Exception
                MsgBox("Error in brand / brandAlt")
            End Try
        Else
            LoadComboBoxOrcad = True
        End If
        If ComboBoxComponentOrcadSupplier.Items.Count > 0 Then ComboBoxComponentOrcadSupplier.Text = ComboBoxComponentOrcadSupplier.Items(0).ToString

    End Function

    Sub SumBomQt(ByVal bom As String, ByVal type As String)

        Dim tblMySql As New System.Data.DataTable
        Dim dsMySql As New DataSet


        Try
            tblMySql.Clear()
            dsMySql.Clear()
            Dim adapterMySql As MySqlDataAdapter = New MySqlDataAdapter("SELECT SUM(`qt_v1`) AS `qt_v1`,SUM(`qt_v2`) AS `qt_v2`,SUM(`qt_v3`) AS `qt_v3`,SUM(`qt_v4`) AS `qt_v4`," & _
                                            "SUM(`qt_v5`) AS `qt_v5`,SUM(`qt_v6`) AS `qt_v6` FROM `offer` WHERE `name`='" & bom & "' AND `type`='" & type & "'", MySqlconnection)
            adapterMySql.Fill(dsMySql, "offer")
            tblMySql = dsMySql.Tables("offer")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim qt_v1 As String = ss(tblMySql.Rows(0).Item("qt_v1").ToString())
        Dim qt_v2 As String = ss(tblMySql.Rows(0).Item("qt_v2").ToString())
        Dim qt_v3 As String = ss(tblMySql.Rows(0).Item("qt_v3").ToString())
        Dim qt_v4 As String = ss(tblMySql.Rows(0).Item("qt_v4").ToString())
        Dim qt_v5 As String = ss(tblMySql.Rows(0).Item("qt_v5").ToString())
        Dim qt_v6 As String = ss(tblMySql.Rows(0).Item("qt_v6").ToString())

        'If qt_v1 + qt_v1 + qt_v1 + qt_v1 + qt_v1 + qt_v1 + qt_v1 > 0 And type = "" And typefilled = "" Then
        '    MsgBox("type not fill!")
        '    typefilled = "INFORMED"
        'End If

        Dim str As String = "[1]" & If(qt_v1 <> "", qt_v1, "0") & "[2]" & If(qt_v2 <> "", qt_v2, "0") & "[3]" & If(qt_v3 <> "", qt_v3, "0") & "[4]" & If(qt_v4 <> "", qt_v4, "0") & "[5]" & If(qt_v5 <> "", qt_v5, "0") & "[6]" & If(qt_v6 <> "", qt_v6, "0") & ""
        Dim commandMySql As MySqlCommand = New MySqlCommand("UPDATE `bomoffer` SET `" & IIf(type = "", "UN", type) & "`='" & str & "'  WHERE `name`='" & bom & "'", MySqlconnection)
        commandMySql.ExecuteNonQuery()

    End Sub

    Function ss(ByVal sval As Object) As String
        If Val(sval.ToString) = 0 Then
            ss = "0"
        Else
            ss = Int(sval)
        End If
    End Function

    Private Sub ButtonComponentDelLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentDelLink.Click
        TextBoxComponentDS.Text = ""
    End Sub

    Private Sub ButtonMakeOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonMakeOffer.Click
        ButtonBomBest_Click(Me, e)
        Try
            If TextBoxBomName.Text <> "" Then

                Dim bomOfferName As String = TextBoxBomName.Text
                Dim tblOffer As New System.Data.DataTable
                Dim dsOffer As New DataSet
                Dim adapterOffer As MySqlDataAdapter
                Dim tblBomOffer As New System.Data.DataTable
                Dim dsBomOffer As New DataSet
                Dim adapterBomOffer As MySqlDataAdapter

                Dim i As Integer, j As Integer

                'extract currency and pathoffer from parameterset table
                Dim eur_usd As Double = ParameterTable("eur_usd")
                Dim usd_cny As Double = ParameterTable("usd_cny")
                Dim eur_jpy As Double = ParameterTable("eur_jpy")
                Dim pathOffer As String = ParameterTable("pathoffer")
                Dim pathTemplate As String = ParameterTable("OfferTemplate")

                Dim msgREsult = MsgBox("Do you want select a specific offer? ", MsgBoxStyle.YesNoCancel)
                If msgREsult = MsgBoxResult.Yes Then
                    If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        pathTemplate = OpenFileDialog1.FileName
                    End If
                ElseIf msgREsult = vbCancel Then
                    Exit Sub
                End If

                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

                'open the offer template
                Dim excelApp As New Object
                excelApp = CreateObject("Excel.Application")

                Dim excelWorkbook As Object
                Dim excelSheet As Object
                Try
                    excelWorkbook = excelApp.Workbooks.Open(pathTemplate)
                    excelWorkbook.Activate()
                    excelSheet = excelWorkbook.Worksheets("DATA")
                    excelSheet.Activate()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                excelApp.Visible = True


                'extract data from OFFER table
                tblOffer.Clear()
                dsOffer.Clear()
                adapterOffer = New MySqlDataAdapter("SELECT `id`,`name`,`bitronpn`,`description`,`brand`,`brandalt`,`qt_v1`,`qt_v2`,`qt_v3`,`qt_v4`,`qt_v5`,`qt_v6`,`reference`," & _
                "`noternd`,`notepurchasing`,`notegeneric`,`customerpn`,`type`,`Class`,`tum`,`status`,`BitronpnPrice`,`BitronpnCurrency`,`AltPrice`,`AltCurrency`,`customerprice`,`customercurrency`,`Brandprice`,`Brandcurrency`,`deltaavl`,`deltaorcad`,`deltaprice`,`deltapricecurrency` FROM `offer` WHERE `name`='" & bomOfferName & "' order by id", MySqlconnection)
                adapterOffer.Fill(dsOffer, "offer")
                tblOffer = dsOffer.Tables("offer")

                tblBomOffer.Clear()
                dsBomOffer.Clear()
                'extract data from BOM OFFER table
                adapterBomOffer = New MySqlDataAdapter("SELECT `id`,`eta`,`name`,`currency`,`vol1`,`vol2`,`vol3`,`vol4`,`vol5`,`vol6`,`var1`,`var2`,`var3`,`var4`,`var5`,`var6`,`note`,`customer`,`status`,`smd_t`,`smd_b`,`ax`,`rd`,`p`,`un` FROM `bomoffer` WHERE `name`='" & bomOfferName & "' order by id", MySqlconnection)
                adapterBomOffer.Fill(dsBomOffer, "bomoffer")
                tblBomOffer = dsBomOffer.Tables("bomoffer")

                'copy BOM INFORMATION to DATA worksheet        
                For i = 0 To tblBomOffer.Columns.Count - 1
                    excelApp.Cells(2, i + 2) = UCase(tblBomOffer.Columns.Item(i).ColumnName)
                    excelApp.Cells(3, i + 2) = tblBomOffer.Rows(0).Item(i).ToString()
                Next

                'copy BOM CONTENTS to DATA worksheet
                'from id to qt_v6
                For i = 0 To tblOffer.Rows.Count - 1
                    j = 1
                    For Each column In tblOffer.Columns
                        excelApp.Cells(5, j + 1) = UCase(column.ColumnName)
                        excelApp.Cells(i + 6, j + 1) = tblOffer.Rows(i).Item(column.ColumnName).ToString()
                        j = j + 1
                    Next
                Next

                i = tblOffer.Rows.Count + 6  ' delete the existent row

                While excelApp.Cells(i, 2).text <> ""
                    j = 1
                    For Each column In tblOffer.Columns
                        excelApp.Cells(i, j) = ""
                        j = j + 1
                    Next
                    i = i + 1
                End While

                excelApp.Cells(3, 27) = usd_cny.ToString()
                excelApp.Cells(3, 28) = eur_usd.ToString()
                excelApp.Cells(3, 29) = eur_jpy.ToString()
                excelApp.Cells(2, 27) = "usd/cny"
                excelApp.Cells(2, 28) = "eur/usd"
                excelApp.Cells(2, 29) = "eur/jpy"
                Try
                    SaveFileDialog1.FileName = ParameterTable("plant") & "R_OFF_PRO_" & tblBomOffer.Rows(0).Item("id").ToString() & " - " & TextBoxBomName.Text & "_0.xlsx"

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
            Else
                MsgBox("offer a BomName")
            End If
        Catch ex As Exception
            MsgBox("offer a BomName " & ex.Message)
        End Try

    End Sub

    Private Sub ButtonBomDelOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomDelOffer.Click
        TextBoxBomOfferLink.Text = ""
    End Sub

    Private Sub ButtonBomAddOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomAddOffer.Click
        OpenFileDialog1.Filter = "All File (*.*)|*.*"

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK And OpenFileDialog1.CheckFileExists And InStr(OpenFileDialog1.FileName, "'", CompareMethod.Text) <= 0 And Not IsNothing(TreeViewBomList.SelectedNode) Then
            Try
                MkDir(ParameterTable("PathOfferToCustomer"))
            Catch ex As Exception

            End Try
            Try
                FileCopy(OpenFileDialog1.FileName, ParameterTable("PathOfferToCustomer") & Mid(OpenFileDialog1.FileName, 1 + InStrRev(OpenFileDialog1.FileName, "\")))
                MsgBox("File copied!   " & vbCrLf & vbCrLf & ParameterTable("PathOfferToCustomer") & Mid(OpenFileDialog1.FileName, 1 + InStrRev(OpenFileDialog1.FileName, "\")), MsgBoxStyle.Information)
                TextBoxBomOfferLink.Text = Mid(OpenFileDialog1.FileName, 1 + InStrRev(OpenFileDialog1.FileName, "\"))
            Catch ex As Exception
                MsgBox("File exist or others file problem! " & ex.Message)
            End Try
        Else
            MsgBox("Please select a product or delete in name file ' char ")
        End If

    End Sub

    Private Sub ButtonBomOfferOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomOfferOpen.Click
        Try
            Process.Start(ParameterTable("PathOfferToCustomer") & TextBoxBomOfferLink.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ButtonUpdatePfp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpdatePfp.Click
        ButtonUpdatePfp.Text = "Please wait......"
        ButtonUpdatePfp.Enabled = False
        Update_Pfp()
        ButtonUpdatePfp.Enabled = True
        ButtonUpdatePfp.Text = "Update PFP Done"
    End Sub

    Private Sub Update_Pfp()
        Dim sql As String
        Dim commandMySql As MySqlCommand
        CollectProcess()
        'open Parti_Fornitori_Prezzi.xls
        ButtonUpdatePfp.BackColor = Color.LightYellow
        Dim xlsApp As New Object
        xlsApp = CreateObject("Excel.Application")

        xlsApp.DisplayAlerts = False
        xlsApp.Visible = False
        Dim xlsWorkbook As Object = xlsApp.Workbooks.Open(ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("ExcelPfp"))
        xlsWorkbook.Activate()
        Dim xlsWorksheet As Object = xlsWorkbook.Worksheets(1)
        xlsWorksheet.Activate()

        'empty the PFP table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`pfp`", MySqlconnection)
        commandMySql.ExecuteNonQuery()

        'save the .xls file in .csv format
        Dim tempPath = Path.GetTempPath() & "temp.csv"
        Try
            If File.Exists(tempPath) Then
                File.Delete(tempPath)
            End If
            xlsWorkbook.SaveAs(tempPath, 6)
            xlsWorkbook.Close(True)
            xlsApp.Quit()
            Dim generation As Integer = GC.GetGeneration(xlsApp)
            GC.Collect(generation)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'copy data from excel to `pfp`
        sql = "load data local infile '" & Replace(tempPath, "\", "\\") & "' into table `pfp` fields terminated by ','  lines terminated by '\r\n' ignore 1 lines  (`pfidf`,`pepre`,`peval`,`pfpaf`,`pfpan`,`pfpad`,`pelot`,`pedin`,`pedfi`,`pefor`,`forsc`)"
        commandMySql = New MySqlCommand(sql, MySqlconnection)
        commandMySql.ExecuteNonQuery()

        'Update the table pfp_elborated
        Dim rowShowMain As DataRow(), rowShow As DataRow(), ass As Integer, LastBitronpn As String, currency As String, value As Single, datePfp As String
        Dim supplier As String, average As String, supcode As String

        'empty the PFP table
        commandMySql = New MySqlCommand("TRUNCATE TABLE `srvdoc`.`pfp_Elaborated`", MySqlconnection)
        commandMySql.ExecuteNonQuery()

        LastBitronpn = ""
        rowShowMain = tblPfp.Select("pfidf like '*'", "pfidf")
        For Each rowMain In rowShowMain

            If LastBitronpn <> rowMain("pfidf") Then
                LastBitronpn = rowMain("pfidf")

                ' pfp calculation
                rowShow = tblPfp.Select("pfidf = '" & rowMain("pfidf") & "'  and  pedfi = '0'", "pfpan desc,  pfpaf desc, pedin, pfppad")
                ass = 0
                currency = ""
                datePfp = ""
                value = 0
                supplier = ""
                average = "NO"
                supcode = ""
                For Each row In rowShow
                        If Val(row("pfpan")) <> 0 Then
                            If currency = "" Then currency = row("peval")
                            If datePfp = "" Then datePfp = row("pedin")
                            value = value + Val(row("pfpan")) * usd(ConvPrice(Val(row("pepre")), row("pelot")), row("peval"))
                            ass = Val(row("pfpan")) + ass
                            If ass > 0 And ass < 100 Then average = "YES"
                            supplier = supplier & IIf(supplier <> "", " ; ", "") & strip(row("FORSC"))
                            supcode = supcode & IIf(supcode <> "", ";", "") & strip(row("pefor"))
                            If ass >= 100 Then Exit For
                        End If

                Next

                If ass = 0 Then
                    For Each row In rowShow
                            If Val(row("pfpaf")) <> 0 Then
                                If currency = "" Then currency = row("peval")
                                If datePfp = "" Then datePfp = row("pedin")
                                value = value + Val(row("pfpaf")) * usd(ConvPrice(Val(row("pepre")), row("pelot")), row("peval"))
                                ass = Val(row("pfpaf")) + ass
                                If ass > 0 And ass < 100 Then average = "YES"
                                supplier = supplier & IIf(supplier <> "", " ; ", "") & strip(row("FORSC"))
                                supcode = supcode & IIf(supcode <> "", ";", "") & strip(row("pefor"))
                                If ass >= 100 Then Exit For
                            End If

                    Next
                End If
                value = value / 100
                value = ConvertPriceCurency(currency, value, "USD")

                If (ass < 100 Or ass > 100) And rowShow.Length > 0 Then
                    'MsgBox("Pfp not recognized!  " & rowMain("pfidf"))
                ElseIf ass = 100 Then
                    'write pfpElaborated
                    sql = "INSERT INTO `" & DBName & "`.`pfp_Elaborated` (`bitronpn` ,`value`,`currency`,`datePfp`,`average`,`supplier`,`suppliercode`) VALUES ('" & _
                    rowMain("pfidf") & "', " & Replace(Str(Math.Round(value, 6)), ",", ".") & ", '" & currency & "', '" & datePfp & "', '" & average & "', '" & supplier & "', '" & supcode & "'); "
                    commandMySql = New MySqlCommand(sql, MySqlconnection)
                    commandMySql.ExecuteNonQuery()
                Else
                    ' row not rilevant
                End If
            End If
        Next
        ButtonUpdatePfp.BackColor = Color.Green
        KillLastExcel()

    End Sub



    Public Function strip(ByVal des As String)
        Dim strorigFileName As String
        Dim intCounter As Integer
        Dim arrSpecialChar() As String = {".", ",", "<", ">", ":", "?", """", "/", "{", "[", "}", "]", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "|", "'", "\"}
        strorigFileName = des
        intCounter = 0
        Dim i As Integer
        For i = 0 To arrSpecialChar.Length - 1
            Do Until intCounter = 29
                des = Replace(strorigFileName, arrSpecialChar(i), "")
                intCounter = intCounter + 1
                strorigFileName = des
            Loop
            intCounter = 0
        Next
        Return strorigFileName

    End Function


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

    Function OnlyChar(ByVal s As String) As String
        OnlyChar = s
        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Then
            Else
                s = Replace(s, Mid(s, i, 1), " ")
            End If
            OnlyChar = Replace(s, " ", "")
        Next

    End Function

    Function ReadBrandLiking(ByVal brand As String) As String


        Dim rowShow As DataRow()
        ReadBrandLiking = "MISSING"
        ' Search Brand corrispondence
        If BooLinkingChanges = True Then
            Try
                DsBrand.Clear()
                tblBrand.Clear()
            Catch ex As Exception

            End Try
            AdapterBrand.Fill(DsBrand, "Brand")
            tblBrand = DsBrand.Tables("Brand")
            BooLinkingChanges = False
        End If

        rowShow = tblBrand.Select("Brand = '" & brand & "' and buyer='SystemLiking' ")

        For Each row In rowShow
            ReadBrandLiking = row("SUPPLIER").ToString
        Next

        If rowShow.Length > 1 Then MsgBox("More of one Liking name brand: " & brand)

    End Function

    Sub WriteBrandLiking(ByVal brand As String, ByVal liking As String)

        Dim cmd As New MySqlCommand()
        Dim sql As String

        If ReadBrandLiking(brand) <> "MISSING" Then
            Try
                sql = "UPDATE `" & DBName & "`.`Brand` SET " & _
                "`Supplier` = '" & liking & "'" & _
                ", `buyer` = 'SystemLiking'" & _
                " WHERE `Brand`.`brand` = '" & brand & "'"

                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox("Mysql brand liking update query error!" & ex.Message, MsgBoxStyle.Critical)
            End Try

        Else

            Try
                sql = "INSERT INTO `" & DBName & "`.`Brand` (`Brand` ,`supplier`,`buyer`) VALUES ('" & _
                brand & "', '" & liking & "','SystemLiking' ); "
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
                updateBrandList()

            Catch ex As Exception
                MsgBox("Brand liking insert error !" & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If

    End Sub

    Sub UpdateLiking()
        Try



            Dim AllLiked As Boolean, Liking As String

            ' BRAND

            If ComboBoxComponentBrand.Text <> "" Then
                ButtonBrandLiking.Visible = True
                Liking = ReadBrandLiking(Mid(ComboBoxComponentBrand.Text, 1, InStr(ComboBoxComponentBrand.Text, "[") - 1))

                If Liking = "YELLOW" Then
                    ButtonBrandLiking.BackColor = Color.Yellow
                ElseIf Liking = "RED" Then
                    ButtonBrandLiking.BackColor = Color.Red
                ElseIf Liking = "GREEN" Then
                    ButtonBrandLiking.BackColor = Color.Green
                ElseIf Liking = "MISSING" Or Liking = "WHITE" Then
                    ButtonBrandLiking.BackColor = Color.White
                End If

                AllLiked = True
                For Each Brand In ComboBoxComponentBrand.Items
                    If ReadBrandLiking(Mid(Brand, 1, InStr(Brand, "[") - 1)) = "MISSING" Then AllLiked = False
                Next
            Else
                ButtonBrandLiking.Visible = False
            End If

            If AllLiked = True Then
                ButtonBrandLiking.Text = ""
            Else
                ButtonBrandLiking.Text = "?"
            End If


            ' BRAND ALT
            Try

            Catch ex As Exception

            End Try
            If ComboBoxComponentALTBrand.Text <> "" Then
                ButtonBrandAltLiking.Visible = True
                Liking = ReadBrandLiking(Mid(ComboBoxComponentALTBrand.Text, 1, InStr(ComboBoxComponentALTBrand.Text, "[") - 1))

                If Liking = "YELLOW" Then
                    ButtonBrandAltLiking.BackColor = Color.Yellow
                ElseIf Liking = "RED" Then
                    ButtonBrandAltLiking.BackColor = Color.Red
                ElseIf Liking = "GREEN" Then
                    ButtonBrandAltLiking.BackColor = Color.Green
                ElseIf Liking = "MISSING" Or Liking = "WHITE" Then
                    ButtonBrandAltLiking.BackColor = Color.White
                End If

                AllLiked = True
                For Each Brand In ComboBoxComponentALTBrand.Items
                    If ReadBrandLiking(Mid(Brand, 1, InStr(Brand, "[") - 1)) = "MISSING" Then AllLiked = False
                Next
            Else
                ButtonBrandAltLiking.Visible = False
            End If

            If AllLiked = True Then
                ButtonBrandAltLiking.Text = ""
            Else
                ButtonBrandAltLiking.Text = "?"
            End If


            ' BITRON PN

            If TextBoxComponentBitronPN.Text <> "" Then
                ButtonBitronPnLiking.Visible = True
                Liking = ReadBrandLiking(Replace(TextBoxComponentBitronPN.Text, "E", ""))

                If Liking = "YELLOW" Then
                    ButtonBitronPnLiking.BackColor = Color.Yellow
                ElseIf Liking = "RED" Then
                    ButtonBitronPnLiking.BackColor = Color.Red
                ElseIf Liking = "GREEN" Then
                    ButtonBitronPnLiking.BackColor = Color.Green
                ElseIf Liking = "MISSING" Or Liking = "WHITE" Then
                    ButtonBitronPnLiking.BackColor = Color.White
                End If

                AllLiked = True

                If ReadBrandLiking(Replace(TextBoxComponentBitronPN.Text, "E", "")) = "MISSING" Then AllLiked = False
            Else
                ButtonBitronPnLiking.Visible = False
            End If

            If AllLiked = True Then
                ButtonBitronPnLiking.Text = ""
            Else
                ButtonBitronPnLiking.Text = "?"
            End If
        Catch ex As Exception

        End Try

    End Sub

    Function NextColor(ByVal Mycolor As Color) As String
        NextColor = "WHITE"
        If Mycolor = Color.White Then NextColor = "GREEN"
        If Mycolor = Color.Green Then NextColor = "YELLOW"
        If Mycolor = Color.Yellow Then NextColor = "RED"
        If Mycolor = Color.Red Then NextColor = "WHITE"

    End Function

    Private Sub ButtonBitronPnLiking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBitronPnLiking.Click
        If controlRight("U") >= 2 Then
            If TextBoxComponentBitronPN.Text <> "" Then WriteBrandLiking(TextBoxComponentBitronPN.Text, NextColor(ButtonBitronPnLiking.BackColor))
            BooLinkingChanges = True
            UpdateLiking()
        End If
    End Sub

    Private Sub ButtonBrandAltLiking_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandAltLiking.Click
        If controlRight("U") >= 2 Then
            If ComboBoxComponentALTBrand.Text <> "" Then WriteBrandLiking(Mid(ComboBoxComponentALTBrand.Text, 1, InStr(ComboBoxComponentALTBrand.Text, "[") - 1), NextColor(ButtonBrandAltLiking.BackColor))
            BooLinkingChanges = True
            UpdateLiking()
        End If
    End Sub

    Private Sub ButtonBrandLight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrandLiking.Click
        If controlRight("U") >= 2 Then
            If ComboBoxComponentBrand.Text <> "" Then WriteBrandLiking(Mid(ComboBoxComponentBrand.Text, 1, InStr(ComboBoxComponentBrand.Text, "[") - 1), NextColor(ButtonBrandLiking.BackColor))
            BooLinkingChanges = True
            UpdateLiking()
        End If
    End Sub


    Private Sub ButtonBomSigipCompare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomSigipCompare.Click

        ' primo tentativo pare non ok, da debaggare
        SigipOfferDifference()
        Application.DoEvents()
        Dim sw As StreamWriter
        SaveFileDialog1.FileName = "Difference SIGIP and " & TextBoxBomName.Text & ".txt"
        If DialogResult.OK = SaveFileDialog1.ShowDialog() Then
            Dim FileDifference = SaveFileDialog1.FileName
            sw = File.CreateText(FileDifference)
            sw.WriteLine(SigipOfferDifference)
            sw.Flush()
            sw.Close()
            Application.DoEvents()
            Process.Start(FileDifference)
        End If
    End Sub


    Function SigipOfferDifference() As String
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim rowShowOffer As DataRow()
        Dim rowShowSigip As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        Dim tblSigip As DataTable
        Dim DsSigip As New DataSet
        Dim TotQt As Double
        Dim lastSigipPn As String = ""
        Dim VectorResult As String = ""
        Dim AdapterOffMY As New MySqlDataAdapter("SELECT * FROM offer", MySqlconnection)
        AdapterOffMY.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")

        Dim AdapterSigipMY As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
        AdapterSigipMY.Fill(DsSigip, "sigip")
        tblSigip = DsSigip.Tables("sigip")
        Application.DoEvents()
        ButtonBomSigipCompare.BackColor = Color.Yellow
        If TextBoxBomName.Text <> "" Then
            VectorResult = ""
            For i = 1 To 6

                If TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text <> "" Then
                    Try
                        sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                        "`CompareFlag` = 'X'  WHERE `offer`.`name` = '" & TextBoxBomName.Text & "'"
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        MsgBox("Compare flag delete error error!" & ex.Message, MsgBoxStyle.Critical)
                    End Try

                    rowShowSigip = tblSigip.Select("bom = '" & TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text & "'", "bitron_pn")
                    If rowShowSigip.Length > 0 Then

                        For Each RowSigip In rowShowSigip

                            If lastSigipPn <> RowSigip("bitron_pn").ToString Then
                                lastSigipPn = RowSigip("bitron_pn").ToString
                                Application.DoEvents()
                                rowShowOffer = tblOff.Select("bitronpn='" & RowSigip("bitron_pn").ToString & "' and name = '" & TextBoxBomName.Text & "'")
                                TotQt = 0
                                For Each RowOffer In rowShowOffer
                                    TotQt = TotQt + RowOffer("qt_v" & i)
                                Next
                                If Math.Round(Val(TotQt), 5) <> Math.Round(Val(TotQtSigip(RowSigip("bitron_pn").ToString, TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text, tblSigip))) Then
                                    VectorResult = VectorResult & TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text & _
                                          " --> " & Mid(RowSigip("bitron_pn").ToString & "                    ", 1, 12) & " " & Mid(RowSigip("des_pn").ToString & "                                        ", 1, 30) & " SIGIP_Qt[" & TotQtSigip(RowSigip("bitron_pn").ToString, TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text, tblSigip) & "] -- Quote Qt[" & _
                                          TotQt & "]" & vbCrLf
                                End If

                                Try
                                    sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                                    "`CompareFlag` = 'C'  WHERE `offer`.`name` = '" & TextBoxBomName.Text & "' and bitronpn='" & RowSigip("bitron_pn").ToString & "'"
                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox("Compare flag delete error error!" & ex.Message, MsgBoxStyle.Critical)
                                End Try
                            End If
                        Next
                        DsOff.Clear()
                        tblOff.Clear()
                        AdapterOffMY.Fill(DsOff, "Offer")
                        rowShowOffer = tblOff.Select("CompareFlag='X' AND name = '" & TextBoxBomName.Text & "'")

                        For Each RowOffer In rowShowOffer
                            VectorResult = VectorResult & TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text & _
                            " --> " & Mid(RowOffer("bitronpn").ToString & "                     ", 1, 12) & " " & Mid(RowOffer("description").ToString & "                                              ", 1, 30) _
                            & " SIGIP_Qt[0] -- Quote Qt[" & _
                            RowOffer("qt_v" & i).ToString & "]" & vbCrLf
                        Next

                    Else
                        VectorResult = VectorResult & " BOM NOT FIND IN SIGIP " & TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text & vbCrLf
                    End If
                End If
                Application.DoEvents()
            Next
            SigipOfferDifference = VectorResult

        End If
        ButtonBomSigipCompare.BackColor = Color.Green
        DsOff.Dispose()
        tblOff.Dispose()
    End Function

    Private Sub ButtonBomImportSigipBom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBomImportSigipBom.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim rowShowOffer As DataRow()
        Dim rowShowSigip As DataRow()
        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        Dim tblSigip As DataTable
        Dim DsSigip As New DataSet

        ButtonBomImportSigipBom.BackColor = Color.Yellow
        ButtonBomImportSigipBom.Enabled = False
        Dim AdapterOffMY As New MySqlDataAdapter("SELECT * FROM offer", MySqlconnection)
        AdapterOffMY.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")

        Dim AdapterSigipMY As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
        AdapterSigipMY.Fill(DsSigip, "sigip")
        tblSigip = DsSigip.Tables("sigip")

        Application.DoEvents()
        rowShowOffer = tblOff.Select("name = '" & TextBoxBomName.Text & "'")
        If TextBoxBomName.Text <> "" And rowShowOffer.Length = 0 Then
            For i = 1 To 6

                If TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text <> "" Then

                    rowShowSigip = tblSigip.Select("(not BITRON_PN like '18*') and ACQ_FAB ='ACQ' AND bom = '" & TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text & "'")
                    If rowShowSigip.Length > 0 Then

                        For Each RowSigip In rowShowSigip
                            Application.DoEvents()
                            DsOff.Clear()
                            tblOff.Clear()
                            AdapterOffMY.Fill(DsOff, "Offer")
                            tblOff = DsOff.Tables("Offer")
                            rowShowOffer = tblOff.Select("bitronpn='" & RowSigip("bitron_pn").ToString & "' and name = '" & TextBoxBomName.Text & "'")
                            If rowShowOffer.Length > 0 Then
                                Try
                                    sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                                    " `description` = '" & RowSigip("des_pn").ToString & "', `" & "qt_v" & i & "` = '" & Val(RowSigip("qt").ToString) + Val(rowShowOffer(0).Item("qt_v" & i).ToString) & "' WHERE `offer`.`name` = '" & TextBoxBomName.Text & "' and bitronpn='" & RowSigip("bitron_pn").ToString & "'"
                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox("Compare flag delete error error!" & ex.Message, MsgBoxStyle.Critical)
                                End Try

                            Else
                                Try
                                    sql = "INSERT INTO `" & DBName & "`.`offer` (`" & "qt_v" & i & "` ,`bitronpn` ,`Name` ,`description`) VALUES ('" & _
                                    Val(RowSigip("qt").ToString) & "', '" & RowSigip("bitron_pn").ToString & "', '" & TextBoxBomName.Text & "', '" & RowSigip("des_pn").ToString & "');"
                                    cmd = New MySqlCommand(sql, MySqlconnection)
                                    cmd.ExecuteNonQuery()
                                Catch ex As Exception
                                    MsgBox("Sigip Component insert error " & ex.Message)
                                End Try

                            End If

                        Next

                    End If
                End If
                ButtonBomImportSigipBom.Text = "Import BOM n...." & i
                ButtonBomImportSigipBom.BackColor = Color.Yellow
            Next
            DsOff.Clear()
            tblOff.Clear()
            AdapterOffMY.Fill(DsOff, "Offer")
            rowShowOffer = tblOff.Select("name = '" & TextBoxBomName.Text & "'")
            If rowShowOffer.Length > 0 Then
                For Each RowOffer In rowShowOffer
                    For i = 1 To 6
                        If RowOffer("qt_v" & i).ToString = "" And TabControl.TabPages(0).Controls("TextBoxNameV" & i).Text <> "" Then
                            Try
                                sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                                " `" & "qt_v" & i & "` = '0'  WHERE `offer`.`name` = '" & TextBoxBomName.Text & "' and id='" & RowOffer("id").ToString & "'"
                                cmd = New MySqlCommand(sql, MySqlconnection)
                                cmd.ExecuteNonQuery()
                            Catch ex As Exception
                                MsgBox("Zero insert delete error error!" & ex.Message, MsgBoxStyle.Critical)
                            End Try
                        End If
                    Next
                Next
            End If
            ButtonBomImportSigipBom.Text = "Bom Imported"
            ButtonBomImportSigipBom.BackColor = Color.Green
            MsgBox("Bom Imported!")
        Else
            MsgBox("No selected BOM or component already present!")
            ButtonBomImportSigipBom.BackColor = Color.Red
        End If

        ButtonBomImportSigipBom.Enabled = True


    End Sub

    Function TotQtSigip(ByVal pn As String, ByVal bom As String, ByVal tblSigip As DataTable) As String
        Dim rowShowOffer As DataRow()

        Application.DoEvents()
        rowShowOffer = tblSigip.Select("bom = '" & bom & "' and bitron_pn='" & pn & "'")

        TotQtSigip = 0
        For i = 1 To rowShowOffer.Length
            TotQtSigip = Val(TotQtSigip) + Val(rowShowOffer(i - 1).Item("qt").ToString)
        Next

    End Function


    Private Sub ButtonComponentImportOrcad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentImportOrcad.Click
        ComboBoxComponentBrand.Items.Clear()
        ComboBoxComponentBrand.Text = ""
        For i = 0 To ComboBoxComponentOrcadSupplier.Items.Count - 1
            ComboBoxComponentBrand.Items.Add(ComboBoxComponentOrcadSupplier.Items(i).ToString)
        Next i
        Try
            ComboBoxComponentBrand.Text = ComboBoxComponentBrand.Items(0).ToString
            ValueChangedComponent(Me, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        NoInfoBomBest = True
        ButtonBomBest_Click(Me, e)
        updateComponentList()
        NoInfoBomBest = False
    End Sub

    Private Sub TabPageComponent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageComponent.Click

    End Sub

    Private Sub TextBoxComponentFilter_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxComponentFilter.KeyUp
        If e.KeyValue = 13 Then updateComponentList()
    End Sub

    Private Sub CheckBoxNO_ALTP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxNO_ALTP.CheckedChanged
        updateComponentList()
    End Sub

    Private Sub TreeViewComponent_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TreeViewComponent.KeyUp
        If e.KeyCode = 112 Then ButtonComponentCustomerTRF_Click(Me, e)
    End Sub

    Private Sub ButtonOpenOfferBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOpenOfferBrand.Click

        Dim rowShow As DataRow()

        Try
            tblBrand.Clear()
            DsBrand.Clear()
        Catch ex As Exception

        End Try
        Try
            AdapterBrand.Fill(DsBrand, "brand")
            tblBrand = DsBrand.Tables("brand")

            rowShow = tblBrand.Select("(not buyer = 'SystemLiking') and (brand like '" & Mid(Replace(ComboBoxComponentBrand.Text, "[", "[[]"), 1, -1 + Len(Replace(ComboBoxComponentBrand.Text, "[", "[[]"))) & "[]]')", "brand")

            For Each row In rowShow
                If row("offerlink").ToString <> "" Then
                    Try
                        Process.Start(row("offerlink").ToString)
                    Catch ex As Exception

                    End Try
                Else
                    MsgBox("No Offer stored for this p/n")
                End If

            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonOpenOfferBrandAlt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOpenOfferBrandAlt.Click
        Dim rowShow As DataRow()

        Try
            tblBrand.Clear()
            DsBrand.Clear()
        Catch ex As Exception

        End Try

        AdapterBrand.Fill(DsBrand, "brand")
        tblBrand = DsBrand.Tables("brand")

        rowShow = tblBrand.Select("(not buyer = 'SystemLiking') and (brand like '" & Mid(Replace(ComboBoxComponentALTBrand.Text, "[", "[[]"), 1, -1 + Len(Replace(ComboBoxComponentALTBrand.Text, "[", "[[]"))) & "[]]')", "brand")

        For Each row In rowShow
            If row("offerlink").ToString <> "" Then
                Try
                    Process.Start(row("offerlink").ToString)
                Catch ex As Exception

                End Try
            Else
                MsgBox("No Offer stored for this p/n")
            End If

        Next
    End Sub


    Private Sub ButtonPredict_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPredict.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String
        Dim rowShowOffer As DataRow()

        Dim tblOff As DataTable
        Dim DsOff As New DataSet
        Dim ComponentClass As String = ""


        Dim AdapterOffMY As New MySqlDataAdapter("SELECT * FROM offer", MySqlconnection)
        AdapterOffMY.Fill(DsOff, "Offer")
        tblOff = DsOff.Tables("Offer")

        If vbYes = MsgBox("Do you want fill all class based on description?", vbYesNo) Then

            rowShowOffer = tblOff.Select("name = '" & TextBoxBomName.Text & "'")
            If rowShowOffer.Length > 0 Then
                For Each RowOffer In rowShowOffer
                    ComponentClass = ""
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "res" Then ComponentClass = "Resistor"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "cc." Then ComponentClass = "Capacitor"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "ic." Then ComponentClass = "Ic"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "rele." Then ComponentClass = "Relay"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "cc." Then ComponentClass = "Capacitor"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "cc." Then ComponentClass = "Capacitor"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "cc." Then ComponentClass = "Capacitor"
                    If Mid(RowOffer("description").ToString = "", 1, 3) = "cc." Then ComponentClass = "Capacitor"

                    If ComponentClass <> "" Then
                        Try
                            sql = "UPDATE `" & DBName & "`.`offer` SET " & _
                            " `" & "class" & ComponentClass & "` = '0'  WHERE `offer`.`id` = " & RowOffer("id").ToString
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            MsgBox("error in class writing!" & ex.Message, MsgBoxStyle.Critical)
                        End Try
                    End If
                Next
            End If
            ButtonBomImportSigipBom.Text = "Bom Imported"
            ButtonBomImportSigipBom.BackColor = Color.Green
            MsgBox("Bom Imported!")
        Else
            MsgBox("No selected BOM or component already present!")
        End If

    End Sub


    Private Sub CheckBoxOrderByNumber_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxOrderByNumber.CheckedChanged
        UpdateTreeBomOffer()
    End Sub

    Private Sub TabPageOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabPageOffer.Click

    End Sub

    Private Sub ButtonEstimation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEstimation.Click
        If InStr(TextBoxComponentBitronPN.Text, "Price_Est_", CompareMethod.Text) > 0 Then
            TextBoxComponentBitronPN.Text = Replace(TextBoxComponentBitronPN.Text, "Price_Est_", "")
        Else
            TextBoxComponentBitronPN.Text = "Price_Est_" & TextBoxComponentBitronPN.Text
        End If

    End Sub

    Private Sub CheckBoxEstimation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEstimation.CheckedChanged
        UpdateTreeBomOffer()
    End Sub

    Private Sub ButtonComponentsImportImportORCAD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentsImportORCAD.Click
        ComboBoxComponentALTBrand.Items.Clear()
        ComboBoxComponentALTBrand.Text = ""
        For i = 0 To ComboBoxComponentOrcadSupplier.Items.Count - 1
            ComboBoxComponentALTBrand.Items.Add(ComboBoxComponentOrcadSupplier.Items(i).ToString)
        Next i
        Try
            ComboBoxComponentALTBrand.Text = ComboBoxComponentALTBrand.Items(0).ToString
            ValueChangedComponent(Me, e)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ButtonComponentImportBrand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonComponentImportBrand.Click
        ComboBoxComponentALTBrand.Items.Clear()
        ComboBoxComponentALTBrand.Text = ""
        For i = 0 To ComboBoxComponentBrand.Items.Count - 1
            ComboBoxComponentALTBrand.Items.Add(ComboBoxComponentBrand.Items(i).ToString)
        Next i
        Try
            ComboBoxComponentALTBrand.Text = ComboBoxComponentALTBrand.Items(0).ToString
            ValueChangedComponent(Me, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBoxResult_On_Going_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxResult_On_Going.CheckedChanged
        If updatigComponent = False Then UpdateTreeBomOffer()
    End Sub

    Private Sub ComboBoxCustomerFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCustomerFilter.SelectedIndexChanged
        UpdateTreeBomOffer()
        ProgressBarBom.Value = 0
    End Sub

    Private Sub ComboBoxBomStatusFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBomStatusFilter.SelectedIndexChanged
        UpdateTreeBomOffer()
        ProgressBarBom.Value = 0
    End Sub

    Private Sub ComboBoxBomCurrency_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBomCurrency.SelectedIndexChanged

    End Sub
End Class