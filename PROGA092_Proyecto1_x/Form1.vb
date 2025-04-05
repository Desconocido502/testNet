Imports MySql.Data.MySqlClient
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Establecer el texto de placeholder cuando el formulario carga
        txtBoxCU.Text = "Correo o Usuario"
        txtBoxCU.ForeColor = Color.Gray
        txtBoxPassword.Text = "Contraseña"
        txtBoxPassword.ForeColor = Color.Gray
        txtBoxPassword.UseSystemPasswordChar = True ' Ocultar el texto de contraseña
    End Sub

    Private Sub txtBoxCU_Enter(sender As Object, e As EventArgs) Handles txtBoxCU.Enter
        If txtBoxCU.Text = "Correo o Usuario" Then
            txtBoxCU.Text = ""
            txtBoxCU.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtBoxCU_Leave(sender As Object, e As EventArgs) Handles txtBoxCU.Leave
        If txtBoxCU.Text = "" Then
            txtBoxCU.Text = "Correo o Usuario"
            txtBoxCU.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtBoxPassword_Enter(sender As Object, e As EventArgs) Handles txtBoxPassword.Enter
        If txtBoxPassword.Text = "Contraseña" Then
            txtBoxPassword.Text = ""
            txtBoxPassword.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtBoxPassword_Leave(sender As Object, e As EventArgs) Handles txtBoxPassword.Leave
        If txtBoxPassword.Text = "" Then
            txtBoxPassword.Text = "Contraseña"
            txtBoxPassword.ForeColor = Color.Gray
        End If
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
                Label1.Text = "Conexión exitosa"
            End Using
        Catch ex As Exception
            Label1.Text = "Error: " & ex.Message
        End Try
    End Sub

    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        ' Recuperar datos ingresados
        Dim cu As String = txtBoxCU.Text ' CU: Correo o Usuario
        Dim password As String = txtBoxPassword.Text

        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "videojuegosdb"
        Dim cadenaConexion = "server=" & server & ";user=" & user & ";pwd=" & pwd & ";database=" & database & ";SslMode=none"

        Try
            Using myCon As New MySqlConnection(cadenaConexion)
                myCon.Open()

                Dim query As String = "SELECT idUsuario, tipo_user, username FROM usuario WHERE (email = @cu OR username = @cu) AND password_ = @password"
                Using cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@cu", cu)
                    cmd.Parameters.AddWithValue("@password", password)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()

                            Dim idUsuario As Integer = reader.GetInt32(0)
                            Dim tipoUsuario As String = reader.GetString(1)
                            Dim username As String = reader.GetString(2)

                            ' Limpiar inputs
                            txtBoxCU.Text = "Correo o Usuario"
                            txtBoxCU.ForeColor = Color.Gray
                            txtBoxPassword.Text = "Contraseña"
                            txtBoxPassword.ForeColor = Color.Gray

                            If tipoUsuario = "admin" Then
                                MsgBox("Bienvenido, Administrador.", MsgBoxStyle.Information, "Login Exitoso")
                                Dim form2 As New Form2()
                                form2.Show()
                            ElseIf tipoUsuario = "user" Then
                                MsgBox($"Inicio de sesión exitoso. {username}", MsgBoxStyle.Information, "Login Exitoso")
                                Dim form10 As New Form10()
                                form10.idUsuario = idUsuario
                                form10.Show()
                            End If

                            Me.Hide()
                        Else
                            MsgBox("Usuario o contraseña incorrectos.", MsgBoxStyle.Critical, "Login Fallido")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("Error de conexión: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

End Class


'INSERT INTO Usuario (nombre, username, date_nac, email, password_, tipo_user) 
''VALUES('Administrador', 'admin', '1990-01-01', 'admin@example.com', 'admin123', 'admin');