Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Linq
Imports MySql.Data.MySqlClient

Public Class FormBomOffer

    Private m_coll As ArrayList = New ArrayList()
    Private m_lastNode As TreeNode, m_firstNode As TreeNode
    Dim myImageList As New ImageList()
    Dim VersionsWithQuatity As Dictionary(Of String, Integer)

    Property SelectedNodes() As ArrayList
        Get
            Return m_coll
        End Get
        Set(ByVal Value As ArrayList)
            RemovePaintFromNodes()
            m_coll.Clear()
            m_coll = Value
            PaintSelectedNodes()
        End Set
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonImport.Click
        Dim builderBEQS As New Common.DbConnectionStringBuilder()
        builderBEQS.ConnectionString = ConfigurationManager.ConnectionStrings("BEQS").ConnectionString
        Using conBEQS = NewOpenConnectionSqlBeqs(builderBEQS("host"), builderBEQS("database"), builderBEQS("username"), builderBEQS("password"))
            For Each offerid As TreeNode In m_coll
                Dim tblBomOffer As DataTable
                Dim DsBomOffer As New DataSet
                Try


                    Using AdapterBomOffer As New SqlDataAdapter("select distinct a.BitronPN, max(a.offerId) as offerId, max(a.componentId) as componentId, sum(a.quantity) as RequestQT " &
                                                              "  from (select distinct case " &
                                                               "    when b.BitronPNMatch = 'true' and b.bitronPn is not null then SUBSTRING(b.bitronPn, PATINDEX('%[^0 ]%', b.bitronPn + ' '), LEN(b.bitronPn)) " &
                                                                "   else 'BEQS_' + cast(a.offerId as varchar(10)) + '_' +  cast(d.componentid as CHAR(10))  end as BitronPN, " &
                                                                "   b.BitronPNMatch, a.offerId, d.componentid , d.quantity " &
                                                                "   from quotegeneralinformation a " &
                                                                "   join offerversion c on a.offerId = c.offerId " &
                                                                "   join bomdetailed b on a.offerId = b.offerId " &
                                                                "   join componentversion d on d.offerid = a.offerid and d.offerversionid = c.offerVersionId and d.componentid = b.componentId " &
                                                                "   where a.offerid = " & offerid.Name & " ) a " &
                                                                " group by a.bitronPn", conBEQS)
                        AdapterBomOffer.Fill(DsBomOffer, "BomOffer")
                        tblBomOffer = DsBomOffer.Tables("BomOffer")
                    End Using
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Dim DsMaterialRequest As New DataSet
                Dim tblMaterialRequest As DataTable
                Dim builder As New Common.DbConnectionStringBuilder()
                builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
                Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
                    Using AdapterProd As New MySqlDataAdapter("SELECT distinct TRIM(LEADING '0' FROM bitronPn) as BitronPN FROM materialrequest ", con)
                        AdapterProd.Fill(DsMaterialRequest, "MaterialRequest")
                        tblMaterialRequest = DsMaterialRequest.Tables("MaterialRequest")
                    End Using

                    For Each bom In tblBomOffer.Rows
                        Dim bitronPn As String = ""
                        Dim sqlCommand As String = ""
                        Dim values As String = ""

                        bitronPn = (From a In tblMaterialRequest.AsEnumerable() Where a.Field(Of String)("BitronPN") = bom("bitronPn") Select a.Field(Of String)("bitronPn")).ToList().FirstOrDefault()
                        Dim qty = bom("RequestQt") * Me.VersionsWithQuatity.Item(offerid.Parent.Text)
                        If bitronPn Is Nothing Then
                            values = "VALUES(" &
                                            "'" & bom("bitronPn") & "'," &
                                            "'" & qty & "'," &
                                            "'" & offerid.Parent.Text & " - [" & bom("RequestQt") & "]'," &
                                            "'', '', 0, '', 0, 0, '', '', '', '', '', 0, 0, 0, 0, 0, 0, '', 0, '', '' )"
                            sqlCommand = "INSERT INTO MaterialRequest(bitronPN, RequestQt, BomList, des_pn, Brand, BrandALT, NotePurchasing, WareHouse3D, Delta," &
                                                "NoteRnd, pfp, doc, ProductionUsed, DeltaUsedFlag, RequestQt_1, RequestQt_2, RequestQt_3, RequestQt_4, RequestQt_5, STOCK_W," &
                                                "STATUS, w_wareHouse, RDA_ETA, Order_ETA) " & values

                        Else
                            sqlCommand = "UPDATE MaterialRequest Set RequestQt = " & qty & ", BomList = CONCAT(BomList, ';" & offerid.Parent.Text & " - [" & bom("RequestQt") & "]') where TRIM(LEADING '0' FROM bitronPn) = '" & bom("bitronPn") & "'"
                        End If
                        Dim cmd = New MySqlCommand(sqlCommand, con)
                        cmd.ExecuteNonQuery()
                    Next
                End Using
            Next
        End Using
        MessageBox.Show("Import is done!")
        Me.Close()
    End Sub

    Public Sub ShowForm(Versions As Dictionary(Of String, Integer))
        Dim tblBomOffer As DataTable
        Dim builderBEQS As New Common.DbConnectionStringBuilder()
        Dim DsBomOffer As New DataSet

        myImageList.Images.Add(My.Resources.check_icon)
        myImageList.Images.Add(My.Resources.uncheck_icon)
        TreeView1.ImageList = myImageList
        Me.VersionsWithQuatity = Versions

        builderBEQS.ConnectionString = ConfigurationManager.ConnectionStrings("BEQS").ConnectionString
        Using conBEQS = NewOpenConnectionSqlBeqs(builderBEQS("host"), builderBEQS("database"), builderBEQS("username"), builderBEQS("password"))
            For Each item In Versions
                Using AdapterBomOffer As New SqlDataAdapter("select a.offerversionname, a.offerId, c.customerName, b.offerName " &
                                                              "  from offerVersion a " &
                                                              "  join quotegeneralinformation b on a.offerId = b.offerId " &
                                                             "   join customer c on b.customerId = c.customerId " &
                                                              "  where offerversionname = '" & item.Key & "'", conBEQS)

                    AdapterBomOffer.Fill(DsBomOffer, "BomOffer")
                    tblBomOffer = DsBomOffer.Tables("BomOffer")
                    Dim VersionNode = New TreeNode(item.Key, myImageList.Images.Count, myImageList.Images.Count)
                    VersionNode.Name = "bitronpn"

                    For Each offer In tblBomOffer.Rows
                        Dim str As String = ""
                        Dim OfferNode As TreeNode

                        str = offer("offerid") & " – " & offer("customerName") & " - " & offer("offerName")
                        OfferNode = New TreeNode(str)
                        OfferNode.Name = offer("offerid")
                        OfferNode.ImageIndex = 1
                        VersionNode.Nodes.Add(OfferNode)
                    Next
                    If VersionNode.Nodes.Count > 0 Then
                        TreeView1.Nodes.Add(VersionNode)
                    End If
                End Using
            Next
        End Using
        If TreeView1.Nodes.Count <> 0 Then
            Me.Show()
        End If
    End Sub

    Private Sub FormBomOffer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TreeView1.ExpandAll()
        For Each parent As TreeNode In TreeView1.Nodes
            m_coll.Add(parent.Nodes(0))
        Next
        PaintSelectedNodes()
    End Sub

    Private Sub TreeView1_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles TreeView1.BeforeSelect
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim Parent As TreeNode = e.Node.Parent
        If Parent Is Nothing Then
            m_coll.Add(e.Node)
            RemovePaintFromNodes()
            m_coll.Remove(e.Node)
            PaintSelectedNodes()
        Else
            For Each child As TreeNode In Parent.Nodes
                If m_coll.Contains(child) Then
                    RemovePaintFromNodes()
                    m_coll.Remove(child)
                End If
            Next
            m_coll.Add(e.Node)
            PaintSelectedNodes()
        End If
    End Sub

    Private Sub PaintSelectedNodes()
        For Each n As TreeNode In m_coll
            If n.Parent Is Nothing Then
                n.ImageIndex = myImageList.Images.Count
            Else
                n.ImageIndex = 0
            End If
            n.BackColor = SystemColors.Highlight
            n.ForeColor = SystemColors.HighlightText
        Next
    End Sub

    Private Sub RemovePaintFromNodes()
        If m_coll.Count = 0 Then Return
        Dim n0 As TreeNode = CType(m_coll(0), TreeNode)
        Dim back As Color = n0.TreeView.BackColor
        Dim fore As Color = n0.TreeView.ForeColor

        For Each n As TreeNode In m_coll
            If n.Parent Is Nothing Then
                n.ImageIndex = myImageList.Images.Count
            Else
                n.ImageIndex = 1
            End If
            n.BackColor = back
            n.ForeColor = fore
        Next
    End Sub

    Private Function isParent(parentNode As TreeNode, childNode As TreeNode) As Boolean
        If parentNode.Equals(childNode) Then
            Return True
        End If
        Dim n As TreeNode = childNode
        Dim bFound As Boolean = False

        While bFound = False And n Is Nothing
            n = n.Parent
            bFound = If(n.Equals(parentNode), True, False)
        End While
        Return bFound
    End Function
End Class