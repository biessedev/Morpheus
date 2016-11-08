<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormStart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormStart))
        Me.ButtonDocManagement = New System.Windows.Forms.Button()
        Me.ButtonProduct = New System.Windows.Forms.Button()
        Me.ButtonECR = New System.Windows.Forms.Button()
        Me.ButtonAbout = New System.Windows.Forms.Button()
        Me.ButtonSystem = New System.Windows.Forms.Button()
        Me.ButtonTypeEdit = New System.Windows.Forms.Button()
        Me.ButtonLoadDoc = New System.Windows.Forms.Button()
        Me.ButtonHelp = New System.Windows.Forms.Button()
        Me.ButtonNpi = New System.Windows.Forms.Button()
        Me.ButtonQuote = New System.Windows.Forms.Button()
        Me.ButtonCommit = New System.Windows.Forms.Button()
        Me.ButtonEq = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ButtonMould = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonProjectShow = New System.Windows.Forms.Button()
        Me.ButtonTiming = New System.Windows.Forms.Button()
        Me.ButtonRunning = New System.Windows.Forms.Button()
        Me.ButtonBom = New System.Windows.Forms.Button()
        Me.ButtonCrypted = New System.Windows.Forms.Button()
        Me.ButtonChangePassword = New System.Windows.Forms.Button()
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'ButtonDocManagement
        '
        Me.ButtonDocManagement.BackColor = System.Drawing.Color.Transparent
        Me.ButtonDocManagement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonDocManagement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonDocManagement.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonDocManagement.Image = CType(resources.GetObject("ButtonDocManagement.Image"),System.Drawing.Image)
        Me.ButtonDocManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonDocManagement.Location = New System.Drawing.Point(43, 141)
        Me.ButtonDocManagement.Name = "ButtonDocManagement"
        Me.ButtonDocManagement.Size = New System.Drawing.Size(240, 40)
        Me.ButtonDocManagement.TabIndex = 0
        Me.ButtonDocManagement.Text = "Document Manager"
        Me.ButtonDocManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonDocManagement.UseVisualStyleBackColor = false
        '
        'ButtonProduct
        '
        Me.ButtonProduct.BackColor = System.Drawing.Color.Transparent
        Me.ButtonProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonProduct.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonProduct.Image = CType(resources.GetObject("ButtonProduct.Image"),System.Drawing.Image)
        Me.ButtonProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonProduct.Location = New System.Drawing.Point(43, 187)
        Me.ButtonProduct.Name = "ButtonProduct"
        Me.ButtonProduct.Size = New System.Drawing.Size(240, 40)
        Me.ButtonProduct.TabIndex = 1
        Me.ButtonProduct.Text = "Product Manager"
        Me.ButtonProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProduct.UseVisualStyleBackColor = false
        '
        'ButtonECR
        '
        Me.ButtonECR.BackColor = System.Drawing.Color.Transparent
        Me.ButtonECR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonECR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonECR.Enabled = false
        Me.ButtonECR.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonECR.Image = CType(resources.GetObject("ButtonECR.Image"),System.Drawing.Image)
        Me.ButtonECR.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonECR.Location = New System.Drawing.Point(43, 233)
        Me.ButtonECR.Name = "ButtonECR"
        Me.ButtonECR.Size = New System.Drawing.Size(240, 40)
        Me.ButtonECR.TabIndex = 2
        Me.ButtonECR.Text = "ECR"
        Me.ButtonECR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonECR.UseVisualStyleBackColor = false
        '
        'ButtonAbout
        '
        Me.ButtonAbout.BackColor = System.Drawing.Color.Transparent
        Me.ButtonAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonAbout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonAbout.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonAbout.Image = CType(resources.GetObject("ButtonAbout.Image"),System.Drawing.Image)
        Me.ButtonAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonAbout.Location = New System.Drawing.Point(43, 325)
        Me.ButtonAbout.Name = "ButtonAbout"
        Me.ButtonAbout.Size = New System.Drawing.Size(240, 40)
        Me.ButtonAbout.TabIndex = 3
        Me.ButtonAbout.Text = "About SrvDoc"
        Me.ButtonAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAbout.UseVisualStyleBackColor = false
        '
        'ButtonSystem
        '
        Me.ButtonSystem.BackColor = System.Drawing.Color.Transparent
        Me.ButtonSystem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonSystem.Enabled = false
        Me.ButtonSystem.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonSystem.Image = CType(resources.GetObject("ButtonSystem.Image"),System.Drawing.Image)
        Me.ButtonSystem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSystem.Location = New System.Drawing.Point(43, 371)
        Me.ButtonSystem.Name = "ButtonSystem"
        Me.ButtonSystem.Size = New System.Drawing.Size(240, 40)
        Me.ButtonSystem.TabIndex = 4
        Me.ButtonSystem.Text = "Server Service "
        Me.ButtonSystem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSystem.UseVisualStyleBackColor = false
        '
        'ButtonTypeEdit
        '
        Me.ButtonTypeEdit.BackColor = System.Drawing.Color.Transparent
        Me.ButtonTypeEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonTypeEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonTypeEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonTypeEdit.Image = CType(resources.GetObject("ButtonTypeEdit.Image"),System.Drawing.Image)
        Me.ButtonTypeEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonTypeEdit.Location = New System.Drawing.Point(43, 49)
        Me.ButtonTypeEdit.Name = "ButtonTypeEdit"
        Me.ButtonTypeEdit.Size = New System.Drawing.Size(240, 40)
        Me.ButtonTypeEdit.TabIndex = 5
        Me.ButtonTypeEdit.Text = "Doc Type"
        Me.ButtonTypeEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonTypeEdit.UseVisualStyleBackColor = false
        '
        'ButtonLoadDoc
        '
        Me.ButtonLoadDoc.BackColor = System.Drawing.Color.Transparent
        Me.ButtonLoadDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonLoadDoc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonLoadDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonLoadDoc.Image = CType(resources.GetObject("ButtonLoadDoc.Image"),System.Drawing.Image)
        Me.ButtonLoadDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonLoadDoc.Location = New System.Drawing.Point(43, 95)
        Me.ButtonLoadDoc.Name = "ButtonLoadDoc"
        Me.ButtonLoadDoc.Size = New System.Drawing.Size(240, 40)
        Me.ButtonLoadDoc.TabIndex = 6
        Me.ButtonLoadDoc.Text = "Upload Document"
        Me.ButtonLoadDoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonLoadDoc.UseVisualStyleBackColor = false
        '
        'ButtonHelp
        '
        Me.ButtonHelp.BackColor = System.Drawing.Color.Transparent
        Me.ButtonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonHelp.Image = CType(resources.GetObject("ButtonHelp.Image"),System.Drawing.Image)
        Me.ButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonHelp.Location = New System.Drawing.Point(353, 371)
        Me.ButtonHelp.Name = "ButtonHelp"
        Me.ButtonHelp.Size = New System.Drawing.Size(242, 39)
        Me.ButtonHelp.TabIndex = 7
        Me.ButtonHelp.Text = "Help"
        Me.ButtonHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonHelp.UseVisualStyleBackColor = false
        '
        'ButtonNpi
        '
        Me.ButtonNpi.BackColor = System.Drawing.Color.Transparent
        Me.ButtonNpi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonNpi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonNpi.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonNpi.Image = CType(resources.GetObject("ButtonNpi.Image"),System.Drawing.Image)
        Me.ButtonNpi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonNpi.Location = New System.Drawing.Point(353, 49)
        Me.ButtonNpi.Name = "ButtonNpi"
        Me.ButtonNpi.Size = New System.Drawing.Size(242, 40)
        Me.ButtonNpi.TabIndex = 120
        Me.ButtonNpi.Text = "NPI"
        Me.ButtonNpi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonNpi.UseVisualStyleBackColor = false
        '
        'ButtonQuote
        '
        Me.ButtonQuote.BackColor = System.Drawing.Color.Transparent
        Me.ButtonQuote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonQuote.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonQuote.Enabled = false
        Me.ButtonQuote.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonQuote.Image = CType(resources.GetObject("ButtonQuote.Image"),System.Drawing.Image)
        Me.ButtonQuote.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonQuote.Location = New System.Drawing.Point(353, 187)
        Me.ButtonQuote.Name = "ButtonQuote"
        Me.ButtonQuote.Size = New System.Drawing.Size(242, 40)
        Me.ButtonQuote.TabIndex = 121
        Me.ButtonQuote.Text = "Quota"
        Me.ButtonQuote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonQuote.UseVisualStyleBackColor = false
        '
        'ButtonCommit
        '
        Me.ButtonCommit.BackColor = System.Drawing.Color.Transparent
        Me.ButtonCommit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonCommit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonCommit.Enabled = false
        Me.ButtonCommit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonCommit.Image = CType(resources.GetObject("ButtonCommit.Image"),System.Drawing.Image)
        Me.ButtonCommit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCommit.Location = New System.Drawing.Point(353, 141)
        Me.ButtonCommit.Name = "ButtonCommit"
        Me.ButtonCommit.Size = New System.Drawing.Size(242, 40)
        Me.ButtonCommit.TabIndex = 122
        Me.ButtonCommit.Text = "Commit"
        Me.ButtonCommit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCommit.UseVisualStyleBackColor = false
        Me.ButtonCommit.Visible = false
        '
        'ButtonEq
        '
        Me.ButtonEq.BackColor = System.Drawing.Color.Transparent
        Me.ButtonEq.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonEq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonEq.Enabled = false
        Me.ButtonEq.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonEq.Image = CType(resources.GetObject("ButtonEq.Image"),System.Drawing.Image)
        Me.ButtonEq.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonEq.Location = New System.Drawing.Point(353, 95)
        Me.ButtonEq.Name = "ButtonEq"
        Me.ButtonEq.Size = New System.Drawing.Size(242, 40)
        Me.ButtonEq.TabIndex = 123
        Me.ButtonEq.Text = "Equipments"
        Me.ButtonEq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonEq.UseVisualStyleBackColor = false
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.BitronPoland
        Me.PictureBox1.Location = New System.Drawing.Point(631, 501)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = false
        '
        'ButtonMould
        '
        Me.ButtonMould.BackColor = System.Drawing.Color.Transparent
        Me.ButtonMould.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonMould.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonMould.Enabled = false
        Me.ButtonMould.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonMould.Image = CType(resources.GetObject("ButtonMould.Image"),System.Drawing.Image)
        Me.ButtonMould.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonMould.Location = New System.Drawing.Point(43, 279)
        Me.ButtonMould.Name = "ButtonMould"
        Me.ButtonMould.Size = New System.Drawing.Size(240, 40)
        Me.ButtonMould.TabIndex = 234
        Me.ButtonMould.Text = "IFP Management"
        Me.ButtonMould.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonMould.UseVisualStyleBackColor = false
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 246!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 248!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 6!))
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonAbout, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonMould, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonECR, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProduct, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonDocManagement, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonLoadDoc, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonTypeEdit, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonQuote, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCommit, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonEq, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonNpi, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonSystem, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonProjectShow, 3, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonTiming, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonRunning, 2, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonHelp, 3, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonBom, 3, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonCrypted, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.ButtonChangePassword, 3, 9)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(27, 25)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(656, 462)
        Me.TableLayoutPanel1.TabIndex = 235
        '
        'ButtonProjectShow
        '
        Me.ButtonProjectShow.BackColor = System.Drawing.Color.Transparent
        Me.ButtonProjectShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonProjectShow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonProjectShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonProjectShow.Image = CType(resources.GetObject("ButtonProjectShow.Image"),System.Drawing.Image)
        Me.ButtonProjectShow.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonProjectShow.Location = New System.Drawing.Point(353, 279)
        Me.ButtonProjectShow.Name = "ButtonProjectShow"
        Me.ButtonProjectShow.Size = New System.Drawing.Size(242, 40)
        Me.ButtonProjectShow.TabIndex = 236
        Me.ButtonProjectShow.Text = "Project Timing Show"
        Me.ButtonProjectShow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonProjectShow.UseVisualStyleBackColor = false
        '
        'ButtonTiming
        '
        Me.ButtonTiming.BackColor = System.Drawing.Color.Transparent
        Me.ButtonTiming.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonTiming.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonTiming.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonTiming.Image = CType(resources.GetObject("ButtonTiming.Image"),System.Drawing.Image)
        Me.ButtonTiming.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonTiming.Location = New System.Drawing.Point(353, 233)
        Me.ButtonTiming.Name = "ButtonTiming"
        Me.ButtonTiming.Size = New System.Drawing.Size(242, 40)
        Me.ButtonTiming.TabIndex = 237
        Me.ButtonTiming.Text = "Project Timing"
        Me.ButtonTiming.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonTiming.UseVisualStyleBackColor = false
        '
        'ButtonRunning
        '
        Me.ButtonRunning.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonRunning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonRunning.Location = New System.Drawing.Point(288, 370)
        Me.ButtonRunning.Margin = New System.Windows.Forms.Padding(2)
        Me.ButtonRunning.Name = "ButtonRunning"
        Me.ButtonRunning.Size = New System.Drawing.Size(60, 42)
        Me.ButtonRunning.TabIndex = 235
        Me.ButtonRunning.UseVisualStyleBackColor = false
        '
        'ButtonBom
        '
        Me.ButtonBom.BackColor = System.Drawing.Color.Transparent
        Me.ButtonBom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonBom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonBom.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonBom.Image = CType(resources.GetObject("ButtonBom.Image"),System.Drawing.Image)
        Me.ButtonBom.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonBom.Location = New System.Drawing.Point(353, 325)
        Me.ButtonBom.Name = "ButtonBom"
        Me.ButtonBom.Size = New System.Drawing.Size(242, 40)
        Me.ButtonBom.TabIndex = 238
        Me.ButtonBom.Text = "Bom Tools"
        Me.ButtonBom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonBom.UseVisualStyleBackColor = false
        '
        'ButtonCrypted
        '
        Me.ButtonCrypted.BackColor = System.Drawing.Color.Transparent
        Me.ButtonCrypted.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonCrypted.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonCrypted.Image = CType(resources.GetObject("ButtonCrypted.Image"),System.Drawing.Image)
        Me.ButtonCrypted.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCrypted.Location = New System.Drawing.Point(43, 417)
        Me.ButtonCrypted.Name = "ButtonCrypted"
        Me.ButtonCrypted.Size = New System.Drawing.Size(240, 39)
        Me.ButtonCrypted.TabIndex = 239
        Me.ButtonCrypted.Text = "Create Crypted Signature"
        Me.ButtonCrypted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCrypted.UseVisualStyleBackColor = false
        '
        'ButtonChangePassword
        '
        Me.ButtonChangePassword.BackColor = System.Drawing.Color.Transparent
        Me.ButtonChangePassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtonChangePassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ButtonChangePassword.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonChangePassword.Location = New System.Drawing.Point(353, 417)
        Me.ButtonChangePassword.Name = "ButtonChangePassword"
        Me.ButtonChangePassword.Size = New System.Drawing.Size(242, 39)
        Me.ButtonChangePassword.TabIndex = 242
        Me.ButtonChangePassword.Text = "Change Password"
        Me.ButtonChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonChangePassword.UseVisualStyleBackColor = false
        '
        'FormStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"),System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(761, 560)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = false
        Me.Name = "FormStart"
        Me.Text = "3D PLM - Product Lifecycle Management"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents ButtonDocManagement As System.Windows.Forms.Button
    Friend WithEvents ButtonProduct As System.Windows.Forms.Button
    Friend WithEvents ButtonECR As System.Windows.Forms.Button
    Friend WithEvents ButtonAbout As System.Windows.Forms.Button
    Friend WithEvents ButtonSystem As System.Windows.Forms.Button
    Friend WithEvents ButtonTypeEdit As System.Windows.Forms.Button
    Friend WithEvents ButtonLoadDoc As System.Windows.Forms.Button
    Friend WithEvents ButtonHelp As System.Windows.Forms.Button
    Friend WithEvents ButtonNpi As System.Windows.Forms.Button
    Friend WithEvents ButtonQuote As System.Windows.Forms.Button
    Friend WithEvents ButtonCommit As System.Windows.Forms.Button
    Friend WithEvents ButtonEq As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonMould As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonRunning As System.Windows.Forms.Button
    Friend WithEvents ButtonProjectShow As System.Windows.Forms.Button
    Friend WithEvents ButtonTiming As System.Windows.Forms.Button
    Friend WithEvents ButtonBom As System.Windows.Forms.Button
    Friend WithEvents ButtonCrypted As System.Windows.Forms.Button
    Friend WithEvents ButtonChangePassword As Button
End Class
