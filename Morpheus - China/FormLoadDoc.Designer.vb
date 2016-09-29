<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormLoadDoc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormLoadDoc))
        Me.ButtonLoad = New System.Windows.Forms.Button()
        Me.ButtonBrouse = New System.Windows.Forms.Button()
        Me.TextBoxDocName = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxFileName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxHeader = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxRev = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxExtension = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxLastRevision = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.ComboBoxRevNote = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonLoad
        '
        Me.ButtonLoad.BackgroundImage = CType(resources.GetObject("ButtonLoad.BackgroundImage"), System.Drawing.Image)
        Me.ButtonLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonLoad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonLoad.Location = New System.Drawing.Point(845, 247)
        Me.ButtonLoad.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonLoad.Name = "ButtonLoad"
        Me.ButtonLoad.Size = New System.Drawing.Size(189, 46)
        Me.ButtonLoad.TabIndex = 0
        Me.ButtonLoad.Text = "Load Document"
        Me.ButtonLoad.UseVisualStyleBackColor = True
        '
        'ButtonBrouse
        '
        Me.ButtonBrouse.BackgroundImage = CType(resources.GetObject("ButtonBrouse.BackgroundImage"), System.Drawing.Image)
        Me.ButtonBrouse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonBrouse.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonBrouse.Location = New System.Drawing.Point(849, 52)
        Me.ButtonBrouse.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonBrouse.Name = "ButtonBrouse"
        Me.ButtonBrouse.Size = New System.Drawing.Size(189, 44)
        Me.ButtonBrouse.TabIndex = 1
        Me.ButtonBrouse.Text = "Browse"
        Me.ButtonBrouse.UseVisualStyleBackColor = True
        '
        'TextBoxDocName
        '
        Me.TextBoxDocName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDocName.Location = New System.Drawing.Point(23, 66)
        Me.TextBoxDocName.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxDocName.Name = "TextBoxDocName"
        Me.TextBoxDocName.ReadOnly = True
        Me.TextBoxDocName.Size = New System.Drawing.Size(792, 30)
        Me.TextBoxDocName.TabIndex = 2
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Document Path"
        '
        'TextBoxFileName
        '
        Me.TextBoxFileName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxFileName.Location = New System.Drawing.Point(237, 153)
        Me.TextBoxFileName.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxFileName.Name = "TextBoxFileName"
        Me.TextBoxFileName.ReadOnly = True
        Me.TextBoxFileName.Size = New System.Drawing.Size(555, 30)
        Me.TextBoxFileName.TabIndex = 4
        Me.TextBoxFileName.Tag = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(232, 124)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 25)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "File Name"
        '
        'TextBoxHeader
        '
        Me.TextBoxHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxHeader.Location = New System.Drawing.Point(23, 152)
        Me.TextBoxHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxHeader.Name = "TextBoxHeader"
        Me.TextBoxHeader.ReadOnly = True
        Me.TextBoxHeader.Size = New System.Drawing.Size(180, 30)
        Me.TextBoxHeader.TabIndex = 6
        Me.TextBoxHeader.Tag = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(829, 124)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 25)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Revision"
        '
        'TextBoxRev
        '
        Me.TextBoxRev.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxRev.Location = New System.Drawing.Point(830, 153)
        Me.TextBoxRev.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxRev.Name = "TextBoxRev"
        Me.TextBoxRev.ReadOnly = True
        Me.TextBoxRev.Size = New System.Drawing.Size(85, 30)
        Me.TextBoxRev.TabIndex = 8
        Me.TextBoxRev.Tag = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(941, 124)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 25)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Exension"
        '
        'TextBoxExtension
        '
        Me.TextBoxExtension.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxExtension.Location = New System.Drawing.Point(946, 153)
        Me.TextBoxExtension.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxExtension.Name = "TextBoxExtension"
        Me.TextBoxExtension.ReadOnly = True
        Me.TextBoxExtension.Size = New System.Drawing.Size(92, 30)
        Me.TextBoxExtension.TabIndex = 10
        Me.TextBoxExtension.Tag = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 124)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 25)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Header"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(590, 218)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 25)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Last Revision"
        '
        'TextBoxLastRevision
        '
        Me.TextBoxLastRevision.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxLastRevision.Location = New System.Drawing.Point(595, 247)
        Me.TextBoxLastRevision.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxLastRevision.Name = "TextBoxLastRevision"
        Me.TextBoxLastRevision.ReadOnly = True
        Me.TextBoxLastRevision.Size = New System.Drawing.Size(132, 30)
        Me.TextBoxLastRevision.TabIndex = 18
        Me.TextBoxLastRevision.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 301)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 25)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Event Log"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(17, 219)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(129, 25)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Revision note"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.ItemHeight = 20
        Me.ListBoxLog.Location = New System.Drawing.Point(23, 329)
        Me.ListBoxLog.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(1011, 164)
        Me.ListBoxLog.TabIndex = 28
        '
        'ComboBoxRevNote
        '
        Me.ComboBoxRevNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxRevNote.FormattingEnabled = True
        Me.ComboBoxRevNote.Location = New System.Drawing.Point(23, 247)
        Me.ComboBoxRevNote.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxRevNote.Name = "ComboBoxRevNote"
        Me.ComboBoxRevNote.Size = New System.Drawing.Size(536, 33)
        Me.ComboBoxRevNote.TabIndex = 29
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(936, 510)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(135, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = False
        '
        'FormLoadDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1083, 556)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ComboBoxRevNote)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxLastRevision)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxExtension)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxRev)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxHeader)
        Me.Controls.Add(Me.TextBoxFileName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxDocName)
        Me.Controls.Add(Me.ButtonBrouse)
        Me.Controls.Add(Me.ButtonLoad)
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormLoadDoc"
        Me.Text = "SrvDoc - Document Management System -> UpLoad Form"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonLoad As System.Windows.Forms.Button
    Friend WithEvents ButtonBrouse As System.Windows.Forms.Button
    Friend WithEvents TextBoxDocName As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxHeader As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxRev As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExtension As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLastRevision As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ListBoxLog As System.Windows.Forms.ListBox
    Friend WithEvents ComboBoxRevNote As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
