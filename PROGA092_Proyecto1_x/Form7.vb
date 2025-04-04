Imports MySql.Data.MySqlClient

Public Class Form7
    ' Variable para almacenar el formulario inicial
    Public formularioAnterior As Form
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "videojuegosdb"
        Dim cadenaConexion = "server=" & server & ";user=" & user & ";pwd=" & pwd & ";database=" & database & ";SslMode=none"

        ' Usamos MySqlConnection para la conexión a MySQL
        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                ' Consulta SQL para obtener los nombres de los videojuegos
                Dim query As String = "SELECT nombre FROM Videojuego"
                ' Usamos MySqlCommand en lugar de SqlCommand
                Dim cmd As New MySqlCommand(query, myCon)

                ' Abrir la conexión
                myCon.Open()

                ' Ejecutar la consulta y leer los resultados
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ' Limpiar el ComboBox antes de agregar nuevos elementos
                ComboBox1.Items.Clear()

                ' Agregar los nombres de los videojuegos al ComboBox
                While reader.Read()
                    ComboBox1.Items.Add(reader("nombre").ToString())
                End While

                ' Cerrar el lector de datos
                reader.Close()
            Catch ex As Exception
                ' Manejo de errores
                MessageBox.Show("Error al cargar los videojuegos: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs)
        ' Cerrar el formulario actual (Form7)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form5 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Cerrar el formulario actual (Form7)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form8 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub
End Class
