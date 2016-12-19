Imports System.Configuration
Imports System.Linq
Imports MySql.Data.MySqlClient

Public Class FormMaterialRequest

    Dim tblMaterialRequest As DataTable
    Dim startPoint As Point


    Private Sub FormMaterialRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Try
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Using Adapter As New MySqlDataAdapter("SELECT * FROM materialrequest", con)
                    Adapter.Fill(dataSet, "materialrequest")
                    tblMaterialRequest = dataSet.Tables("MaterialRequest")
                End Using
            End Using

            Me.dataGridView.AutoGenerateColumns = True
            If tblMaterialRequest.Rows.Count > 0 Then
                Me.BindingSource.DataMember = tblMaterialRequest.TableName
            End If
            SetColumnsProperties()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    

    Private Sub SetColumnsProperties()
        'Set properties for all columns
        For Each column As DataGridViewColumn In dataGridView.Columns
            column.ReadOnly = True
            column.Visible = False
        Next
        'Set particular properties
        dataGridView.Columns("NoteRnd").ReadOnly = False
        dataGridView.Columns("NotePurchasing").ReadOnly = False
        dataGridView.Columns("NoteRnd").DefaultCellStyle.BackColor  = Color.Beige
        dataGridView.Columns("NotePurchasing").DefaultCellStyle.BackColor  = Color.Beige
        dataGridView.Columns("BitronPN").Visible = True
        dataGridView.Columns("Des_PN").Visible = True
        dataGridView.Columns("Brand").Visible = True
        dataGridView.Columns("BrandALT").Visible = True
        dataGridView.Columns("RequestQT").Visible = True
        dataGridView.Columns("Bomlist").Visible = True
        dataGridView.Columns("NoteRnd").Visible = True
        dataGridView.Columns("NotePurchasing").Visible = True
        dataGridView.Columns("delta").Visible = True
        dataGridView.Columns("pfp").Visible = True
        dataGridView.Columns("doc").Visible = True
        dataGridView.Columns("ProductionUsed").Visible = True
        dataGridView.Columns("Status").Visible = True
        dataGridView.Columns("w_warehouse").Visible = True

    End Sub

    Private Sub dataGridView_SortStringChanged(sender As Object, e As EventArgs) Handles dataGridView.SortStringChanged
        Me.BindingSource.Sort = Me.dataGridView.SortString
    End Sub

    Private Sub dataGridView_FilterStringChanged(sender As Object, e As EventArgs) Handles dataGridView.FilterStringChanged
        Try
            Me.BindingSource.Filter = Me.dataGridView.FilterString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ShowContextMenuStrip()
        Dim showAll As Boolean = False
        ContextMenuStrip.Items.Clear()
        For Each column As DataGridViewColumn In dataGridView.Columns
            Dim item As ToolStripMenuItem = New ToolStripMenuItem(column.Name)
            If (column.Visible) Then
                item.Checked = True
            Else
                item.Checked = False
                showAll = True
            End If
            ContextMenuStrip.Items.Add(item)
        Next
        ContextMenuStrip.Items.Add(New ToolStripSeparator())
        Dim showAllItem As ToolStripMenuItem = New ToolStripMenuItem("Show All")
        showAllItem.Name = "ShowAllColumns"
        If (showAll = False) Then showAllItem.Enabled = False
        ContextMenuStrip.Items.Add(showAllItem)

        ContextMenuStrip.Show(dataGridView, Me.startPoint)
    End Sub


    Private Sub dataGridView_MouseDown(sender As Object, e As MouseEventArgs) Handles dataGridView.MouseDown

        If (e.Button = System.Windows.Forms.MouseButtons.Right) Then
            If (dataGridView.HitTest(e.X, e.Y).Type = DataGridViewHitTestType.ColumnHeader) Then
                Me.startPoint = e.Location
                ShowContextMenuStrip()
            End If
        End If
    End Sub

    Private Sub ContextMenuStrip_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ContextMenuStrip.ItemClicked
        Dim item As ToolStripMenuItem = TryCast(e.ClickedItem, ToolStripMenuItem)
        If item.Checked = True Then
            dataGridView.Columns(e.ClickedItem.Text).Visible = False
        Else
            If e.ClickedItem.Name = "ShowAllColumns" Then
                For Each column As DataGridViewColumn In dataGridView.Columns
                    column.Visible = True
                Next
            Else
                dataGridView.Columns(e.ClickedItem.Text).Visible = True

            End If
        End If
        ShowContextMenuStrip()
    End Sub

    Private Sub ContextMenuStrip_Closing(sender As Object, e As ToolStripDropDownClosingEventArgs) Handles ContextMenuStrip.Closing
        If (e.CloseReason = ToolStripDropDownCloseReason.ItemClicked) Then e.Cancel = True
    End Sub

    Private Sub ButtonClearFilter_Click(sender As Object, e As EventArgs) Handles ButtonClearFilter.Click
        dataGridView.CleanFilter()
    End Sub

    Private Sub ButtonClearSort_Click(sender As Object, e As EventArgs) Handles ButtonClearSort.Click
        dataGridView.CleanSort()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If needSaveList.Count = 0 Then Return
        Try
            Dim builder As New Common.DbConnectionStringBuilder()
            builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
            Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                Dim sqlCommand As String = ""
                For Each row In needSaveList
                    sqlCommand = "Update materialrequest set NoteRnd = '" & row.Cells("NoteRnd").Value.ToString & "' , NotePurchasing = '" & row.Cells("NotePurchasing").Value.ToString & "' where id = " & row.Cells("id").Value
                    Dim cmd = New MySqlCommand(sqlCommand, con)
                    cmd.ExecuteNonQuery()
                Next
                needSaveList.Clear()
                MessageBox.Show("All changes saved.")
             End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Dim needSaveList As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
     
    Private Sub dataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dataGridView.CellEndEdit
        If needSaveList.Contains(dataGridView.Rows(e.RowIndex)) =False Then
            needSaveList.Add(dataGridView.Rows(e.RowIndex))
        End If
        
    End Sub

    Private Sub FormMaterialRequest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If needSaveList.Count > 0 Then 
            Dim result As Integer = MessageBox.Show("Do you want to save pe changes", "caption", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                ButtonSave_Click(sender, e)
            End If
        End If
    End Sub
End Class
