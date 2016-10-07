Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class FormProduct

    Dim index As Long = 1
    Dim AdapterProd As New MySqlDataAdapter("SELECT * FROM Product", MySqlconnection)
    Dim tblProd As DataTable
    Dim DsProd As New DataSet
    Dim AdapterCus As New MySqlDataAdapter("SELECT * FROM Customer", MySqlconnection)
    Dim tblCus As DataTable
    Dim DsCus As New DataSet
    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet
    Dim Adaptertype As New MySqlDataAdapter("SELECT * FROM doctype", MySqlconnection)
    Dim tbltype As DataTable
    Dim Dstype As New DataSet
    Dim User3 As String
    Dim AdapterSigip As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
    Dim tblSigip As DataTable
    Dim DsSigip As New DataSet
    Dim AdapterEcr As New MySqlDataAdapter("SELECT * FROM Ecr", MySqlconnection)
    Dim tblEcr As DataTable
    Dim DsEcr As New DataSet
    Dim AdapterBom As New MySqlDataAdapter("SELECT * FROM sigip", MySqlconnection)
    Dim tblbom As New DataTable
    Dim dsbom As New DataSet
    Dim DsDocComp As New DataSet
    Dim tblDocComp As New DataTable
    Dim AdapterSql As SqlDataAdapter
    Dim TblSql As New DataTable
    Dim DsSql As New DataSet
    Dim ConnectionStringOrcad As String
    Dim SqlconnectionOrcad As New SqlConnection
    ' EVENT



    Private Sub FormProduct_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        FormStart.Show()
        tblProd.Dispose()
        DsProd.Dispose()
        AdapterProd.Dispose()
        tblCus.Dispose()
        DsCus.Dispose()
        AdapterCus.Dispose()
    End Sub

    Private Sub FormProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        Me.Focus()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        AdapterCus.Fill(DsCus, "Customer")
        tblCus = DsCus.Tables("Customer")
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")
        Adaptertype.Fill(Dstype, "doctype")
        tbltype = Dstype.Tables("doctype")
        'AdapterSigip.Fill(DsSigip, "sigip")
        'tblSigip = DsSigip.Tables("sigip")
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")
        fillEcrComboMch()
        FillCustomerCombo()
        ComboBoxStatus.Items.Add("")
        ComboBoxStatus.Items.Add("OBSOLETE")
        ComboBoxStatus.Items.Add("SOP_SAMPLE")
        ComboBoxStatus.Items.Add("R&D_APPROVED")
        ComboBoxStatus.Items.Add("LOGISTIC_APPROVED")
        ComboBoxStatus.Items.Add("CUSTOMER_APPROVED")
        ComboBoxStatus.Items.Add("PURCHASING_APPROVED")
        ComboBoxStatus.Items.Add("PRODUCTION_APPROVED")
        ComboBoxStatus.Items.Add("TIME&MOTION_APPROVED")
        ComboBoxStatus.Items.Add("TESTING_ENG_APPROVED")
        ComboBoxStatus.Items.Add("PROCESS_ENG_APPROVED")
        ComboBoxStatus.Items.Add("FINANCIAL_APPROVED")
        ComboBoxStatus.Items.Add("MPA_APPROVED")
        ComboBoxStatus.Items.Add("MPA_STOPPED")

        If controlRight("R") >= 2 Then ButtonSIGIP.Visible = True
        If controlRight("F") >= 2 Then ButtonSIGIP.Visible = True

        updateECRMark()
        FillListView()

        'If controlRight("R") >= 3 Then  
        If controlRight("R") >= 2 Then
            ButtonAddProduct.Enabled = True
            ButtonDelete.Enabled = True
            ButtonCustomerAdd.Enabled = True
            ButtonDeleteCustomer.Enabled = True
            ButtonRemoveMch.Enabled = True
            ButtonAddMch.Enabled = True
            ButtonUpdate.Enabled = True
            TextBoxDAI.Enabled = True
        Else
            ButtonAddMch.Enabled = False
            ButtonAddProduct.Enabled = False
            ButtonDelete.Enabled = False
            ButtonDeleteCustomer.Enabled = False
            ButtonCustomerAdd.Enabled = False
            ButtonRemoveMch.Enabled = False
            ButtonUpdate.Enabled = False
            TextBoxDAI.Enabled = False
        End If

        Dim h As New ColumnHeader
        Dim h2 As New ColumnHeader
        h.Text = "ElementCode"
        h.Width = 100
        h2.Text = "Description"
        h2.Width = 260
        ListViewMch.Columns.Add(h)
        ListViewMch.Columns.Add(h2)
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String

        Dim mch As String = ""
        For i = 0 To ListViewMch.Items.Count - 1
            mch = mch & StrDup(20 - Len(ListViewMch.Items(i).SubItems(0).Text()), " ") & ListViewMch.Items(i).SubItems(0).Text
            mch = mch & StrDup(40 - Len(ListViewMch.Items(i).SubItems(1).Text()), " ") & ListViewMch.Items(i).SubItems(1).Text
        Next

        If controlRight("W") = 3 Then
            If ComboBoxCustomer.Text <> "" And TextBoxProduct.Text <> "" And TextBoxDescription.Text <> "" Then
                Try
                    sql = "INSERT INTO `" & DBName & "`.`product` (`BitronPN` ,`Name` ,`Customer` ,`Status` ,`DocFlag` ,`pcbCode`,`PiastraCode`,`StatusUpdateDate`,`MchElement`) VALUES ('" &
                    Trim(TextBoxProduct.Text) & "', '" & Trim(UCase(TextBoxDescription.Text)) & "', '" & Trim(ComboBoxCustomer.Text) & "', '" &
                    "" & "', '" & strControl() & "', '" & Trim(TextBoxPcb.Text) & "', '" &
                    Trim(TextBoxPiastra.Text) & "', 'INSERT[" & date_to_string(Today) & "]','" &
                    mch & "'" & ");"

                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()

                    ComunicationLog("5041") ' 
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error, check if bitron p/n is already in db
                End Try
                reset()
                FillListView()
            Else
                ComunicationLog("5049") ' please fill all field before update
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If

    End Sub

    Private Sub ButtonUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click

        Dim cmd As New MySqlCommand()
        Dim sql As String

        Dim mch As String = ""
        For i = 0 To ListViewMch.Items.Count - 1
            mch = mch & StrDup(20 - Len(ListViewMch.Items(i).SubItems(0).Text()), " ") & ListViewMch.Items(i).SubItems(0).Text
            mch = mch & StrDup(40 - Len(Mid(ListViewMch.Items(i).SubItems(1).Text(), 1, 40)), " ") & Mid(ListViewMch.Items(i).SubItems(1).Text, 1, 40)
        Next

        If controlRight("W") >= 2 Then
            If TextBoxProduct.Text <> "" And TextBoxDescription.Text <> "" And (TextBoxDAI.Text = "" Or TextBoxDAI.Text = "NO_DAI" Or (Regex.IsMatch(TextBoxDAI.Text, "^K[0-9]+")) And Len(TextBoxDAI.Text) = 8) Then
                Try
                    sql = "UPDATE `" & DBName & "`.`product` SET `Name` = '" & Trim(UCase(TextBoxDescription.Text)) &
                    "',`Customer` = '" & Trim(ComboBoxCustomer.Text) &
                    "',`PiastraCode` = '" & Trim(TextBoxPiastra.Text) &
                    "',`LS_rmb` = '" & TextBoxLS.Text &
                    "',`dai` = '" & UCase(Trim(TextBoxDAI.Text)) &
                    "',`mchElement` = '" & (mch) &
                    "',`DocFlag` = '" & Trim(strControl()) & "' WHERE `product`.`BitronPN` = '" & Trim(TextBoxProduct.Text) & "' ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    Try
                        griddUpdate(ListView1.SelectedItems.Item(0).SubItems(3).Text = TextBoxProduct.Text)
                        ListBoxLog.Items.Add(ListView1.SelectedItems.Item(0).SubItems(3).Text & "  -  Product Updated!")
                    Catch ex As Exception

                    End Try
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error 
                End Try

            Else
                ComunicationLog("5049") ' please fill all fields before update
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If


    End Sub

    Sub griddUpdate(ByVal bitronpn As String)

        If ListView1.SelectedItems.Count = 1 And ListView1.SelectedItems.Item(0).SubItems(3).Text = TextBoxProduct.Text Then
            ListView1.SelectedItems.Item(0).SubItems(7).Text = ComboBoxStatus.Text
            ListView1.SelectedItems.Item(0).SubItems(4).Text = TextBoxDescription.Text
            ListView1.SelectedItems.Item(0).SubItems(5).Text = ComboBoxCustomer.Text
            ListView1.SelectedItems.Item(0).SubItems(2).Text = TextBoxPiastra.Text
            ListView1.SelectedItems.Item(0).SubItems(17).Text = TextBoxLS.Text
            ListView1.SelectedItems.Item(0).SubItems(10).Text = strControl()
            ListView1.SelectedItems.Item(0).SubItems(6).Text = TextBoxDAI.Text
        Else
            MsgBox("Need to select the same Bitron PN!")
        End If

    End Sub

    Private Sub ListView1_ColumnClick1(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column)
        ' Call the sort method to manually sort.
        ListView1.Sort()

    End Sub

    Private Sub ListView1_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        Dim mech As String
        If ListView1.SelectedItems.Count = 1 Then
            ComboBoxStatus.Text = ListView1.SelectedItems.Item(0).SubItems(7).Text
            TextBoxDescription.Text = ListView1.SelectedItems.Item(0).SubItems(4).Text
            ComboBoxCustomer.Text = ListView1.SelectedItems.Item(0).SubItems(5).Text
            TextBoxProduct.Text = ListView1.SelectedItems.Item(0).SubItems(3).Text
            TextBoxPcb.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
            TextBoxPiastra.Text = ListView1.SelectedItems.Item(0).SubItems(2).Text
            TextBoxLS.Text = ListView1.SelectedItems.Item(0).SubItems(17).Text
            TextBoxDAI.Text = ListView1.SelectedItems.Item(0).SubItems(6).Text
            ListViewMch.Items.Clear()

            mech = ListView1.SelectedItems.Item(0).SubItems(11).Text

            Dim str(2) As String

            For i = 0 To Int(Len(mech) / 60) - 1
                str(0) = Trim(Mid(mech, i * 60 + 1, 20))
                str(1) = Trim(Mid(mech, i * 60 + 21, 40))
                Dim ii As New ListViewItem(str)
                ListViewMch.Items.Add(ii)
            Next

            CheckBoxCa.Checked = Boopresence("A", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCb.Checked = Boopresence("B", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCc.Checked = Boopresence("C", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCd.Checked = Boopresence("D", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCe.Checked = Boopresence("E", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCf.Checked = Boopresence("F", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCg.Checked = Boopresence("G", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxCh.Checked = Boopresence("H", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxci.Checked = Boopresence("I", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxcl.Checked = Boopresence("L", ListView1.SelectedItems.Item(0).SubItems(10).Text)
            CheckBoxcm.Checked = Boopresence("M", ListView1.SelectedItems.Item(0).SubItems(10).Text)
        Else
            reset()
        End If
    End Sub

    Private Sub ButtonDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDelete.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        If controlRight("W") = 3 Then
            If TextBoxProduct.Text <> "" And MsgBox("Do you want to delete this product?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    sql = "DELETE FROM `" & DBName & "`.`product` WHERE `product`.`BitronPN` = '" & TextBoxProduct.Text & "'"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    reset()
                    ComunicationLog("5052") ' Product Deleted
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql delete error 
                End Try

            Else
                ComunicationLog("5049") ' please fill all field before update
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If
        FillListView()
    End Sub

    Private Sub ButtonQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonQuery.Click

        FillListView()

    End Sub

    Private Sub ButtonReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReset.Click
        reset()
    End Sub

    Private Sub ButtonCustomerAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCustomerAdd.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        sql = InputBox("Please write the new customer name", "New Customer - Data input")
        If controlRight("W") = 3 Then
            If sql <> "" Then
                Try
                    sql = "INSERT INTO `" & DBName & "`.`customer` (`name`  ) VALUES ( '" & UCase(sql) & "');"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                    ComunicationLog("5051") ' Customer insert
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql customer insert error
                End Try

            Else
                ComunicationLog("5049") ' please fill the box
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If
        FillCustomerCombo()
    End Sub

    Private Sub ButtonDeleteCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDeleteCustomer.Click
        Dim cmd As New MySqlCommand()
        Dim sql As String
        If controlRight("W") = 3 Then
            If ComboBoxCustomer.Text <> "" And MsgBox("Do you want to delete this Customer?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    sql = "DELETE FROM `" & DBName & "`.`customer` WHERE `customer`.`name` = '" & ComboBoxCustomer.Text & "'"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql delete error 
                End Try

            Else
                ComunicationLog("5049") ' please fill all field before update
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If
        FillCustomerCombo()
    End Sub

    Private Sub ButtonAddMch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAddMch.Click
        Dim pos As Integer, exist As Boolean
        If ComboBoxMch.Text <> "" Then
            If ListViewMch.Items.Count > 0 Then
                For i = 0 To ListViewMch.Items.Count - 1
                    If Trim(ListViewMch.Items(i).SubItems(0).Text) = Mid(Trim(ComboBoxMch.Text), 1, InStr(Trim(ComboBoxMch.Text), "-", CompareMethod.Text) - 2) Then
                        exist = True
                        ComunicationLog("5070") ' product exist in list
                    End If
                Next
            End If
            If Not exist Then
                pos = InStr(ComboBoxMch.Text, "-", CompareMethod.Text)
                Dim str(2) As String
                str(0) = Mid(ComboBoxMch.Text, 1, pos - 2)
                str(1) = Mid(ComboBoxMch.Text, pos + 2)
                Dim ii As New ListViewItem(str)
                ListViewMch.Items.Add(ii)
            End If
        Else
            ComunicationLog("0050") 'Please select an element
        End If
    End Sub

    Private Sub ButtonRemoveMch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonRemoveMch.Click

        If ListViewMch.Items.Count > 0 Then
            For i = 0 To ListViewMch.Items.Count - 1
                Try
                    If ListViewMch.Items(i).Checked = True Then
                        ListViewMch.Items(i).Remove()
                    End If
                Catch ex As Exception

                End Try

            Next
        End If

    End Sub

    ' FUNCTION
    Function strControl() As String
        strControl = "A" & IIf(CheckBoxCa.Checked, "1", "0") &
        "B" & IIf(CheckBoxCb.Checked, "1", "0") &
        "C" & IIf(CheckBoxCc.Checked, "1", "0") &
        "D" & IIf(CheckBoxCd.Checked, "1", "0") &
        "E" & IIf(CheckBoxCe.Checked, "1", "0") &
        "F" & IIf(CheckBoxCf.Checked, "1", "0") &
        "G" & IIf(CheckBoxCg.Checked, "1", "0") &
        "H" & IIf(CheckBoxCh.Checked, "1", "0") &
        "I" & IIf(CheckBoxci.Checked, "1", "0") &
        "L" & IIf(CheckBoxcl.Checked, "1", "0") &
        "M" & IIf(CheckBoxcm.Checked, "1", "0")
    End Function

    Function Boopresence(ByVal strFlag As String, ByVal strControl As String) As Boolean
        If strControl <> "" Then
            Boopresence = IIf(Mid(strControl, InStr(1, strControl, strFlag) + 1, 1) = 1, True, False)
        End If
    End Function

    Sub fillEcrComboMch()
        ListViewMch.Clear()
        Dim i As Integer, result As DataRow()
        result = tblDoc.Select("header = '" & ParameterTable("plant") & "R_PRO_MED'")
        ComboBoxMch.Items.Clear()
        For i = 0 To result.Length - 1
            ComboBoxMch.Items.Add(result(i).Item("filename").ToString)
        Next
        If ComboBoxMch.Items.Count > 0 Then ComboBoxMch.Text = ComboBoxMch.Items(ComboBoxMch.Items.Count - 1)
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

            ListBoxLog.BackColor = Color.LightGreen

        ElseIf Val(ComCode) < 5000 Then

            ListBoxLog.BackColor = Color.OrangeRed

        End If
    End Sub

    Sub reset()
        ComboBoxStatus.Text = ""
        ComboBoxCustomer.Text = ""
        TextBoxDescription.Text = ""
        TextBoxProduct.Text = ""
        TextBoxPiastra.Text = ""
        TextBoxPcb.Text = ""
        TextBoxLS.Text = ""
        TextBoxDAI.Text = ""
        ListViewMch.Items.Clear()
        ComboBoxMch.Text = ""

        CheckBoxCa.Checked = False
        CheckBoxCb.Checked = False
        CheckBoxCc.Checked = False
        CheckBoxCd.Checked = False
        CheckBoxCe.Checked = False
        CheckBoxCf.Checked = False
        CheckBoxCg.Checked = False
        CheckBoxCh.Checked = False
        CheckBoxci.Checked = False
        CheckBoxcl.Checked = False
        CheckBoxcm.Checked = False

    End Sub

    Sub FillCustomerCombo()
        Dim rowResults As DataRow()

        ComboBoxCustomer.Items.Clear()
        ComboBoxCustomer.Items.Add("")
        DsCus.Clear()
        tblCus.Clear()
        AdapterCus.Fill(DsCus, "Customer")
        tblCus = DsCus.Tables("Customer")
        rowResults = tblCus.Select("name like '*'", "name")
        For Each row In rowResults
            ComboBoxCustomer.Items.Add(row("name").ToString)
        Next
        ComboBoxCustomer.Sorted = True
    End Sub

    Sub FillListView()


        Dim rowShow As DataRow()
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Update(DsProd, "product")
        AdapterProd.Fill(DsProd, "product")

        tblProd = DsProd.Tables("product")

        rowShow = tblProd.Select("Status like '*" & IIf(Trim(ComboBoxStatus.Text) <> "", Trim(ComboBoxStatus.Text), "*") &
        "*' and bitronpn like '*" & IIf(TextBoxProduct.Text <> "", TextBoxProduct.Text, "*") &
        "*' and customer like '*" & IIf(ComboBoxCustomer.Text <> "", ComboBoxCustomer.Text, "*") &
        "*' and pcbCode like '*" & IIf(TextBoxPcb.Text <> "", TextBoxPcb.Text, "*") &
        "*' and dai like '*" & IIf(TextBoxDAI.Text <> "", TextBoxDAI.Text, "*") &
        "*' and PiastraCode like '*" & IIf(TextBoxPiastra.Text <> "", TextBoxPiastra.Text, "*") &
        "*' and " & IIf(ComboBoxStatus.Text = "OBSOLETE", "Status like 'OBSOLETE", "not Status like 'OBSOLETE") &
        "*' and name like '*" & IIf(Trim(TextBoxDescription.Text) <> "", TextBoxDescription.Text, "*") & "*'", "Customer")

        ListView1.Clear()
        ListBoxLog.Items.Add("Finded " & rowShow.Length & " product")
        ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
        ListBoxLog.ScrollAlwaysVisible = True
        Dim c As DataColumn, i As Integer, strPrevDoc As String
        Dim Widht(tblProd.Columns.Count - 1) As Integer
        If CheckBoxVis.Checked Then
            Widht(0) = 0  ' 
            Widht(1) = 0  ' 
            Widht(2) = 0
            Widht(3) = 140
            Widht(4) = 370
            Widht(5) = 160
            Widht(6) = 160
            Widht(7) = 180
            Widht(8) = 0
            Widht(9) = 0
            Widht(10) = 0
            Widht(11) = 0
            Widht(12) = 0
            Widht(13) = 0
            Widht(14) = 0
            Widht(15) = 80
            Widht(16) = 400  ' ecr
            Widht(17) = 100   ' ls
            Widht(18) = 100   ' bom value
            Widht(19) = 120   ' bom ratio
            Widht(20) = 0
            Widht(21) = 100
            Widht(22) = 100
            Widht(23) = 130  ' etd
            Widht(24) = 70
            Widht(25) = 0
            Widht(26) = 200  ' name activity
            Widht(27) = 0
            Widht(28) = 0
            Widht(29) = 0
        Else
            Widht(0) = 0  ' 
            Widht(1) = 0  ' 
            Widht(2) = 0
            Widht(3) = 140
            Widht(4) = 170
            Widht(5) = 0
            Widht(6) = 160
            Widht(7) = 160
            Widht(8) = 0
            Widht(9) = 0
            Widht(10) = 0
            Widht(11) = 0
            Widht(12) = 0
            Widht(13) = 0
            Widht(14) = 0
            Widht(15) = 0
            Widht(16) = 0  ' ecr
            Widht(17) = 0   ' ls
            Widht(18) = 100   ' bom value
            Widht(19) = 100   ' bom ratio
            Widht(20) = 0
            Widht(21) = 50
            Widht(22) = 50
            Widht(23) = 130  ' etd
            Widht(24) = 70
            Widht(25) = 0
            Widht(26) = 300  ' name activity
            Widht(27) = 0
            Widht(28) = 0
            Widht(29) = 0

        End If

        i = 0
        For Each c In tblProd.Columns
            'adding names of columns as Listview columns				
            Dim h As New ColumnHeader
            h.Text = c.ColumnName
            h.Width = Widht(i)
            ListView1.Columns.Add(h)
            i = i + 1
        Next

        Dim str(tblProd.Columns.Count - 1) As String
        'adding Datarows as listview Grids
        strPrevDoc = ""
        For i = 0 To rowShow.Length - 1
            For col As Integer = 0 To tblProd.Columns.Count - 1
                str(col) = UCase(rowShow(i).ItemArray(col).ToString())
            Next
            Dim ii As New ListViewItem(str)
            ListView1.Items.Add(ii)

            'ListView1.Items(ListView1.Items.Count - 1).SubItems(6).Text = mpaFunction(ListView1.Items(ListView1.Items.Count - 1).SubItems(3).Text)
            ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.White

            If ListView1.Items(ListView1.Items.Count - 1).SubItems(14).Text <> "" Then
                ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.LightCoral
            End If

            'If ListView1.Items(ListView1.Items.Count - 1).SubItems(1).Text = "" Or _
            '   ListView1.Items(ListView1.Items.Count - 1).SubItems(8).Text = "" Or _
            '   ListView1.Items(ListView1.Items.Count - 1).SubItems(2).Text = "" Or _
            '   ListView1.Items(ListView1.Items.Count - 1).SubItems(11).Text = "" Or _
            '   ListView1.Items(ListView1.Items.Count - 1).SubItems(4).Text = "" _
            'Then ListView1.Items(ListView1.Items.Count - 1).BackColor = Color.LightPink

        Next
        ListView1.Refresh()
    End Sub

    Private Sub ButtonGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonGroup.Click
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")

        Dim i As Integer, result As DataRow()
        GroupList = ""
        If TextBoxProduct.Text <> "" Then

            result = tblProd.Select("BitronPN = '" & TextBoxProduct.Text & "'")
            If result.Length > 0 Then
                GroupList = result(0).Item("groupList").ToString

                result = tbltype.Select("id > 0")
                FormGroup.ComboBoxGroup.Items.Clear()
                For i = 0 To result.Length - 1
                    If controlRight(Mid(result(i).Item("header").ToString, 3, 1)) >= 2 Then
                        FormGroup.ComboBoxGroup.Items.Add(result(i).Item("header").ToString & " --> " _
                        & result(i).Item("firstType").ToString & " --> " _
                        & result(i).Item("secondType").ToString & " --> " _
                        & result(i).Item("thirdtype").ToString)
                    End If
                Next
                If FormGroup.ComboBoxGroup.Items.Count > 0 Then FormGroup.ComboBoxGroup.Text = FormGroup.ComboBoxGroup.Items(FormGroup.ComboBoxGroup.Items.Count - 1)
                FormGroup.Show()
            End If
        End If

    End Sub

    Private Sub ButtonOpenIssue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenIssue.Click

        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        User3 = user()

        Dim result As DataRow()
        OpenIssue = ""
        If TextBoxProduct.Text <> "" Then

            result = tblProd.Select("BitronPN = '" & TextBoxProduct.Text & "'")
            If result.Length > 0 Then
                OpenIssue = result(0).Item("OpenIssue").ToString
                ProdOpenIssue = result(0).Item("bitronpn").ToString

                If controlRight("W") >= 2 Then
                    If controlRight("U") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("PURCHASING")
                    If controlRight("L") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("LOGISTIC")
                    If controlRight("N") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("QUALITY")
                    If controlRight("Q") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("TIME&MOTION")
                    If controlRight("E") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("TESTING ENGINEERING")
                    If controlRight("P") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("PRODUCTION")
                    If controlRight("R") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("R&D")
                    If controlRight("C") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("CUSTOMER SERVICE")
                    If controlRight("F") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("FINANCIAL")
                    If controlRight("B") >= 2 Then FormOpenIssue.ComboBoxGroup.Items.Add("PROCESS ENGINEERING")
                End If

                FormOpenIssue.ComboBoxGroup.Text = ""
                FormOpenIssue.Show()



            End If
        End If

    End Sub

    Private Sub ButtonStatusUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonStatusUP.Click
        Dim currentStatus As String
        Dim result As DataRow()


        User3 = user()
        If controlRight("W") >= 3 Then
            If TextBoxProduct.Text <> "" Then

                result = tblProd.Select("BitronPN = '" & TextBoxProduct.Text & "'")
                If result.Length = 1 Then
                    If (result(0).Item("mail").ToString <> "SENT") Or (ComboBoxStatus.Text = "OBSOLETE" And controlRight("R") >= 2) Then
                        currentStatus = result(0).Item("status").ToString
                        If controlRight("R") >= 2 Then
                            If (ComboBoxStatus.Text = "OBSOLETE" Or ComboBoxStatus.Text = "" Or ComboBoxStatus.Text = "SOP_SAMPLE" Or ComboBoxStatus.Text = "R&D_APPROVED") _
                                And (currentStatus = "MPA_APPROVED" And ComboBoxStatus.Text = "OBSOLETE" Or currentStatus = "OBSOLETE" Or currentStatus = "" Or currentStatus = "SOP_SAMPLE" Or currentStatus = "R&D_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status """"; ""OBSOLETE"" ; ""SOP_SAMPLE""; ""R&D_APPROVED""; ")
                            End If
                        End If

                        If User3 = "L" Then
                            If (ComboBoxStatus.Text = "R&D_APPROVED" Or ComboBoxStatus.Text = "LOGISTIC_APPROVED") _
                            And (currentStatus = "R&D_APPROVED" Or currentStatus = "LOGISTIC_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""R&D_APPROVED""; ""LOGISTIC_APPROVED"" ")
                            End If
                        End If

                        If User3 = "C" Then
                            If (ComboBoxStatus.Text = "CUSTOMER_APPROVED" Or ComboBoxStatus.Text = "LOGISTIC_APPROVED") _
                            And (currentStatus = "CUSTOMER_APPROVED" Or currentStatus = "LOGISTIC_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""CUSTOMER_APPROVED""; ""LOGISTIC_APPROVED"" ")
                            End If
                        End If

                        If User3 = "U" Then
                            If (ComboBoxStatus.Text = "PURCHASING_APPROVED" Or ComboBoxStatus.Text = "CUSTOMER_APPROVED") _
                                And (currentStatus = "PURCHASING_APPROVED" Or currentStatus = "CUSTOMER_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""PURCHASING_APPROVED""; ""CUSTOMER_APPROVED"";")
                            End If
                        End If


                        If User3 = "P" Then
                            If (ComboBoxStatus.Text = "PRODUCTION_APPROVED" Or ComboBoxStatus.Text = "PURCHASING_APPROVED") _
                                And (currentStatus = "PRODUCTION_APPROVED" Or currentStatus = "PURCHASING_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""PRODUCTION_APPROVED""; ""PURCHASING_APPROVED"" ")
                            End If
                        End If

                        If User3 = "Q" Then
                            If (ComboBoxStatus.Text = "TIME&MOTION_APPROVED" Or ComboBoxStatus.Text = "PRODUCTION_APPROVED") _
                                And (currentStatus = "TIME&MOTION_APPROVED" Or currentStatus = "PRODUCTION_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""TIME&MOTION_APPROVED""; ""PRODUCTION_APPROVED"" ")
                            End If
                        End If

                        If User3 = "E" Then
                            If (ComboBoxStatus.Text = "TESTING_ENG_APPROVED" Or ComboBoxStatus.Text = "TIME&MOTION_APPROVED") _
                                And (currentStatus = "TESTING_ENG_APPROVED" Or currentStatus = "TIME&MOTION_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""TIME&MOTION_APPROVED""; ""TESTING_ENG_APPROVED"" ")
                            End If
                        End If

                        If User3 = "B" Then
                            If (ComboBoxStatus.Text = "PROCESS_ENG_APPROVED" Or ComboBoxStatus.Text = "TESTING_ENG_APPROVED") _
                                And (currentStatus = "TESTING_ENG_APPROVED" Or currentStatus = "PROCESS_ENG_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""PROCESS_ENG_APPROVED""; ""TESTING_ENG_APPROVED"" ")
                            End If
                        End If


                        If User3 = "F" Then
                            If (ComboBoxStatus.Text = "FINANCIAL_APPROVED" Or ComboBoxStatus.Text = "PROCESS_ENG_APPROVED") _
                                And (currentStatus = "PROCESS_ENG_APPROVED" Or currentStatus = "FINANCIAL_APPROVED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""PROCESS_ENG_APPROVED""; ""FINANCIAL_APPROVED"" ")
                            End If
                        End If

                        If User3 = "N" Then
                            If (ComboBoxStatus.Text = "MPA_APPROVED" Or ComboBoxStatus.Text = "MPA_STOPPED" Or ComboBoxStatus.Text = "MPA_STOPPED") _
                                And (currentStatus = "FINANCIAL_APPROVED" Or currentStatus = "MPA_APPROVED" Or currentStatus = "MPA_STOPPED") Then
                                StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                            Else
                                MsgBox("You can update only the product in status ""FINANCIAL_APPROVED"" or ""MPA_APPROVED"" ""MPA_STOP"" ")
                            End If
                        End If

                    ElseIf User3 = "N" And (result(0).Item("status").ToString = "MPA_APPROVED" Or result(0).Item("status").ToString = "MPA_STOPPED") And result(0).Item("mail").ToString = "SENT" And (ComboBoxStatus.Text = "MPA_APPROVED" Or ComboBoxStatus.Text = "MPA_STOPPED") Then
                        StatusUpdate(result(0).Item("StatusUpdateDate").ToString)
                    Else
                        MsgBox("Product already with MPA SENT, only the Quality Dept. can change status in MPA_STOPPED")
                    End If
                Else
                    MsgBox("Product not found, please update the table")
                End If
            Else
                MsgBox("Please select a product before using this function!")
            End If
        Else
            MsgBox("Need W3 level to update status of a product!")
        End If

    End Sub

    Sub StatusUpdate(ByVal StatusUpdateDate As String)

        Dim cmd As New MySqlCommand()
        Dim sql As String


        If controlRight("W") >= 2 Then
            If TextBoxProduct.Text <> "" And TextBoxDescription.Text <> "" Then
                Try
                    sql = "UPDATE `" & DBName & "`.`product` SET `StatusUpdateDate` = '" & Trim(ComboBoxStatus.Text) & "[" & string_to_date(Today.Year & "/" & Today.Month & "/" & Today.Day) & "]" & StatusUpdateDate & "',`Status` = '" & Trim(ComboBoxStatus.Text) & "', `MAIL` = '' WHERE `product`.`BitronPN` = '" & Trim(TextBoxProduct.Text) & "' ;"
                    cmd = New MySqlCommand(sql, MySqlconnection)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    ComunicationLog("5050") ' Mysql update query error 
                End Try

            Else
                ComunicationLog("5049") ' please fill all field before update
            End If
        Else
            ComunicationLog("0043") ' no enough right
        End If

        griddUpdate(ListView1.SelectedItems.Item(0).SubItems(3).Text = TextBoxProduct.Text)
        ListBoxLog.Items.Add(ListView1.SelectedItems.Item(0).SubItems(3).Text & "  -  Status Updated!")
    End Sub

    Private Sub ButtonOpenIssuePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOpenIssuePrint.Click

        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        User3 = user()
        WriteFile("", False)
        Dim i As Integer, result As DataRow(), k As Integer, j As Integer
        OpenIssue = ""
        result = tblProd.Select("not status = 'OBSOLETE'")
        For Each res In result
            OpenIssue = res("OpenIssue").ToString

            If OpenIssue <> "" Then
                Dim str(2) As String
                k = 1
                i = InStr(OpenIssue, "[", CompareMethod.Text)
                j = InStr(OpenIssue, "]", CompareMethod.Text)
                While j > 0
                    str(0) = Mid(OpenIssue, k, i - k)
                    str(1) = Mid(OpenIssue, i + 1, j - 1 - i)
                    WriteFile(res("status").ToString & " ; " & str(0) & " ; " & res("bitronpn").ToString & " ; " & res("name").ToString & " ; " & str(1), True)
                    k = j + 2
                    i = InStr(j, OpenIssue, "[", CompareMethod.Text)
                    j = InStr(j + 1, OpenIssue, "]", CompareMethod.Text)
                End While
            End If

        Next
        SaveFileDialog1.FileName = System.IO.Path.GetTempPath & "SrvQueryLog.txt"
        SaveFileDialog1.ShowDialog()
        Try
            FileCopy(System.IO.Path.GetTempPath & "SrvQueryLog.txt", SaveFileDialog1.FileName)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ButtonSIGIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSIGIP.Click
        If controlRight("R") >= 2 And controlRight("J") >= 2 Then

            If InStr(ParameterTable("LAST_SIGIP_BOM_UPDATE"), "DONE", CompareMethod.Text) > 0 Then

                ParameterTableWrite("LAST_SIGIP_BOM_UPDATE", "START - " & CreAccount.strUserName & " " & Today)
                FolderBrowserDialog1.SelectedPath = ParameterTable("PathMorpheus") & ParameterTable("PathNPI") & ParameterTable("SIGIP_BOM_FOLDER")
                FolderBrowserDialog1.Description = "Please select SIGIP BOM folder"
                If vbOK = FolderBrowserDialog1.ShowDialog() Then
                    Try
                        DsSigip.Clear()
                        tblSigip.Clear()
                    Catch ex As Exception

                    End Try

                    AdapterSigip.Fill(DsSigip, "sigip")
                    tblSigip = DsSigip.Tables("sigip")
                    Dim cmd As New MySqlCommand(), sql As String
                    Try
                        sql = "DELETE FROM `" & DBName & "`.`sigip` "
                        cmd = New MySqlCommand(sql, MySqlconnection)
                        cmd.ExecuteNonQuery()
                        reset()
                    Catch ex As Exception
                        ComunicationLog("5050") ' Mysql delete error 
                    End Try

                    index = 1
                    Dim FileContenuti() As IO.FileInfo = New IO.DirectoryInfo(FolderBrowserDialog1.SelectedPath).GetFiles()
                    Dim i As Integer
                    For i = 0 To FileContenuti.Length - 1
                        If Mid(FileContenuti(i).ToString, Len(FileContenuti(i).ToString) - 2, 3) = "xls" Then

                            InsertSigipBomXLS(FolderBrowserDialog1.SelectedPath & "\" & FileContenuti(i).ToString)

                        Else
                            MsgBox("File should be xls!")
                        End If
                    Next i

                    ListBoxLog.Items.Add("Update product list...")
                    updateSigipMark()

                    ListBoxLog.Items.Add("Update product cost...")
                    UpdateBomCost()
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

                    If SqlconnectionOrcad.State = ConnectionState.Open Then
                        ListBoxLog.Items.Add("Update Component Doc...")
                        updateSigipBomOrcadDoc()
                        ParameterTableWrite("LAST_SIGIP_BOM_UPDATE", "DONE - " & CreAccount.strUserName & " " & Today & " - All OK")
                    Else
                        MsgBox("Orcad connection problem, HC not filled")
                        ParameterTableWrite("LAST_SIGIP_BOM_UPDATE", "DONE - " & CreAccount.strUserName & " " & Today & " " & " Orcad Error")
                    End If



                End If
                ButtonQuery_Click(Me, e)
                ListBoxLog.Items.Add("Process END")
                ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
                ListBoxLog.ScrollAlwaysVisible = True
            Else
                MsgBox("Functionality already in use... " & ParameterTable("LAST_SIGIP_BOM_UPDATE"))
                If MsgBox("Do you want to reset the functionality and invalid previous job?", MsgBoxStyle.YesNo) = vbYes Then
                    ParameterTableWrite("LAST_SIGIP_BOM_UPDATE", "DONE - " & CreAccount.strUserName & " " & Today & " " & " Reset")
                End If
            End If
        Else
            ListBoxLog.Items.Add("No enought right for this operation...")
        End If
    End Sub
    ' "InsertSigipBom" is not used
    'Sub InsertSigipBom(ByVal sfilename As String)
    '    ParameterTableWrite("LAST_BOM_UPDATE", "Start but not finish....")
    '    '1         09001495                NR       0,031250   0,012556                                                                                                                             0,012556*
    '    '                                                                                                                                                                                                   *D1
    '    '      ACQ COCONUT-SHELL ACTIVATED CARBON 15  CNFG                                                                                                                                          ///      D2
    '    '          Fornitore 1: 770155S QINGDAO XIANGYUAN DESICCA   % Ass:  100                                                                                                                              D3

    '    '1         20418867                NR       1,000000                                               0,150316   0,257094   0,105492   0,081500                                                0,594402*
    '    Dim cmd As New MySqlCommand(), sLine As String, nr As String
    '    Dim sql As String = "", bom As String, currency As String, bitron_pn As String, des As String, qt As String, price As String
    '    Dim liv As String, acq_fab As String, despn As String, supp1 As String, ass1 As String, des1 As String, supp2 As String, ass2 As String, des2 As String
    '    Dim sReader = New IO.StreamReader(sfilename)
    '    Dim mdo As String, mdi As String, amm As String, spe As String
    '    Dim cb As New MySqlCommandBuilder(AdapterSigip)

    '    ListBoxLog.Items.Clear()

    '    bom = ""
    '    des = ""
    '    currency = ""



    '    For i = 1 To 100
    '        sLine = sReader.ReadLine
    '        sLine = Replace(sLine, "Valuta di stampa ", "Printout Currency")
    '        sLine = Replace(sLine, "Assieme ", "Assembly")
    '        If InStr(sLine, "Printout Currency") > 0 Then
    '            currency = Trim(Mid(sLine, InStr(sLine, "currency", CompareMethod.Text) + 8, 40))
    '        End If
    '        If InStr(sLine, "Assembly") > 0 Then
    '            bom = Trim(Mid(sLine, InStr(sLine, "Assembly", CompareMethod.Text) + 8, 20))
    '            des = Trim(Mid(sLine, InStr(sLine, "Assembly", CompareMethod.Text) + 29, 50))
    '            Exit For
    '        End If
    '        If i = 49 Then MsgBox("BOM not full recognized, please check the txt format: " & sfilename)
    '    Next




    '    If bom = Mid(sfilename, Len(sfilename) - 3 - Len(bom), Len(bom)) Then
    '        ListBoxLog.Items.Add("Process BOM: " & bom)
    '        Application.DoEvents()
    '        ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
    '        ListBoxLog.ScrollAlwaysVisible = True

    '        sLine = sReader.ReadLine
    '        While Not sReader.EndOfStream

    '            supp1 = ""
    '            ass1 = ""
    '            des1 = ""
    '            supp2 = ""
    '            ass2 = ""
    '            des2 = ""
    '            despn = ""
    '            price = ""
    '            liv = ""
    '            qt = ""
    '            nr = ""
    '            bitron_pn = ""
    '            despn = ""
    '            mdo = ""
    '            mdi = ""
    '            amm = ""
    '            spe = ""
    '            If Mid(sLine, 1, 1) = "1" Or Mid(sLine, 1, 1) = "." Then
    '                bitron_pn = Trim(Mid(sLine, 10, 24))
    '                nr = Trim(Mid(sLine, 35, 8))
    '                qt = Replace(Trim(Mid(sLine, 41, 11)), ",", ".")
    '                price = Replace(Trim(Mid(sLine, 181, 14)), ",", ".")
    '                mdo = Replace(Trim(Mid(sLine, 99, 8)), ",", ".")
    '                If mdi <> "" Then Stop
    '                mdi = Replace(Trim(Mid(sLine, 110, 8)), ",", ".")
    '                amm = Replace(Trim(Mid(sLine, 121, 8)), ",", ".")
    '                spe = Replace(Trim(Mid(sLine, 132, 8)), ",", ".")

    '                liv = Mid(sLine, 1, 10)
    '                sLine = sReader.ReadLine()
    '                acq_fab = Replace(Trim(Mid(sLine, 1, 10)), ".", "")
    '                If acq_fab = "acq" Or acq_fab = "fab" Or acq_fab = "acv" Then

    '                Else
    '                    sLine = sReader.ReadLine()
    '                    acq_fab = Replace(Trim(Mid(sLine, 1, 10)), ".", "")
    '                End If
    '                despn = Trim(Mid(sLine, 10, 36))
    '                sLine = sReader.ReadLine()
    '                If Mid(sLine, 11, 9) = "fornitore" Then
    '                    supp1 = Trim(Mid(sLine, 23, 8))
    '                    ass1 = Trim(Mid(sLine, 68, 3))
    '                    des1 = Trim(Mid(sLine, 30, 30))
    '                    supp2 = Trim(Mid(sLine, 89, 8))
    '                    ass2 = (Mid(sLine, 132, 3))
    '                    des2 = Trim(Mid(sLine, 97, 28))
    '                Else

    '                End If


    '                sql = "(" & index & "," & _
    '                "'" & bom & "'," & _
    '                "'" & Replace(des, "'", "") & "'," & _
    '                "'" & nr & "'," & _
    '                "'" & (qt) & "'," & _
    '                "'" & (price) & "'," & _
    '                "'" & currency & "'," & _
    '                "'" & liv & "'," & _
    '                "'" & acq_fab & "'," & _
    '                "'" & bitron_pn & "'," & _
    '                "'" & supp1 & "'," & _
    '                "'" & (ass1) & "'," & _
    '                "'" & Replace(des1, "'", "") & "'," & _
    '                "'" & supp2 & "'," & _
    '                "'" & (ass2) & "'," & _
    '                "'" & Replace(des2, "'", "") & "'," & _
    '                "'" & ReplaceChar(despn) & "'," & _
    '                "'" & mdi & "'," & _
    '                "'" & mdo & "'," & _
    '                "'" & amm & "'," & _
    '                "'" & spe & "'" & _
    '                 ")," & sql

    '                Try
    '                    sql = Mid(sql, 1, Len(sql) - 1)
    '                    sql = "INSERT INTO `" & DBName & "`.`sigip` (`id` ,`bom`,`DES_bom`,`NR`,`QT` ,`price` ,`currency`,`liv`,`acq_fab` ,`bitron_pn`  ,`pn_supp1`,`ass1`,`des_supp1`,`pn_supp2`,`ass2`,`des_supp2` ,`DES_PN`,`mdi`,`mdo`,`amm`,`spe`) VALUES " & sql & ";"
    '                    cmd = New MySqlCommand(sql, MySqlconnection)
    '                    cmd.ExecuteNonQuery()
    '                    sql = ""
    '                Catch ex As Exception
    '                    MsgBox("Sigip update error! " & ex.Message)
    '                End Try

    '                index = index + 1

    '            Else


    '            End If
    '            sLine = sReader.ReadLine

    '        End While
    '        '   Application.DoEvents()
    '    Else

    '        MsgBox("Mismatch between filename and PN_BOM inside the file: " & sfilename)
    '    End If
    '    sReader.Close()


    'End Sub

    ' BEC
    'Sub InsertSigipBomXLS(ByVal sfilename As String)

    '    Dim cmd As New MySqlCommand(), nr As String
    '    Dim sql As String = "", bom As String, currency As String, bitron_pn As String, des As String, qt As String, price As String
    '    Dim liv As String, acq_fab As String, despn As String, mdo_t As String, mdi_t As String, spe_t As String, amm_t As String
    '    Dim mdo As String, mdi As String, amm As String, spe As String
    '    Dim cb As New MySqlCommandBuilder(AdapterSigip), xlsRow As Integer

    '    'Dim excel() As Process = Process.GetProcessesByName("excel")
    '    'For Each Process As Process In excel
    '    '    Process.Kill()
    '    'Next

    '    'open the offer template
    '    Dim excelApp As New Object
    '    excelApp = CreateObject("Excel.Application")

    '    Dim excelWorkbook As Object
    '    Dim excelSheet As Object
    '    Try
    '        excelWorkbook = excelApp.Workbooks.Open(sfilename)
    '        excelWorkbook.Activate()
    '        excelSheet = excelWorkbook.Worksheets("Foglio 1")
    '        excelSheet.Activate()


    '        bom = excelSheet.Cells(1, 2).Text
    '        sql = ""
    '        des = excelSheet.Cells(1, 3).Text
    '        mdi_t = Replace(excelSheet.Cells(1, 11).Text, ",", ".")
    '        mdo_t = Replace(excelSheet.Cells(1, 10).Text, ",", ".")
    '        amm_t = Replace(excelSheet.Cells(1, 12).Text, ",", ".")
    '        spe_t = Replace(excelSheet.Cells(1, 13).Text, ",", ".")

    '        If bom = Mid(sfilename, Len(sfilename) - 3 - Len(bom), Len(bom)) Then
    '            ListBoxLog.Items.Add("Process BOM: " & bom)
    '            Application.DoEvents()
    '            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
    '            ListBoxLog.ScrollAlwaysVisible = True

    '            xlsRow = 3
    '            While excelSheet.Cells(xlsRow, 1).text <> ""


    '                nr = excelSheet.Cells(xlsRow, 5).Text
    '                qt = Replace(excelSheet.Cells(xlsRow, 6).Text, ",", ".")
    '                price = Replace(excelSheet.Cells(xlsRow, 17).Text, ",", ".")
    '                currency = ""
    '                liv = excelSheet.Cells(xlsRow, 1).Text
    '                acq_fab = excelSheet.Cells(xlsRow, 4).Text
    '                bitron_pn = excelSheet.Cells(xlsRow, 2).Text
    '                despn = excelSheet.Cells(xlsRow, 3).Text
    '                mdi = Replace(excelSheet.Cells(xlsRow, 12).Text, ",", ".")
    '                mdo = Replace(excelSheet.Cells(xlsRow, 11).Text, ",", ".")
    '                amm = Replace(excelSheet.Cells(xlsRow, 13).Text, ",", ".")
    '                spe = Replace(excelSheet.Cells(xlsRow, 14).Text, ",", ".")



    '                sql = "(" & index & "," & _
    '                "'" & bom & "'," & _
    '                "'" & Replace(des, "'", "") & "'," & _
    '                "'" & nr & "'," & _
    '                "'" & (qt) & "'," & _
    '                "'" & (price) & "'," & _
    '                "'" & currency & "'," & _
    '                "'" & liv & "'," & _
    '                "'" & acq_fab & "'," & _
    '                "'" & Replace(ReplaceChar(bitron_pn), "-", "") & "'," & _
    '                "'" & ReplaceChar(despn) & "'," & _
    '                "'" & mdi & "'," & _
    '                "'" & mdo & "'," & _
    '                "'" & amm & "'," & _
    '                "'" & spe & "'," & _
    '                "'" & mdi_t & "'," & _
    '                "'" & mdo_t & "'," & _
    '                "'" & amm_t & "'," & _
    '                "'" & spe_t & "'" & _
    '                 ")," & sql

    '                If excelApp.Cells(xlsRow + 1, 1).text = "" Or Int(xlsRow / 100) = xlsRow / 100 Then
    '                    Try
    '                        sql = Mid(sql, 1, Len(sql) - 1)
    '                        sql = "INSERT INTO `" & DBName & "`.`sigip` (`id` ,`bom`,`DES_bom`,`NR`,`QT` ,`price` ,`currency`,`liv`,`acq_fab` ,`bitron_pn` ,`DES_PN`,`mdi`,`mdo`,`amm`,`spe`,`mdi_t`,`mdo_t`,`amm_t`,`spe_t`) VALUES " & sql & ";"
    '                        cmd = New MySqlCommand(sql, MySqlconnection)
    '                        cmd.ExecuteNonQuery()
    '                        sql = ""
    '                    Catch ex As Exception
    '                        MsgBox("Sigip update error! " & ex.Message)
    '                    End Try

    '                End If
    '                xlsRow = xlsRow + 1
    '                index = index + 1
    '            End While

    '        Else
    '            MsgBox("Mismatch between filename and product inside the file! File not considered " & sfilename)
    '        End If
    '        excelWorkbook.Close(True)
    '        excelApp.Quit()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    ' CO18 in BitronPoland
    Sub InsertSigipBomXLS(ByVal sfilename As String)

        Dim cmd As New MySqlCommand(), nr As String
        Dim sql As String = "", bom As String, currency As String, bitron_pn As String, des As String, qt As String, price As String
        Dim liv As String, acq_fab As String, despn As String, mdo_t As String, mdi_t As String, spe_t As String, amm_t As String
        Dim mdo As String, mdi As String, amm As String, spe As String
        Dim cb As New MySqlCommandBuilder(AdapterSigip), xlsRow As Integer

        'Dim excel() As Process = Process.GetProcessesByName("excel")
        'For Each Process As Process In excel
        '    Process.Kill()
        'Next

        'open the offer template


        Dim excelApp As New Object
        excelApp = CreateObject("Excel.Application")

        Dim excelWorkbook As Object
        Dim excelSheet As Object
        Try
            excelWorkbook = excelApp.Workbooks.Open(sfilename)
            excelWorkbook.Activate()
            excelSheet = excelWorkbook.Worksheets("Foglio 1")
            excelSheet.Activate()

            bom = Mid(sfilename, Len(sfilename) - 11, 8)
            sql = ""
            des = ""
            mdi_t = ""
            mdo_t = ""
            amm_t = ""
            spe_t = ""

            ListBoxLog.Items.Add("Process BOM: " & bom)
            Application.DoEvents()
            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
            ListBoxLog.ScrollAlwaysVisible = True

            xlsRow = 2
            While excelSheet.Cells(xlsRow, 1).text <> ""

                If excelSheet.Cells(xlsRow, 3).text <> "" Then 'Non considera le righe contenenti il nome del fornitore

                    nr = excelSheet.Cells(xlsRow, 6).Text
                    qt = Replace(excelSheet.Cells(xlsRow, 7).Text, ",", ".")
                    price = Replace(excelSheet.Cells(xlsRow, 18).Text, ",", ".")
                    currency = ""
                    liv = excelSheet.Cells(xlsRow, 1).Text
                    acq_fab = excelSheet.Cells(xlsRow, 5).Text
                    bitron_pn = excelSheet.Cells(xlsRow, 2).Text
                    despn = excelSheet.Cells(xlsRow, 3).Text
                    mdi = Replace(excelSheet.Cells(xlsRow, 13).Text, ",", ".")
                    mdo = Replace(excelSheet.Cells(xlsRow, 12).Text, ",", ".")
                    amm = Replace(excelSheet.Cells(xlsRow, 14).Text, ",", ".")
                    spe = Replace(excelSheet.Cells(xlsRow, 15).Text, ",", ".")

                    sql = "(" & index & "," &
                    "'" & bom & "'," &
                    "'" & Replace(des, "'", "") & "'," &
                    "'" & nr & "'," &
                    "'" & (qt) & "'," &
                    "'" & (price) & "'," &
                    "'" & currency & "'," &
                    "'" & liv & "'," &
                    "'" & acq_fab & "'," &
                    "'" & Replace(ReplaceChar(bitron_pn), "-", "") & "'," &
                    "'" & ReplaceChar(despn) & "'," &
                    "'" & mdi & "'," &
                    "'" & mdo & "'," &
                    "'" & amm & "'," &
                    "'" & spe & "'," &
                    "'" & mdi_t & "'," &
                    "'" & mdo_t & "'," &
                    "'" & amm_t & "'," &
                    "'" & spe_t & "'" &
                     ")," & sql

                End If
                xlsRow = xlsRow + 1
                index = index + 1
            End While

            Try
                Dim sqlCommand As String
                sqlCommand = Mid(sql, 1, Len(sql) - 1)
                sqlCommand = "INSERT INTO `" & DBName & "`.`sigip` (`id` ,`bom`,`DES_bom`,`NR`,`QT` ,`price` ,`currency`,`liv`,`acq_fab` ,`bitron_pn` ,`DES_PN`,`mdi`,`mdo`,`amm`,`spe`,`mdi_t`,`mdo_t`,`amm_t`,`spe_t`) VALUES " & sqlCommand & ";"
                cmd = New MySqlCommand(sqlCommand, MySqlconnection)
                cmd.ExecuteNonQuery()
                sqlCommand = ""
                sql = ""
            Catch ex As Exception
                MsgBox("Sigip update error! " & ex.Message)
            End Try

            ' *** OLD CODE (with issues during import Sigip BOM process) *** '
            'sql = "(" & index & "," & _
            '"'" & bom & "'," & _
            '"'" & Replace(des, "'", "") & "'," & _
            '"'" & nr & "'," & _
            '"'" & (qt) & "'," & _
            '"'" & (price) & "'," & _
            '"'" & currency & "'," & _
            '"'" & liv & "'," & _
            '"'" & acq_fab & "'," & _
            '"'" & Replace(ReplaceChar(bitron_pn), "-", "") & "'," & _
            '"'" & ReplaceChar(despn) & "'," & _
            '"'" & mdi & "'," & _
            '"'" & mdo & "'," & _
            '"'" & amm & "'," & _
            '"'" & spe & "'," & _
            '"'" & mdi_t & "'," & _
            '"'" & mdo_t & "'," & _
            '"'" & amm_t & "'," & _
            '"'" & spe_t & "'" & _
            ' ")," & sql

            'If excelApp.Cells(xlsRow + 1, 1).text = "" Or Int(xlsRow / 100) = xlsRow / 100 Then
            '    Try
            '        sql = Mid(sql, 1, Len(sql) - 1)
            '        sql = "INSERT INTO `" & DBName & "`.`sigip` (`id` ,`bom`,`DES_bom`,`NR`,`QT` ,`price` ,`currency`,`liv`,`acq_fab` ,`bitron_pn` ,`DES_PN`,`mdi`,`mdo`,`amm`,`spe`,`mdi_t`,`mdo_t`,`amm_t`,`spe_t`) VALUES " & sql & ";"
            '        cmd = New MySqlCommand(sql, MySqlconnection)
            '        cmd.ExecuteNonQuery()
            '        sql = ""
            '    Catch ex As Exception
            '        MsgBox("Sigip update error! " & ex.Message)
            '    End Try

            'End If

            '    End If
            '    xlsRow = xlsRow + 1
            '    index = index + 1
            'End While

            excelWorkbook.Close(True)
            excelApp.Quit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Function ProductStatus(ByVal bom As String) As String


        Dim results As DataRow()

        results = tblProd.Select("bitronpn = '" & bom & "'")
        ProductStatus = ""
        For Each res In results
            ProductStatus = res("status").ToString
        Next

    End Function
    Sub UpdateBomCost()

        Dim cmd As New MySqlCommand(), sql As String
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        DsSigip.Clear()
        tblSigip.Clear()
        AdapterSigip.Fill(DsSigip, "sigip")
        tblSigip = DsSigip.Tables("sigip")

        Dim results As DataRow(), cost As Single, resultSigip As DataRow()

        results = tblProd.Select("status like '*' AND not status ='OBSOLETE'")

        For Each res In results
            ListBoxLog.Items.Add("Update BOM cost: " & res("bitronpn").ToString)
            Application.DoEvents()
            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
            ListBoxLog.ScrollAlwaysVisible = True

            resultSigip = tblSigip.Select("bom = '" & res("bitronpn").ToString & "' and acq_fab = 'acq' and not (bitron_pn like '18*')")
            If resultSigip.Length > 0 Then

                cost = 0
                ListBoxLog.Items.Add("Update cost for " & res("bitronpn").ToString)
                ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
                ListBoxLog.ScrollAlwaysVisible = True
                Application.DoEvents()

                For Each ressigip In resultSigip

                    If Val(ressigip("Price").ToString) > 0 Then
                        cost = cost + Val(ressigip("Price").ToString)
                    Else
                        cost = 0
                        Exit For
                    End If
                Next

            Else
                cost = 0
            End If

            Try
                sql = "UPDATE `" & DBName & "`.`product` SET `bom_val` = '" & Math.Round(cost, 2) & "', `bom_ratio` = '" & If(Val(res("ls_rmb").ToString) > 0, Math.Round(cost / Val(res("ls_rmb").ToString), 2) * 100, 0) & "%'  WHERE `product`.`bitronpn` = '" & res("bitronpn").ToString & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try


        Next


    End Sub

    Sub updateSigipMark()

        Dim cmd As New MySqlCommand(), sql As String
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        DsSigip.Clear()
        tblSigip.Clear()
        AdapterSigip.Fill(DsSigip, "sigip")
        tblSigip = DsSigip.Tables("sigip")

        Dim result As DataRow(), sigip As String, resultSigip As DataRow()

        result = tblProd.Select("status like '*'")

        For Each res In result

            Application.DoEvents()
            ListBoxLog.Items.Add("Update Sigip mark " & res("bitronpn").ToString)
            ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1

            resultSigip = tblSigip.Select("bom = '" & res("bitronpn").ToString & "'")
            If resultSigip.Length > 0 Then
                If ProductStatus(resultSigip(0).Item("bom").ToString) <> "OBSOLETE" Then
                    sigip = "YES"
                Else
                    sigip = "NO"
                End If

            Else
                sigip = "NO"
            End If

            If MySqlconnection.State = ConnectionState.Closed Then
                MySqlconnection.Open()
            End If


            Try
                sql = "UPDATE `" & DBName & "`.`sigip` SET `active` = '" & sigip & "' WHERE `sigip`.`bom` = '" & res("bitronpn").ToString & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try
            Try
                sql = "UPDATE `" & DBName & "`.`product` SET `sigip` = '" & sigip & "' WHERE `product`.`BitronPN` = '" & res("bitronpn").ToString & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try

        Next

    End Sub

    Sub updateECRMark()


        Dim cmd As New MySqlCommand(), sql As String
        DsProd.Clear()
        tblProd.Clear()
        AdapterProd.Fill(DsProd, "product")
        tblProd = DsProd.Tables("product")
        DsEcr.Clear()
        tblEcr.Clear()
        AdapterEcr.Fill(DsEcr, "ecr")
        tblEcr = DsEcr.Tables("ecr")

        Dim result As DataRow(), ecr As String, resultEcr As DataRow()

        result = tblProd.Select("status like '*'")

        For Each res In result

            ecr = ""
            resultEcr = tblEcr.Select("prod like '*" & res("bitronpn").ToString & "*'")
            For Each resEcr In resultEcr

                ecr = ecr & resEcr("number").ToString & "[" & IIf(resEcr("confirm").ToString <> "", "C", "W") & "]" & ";"
            Next

            Try
                sql = "UPDATE `" & DBName & "`.`product` SET `ECR` = '" & ecr & "' WHERE `product`.`BitronPN` = '" & res("bitronpn").ToString & "' ;"
                cmd = New MySqlCommand(sql, MySqlconnection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ComunicationLog("5050") ' Mysql update query error 
            End Try

        Next

    End Sub

    Sub updateSigipBomOrcadDoc()
        Dim cmd As New MySqlCommand()
        Dim sql As String, doc As String
        Dim RowSearchBom As DataRow()
        Dim RowSearchDoc As DataRow()
        Dim RowHC As DataRow()
        Dim i As Long, updated As Boolean

        Dim OrcadDBAdr = ParameterTable("OrcadDBAdr")
        Dim OrcadDBName = ParameterTable("OrcadDBName")
        Dim OrcadDBUser = ParameterTable("OrcadDBUser")
        Dim OrcadDBPwd = ParameterTable("OrcadDBPwd")

        ParameterTableWrite("LAST_BOM_UPDATE", "Start but not finish....")
        ' clear field
        Dim allOK As Boolean = True
        Try
            sql = "UPDATE `" & DBName & "`.`sigip` SET `doc` = '';"
            cmd = New MySqlCommand(sql, MySqlconnection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ComunicationLog("5050") ' Mysql update query error 
            allOK = False
        End Try

        AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM DOC;", MySqlconnection)
        DsDoc.Clear()
        tblDoc.Clear()
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")

        updated = True
        ListBoxLog.Items.Add("Open Orcad Homologation Card......Wait...")
        Try
            Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where not valido = 'no_valido'", SqlconnectionOrcad)
            DsDocComp.Clear()
            tblDocComp.Clear()
            AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
            tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")
        Catch ex As Exception
            ListBoxLog.Items.Add("Connection lost, need waiting 20 sec...")
            CloseConnectionSqlOrcad()
            'OpenConnectionMySqlOrcad("10.10.10.15", "orcad1", "orcadw", "orcadw")
            OpenConnectionMySqlOrcad(OrcadDBAdr, OrcadDBName, OrcadDBUser, OrcadDBPwd)
            ListBoxLog.Items.Add("Connection estabilished...Done!")
            DsDocComp.Clear()
            tblDocComp.Clear()
            Dim AdapterDocComp As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where not valido = 'no_valido'", SqlconnectionOrcad)
            AdapterDocComp.Fill(DsDocComp, "orcadw.T_orcadcis")
            tblDocComp = DsDocComp.Tables("orcadw.T_orcadcis")

        End Try
        ListBoxLog.Items.Add("Open Orcad Homologation Card......Open!")
        Try
            sql = "UPDATE `" & DBName & "`.`sigip` SET `doc` = 'FAB' WHERE not (`sigip`.`ACQ_FAB` = 'ACQ') ;"
            cmd = New MySqlCommand(sql, MySqlconnection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ComunicationLog("5050") ' Mysql update query error
            allOK = False
        End Try

        Try
            sql = "UPDATE `" & DBName & "`.`sigip` SET `doc` = 'OBSOLETE' WHERE not (`sigip`.`ACTIVE` = 'YES') ;"
            cmd = New MySqlCommand(sql, MySqlconnection)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            ComunicationLog("5050") ' Mysql update query error
            allOK = False
        End Try

        sql = ""

        Application.DoEvents()
        updated = True
        i = 1

        Dim changed As Boolean = True



        While changed
            changed = False
            AdapterDoc.SelectCommand = New MySqlCommand("SELECT * FROM sigip;", MySqlconnection)
            dsbom.Clear()
            tblbom.Clear()
            AdapterBom.Fill(dsbom, "sigip")
            tblbom = dsbom.Tables("sigip")
            RowSearchBom = tblbom.Select("ACQ_FAB like '*ACQ*' and doc =''", "bitron_pn")
            ListBoxLog.Items.Add("Comp. updating.. For finish.." & RowSearchBom.Length)
            Application.DoEvents()
            Dim CurrentBitronPN As String = ""
            For Each row In RowSearchBom

                If CurrentBitronPN <> row("bitron_pn").ToString Then
                    CurrentBitronPN = row("bitron_pn").ToString
                    Application.DoEvents()
                    ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
                    ' find only file type c1 or c2 also if not declared.To avoid to find code inside others code.

                    RowSearchDoc = tblDoc.Select("filename like '" & row("bitron_pn").ToString & " - *' or filename like '" & row("bitron_pn").ToString & "'", "rev DESC")
                    If RowSearchDoc.Length > 0 And Mid(row("bitron_pn").ToString, 1, 2) <> "15" Then
                        doc = "SRV_DOC - " & RowSearchDoc(0)("header").ToString & "_" & RowSearchDoc(0)("filename").ToString & "_" & RowSearchDoc(0)("rev").ToString & "." & RowSearchDoc(0)("extension").ToString
                    Else
                        RowHC = tblDocComp.Select("codice_bitron = '" & row("bitron_pn").ToString & "'", "valido")
                        If RowHC.Length = 1 Then
                            doc = "HC-" & RowHC(0)("cod_comp").ToString
                        ElseIf RowHC.Length > 1 Then

                            RowHC = tblDocComp.Select("codice_bitron = '" & row("bitron_pn").ToString & "' and valido = 'valido'", "valido")
                            If RowHC.Length = 1 Then
                                doc = "HC-" & RowHC(0)("cod_comp").ToString
                            Else
                                MsgBox("HC with two valid sheet! " & RowHC(0)("codice_bitron").ToString)
                                doc = "ERROR"
                            End If
                        Else
                            doc = "NO"
                        End If

                    End If
                    sql = sql & "UPDATE `" & DBName & "`.`sigip` SET `OrcadSupplier` = '" & GetOrcadSupplier(row("bitron_pn").ToString) & "' , `doc` = '" & doc & "' WHERE `sigip`.`bitron_pn` = '" & row("bitron_pn").ToString & "' ; "
                    If Len(sql) > 1000 Then
                        Try
                            cmd = New MySqlCommand(sql, MySqlconnection)
                            cmd.ExecuteNonQuery()
                            sql = ""
                            changed = True
                            Exit For
                        Catch ex As Exception
                            ComunicationLog("5050") ' Mysql update query error 
                            allOK = False
                        End Try
                    End If
                End If
            Next row

        End While
        Try
            cmd = New MySqlCommand(sql, MySqlconnection)
            cmd.ExecuteNonQuery()
            sql = ""
        Catch ex As Exception
            ComunicationLog("5050") ' Mysql update query error 
            allOK = False
        End Try
        If allOK Then ParameterTableWrite("LAST_BOM_UPDATE", Today)
        ListBoxLog.Items.Add("HC updating...Finish!")
        ListBoxLog.SelectedIndex = ListBoxLog.Items.Count - 1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExport.Click
        ExportListview2Excel(ListView1)
    End Sub


    Function GetOrcadSupplier(ByVal BitronPN As String) As String
        GetOrcadSupplier = ""
        Dim info As String = ""
        Try
            Dim AdapterSql As New SqlDataAdapter("SELECT * FROM orcadw.T_orcadcis where ( valido = 'valido') and codice_bitron = '" & BitronPN & "'", SqlconnectionOrcad)
            TblSql.Clear()
            DsSql.Clear()
            AdapterSql.Fill(DsSql, "orcadw.T_orcadcis")
            TblSql = DsSql.Tables("orcadw.T_orcadcis")

            If TblSql.Rows.Count > 0 Then
                info = IIf(TblSql.Rows.Item(0)("costruttore").ToString <> "", TblSql.Rows.Item(0)("costruttore").ToString & "[" & TblSql.Rows.Item(0)("orderingcode").ToString & "];", "")
                'GetOrcadSupplier = ReplaceChar(info)
                For i = 2 To 9
                    GetOrcadSupplier = GetOrcadSupplier & IIf(TblSql.Rows.Item(0)("costruttore" & i).ToString <> "", TblSql.Rows.Item(0)("costruttore" & i).ToString & "[" & TblSql.Rows.Item(0)("orderingcode" & i).ToString & "];", "")
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Function

    Sub OpenConnectionSqlOrcad(ByVal strHost As String, ByVal strDatabase As String, ByVal strUserName As String, ByVal strPassword As String)
        Try
            ConnectionStringOrcad = "server=" & strHost & ";user id=" & strUserName & ";" & "pwd=" & strPassword & ";" & "database=" & strDatabase & ";Connect Timeout=120;"
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


    Private Sub TextBoxLS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxLS.TextChanged
        If IsNumeric(TextBoxLS.Text) Then
        Else
            TextBoxLS.Text = ""
        End If
    End Sub

    Function ReplaceChar(ByVal s As String) As String
        ReplaceChar = s
        For i = 1 To Len(s)
            If (Asc(Mid(s, i, 1)) >= 48 And Asc(Mid(s, i, 1)) <= 57) _
             Or (Asc(Mid(s, i, 1)) >= 65 And Asc(Mid(s, i, 1)) <= 90) _
             Or (Asc(Mid(s, i, 1)) >= 97 And Asc(Mid(s, i, 1)) <= 122) Or Asc(Mid(s, i, 1)) = 93 Or Asc(Mid(s, i, 1)) = 91 Or Asc(Mid(s, i, 1)) = 59 Or Asc(Mid(s, i, 1)) = 46 Or Asc(Mid(s, i, 1)) = 37 Then
            Else
                s = Mid(s, 1, i - 1) & "-" & Mid(s, i + 1)
            End If
            ReplaceChar = s
        Next

    End Function

End Class