Imports MySql.Data.MySqlClient

Public Class Form13
    Public idUsuario As Integer
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form13)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form10 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                myCon.Open()

                Dim query As String = "
            SELECT 
                V.nombre AS Videojuego,
                C.comentario AS Comentario,
                C.fecha_hora AS Fecha
            FROM 
                Compra C
            JOIN 
                Videojuego V ON C.VideoJuego_idVideoJuego = V.idVideoJuego
            WHERE 
                C.Usuario_idUsuario = @idUsuario AND C.comentario IS NOT NULL AND C.comentario <> ''
        "

                Dim cmd As New MySqlCommand(query, myCon)
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                DataGridView1.DataSource = dt
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Catch ex As Exception
                MessageBox.Show("Error al cargar las reseñas: " & ex.Message)
            End Try
        End Using

    End Sub

    Private Sub btnCreateUser_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class