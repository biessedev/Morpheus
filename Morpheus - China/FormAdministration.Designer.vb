<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAdministration
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormAdministration))
        Me.TimerECR = New System.Windows.Forms.Timer(Me.components)
        Me.ButtonClose = New System.Windows.Forms.Button
        Me.ListBoxLog = New System.Windows.Forms.ListBox
        Me.ButtonCompare = New System.Windows.Forms.Button
        Me.CheckBoxDeleteFile = New System.Windows.Forms.CheckBox
        Me.CheckBoxDeleteRecord = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ButtonDelDup = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxEcr = New System.Windows.Forms.TextBox
        Me.TextBoxService = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ButtonSchedule = New System.Windows.Forms.Button
        Me.RichTextBoxConv = New System.Windows.Forms.RichTextBox
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerECR
        '
        Me.TimerECR.Enabled = True
        Me.TimerECR.Interval = 3600000
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(689, 171)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(61, 32)
        Me.ButtonClose.TabIndex = 0
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ListBoxLog
        '
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.Location = New System.Drawing.Point(12, 210)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxLog.Size = New System.Drawing.Size(739, 134)
        Me.ListBoxLog.TabIndex = 97
        '
        'ButtonCompare
        '
        Me.ButtonCompare.Location = New System.Drawing.Point(21, 77)
        Me.ButtonCompare.Name = "ButtonCompare"
        Me.ButtonCompare.Size = New System.Drawing.Size(163, 23)
        Me.ButtonCompare.TabIndex = 100
        Me.ButtonCompare.Text = "Compare DB - File"
        Me.ButtonCompare.UseVisualStyleBackColor = True
        '
        'CheckBoxDeleteFile
        '
        Me.CheckBoxDeleteFile.AutoSize = True
        Me.CheckBoxDeleteFile.Checked = True
        Me.CheckBoxDeleteFile.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDeleteFile.Location = New System.Drawing.Point(21, 31)
        Me.CheckBoxDeleteFile.Name = "CheckBoxDeleteFile"
        Me.CheckBoxDeleteFile.Size = New System.Drawing.Size(164, 17)
        Me.CheckBoxDeleteFile.TabIndex = 101
        Me.CheckBoxDeleteFile.Text = "Delete File, if no record in DB"
        Me.CheckBoxDeleteFile.UseVisualStyleBackColor = True
        '
        'CheckBoxDeleteRecord
        '
        Me.CheckBoxDeleteRecord.AutoSize = True
        Me.CheckBoxDeleteRecord.Checked = True
        Me.CheckBoxDeleteRecord.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxDeleteRecord.Location = New System.Drawing.Point(21, 54)
        Me.CheckBoxDeleteRecord.Name = "CheckBoxDeleteRecord"
        Me.CheckBoxDeleteRecord.Size = New System.Drawing.Size(163, 17)
        Me.CheckBoxDeleteRecord.TabIndex = 102
        Me.CheckBoxDeleteRecord.Text = "Delete Record in DB if no file"
        Me.CheckBoxDeleteRecord.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxDeleteFile)
        Me.GroupBox1.Controls.Add(Me.CheckBoxDeleteRecord)
        Me.GroupBox1.Controls.Add(Me.ButtonCompare)
        Me.GroupBox1.Controls.Add(Me.ButtonDelDup)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(214, 170)
        Me.GroupBox1.TabIndex = 103
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Clean Function"
        '
        'ButtonDelDup
        '
        Me.ButtonDelDup.Location = New System.Drawing.Point(21, 118)
        Me.ButtonDelDup.Name = "ButtonDelDup"
        Me.ButtonDelDup.Size = New System.Drawing.Size(164, 23)
        Me.ButtonDelDup.TabIndex = 105
        Me.ButtonDelDup.Text = "Delete record Duplicate"
        Me.ButtonDelDup.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(430, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 108
        Me.Label1.Text = "ECR Scheduling"
        '
        'TextBoxEcr
        '
        Me.TextBoxEcr.Location = New System.Drawing.Point(434, 51)
        Me.TextBoxEcr.Name = "TextBoxEcr"
        Me.TextBoxEcr.ReadOnly = True
        Me.TextBoxEcr.Size = New System.Drawing.Size(201, 20)
        Me.TextBoxEcr.TabIndex = 109
        '
        'TextBoxService
        '
        Me.TextBoxService.Location = New System.Drawing.Point(434, 102)
        Me.TextBoxService.Name = "TextBoxService"
        Me.TextBoxService.ReadOnly = True
        Me.TextBoxService.Size = New System.Drawing.Size(201, 20)
        Me.TextBoxService.TabIndex = 111
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(430, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 110
        Me.Label2.Text = "Service Scheduling"
        '
        'ButtonSchedule
        '
        Me.ButtonSchedule.Location = New System.Drawing.Point(434, 128)
        Me.ButtonSchedule.Name = "ButtonSchedule"
        Me.ButtonSchedule.Size = New System.Drawing.Size(201, 27)
        Me.ButtonSchedule.TabIndex = 112
        Me.ButtonSchedule.Text = "Schedule"
        Me.ButtonSchedule.UseVisualStyleBackColor = True
        '
        'RichTextBoxConv
        '
        Me.RichTextBoxConv.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RichTextBoxConv.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxConv.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.RichTextBoxConv.Location = New System.Drawing.Point(380, 183)
        Me.RichTextBoxConv.Name = "RichTextBoxConv"
        Me.RichTextBoxConv.Size = New System.Drawing.Size(33, 21)
        Me.RichTextBoxConv.TabIndex = 172
        Me.RichTextBoxConv.Text = ""
        Me.RichTextBoxConv.Visible = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "SrvDoc"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(649, 10)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = False
        '
        'FormAdministration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 358)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.RichTextBoxConv)
        Me.Controls.Add(Me.ButtonSchedule)
        Me.Controls.Add(Me.TextBoxService)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxEcr)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.ButtonClose)
        Me.Name = "FormAdministration"
        Me.Text = "SrvDoc - Document Management System -> Administration Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerECR As System.Windows.Forms.Timer
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ListBoxLog As System.Windows.Forms.ListBox
    Friend WithEvents ButtonCompare As System.Windows.Forms.Button
    Friend WithEvents CheckBoxDeleteFile As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxDeleteRecord As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonDelDup As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxEcr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxService As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonSchedule As System.Windows.Forms.Button
    Friend WithEvents RichTextBoxConv As System.Windows.Forms.RichTextBox
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
