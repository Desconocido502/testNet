Imports MySql.Data.MySqlClient

Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "VideojuegosDB"
        Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

        ' Cargar los datos
        CargarUsuarios(cadenaConexion)
    End Sub

    Private Sub CargarUsuarios(cadenaConexion As String)
        ' Crear la conexión y el comando
        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                ' Abrir la conexión
                myCon.Open()

                ' Crear el adaptador y el DataTable
                Dim query As String = "SELECT idUsuario, nombre, username, date_nac, email, tipo_user FROM usuario"
                Dim da As New MySqlDataAdapter(query, myCon)
                Dim dt As New DataTable()

                ' Llenar el DataTable
                da.Fill(dt)

                ' Limpiar el DataGridView y eliminar la columna de acciones si existe
                DataGridView1.DataSource = Nothing
                DataGridView1.Columns.Clear()

                ' Asignar el DataTable al DataGridView de usuarios
                DataGridView1.DataSource = dt

                ' Agregar formato visual para la columna tipo_user
                If DataGridView1.Columns.Contains("tipo_user") Then
                    Dim tipoUserColumn As DataGridViewColumn = DataGridView1.Columns("tipo_user")

                    ' Ajustar el formato de visualización para esta columna
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim tipoValue As String = If(row.Cells("tipo_user").Value IsNot Nothing, row.Cells("tipo_user").Value.ToString(), "")
                            If tipoValue = "admin" Then
                                row.Cells("tipo_user").Value = "Administrador"
                            ElseIf tipoValue = "user" Then
                                row.Cells("tipo_user").Value = "Usuario"
                            End If
                        End If
                    Next
                End If

                ' Agregar la columna de acciones
                AgregarColumnaAcciones()

            Catch ex As MySqlException
                MsgBox("Error de base de datos: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Catch ex As Exception
                MsgBox("Error inesperado: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End Using
    End Sub

    Private Sub AgregarColumnaAcciones()
        ' Crear la columna de botones
        Dim accionesColumn As New DataGridViewButtonColumn()

        ' Establecer el nombre y el encabezado de la columna
        accionesColumn.Name = "Acciones"
        accionesColumn.HeaderText = "Acciones"
        accionesColumn.Text = "Seleccionar acción"
        accionesColumn.UseColumnTextForButtonValue = True

        ' Agregar la columna de acciones al DataGridView
        DataGridView1.Columns.Add(accionesColumn)

        ' Manejar el evento de clic en el botón
        AddHandler DataGridView1.CellContentClick, AddressOf DataGridView1_CellContentClick
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        ' Verificar si el clic fue en la columna de acciones (índice de la columna "Acciones")
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = DataGridView1.Columns("Acciones").Index Then
            ' Obtener el ID del usuario en la fila clickeada
            Dim idUsuario As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("idUsuario").Value)

            ' Crear un Form pequeño para mostrar las opciones
            Dim formOpciones As New Form()
            formOpciones.Text = "Seleccionar acción"
            formOpciones.Width = 300
            formOpciones.Height = 150
            formOpciones.StartPosition = FormStartPosition.CenterParent
            formOpciones.FormBorderStyle = FormBorderStyle.FixedDialog
            formOpciones.MaximizeBox = False
            formOpciones.MinimizeBox = False

            ' Crear ComboBox con opciones
            Dim cboAcciones As New ComboBox()
            cboAcciones.DropDownStyle = ComboBoxStyle.DropDownList
            cboAcciones.Items.Add("Ver")
            cboAcciones.Items.Add("Editar")
            cboAcciones.Items.Add("Eliminar")
            cboAcciones.SelectedIndex = 0
            cboAcciones.Width = 200
            cboAcciones.Location = New Point(50, 20)

            ' Crear botones Aceptar y Cancelar
            Dim btnAceptar As New Button()
            btnAceptar.Text = "Aceptar"
            btnAceptar.DialogResult = DialogResult.OK
            btnAceptar.Location = New Point(50, 60)

            Dim btnCancelar As New Button()
            btnCancelar.Text = "Cancelar"
            btnCancelar.DialogResult = DialogResult.Cancel
            btnCancelar.Location = New Point(150, 60)

            ' Agregar controles al formulario
            formOpciones.Controls.Add(cboAcciones)
            formOpciones.Controls.Add(btnAceptar)
            formOpciones.Controls.Add(btnCancelar)
            formOpciones.AcceptButton = btnAceptar
            formOpciones.CancelButton = btnCancelar

            ' Mostrar el formulario y procesar el resultado
            Dim result As DialogResult = formOpciones.ShowDialog()

            If result = DialogResult.OK Then
                Select Case cboAcciones.SelectedItem.ToString().ToLower()
                    Case "ver"
                        VerDetalleUsuario(idUsuario)
                    Case "editar"
                        EditarUsuario(idUsuario)
                    Case "eliminar"
                        EliminarUsuario(idUsuario)
                End Select
            End If
        End If
    End Sub

    ' Método para ver detalles de usuario
    Private Sub VerDetalleUsuario(idUsuario As Integer)
        ' Aquí puedes abrir un formulario con la información del usuario
        ' Ejemplo: Mostrar detalles en un cuadro de mensaje
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            myCon.Open()

            ' Recuperar detalles del usuario por id
            Dim query As String = "SELECT * FROM usuario WHERE idUsuario = @idUsuario"
            Dim cmd As New MySqlCommand(query, myCon)
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ' Aquí mostrar los detalles (puedes crear un formulario más detallado si es necesario)
                MsgBox("ID: " & reader("idUsuario") & vbCrLf &
                       "Nombre: " & reader("nombre") & vbCrLf &
                       "Username: " & reader("username") & vbCrLf &
                       "Fecha Nacimiento: " & reader("date_nac") & vbCrLf &
                       "Email: " & reader("email") & vbCrLf &
                       "Tipo: " & reader("tipo_user"))
            End If
        End Using
    End Sub

    ' Método para editar usuario
    Private Sub EditarUsuario(idUsuario As Integer)
        ' Crear un formulario para editar los datos del usuario
        Dim formEditar As New Form()
        formEditar.Text = "Editar Usuario ID: " & idUsuario
        formEditar.Width = 400
        formEditar.Height = 350
        formEditar.StartPosition = FormStartPosition.CenterParent
        formEditar.FormBorderStyle = FormBorderStyle.FixedDialog
        formEditar.MaximizeBox = False
        formEditar.MinimizeBox = False

        ' Variables para almacenar los datos actuales
        Dim nombre As String = ""
        Dim username As String = ""
        Dim dateNac As DateTime
        Dim email As String = ""
        Dim tipoUser As String = ""

        ' Obtener los datos actuales del usuario
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            Try
                myCon.Open()

                ' Consulta para obtener los datos del usuario
                Dim query As String = "SELECT * FROM usuario WHERE idUsuario = @idUsuario"
                Dim cmd As New MySqlCommand(query, myCon)
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    nombre = reader("nombre").ToString()
                    username = reader("username").ToString()
                    dateNac = Convert.ToDateTime(reader("date_nac"))
                    email = reader("email").ToString()
                    tipoUser = reader("tipo_user").ToString()
                End If
            Catch ex As Exception
                MsgBox("Error al cargar datos: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Return
            End Try
        End Using

        ' Crear controles para el formulario
        Dim lblNombre As New Label() With {.Text = "Nombre:", .Location = New Point(30, 30), .Width = 100}
        Dim txtNombre As New TextBox() With {.Text = nombre, .Location = New Point(150, 30), .Width = 200}

        Dim lblUsername As New Label() With {.Text = "Username:", .Location = New Point(30, 70), .Width = 100}
        Dim txtUsername As New TextBox() With {.Text = username, .Location = New Point(150, 70), .Width = 200}

        Dim lblFechaNac As New Label() With {.Text = "Fecha Nacimiento:", .Location = New Point(30, 110), .Width = 120}
        Dim dtpFechaNac As New DateTimePicker() With {.Value = dateNac, .Location = New Point(150, 110), .Width = 200}

        Dim lblEmail As New Label() With {.Text = "Email:", .Location = New Point(30, 150), .Width = 100}
        Dim txtEmail As New TextBox() With {.Text = email, .Location = New Point(150, 150), .Width = 200}

        Dim lblTipoUser As New Label() With {.Text = "Tipo Usuario:", .Location = New Point(30, 190), .Width = 100}
        Dim cboTipoUser As New ComboBox() With {.Location = New Point(150, 190), .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList}

        ' Crear un diccionario para mapear las etiquetas visuales a los valores internos
        Dim tiposUsuario As New Dictionary(Of String, String)()
        tiposUsuario.Add("Administrador", "admin")
        tiposUsuario.Add("Usuario", "user")

        ' Agregar opciones al ComboBox de tipo de usuario con etiquetas amigables
        cboTipoUser.Items.Add("Administrador")
        cboTipoUser.Items.Add("Usuario")

        ' Seleccionar el tipo correcto en el ComboBox
        If tipoUser = "admin" Then
            cboTipoUser.SelectedIndex = 0 ' Administrador
        Else
            cboTipoUser.SelectedIndex = 1 ' Usuario
        End If

        ' Crear botones de Guardar y Cancelar
        Dim btnGuardar As New Button() With {.Text = "Guardar", .DialogResult = DialogResult.OK, .Location = New Point(100, 250)}
        Dim btnCancelar As New Button() With {.Text = "Cancelar", .DialogResult = DialogResult.Cancel, .Location = New Point(200, 250)}

        ' Agregar controles al formulario
        formEditar.Controls.AddRange({lblNombre, txtNombre, lblUsername, txtUsername, lblFechaNac,
                                  dtpFechaNac, lblEmail, txtEmail, lblTipoUser, cboTipoUser,
                                  btnGuardar, btnCancelar})

        formEditar.AcceptButton = btnGuardar
        formEditar.CancelButton = btnCancelar

        ' Mostrar el formulario y procesar el resultado
        If formEditar.ShowDialog() = DialogResult.OK Then
            ' Obtener el valor interno correspondiente a la etiqueta seleccionada
            Dim tipoUserSeleccionado As String = "user" ' Valor por defecto
            If cboTipoUser.SelectedIndex = 0 Then
                tipoUserSeleccionado = "admin"
            Else
                tipoUserSeleccionado = "user"
            End If

            ' Actualizar los datos en la base de datos
            Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                Try
                    myCon.Open()

                    ' Consulta para actualizar los datos
                    Dim query As String = "UPDATE usuario SET nombre = @nombre, username = @username, " &
                                     "date_nac = @dateNac, email = @email, tipo_user = @tipoUser " &
                                     "WHERE idUsuario = @idUsuario"

                    Dim cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text)
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text)
                    cmd.Parameters.AddWithValue("@dateNac", dtpFechaNac.Value)
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                    cmd.Parameters.AddWithValue("@tipoUser", tipoUserSeleccionado)
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

                    cmd.ExecuteNonQuery()

                    MsgBox("Usuario actualizado correctamente.", MsgBoxStyle.Information, "Actualización exitosa")

                    ' Recargar los datos
                    CargarUsuarios("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")

                Catch ex As Exception
                    MsgBox("Error al actualizar: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Using
        End If
    End Sub

    ' Método para eliminar usuario
    Private Sub EliminarUsuario(idUsuario As Integer)
        ' Preguntar al usuario si está seguro de eliminar
        Dim confirmDelete As DialogResult = MessageBox.Show("¿Está seguro de eliminar al usuario con ID: " & idUsuario & "?",
                                                        "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmDelete = DialogResult.Yes Then
            ' Eliminar usuario de la base de datos
            Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                Try
                    myCon.Open()

                    ' Ejecutar la eliminación del usuario
                    Dim query As String = "DELETE FROM usuario WHERE idUsuario = @idUsuario"
                    Dim cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario)

                    cmd.ExecuteNonQuery()

                    ' Actualizar la tabla
                    MsgBox("Usuario con ID: " & idUsuario & " eliminado correctamente.", MsgBoxStyle.Information, "Eliminación exitosa")

                    ' Limpiar y recargar el DataGridView
                    If DataGridView1.Columns.Contains("Acciones") Then
                        DataGridView1.Columns.Remove("Acciones")
                    End If

                    CargarUsuarios("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")

                Catch ex As Exception
                    MsgBox("Error al eliminar el usuario: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Using
        End If
    End Sub
End Class
