<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOpenIssue
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ComboBoxGroup = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBoxName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListViewGRU = New System.Windows.Forms.ListView()
        Me.SuspendLayout()
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemove.Location = New System.Drawing.Point(738, 392)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(92, 22)
        Me.ButtonRemove.TabIndex = 222
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAdd.Location = New System.Drawing.Point(12, 392)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(82, 22)
        Me.ButtonAdd.TabIndex = 221
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ComboBoxGroup
        '
        Me.ComboBoxGroup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ComboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxGroup.FormattingEnabled = True
        Me.ComboBoxGroup.Location = New System.Drawing.Point(12, 26)
        Me.ComboBoxGroup.Name = "ComboBoxGroup"
        Me.ComboBoxGroup.Size = New System.Drawing.Size(209, 24)
        Me.ComboBoxGroup.TabIndex = 220
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(9, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 13)
        Me.Label10.TabIndex = 218
        Me.Label10.Text = "Department"
        '
        'ComboBoxName
        '
        Me.ComboBoxName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ComboBoxName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxName.FormattingEnabled = True
        Me.ComboBoxName.Location = New System.Drawing.Point(12, 73)
        Me.ComboBoxName.Name = "ComboBoxName"
        Me.ComboBoxName.Size = New System.Drawing.Size(818, 24)
        Me.ComboBoxName.TabIndex = 224
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 223
        Me.Label1.Text = "Type Doc"
        '
        'ListViewGRU
        '
        Me.ListViewGRU.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ListViewGRU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewGRU.GridLines = True
        ListViewItem1.StateImageIndex = 0
        Me.ListViewGRU.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListViewGRU.Location = New System.Drawing.Point(12, 122)
        Me.ListViewGRU.MultiSelect = False
        Me.ListViewGRU.Name = "ListViewGRU"
        Me.ListViewGRU.Size = New System.Drawing.Size(818, 264)
        Me.ListViewGRU.TabIndex = 225
        Me.ListViewGRU.UseCompatibleStateImageBehavior = False
        Me.ListViewGRU.View = System.Windows.Forms.View.Details
        '
        'FormOpenIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 417)
        Me.Controls.Add(Me.ListViewGRU)
        Me.Controls.Add(Me.ComboBoxName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ComboBoxGroup)
        Me.Controls.Add(Me.Label10)
        Me.Name = "FormOpenIssue"
        Me.Text = "Open Issue Management - Product Status Derogation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ComboBoxGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListViewGRU As System.Windows.Forms.ListView
End Class
