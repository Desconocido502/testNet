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

    Private Sub btnDeleteAccount_Click(sender As Object, e As EventArgs) Handles btnDeleteAccount.Click
        Dim confirm As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar tu cuenta? Esta acción no se puede deshacer.",
                                                  "Confirmar eliminación",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning)

        If confirm = DialogResult.Yes Then
            Dim server = "localhost"
            Dim user = "Root"
            Dim pwd = "root"
            Dim database = "VideojuegosDB"
            Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

            Try
                Using myCon As New MySqlConnection(cadenaConexion)
                    myCon.Open()

                    Dim deleteQuery As String = "DELETE FROM usuario WHERE idUsuario = @id"
                    Using cmd As New MySqlCommand(deleteQuery, myCon)
                        cmd.Parameters.AddWithValue("@id", idUsuario)
                        Dim rows = cmd.ExecuteNonQuery()

                        If rows > 0 Then
                            MessageBox.Show("Tu cuenta ha sido eliminada.", "Cuenta eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ' Cerrar este form y volver al login u otro lugar
                            Me.Close()
                            Form1.Show() ' O el formulario de inicio de sesión
                        Else
                            MessageBox.Show("No se pudo eliminar la cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error al eliminar la cuenta: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub


    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        If txt_box_names.Text = "" Or txt_box_user.Text = "" Or txt_box_email.Text = "" Or ComboBox1.Text = "" Then
            MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "VideojuegosDB"
        Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

        Try
            Using myCon As New MySqlConnection(cadenaConexion)
                myCon.Open()

                ' Si la nueva contraseña está vacía, no la actualizamos
                Dim updateQuery As String

                If txt_box_new_password.Text.Trim() = "" Then
                    updateQuery = "
                    UPDATE usuario
                    SET nombre = @nombre,
                        username = @username,
                        date_nac = @fecha,
                        tipo_user = @tipo,
                        email = @correo
                    WHERE idUsuario = @id
                "
                Else
                    updateQuery = "
                    UPDATE usuario
                    SET nombre = @nombre,
                        username = @username,
                        date_nac = @fecha,
                        tipo_user = @tipo,
                        email = @correo,
                        contraseña = @newPass
                    WHERE idUsuario = @id
                "
                End If

                Using cmd As New MySqlCommand(updateQuery, myCon)
                    cmd.Parameters.AddWithValue("@nombre", txt_box_names.Text.Trim())
                    cmd.Parameters.AddWithValue("@username", txt_box_user.Text.Trim())
                    cmd.Parameters.AddWithValue("@fecha", dtp_date.Value)
                    cmd.Parameters.AddWithValue("@tipo", ComboBox1.Text.Trim())
                    cmd.Parameters.AddWithValue("@correo", txt_box_email.Text.Trim())
                    cmd.Parameters.AddWithValue("@id", idUsuario)

                    If txt_box_new_password.Text.Trim() <> "" Then
                        cmd.Parameters.AddWithValue("@newPass", txt_box_new_password.Text.Trim())
                    End If

                    Dim rowsAffected = cmd.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("No se realizaron cambios.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al actualizar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
