Imports MySql.Data.MySqlClient

Public Class Form11
    Public idUsuario As Integer

    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "VideojuegosDB"
        Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

        Try
            Using myCon As New MySqlConnection(cadenaConexion)
                myCon.Open()

                Dim query As String = "SELECT nombre, username, date_nac, tipo_user, email FROM usuario WHERE idUsuario = @id"
                Using cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@id", idUsuario)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txt_box_names.Text = reader("nombre").ToString()
                            txt_box_user.Text = reader("username").ToString()
                            dtp_date.Value = Convert.ToDateTime(reader("date_nac"))
                            ComboBox1.Text = reader("tipo_user").ToString()
                            txt_box_email.Text = reader("email").ToString()
                        Else
                            MsgBox("Usuario no encontrado.", MsgBoxStyle.Critical, "Error")
                            Me.Close()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error al cargar los datos del usuario: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form10)
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
