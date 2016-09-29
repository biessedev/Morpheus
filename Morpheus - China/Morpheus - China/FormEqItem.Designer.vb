<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEqItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEqItem))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RichTextBoxNote = New System.Windows.Forms.RichTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxActivity = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DateTimePickerED = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DateTimePickerOD = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxDelay = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBoxCost = New System.Windows.Forms.TextBox()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBoxTotalCost = New System.Windows.Forms.TextBox()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.TextBoxAssetID = New System.Windows.Forms.TextBox()
        Me.TextBoxSupplier = New System.Windows.Forms.TextBox()
        Me.TextBoxOrder = New System.Windows.Forms.TextBox()
        Me.TreeViewEQAsset = New System.Windows.Forms.TreeView()
        Me.ButtonClosedDate = New System.Windows.Forms.Button()
        Me.ComboBoxResponsible = New System.Windows.Forms.ComboBox()
        Me.ComboBoxDai = New System.Windows.Forms.ComboBox()
        Me.ButtonAssetImport = New System.Windows.Forms.Button()
        Me.ComboBoxEstimatedClosed = New System.Windows.Forms.ComboBox()
        Me.ComboBoxOpenDate = New System.Windows.Forms.ComboBox()
        Me.GroupBoxItem = New System.Windows.Forms.GroupBox()
        Me.ComboBoxRDA = New System.Windows.Forms.ComboBox()
        Me.ButtonComponentDelLink = New System.Windows.Forms.Button()
        Me.ButtonComponentOpenDs = New System.Windows.Forms.Button()
        Me.TextBoxDS = New System.Windows.Forms.TextBox()
        Me.ButtonComponetAddDs = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBoxItem.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 20)
        Me.Label1.TabIndex = 674
        Me.Label1.Text = "Activity ID"
        '
        'RichTextBoxNote
        '
        Me.RichTextBoxNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxNote.Location = New System.Drawing.Point(16, 20)
        Me.RichTextBoxNote.Name = "RichTextBoxNote"
        Me.RichTextBoxNote.Size = New System.Drawing.Size(743, 198)
        Me.RichTextBoxNote.TabIndex = 719
        Me.RichTextBoxNote.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(124, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 20)
        Me.Label3.TabIndex = 721
        Me.Label3.Text = "Item List"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(248, 234)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 20)
        Me.Label5.TabIndex = 728
        Me.Label5.Text = "Responsible"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(42, 238)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 20)
        Me.Label6.TabIndex = 730
        Me.Label6.Text = "Item Asset ID"
        '
        'TextBoxActivity
        '
        Me.TextBoxActivity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxActivity.Location = New System.Drawing.Point(12, 40)
        Me.TextBoxActivity.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxActivity.Name = "TextBoxActivity"
        Me.TextBoxActivity.Size = New System.Drawing.Size(452, 27)
        Me.TextBoxActivity.TabIndex = 729
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(42, 414)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 20)
        Me.Label7.TabIndex = 732
        Me.Label7.Text = "RDA Number"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(445, 234)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 20)
        Me.Label8.TabIndex = 734
        Me.Label8.Text = "Supplier"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(248, 323)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(132, 20)
        Me.Label10.TabIndex = 737
        Me.Label10.Text = "Estimated Close"
        '
        'DateTimePickerED
        '
        Me.DateTimePickerED.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerED.Location = New System.Drawing.Point(225, 350)
        Me.DateTimePickerED.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerED.Name = "DateTimePickerED"
        Me.DateTimePickerED.Size = New System.Drawing.Size(24, 27)
        Me.DateTimePickerED.TabIndex = 735
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(42, 320)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 20)
        Me.Label11.TabIndex = 740
        Me.Label11.Text = "Open Date"
        '
        'DateTimePickerOD
        '
        Me.DateTimePickerOD.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerOD.Location = New System.Drawing.Point(19, 352)
        Me.DateTimePickerOD.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerOD.Name = "DateTimePickerOD"
        Me.DateTimePickerOD.Size = New System.Drawing.Size(24, 27)
        Me.DateTimePickerOD.TabIndex = 738
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(615, 310)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 20)
        Me.Label12.TabIndex = 743
        Me.Label12.Text = "Closed Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(445, 360)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 20)
        Me.Label4.TabIndex = 745
        Me.Label4.Text = "Delay"
        '
        'TextBoxDelay
        '
        Me.TextBoxDelay.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDelay.Location = New System.Drawing.Point(523, 342)
        Me.TextBoxDelay.Margin = New System.Windows.Forms.Padding(10, 4, 4, 4)
        Me.TextBoxDelay.Name = "TextBoxDelay"
        Me.TextBoxDelay.Size = New System.Drawing.Size(66, 38)
        Me.TextBoxDelay.TabIndex = 744
        Me.TextBoxDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(248, 414)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(116, 20)
        Me.Label13.TabIndex = 747
        Me.Label13.Text = "Order Number"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(445, 414)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(101, 20)
        Me.Label14.TabIndex = 749
        Me.Label14.Text = "DAI Number"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(650, 413)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(109, 20)
        Me.Label15.TabIndex = 751
        Me.Label15.Text = "Cost ( RMB )"
        '
        'TextBoxCost
        '
        Me.TextBoxCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxCost.Location = New System.Drawing.Point(619, 442)
        Me.TextBoxCost.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxCost.Name = "TextBoxCost"
        Me.TextBoxCost.Size = New System.Drawing.Size(140, 38)
        Me.TextBoxCost.TabIndex = 750
        Me.TextBoxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Location = New System.Drawing.Point(388, 83)
        Me.ButtonAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(76, 27)
        Me.ButtonAdd.TabIndex = 752
        Me.ButtonAdd.Text = "Add"
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Location = New System.Drawing.Point(12, 83)
        Me.ButtonRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(76, 27)
        Me.ButtonRemove.TabIndex = 753
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(13, 585)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(210, 29)
        Me.Label17.TabIndex = 757
        Me.Label17.Text = "Total Cost ( RMB )"
        '
        'TextBoxTotalCost
        '
        Me.TextBoxTotalCost.BackColor = System.Drawing.Color.Silver
        Me.TextBoxTotalCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTotalCost.Location = New System.Drawing.Point(263, 582)
        Me.TextBoxTotalCost.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTotalCost.Name = "TextBoxTotalCost"
        Me.TextBoxTotalCost.ReadOnly = True
        Me.TextBoxTotalCost.Size = New System.Drawing.Size(201, 38)
        Me.TextBoxTotalCost.TabIndex = 756
        '
        'ButtonSave
        '
        Me.ButtonSave.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSave.Location = New System.Drawing.Point(619, 519)
        Me.ButtonSave.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(140, 45)
        Me.ButtonSave.TabIndex = 758
        Me.ButtonSave.Text = "SAVE"
        Me.ButtonSave.UseVisualStyleBackColor = False
        '
        'TextBoxAssetID
        '
        Me.TextBoxAssetID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxAssetID.Location = New System.Drawing.Point(46, 262)
        Me.TextBoxAssetID.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxAssetID.Name = "TextBoxAssetID"
        Me.TextBoxAssetID.Size = New System.Drawing.Size(140, 30)
        Me.TextBoxAssetID.TabIndex = 759
        Me.TextBoxAssetID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxSupplier
        '
        Me.TextBoxSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSupplier.Location = New System.Drawing.Point(449, 262)
        Me.TextBoxSupplier.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxSupplier.Name = "TextBoxSupplier"
        Me.TextBoxSupplier.Size = New System.Drawing.Size(310, 30)
        Me.TextBoxSupplier.TabIndex = 760
        '
        'TextBoxOrder
        '
        Me.TextBoxOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxOrder.Location = New System.Drawing.Point(252, 442)
        Me.TextBoxOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxOrder.Name = "TextBoxOrder"
        Me.TextBoxOrder.Size = New System.Drawing.Size(140, 34)
        Me.TextBoxOrder.TabIndex = 763
        Me.TextBoxOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TreeViewEQAsset
        '
        Me.TreeViewEQAsset.BackColor = System.Drawing.SystemColors.Window
        Me.TreeViewEQAsset.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeViewEQAsset.HideSelection = False
        Me.TreeViewEQAsset.Indent = 22
        Me.TreeViewEQAsset.ItemHeight = 22
        Me.TreeViewEQAsset.Location = New System.Drawing.Point(13, 119)
        Me.TreeViewEQAsset.Margin = New System.Windows.Forms.Padding(40, 37, 40, 37)
        Me.TreeViewEQAsset.Name = "TreeViewEQAsset"
        Me.TreeViewEQAsset.Size = New System.Drawing.Size(451, 459)
        Me.TreeViewEQAsset.TabIndex = 765
        '
        'ButtonClosedDate
        '
        Me.ButtonClosedDate.BackColor = System.Drawing.Color.Tomato
        Me.ButtonClosedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonClosedDate.Location = New System.Drawing.Point(619, 336)
        Me.ButtonClosedDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonClosedDate.Name = "ButtonClosedDate"
        Me.ButtonClosedDate.Size = New System.Drawing.Size(140, 44)
        Me.ButtonClosedDate.TabIndex = 766
        Me.ButtonClosedDate.Text = "OPEN"
        Me.ButtonClosedDate.UseVisualStyleBackColor = False
        '
        'ComboBoxResponsible
        '
        Me.ComboBoxResponsible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxResponsible.FormattingEnabled = True
        Me.ComboBoxResponsible.Location = New System.Drawing.Point(252, 261)
        Me.ComboBoxResponsible.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxResponsible.Name = "ComboBoxResponsible"
        Me.ComboBoxResponsible.Size = New System.Drawing.Size(140, 33)
        Me.ComboBoxResponsible.TabIndex = 767
        '
        'ComboBoxDai
        '
        Me.ComboBoxDai.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxDai.FormattingEnabled = True
        Me.ComboBoxDai.Location = New System.Drawing.Point(449, 442)
        Me.ComboBoxDai.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxDai.Name = "ComboBoxDai"
        Me.ComboBoxDai.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ComboBoxDai.Size = New System.Drawing.Size(140, 37)
        Me.ComboBoxDai.TabIndex = 768
        '
        'ButtonAssetImport
        '
        Me.ButtonAssetImport.Image = CType(resources.GetObject("ButtonAssetImport.Image"), System.Drawing.Image)
        Me.ButtonAssetImport.Location = New System.Drawing.Point(19, 263)
        Me.ButtonAssetImport.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonAssetImport.Name = "ButtonAssetImport"
        Me.ButtonAssetImport.Size = New System.Drawing.Size(24, 27)
        Me.ButtonAssetImport.TabIndex = 769
        Me.ButtonAssetImport.UseVisualStyleBackColor = True
        '
        'ComboBoxEstimatedClosed
        '
        Me.ComboBoxEstimatedClosed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEstimatedClosed.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxEstimatedClosed.FormattingEnabled = True
        Me.ComboBoxEstimatedClosed.Location = New System.Drawing.Point(252, 350)
        Me.ComboBoxEstimatedClosed.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxEstimatedClosed.Name = "ComboBoxEstimatedClosed"
        Me.ComboBoxEstimatedClosed.Size = New System.Drawing.Size(140, 28)
        Me.ComboBoxEstimatedClosed.TabIndex = 736
        '
        'ComboBoxOpenDate
        '
        Me.ComboBoxOpenDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxOpenDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxOpenDate.FormattingEnabled = True
        Me.ComboBoxOpenDate.Location = New System.Drawing.Point(46, 352)
        Me.ComboBoxOpenDate.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxOpenDate.Name = "ComboBoxOpenDate"
        Me.ComboBoxOpenDate.Size = New System.Drawing.Size(140, 28)
        Me.ComboBoxOpenDate.TabIndex = 739
        '
        'GroupBoxItem
        '
        Me.GroupBoxItem.Controls.Add(Me.ComboBoxRDA)
        Me.GroupBoxItem.Controls.Add(Me.ButtonComponentDelLink)
        Me.GroupBoxItem.Controls.Add(Me.ButtonComponentOpenDs)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxDS)
        Me.GroupBoxItem.Controls.Add(Me.ButtonComponetAddDs)
        Me.GroupBoxItem.Controls.Add(Me.ButtonAssetImport)
        Me.GroupBoxItem.Controls.Add(Me.RichTextBoxNote)
        Me.GroupBoxItem.Controls.Add(Me.ComboBoxDai)
        Me.GroupBoxItem.Controls.Add(Me.Label5)
        Me.GroupBoxItem.Controls.Add(Me.ComboBoxResponsible)
        Me.GroupBoxItem.Controls.Add(Me.Label6)
        Me.GroupBoxItem.Controls.Add(Me.ButtonClosedDate)
        Me.GroupBoxItem.Controls.Add(Me.Label7)
        Me.GroupBoxItem.Controls.Add(Me.Label8)
        Me.GroupBoxItem.Controls.Add(Me.DateTimePickerED)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxOrder)
        Me.GroupBoxItem.Controls.Add(Me.ComboBoxEstimatedClosed)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxSupplier)
        Me.GroupBoxItem.Controls.Add(Me.Label10)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxAssetID)
        Me.GroupBoxItem.Controls.Add(Me.DateTimePickerOD)
        Me.GroupBoxItem.Controls.Add(Me.ButtonSave)
        Me.GroupBoxItem.Controls.Add(Me.ComboBoxOpenDate)
        Me.GroupBoxItem.Controls.Add(Me.Label11)
        Me.GroupBoxItem.Controls.Add(Me.Label12)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxDelay)
        Me.GroupBoxItem.Controls.Add(Me.Label4)
        Me.GroupBoxItem.Controls.Add(Me.Label15)
        Me.GroupBoxItem.Controls.Add(Me.Label13)
        Me.GroupBoxItem.Controls.Add(Me.TextBoxCost)
        Me.GroupBoxItem.Controls.Add(Me.Label14)
        Me.GroupBoxItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxItem.Location = New System.Drawing.Point(471, 12)
        Me.GroupBoxItem.Name = "GroupBoxItem"
        Me.GroupBoxItem.Size = New System.Drawing.Size(774, 608)
        Me.GroupBoxItem.TabIndex = 770
        Me.GroupBoxItem.TabStop = False
        Me.GroupBoxItem.Text = " Item Description"
        '
        'ComboBoxRDA
        '
        Me.ComboBoxRDA.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxRDA.FormattingEnabled = True
        Me.ComboBoxRDA.Location = New System.Drawing.Point(22, 438)
        Me.ComboBoxRDA.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxRDA.Name = "ComboBoxRDA"
        Me.ComboBoxRDA.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ComboBoxRDA.Size = New System.Drawing.Size(164, 33)
        Me.ComboBoxRDA.TabIndex = 774
        '
        'ButtonComponentDelLink
        '
        Me.ButtonComponentDelLink.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonComponentDelLink.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonComponentDelLink.Location = New System.Drawing.Point(46, 531)
        Me.ButtonComponentDelLink.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonComponentDelLink.Name = "ButtonComponentDelLink"
        Me.ButtonComponentDelLink.Size = New System.Drawing.Size(71, 27)
        Me.ButtonComponentDelLink.TabIndex = 773
        Me.ButtonComponentDelLink.Text = "DEL DS"
        Me.ButtonComponentDelLink.UseVisualStyleBackColor = True
        '
        'ButtonComponentOpenDs
        '
        Me.ButtonComponentOpenDs.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonComponentOpenDs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonComponentOpenDs.Location = New System.Drawing.Point(506, 530)
        Me.ButtonComponentOpenDs.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonComponentOpenDs.Name = "ButtonComponentOpenDs"
        Me.ButtonComponentOpenDs.Size = New System.Drawing.Size(83, 28)
        Me.ButtonComponentOpenDs.TabIndex = 770
        Me.ButtonComponentOpenDs.Text = "OPEN DS"
        Me.ButtonComponentOpenDs.UseVisualStyleBackColor = True
        '
        'TextBoxDS
        '
        Me.TextBoxDS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxDS.Location = New System.Drawing.Point(125, 530)
        Me.TextBoxDS.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxDS.Name = "TextBoxDS"
        Me.TextBoxDS.Size = New System.Drawing.Size(267, 27)
        Me.TextBoxDS.TabIndex = 771
        '
        'ButtonComponetAddDs
        '
        Me.ButtonComponetAddDs.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonComponetAddDs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ButtonComponetAddDs.Location = New System.Drawing.Point(409, 531)
        Me.ButtonComponetAddDs.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonComponetAddDs.Name = "ButtonComponetAddDs"
        Me.ButtonComponetAddDs.Size = New System.Drawing.Size(73, 27)
        Me.ButtonComponetAddDs.TabIndex = 772
        Me.ButtonComponetAddDs.Text = "ADD DS"
        Me.ButtonComponetAddDs.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(294, 83)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(76, 27)
        Me.Button1.TabIndex = 771
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormEqItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 638)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBoxItem)
        Me.Controls.Add(Me.TreeViewEQAsset)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TextBoxTotalCost)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.TextBoxActivity)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormEqItem"
        Me.Text = "Morpheus  -  Equipment  --> Tools  -->  Item"
        Me.GroupBoxItem.ResumeLayout(False)
        Me.GroupBoxItem.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RichTextBoxNote As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBoxActivity As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerED As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerOD As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxDelay As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCost As System.Windows.Forms.TextBox
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalCost As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents TextBoxAssetID As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSupplier As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxOrder As System.Windows.Forms.TextBox
    Friend WithEvents TreeViewEQAsset As System.Windows.Forms.TreeView
    Friend WithEvents ButtonClosedDate As System.Windows.Forms.Button
    Protected WithEvents ComboBoxResponsible As System.Windows.Forms.ComboBox
    Protected WithEvents ComboBoxDai As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonAssetImport As System.Windows.Forms.Button
    Protected WithEvents ComboBoxEstimatedClosed As System.Windows.Forms.ComboBox
    Protected WithEvents ComboBoxOpenDate As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBoxItem As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonComponentDelLink As System.Windows.Forms.Button
    Friend WithEvents ButtonComponentOpenDs As System.Windows.Forms.Button
    Friend WithEvents TextBoxDS As System.Windows.Forms.TextBox
    Friend WithEvents ButtonComponetAddDs As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Protected WithEvents ComboBoxRDA As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
