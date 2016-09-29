<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDownload
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {""}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, New System.Drawing.Font("Courier New", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)))
        Me.ButtonReset = New System.Windows.Forms.Button
        Me.ButtonDownload = New System.Windows.Forms.Button
        Me.ButtonBrouse = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.ButtonQuery = New System.Windows.Forms.Button
        Me.TextBoxFilePath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBoxRevision = New System.Windows.Forms.ComboBox
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.ComboBoxStatus = New System.Windows.Forms.ComboBox
        Me.CheckBoxObsolete = New System.Windows.Forms.CheckBox
        Me.CheckGru = New System.Windows.Forms.CheckBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.ComboBoxCustomer = New System.Windows.Forms.ComboBox
        Me.ComboBoxProd = New System.Windows.Forms.ComboBox
        Me.RadioButtonProductSearch = New System.Windows.Forms.RadioButton
        Me.Label9 = New System.Windows.Forms.Label
        Me.ComboBoxSign = New System.Windows.Forms.ComboBox
        Me.Label1LastBomUpdate = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.ButtonStop = New System.Windows.Forms.Button
        Me.ComboBoxEcrNull = New System.Windows.Forms.ComboBox
        Me.ComboBoxEcrPending = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.ButtonEcr = New System.Windows.Forms.Button
        Me.CheckComp = New System.Windows.Forms.CheckBox
        Me.ButtonSign = New System.Windows.Forms.Button
        Me.ButtonDelete = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.TextBoxCompPn = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.ListBoxLog = New System.Windows.Forms.ListBox
        Me.ButtonSel = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.CheckBox5 = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.CheckBox7 = New System.Windows.Forms.CheckBox
        Me.CheckBox8 = New System.Windows.Forms.CheckBox
        Me.CheckBox9 = New System.Windows.Forms.CheckBox
        Me.CheckBox10 = New System.Windows.Forms.CheckBox
        Me.CheckBox11 = New System.Windows.Forms.CheckBox
        Me.CheckBox12 = New System.Windows.Forms.CheckBox
        Me.CheckBoxSaveInfo = New System.Windows.Forms.CheckBox
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ButtonExport = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ButtonConnection = New System.Windows.Forms.Button
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.RadioButtonGeneralSearch = New System.Windows.Forms.RadioButton
        Me.ComboBoxThirdType = New System.Windows.Forms.ComboBox
        Me.TextBoxfileName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBoxFirstType = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ComboBoxSecondType = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.FileSystemWatcher1 = New System.IO.FileSystemWatcher
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel4.SuspendLayout()
        Me.FlowLayoutPanel2.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonReset
        '
        Me.ButtonReset.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonReset.Location = New System.Drawing.Point(4, 42)
        Me.ButtonReset.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonReset.Name = "ButtonReset"
        Me.ButtonReset.Size = New System.Drawing.Size(59, 30)
        Me.ButtonReset.TabIndex = 62
        Me.ButtonReset.Text = "Reset Type"
        Me.ButtonReset.UseVisualStyleBackColor = True
        '
        'ButtonDownload
        '
        Me.ButtonDownload.Location = New System.Drawing.Point(217, 4)
        Me.ButtonDownload.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDownload.Name = "ButtonDownload"
        Me.ButtonDownload.Size = New System.Drawing.Size(95, 30)
        Me.ButtonDownload.TabIndex = 65
        Me.ButtonDownload.Text = "Download"
        Me.ButtonDownload.UseVisualStyleBackColor = True
        '
        'ButtonBrouse
        '
        Me.ButtonBrouse.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonBrouse.Location = New System.Drawing.Point(114, 4)
        Me.ButtonBrouse.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonBrouse.Name = "ButtonBrouse"
        Me.ButtonBrouse.Size = New System.Drawing.Size(95, 30)
        Me.ButtonBrouse.TabIndex = 78
        Me.ButtonBrouse.Text = "Browse"
        Me.ButtonBrouse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(4, 10)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 17)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "Download path"
        '
        'ButtonQuery
        '
        Me.ButtonQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonQuery.Location = New System.Drawing.Point(826, 268)
        Me.ButtonQuery.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonQuery.Name = "ButtonQuery"
        Me.ButtonQuery.Size = New System.Drawing.Size(277, 78)
        Me.ButtonQuery.TabIndex = 85
        Me.ButtonQuery.Text = "Query"
        Me.ButtonQuery.UseVisualStyleBackColor = True
        '
        'TextBoxFilePath
        '
        Me.TextBoxFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBoxFilePath.Location = New System.Drawing.Point(4, 42)
        Me.TextBoxFilePath.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxFilePath.Name = "TextBoxFilePath"
        Me.TextBoxFilePath.Size = New System.Drawing.Size(508, 23)
        Me.TextBoxFilePath.TabIndex = 79
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(8, 201)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 17)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "Revision"
        '
        'ComboBoxRevision
        '
        Me.ComboBoxRevision.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxRevision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxRevision.FormattingEnabled = True
        Me.ComboBoxRevision.Location = New System.Drawing.Point(11, 223)
        Me.ComboBoxRevision.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxRevision.Name = "ComboBoxRevision"
        Me.ComboBoxRevision.Size = New System.Drawing.Size(156, 25)
        Me.ComboBoxRevision.TabIndex = 72
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.ComboBoxStatus)
        Me.GroupBox1.Controls.Add(Me.CheckBoxObsolete)
        Me.GroupBox1.Controls.Add(Me.CheckGru)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.ComboBoxCustomer)
        Me.GroupBox1.Controls.Add(Me.ComboBoxProd)
        Me.GroupBox1.Controls.Add(Me.RadioButtonProductSearch)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.ComboBoxRevision)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ComboBoxSign)
        Me.GroupBox1.Controls.Add(Me.Label1LastBomUpdate)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.ButtonStop)
        Me.GroupBox1.Controls.Add(Me.ComboBoxEcrNull)
        Me.GroupBox1.Controls.Add(Me.ComboBoxEcrPending)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.ButtonEcr)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(333, 4)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(485, 256)
        Me.GroupBox1.TabIndex = 87
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "         Product Document"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(239, 42)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(48, 17)
        Me.Label14.TabIndex = 145
        Me.Label14.Text = "Status"
        '
        'ComboBoxStatus
        '
        Me.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStatus.Enabled = False
        Me.ComboBoxStatus.FormattingEnabled = True
        Me.ComboBoxStatus.Location = New System.Drawing.Point(242, 63)
        Me.ComboBoxStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxStatus.Name = "ComboBoxStatus"
        Me.ComboBoxStatus.Size = New System.Drawing.Size(230, 25)
        Me.ComboBoxStatus.TabIndex = 144
        '
        'CheckBoxObsolete
        '
        Me.CheckBoxObsolete.AutoSize = True
        Me.CheckBoxObsolete.Enabled = False
        Me.CheckBoxObsolete.Location = New System.Drawing.Point(84, 38)
        Me.CheckBoxObsolete.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxObsolete.Name = "CheckBoxObsolete"
        Me.CheckBoxObsolete.Size = New System.Drawing.Size(122, 21)
        Me.CheckBoxObsolete.TabIndex = 143
        Me.CheckBoxObsolete.Text = "Show obsolete"
        Me.CheckBoxObsolete.UseVisualStyleBackColor = True
        '
        'CheckGru
        '
        Me.CheckGru.AutoSize = True
        Me.CheckGru.Checked = True
        Me.CheckGru.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckGru.Enabled = False
        Me.CheckGru.Location = New System.Drawing.Point(242, 92)
        Me.CheckGru.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckGru.Name = "CheckGru"
        Me.CheckGru.Size = New System.Drawing.Size(184, 21)
        Me.CheckGru.TabIndex = 125
        Me.CheckGru.Text = "Grugliasco Intranet DOC"
        Me.CheckGru.UseVisualStyleBackColor = True
        Me.CheckGru.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(8, 42)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 17)
        Me.Label13.TabIndex = 124
        Me.Label13.Text = "Customer"
        '
        'ComboBoxCustomer
        '
        Me.ComboBoxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCustomer.Enabled = False
        Me.ComboBoxCustomer.FormattingEnabled = True
        Me.ComboBoxCustomer.Location = New System.Drawing.Point(6, 63)
        Me.ComboBoxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxCustomer.Name = "ComboBoxCustomer"
        Me.ComboBoxCustomer.Size = New System.Drawing.Size(208, 25)
        Me.ComboBoxCustomer.TabIndex = 123
        '
        'ComboBoxProd
        '
        Me.ComboBoxProd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxProd.Enabled = False
        Me.ComboBoxProd.FormattingEnabled = True
        Me.ComboBoxProd.Location = New System.Drawing.Point(8, 116)
        Me.ComboBoxProd.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxProd.Name = "ComboBoxProd"
        Me.ComboBoxProd.Size = New System.Drawing.Size(464, 25)
        Me.ComboBoxProd.TabIndex = 120
        '
        'RadioButtonProductSearch
        '
        Me.RadioButtonProductSearch.AutoSize = True
        Me.RadioButtonProductSearch.Location = New System.Drawing.Point(27, 0)
        Me.RadioButtonProductSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButtonProductSearch.Name = "RadioButtonProductSearch"
        Me.RadioButtonProductSearch.Size = New System.Drawing.Size(17, 16)
        Me.RadioButtonProductSearch.TabIndex = 119
        Me.RadioButtonProductSearch.TabStop = True
        Me.RadioButtonProductSearch.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Enabled = False
        Me.Label9.Location = New System.Drawing.Point(8, 95)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 17)
        Me.Label9.TabIndex = 90
        Me.Label9.Text = "Final Product"
        '
        'ComboBoxSign
        '
        Me.ComboBoxSign.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSign.FormattingEnabled = True
        Me.ComboBoxSign.Location = New System.Drawing.Point(316, 223)
        Me.ComboBoxSign.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxSign.Name = "ComboBoxSign"
        Me.ComboBoxSign.Size = New System.Drawing.Size(156, 25)
        Me.ComboBoxSign.TabIndex = 122
        '
        'Label1LastBomUpdate
        '
        Me.Label1LastBomUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1LastBomUpdate.AutoSize = True
        Me.Label1LastBomUpdate.BackColor = System.Drawing.Color.Transparent
        Me.Label1LastBomUpdate.Location = New System.Drawing.Point(8, 20)
        Me.Label1LastBomUpdate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1LastBomUpdate.Name = "Label1LastBomUpdate"
        Me.Label1LastBomUpdate.Size = New System.Drawing.Size(117, 17)
        Me.Label1LastBomUpdate.TabIndex = 146
        Me.Label1LastBomUpdate.Text = "Last Bom Update"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(313, 202)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 17)
        Me.Label11.TabIndex = 123
        Me.Label11.Text = "Sign"
        '
        'ButtonStop
        '
        Me.ButtonStop.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonStop.Location = New System.Drawing.Point(187, 219)
        Me.ButtonStop.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonStop.Name = "ButtonStop"
        Me.ButtonStop.Size = New System.Drawing.Size(108, 30)
        Me.ButtonStop.TabIndex = 130
        Me.ButtonStop.Text = "Stop"
        Me.ButtonStop.UseVisualStyleBackColor = True
        '
        'ComboBoxEcrNull
        '
        Me.ComboBoxEcrNull.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxEcrNull.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEcrNull.FormattingEnabled = True
        Me.ComboBoxEcrNull.Location = New System.Drawing.Point(316, 170)
        Me.ComboBoxEcrNull.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxEcrNull.Name = "ComboBoxEcrNull"
        Me.ComboBoxEcrNull.Size = New System.Drawing.Size(156, 25)
        Me.ComboBoxEcrNull.TabIndex = 126
        Me.ComboBoxEcrNull.Visible = False
        '
        'ComboBoxEcrPending
        '
        Me.ComboBoxEcrPending.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxEcrPending.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEcrPending.FormattingEnabled = True
        Me.ComboBoxEcrPending.Location = New System.Drawing.Point(11, 175)
        Me.ComboBoxEcrPending.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxEcrPending.Name = "ComboBoxEcrPending"
        Me.ComboBoxEcrPending.Size = New System.Drawing.Size(156, 25)
        Me.ComboBoxEcrPending.TabIndex = 125
        Me.ComboBoxEcrPending.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(196, 152)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 17)
        Me.Label1.TabIndex = 127
        Me.Label1.Text = "EcrPending"
        Me.Label1.Visible = False
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(313, 152)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 17)
        Me.Label7.TabIndex = 128
        Me.Label7.Text = "EcrNull"
        Me.Label7.Visible = False
        '
        'ButtonEcr
        '
        Me.ButtonEcr.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonEcr.Location = New System.Drawing.Point(186, 170)
        Me.ButtonEcr.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonEcr.Name = "ButtonEcr"
        Me.ButtonEcr.Size = New System.Drawing.Size(108, 30)
        Me.ButtonEcr.TabIndex = 129
        Me.ButtonEcr.Text = "Null ECR"
        Me.ButtonEcr.UseVisualStyleBackColor = True
        Me.ButtonEcr.Visible = False
        '
        'CheckComp
        '
        Me.CheckComp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.CheckComp.AutoSize = True
        Me.CheckComp.BackColor = System.Drawing.Color.Transparent
        Me.CheckComp.Checked = True
        Me.CheckComp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckComp.Enabled = False
        Me.CheckComp.Location = New System.Drawing.Point(4, 52)
        Me.CheckComp.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckComp.Name = "CheckComp"
        Me.CheckComp.Size = New System.Drawing.Size(156, 21)
        Me.CheckComp.TabIndex = 145
        Me.CheckComp.Text = "Add component info"
        Me.CheckComp.UseVisualStyleBackColor = False
        '
        'ButtonSign
        '
        Me.ButtonSign.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonSign.Location = New System.Drawing.Point(4, 80)
        Me.ButtonSign.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSign.Name = "ButtonSign"
        Me.ButtonSign.Size = New System.Drawing.Size(59, 30)
        Me.ButtonSign.TabIndex = 89
        Me.ButtonSign.Text = "Sign"
        Me.ButtonSign.UseVisualStyleBackColor = True
        '
        'ButtonDelete
        '
        Me.ButtonDelete.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonDelete.Location = New System.Drawing.Point(4, 118)
        Me.ButtonDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonDelete.Name = "ButtonDelete"
        Me.ButtonDelete.Size = New System.Drawing.Size(59, 30)
        Me.ButtonDelete.TabIndex = 92
        Me.ButtonDelete.Text = "Delete"
        Me.ButtonDelete.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        ListViewItem1.StateImageIndex = 0
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(4, 407)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1105, 196)
        Me.ListView1.TabIndex = 93
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'TextBoxCompPn
        '
        Me.TextBoxCompPn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TextBoxCompPn.Enabled = False
        Me.TextBoxCompPn.Location = New System.Drawing.Point(4, 21)
        Me.TextBoxCompPn.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxCompPn.Name = "TextBoxCompPn"
        Me.TextBoxCompPn.Size = New System.Drawing.Size(187, 23)
        Me.TextBoxCompPn.TabIndex = 119
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 17)
        Me.Label12.TabIndex = 120
        Me.Label12.Text = "Component part number filter"
        '
        'ListBoxLog
        '
        Me.ListBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBoxLog.FormattingEnabled = True
        Me.ListBoxLog.ItemHeight = 17
        Me.ListBoxLog.Location = New System.Drawing.Point(4, 42)
        Me.ListBoxLog.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBoxLog.Name = "ListBoxLog"
        Me.ListBoxLog.Size = New System.Drawing.Size(274, 206)
        Me.ListBoxLog.TabIndex = 120
        '
        'ButtonSel
        '
        Me.ButtonSel.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonSel.Location = New System.Drawing.Point(4, 4)
        Me.ButtonSel.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSel.Name = "ButtonSel"
        Me.ButtonSel.Size = New System.Drawing.Size(59, 30)
        Me.ButtonSel.TabIndex = 124
        Me.ButtonSel.Text = "Select All"
        Me.ButtonSel.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'CheckBox1
        '
        Me.CheckBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(113, 4)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(43, 21)
        Me.CheckBox1.TabIndex = 131
        Me.CheckBox1.Text = "ID"
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'CheckBox2
        '
        Me.CheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox2.Checked = True
        Me.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox2.Location = New System.Drawing.Point(164, 4)
        Me.CheckBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(77, 21)
        Me.CheckBox2.TabIndex = 132
        Me.CheckBox2.Text = "Header"
        Me.CheckBox2.UseVisualStyleBackColor = False
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Location = New System.Drawing.Point(249, 4)
        Me.CheckBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(67, 21)
        Me.CheckBox3.TabIndex = 133
        Me.CheckBox3.Text = "Name"
        Me.CheckBox3.UseVisualStyleBackColor = False
        '
        'CheckBox4
        '
        Me.CheckBox4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox4.Location = New System.Drawing.Point(324, 4)
        Me.CheckBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(50, 21)
        Me.CheckBox4.TabIndex = 134
        Me.CheckBox4.Text = "rev"
        Me.CheckBox4.UseVisualStyleBackColor = False
        '
        'CheckBox5
        '
        Me.CheckBox5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox5.Location = New System.Drawing.Point(382, 4)
        Me.CheckBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(49, 21)
        Me.CheckBox5.TabIndex = 135
        Me.CheckBox5.Text = "Ext"
        Me.CheckBox5.UseVisualStyleBackColor = False
        '
        'CheckBox6
        '
        Me.CheckBox6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox6.Location = New System.Drawing.Point(439, 4)
        Me.CheckBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(85, 21)
        Me.CheckBox6.TabIndex = 136
        Me.CheckBox6.Text = "RevNote"
        Me.CheckBox6.UseVisualStyleBackColor = False
        '
        'CheckBox7
        '
        Me.CheckBox7.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox7.Location = New System.Drawing.Point(532, 4)
        Me.CheckBox7.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(58, 21)
        Me.CheckBox7.TabIndex = 137
        Me.CheckBox7.Text = "Sign"
        Me.CheckBox7.UseVisualStyleBackColor = False
        '
        'CheckBox8
        '
        Me.CheckBox8.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox8.Location = New System.Drawing.Point(598, 4)
        Me.CheckBox8.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(67, 21)
        Me.CheckBox8.TabIndex = 138
        Me.CheckBox8.Text = "Editor"
        Me.CheckBox8.UseVisualStyleBackColor = False
        '
        'CheckBox9
        '
        Me.CheckBox9.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox9.Location = New System.Drawing.Point(673, 4)
        Me.CheckBox9.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(75, 21)
        Me.CheckBox9.TabIndex = 139
        Me.CheckBox9.Text = "Control"
        Me.CheckBox9.UseVisualStyleBackColor = False
        '
        'CheckBox10
        '
        Me.CheckBox10.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox10.AutoSize = True
        Me.CheckBox10.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox10.Location = New System.Drawing.Point(756, 4)
        Me.CheckBox10.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(103, 21)
        Me.CheckBox10.TabIndex = 140
        Me.CheckBox10.Text = "EcrPending"
        Me.CheckBox10.UseVisualStyleBackColor = False
        '
        'CheckBox11
        '
        Me.CheckBox11.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox11.AutoSize = True
        Me.CheckBox11.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox11.Location = New System.Drawing.Point(867, 4)
        Me.CheckBox11.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(75, 21)
        Me.CheckBox11.TabIndex = 141
        Me.CheckBox11.Text = "EcrNull"
        Me.CheckBox11.UseVisualStyleBackColor = False
        '
        'CheckBox12
        '
        Me.CheckBox12.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBox12.AutoSize = True
        Me.CheckBox12.BackColor = System.Drawing.Color.Transparent
        Me.CheckBox12.Checked = True
        Me.CheckBox12.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox12.Location = New System.Drawing.Point(4, 4)
        Me.CheckBox12.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.Size = New System.Drawing.Size(101, 21)
        Me.CheckBox12.TabIndex = 142
        Me.CheckBox12.Text = "Description"
        Me.CheckBox12.UseVisualStyleBackColor = False
        '
        'CheckBoxSaveInfo
        '
        Me.CheckBoxSaveInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSaveInfo.AutoSize = True
        Me.CheckBoxSaveInfo.BackColor = System.Drawing.Color.Transparent
        Me.CheckBoxSaveInfo.Location = New System.Drawing.Point(107, 4)
        Me.CheckBoxSaveInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxSaveInfo.Name = "CheckBoxSaveInfo"
        Me.CheckBoxSaveInfo.Size = New System.Drawing.Size(89, 30)
        Me.CheckBoxSaveInfo.TabIndex = 144
        Me.CheckBoxSaveInfo.Text = "Save info"
        Me.CheckBoxSaveInfo.UseVisualStyleBackColor = False
        '
        'ButtonExport
        '
        Me.ButtonExport.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonExport.Location = New System.Drawing.Point(4, 4)
        Me.ButtonExport.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonExport.Name = "ButtonExport"
        Me.ButtonExport.Size = New System.Drawing.Size(95, 30)
        Me.ButtonExport.TabIndex = 147
        Me.ButtonExport.Text = "Export list"
        Me.ButtonExport.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(1161, 969)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(135, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 493.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonConnection, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel4, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel2, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FlowLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonQuery, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 264.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1107, 350)
        Me.TableLayoutPanel1.TabIndex = 233
        '
        'ButtonConnection
        '
        Me.ButtonConnection.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ButtonConnection.Location = New System.Drawing.Point(260, 268)
        Me.ButtonConnection.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonConnection.Name = "ButtonConnection"
        Me.ButtonConnection.Size = New System.Drawing.Size(65, 78)
        Me.ButtonConnection.TabIndex = 236
        Me.ButtonConnection.Text = "Gru Conn"
        Me.ButtonConnection.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.Controls.Add(Me.ButtonExport)
        Me.FlowLayoutPanel4.Controls.Add(Me.CheckBoxSaveInfo)
        Me.FlowLayoutPanel4.Controls.Add(Me.ListBoxLog)
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(825, 3)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(279, 258)
        Me.FlowLayoutPanel4.TabIndex = 235
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel2.Controls.Add(Me.Label4)
        Me.FlowLayoutPanel2.Controls.Add(Me.ButtonBrouse)
        Me.FlowLayoutPanel2.Controls.Add(Me.ButtonDownload)
        Me.FlowLayoutPanel2.Controls.Add(Me.TextBoxFilePath)
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(332, 267)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(487, 80)
        Me.FlowLayoutPanel2.TabIndex = 236
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.Controls.Add(Me.Label12)
        Me.FlowLayoutPanel3.Controls.Add(Me.TextBoxCompPn)
        Me.FlowLayoutPanel3.Controls.Add(Me.CheckComp)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(3, 267)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(250, 80)
        Me.FlowLayoutPanel3.TabIndex = 237
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonSel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonDelete, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonSign, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.ButtonReset, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(259, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(67, 258)
        Me.TableLayoutPanel2.TabIndex = 125
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.RadioButtonGeneralSearch)
        Me.GroupBox2.Controls.Add(Me.ComboBoxThirdType)
        Me.GroupBox2.Controls.Add(Me.TextBoxfileName)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ComboBoxFirstType)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.ComboBoxSecondType)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(248, 256)
        Me.GroupBox2.TabIndex = 88
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "         General Search"
        '
        'RadioButtonGeneralSearch
        '
        Me.RadioButtonGeneralSearch.AutoSize = True
        Me.RadioButtonGeneralSearch.Location = New System.Drawing.Point(23, 0)
        Me.RadioButtonGeneralSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.RadioButtonGeneralSearch.Name = "RadioButtonGeneralSearch"
        Me.RadioButtonGeneralSearch.Size = New System.Drawing.Size(17, 16)
        Me.RadioButtonGeneralSearch.TabIndex = 118
        Me.RadioButtonGeneralSearch.TabStop = True
        Me.RadioButtonGeneralSearch.UseVisualStyleBackColor = True
        '
        'ComboBoxThirdType
        '
        Me.ComboBoxThirdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxThirdType.FormattingEnabled = True
        Me.ComboBoxThirdType.Location = New System.Drawing.Point(8, 149)
        Me.ComboBoxThirdType.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxThirdType.MaxDropDownItems = 16
        Me.ComboBoxThirdType.Name = "ComboBoxThirdType"
        Me.ComboBoxThirdType.Size = New System.Drawing.Size(230, 25)
        Me.ComboBoxThirdType.TabIndex = 73
        '
        'TextBoxfileName
        '
        Me.TextBoxfileName.Location = New System.Drawing.Point(8, 204)
        Me.TextBoxfileName.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxfileName.Name = "TextBoxfileName"
        Me.TextBoxfileName.Size = New System.Drawing.Size(229, 23)
        Me.TextBoxfileName.TabIndex = 66
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(4, 181)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 17)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "File name"
        '
        'ComboBoxFirstType
        '
        Me.ComboBoxFirstType.Cursor = System.Windows.Forms.Cursors.Default
        Me.ComboBoxFirstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFirstType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ComboBoxFirstType.FormattingEnabled = True
        Me.ComboBoxFirstType.Location = New System.Drawing.Point(8, 42)
        Me.ComboBoxFirstType.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxFirstType.Name = "ComboBoxFirstType"
        Me.ComboBoxFirstType.Size = New System.Drawing.Size(230, 25)
        Me.ComboBoxFirstType.TabIndex = 75
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(4, 125)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 17)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "Third type"
        '
        'ComboBoxSecondType
        '
        Me.ComboBoxSecondType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSecondType.FormattingEnabled = True
        Me.ComboBoxSecondType.Location = New System.Drawing.Point(8, 92)
        Me.ComboBoxSecondType.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxSecondType.Name = "ComboBoxSecondType"
        Me.ComboBoxSecondType.Size = New System.Drawing.Size(230, 25)
        Me.ComboBoxSecondType.TabIndex = 76
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(4, 22)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 17)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "First Type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(4, 74)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "Second type"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ListView1, 0, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 356.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1113, 607)
        Me.TableLayoutPanel3.TabIndex = 234
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox12)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox1)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox2)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox3)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox4)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox5)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox6)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox7)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox8)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox9)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox10)
        Me.FlowLayoutPanel1.Controls.Add(Me.CheckBox11)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 359)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1107, 41)
        Me.FlowLayoutPanel1.TabIndex = 235
        '
        'FileSystemWatcher1
        '
        Me.FileSystemWatcher1.EnableRaisingEvents = True
        Me.FileSystemWatcher1.SynchronizingObject = Me
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.BackColor = System.Drawing.Color.DarkGray
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1119, 613)
        Me.TableLayoutPanel4.TabIndex = 235
        '
        'FormDownload
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1119, 613)
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormDownload"
        Me.Text = "SrvDoc - Document Management System -> Search Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel4.ResumeLayout(False)
        Me.FlowLayoutPanel4.PerformLayout()
        Me.FlowLayoutPanel2.ResumeLayout(False)
        Me.FlowLayoutPanel2.PerformLayout()
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        CType(Me.FileSystemWatcher1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonReset As System.Windows.Forms.Button
    Friend WithEvents ButtonDownload As System.Windows.Forms.Button
    Friend WithEvents ButtonBrouse As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ButtonQuery As System.Windows.Forms.Button
    Friend WithEvents TextBoxFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxRevision As System.Windows.Forms.ComboBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ButtonSign As System.Windows.Forms.Button
    Friend WithEvents ButtonDelete As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents RadioButtonProductSearch As System.Windows.Forms.RadioButton
    Friend WithEvents ListBoxLog As System.Windows.Forms.ListBox
    Friend WithEvents ComboBoxSign As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ButtonSel As System.Windows.Forms.Button
    Friend WithEvents ComboBoxEcrPending As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxEcrNull As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ButtonEcr As System.Windows.Forms.Button
    Friend WithEvents ComboBoxProd As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonStop As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents CheckGru As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxSaveInfo As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CheckBoxObsolete As System.Windows.Forms.CheckBox
    Friend WithEvents CheckComp As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxCompPn As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ButtonExport As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonGeneralSearch As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBoxThirdType As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxfileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFirstType As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxSecondType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FileSystemWatcher1 As System.IO.FileSystemWatcher
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel4 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1LastBomUpdate As System.Windows.Forms.Label
    Friend WithEvents ButtonConnection As System.Windows.Forms.Button
End Class
