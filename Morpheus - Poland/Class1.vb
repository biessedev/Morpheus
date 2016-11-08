Public Class ListViewItemComparer
    Implements IComparer
    Private col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                            Implements IComparer.Compare
        Dim returnVal As Integer = -1
        returnVal = [String].Compare(CType(x,  _
                        ListViewItem).SubItems(col).Text, _
                        CType(y, ListViewItem).SubItems(col).Text)
        Return returnVal
    End Function


End Class


Class ListViewItemComparerAscDesc
    Implements IComparer 
    Private col As Integer
    Private order as SortOrder
    
    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub
    
    Public Sub New(column As Integer, order as SortOrder)
        col = column
        Me.order = order
    End Sub
    
    Public Function Compare(x As Object, y As Object) As Integer _
                        Implements System.Collections.IComparer.Compare
        Dim returnVal as Integer = -1
        returnVal = [String].Compare(CType(x, _
                        ListViewItem).SubItems(col).Text, _
                        CType(y, ListViewItem).SubItems(col).Text)
        ' Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If

        Return returnVal
    End Function
End Class
