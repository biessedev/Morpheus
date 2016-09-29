Public Class FormStart

    Private Sub ButtonLoadDoc_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLoadDoc.Click
        FormLoadDoc.Show()
        FormLoadDoc.Focus()
        FormLoadDoc.Text = FormLoadDoc.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonDocManagement_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDocManagement.Click
        FormDownload.Show()
        FormDownload.Focus()
        FormDownload.Text = FormDownload.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonTypeEdit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTypeEdit.Click
        FormTypeAdmin.Show()
        FormTypeAdmin.Focus()
        FormTypeAdmin.Text = FormTypeAdmin.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonProduct.Click
        FormProduct.Show()
        FormProduct.Focus()
        FormProduct.Text = FormProduct.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonECR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonECR.Click
        FormECR.Show()
        FormECR.Focus()
        FormECR.Text = FormECR.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAbout.Click
        FormAbaut.Show()
        FormAbaut.Focus()

    End Sub

    Private Sub FormStart_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Application.Exit()
        Me.Close()
    End Sub

    Private Sub FormStart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormCredentials.Hide()
        Application.DoEvents()
        If controlRight("A") >= 3 Or controlRight("E") >= 3 Or controlRight("N") >= 3 Or controlRight("L") >= 3 Or controlRight("P") >= 3 Or controlRight("Q") >= 3 Or controlRight("R") >= 3 Or controlRight("U") >= 3 Then ButtonECR.Enabled = True
        If controlRight("L") >= 2 Or controlRight("E") >= 2 Or controlRight("R") >= 2 Then ButtonECR.Enabled = True
        If controlRight("R") >= 2 Or controlRight("U") >= 2 Or controlRight("B") >= 2 Then ButtonQuote.Enabled = True
        If controlRight("R") >= 2 Or controlRight("E") >= 2 Then ButtonEq.Enabled = True
        If controlRight("R") >= 2 Then ButtonNpi.Enabled = True

        If controlRight("R") >= 2 Then ButtonCommit.Enabled = True
        If controlRight("Z") = 3 Then ButtonSystem.Enabled = True
        Me.Text = "Welcome : " & CreAccount.strUserName

        If controlRight("R") >= 3 Then ButtonRunning.Visible = True
        ButtonRunning.BackColor = Color.Green
        If DateDiff("d", string_to_date(ParameterTable("LAST_AUTOMATIC_SCHEDULER")), Today) > 1 Then ButtonRunning.BackColor = Color.Red

    End Sub

    Private Sub ButtonSystem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSystem.Click

        If controlRight("Z") = 3 Then
            FormAdministration.Show()
            Me.Hide()
        End If

        FormAdministration.Text = FormAdministration.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonActivity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonNpi.Click
        FormSamples.Show()
        FormSamples.Focus()
        FormSamples.Text = FormSamples.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonQuote.Click
        FormOffer.Show()
        FormOffer.Focus()
        FormOffer.Text = FormOffer.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub
    Private Sub ButtonCommit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCommit.Click
        FormCommit.Show()
        FormCommit.Focus()
        FormCommit.Text = FormCommit.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonEq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEq.Click
        FormEquipments.Show()
        FormEquipments.Focus()
        FormEquipments.Text = FormEquipments.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub


    Private Sub ButtonMould_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonMould.Click
        FormMould.Show()
        FormMould.Focus()
        FormMould.Text = FormMould.Text & " <>  Welcome : " & CreAccount.strUserName
    End Sub


    Private Sub ButtonProjectShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonProjectShow.Click
        If controlRight("J") >= 3 Then
            FormTimeShow.Show()
            FormTimeShow.Focus()
            Me.Hide()
        End If
    End Sub

    Private Sub ButtonTiming_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTiming.Click
        FormTime.Show()
        FormTime.Focus()
        FormTime.Text = "Project Time and Quality management" & " <>  Welcome : " & CreAccount.strUserName
    End Sub

    Private Sub ButtonBom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBom.Click
        FormBomUtility.Show()
        FormBomUtility.Focus()
        FormBomUtility.Text = "Bom Tools " & " <>  Welcome : " & CreAccount.strUserName
    End Sub


    Private Sub ButtonCrypted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCrypted.Click
        FormCoding.Show()
        FormCoding.Focus()
        FormCoding.Text = "Signature Crypt " & " <>  Welcome : " & CreAccount.strUserName
    End Sub
End Class