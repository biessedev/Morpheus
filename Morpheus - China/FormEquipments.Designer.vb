<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEquipments
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEquipments))
        Me.Label17 = New System.Windows.Forms.Label()
        Me.ComboBoxmpa = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBoxWorkHours = New System.Windows.Forms.TextBox()
        Me.RichTextBoxNote = New System.Windows.Forms.RichTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxToolId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxCustomer = New System.Windows.Forms.ComboBox()
        Me.GroupBoxDate = New System.Windows.Forms.GroupBox()
        Me.ComboBoxHWDoc = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerHWDoc = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerHWBuilding = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxHWBuilding = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerHWDebug = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxHWDebug = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerSWDebug = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxSWDebug = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBoxStart = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerStart = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBoxEnd = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.DateTimePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.LabelCostHour = New System.Windows.Forms.Label()
        Me.Label1Range = New System.Windows.Forms.Label()
        Me.ComboBoxRange = New System.Windows.Forms.ComboBox()
        Me.ButtonEqGr = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonCollapsExpand = New System.Windows.Forms.Button()
        Me.ComboBoxSop = New System.Windows.Forms.ComboBox()
        Me.CheckBoxOpen = New System.Windows.Forms.CheckBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.TextBoxTime = New System.Windows.Forms.TextBox()
        Me.ComboBoxStatus = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.ComboBoxActivityId = New System.Windows.Forms.ComboBox()
        Me.DateTimePickerSop = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxToolsType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxToolName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TreeViewEQ = New System.Windows.Forms.TreeView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ButtonLoadTools = New System.Windows.Forms.Button()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ButtonAddActivity = New System.Windows.Forms.Button()
        Me.TimerRecord = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBoxDate.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1177, 303)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(37, 20)
        Me.Label17.TabIndex = 758
        Me.Label17.Text = "DAI"
        '
        'ComboBoxmpa
        '
        Me.ComboBoxmpa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxmpa.FormattingEnabled = True
        Me.ComboBoxmpa.Location = New System.Drawing.Point(1179, 327)
        Me.ComboBoxmpa.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxmpa.Name = "ComboBoxmpa"
        Me.ComboBoxmpa.Size = New System.Drawing.Size(147, 28)
        Me.ComboBoxmpa.TabIndex = 757
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(880, 397)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(113, 20)
        Me.Label16.TabIndex = 756
        Me.Label16.Text = "Hours worked"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(877, 464)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(95, 20)
        Me.Label14.TabIndex = 755
        Me.Label14.Text = "Description"
        '
        'TextBoxWorkHours
        '
        Me.TextBoxWorkHours.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxWorkHours.Location = New System.Drawing.Point(1031, 387)
        Me.TextBoxWorkHours.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxWorkHours.Name = "TextBoxWorkHours"
        Me.TextBoxWorkHours.Size = New System.Drawing.Size(119, 30)
        Me.TextBoxWorkHours.TabIndex = 754
        Me.TextBoxWorkHours.Text = "30"
        '
        'RichTextBoxNote
        '
        Me.RichTextBoxNote.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBoxNote.Location = New System.Drawing.Point(879, 487)
        Me.RichTextBoxNote.Name = "RichTextBoxNote"
        Me.RichTextBoxNote.Size = New System.Drawing.Size(452, 188)
        Me.RichTextBoxNote.TabIndex = 753
        Me.RichTextBoxNote.Text = ""
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1127, 237)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 20)
        Me.Label9.TabIndex = 751
        Me.Label9.Text = "Asset ID"
        '
        'TextBoxToolId
        '
        Me.TextBoxToolId.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxToolId.Location = New System.Drawing.Point(1131, 261)
        Me.TextBoxToolId.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxToolId.Name = "TextBoxToolId"
        Me.TextBoxToolId.Size = New System.Drawing.Size(200, 27)
        Me.TextBoxToolId.TabIndex = 750
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(875, 241)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 20)
        Me.Label6.TabIndex = 749
        Me.Label6.Text = "Customer"
        '
        'ComboBoxCustomer
        '
        Me.ComboBoxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxCustomer.FormattingEnabled = True
        Me.ComboBoxCustomer.Location = New System.Drawing.Point(879, 264)
        Me.ComboBoxCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxCustomer.Name = "ComboBoxCustomer"
        Me.ComboBoxCustomer.Size = New System.Drawing.Size(228, 28)
        Me.ComboBoxCustomer.TabIndex = 748
        '
        'GroupBoxDate
        '
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxHWDoc)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerHWDoc)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerHWBuilding)
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxHWBuilding)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerHWDebug)
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxHWDebug)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerSWDebug)
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxSWDebug)
        Me.GroupBoxDate.Controls.Add(Me.Label12)
        Me.GroupBoxDate.Controls.Add(Me.Label5)
        Me.GroupBoxDate.Controls.Add(Me.Label11)
        Me.GroupBoxDate.Controls.Add(Me.Label7)
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxStart)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerStart)
        Me.GroupBoxDate.Controls.Add(Me.Label8)
        Me.GroupBoxDate.Controls.Add(Me.ComboBoxEnd)
        Me.GroupBoxDate.Controls.Add(Me.Label10)
        Me.GroupBoxDate.Controls.Add(Me.Label13)
        Me.GroupBoxDate.Controls.Add(Me.DateTimePickerEnd)
        Me.GroupBoxDate.Location = New System.Drawing.Point(41, 574)
        Me.GroupBoxDate.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxDate.Name = "GroupBoxDate"
        Me.GroupBoxDate.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxDate.Size = New System.Drawing.Size(816, 149)
        Me.GroupBoxDate.TabIndex = 747
        Me.GroupBoxDate.TabStop = False
        '
        'ComboBoxHWDoc
        '
        Me.ComboBoxHWDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHWDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxHWDoc.FormattingEnabled = True
        Me.ComboBoxHWDoc.Location = New System.Drawing.Point(36, 105)
        Me.ComboBoxHWDoc.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxHWDoc.Name = "ComboBoxHWDoc"
        Me.ComboBoxHWDoc.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxHWDoc.TabIndex = 652
        '
        'DateTimePickerHWDoc
        '
        Me.DateTimePickerHWDoc.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerHWDoc.Location = New System.Drawing.Point(11, 103)
        Me.DateTimePickerHWDoc.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerHWDoc.Name = "DateTimePickerHWDoc"
        Me.DateTimePickerHWDoc.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerHWDoc.TabIndex = 651
        '
        'DateTimePickerHWBuilding
        '
        Me.DateTimePickerHWBuilding.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerHWBuilding.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerHWBuilding.Location = New System.Drawing.Point(209, 103)
        Me.DateTimePickerHWBuilding.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerHWBuilding.Name = "DateTimePickerHWBuilding"
        Me.DateTimePickerHWBuilding.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerHWBuilding.TabIndex = 658
        '
        'ComboBoxHWBuilding
        '
        Me.ComboBoxHWBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHWBuilding.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxHWBuilding.FormattingEnabled = True
        Me.ComboBoxHWBuilding.Location = New System.Drawing.Point(236, 103)
        Me.ComboBoxHWBuilding.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxHWBuilding.Name = "ComboBoxHWBuilding"
        Me.ComboBoxHWBuilding.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxHWBuilding.TabIndex = 659
        '
        'DateTimePickerHWDebug
        '
        Me.DateTimePickerHWDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerHWDebug.Location = New System.Drawing.Point(419, 103)
        Me.DateTimePickerHWDebug.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerHWDebug.Name = "DateTimePickerHWDebug"
        Me.DateTimePickerHWDebug.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerHWDebug.TabIndex = 660
        '
        'ComboBoxHWDebug
        '
        Me.ComboBoxHWDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxHWDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxHWDebug.FormattingEnabled = True
        Me.ComboBoxHWDebug.Location = New System.Drawing.Point(445, 103)
        Me.ComboBoxHWDebug.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxHWDebug.Name = "ComboBoxHWDebug"
        Me.ComboBoxHWDebug.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxHWDebug.TabIndex = 661
        '
        'DateTimePickerSWDebug
        '
        Me.DateTimePickerSWDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerSWDebug.Location = New System.Drawing.Point(624, 103)
        Me.DateTimePickerSWDebug.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerSWDebug.Name = "DateTimePickerSWDebug"
        Me.DateTimePickerSWDebug.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerSWDebug.TabIndex = 662
        '
        'ComboBoxSWDebug
        '
        Me.ComboBoxSWDebug.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSWDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSWDebug.FormattingEnabled = True
        Me.ComboBoxSWDebug.Location = New System.Drawing.Point(651, 103)
        Me.ComboBoxSWDebug.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxSWDebug.Name = "ComboBoxSWDebug"
        Me.ComboBoxSWDebug.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxSWDebug.TabIndex = 663
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(620, 80)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 20)
        Me.Label12.TabIndex = 618
        Me.Label12.Text = "SW Debug"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 81)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 20)
        Me.Label5.TabIndex = 596
        Me.Label5.Text = "HW Doc"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(205, 80)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 20)
        Me.Label11.TabIndex = 608
        Me.Label11.Text = "HW Building"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(415, 80)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 20)
        Me.Label7.TabIndex = 617
        Me.Label7.Text = "HW Debug"
        '
        'ComboBoxStart
        '
        Me.ComboBoxStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxStart.FormattingEnabled = True
        Me.ComboBoxStart.Items.AddRange(New Object() {"3333/33/33"})
        Me.ComboBoxStart.Location = New System.Drawing.Point(36, 38)
        Me.ComboBoxStart.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxStart.Name = "ComboBoxStart"
        Me.ComboBoxStart.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxStart.TabIndex = 656
        '
        'DateTimePickerStart
        '
        Me.DateTimePickerStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerStart.Location = New System.Drawing.Point(11, 38)
        Me.DateTimePickerStart.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerStart.Name = "DateTimePickerStart"
        Me.DateTimePickerStart.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerStart.TabIndex = 655
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 15)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 20)
        Me.Label8.TabIndex = 657
        Me.Label8.Text = "Start"
        '
        'ComboBoxEnd
        '
        Me.ComboBoxEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxEnd.FormattingEnabled = True
        Me.ComboBoxEnd.Location = New System.Drawing.Point(651, 43)
        Me.ComboBoxEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxEnd.Name = "ComboBoxEnd"
        Me.ComboBoxEnd.Size = New System.Drawing.Size(140, 26)
        Me.ComboBoxEnd.TabIndex = 665
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(617, 20)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 20)
        Me.Label10.TabIndex = 611
        Me.Label10.Text = "Validation"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(749, 20)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 20)
        Me.Label13.TabIndex = 619
        Me.Label13.Text = "End"
        '
        'DateTimePickerEnd
        '
        Me.DateTimePickerEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerEnd.Location = New System.Drawing.Point(624, 43)
        Me.DateTimePickerEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerEnd.Name = "DateTimePickerEnd"
        Me.DateTimePickerEnd.Size = New System.Drawing.Size(24, 29)
        Me.DateTimePickerEnd.TabIndex = 664
        '
        'LabelCostHour
        '
        Me.LabelCostHour.AutoSize = True
        Me.LabelCostHour.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCostHour.Location = New System.Drawing.Point(875, 689)
        Me.LabelCostHour.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LabelCostHour.Name = "LabelCostHour"
        Me.LabelCostHour.Size = New System.Drawing.Size(197, 20)
        Me.LabelCostHour.TabIndex = 746
        Me.LabelCostHour.Text = "Total Activity Cost / Hour"
        '
        'Label1Range
        '
        Me.Label1Range.AutoSize = True
        Me.Label1Range.BackColor = System.Drawing.Color.Transparent
        Me.Label1Range.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1Range.Location = New System.Drawing.Point(658, 590)
        Me.Label1Range.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1Range.Name = "Label1Range"
        Me.Label1Range.Size = New System.Drawing.Size(69, 25)
        Me.Label1Range.TabIndex = 745
        Me.Label1Range.Text = "Range"
        Me.Label1Range.Visible = False
        '
        'ComboBoxRange
        '
        Me.ComboBoxRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxRange.FormatString = "N0"
        Me.ComboBoxRange.FormattingEnabled = True
        Me.ComboBoxRange.Items.AddRange(New Object() {"15", "30", "45", "60", "75", "90", "120"})
        Me.ComboBoxRange.Location = New System.Drawing.Point(734, 586)
        Me.ComboBoxRange.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxRange.Name = "ComboBoxRange"
        Me.ComboBoxRange.Size = New System.Drawing.Size(123, 26)
        Me.ComboBoxRange.TabIndex = 744
        Me.ComboBoxRange.Visible = False
        '
        'ButtonEqGr
        '
        Me.ButtonEqGr.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonEqGr.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonEqGr.Location = New System.Drawing.Point(38, 82)
        Me.ButtonEqGr.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonEqGr.Name = "ButtonEqGr"
        Me.ButtonEqGr.Size = New System.Drawing.Size(201, 28)
        Me.ButtonEqGr.TabIndex = 743
        Me.ButtonEqGr.Text = "Equipments List"
        Me.ButtonEqGr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonEqGr.UseVisualStyleBackColor = True
        Me.ButtonEqGr.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(730, 82)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(76, 27)
        Me.Button1.TabIndex = 742
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonCollapsExpand
        '
        Me.ButtonCollapsExpand.Location = New System.Drawing.Point(814, 82)
        Me.ButtonCollapsExpand.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonCollapsExpand.Name = "ButtonCollapsExpand"
        Me.ButtonCollapsExpand.Size = New System.Drawing.Size(40, 27)
        Me.ButtonCollapsExpand.TabIndex = 741
        Me.ButtonCollapsExpand.Text = "E"
        Me.ButtonCollapsExpand.UseVisualStyleBackColor = True
        '
        'ComboBoxSop
        '
        Me.ComboBoxSop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSop.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSop.FormattingEnabled = True
        Me.ComboBoxSop.Location = New System.Drawing.Point(1031, 325)
        Me.ComboBoxSop.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxSop.Name = "ComboBoxSop"
        Me.ComboBoxSop.Size = New System.Drawing.Size(119, 28)
        Me.ComboBoxSop.TabIndex = 739
        '
        'CheckBoxOpen
        '
        Me.CheckBoxOpen.AutoSize = True
        Me.CheckBoxOpen.Checked = True
        Me.CheckBoxOpen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxOpen.Location = New System.Drawing.Point(879, 84)
        Me.CheckBoxOpen.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxOpen.Name = "CheckBoxOpen"
        Me.CheckBoxOpen.Size = New System.Drawing.Size(102, 21)
        Me.CheckBoxOpen.TabIndex = 735
        Me.CheckBoxOpen.Text = "OPEN Only"
        Me.CheckBoxOpen.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(1215, 79)
        Me.Label44.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(111, 20)
        Me.Label44.TabIndex = 734
        Me.Label44.Text = "Time Session"
        '
        'TextBoxTime
        '
        Me.TextBoxTime.Enabled = False
        Me.TextBoxTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxTime.Location = New System.Drawing.Point(1219, 103)
        Me.TextBoxTime.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxTime.Name = "TextBoxTime"
        Me.TextBoxTime.Size = New System.Drawing.Size(112, 24)
        Me.TextBoxTime.TabIndex = 733
        Me.TextBoxTime.Text = "30"
        '
        'ComboBoxStatus
        '
        Me.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxStatus.FormattingEnabled = True
        Me.ComboBoxStatus.Location = New System.Drawing.Point(537, 82)
        Me.ComboBoxStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxStatus.Name = "ComboBoxStatus"
        Me.ComboBoxStatus.Size = New System.Drawing.Size(184, 26)
        Me.ComboBoxStatus.TabIndex = 732
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(533, 59)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(103, 20)
        Me.Label18.TabIndex = 731
        Me.Label18.Text = "Status Tools"
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonAdd.Location = New System.Drawing.Point(40, 530)
        Me.ButtonAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(161, 36)
        Me.ButtonAdd.TabIndex = 730
        Me.ButtonAdd.Text = "Add Tool"
        Me.ButtonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAdd.UseVisualStyleBackColor = True
        '
        'ComboBoxActivityId
        '
        Me.ComboBoxActivityId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxActivityId.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxActivityId.FormattingEnabled = True
        Me.ComboBoxActivityId.Location = New System.Drawing.Point(879, 193)
        Me.ComboBoxActivityId.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxActivityId.Name = "ComboBoxActivityId"
        Me.ComboBoxActivityId.Size = New System.Drawing.Size(452, 28)
        Me.ComboBoxActivityId.TabIndex = 729
        '
        'DateTimePickerSop
        '
        Me.DateTimePickerSop.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePickerSop.Location = New System.Drawing.Point(1003, 325)
        Me.DateTimePickerSop.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePickerSop.Name = "DateTimePickerSop"
        Me.DateTimePickerSop.Size = New System.Drawing.Size(24, 27)
        Me.DateTimePickerSop.TabIndex = 728
        '
        'ComboBoxToolsType
        '
        Me.ComboBoxToolsType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxToolsType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxToolsType.FormattingEnabled = True
        Me.ComboBoxToolsType.Location = New System.Drawing.Point(252, 82)
        Me.ComboBoxToolsType.Margin = New System.Windows.Forms.Padding(4)
        Me.ComboBoxToolsType.Name = "ComboBoxToolsType"
        Me.ComboBoxToolsType.Size = New System.Drawing.Size(256, 26)
        Me.ComboBoxToolsType.TabIndex = 727
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(999, 301)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 20)
        Me.Label4.TabIndex = 726
        Me.Label4.Text = "SOP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(248, 59)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 20)
        Me.Label3.TabIndex = 725
        Me.Label3.Text = "Tools Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(875, 112)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 20)
        Me.Label2.TabIndex = 724
        Me.Label2.Text = "Tool name"
        '
        'TextBoxToolName
        '
        Me.TextBoxToolName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxToolName.Location = New System.Drawing.Point(879, 135)
        Me.TextBoxToolName.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBoxToolName.Name = "TextBoxToolName"
        Me.TextBoxToolName.Size = New System.Drawing.Size(452, 27)
        Me.TextBoxToolName.TabIndex = 723
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(875, 171)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 20)
        Me.Label1.TabIndex = 722
        Me.Label1.Text = "Activity ID"
        '
        'TreeViewEQ
        '
        Me.TreeViewEQ.BackColor = System.Drawing.SystemColors.Window
        Me.TreeViewEQ.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeViewEQ.Indent = 22
        Me.TreeViewEQ.ItemHeight = 22
        Me.TreeViewEQ.Location = New System.Drawing.Point(38, 133)
        Me.TreeViewEQ.Margin = New System.Windows.Forms.Padding(40, 37, 40, 37)
        Me.TreeViewEQ.Name = "TreeViewEQ"
        Me.TreeViewEQ.Size = New System.Drawing.Size(816, 384)
        Me.TreeViewEQ.TabIndex = 721
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.MORPHEUS.My.Resources.Resources.Bitron_BEC
        Me.PictureBox1.Location = New System.Drawing.Point(1196, 685)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(135, 39)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 752
        Me.PictureBox1.TabStop = False
        '
        'ButtonLoadTools
        '
        Me.ButtonLoadTools.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonLoadTools.Image = CType(resources.GetObject("ButtonLoadTools.Image"), System.Drawing.Image)
        Me.ButtonLoadTools.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonLoadTools.Location = New System.Drawing.Point(476, 530)
        Me.ButtonLoadTools.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonLoadTools.Name = "ButtonLoadTools"
        Me.ButtonLoadTools.Size = New System.Drawing.Size(168, 36)
        Me.ButtonLoadTools.TabIndex = 740
        Me.ButtonLoadTools.Text = "Load Tools"
        Me.ButtonLoadTools.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonLoadTools.UseVisualStyleBackColor = True
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonRemove.Image = CType(resources.GetObject("ButtonRemove.Image"), System.Drawing.Image)
        Me.ButtonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonRemove.Location = New System.Drawing.Point(255, 530)
        Me.ButtonRemove.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(168, 36)
        Me.ButtonRemove.TabIndex = 738
        Me.ButtonRemove.Text = "Remove Tool"
        Me.ButtonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonRemove.UseVisualStyleBackColor = True
        '
        'ButtonSave
        '
        Me.ButtonSave.BackColor = System.Drawing.Color.LimeGreen
        Me.ButtonSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonSave.Image = CType(resources.GetObject("ButtonSave.Image"), System.Drawing.Image)
        Me.ButtonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonSave.Location = New System.Drawing.Point(696, 530)
        Me.ButtonSave.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(161, 36)
        Me.ButtonSave.TabIndex = 737
        Me.ButtonSave.Text = "Save"
        Me.ButtonSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSave.UseVisualStyleBackColor = False
        '
        'ButtonAddActivity
        '
        Me.ButtonAddActivity.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonAddActivity.Image = CType(resources.GetObject("ButtonAddActivity.Image"), System.Drawing.Image)
        Me.ButtonAddActivity.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.ButtonAddActivity.Location = New System.Drawing.Point(879, 322)
        Me.ButtonAddActivity.Margin = New System.Windows.Forms.Padding(4)
        Me.ButtonAddActivity.Name = "ButtonAddActivity"
        Me.ButtonAddActivity.Size = New System.Drawing.Size(93, 32)
        Me.ButtonAddActivity.TabIndex = 736
        Me.ButtonAddActivity.Text = "Add Act"
        Me.ButtonAddActivity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonAddActivity.UseVisualStyleBackColor = True
        '
        'TimerRecord
        '
        '
        'FormEquipments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1369, 782)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.ComboBoxmpa)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.TextBoxWorkHours)
        Me.Controls.Add(Me.RichTextBoxNote)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxToolId)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBoxCustomer)
        Me.Controls.Add(Me.GroupBoxDate)
        Me.Controls.Add(Me.LabelCostHour)
        Me.Controls.Add(Me.Label1Range)
        Me.Controls.Add(Me.ComboBoxRange)
        Me.Controls.Add(Me.ButtonEqGr)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ButtonCollapsExpand)
        Me.Controls.Add(Me.ButtonLoadTools)
        Me.Controls.Add(Me.ComboBoxSop)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.ButtonSave)
        Me.Controls.Add(Me.ButtonAddActivity)
        Me.Controls.Add(Me.CheckBoxOpen)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.TextBoxTime)
        Me.Controls.Add(Me.ComboBoxStatus)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ComboBoxActivityId)
        Me.Controls.Add(Me.DateTimePickerSop)
        Me.Controls.Add(Me.ComboBoxToolsType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxToolName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TreeViewEQ)
        Me.Name = "FormEquipments"
        Me.Text = "FormEquipments"
        Me.GroupBoxDate.ResumeLayout(False)
        Me.GroupBoxDate.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxmpa As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBoxWorkHours As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBoxNote As System.Windows.Forms.RichTextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBoxToolId As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBoxDate As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBoxHWDoc As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerHWDoc As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerHWBuilding As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxHWBuilding As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerHWDebug As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxHWDebug As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerSWDebug As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxSWDebug As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents ComboBoxStart As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents ComboBoxEnd As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents LabelCostHour As System.Windows.Forms.Label
    Friend WithEvents Label1Range As System.Windows.Forms.Label
    Friend WithEvents ComboBoxRange As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonEqGr As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ButtonCollapsExpand As System.Windows.Forms.Button
    Friend WithEvents ButtonLoadTools As System.Windows.Forms.Button
    Protected WithEvents ComboBoxSop As System.Windows.Forms.ComboBox
    Friend WithEvents ButtonRemove As System.Windows.Forms.Button
    Friend WithEvents ButtonSave As System.Windows.Forms.Button
    Friend WithEvents ButtonAddActivity As System.Windows.Forms.Button
    Friend WithEvents CheckBoxOpen As System.Windows.Forms.CheckBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTime As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ButtonAdd As System.Windows.Forms.Button
    Protected WithEvents ComboBoxActivityId As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePickerSop As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxToolsType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxToolName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TreeViewEQ As System.Windows.Forms.TreeView
    Friend WithEvents TimerRecord As System.Windows.Forms.Timer
End Class
