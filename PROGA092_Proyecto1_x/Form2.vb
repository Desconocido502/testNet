Imports MySql.Data.MySqlClient
Public Class Form2
    Private Sub btn_return_login_Click(sender As Object, e As EventArgs) Handles btn_return_login.Click
        ' Cerrar el formulario actual (Form3)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form1 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub btnConexion_Click(sender As Object, e As EventArgs) Handles btnConexion.Click
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "videojuegosdb"
        Dim cadenaConexion = "server=" & server & ";user=" & user & ";pwd=" & pwd & ";database=" & database & ";SslMode=none"

        Try
            ' Conexión a la base de datos
            Using myCon As New MySqlConnection(cadenaConexion)
                myCon.Open()
                MsgBox("Conexión exitosa", MsgBoxStyle.Information, "Error")
            End Using
        Catch ex As Exception
            ' Si ocurre un error, mostrar el mensaje
            MsgBox("Error de conexión: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PBGames_Click(sender As Object, e As EventArgs) Handles PBGames.Click
        MsgBox("DashBoard Juegos.", MsgBoxStyle.Information, "Juegos")
    End Sub

    Private Sub PBUsers_Click(sender As Object, e As EventArgs) Handles PBUsers.Click
        MsgBox("DashBoard Usuarios.", MsgBoxStyle.Information, "Usuarios")
        ' vamos al form de dashboard usuarios
        Dim form4 As New Form4()

        ' Mostrar el formulario adecuado
        form4.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' Crear una nueva instancia de Form2
        Dim form3 As New Form3()
        form3.formularioAnterior = Me  ' Establece Form1 como el formulario anterior
        ' Mostrar Form2
        form3.Show()
        ' Cerrar el formulario actual (Form1)
        Me.Hide()
    End Sub
End Class