Option Explicit On
Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Data.SqlClient
Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Data
Imports System.Data.OleDb
Imports System.Text.RegularExpressions


Public Class FormMould

    Dim AdapterType As New MySqlDataAdapter("SELECT * FROM doctype", MySqlconnection)
    Dim tblType As DataTable
    Dim DsType As New DataSet

    Dim AdapterIfp As New MySqlDataAdapter("SELECT * FROM Ifp", MySqlconnection)
    Dim tblIfp As DataTable
    Dim DsIfp As New DataSet

    Dim AdapterDoc As New MySqlDataAdapter("SELECT * FROM Doc", MySqlconnection)
    Dim tblDoc As DataTable
    Dim DsDoc As New DataSet



    Private Sub FormMould_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AdapterDoc.Fill(DsDoc, "doc")
        tblDoc = DsDoc.Tables("doc")


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

        Dim rootNode As TreeNode

        Dim rowShow As DataRow(), i As Integer, sql As String, filename As String, IFPStatusStr As String, refresh As Boolean = True
        TreeViewIfp.Font = New Font("Courier New", 12, FontStyle.Bold)
        TreeViewIfp.Nodes.Clear()
        TreeViewIfp.BackColor = Color.White


        sql = "header = '" & ParameterTable("IfpFileHeader") & "' "
        rowShow = tblDoc.Select(sql, "filename, rev DESC")

        filename = ""
        For Each row In rowShow

            If row("filename").ToString <> filename Then
                rootNode = New TreeNode("Rev. " & row("REV").ToString & " - " & row("filename").ToString)

                TreeViewIfp.BeginUpdate()
                TreeViewIfp.Nodes.Add(rootNode)
                TreeViewIfp.EndUpdate()
                TreeViewIfp.ResumeLayout()
                filename = row("project").ToString
                IFPStatusStr = IFPStatus(row("project").ToString, refresh)
                If IFPStatusStr = "MASS_PRODUCTION" Then rootNode.ForeColor = Color.Green
                If IFPStatusStr = "OBSOLETE" Then rootNode.ForeColor = Color.Gray
                If IFPStatusStr = "SAMPLING" Then rootNode.ForeColor = Color.Blue
                If IFPStatusStr = "TOOLS_MODIFICATION" Then rootNode.ForeColor = Color.Orange
                If IFPStatusStr = "TOOLS_BUILDING" Then rootNode.ForeColor = Color.Orange

            End If
        Next


    End Sub


    Function IFPStatus(ByVal ifpDocId As Integer, ByVal refresh As Boolean) As String









    End Function





    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub TreeViewIfp_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewIfp.AfterSelect

    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub
End Class