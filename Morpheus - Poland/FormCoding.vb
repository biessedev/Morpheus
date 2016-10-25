
Option Strict Off
Option Compare Text

Imports System.Security.Cryptography
Imports System.Text.UTF8Encoding
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System

Public Class FormCoding

    'Vettore di bytes casuali usati per oscurare la chiave:
    'verrà usato nella funzione di derivazione della password
    Private SaltBytes As Byte() = New Byte() _
    {162, 21, 92, 34, 27, 239, 64, 30, 136, 102, 223}

    'Questo è un vettore di inizializzazione per algoritmi
    'simmetrici a 256-bit. Si nota, infatti, che è lungo 32 bytes
    Private IV32 As Byte() = New Byte() _
    {133, 206, 56, 64, 110, 158, 132, 22, _
    99, 190, 35, 129, 101, 49, 204, 248, _
    251, 243, 13, 194, 160, 195, 89, 152, _
    149, 227, 245, 5, 218, 86, 161, 124}

    'La derivazione di password è un'altra delle tecniche
    'usate in criptazione: si cifra la chiave iniziale con un
    'algoritmo di derivazione, fornendo come base un vettore
    'di bytes casuali, chiamato <strong>salt crittografico</strong>.
    'L'algoritmo applica una trasformazione sulla chiave un
    'numero dato di volte (iterazioni) e restituisce alla fine una
    'password di lunghezza specificata. In questo caso, poiché
    'si sta utilizzando l'algoritmo Rijndael a 256 bit, sarà
    'di 32 bytes
    Private Function DerivePassword(ByVal Key As String) As Byte()
        'Il provider crittografico
        Dim Derive As Rfc2898DeriveBytes
        'Il risultato dell'operazione
        Dim DerivedBytes() As Byte

        'Crea un nuovo provider crittografico per l'algoritmo
        'di derivazione RFC2898, che ha come input Key, come
        'salt crittografico l'array SaltBytes sopra definito
        'e come numero di iterazioni 5. Il secondo e il terzo
        'parametro sono del tutto casuali: li si può
        'modificare arbitrariamente
        Derive = New Rfc2898DeriveBytes(Key, SaltBytes, 5)
        'Applica la trasformazione e deriva una nuova password
        'ottenuta come array di 32 bytes
        DerivedBytes = Derive.GetBytes(32)

        Return DerivedBytes
    End Function

    'Data una chiave Key e un messaggio Text, usa l'algoritmo simmetrico 
    'a blocchi Rijndael (AES) per ottenere un insieme di dati criptato
    Public Function RijndaelEncrypt(ByVal Key As String, _
        ByVal Text As String) As Byte()
        'Crea il nuovo provider crittografico per questo algoritmo
        Dim Provider As New RijndaelManaged
        'La password derivata
        Dim BytePassword As Byte()
        'L'oggetto che ha il compito di processare le informazioni
        Dim Encryptor As ICryptoTransform
        'L'output della funzione
        Dim Output As Byte()
        'L'input della funzione, ossia il testo convertito
        'in forma binaria. Il formato UTF8 permette di
        'mantenere anche i caratteri speciali come quelli accentati
        Dim Input As Byte() = UTF8.GetBytes(Text)

        'Imposta la dimensione della chiave
        Provider.KeySize = 256
        'Imposta la dimensione del blocco 
        Provider.BlockSize = 256
        'Ottiene la password tramite derivazione dalla chiave
        BytePassword = DerivePassword(Key)
        'Crea un nuovo oggetto codificatore
        Encryptor = Provider.CreateEncryptor(BytePassword, IV32)
        'Cripta il testo
        Output = Encryptor.TransformFinalBlock(Input, 0, Input.Length)

        'Elimina le informazioni fornite al provider
        Provider.Clear()
        'Distrugge l'oggetto codificatore
        Encryptor.Dispose()

        Return Output
    End Function

    'Data una chiave Key e un messaggio cifrato Data, usa l'algoritmo 
    'simmetrico a blocchi Rijndael (AES) per ottenere l'insieme di 
    'dati di partenza
    Public Function RijndaelDecrypt(ByVal Key As String, _
        ByVal Data() As Byte) As String
        'Crea un nuovo provider crittografico
        Dim Provider As New RijndaelManaged
        'La password derivata
        Dim BytePassword As Byte()
        'L'oggetto che ha il compito di processare le informazioni
        Dim Decryptor As ICryptoTransform
        'L'output della funzione in bytes
        Dim Output As Byte()

        Provider.KeySize = 256
        Provider.BlockSize = 256
        BytePassword = DerivePassword(Key)
        'Ottiene l'oggetto decodificatore
        Decryptor = Provider.CreateDecryptor(BytePassword, IV32)

        'Tenta di decriptare il messaggio: se la chiave è
        'sbagliata, lancia un'eccezione

        Try
            Output = Decryptor.TransformFinalBlock(Data, 0, Data.Length)
            Return UTF8.GetString(Output)
        Catch Ex As Exception
            MsgBox("Crypt fail!")
        Finally
            Provider.Clear()
            Decryptor.Dispose()
        End Try


    End Function

    'I dati prodotti in output sono allocati in vettori di bytes,
    'ma le stringhe non sono il supporto più adatto per
    'visualizzarli, poiché vengono compresi anche
    'caratteri di controllo o null terminator. In ogni caso,
    'la stringa sarebbe o compromessa o illeggibile (non che
    'non lo debba essere). Questa funzione restituisce tutto
    'il vettore come rappresentazione esadecimale in stringa
    'rendendo più gradevole la vista del nostro
    'magnifico messaggio cifrato
    Public Function ToHex(ByVal Bytes() As Byte) As String
        Dim Result As New StringBuilder

        For I = 0 To Bytes.Length - 1
            'Accoda alla stringa il codice in formato esadecimale, 
            'facendo in modo che occupi sempre due posti, eventualmente
            'pareggiando con uno zero sulla sinistra
            Result.AppendFormat("{0:X2}", Bytes(I))
        Next

        Return Result.ToString
    End Function

    Public Function ToBin(ByVal hexString As String) As Byte()
        Try
            Dim mybyte(0 To hexString.Length / 2 - 1) As Byte
            For I = 1 To hexString.Length / 2 Step 1
                mybyte((I - 1)) = (16 * HexToInt(Mid(hexString, I * 2 - 1, 1)) + HexToInt(Mid(hexString, I * 2, 1)))

            Next

            Return mybyte
        Catch ex As Exception

        End Try

    End Function

    Function HexToInt(ByVal s As String) As Integer

        If IsNumeric(s) Then
            HexToInt = Val(s)
        ElseIf s = "A" Then
            HexToInt = 10
        ElseIf s = "B" Then
            HexToInt = 11
        ElseIf s = "C" Then
            HexToInt = 12
        ElseIf s = "D" Then
            HexToInt = 13
        ElseIf s = "E" Then
            HexToInt = 14
        ElseIf s = "F" Then
            HexToInt = 15
        Else
        End If

    End Function

    Sub Main()
        Console.WriteLine("Inserire un testo qualsiasi:")
        Dim Input As String = Console.ReadLine
        Console.WriteLine("Inserire una chiave di criptazione:")
        Dim Key As String = Console.ReadLine

        Try
            Dim Output As String = ToHex(RijndaelEncrypt(Key, Input))
            Console.WriteLine()
            Console.WriteLine("Il testo criptato è:")
            Console.WriteLine(Output)
        Catch CE As CryptographicException
            Console.WriteLine("Password errata!")
        End Try

        Console.ReadKey()
    End Sub

    Private Sub FormCoding_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        TextBoxPsw.Text = CreAccount.strPassword
        TextBoxName.Text = CreAccount.strUserName

    End Sub

    Private Sub DateTimePickerQ_CloseUp(ByVal sender As Object, ByVal e As EventArgs) Handles DateTimePickerQ.CloseUp
        ButtonDate.Text = date_to_string(DateTimePickerQ.Value)
    End Sub


    Private Sub ButtonEncrypt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonEncrypt.Click
        If ButtonDate.Text <> "" And TextBoxName.Text <> "" And TextBoxPsw.Text <> "" And TextBoxInfo.Text <> "" Then
            TextBoxencrypted.Text = ToHex(RijndaelEncrypt(TextBoxPsw.Text, UCase(TextBoxName.Text) & "." & ButtonDate.Text & "." & UCase(TextBoxInfo.Text)))
        Else
            MsgBox("Please fill date and info!")
        End If

    End Sub

    Private Sub ButtonDecrypt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDecrypt.Click
        TextBoxdecrypted.Text = RijndaelDecrypt(TextBoxPsw.Text, ToBin(TextBoxencrypted.Text))
    End Sub

    Private Sub ButtonValid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonValid.Click

        If ButtonValid.Text = "CHECK VALIDITY" Then
            Dim PSW = ""
            Dim Adapter As New MySqlDataAdapter("SELECT * FROM credentials where username='" & TextBoxName.Text & "'", MySqlconnection)
            Dim ds As New DataSet
            Adapter.Fill(ds)
            Dim tblCredentials As DataTable = ds.Tables(0)
            If tblCredentials.Rows.Count = 1 Then
                PSW = tblCredentials.Rows.Item(0)("PASSWORD").ToString
            Else
                MsgBox("User name not found in DB")
            End If

            If PSW <> "" Then

                If RijndaelDecrypt(PSW, ToBin(TextBoxencrypted.Text)) = UCase(TextBoxName.Text) & "." & ButtonDate.Text & "." & UCase(TextBoxInfo.Text) Then
                    ButtonValid.Text = "VALID!!!"
                    ButtonValid.BackColor = Color.Green
                Else
                    ButtonValid.Text = "NOT VALID!!!"
                    ButtonValid.BackColor = Color.Red
                End If
            Else
                ButtonValid.Text = "NOT VALID!!!"
                ButtonValid.BackColor = Color.Red
            End If
        Else

        End If

    End Sub

    Private Sub TextBoxencrypted_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxencrypted.TextChanged
        ButtonValid.Text = "CHECK VALIDITY"
        ButtonValid.BackColor = Color.Gray
    End Sub

    Private Sub TextBoxName_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxName.TextChanged
        ButtonValid.Text = "CHECK VALIDITY"
        ButtonValid.BackColor = Color.Gray
    End Sub

    Private Sub TextBoxInfo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBoxInfo.TextChanged
        ButtonValid.Text = "CHECK VALIDITY"
        ButtonValid.BackColor = Color.Gray
    End Sub

    Private Sub ButtonDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonDate.TextChanged
        ButtonValid.Text = "CHECK VALIDITY"
        ButtonValid.BackColor = Color.Gray
    End Sub

End Class