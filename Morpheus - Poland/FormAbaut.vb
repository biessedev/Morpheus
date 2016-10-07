Public Class FormAbaut

    Private Sub FormAbaut_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        FormStart.Show()
    End Sub

    Private Sub FormAbaut_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        FormStart.Hide()
        RichTextBox1.Rtf = "{\rtf1\ansi\ansicpg1252\deff0\deflang1040{\fonttbl{\f0\froman\fprq2\fcharset0 Cambria;}{\f1\froman\fprq2\fcharset0 Times New Roman;}{\f2\fnil\fcharset0 Microsoft Sans Serif;}} {\colortbl ;\red54\green95\blue145;} \viewkind4\uc1\pard\keep\keepn\li142\sb240\f0\fs24  \par \cf1\lang1033\b\fs28 SrvDoc - \fs22  Document Management System 2011\fs28\par \pard\keep\keepn\li142\sl276\slmult1\fs22 Bitron 3DEQ  China\par \'a9 2009 - 2015 All right reserved\f1\par \f0 Software developed by R&D Department\par [Antonio Tomasiello]\f1\par \pard\cf0\lang1040\b0\f2\fs17\par } "

    End Sub

End Class