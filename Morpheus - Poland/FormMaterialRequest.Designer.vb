Imports ADGV

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMaterialRequest
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
        Me.components = New System.ComponentModel.Container()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dataSet = New System.Data.DataSet()
        Me.dataGridView = New Zuby.ADGV.AdvancedDataGridView()
        Me.ContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ButtonClearFilter = New System.Windows.Forms.Button()
        Me.ButtonClearSort = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        CType(Me.BindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dataSet,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = Me.dataSet
        Me.BindingSource.Position = 0
        '
        'dataSet
        '
        Me.dataSet.DataSetName = "NewDataSet"
        '
        'dataGridView
        '
        Me.dataGridView.AllowUserToDeleteRows = false
        Me.dataGridView.AllowUserToOrderColumns = true
        Me.dataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.dataGridView.AutoGenerateColumns = false
        Me.dataGridView.ColumnHeadersHeight = 22
        Me.dataGridView.DataSource = Me.BindingSource
        Me.dataGridView.FilterAndSortEnabled = true
        Me.dataGridView.Location = New System.Drawing.Point(0, 41)
        Me.dataGridView.Name = "dataGridView"
        Me.dataGridView.Size = New System.Drawing.Size(1041, 600)
        Me.dataGridView.TabIndex = 2
        '
        'ContextMenuStrip
        '
        Me.ContextMenuStrip.Name = "ContextMenuStrip"
        Me.ContextMenuStrip.Size = New System.Drawing.Size(61, 4)
        '
        'ButtonClearFilter
        '
        Me.ButtonClearFilter.Location = New System.Drawing.Point(41, 11)
        Me.ButtonClearFilter.Name = "ButtonClearFilter"
        Me.ButtonClearFilter.Size = New System.Drawing.Size(98, 24)
        Me.ButtonClearFilter.TabIndex = 3
        Me.ButtonClearFilter.Text = "Clear Filter"
        Me.ButtonClearFilter.UseVisualStyleBackColor = true
        '
        'ButtonClearSort
        '
        Me.ButtonClearSort.Location = New System.Drawing.Point(165, 11)
        Me.ButtonClearSort.Name = "ButtonClearSort"
        Me.ButtonClearSort.Size = New System.Drawing.Size(98, 24)
        Me.ButtonClearSort.TabIndex = 4
        Me.ButtonClearSort.Text = "Clear Sort"
        Me.ButtonClearSort.UseVisualStyleBackColor = true
        '
        'ButtonSave
        '
        Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ButtonSave.Location = New System.Drawing.Point(931, 12)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(98, 23)
        Me.ButtonSave.TabIndex = 5
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.UseVisualStyleBackColor = true
        '
        'FormMaterialRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1041, 641)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonClearSort)
        Me.Controls.Add(Me.ButtonClearFilter)
        Me.Controls.Add(Me.dataGridView)
        Me.Name = "FormMaterialRequest"
        Me.Text = "ADGVSample"
        CType(Me.BindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dataSet,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.dataGridView,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub






    Friend WithEvents dataGridView As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents dataSet As Data.DataSet
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents ContextMenuStrip As ContextMenuStrip
    Friend WithEvents ButtonClearFilter As Button
    Friend WithEvents ButtonClearSort As Button
    Friend WithEvents ButtonSave As Button
End Class
