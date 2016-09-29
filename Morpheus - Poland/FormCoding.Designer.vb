<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCoding
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCoding))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.TextBoxencrypted = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerQ = New System.Windows.Forms.DateTimePicker()
        Me.ButtonDate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxInfo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxdecrypted = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxPsw = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ButtonDecrypt = New System.Windows.Forms.Button()
        Me.ButtonEncrypt = New System.Windows.Forms.Button()
        Me.ButtonValid = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 28)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 25)
        Me.Label3.TabIndex = 500
        Me.Label3.Text = "Name"
        '
        'TextBoxName
        '
        Me.TextBoxName.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxName.Location = New System.Drawing.Point(27, 57)
        Me.TextBoxName.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(158, 34)
        Me.TextBoxName.TabIndex = 515
        '
        'TextBoxencrypted
        '
        Me.TextBoxencrypted.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxencrypted.Location = New System.Drawing.Point(27, 144)
        Me.TextBoxencrypted.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxencrypted.Name = "TextBoxencrypted"
        Me.TextBoxencrypted.Size = New System.Drawing.Size(653, 27)
        Me.TextBoxencrypted.TabIndex = 517
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 115)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 25)
        Me.Label1.TabIndex = 516
        Me.Label1.Text = "Encrypted test"
        '
        'DateTimePickerQ
        '
        Me.DateTimePickerQ.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePickerQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerQ.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerQ.Location = New System.Drawing.Point(365, 59)
        Me.DateTimePickerQ.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerQ.Name = "DateTimePickerQ"
        Me.DateTimePickerQ.Size = New System.Drawing.Size(25, 34)
        Me.DateTimePickerQ.TabIndex = 740
        '
        'ButtonDate
        '
        Me.ButtonDate.BackColor = System.Drawing.Color.LightGray
        Me.ButtonDate.Enabled = False
        Me.ButtonDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDate.Location = New System.Drawing.Point(203, 58)
        Me.ButtonDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDate.Name = "ButtonDate"
        Me.ButtonDate.Size = New System.Drawing.Size(156, 35)
        Me.ButtonDate.TabIndex = 739
        Me.ButtonDate.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(198, 28)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 25)
        Me.Label2.TabIndex = 741
        Me.Label2.Text = "Date"
        '
        'TextBoxInfo
        '
        Me.TextBoxInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxInfo.Location = New System.Drawing.Point(398, 59)
        Me.TextBoxInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxInfo.Name = "TextBoxInfo"
        Me.TextBoxInfo.Size = New System.Drawing.Size(240, 34)
        Me.TextBoxInfo.TabIndex = 743
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(393, 30)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 25)
        Me.Label4.TabIndex = 742
        Me.Label4.Text = "Info"
        '
        'TextBoxdecrypted
        '
        Me.TextBoxdecrypted.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxdecrypted.Location = New System.Drawing.Point(27, 279)
        Me.TextBoxdecrypted.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxdecrypted.Name = "TextBoxdecrypted"
        Me.TextBoxdecrypted.Size = New System.Drawing.Size(653, 30)
        Me.TextBoxdecrypted.TabIndex = 746
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(22, 250)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(137, 25)
        Me.Label5.TabIndex = 745
        Me.Label5.Text = "Decrypted text"
        '
        'TextBoxPsw
        '
        Me.TextBoxPsw.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPsw.Location = New System.Drawing.Point(659, 58)
        Me.TextBoxPsw.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxPsw.Name = "TextBoxPsw"
        Me.TextBoxPsw.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPsw.Size = New System.Drawing.Size(151, 34)
        Me.TextBoxPsw.TabIndex = 748
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(654, 28)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(138, 25)
        Me.Label6.TabIndex = 747
        Me.Label6.Text = "PSW for Crypt"
        '
        'ButtonDecrypt
        '
        Me.ButtonDecrypt.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonDecrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDecrypt.Image = Global.MORPHEUS.My.Resources.Resources.text
        Me.ButtonDecrypt.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonDecrypt.Location = New System.Drawing.Point(704, 238)
        Me.ButtonDecrypt.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDecrypt.Name = "ButtonDecrypt"
        Me.ButtonDecrypt.Size = New System.Drawing.Size(88, 71)
        Me.ButtonDecrypt.TabIndex = 744
        Me.ButtonDecrypt.Text = "Decrypt"
        Me.ButtonDecrypt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonDecrypt.UseVisualStyleBackColor = False
        '
        'ButtonEncrypt
        '
        Me.ButtonEncrypt.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonEncrypt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEncrypt.Image = Global.MORPHEUS.My.Resources.Resources.hlocchart
        Me.ButtonEncrypt.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ButtonEncrypt.Location = New System.Drawing.Point(704, 144)
        Me.ButtonEncrypt.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonEncrypt.Name = "ButtonEncrypt"
        Me.ButtonEncrypt.Size = New System.Drawing.Size(88, 69)
        Me.ButtonEncrypt.TabIndex = 738
        Me.ButtonEncrypt.Text = "Encrypt"
        Me.ButtonEncrypt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ButtonEncrypt.UseVisualStyleBackColor = False
        '
        'ButtonValid
        '
        Me.ButtonValid.BackColor = System.Drawing.Color.Gray
        Me.ButtonValid.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonValid.Image = CType(resources.GetObject("ButtonValid.Image"), System.Drawing.Image)
        Me.ButtonValid.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonValid.Location = New System.Drawing.Point(27, 177)
        Me.ButtonValid.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonValid.Name = "ButtonValid"
        Me.ButtonValid.Size = New System.Drawing.Size(202, 46)
        Me.ButtonValid.TabIndex = 749
        Me.ButtonValid.Text = "CHECK VALIDITY"
        Me.ButtonValid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonValid.UseVisualStyleBackColor = False
        '
        'FormCoding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 374)
        Me.Controls.Add(Me.ButtonValid)
        Me.Controls.Add(Me.TextBoxPsw)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxdecrypted)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ButtonDecrypt)
        Me.Controls.Add(Me.TextBoxInfo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerQ)
        Me.Controls.Add(Me.ButtonDate)
        Me.Controls.Add(Me.ButtonEncrypt)
        Me.Controls.Add(Me.TextBoxencrypted)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.Label3)
        Me.Name = "FormCoding"
        Me.Text = "FormCoding"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxencrypted As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonEncrypt As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerQ As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonDate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonDecrypt As System.Windows.Forms.Button
    Friend WithEvents TextBoxdecrypted As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPsw As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ButtonValid As System.Windows.Forms.Button
End Class
