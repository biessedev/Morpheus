﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormProduct
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormProduct))
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {""}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Times New Roman", 23.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.ButtonAddProduct = New System.Windows.Forms.Button()
        Me.ButtonQuery = New System.Windows.Forms.Button()
        Me.ButtonDelete = New System.Windows.Forms.Button()
        Me.ComboBoxCustomer = New System.Windows.Forms.ComboBox()
        Me.TextBoxDescription = New System.Windows.Forms.TextBox()
        Me.TextBoxProduct = New System.Windows.Forms.TextBox()
        Me.ListBoxLog = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBoxCa = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCe = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCc = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCb = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCd = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCf = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCg = New System.Windows.Forms.CheckBox()
        Me.ButtonCustomerAdd = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBoxStatus = New System.Windows.Forms.ComboBox()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonReset = New System.Windows.Forms.Button()
        Me.ButtonDeleteCustomer = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxPiastra = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.CheckBoxCh = New System.Windows.Forms.CheckBox()
        Me.CheckBoxci = New System.Windows.Forms.CheckBox()
        Me.CheckBoxcl = New System.Windows.Forms.CheckBox()
        Me.CheckBoxcm = New System.Windows.Forms.CheckBox()
        Me.ButtonGroup = New System.Windows.Forms.Button()
        Me.ButtonStatusUP = New System.Windows.Forms.Button()
        Me.ButtonOpenIssue = New System.Windows.Forms.Button()
        Me.ButtonOpenIssuePrint = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ButtonSIGIP = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ButtonAddMch = New System.Windows.Forms.Button()
        Me.ButtonRemoveMch = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxPcb = New System.Windows.Forms.TextBox()
        Me.ComboBoxMch = New System.Windows.Forms.ComboBox()
        Me.ListViewMch = New System.Windows.Forms.ListView()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxLS = New System.Windows.Forms.TextBox()
        Me.ButtonExport = New System.Windows.Forms.Button()
        Me.CheckBoxVis = New System.Windows.Forms.CheckBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxDAI = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonAddProduct
        '
        Me.ButtonAddProduct.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonAddProduct.BackColor = System.Drawing.Color.Transparent
        Me.ButtonAddProduct.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.ButtonAddProduct.FlatAppearance.BorderSize = 0
        Me.ButtonAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonAddProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAddProduct.Image = CType(resources.GetObject("ButtonAddProduct.Image"), System.Drawing.Image)
        Me.ButtonAddProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAddProduct.Location = New System.Drawing.Point(379, 236)
        Me.ButtonAddProduct.Name = "ButtonAddProduct"
        Me.ButtonAddProduct.Size = New System.Drawing.Size(111, 40)
        Me.ButtonAddProduct.TabIndex = 1
        Me.ButtonAddProduct.Text = "Product Add"
        Me.ButtonAddProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonAddProduct.UseVisualStyleBackColor = False
        '
        'ButtonQuery
        '
        Me.ButtonQuery.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonQuery.BackColor = System.Drawing.Color.Transparent
        Me.ButtonQuery.FlatAppearance.BorderSize = 0
        Me.ButtonQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonQuery.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonQuery.Image = CType(resources.GetObject("ButtonQuery.Image"), System.Drawing.Image)
        Me.ButtonQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonQuery.Location = New System.Drawing.Point(884, 321)
        Me.ButtonQuery.Name = "ButtonQuery"
        Me.ButtonQuery.Size = New System.Drawing.Size(68, 37)
        Me.ButtonQuery.TabIndex = 2
        Me.ButtonQuery.Text = "Query"
        Me.ButtonQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonQuery.UseVisualStyleBackColor = False
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonDelete.BackColor = System.Drawing.Color.Transparent
        Me.ButtonDelete.FlatAppearance.BorderSize = 0
        Me.ButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDelete.Image = CType(resources.GetObject("ButtonDelete.Image"), System.Drawing.Image)
        Me.ButtonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonDelete.Location = New System.Drawing.Point(379, 300)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(128, 42)
        Me.ButtonDelete.TabIndex = 3
        Me.ButtonDelete.Text = "Product Delete "
        Me.ButtonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonDelete.UseVisualStyleBackColor = False
        '
        'ComboBoxCustomer
        '
        Me.ComboBoxCustomer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ComboBoxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxCustomer.FormattingEnabled = True
        Me.ComboBoxCustomer.Location = New System.Drawing.Point(640, 51)
        Me.ComboBoxCustomer.Name = "ComboBoxCustomer"
        Me.ComboBoxCustomer.Size = New System.Drawing.Size(155, 21)
        Me.ComboBoxCustomer.TabIndex = 4
        '
        'TextBoxDescription
        '
        Me.TextBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDescription.Location = New System.Drawing.Point(201, 52)
        Me.TextBoxDescription.Name = "TextBoxDescription"
        Me.TextBoxDescription.Size = New System.Drawing.Size(419, 20)
        Me.TextBoxDescription.TabIndex = 5
        '
        'TextBoxProduct
        '
        Me.TextBoxProduct.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxProduct.Location = New System.Drawing.Point(43, 52)
        Me.TextBoxProduct.Name = "TextBoxProduct"
        Me.TextBoxProduct.Size = New System.Drawing.Size(134, 20)
        Me.TextBoxProduct.TabIndex = 6
        '
        'ListBoxLog
        '
        Me.ListBoxLog.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ListBoxLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.Location = New System.Drawing.Point(43, 216)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(311, 95)
        Me.ListBoxLog.TabIndex = 96
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(40, 200)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 95
        Me.Label2.Text = "Event Log"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(198, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "Description"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(42, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "Final Product Code"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(638, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 123
        Me.Label1.Text = "Customer"
        '
        'CheckBoxCa
        '
        Me.CheckBoxCa.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCa.AutoSize = True
        Me.CheckBoxCa.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCa.Location = New System.Drawing.Point(828, 101)
        Me.CheckBoxCa.Name = "CheckBoxCa"
        Me.CheckBoxCa.Size = New System.Drawing.Size(116, 17)
        Me.CheckBoxCa.TabIndex = 124
        Me.CheckBoxCa.Text = "A --> SMD Process"
        Me.CheckBoxCa.UseVisualStyleBackColor = False
        '
        'CheckBoxCe
        '
        Me.CheckBoxCe.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCe.AutoSize = True
        Me.CheckBoxCe.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCe.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCe.Location = New System.Drawing.Point(828, 51)
        Me.CheckBoxCe.Name = "CheckBoxCe"
        Me.CheckBoxCe.Size = New System.Drawing.Size(79, 17)
        Me.CheckBoxCe.TabIndex = 125
        Me.CheckBoxCe.Text = "E --> ENEL"
        Me.CheckBoxCe.UseVisualStyleBackColor = False
        '
        'CheckBoxCc
        '
        Me.CheckBoxCc.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCc.AutoSize = True
        Me.CheckBoxCc.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCc.Location = New System.Drawing.Point(836, 12)
        Me.CheckBoxCc.Name = "CheckBoxCc"
        Me.CheckBoxCc.Size = New System.Drawing.Size(126, 17)
        Me.CheckBoxCc.TabIndex = 127
        Me.CheckBoxCc.Text = "C-->SMD Solder Past"
        Me.CheckBoxCc.UseVisualStyleBackColor = False
        Me.CheckBoxCc.Visible = False
        '
        'CheckBoxCb
        '
        Me.CheckBoxCb.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCb.AutoSize = True
        Me.CheckBoxCb.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCb.Location = New System.Drawing.Point(684, 12)
        Me.CheckBoxCb.Name = "CheckBoxCb"
        Me.CheckBoxCb.Size = New System.Drawing.Size(133, 17)
        Me.CheckBoxCb.TabIndex = 128
        Me.CheckBoxCb.Text = "B--> All part assembled"
        Me.CheckBoxCb.UseVisualStyleBackColor = False
        Me.CheckBoxCb.Visible = False
        '
        'CheckBoxCd
        '
        Me.CheckBoxCd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCd.AutoSize = True
        Me.CheckBoxCd.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCd.Location = New System.Drawing.Point(828, 214)
        Me.CheckBoxCd.Name = "CheckBoxCd"
        Me.CheckBoxCd.Size = New System.Drawing.Size(87, 17)
        Me.CheckBoxCd.TabIndex = 129
        Me.CheckBoxCd.Text = "D --> Testing"
        Me.CheckBoxCd.UseVisualStyleBackColor = False
        '
        'CheckBoxCf
        '
        Me.CheckBoxCf.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCf.AutoSize = True
        Me.CheckBoxCf.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCf.Location = New System.Drawing.Point(828, 169)
        Me.CheckBoxCf.Name = "CheckBoxCf"
        Me.CheckBoxCf.Size = New System.Drawing.Size(92, 17)
        Me.CheckBoxCf.TabIndex = 130
        Me.CheckBoxCf.Text = "F --> Software"
        Me.CheckBoxCf.UseVisualStyleBackColor = False
        '
        'CheckBoxCg
        '
        Me.CheckBoxCg.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCg.AutoSize = True
        Me.CheckBoxCg.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCg.Location = New System.Drawing.Point(828, 236)
        Me.CheckBoxCg.Name = "CheckBoxCg"
        Me.CheckBoxCg.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxCg.TabIndex = 131
        Me.CheckBoxCg.Text = "G --> Label"
        Me.CheckBoxCg.UseVisualStyleBackColor = False
        '
        'ButtonCustomerAdd
        '
        Me.ButtonCustomerAdd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonCustomerAdd.BackColor = System.Drawing.Color.Transparent
        Me.ButtonCustomerAdd.FlatAppearance.BorderSize = 0
        Me.ButtonCustomerAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonCustomerAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonCustomerAdd.Image = CType(resources.GetObject("ButtonCustomerAdd.Image"), System.Drawing.Image)
        Me.ButtonCustomerAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCustomerAdd.Location = New System.Drawing.Point(684, 135)
        Me.ButtonCustomerAdd.Name = "ButtonCustomerAdd"
        Me.ButtonCustomerAdd.Size = New System.Drawing.Size(111, 30)
        Me.ButtonCustomerAdd.TabIndex = 133
        Me.ButtonCustomerAdd.Text = "Customer Add"
        Me.ButtonCustomerAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCustomerAdd.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(42, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 137
        Me.Label5.Text = "Status"
        '
        'ComboBoxStatus
        '
        Me.ComboBoxStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxStatus.FormattingEnabled = True
        Me.ComboBoxStatus.Location = New System.Drawing.Point(43, 149)
        Me.ComboBoxStatus.Name = "ComboBoxStatus"
        Me.ComboBoxStatus.Size = New System.Drawing.Size(226, 21)
        Me.ComboBoxStatus.TabIndex = 136
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonUpdate.BackColor = System.Drawing.Color.Transparent
        Me.ButtonUpdate.FlatAppearance.BorderSize = 0
        Me.ButtonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonUpdate.Image = CType(resources.GetObject("ButtonUpdate.Image"), System.Drawing.Image)
        Me.ButtonUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonUpdate.Location = New System.Drawing.Point(379, 268)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(128, 39)
        Me.ButtonUpdate.TabIndex = 138
        Me.ButtonUpdate.Text = "Product Update"
        Me.ButtonUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonUpdate.UseVisualStyleBackColor = False
        '
        'ButtonReset
        '
        Me.ButtonReset.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonReset.BackColor = System.Drawing.Color.Transparent
        Me.ButtonReset.FlatAppearance.BorderSize = 0
        Me.ButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonReset.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonReset.Image = CType(resources.GetObject("ButtonReset.Image"), System.Drawing.Image)
        Me.ButtonReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonReset.Location = New System.Drawing.Point(778, 324)
        Me.ButtonReset.Name = "ButtonReset"
        Me.ButtonReset.Size = New System.Drawing.Size(100, 30)
        Me.ButtonReset.TabIndex = 139
        Me.ButtonReset.Text = "Reset Field"
        Me.ButtonReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonReset.UseVisualStyleBackColor = False
        '
        'ButtonDeleteCustomer
        '
        Me.ButtonDeleteCustomer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonDeleteCustomer.BackColor = System.Drawing.Color.Transparent
        Me.ButtonDeleteCustomer.FlatAppearance.BorderSize = 0
        Me.ButtonDeleteCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonDeleteCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonDeleteCustomer.Image = CType(resources.GetObject("ButtonDeleteCustomer.Image"), System.Drawing.Image)
        Me.ButtonDeleteCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonDeleteCustomer.Location = New System.Drawing.Point(684, 172)
        Me.ButtonDeleteCustomer.Name = "ButtonDeleteCustomer"
        Me.ButtonDeleteCustomer.Size = New System.Drawing.Size(127, 23)
        Me.ButtonDeleteCustomer.TabIndex = 140
        Me.ButtonDeleteCustomer.Text = "Customer Delete"
        Me.ButtonDeleteCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonDeleteCustomer.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(377, 82)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 144
        Me.Label6.Text = "Piastra Code"
        '
        'TextBoxPiastra
        '
        Me.TextBoxPiastra.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxPiastra.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPiastra.Location = New System.Drawing.Point(380, 101)
        Me.TextBoxPiastra.Name = "TextBoxPiastra"
        Me.TextBoxPiastra.Size = New System.Drawing.Size(415, 20)
        Me.TextBoxPiastra.TabIndex = 143
        '
        'ListView1
        '
        Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ListView1.AutoArrange = False
        Me.ListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListView1.Font = New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        ListViewItem1.StateImageIndex = 0
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(43, 359)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(916, 301)
        Me.ListView1.TabIndex = 94
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'CheckBoxCh
        '
        Me.CheckBoxCh.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxCh.AutoSize = True
        Me.CheckBoxCh.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxCh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxCh.Location = New System.Drawing.Point(828, 124)
        Me.CheckBoxCh.Name = "CheckBoxCh"
        Me.CheckBoxCh.Size = New System.Drawing.Size(133, 17)
        Me.CheckBoxCh.TabIndex = 226
        Me.CheckBoxCh.Text = "H --> RX / AX Process"
        Me.CheckBoxCh.UseVisualStyleBackColor = False
        '
        'CheckBoxci
        '
        Me.CheckBoxci.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxci.AutoSize = True
        Me.CheckBoxci.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxci.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxci.Location = New System.Drawing.Point(828, 259)
        Me.CheckBoxci.Name = "CheckBoxci"
        Me.CheckBoxci.Size = New System.Drawing.Size(104, 17)
        Me.CheckBoxci.TabIndex = 225
        Me.CheckBoxci.Text = "I --> Laser Matrix"
        Me.CheckBoxci.UseVisualStyleBackColor = False
        '
        'CheckBoxcl
        '
        Me.CheckBoxcl.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxcl.AutoSize = True
        Me.CheckBoxcl.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxcl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxcl.Location = New System.Drawing.Point(828, 146)
        Me.CheckBoxcl.Name = "CheckBoxcl"
        Me.CheckBoxcl.Size = New System.Drawing.Size(120, 17)
        Me.CheckBoxcl.TabIndex = 224
        Me.CheckBoxcl.Text = "L --> Wave Process"
        Me.CheckBoxcl.UseVisualStyleBackColor = False
        '
        'CheckBoxcm
        '
        Me.CheckBoxcm.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxcm.AutoSize = True
        Me.CheckBoxcm.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxcm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxcm.Location = New System.Drawing.Point(828, 191)
        Me.CheckBoxcm.Name = "CheckBoxcm"
        Me.CheckBoxcm.Size = New System.Drawing.Size(126, 17)
        Me.CheckBoxcm.TabIndex = 227
        Me.CheckBoxcm.Text = "M --> Varnish/Potting"
        Me.CheckBoxcm.UseVisualStyleBackColor = False
        '
        'ButtonGroup
        '
        Me.ButtonGroup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonGroup.BackColor = System.Drawing.Color.Transparent
        Me.ButtonGroup.FlatAppearance.BorderSize = 0
        Me.ButtonGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonGroup.Image = CType(resources.GetObject("ButtonGroup.Image"), System.Drawing.Image)
        Me.ButtonGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonGroup.Location = New System.Drawing.Point(562, 144)
        Me.ButtonGroup.Name = "ButtonGroup"
        Me.ButtonGroup.Size = New System.Drawing.Size(68, 31)
        Me.ButtonGroup.TabIndex = 228
        Me.ButtonGroup.Text = "Group"
        Me.ButtonGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonGroup.UseVisualStyleBackColor = False
        '
        'ButtonStatusUP
        '
        Me.ButtonStatusUP.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonStatusUP.BackColor = System.Drawing.Color.Transparent
        Me.ButtonStatusUP.FlatAppearance.BorderSize = 0
        Me.ButtonStatusUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonStatusUP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonStatusUP.Image = CType(resources.GetObject("ButtonStatusUP.Image"), System.Drawing.Image)
        Me.ButtonStatusUP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonStatusUP.Location = New System.Drawing.Point(275, 149)
        Me.ButtonStatusUP.Name = "ButtonStatusUP"
        Me.ButtonStatusUP.Size = New System.Drawing.Size(90, 22)
        Me.ButtonStatusUP.TabIndex = 229
        Me.ButtonStatusUP.Text = "Status UP"
        Me.ButtonStatusUP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonStatusUP.UseVisualStyleBackColor = False
        '
        'ButtonOpenIssue
        '
        Me.ButtonOpenIssue.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonOpenIssue.BackColor = System.Drawing.Color.Transparent
        Me.ButtonOpenIssue.FlatAppearance.BorderSize = 0
        Me.ButtonOpenIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonOpenIssue.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenIssue.Image = CType(resources.GetObject("ButtonOpenIssue.Image"), System.Drawing.Image)
        Me.ButtonOpenIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOpenIssue.Location = New System.Drawing.Point(562, 175)
        Me.ButtonOpenIssue.Name = "ButtonOpenIssue"
        Me.ButtonOpenIssue.Size = New System.Drawing.Size(99, 30)
        Me.ButtonOpenIssue.TabIndex = 230
        Me.ButtonOpenIssue.Text = "Open Issue"
        Me.ButtonOpenIssue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonOpenIssue.UseVisualStyleBackColor = False
        '
        'ButtonOpenIssuePrint
        '
        Me.ButtonOpenIssuePrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonOpenIssuePrint.BackColor = System.Drawing.Color.Transparent
        Me.ButtonOpenIssuePrint.FlatAppearance.BorderSize = 0
        Me.ButtonOpenIssuePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonOpenIssuePrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonOpenIssuePrint.Image = CType(resources.GetObject("ButtonOpenIssuePrint.Image"), System.Drawing.Image)
        Me.ButtonOpenIssuePrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonOpenIssuePrint.Location = New System.Drawing.Point(562, 211)
        Me.ButtonOpenIssuePrint.Name = "ButtonOpenIssuePrint"
        Me.ButtonOpenIssuePrint.Size = New System.Drawing.Size(127, 30)
        Me.ButtonOpenIssuePrint.TabIndex = 231
        Me.ButtonOpenIssuePrint.Text = "Print Open Issue"
        Me.ButtonOpenIssuePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonOpenIssuePrint.UseVisualStyleBackColor = False
        '
        'ButtonSIGIP
        '
        Me.ButtonSIGIP.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonSIGIP.BackColor = System.Drawing.Color.Transparent
        Me.ButtonSIGIP.FlatAppearance.BorderSize = 0
        Me.ButtonSIGIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSIGIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSIGIP.Image = CType(resources.GetObject("ButtonSIGIP.Image"), System.Drawing.Image)
        Me.ButtonSIGIP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSIGIP.Location = New System.Drawing.Point(562, 247)
        Me.ButtonSIGIP.Name = "ButtonSIGIP"
        Me.ButtonSIGIP.Size = New System.Drawing.Size(174, 30)
        Me.ButtonSIGIP.TabIndex = 232
        Me.ButtonSIGIP.Text = "     Import Sigip BOM"
        Me.ButtonSIGIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSIGIP.UseVisualStyleBackColor = False
        Me.ButtonSIGIP.Visible = False
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'ButtonAddMch
        '
        Me.ButtonAddMch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonAddMch.BackColor = System.Drawing.Color.Transparent
        Me.ButtonAddMch.FlatAppearance.BorderSize = 0
        Me.ButtonAddMch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonAddMch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAddMch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonAddMch.Location = New System.Drawing.Point(43, 330)
        Me.ButtonAddMch.Name = "ButtonAddMch"
        Me.ButtonAddMch.Size = New System.Drawing.Size(82, 28)
        Me.ButtonAddMch.TabIndex = 214
        Me.ButtonAddMch.Text = "Add"
        Me.ButtonAddMch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAddMch.UseVisualStyleBackColor = False
        Me.ButtonAddMch.Visible = False
        '
        'ButtonRemoveMch
        '
        Me.ButtonRemoveMch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonRemoveMch.BackColor = System.Drawing.Color.Transparent
        Me.ButtonRemoveMch.FlatAppearance.BorderSize = 0
        Me.ButtonRemoveMch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonRemoveMch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemoveMch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonRemoveMch.Location = New System.Drawing.Point(240, 330)
        Me.ButtonRemoveMch.Name = "ButtonRemoveMch"
        Me.ButtonRemoveMch.Size = New System.Drawing.Size(115, 28)
        Me.ButtonRemoveMch.TabIndex = 215
        Me.ButtonRemoveMch.Text = "Remove"
        Me.ButtonRemoveMch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonRemoveMch.UseVisualStyleBackColor = False
        Me.ButtonRemoveMch.Visible = False
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(42, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 146
        Me.Label7.Text = "PCB Code"
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(237, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(103, 13)
        Me.Label10.TabIndex = 211
        Me.Label10.Text = "Mechanical Element"
        Me.Label10.Visible = False
        '
        'TextBoxPcb
        '
        Me.TextBoxPcb.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxPcb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxPcb.Location = New System.Drawing.Point(43, 101)
        Me.TextBoxPcb.Name = "TextBoxPcb"
        Me.TextBoxPcb.Size = New System.Drawing.Size(331, 20)
        Me.TextBoxPcb.TabIndex = 145
        '
        'ComboBoxMch
        '
        Me.ComboBoxMch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ComboBoxMch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxMch.FormattingEnabled = True
        Me.ComboBoxMch.Location = New System.Drawing.Point(367, 10)
        Me.ComboBoxMch.Name = "ComboBoxMch"
        Me.ComboBoxMch.Size = New System.Drawing.Size(312, 21)
        Me.ComboBoxMch.TabIndex = 213
        Me.ComboBoxMch.Visible = False
        '
        'ListViewMch
        '
        Me.ListViewMch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ListViewMch.CheckBoxes = True
        Me.ListViewMch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewMch.GridLines = True
        ListViewItem2.StateImageIndex = 0
        Me.ListViewMch.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem2})
        Me.ListViewMch.Location = New System.Drawing.Point(43, 231)
        Me.ListViewMch.MultiSelect = False
        Me.ListViewMch.Name = "ListViewMch"
        Me.ListViewMch.Size = New System.Drawing.Size(312, 94)
        Me.ListViewMch.TabIndex = 212
        Me.ListViewMch.UseCompatibleStateImageBehavior = False
        Me.ListViewMch.View = System.Windows.Forms.View.Details
        Me.ListViewMch.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(379, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(20, 13)
        Me.Label8.TabIndex = 234
        Me.Label8.Text = "LS"
        '
        'TextBoxLS
        '
        Me.TextBoxLS.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxLS.Location = New System.Drawing.Point(380, 153)
        Me.TextBoxLS.Name = "TextBoxLS"
        Me.TextBoxLS.Size = New System.Drawing.Size(156, 22)
        Me.TextBoxLS.TabIndex = 233
        '
        'ButtonExport
        '
        Me.ButtonExport.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ButtonExport.BackColor = System.Drawing.Color.Transparent
        Me.ButtonExport.FlatAppearance.BorderSize = 0
        Me.ButtonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonExport.Image = CType(resources.GetObject("ButtonExport.Image"), System.Drawing.Image)
        Me.ButtonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonExport.Location = New System.Drawing.Point(562, 283)
        Me.ButtonExport.Name = "ButtonExport"
        Me.ButtonExport.Size = New System.Drawing.Size(127, 30)
        Me.ButtonExport.TabIndex = 236
        Me.ButtonExport.Text = "     Export List"
        Me.ButtonExport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonExport.UseVisualStyleBackColor = False
        '
        'CheckBoxVis
        '
        Me.CheckBoxVis.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CheckBoxVis.AutoSize = True
        Me.CheckBoxVis.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxVis.Checked = True
        Me.CheckBoxVis.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxVis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxVis.Location = New System.Drawing.Point(43, 337)
        Me.CheckBoxVis.Name = "CheckBoxVis"
        Me.CheckBoxVis.Size = New System.Drawing.Size(138, 17)
        Me.CheckBoxVis.TabIndex = 237
        Me.CheckBoxVis.Text = "Show Activity / Product"
        Me.CheckBoxVis.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.BitronPoland
        Me.PictureBox1.Location = New System.Drawing.Point(899, 666)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 238
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(379, 184)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 240
        Me.Label9.Text = "DAI Number"
        '
        'TextBoxDAI
        '
        Me.TextBoxDAI.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxDAI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDAI.Location = New System.Drawing.Point(379, 199)
        Me.TextBoxDAI.Name = "TextBoxDAI"
        Me.TextBoxDAI.Size = New System.Drawing.Size(156, 22)
        Me.TextBoxDAI.TabIndex = 239
        '
        'FormProduct
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1011, 709)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxDAI)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.CheckBoxVis)
        Me.Controls.Add(Me.ButtonExport)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxLS)
        Me.Controls.Add(Me.ButtonSIGIP)
        Me.Controls.Add(Me.ButtonOpenIssuePrint)
        Me.Controls.Add(Me.ButtonOpenIssue)
        Me.Controls.Add(Me.ButtonStatusUP)
        Me.Controls.Add(Me.ButtonGroup)
        Me.Controls.Add(Me.CheckBoxcm)
        Me.Controls.Add(Me.CheckBoxCh)
        Me.Controls.Add(Me.CheckBoxci)
        Me.Controls.Add(Me.CheckBoxcl)
        Me.Controls.Add(Me.ButtonRemoveMch)
        Me.Controls.Add(Me.ButtonAddMch)
        Me.Controls.Add(Me.ComboBoxMch)
        Me.Controls.Add(Me.ListViewMch)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxPcb)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxPiastra)
        Me.Controls.Add(Me.ButtonReset)
        Me.Controls.Add(Me.ButtonDeleteCustomer)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBoxStatus)
        Me.Controls.Add(Me.ButtonCustomerAdd)
        Me.Controls.Add(Me.CheckBoxCg)
        Me.Controls.Add(Me.CheckBoxCd)
        Me.Controls.Add(Me.CheckBoxCf)
        Me.Controls.Add(Me.CheckBoxCb)
        Me.Controls.Add(Me.CheckBoxCc)
        Me.Controls.Add(Me.CheckBoxCe)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckBoxCa)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBoxLog)
        Me.Controls.Add(Me.TextBoxProduct)
        Me.Controls.Add(Me.TextBoxDescription)
        Me.Controls.Add(Me.ComboBoxCustomer)
        Me.Controls.Add(Me.ButtonDelete)
        Me.Controls.Add(Me.ButtonQuery)
        Me.Controls.Add(Me.ButtonAddProduct)
        Me.MinimumSize = New System.Drawing.Size(1027, 748)
        Me.Name = "FormProduct"
        Me.Text = "SrvDoc - Document Management System -> Product Form"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonAddProduct As System.Windows.Forms.Button
    Friend WithEvents ButtonQuery As System.Windows.Forms.Button
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents ComboBoxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxProduct As System.Windows.Forms.TextBox
    Friend WithEvents ListBoxLog As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxCa As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCe As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCc As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCd As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCf As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCg As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonCustomerAdd As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonUpdate As System.Windows.Forms.Button
    Friend WithEvents ButtonReset As System.Windows.Forms.Button
    Friend WithEvents ButtonDeleteCustomer As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPiastra As System.Windows.Forms.TextBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents CheckBoxCh As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxci As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxcl As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxcm As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonGroup As System.Windows.Forms.Button
    Friend WithEvents ButtonStatusUP As System.Windows.Forms.Button
    Friend WithEvents ButtonOpenIssue As System.Windows.Forms.Button
    Friend WithEvents ButtonOpenIssuePrint As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ButtonSIGIP As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ButtonAddMch As System.Windows.Forms.Button
    Friend WithEvents ButtonRemoveMch As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPcb As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxMch As System.Windows.Forms.ComboBox
    Friend WithEvents ListViewMch As System.Windows.Forms.ListView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxLS As System.Windows.Forms.TextBox
    Friend WithEvents ButtonExport As System.Windows.Forms.Button
    Friend WithEvents CheckBoxVis As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDAI As System.Windows.Forms.TextBox
End Class
