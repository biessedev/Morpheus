<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormNPIDocMamagement
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
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Cob_NameDoc = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Btn_Cancel = New System.Windows.Forms.Button
        Me.Btn_Add = New System.Windows.Forms.Button
        Me.Cob_TypeDoc = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.ListViewNPI = New System.Windows.Forms.ListView
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(992, 352)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(135, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 240
        Me.PictureBox1.TabStop = False
        '
        'Cob_NameDoc
        '
        Me.Cob_NameDoc.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cob_NameDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cob_NameDoc.FormattingEnabled = True
        Me.Cob_NameDoc.Location = New System.Drawing.Point(31, 98)
        Me.Cob_NameDoc.Margin = New System.Windows.Forms.Padding(4)
        Me.Cob_NameDoc.Name = "Cob_NameDoc"
        Me.Cob_NameDoc.Size = New System.Drawing.Size(919, 25)
        Me.Cob_NameDoc.TabIndex = 238
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 78)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 237
        Me.Label1.Text = "Name Doc"
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Btn_Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Cancel.Location = New System.Drawing.Point(828, 364)
        Me.Btn_Cancel.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(123, 27)
        Me.Btn_Cancel.TabIndex = 236
        Me.Btn_Cancel.Text = "Cancel"
        Me.Btn_Cancel.UseVisualStyleBackColor = True
        '
        'Btn_Add
        '
        Me.Btn_Add.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Btn_Add.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Add.Location = New System.Drawing.Point(31, 364)
        Me.Btn_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.Btn_Add.Name = "Btn_Add"
        Me.Btn_Add.Size = New System.Drawing.Size(109, 27)
        Me.Btn_Add.TabIndex = 235
        Me.Btn_Add.Text = "Link"
        Me.Btn_Add.UseVisualStyleBackColor = True
        '
        'Cob_TypeDoc
        '
        Me.Cob_TypeDoc.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cob_TypeDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cob_TypeDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cob_TypeDoc.FormattingEnabled = True
        Me.Cob_TypeDoc.Location = New System.Drawing.Point(31, 34)
        Me.Cob_TypeDoc.Margin = New System.Windows.Forms.Padding(4)
        Me.Cob_TypeDoc.Name = "Cob_TypeDoc"
        Me.Cob_TypeDoc.Size = New System.Drawing.Size(919, 25)
        Me.Cob_TypeDoc.TabIndex = 234
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(27, 14)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 17)
        Me.Label10.TabIndex = 233
        Me.Label10.Text = "Type Doc"
        '
        'ListViewNPI
        '
        Me.ListViewNPI.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ListViewNPI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewNPI.GridLines = True
        ListViewItem3.StateImageIndex = 0
        Me.ListViewNPI.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem3})
        Me.ListViewNPI.Location = New System.Drawing.Point(30, 154)
        Me.ListViewNPI.Margin = New System.Windows.Forms.Padding(4)
        Me.ListViewNPI.MultiSelect = False
        Me.ListViewNPI.Name = "ListViewNPI"
        Me.ListViewNPI.Size = New System.Drawing.Size(919, 202)
        Me.ListViewNPI.TabIndex = 241
        Me.ListViewNPI.UseCompatibleStateImageBehavior = False
        Me.ListViewNPI.View = System.Windows.Forms.View.Details
        '
        'FormNPIDocMamagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1155, 404)
        Me.Controls.Add(Me.ListViewNPI)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Cob_NameDoc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.Btn_Add)
        Me.Controls.Add(Me.Cob_TypeDoc)
        Me.Controls.Add(Me.Label10)
        Me.Name = "FormNPIDocMamagement"
        Me.Text = "NPI Doc Mamagement"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Cob_NameDoc As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Btn_Add As System.Windows.Forms.Button
    Friend WithEvents Cob_TypeDoc As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ListViewNPI As System.Windows.Forms.ListView
End Class
