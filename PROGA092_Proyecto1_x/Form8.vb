Imports MySql.Data.MySqlClient
Public Class Form8
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form5)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form5 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub btnCreateVG_Click(sender As Object, e As EventArgs) Handles btnCreateVG.Click
        ' Crear una nueva instancia de Form7
        Dim form7 As New Form7()
        form7.formularioAnterior = Me  ' Establece Form7 como el formulario anterior
        ' Mostrar Form7
        form7.Show()
        ' Cerrar el formulario actual (Form8)
        Me.Hide()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "VideojuegosDB"
        Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

        ' Cargar los datos
        CargarDLCs(cadenaConexion)
    End Sub

    Private Sub CargarDLCs(cadenaConexion As String)
        ' Crear la conexión y el comando
        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                ' Abrir la conexión
                myCon.Open()

                ' Crear el adaptador y el DataTable
                Dim query As String = "SELECT idDLC, nombre, precio, year_Lanzamiento FROM dlc"
                Dim da As New MySqlDataAdapter(query, myCon)
                Dim dt As New DataTable()

                ' Llenar el DataTable
                da.Fill(dt)

                ' Limpiar el DataGridView y eliminar la columna de acciones si existe
                DataGridView1.DataSource = Nothing
                DataGridView1.Columns.Clear()

                ' Asignar el DataTable al DataGridView de dlc's
                DataGridView1.DataSource = dt

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
            ' Obtener el ID del DLC en la fila clickeada
            Dim idDLC As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("idDLC").Value)

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
                        VerDetalleDLC(idDLC)
                    Case "editar"
                        EditarDLC(idDLC)
                    Case "eliminar"
                        EliminarDLC(idDLC)
                End Select
            End If
        End If
    End Sub

    ' Método para ver detalles de dlc
    Private Sub VerDetalleDLC(idDLC As Integer)
        ' Aquí puedes abrir un formulario con la información del dlc
        ' Ejemplo: Mostrar detalles en un cuadro de mensaje
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            myCon.Open()

            ' Recuperar detalles del DLC por id
            Dim query As String = "SELECT * FROM dlc WHERE idDLC = @idDLC"
            Dim cmd As New MySqlCommand(query, myCon)
            cmd.Parameters.AddWithValue("@idDLC", idDLC)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ' Aquí mostrar los detalles (puedes crear un formulario más detallado si es necesario)
                MsgBox("ID: " & reader("idDLC") & vbCrLf &
                       "Nombre: " & reader("nombre") & vbCrLf &
                       "Precio: " & reader("precio") & vbCrLf &
                       "Año de lanzamiento: " & reader("year_Lanzamiento"))
            End If
        End Using
    End Sub

    ' Método para editar DLC
    Private Sub EditarDLC(idDLC As Integer)
        ' Crear un formulario para editar los datos del DLC
        Dim formEditar As New Form()
        formEditar.Text = "Editar DLC ID: " & idDLC
        formEditar.Width = 400
        formEditar.Height = 350
        formEditar.StartPosition = FormStartPosition.CenterParent
        formEditar.FormBorderStyle = FormBorderStyle.FixedDialog
        formEditar.MaximizeBox = False
        formEditar.MinimizeBox = False

        ' Variables para almacenar los datos actuales
        Dim nombre As String = ""
        Dim precio As String = ""
        Dim year_Lanzamiento As String = ""

        ' Obtener los datos actuales del DLC
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            Try
                myCon.Open()

                ' Consulta para obtener los datos del DLC
                Dim query As String = "SELECT * FROM dlc WHERE idDLC = @idDLC"
                Dim cmd As New MySqlCommand(query, myCon)
                cmd.Parameters.AddWithValue("@idDLC", idDLC)

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    nombre = reader("nombre").ToString()
                    precio = reader("precio").ToString()
                    year_Lanzamiento = reader("year_Lanzamiento").ToString()
                End If
            Catch ex As Exception
                MsgBox("Error al cargar datos: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Return
            End Try
        End Using

        ' Crear controles para el formulario
        Dim lblNombre As New Label() With {.Text = "Nombre:", .Location = New Point(30, 30), .Width = 100}
        Dim txtNombre As New TextBox() With {.Text = nombre, .Location = New Point(150, 30), .Width = 200}

        Dim lblPrecio As New Label() With {.Text = "Precio:", .Location = New Point(30, 70), .Width = 100}
        Dim txtPrecio As New TextBox() With {.Text = precio, .Location = New Point(150, 70), .Width = 200}

        Dim lblYearLanzamiento As New Label() With {.Text = "Año de lanzamiento:", .Location = New Point(30, 110), .Width = 120}
        Dim dtpYearLanzamiento As New TextBox() With {.Text = year_Lanzamiento, .Location = New Point(150, 110), .Width = 200}

        ' Crear botones de Guardar y Cancelar
        Dim btnGuardar As New Button() With {.Text = "Guardar", .DialogResult = DialogResult.OK, .Location = New Point(100, 250)}
        Dim btnCancelar As New Button() With {.Text = "Cancelar", .DialogResult = DialogResult.Cancel, .Location = New Point(200, 250)}

        ' Agregar controles al formulario
        formEditar.Controls.AddRange({lblNombre, txtNombre, lblPrecio, txtPrecio, lblYearLanzamiento,
                                  dtpYearLanzamiento, btnGuardar, btnCancelar})

        formEditar.AcceptButton = btnGuardar
        formEditar.CancelButton = btnCancelar

        ' Mostrar el formulario y procesar el resultado
        If formEditar.ShowDialog() = DialogResult.OK Then
            ' Actualizar los datos en la base de datos
            Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                Try
                    myCon.Open()

                    ' Consulta para actualizar los datos
                    Dim query As String = "UPDATE dlc SET nombre = @nombre, precio = @precio, " &
                                     "year_Lanzamiento = @year_Lanzamiento " &
                                     "WHERE idDLC = @idDLC"

                    Dim cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text)
                    cmd.Parameters.AddWithValue("@precio", txtPrecio.Text)
                    cmd.Parameters.AddWithValue("@year_Lanzamiento", dtpYearLanzamiento.Text)
                    cmd.Parameters.AddWithValue("@idDLC", idDLC)

                    cmd.ExecuteNonQuery()

                    MsgBox("DLC actualizado correctamente.", MsgBoxStyle.Information, "Actualización exitosa")

                    ' Recargar los datos
                    CargarDLCs("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")

                Catch ex As Exception
                    MsgBox("Error al actualizar: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Using
        End If
    End Sub

    ' Método para eliminar dlc
    Private Sub EliminarDLC(idDLC As Integer)
        ' Preguntar al DLC si está seguro de eliminar
        Dim confirmDelete As DialogResult = MessageBox.Show("¿Está seguro de eliminar al DLC con ID: " & idDLC & "?",
                                                        "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmDelete = DialogResult.Yes Then
            ' Eliminar DLC de la base de datos
            Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                Try
                    myCon.Open()

                    ' Ejecutar la eliminación del DLC
                    Dim query As String = "DELETE FROM dlc WHERE idDLC = @idDLC"
                    Dim cmd As New MySqlCommand(query, myCon)
                    cmd.Parameters.AddWithValue("@idDLC", idDLC)

                    cmd.ExecuteNonQuery()

                    ' Actualizar la tabla
                    MsgBox("DLC con ID: " & idDLC & " eliminado correctamente.", MsgBoxStyle.Information, "Eliminación exitosa")

                    ' Limpiar y recargar el DataGridView
                    If DataGridView1.Columns.Contains("Acciones") Then
                        DataGridView1.Columns.Remove("Acciones")
                    End If

                    CargarDLCs("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")

                Catch ex As Exception
                    MsgBox("Error al eliminar el DLC: " & ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            End Using
        End If
    End Sub


End Class