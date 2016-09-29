<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBomUtility
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
        Me.OpenFileDialogExcel = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ButtonMissingPf = New System.Windows.Forms.Button
        Me.ComboBoxIndesit = New System.Windows.Forms.ComboBox
        Me.ButtonCompact = New System.Windows.Forms.Button
        Me.ButtonOpen = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabelExcel = New System.Windows.Forms.Label
        Me.Labelindex = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Btn_missingpf_dateup = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.ButtonCompactElux = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialogExcel
        '
        Me.OpenFileDialogExcel.FileName = "OpenFileDialog1"
        '
        'ButtonMissingPf
        '
        Me.ButtonMissingPf.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonMissingPf.ForeColor = System.Drawing.Color.Black
        Me.ButtonMissingPf.Location = New System.Drawing.Point(12, 337)
        Me.ButtonMissingPf.Name = "ButtonMissingPf"
        Me.ButtonMissingPf.Size = New System.Drawing.Size(724, 54)
        Me.ButtonMissingPf.TabIndex = 640
        Me.ButtonMissingPf.Text = "Update Missing PF Doc"
        Me.ButtonMissingPf.UseVisualStyleBackColor = True
        '
        'ComboBoxIndesit
        '
        Me.ComboBoxIndesit.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxIndesit.FormattingEnabled = True
        Me.ComboBoxIndesit.Location = New System.Drawing.Point(17, 145)
        Me.ComboBoxIndesit.Name = "ComboBoxIndesit"
        Me.ComboBoxIndesit.Size = New System.Drawing.Size(647, 37)
        Me.ComboBoxIndesit.TabIndex = 638
        '
        'ButtonCompact
        '
        Me.ButtonCompact.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCompact.Location = New System.Drawing.Point(17, 247)
        Me.ButtonCompact.Name = "ButtonCompact"
        Me.ButtonCompact.Size = New System.Drawing.Size(199, 39)
        Me.ButtonCompact.TabIndex = 637
        Me.ButtonCompact.Text = "Compact Indesit BOM"
        Me.ButtonCompact.UseVisualStyleBackColor = True
        '
        'ButtonOpen
        '
        Me.ButtonOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpen.Location = New System.Drawing.Point(17, 42)
        Me.ButtonOpen.Name = "ButtonOpen"
        Me.ButtonOpen.Size = New System.Drawing.Size(172, 38)
        Me.ButtonOpen.TabIndex = 0
        Me.ButtonOpen.Text = "OpenExcel"
        Me.ButtonOpen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 640
        Me.Label1.Text = "Indesit Bom"
        '
        'LabelExcel
        '
        Me.LabelExcel.AutoSize = True
        Me.LabelExcel.BackColor = System.Drawing.Color.Transparent
        Me.LabelExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LabelExcel.Location = New System.Drawing.Point(13, 107)
        Me.LabelExcel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelExcel.Name = "LabelExcel"
        Me.LabelExcel.Size = New System.Drawing.Size(33, 20)
        Me.LabelExcel.TabIndex = 641
        Me.LabelExcel.Text = "****"
        '
        'Labelindex
        '
        Me.Labelindex.AutoSize = True
        Me.Labelindex.BackColor = System.Drawing.Color.Transparent
        Me.Labelindex.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labelindex.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Labelindex.Location = New System.Drawing.Point(218, 60)
        Me.Labelindex.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Labelindex.Name = "Labelindex"
        Me.Labelindex.Size = New System.Drawing.Size(57, 20)
        Me.Labelindex.TabIndex = 642
        Me.Labelindex.Text = "********"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Btn_missingpf_dateup)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.ButtonCompactElux)
        Me.Panel1.Controls.Add(Me.Labelindex)
        Me.Panel1.Controls.Add(Me.LabelExcel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ButtonOpen)
        Me.Panel1.Controls.Add(Me.ButtonCompact)
        Me.Panel1.Controls.Add(Me.ComboBoxIndesit)
        Me.Panel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(724, 308)
        Me.Panel1.TabIndex = 639
        '
        'Btn_missingpf_dateup
        '
        Me.Btn_missingpf_dateup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_missingpf_dateup.Location = New System.Drawing.Point(222, 247)
        Me.Btn_missingpf_dateup.Name = "Btn_missingpf_dateup"
        Me.Btn_missingpf_dateup.Size = New System.Drawing.Size(213, 39)
        Me.Btn_missingpf_dateup.TabIndex = 645
        Me.Btn_missingpf_dateup.Text = "Missing_Pf_Date_update"
        Me.Btn_missingpf_dateup.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(17, 202)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(418, 22)
        Me.CheckBox1.TabIndex = 644
        Me.CheckBox1.Text = "Add Customer PN in Cross table and fill in info on BitronPN"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'ButtonCompactElux
        '
        Me.ButtonCompactElux.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCompactElux.Location = New System.Drawing.Point(442, 247)
        Me.ButtonCompactElux.Name = "ButtonCompactElux"
        Me.ButtonCompactElux.Size = New System.Drawing.Size(222, 39)
        Me.ButtonCompactElux.TabIndex = 643
        Me.ButtonCompactElux.Text = "Compact Electrolux BOM"
        Me.ButtonCompactElux.UseVisualStyleBackColor = True
        '
        'FormBomUtility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(774, 414)
        Me.Controls.Add(Me.ButtonMissingPf)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FormBomUtility"
        Me.Text = "FormBomUtility"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialogExcel As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonMissingPf As System.Windows.Forms.Button
    Friend WithEvents ComboBoxIndesit As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonCompact As System.Windows.Forms.Button
    Friend WithEvents ButtonOpen As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelExcel As System.Windows.Forms.Label
    Friend WithEvents Labelindex As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCompactElux As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Btn_missingpf_dateup As System.Windows.Forms.Button
End Class
