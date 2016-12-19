Imports System.Linq

Public Class FormStart

    Private Sub ButtonLoadDoc_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonLoadDoc.Click
        FormLoadDoc.Show()
        FormLoadDoc.Focus()
        FormLoadDoc.Text = FormLoadDoc.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonDocManagement_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDocManagement.Click
        FormDownload.Show()
        FormDownload.Focus()
        FormDownload.Text = FormDownload.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonTypeEdit_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTypeEdit.Click
        FormTypeAdmin.Show()
        FormTypeAdmin.Focus()
        FormTypeAdmin.Text = FormTypeAdmin.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonProduct_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonProduct.Click
        FormProduct.Show()
        FormProduct.Focus()
        FormProduct.Text = FormProduct.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonECR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonECR.Click
        FormECR.Show()
        FormECR.Focus()
        FormECR.Text = FormECR.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonAbout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonAbout.Click
        FormAbaut.Show()
        FormAbaut.Focus()

    End Sub

    Private Sub FormStart_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        Application.Exit()
        Me.Close()
    End Sub

    Private Sub FormStart_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        FormCredentials.Hide()
        Application.DoEvents()
        If controlRight("A") >= 3 Or controlRight("E") >= 3 Or controlRight("N") >= 3 Or controlRight("L") >= 3 Or controlRight("P") >= 3 Or controlRight("Q") >= 3 Or controlRight("R") >= 3 Or controlRight("U") >= 3 Then ButtonECR.Enabled = True
        If controlRight("L") >= 2 Or controlRight("B") >= 2 Or controlRight("R") >= 2 Then ButtonECR.Enabled = True
        'If controlRight("R") >= 2 Or controlRight("U") >= 2 Then ButtonQuote.Enabled = True
        'If controlRight("R") >= 2 Or controlRight("E") >= 2 Then ButtonEq.Enabled = True
        If controlRight("R") >= 2 Then ButtonNpi.Enabled = True

        If controlRight("R") >= 2 Then ButtonCommit.Enabled = True
        If controlRight("Z") = 3 Then ButtonSystem.Enabled = True
        Me.Text = "Welcome : " & CreAccount.strUserName

        If controlRight("R") >= 3 Then ButtonRunning.Visible = True
        ButtonRunning.BackColor = Color.Green
        If DateDiff("d", string_to_date(ParameterTable("LAST_AUTOMATIC_SCHEDULER")), Today) > 1 Then ButtonRunning.BackColor = Color.Red

        If (controlRight("R") >= 3) Then
            ButtonChangePassword.Text = "Manage Users Accounts"
        End If
        ButtonChangePassword.Enabled = True

    End Sub

    Private Sub ButtonSystem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonSystem.Click

        If controlRight("Z") = 3 Then
            FormAdministration.Show()
            Me.Hide()
        End If

        FormAdministration.Text = FormAdministration.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonActivity_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonNpi.Click
        FormSamples.Show()
        FormSamples.Focus()
        FormSamples.Text = FormSamples.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonMaterialReques_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMaterialRequest.Click
        FormMaterialRequest.Show()
        FormMaterialRequest.Focus()
        FormMaterialRequest.Text = FormMaterialRequest.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub
    Private Sub ButtonCommit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCommit.Click
        FormCommit.Show()
        FormCommit.Focus()
        FormCommit.Text = FormCommit.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonEq_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEq.Click
        FormEquipments.Show()
        FormEquipments.Focus()
        FormEquipments.Text = FormEquipments.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub


    Private Sub ButtonMould_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonMould.Click
        FormMould.Show()
        FormMould.Focus()
        FormMould.Text = FormMould.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub


    Private Sub ButtonProjectShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonProjectShow.Click
        If controlRight("J") >= 3 Then
            FormTimeShow.Show()
            FormTimeShow.Focus()
            Me.Hide()
        End If
    End Sub

    Private Sub ButtonTiming_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonTiming.Click
        FormTime.Show()
        FormTime.Focus()
        FormTime.Text = "Project Time and Quality management" & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonBom_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonBom.Click
        FormBomUtility.Show()
        FormBomUtility.Focus()
        FormBomUtility.Text = "Bom Tools " & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonCrypted_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonCrypted.Click
        FormCoding.Show()
        FormCoding.Focus()
        FormCoding.Text = "Signature Crypt " & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonHelp_Click(sender As Object, e As EventArgs) Handles ButtonHelp.Click
        Dim strPathFtp As String
        Dim objFtp As ftp = New ftp()
        objFtp.UserName = strFtpServerUser
        objFtp.Password = strFtpServerPsw
        objFtp.Host = strFtpServerAdd
        Dim downloadFileWinPath = ""
        Try
            Dim paramTable As String = ParameterTable("plant")
            strPathFtp = paramTable & "R/" & paramTable & "R_GEN_PRC/"
            Dim str = ""
            objFtp.ListDirectory(strPathFtp, str)
            Dim strings() As String = str.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            Dim docName = paramTable & "R_GEN_PRC_User_Manual_for_Morpheus"
            Dim number = (From foundString In strings Where foundString.Contains(docName) Select Int32.Parse(Mid(foundString, InStr(foundString, docName) + docName.Length + 1, foundString.Length - (InStr(foundString, docName) + docName.Length + 5)))).Concat(New Integer() {0}).Max()
            If number <> 0 Then
                ComunicationLog(objFtp.DownloadFile(strPathFtp, System.IO.Path.GetTempPath, docName & "_" & number & ".docx")) ' download successfull
                downloadFileWinPath = System.IO.Path.GetTempPath & docName & "_" & number & ".docx"
                Process.Start(downloadFileWinPath)
            Else
                MessageBox.Show("The Help document can not be found", "Document not found")
            End If
        Catch ex As Exception
            ComunicationLog("0049") ' Error in ecr Download
        End Try
    End Sub


    Sub ComunicationLog(ByVal ComCode As String)

        Dim rsResult As DataRow()
        rsResult = tblError.Select("code='" & ComCode & "'")
        If rsResult.Length = 0 Then
            ComCode = "0051"
            rsResult = tblError.Select("code='" & ComCode & "'")
        End If
        WriteFile(ComCode & " -> " & rsResult(0).Item("en").ToString & vbCrLf, True)

    End Sub

    Private Sub ButtonChangePassword_Click(sender As Object, e As EventArgs) Handles ButtonChangePassword.Click
        If controlRight("A") = 3 Then
            FormManageAccounts.Show()
            FormManageAccounts.Focus()
        Else
            FormChangePassword.Show()
            FormChangePassword.Focus()
        End If

    End Sub

End Class