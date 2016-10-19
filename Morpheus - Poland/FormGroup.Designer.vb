<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormGroup
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
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ComboBoxGroup = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ComboBoxName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ListViewGRU = New System.Windows.Forms.ListView()
        Me.ListViewForProducts = New System.Windows.Forms.ListView()
        Me.LabelForProductList = New System.Windows.Forms.Label()
        Me.LabelForDocuments = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemove.Location = New System.Drawing.Point(441, 396)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(92, 22)
        Me.ButtonRemove.TabIndex = 222
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAdd.Location = New System.Drawing.Point(741, 395)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(82, 22)
        Me.ButtonAdd.TabIndex = 221
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ComboBoxGroup
        '
        Me.ComboBoxGroup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxGroup.FormattingEnabled = True
        Me.ComboBoxGroup.Location = New System.Drawing.Point(24, 29)
        Me.ComboBoxGroup.Name = "ComboBoxGroup"
        Me.ComboBoxGroup.Size = New System.Drawing.Size(808, 21)
        Me.ComboBoxGroup.TabIndex = 220
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(21, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 13)
        Me.Label10.TabIndex = 218
        Me.Label10.Text = "Doc Type"
        '
        'ComboBoxName
        '
        Me.ComboBoxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxName.FormattingEnabled = True
        Me.ComboBoxName.Location = New System.Drawing.Point(24, 81)
        Me.ComboBoxName.Name = "ComboBoxName"
        Me.ComboBoxName.Size = New System.Drawing.Size(808, 21)
        Me.ComboBoxName.TabIndex = 224
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 223
        Me.Label1.Text = "File Name"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.BitronPoland
        Me.PictureBox1.Location = New System.Drawing.Point(863, 386)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = False
        '
        'ListViewGRU
        '
        Me.ListViewGRU.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewGRU.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewGRU.FullRowSelect = True
        Me.ListViewGRU.GridLines = True
        ListViewItem2.StateImageIndex = 0
        Me.ListViewGRU.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.ListViewGRU.Location = New System.Drawing.Point(441, 140)
        Me.ListViewGRU.MinimumSize = New System.Drawing.Size(382, 249)
        Me.ListViewGRU.Name = "ListViewGRU"
        Me.ListViewGRU.Size = New System.Drawing.Size(391, 249)
        Me.ListViewGRU.TabIndex = 225
        Me.ListViewGRU.UseCompatibleStateImageBehavior = False
        Me.ListViewGRU.View = System.Windows.Forms.View.Details
        '
        'ListViewForProducts
        '
        Me.ListViewForProducts.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewForProducts.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListViewForProducts.FullRowSelect = True
        Me.ListViewForProducts.GridLines = True
        Me.ListViewForProducts.HideSelection = False
        Me.ListViewForProducts.Location = New System.Drawing.Point(24, 140)
        Me.ListViewForProducts.MinimumSize = New System.Drawing.Size(382, 249)
        Me.ListViewForProducts.Name = "ListViewForProducts"
        Me.ListViewForProducts.Size = New System.Drawing.Size(382, 249)
        Me.ListViewForProducts.TabIndex = 233
        Me.ListViewForProducts.UseCompatibleStateImageBehavior = False
        Me.ListViewForProducts.View = System.Windows.Forms.View.Details
        '
        'LabelForProductList
        '
        Me.LabelForProductList.AutoSize = True
        Me.LabelForProductList.Location = New System.Drawing.Point(21, 114)
        Me.LabelForProductList.Name = "LabelForProductList"
        Me.LabelForProductList.Size = New System.Drawing.Size(59, 13)
        Me.LabelForProductList.TabIndex = 234
        Me.LabelForProductList.Text = "Product list"
        '
        'LabelForDocuments
        '
        Me.LabelForDocuments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelForDocuments.AutoSize = True
        Me.LabelForDocuments.Location = New System.Drawing.Point(438, 114)
        Me.LabelForDocuments.Name = "LabelForDocuments"
        Me.LabelForDocuments.Size = New System.Drawing.Size(61, 13)
        Me.LabelForDocuments.TabIndex = 235
        Me.LabelForDocuments.Text = "Documents"
        '
        'FormGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(973, 429)
        Me.Controls.Add(Me.LabelForDocuments)
        Me.Controls.Add(Me.ListViewForProducts)
        Me.Controls.Add(Me.LabelForProductList)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ListViewGRU)
        Me.Controls.Add(Me.ComboBoxName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ComboBoxGroup)
        Me.Controls.Add(Me.Label10)
        Me.MinimumSize = New System.Drawing.Size(989, 468)
        Me.Name = "FormGroup"
        Me.Text = "Product Documentation Management"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ComboBoxGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ListViewGRU As System.Windows.Forms.ListView
    Friend WithEvents ListViewForProducts As ListView
    Friend WithEvents LabelForProductList As Label
    Friend WithEvents LabelForDocuments As Label


End Class
