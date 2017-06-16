<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTypeAdmin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTypeAdmin))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.ButtonRefresh = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxSecondType = New System.Windows.Forms.ComboBox()
        Me.ComboBoxFirstType = New System.Windows.Forms.ComboBox()
        Me.ButtonTypeAdd = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBoxThirdType = New System.Windows.Forms.ComboBox()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBoxPropriety = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxExtension = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(559, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 16)
        Me.Label4.TabIndex = 134
        Me.Label4.Text = "Event Notification"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.FormattingEnabled = true
        Me.ListBoxLog.Location = New System.Drawing.Point(562, 40)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.ListBoxLog.Size = New System.Drawing.Size(340, 251)
        Me.ListBoxLog.TabIndex = 133
        '
        'ButtonRefresh
        '
        Me.ButtonRefresh.BackColor = System.Drawing.Color.Transparent
        Me.ButtonRefresh.BackgroundImage = CType(resources.GetObject("ButtonRefresh.BackgroundImage"),System.Drawing.Image)
        Me.ButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ButtonRefresh.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonRefresh.Location = New System.Drawing.Point(408, 253)
        Me.ButtonRefresh.Name = "ButtonRefresh"
        Me.ButtonRefresh.Size = New System.Drawing.Size(131, 38)
        Me.ButtonRefresh.TabIndex = 132
        Me.ButtonRefresh.Text = "Search"
        Me.ButtonRefresh.UseVisualStyleBackColor = false
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(14, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 16)
        Me.Label6.TabIndex = 121
        Me.Label6.Text = "First Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(14, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 16)
        Me.Label5.TabIndex = 120
        Me.Label5.Text = "Second Type"
        '
        'ComboBoxSecondType
        '
        Me.ComboBoxSecondType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ComboBoxSecondType.FormattingEnabled = true
        Me.ComboBoxSecondType.Location = New System.Drawing.Point(17, 101)
        Me.ComboBoxSecondType.Name = "ComboBoxSecondType"
        Me.ComboBoxSecondType.Size = New System.Drawing.Size(522, 24)
        Me.ComboBoxSecondType.TabIndex = 118
        '
        'ComboBoxFirstType
        '
        Me.ComboBoxFirstType.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboBoxFirstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFirstType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ComboBoxFirstType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboBoxFirstType.FormattingEnabled = true
        Me.ComboBoxFirstType.Location = New System.Drawing.Point(17, 44)
        Me.ComboBoxFirstType.Name = "ComboBoxFirstType"
        Me.ComboBoxFirstType.Size = New System.Drawing.Size(522, 24)
        Me.ComboBoxFirstType.TabIndex = 117
        '
        'ButtonTypeAdd
        '
        Me.ButtonTypeAdd.BackColor = System.Drawing.Color.Transparent
        Me.ButtonTypeAdd.BackgroundImage = CType(resources.GetObject("ButtonTypeAdd.BackgroundImage"),System.Drawing.Image)
        Me.ButtonTypeAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonTypeAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ButtonTypeAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonTypeAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonTypeAdd.Location = New System.Drawing.Point(230, 253)
        Me.ButtonTypeAdd.Name = "ButtonTypeAdd"
        Me.ButtonTypeAdd.Size = New System.Drawing.Size(131, 38)
        Me.ButtonTypeAdd.TabIndex = 115
        Me.ButtonTypeAdd.Text = "Add"
        Me.ButtonTypeAdd.UseVisualStyleBackColor = false
        '
        'Label8
        '
        Me.Label8.AutoSize = true
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(14, 134)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 16)
        Me.Label8.TabIndex = 123
        Me.Label8.Text = "Third Type"
        '
        'ComboBoxThirdType
        '
        Me.ComboBoxThirdType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ComboBoxThirdType.FormattingEnabled = true
        Me.ComboBoxThirdType.Location = New System.Drawing.Point(17, 150)
        Me.ComboBoxThirdType.MaxDropDownItems = 16
        Me.ComboBoxThirdType.Name = "ComboBoxThirdType"
        Me.ComboBoxThirdType.Size = New System.Drawing.Size(522, 24)
        Me.ComboBoxThirdType.TabIndex = 119
        '
        'ButtonDelete
        '
        Me.ButtonDelete.BackColor = System.Drawing.Color.Transparent
        Me.ButtonDelete.BackgroundImage = CType(resources.GetObject("ButtonDelete.BackgroundImage"),System.Drawing.Image)
        Me.ButtonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.ButtonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonDelete.Location = New System.Drawing.Point(17, 253)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(132, 38)
        Me.ButtonDelete.TabIndex = 137
        Me.ButtonDelete.Text = "Delete"
        Me.ButtonDelete.UseVisualStyleBackColor = false
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(856, 323)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = false
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(447, 206)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(92, 25)
        Me.Button1.TabIndex = 233
        Me.Button1.Text = "Help"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'TextBoxPropriety
        '
        Me.TextBoxPropriety.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxPropriety.Location = New System.Drawing.Point(17, 206)
        Me.TextBoxPropriety.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxPropriety.Name = "TextBoxPropriety"
        Me.TextBoxPropriety.Size = New System.Drawing.Size(170, 26)
        Me.TextBoxPropriety.TabIndex = 234
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(14, 187)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 16)
        Me.Label1.TabIndex = 235
        Me.Label1.Text = "Document Properties"
        '
        'TextBoxExtension
        '
        Me.TextBoxExtension.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxExtension.Location = New System.Drawing.Point(191, 206)
        Me.TextBoxExtension.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxExtension.Name = "TextBoxExtension"
        Me.TextBoxExtension.Size = New System.Drawing.Size(170, 26)
        Me.TextBoxExtension.TabIndex = 234
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(197, 187)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 16)
        Me.Label2.TabIndex = 235
        Me.Label2.Text = "File Extension"
        '
        'FormTypeAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"),System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(988, 364)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxExtension)
        Me.Controls.Add(Me.TextBoxPropriety)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.ButtonRefresh)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxThirdType)
        Me.Controls.Add(Me.ComboBoxSecondType)
        Me.Controls.Add(Me.ComboBoxFirstType)
        Me.Controls.Add(Me.ButtonTypeAdd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = false
        Me.Name = "FormTypeAdmin"
        Me.Text = "Doc Type"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ListBoxLog As System.Windows.Forms.ListBox
    Friend WithEvents ButtonRefresh As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSecondType As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxFirstType As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonTypeAdd As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxThirdType As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBoxPropriety As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxExtension As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
