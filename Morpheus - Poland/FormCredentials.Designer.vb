<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCredentials
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormCredentials))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBoxhost = New System.Windows.Forms.TextBox()
        Me.TextBoxDatabase = New System.Windows.Forms.TextBox()
        Me.TextBoxUserName = New System.Windows.Forms.TextBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(640, 454)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(128, 34)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 232
        Me.PictureBox1.TabStop = False
        '
        'TextBoxhost
        '
        Me.TextBoxhost.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxhost.BackColor = System.Drawing.Color.LightGray
        Me.TextBoxhost.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxhost.Location = New System.Drawing.Point(31, 60)
        Me.TextBoxhost.Name = "TextBoxhost"
        Me.TextBoxhost.Size = New System.Drawing.Size(231, 26)
        Me.TextBoxhost.TabIndex = 235
        '
        'TextBoxDatabase
        '
        Me.TextBoxDatabase.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxDatabase.BackColor = System.Drawing.Color.LightGray
        Me.TextBoxDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDatabase.Location = New System.Drawing.Point(324, 60)
        Me.TextBoxDatabase.Name = "TextBoxDatabase"
        Me.TextBoxDatabase.Size = New System.Drawing.Size(231, 26)
        Me.TextBoxDatabase.TabIndex = 27
        Me.TextBoxDatabase.Text = "srvdoc"
        '
        'TextBoxUserName
        '
        Me.TextBoxUserName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxUserName.BackColor = System.Drawing.Color.LightGray
        Me.TextBoxUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxUserName.Location = New System.Drawing.Point(31, 178)
        Me.TextBoxUserName.Name = "TextBoxUserName"
        Me.TextBoxUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxUserName.Size = New System.Drawing.Size(231, 26)
        Me.TextBoxUserName.TabIndex = 700
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBoxPassword.BackColor = System.Drawing.Color.LightGray
        Me.TextBoxPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TextBoxPassword.Location = New System.Drawing.Point(324, 178)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(231, 26)
        Me.TextBoxPassword.TabIndex = 800
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"),System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button1.Location = New System.Drawing.Point(324, 261)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(231, 29)
        Me.Button1.TabIndex = 25
        Me.Button1.Text = "Submit"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = true
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.LightSlateGray
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxPassword, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxhost, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxDatabase, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PictureBox2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxUserName, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 56)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.00496!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.40695!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.58809!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.02347!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.21127!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(586, 336)
        Me.TableLayoutPanel1.TabIndex = 236
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.84615!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(389, 142)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 24)
        Me.Label4.TabIndex = 240
        Me.Label4.Text = "Password"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.84615!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(391, 29)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 24)
        Me.Label2.TabIndex = 238
        Me.Label2.Text = "Database"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.84615!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(89, 142)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 24)
        Me.Label3.TabIndex = 239
        Me.Label3.Text = "User Name"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.84615!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(120, 29)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 24)
        Me.Label1.TabIndex = 237
        Me.Label1.Text = "Host"
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox2.Image = Global.MORPHEUS.My.Resources.Resources.BitronPoland
        Me.PictureBox2.Location = New System.Drawing.Point(30, 245)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(232, 61)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 236
        Me.PictureBox2.TabStop = false
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.LightBlue
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.85542!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.14458!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(590, 394)
        Me.TableLayoutPanel2.TabIndex = 237
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic),System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(2, 15)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(543, 24)
        Me.Label5.TabIndex = 237
        Me.Label5.Text = "   MORPHEUS 1.0 - Office advanced productivity software"
        '
        'FormCredentials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"),System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(590, 394)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "FormCredentials"
        Me.Text = "Morpheus - Office advanced productivity software"
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.PictureBox2,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel2.ResumeLayout(false)
        Me.TableLayoutPanel2.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxhost As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDatabase As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUserName As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
