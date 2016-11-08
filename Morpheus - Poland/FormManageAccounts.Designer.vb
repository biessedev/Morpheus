<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormManageAccounts
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
        Me.ButtonSave = New System.Windows.Forms.Button()
        Me.ListViewForUsers = New System.Windows.Forms.ListView()
        Me.TextBoxForUsername = New System.Windows.Forms.TextBox()
        Me.TextBoxForPassword = New System.Windows.Forms.TextBox()
        Me.TextBoxForSign = New System.Windows.Forms.TextBox()
        Me.Username = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonRemove = New System.Windows.Forms.Button()
        Me.ButtonAdd = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'ButtonSave
        '
        Me.ButtonSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ButtonSave.Location = New System.Drawing.Point(380, 352)
        Me.ButtonSave.Name = "ButtonSave"
        Me.ButtonSave.Size = New System.Drawing.Size(87, 23)
        Me.ButtonSave.TabIndex = 1
        Me.ButtonSave.Text = "Update"
        Me.ButtonSave.UseVisualStyleBackColor = true
        '
        'ListViewForUsers
        '
        Me.ListViewForUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ListViewForUsers.BackColor = System.Drawing.Color.Gainsboro
        Me.ListViewForUsers.FullRowSelect = true
        Me.ListViewForUsers.GridLines = true
        Me.ListViewForUsers.HideSelection = false
        Me.ListViewForUsers.Location = New System.Drawing.Point(12, 76)
        Me.ListViewForUsers.MultiSelect = false
        Me.ListViewForUsers.Name = "ListViewForUsers"
        Me.ListViewForUsers.Size = New System.Drawing.Size(536, 270)
        Me.ListViewForUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListViewForUsers.TabIndex = 2
        Me.ListViewForUsers.UseCompatibleStateImageBehavior = false
        Me.ListViewForUsers.View = System.Windows.Forms.View.Details
        '
        'TextBoxForUsername
        '
        Me.TextBoxForUsername.Location = New System.Drawing.Point(12, 37)
        Me.TextBoxForUsername.Name = "TextBoxForUsername"
        Me.TextBoxForUsername.Size = New System.Drawing.Size(139, 20)
        Me.TextBoxForUsername.TabIndex = 3
        '
        'TextBoxForPassword
        '
        Me.TextBoxForPassword.Location = New System.Drawing.Point(157, 37)
        Me.TextBoxForPassword.Name = "TextBoxForPassword"
        Me.TextBoxForPassword.Size = New System.Drawing.Size(137, 20)
        Me.TextBoxForPassword.TabIndex = 4
        '
        'TextBoxForSign
        '
        Me.TextBoxForSign.Location = New System.Drawing.Point(300, 37)
        Me.TextBoxForSign.Name = "TextBoxForSign"
        Me.TextBoxForSign.Size = New System.Drawing.Size(247, 20)
        Me.TextBoxForSign.TabIndex = 5
        '
        'Username
        '
        Me.Username.AutoSize = true
        Me.Username.Location = New System.Drawing.Point(12, 21)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(55, 13)
        Me.Username.TabIndex = 6
        Me.Username.Text = "Username"
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(154, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(297, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Sign"
        '
        'ButtonRemove
        '
        Me.ButtonRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.ButtonRemove.Location = New System.Drawing.Point(473, 352)
        Me.ButtonRemove.Name = "ButtonRemove"
        Me.ButtonRemove.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemove.TabIndex = 9
        Me.ButtonRemove.Text = "Remove"
        Me.ButtonRemove.UseVisualStyleBackColor = true
        '
        'ButtonAdd
        '
        Me.ButtonAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ButtonAdd.Location = New System.Drawing.Point(12, 352)
        Me.ButtonAdd.Name = "ButtonAdd"
        Me.ButtonAdd.Size = New System.Drawing.Size(90, 23)
        Me.ButtonAdd.TabIndex = 10
        Me.ButtonAdd.Text = "Add New User"
        Me.ButtonAdd.UseVisualStyleBackColor = true
        '
        'FormManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 385)
        Me.Controls.Add(Me.ButtonAdd)
        Me.Controls.Add(Me.ButtonRemove)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.TextBoxForSign)
        Me.Controls.Add(Me.TextBoxForPassword)
        Me.Controls.Add(Me.TextBoxForUsername)
        Me.Controls.Add(Me.ListViewForUsers)
        Me.Controls.Add(Me.ButtonSave)
        Me.Name = "FormManageAccounts"
        Me.Text = "Manage Accounts"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

   

     Private Sub listView1_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles ListViewForUsers.ColumnClick
        ' Determine whether the column is the same as the last column clicked.
        If e.Column <> sortColumn Then
            ' Set the sort column to the new column.
            sortColumn = e.Column
            ' Set the sort order to ascending by default.
            ListViewForUsers.Sorting = SortOrder.Ascending
        Else
            ' Determine what the last sort order was and change it.
            If ListViewForUsers.Sorting = SortOrder.Ascending Then
                ListViewForUsers.Sorting = SortOrder.Descending
            Else
                ListViewForUsers.Sorting = SortOrder.Ascending
            End If
        End If 
        ' Call the sort method to manually sort.
        ListViewForUsers.Sort()
        ' Set the ListViewItemSorter property to a new ListViewItemComparer
        ' object.
        ListViewForUsers.ListViewItemSorter = New ListViewItemComparerAscDesc(e.Column, ListViewForUsers.Sorting)
    End Sub
       
    'Visual Basic 
    Dim sortColumn as Integer = -1
    Friend WithEvents ButtonSave As Button
    Friend WithEvents ListViewForUsers As ListView
    Friend WithEvents TextBoxForUsername As TextBox
    Friend WithEvents TextBoxForPassword As TextBox
    Friend WithEvents TextBoxForSign As TextBox
    Friend WithEvents Username As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ButtonRemove As Button
    Friend WithEvents ButtonAdd As Button
End Class


