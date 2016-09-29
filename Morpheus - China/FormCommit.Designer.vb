<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCommit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCommit))
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.TextBoxNote = New System.Windows.Forms.TextBox
        Me.TreeViewBomList = New System.Windows.Forms.TreeView
        Me.ButtonBomRemove = New System.Windows.Forms.Button
        Me.ButtonNewBom = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ComboBoxCommit = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxDay = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxHour = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBoxMontly = New System.Windows.Forms.TextBox
        Me.TextBoxDescription = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.DateTimePickerStart = New System.Windows.Forms.DateTimePicker
        Me.ButtonRemoveCommit = New System.Windows.Forms.Button
        Me.ButtonNewCommit = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.DateTimePickerEnd = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxUser = New System.Windows.Forms.CheckBox
        Me.TextBoxOpen = New System.Windows.Forms.TextBox
        Me.TextBoxClosed = New System.Windows.Forms.TextBox
        Me.LabelDay = New System.Windows.Forms.Label
        Me.CheckBoxCommit = New System.Windows.Forms.CheckBox
        Me.TextBoxWindows = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.CheckBoxMonthView = New System.Windows.Forms.CheckBox
        Me.ComboBoxUser = New System.Windows.Forms.ComboBox
        Me.ButtonReset = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(272, 37)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(119, 29)
        Me.DateTimePicker1.TabIndex = 1
        '
        'TextBoxNote
        '
        Me.TextBoxNote.Location = New System.Drawing.Point(25, 396)
        Me.TextBoxNote.Multiline = True
        Me.TextBoxNote.Name = "TextBoxNote"
        Me.TextBoxNote.Size = New System.Drawing.Size(366, 107)
        Me.TextBoxNote.TabIndex = 490
        '
        'TreeViewBomList
        '
        Me.TreeViewBomList.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeViewBomList.Location = New System.Drawing.Point(25, 105)
        Me.TreeViewBomList.Name = "TreeViewBomList"
        Me.TreeViewBomList.Size = New System.Drawing.Size(366, 252)
        Me.TreeViewBomList.TabIndex = 491
        '
        'ButtonBomRemove
        '
        Me.ButtonBomRemove.Font = New System.Drawing.Font("Times New Roman", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonBomRemove.Image = CType(resources.GetObject("ButtonBomRemove.Image"), System.Drawing.Image)
        Me.ButtonBomRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonBomRemove.Location = New System.Drawing.Point(477, 454)
        Me.ButtonBomRemove.Name = "ButtonBomRemove"
        Me.ButtonBomRemove.Size = New System.Drawing.Size(172, 36)
        Me.ButtonBomRemove.TabIndex = 493
        Me.ButtonBomRemove.Text = "Remove job"
        Me.ButtonBomRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonBomRemove.UseVisualStyleBackColor = True
        '
        'ButtonNewBom
        '
        Me.ButtonNewBom.Font = New System.Drawing.Font("Times New Roman", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonNewBom.Image = CType(resources.GetObject("ButtonNewBom.Image"), System.Drawing.Image)
        Me.ButtonNewBom.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonNewBom.Location = New System.Drawing.Point(712, 454)
        Me.ButtonNewBom.Name = "ButtonNewBom"
        Me.ButtonNewBom.Size = New System.Drawing.Size(172, 37)
        Me.ButtonNewBom.TabIndex = 492
        Me.ButtonNewBom.Text = "New job"
        Me.ButtonNewBom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonNewBom.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 20)
        Me.Label1.TabIndex = 495
        Me.Label1.Text = "Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 496
        Me.Label2.Text = "Daily job "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(473, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 20)
        Me.Label3.TabIndex = 497
        Me.Label3.Text = "Commit"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(268, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 20)
        Me.Label4.TabIndex = 498
        Me.Label4.Text = "Date"
        '
        'ComboBoxCommit
        '
        Me.ComboBoxCommit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxCommit.FormattingEnabled = True
        Me.ComboBoxCommit.Location = New System.Drawing.Point(477, 117)
        Me.ComboBoxCommit.Name = "ComboBoxCommit"
        Me.ComboBoxCommit.Size = New System.Drawing.Size(407, 32)
        Me.ComboBoxCommit.TabIndex = 499
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(29, 373)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 20)
        Me.Label5.TabIndex = 500
        Me.Label5.Text = "Note"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(627, 367)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 20)
        Me.Label6.TabIndex = 502
        Me.Label6.Text = "Day coverage (h)"
        '
        'TextBoxDay
        '
        Me.TextBoxDay.Enabled = False
        Me.TextBoxDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDay.Location = New System.Drawing.Point(631, 390)
        Me.TextBoxDay.Name = "TextBoxDay"
        Me.TextBoxDay.Size = New System.Drawing.Size(125, 26)
        Me.TextBoxDay.TabIndex = 501
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(473, 295)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 20)
        Me.Label7.TabIndex = 509
        Me.Label7.Text = "Hours"
        '
        'TextBoxHour
        '
        Me.TextBoxHour.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxHour.Location = New System.Drawing.Point(477, 318)
        Me.TextBoxHour.Name = "TextBoxHour"
        Me.TextBoxHour.Size = New System.Drawing.Size(125, 26)
        Me.TextBoxHour.TabIndex = 508
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(757, 367)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 20)
        Me.Label8.TabIndex = 511
        Me.Label8.Text = "Month hours"
        '
        'TextBoxMontly
        '
        Me.TextBoxMontly.Enabled = False
        Me.TextBoxMontly.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxMontly.Location = New System.Drawing.Point(762, 390)
        Me.TextBoxMontly.Name = "TextBoxMontly"
        Me.TextBoxMontly.Size = New System.Drawing.Size(122, 26)
        Me.TextBoxMontly.TabIndex = 510
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDescription.Location = New System.Drawing.Point(477, 155)
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.Size = New System.Drawing.Size(407, 26)
        Me.TextBoxDescription.TabIndex = 514
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(742, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 20)
        Me.Label10.TabIndex = 516
        Me.Label10.Text = "Open"
        '
        'DateTimePickerStart
        '
        Me.DateTimePickerStart.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePickerStart.Enabled = False
        Me.DateTimePickerStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerStart.Location = New System.Drawing.Point(863, 27)
        Me.DateTimePickerStart.Name = "DateTimePickerStart"
        Me.DateTimePickerStart.Size = New System.Drawing.Size(21, 29)
        Me.DateTimePickerStart.TabIndex = 515
        '
        'ButtonRemoveCommit
        '
        Me.ButtonRemoveCommit.Enabled = False
        Me.ButtonRemoveCommit.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemoveCommit.Image = CType(resources.GetObject("ButtonRemoveCommit.Image"), System.Drawing.Image)
        Me.ButtonRemoveCommit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonRemoveCommit.Location = New System.Drawing.Point(477, 184)
        Me.ButtonRemoveCommit.Name = "ButtonRemoveCommit"
        Me.ButtonRemoveCommit.Size = New System.Drawing.Size(129, 24)
        Me.ButtonRemoveCommit.TabIndex = 517
        Me.ButtonRemoveCommit.Text = "Remove Commit"
        Me.ButtonRemoveCommit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonRemoveCommit.UseVisualStyleBackColor = True
        '
        'ButtonNewCommit
        '
        Me.ButtonNewCommit.Enabled = False
        Me.ButtonNewCommit.Font = New System.Drawing.Font("Times New Roman", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonNewCommit.Image = CType(resources.GetObject("ButtonNewCommit.Image"), System.Drawing.Image)
        Me.ButtonNewCommit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonNewCommit.Location = New System.Drawing.Point(755, 184)
        Me.ButtonNewCommit.Name = "ButtonNewCommit"
        Me.ButtonNewCommit.Size = New System.Drawing.Size(129, 24)
        Me.ButtonNewCommit.TabIndex = 518
        Me.ButtonNewCommit.Text = "New Commit"
        Me.ButtonNewCommit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonNewCommit.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(742, 61)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(58, 20)
        Me.Label11.TabIndex = 520
        Me.Label11.Text = "Closed"
        '
        'DateTimePickerEnd
        '
        Me.DateTimePickerEnd.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePickerEnd.Enabled = False
        Me.DateTimePickerEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEnd.Location = New System.Drawing.Point(863, 82)
        Me.DateTimePickerEnd.Name = "DateTimePickerEnd"
        Me.DateTimePickerEnd.Size = New System.Drawing.Size(21, 29)
        Me.DateTimePickerEnd.TabIndex = 519
        '
        'CheckBoxUser
        '
        Me.CheckBoxUser.AutoSize = True
        Me.CheckBoxUser.Checked = True
        Me.CheckBoxUser.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxUser.Location = New System.Drawing.Point(477, 370)
        Me.CheckBoxUser.Name = "CheckBoxUser"
        Me.CheckBoxUser.Size = New System.Drawing.Size(140, 20)
        Me.CheckBoxUser.TabIndex = 521
        Me.CheckBoxUser.Text = "Selected user Only"
        Me.CheckBoxUser.UseVisualStyleBackColor = True
        '
        'TextBoxOpen
        '
        Me.TextBoxOpen.Enabled = False
        Me.TextBoxOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxOpen.Location = New System.Drawing.Point(746, 29)
        Me.TextBoxOpen.Name = "TextBoxOpen"
        Me.TextBoxOpen.Size = New System.Drawing.Size(111, 26)
        Me.TextBoxOpen.TabIndex = 522
        '
        'TextBoxClosed
        '
        Me.TextBoxClosed.Enabled = False
        Me.TextBoxClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxClosed.Location = New System.Drawing.Point(746, 84)
        Me.TextBoxClosed.Name = "TextBoxClosed"
        Me.TextBoxClosed.Size = New System.Drawing.Size(111, 26)
        Me.TextBoxClosed.TabIndex = 523
        '
        'LabelDay
        '
        Me.LabelDay.AutoSize = True
        Me.LabelDay.Font = New System.Drawing.Font("Times New Roman", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDay.Location = New System.Drawing.Point(397, 42)
        Me.LabelDay.Name = "LabelDay"
        Me.LabelDay.Size = New System.Drawing.Size(87, 22)
        Me.LabelDay.TabIndex = 524
        Me.LabelDay.Text = "LabelDay"
        '
        'CheckBoxCommit
        '
        Me.CheckBoxCommit.AutoSize = True
        Me.CheckBoxCommit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCommit.Location = New System.Drawing.Point(477, 396)
        Me.CheckBoxCommit.Name = "CheckBoxCommit"
        Me.CheckBoxCommit.Size = New System.Drawing.Size(129, 20)
        Me.CheckBoxCommit.TabIndex = 525
        Me.CheckBoxCommit.Text = "Selected Commit"
        Me.CheckBoxCommit.UseVisualStyleBackColor = True
        '
        'TextBoxWindows
        '
        Me.TextBoxWindows.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxWindows.Location = New System.Drawing.Point(331, 82)
        Me.TextBoxWindows.Name = "TextBoxWindows"
        Me.TextBoxWindows.Size = New System.Drawing.Size(60, 22)
        Me.TextBoxWindows.TabIndex = 527
        Me.TextBoxWindows.Text = "5"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(262, 86)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(63, 16)
        Me.Label12.TabIndex = 528
        Me.Label12.Text = "Windows"
        '
        'CheckBoxMonthView
        '
        Me.CheckBoxMonthView.AutoSize = True
        Me.CheckBoxMonthView.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxMonthView.Location = New System.Drawing.Point(147, 82)
        Me.CheckBoxMonthView.Name = "CheckBoxMonthView"
        Me.CheckBoxMonthView.Size = New System.Drawing.Size(95, 20)
        Me.CheckBoxMonthView.TabIndex = 529
        Me.CheckBoxMonthView.Text = "Month View"
        Me.CheckBoxMonthView.UseVisualStyleBackColor = True
        '
        'ComboBoxUser
        '
        Me.ComboBoxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxUser.Enabled = False
        Me.ComboBoxUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxUser.FormattingEnabled = True
        Me.ComboBoxUser.Location = New System.Drawing.Point(25, 36)
        Me.ComboBoxUser.Name = "ComboBoxUser"
        Me.ComboBoxUser.Size = New System.Drawing.Size(217, 32)
        Me.ComboBoxUser.TabIndex = 530
        '
        'ButtonReset
        '
        Me.ButtonReset.Enabled = False
        Me.ButtonReset.Location = New System.Drawing.Point(665, 87)
        Me.ButtonReset.Name = "ButtonReset"
        Me.ButtonReset.Size = New System.Drawing.Size(75, 23)
        Me.ButtonReset.TabIndex = 531
        Me.ButtonReset.Text = "Reset"
        Me.ButtonReset.UseVisualStyleBackColor = True
        '
        'FormCommit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 515)
        Me.Controls.Add(Me.ButtonReset)
        Me.Controls.Add(Me.ComboBoxUser)
        Me.Controls.Add(Me.CheckBoxMonthView)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TextBoxWindows)
        Me.Controls.Add(Me.CheckBoxCommit)
        Me.Controls.Add(Me.LabelDay)
        Me.Controls.Add(Me.TextBoxClosed)
        Me.Controls.Add(Me.TextBoxOpen)
        Me.Controls.Add(Me.CheckBoxUser)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DateTimePickerEnd)
        Me.Controls.Add(Me.ButtonNewCommit)
        Me.Controls.Add(Me.ButtonRemoveCommit)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DateTimePickerStart)
        Me.Controls.Add(Me.TextBoxDescription)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxMontly)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxHour)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxDay)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxCommit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNote)
        Me.Controls.Add(Me.ButtonBomRemove)
        Me.Controls.Add(Me.ButtonNewBom)
        Me.Controls.Add(Me.TreeViewBomList)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Name = "FormCommit"
        Me.Text = "Commit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TextBoxNote As System.Windows.Forms.TextBox
    Friend WithEvents ButtonBomRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonNewBom As System.Windows.Forms.Button
    Friend WithEvents TreeViewBomList As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCommit As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDay As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHour As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMontly As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonRemoveCommit As System.Windows.Forms.Button
    Friend WithEvents ButtonNewCommit As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxUser As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxOpen As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxClosed As System.Windows.Forms.TextBox
    Friend WithEvents LabelDay As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCommit As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxWindows As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxMonthView As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxUser As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonReset As System.Windows.Forms.Button
End Class
