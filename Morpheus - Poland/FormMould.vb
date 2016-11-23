Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System
Imports System.Data
Imports System.Configuration

Public Class FormMould

    'Dim AdapterType As New MySqlDataAdapter("SELECT * FROM doctype", MySqlconnection)
    Dim tblType As DataTable
    Dim DsType As New DataSet
    'Dim AdapterIfp As New MySqlDataAdapter("SELECT * FROM Ifp", MySqlconnection)
    Dim tblIfp As DataTable
    Dim DsIfp As New DataSet
    'Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM Doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet



    Private Sub FormMould_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Dim builder As New Common.DbConnectionStringBuilder()
        builder.ConnectionString = ConfigurationManager.ConnectionStrings(hostName).ConnectionString
        Using con = NewConnectionMySql(builder("host"), builder("database"), builder("username"), builder("password"))
            Using AdapterDoc As New MySqlDataAdapter("SELECT * FROM Doc", con)
                AdapterDoc.Fill(DsDoc, "doc")
                tblDoc = DsDoc.Tables("doc")
            End Using
        End Using

        FillTreeview()

        ComboBoxStatus.Items.Clear()
        ComboBoxStatus.Items.Add("")
        ComboBoxStatus.Items.Add("MASS_PRODUCTION")
        ComboBoxStatus.Items.Add("OBSOLETE")
        ComboBoxStatus.Items.Add("SAMPLING")
        ComboBoxStatus.Items.Add("TOOLS_MODIFICATION")
        ComboBoxStatus.Items.Add("TOOLS_BUILDING")

        ComboBoxIFPStatusFilter.Items.Clear()
        ComboBoxIFPStatusFilter.Items.Add("")
        ComboBoxIFPStatusFilter.Items.Add("MASS_PRODUCTION")
        ComboBoxIFPStatusFilter.Items.Add("OBSOLETE")
        ComboBoxIFPStatusFilter.Items.Add("SAMPLING")
        ComboBoxIFPStatusFilter.Items.Add("TOOLS_MODIFICATION")
        ComboBoxIFPStatusFilter.Items.Add("TOOLS_BUILDING")


    End Sub


    Sub FillTreeview()
        TreeViewIfp.Font = New Font("Courier New", 12, FontStyle.Bold)
        TreeViewIfp.Nodes.Clear()
        TreeViewIfp.BackColor = Color.White

        Dim sql As String = "header = '" & ParameterTable("IfpFileHeader") & "' "
        Dim rowShow As DataRow() = tblDoc.Select(sql, "filename, rev DESC")

        Dim filename As String = ""
        For Each row In rowShow

            If row("filename").ToString <> filename Then
                Dim rootNode As TreeNode = New TreeNode("Rev. " & row("REV").ToString & " - " & row("filename").ToString)

                TreeViewIfp.BeginUpdate()
                TreeViewIfp.Nodes.Add(rootNode)
                TreeViewIfp.EndUpdate()
                TreeViewIfp.ResumeLayout()
                filename = row("project").ToString

            End If
        Next

    End Sub

End Class