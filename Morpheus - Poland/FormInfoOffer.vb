Public Class FormInfoOffer

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        TextBoxRes.Text = LabelRes.Text & ComboBoxResCASE.Text & "." & ComboBoxResTol.Text & "]"
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        TextBoxRes.Text = LabelCap.Text & ComboBoxCapCase.Text & "." & ComboBoxCapMat.Text & "." & ComboBoxCapVal.Text & "." & ComboBoxCapTol.Text & "]"
    End Sub

    Private Sub FormInfoOffer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        TextBoxRes.Enabled = True
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        TextBoxRes.Text = Label3.Text
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        TextBoxRes.Text = Label4.Text & ComboBoxth.Text & "." & ComboBoxDim.Text & "." & ComboBoxLayer.Text & "." & ComboBoxCopper.Text & ComboBoxPTH.Text & "." & ComboBoxTG.Text & "]"
    End Sub

    Private Sub ButtonGeneric_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonGeneric.Click
        TextBoxRes.Text = Label14.Text
    End Sub
End Class