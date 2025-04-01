Imports MySql.Data.MySqlClient

Public Class Form3
    ' Variable para almacenar el formulario inicial
    Public formularioAnterior As Form

    ' Conexión a la base de datos
    Private connectionString As String = "server=localhost;userid=root;password=;database=VideojuegosDB;SslMode=none"

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Establecer el estilo del ComboBox para que sea de solo lectura
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList

        ' Cargar los tipos de usuario en el ComboBox
        ComboBox1.Items.Add("Escoja una opción...")
        ComboBox1.Items.Add("Administrador")
        ComboBox1.Items.Add("Usuario")

        ' Seleccionar el placeholder por defecto
        ComboBox1.SelectedIndex = 0

        ' Establecer el texto de placeholder para los TextBox
        SetPlaceholder(txt_box_names, "Nombre Completo")
        SetPlaceholder(txt_box_user, "Usuario")
        SetPlaceholder(txt_box_email, "Correo Electrónico")
        SetPlaceholder(txt_box_password, "Contraseña", isPassword:=True)
        SetPlaceholder(txt_box_c_password, "Confirmar Contraseña", isPassword:=True)
    End Sub

    ' Función para asignar el placeholder inicial a un TextBox
    Private Sub SetPlaceholder(textBox As TextBox, placeholder As String, Optional isPassword As Boolean = False)
        textBox.Text = placeholder
        textBox.ForeColor = Color.Gray
        textBox.Tag = placeholder ' Guardar el placeholder en la propiedad Tag
        If isPassword Then
            textBox.UseSystemPasswordChar = False ' Mostrar texto normal al inicio
        End If
    End Sub

    ' Evento Enter (cuando el usuario hace clic en el TextBox)
    Private Sub TextBox_Enter(sender As Object, e As EventArgs) Handles txt_box_names.Enter, txt_box_user.Enter, txt_box_email.Enter, txt_box_password.Enter, txt_box_c_password.Enter
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = textBox.Tag.ToString() Then
            textBox.Text = ""
            textBox.ForeColor = Color.Black
            If textBox Is txt_box_password OrElse textBox Is txt_box_c_password Then
                textBox.UseSystemPasswordChar = True ' Ocultar el texto al escribir
            End If
        End If
    End Sub

    ' Evento Leave (cuando el usuario sale del TextBox)
    Private Sub TextBox_Leave(sender As Object, e As EventArgs) Handles txt_box_names.Leave, txt_box_user.Leave, txt_box_email.Leave, txt_box_password.Leave, txt_box_c_password.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = textBox.Tag.ToString()
            textBox.ForeColor = Color.Gray
            If textBox Is txt_box_password OrElse textBox Is txt_box_c_password Then
                textBox.UseSystemPasswordChar = False ' Mostrar texto normal si es un placeholder
            End If
        End If
    End Sub

    ' Botón para cancelar y regresar al formulario anterior
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Mostrar el formulario anterior y ocultar Form3
        If formularioAnterior IsNot Nothing Then
            formularioAnterior.Show()
            Me.Close()  ' Cierra Form3
        End If
    End Sub

    ' Botón de registro
    Private Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim nombre As String = txt_box_names.Text.Trim()
        Dim usuario As String = txt_box_user.Text.Trim()
        Dim fechaNacimiento As Date = dtp_date.Value
        Dim fechaNacimientoStr As String = fechaNacimiento.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
        Dim correo As String = txt_box_email.Text.Trim()
        Dim contrasena As String = txt_box_password.Text.Trim()
        Dim confirmacionContrasena As String = txt_box_c_password.Text.Trim()
        Dim tipoUsuario As String

        ' Validaciones de campos vacíos
        If nombre = "Nombre Completo" OrElse usuario = "Usuario" OrElse correo = "Correo Electrónico" OrElse
           contrasena = "Contraseña" OrElse confirmacionContrasena = "Confirmar Contraseña" OrElse ComboBox1.SelectedIndex = 0 Then
            MessageBox.Show("Por favor, complete todos los campos.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Validar la confirmación de contraseña
        If contrasena <> confirmacionContrasena Then
            MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Obtener el tipo de usuario seleccionado
        Select Case ComboBox1.SelectedItem.ToString()
            Case "Administrador"
                tipoUsuario = "admin"
            Case "Usuario"
                tipoUsuario = "user"
            Case Else
                MessageBox.Show("Seleccione un tipo de usuario válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
        End Select

        ' Insertar usuario en la base de datos
        Dim mensaje As String = $"Nombre Completo: {nombre}{vbCrLf}" &
                                $"Usuario: {usuario}{vbCrLf}" &
                                $"Fecha de Nacimiento: {fechaNacimientoStr}{vbCrLf}" &
                                $"Correo Electrónico: {correo}{vbCrLf}" &
                                $"Contraseña: {contrasena}{vbCrLf}" &
                                $"Tipo de Usuario: {ComboBox1.SelectedItem.ToString()}"

        Dim resultado As DialogResult = MessageBox.Show(mensaje, "Confirmar Datos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

        If resultado = DialogResult.OK Then
            Dim connectionString As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"
            Using connection As New MySqlConnection(connectionString)
                connection.Open()
                ' Verificar si el correo ya existe
                Dim checkEmailCommand As New MySqlCommand("SELECT COUNT(*) FROM usuario WHERE email = @email", connection)
                checkEmailCommand.Parameters.AddWithValue("@email", correo)

                Dim emailCount As Integer = Convert.ToInt32(checkEmailCommand.ExecuteScalar())

                If emailCount > 0 Then
                    MessageBox.Show("El correo electrónico ya está registrado. Use otro correo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If


                Dim command As New MySqlCommand("INSERT INTO usuario (nombre, username, date_nac, email, password_, tipo_user) VALUES (@nombre, @username, @date_nac, @email, @password_, @tipo_user)", connection)
                command.Parameters.AddWithValue("@nombre", nombre)
                command.Parameters.AddWithValue("@username", usuario)
                command.Parameters.AddWithValue("@date_nac", fechaNacimientoStr)
                command.Parameters.AddWithValue("@email", correo)
                command.Parameters.AddWithValue("@password_", contrasena)
                command.Parameters.AddWithValue("@tipo_user", tipoUsuario)

                command.ExecuteNonQuery()
            End Using

            ' Limpiar los campos del formulario
            txt_box_names.Text = ""
            txt_box_user.Text = ""
            txt_box_email.Text = ""
            txt_box_password.Text = ""
            txt_box_c_password.Text = ""
            ComboBox1.SelectedIndex = 0
        End If

        ' Mostrar el formulario anterior y ocultar Form
        If formularioAnterior IsNot Nothing Then
            formularioAnterior.Show()
            Me.Close()  ' Cierra Form
        End If

    End Sub
End Class
