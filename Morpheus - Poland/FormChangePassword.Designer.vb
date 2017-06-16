<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormChangePassword
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxOldPass = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNewPass = New System.Windows.Forms.TextBox()
        Me.TextBoxPassCheck = New System.Windows.Forms.TextBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.LabelForValidation = New System.Windows.Forms.Label()
        Me.CheckBoxShowPassword = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Old password"
        '
        'TextBoxOldPass
        '
        Me.TextBoxOldPass.Location = New System.Drawing.Point(140, 48)
        Me.TextBoxOldPass.Name = "TextBoxOldPass"
        Me.TextBoxOldPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxOldPass.Size = New System.Drawing.Size(163, 20)
        Me.TextBoxOldPass.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(12, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "New password"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(12, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Retype new password"
        '
        'TextBoxNewPass
        '
        Me.TextBoxNewPass.Location = New System.Drawing.Point(140, 83)
        Me.TextBoxNewPass.Name = "TextBoxNewPass"
        Me.TextBoxNewPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxNewPass.Size = New System.Drawing.Size(163, 20)
        Me.TextBoxNewPass.TabIndex = 4
        '
        'TextBoxPassCheck
        '
        Me.TextBoxPassCheck.Location = New System.Drawing.Point(140, 117)
        Me.TextBoxPassCheck.Name = "TextBoxPassCheck"
        Me.TextBoxPassCheck.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassCheck.Size = New System.Drawing.Size(163, 20)
        Me.TextBoxPassCheck.TabIndex = 5
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(140, 166)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(66, 23)
        Me.ButtonCancel.TabIndex = 6
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.Location = New System.Drawing.Point(235, 166)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(68, 23)
        Me.ButtonSave.TabIndex = 7
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = True
        '
        'LabelForValidation
        '
        Me.LabelForValidation.AutoSize = True
        Me.LabelForValidation.BackColor = System.Drawing.SystemColors.Control
        Me.LabelForValidation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelForValidation.Location = New System.Drawing.Point(12, 9)
        Me.LabelForValidation.Name = "LabelForValidation"
        Me.LabelForValidation.Size = New System.Drawing.Size(0, 13)
        Me.LabelForValidation.TabIndex = 8
        '
        'CheckBoxShowPassword
        '
        Me.CheckBoxShowPassword.AutoSize = True
        Me.CheckBoxShowPassword.Location = New System.Drawing.Point(140, 143)
        Me.CheckBoxShowPassword.Name = "CheckBoxShowPassword"
        Me.CheckBoxShowPassword.Size = New System.Drawing.Size(102, 17)
        Me.CheckBoxShowPassword.TabIndex = 9
        Me.CheckBoxShowPassword.Text = "Show Password"
        Me.CheckBoxShowPassword.UseVisualStyleBackColor = True
        '
        'FormChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 191)
        Me.Controls.Add(Me.CheckBoxShowPassword)
        Me.Controls.Add(Me.LabelForValidation)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.TextBoxPassCheck)
        Me.Controls.Add(Me.TextBoxNewPass)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxOldPass)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = false
        Me.MaximumSize = New System.Drawing.Size(340, 230)
        Me.MinimizeBox = false
        Me.MinimumSize = New System.Drawing.Size(340, 230)
        Me.Name = "FormChangePassword"
        Me.Text = "Change Password"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxOldPass As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxNewPass As TextBox
    Friend WithEvents TextBoxPassCheck As TextBox
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonSave As Button
    Friend WithEvents LabelForValidation As Label
    Friend WithEvents CheckBoxShowPassword As CheckBox
End Class
