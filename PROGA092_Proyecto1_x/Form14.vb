Imports MySql.Data.MySqlClient

Public Class Form14
    Public idUsuario As Integer
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                myCon.Open()

                Dim query As String = "
                SELECT 
                    V.nombre AS Videojuego,
                    C.tipo_compra AS Tipo,
                    C.fecha_hora AS Fecha,
                    C.Total_Pagar AS Monto,
                    C.nit AS NIT,
                    C.comentario AS Comentario
                FROM 
                    Compra C
                JOIN 
                    Videojuego V ON C.VideoJuego_idVideoJuego = V.idVideoJuego
                WHERE 
                    C.Usuario_idUsuario = @idUsuario
                ORDER BY 
                    C.fecha_hora DESC
            "

                Dim cmd As New MySqlCommand(query, myCon)
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

                Dim adapter As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                adapter.Fill(dt)

                DataGridView1.DataSource = dt
                DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Catch ex As Exception
                MessageBox.Show("Error al cargar el historial de compras: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form14)
        Me.Close()

        ' Mostrar Form10 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form10 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub
End Class