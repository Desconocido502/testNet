Imports MySql.Data.MySqlClient
Public Class Form6
    ' Variable para almacenar el formulario inicial
    Public formularioAnterior As Form
    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form4)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form2 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Parámetros de la conexión a la base de datos
        Dim server = "localhost"
        Dim user = "Root"
        Dim pwd = "root"
        Dim database = "VideojuegosDB"
        Dim cadenaConexion = $"server={server};user={user};pwd={pwd};database={database};SslMode=none"

        ' Cargar los datos de los videojuegos
        CargarVideojuegos(cadenaConexion)
    End Sub

    Private Sub CargarVideojuegos(cadenaConexion As String)
        ' Crear la conexión y el comando
        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                ' Abrir la conexión
                myCon.Open()

                ' Crear el adaptador y el DataTable
                Dim query As String = "SELECT idVideoJuego, nombre, categoria, precio, empresa_desarrollo, year_lanzamiento FROM videojuego"
                Dim da As New MySqlDataAdapter(query, myCon)
                Dim dt As New DataTable()

                ' Llenar el DataTable
                da.Fill(dt)

                ' Limpiar el DataGridView y eliminar la columna de acciones si existe
                DataGridView1.DataSource = Nothing
                DataGridView1.Columns.Clear()

                ' Asignar el DataTable al DataGridView de videojuegos
                DataGridView1.DataSource = dt

                ' Agregar formato visual para la columna tipo de plataforma si es necesario
                If DataGridView1.Columns.Contains("plataforma") Then
                    Dim plataformaColumn As DataGridViewColumn = DataGridView1.Columns("plataforma")

                    ' Ajustar el formato de visualización para esta columna
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim plataformaValue As String = If(row.Cells("plataforma").Value IsNot Nothing, row.Cells("plataforma").Value.ToString(), "")
                            If plataformaValue = "pc" Then
                                row.Cells("plataforma").Value = "PC"
                            ElseIf plataformaValue = "ps5" Then
                                row.Cells("plataforma").Value = "PlayStation 5"
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
            ' Obtener el ID del videojuego en la fila clickeada
            Dim idVideojuego As Integer = Convert.ToInt32(DataGridView1.Rows(e.RowIndex).Cells("idVideojuego").Value)

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
                        VerDetalleVideojuego(idVideojuego)
                    Case "editar"
                        EditarVideojuego(idVideojuego)
                    Case "eliminar"
                        EliminarVideojuego(idVideojuego)
                End Select
            End If
        End If
    End Sub

    ' Método para ver detalles de videojuego
    Private Sub VerDetalleVideojuego(idVideojuego As Integer)
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            myCon.Open()

            ' Recuperar detalles del videojuego por id
            Dim query As String = "SELECT * FROM VideoJuego WHERE idVideoJuego = @idVideojuego"
            Dim cmd As New MySqlCommand(query, myCon)
            cmd.Parameters.AddWithValue("@idVideojuego", idVideojuego)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ' Mostrar los detalles del videojuego
                MsgBox("ID: " & reader("idVideoJuego") & vbCrLf &
                   "Nombre: " & reader("nombre") & vbCrLf &
                   "Categoria: " & reader("categoria") & vbCrLf &
                   "Precio: " & reader("precio") & vbCrLf &
                   "Empresa de Desarrollo: " & reader("empresa_desarrollo") & vbCrLf &
                   "Año de Lanzamiento: " & reader("year_lanzamiento"))
            End If
        End Using
    End Sub

    ' Método para editar videojuego
    Private Sub EditarVideojuego(idVideojuego As Integer)
        Dim formEditar As New Form()
        formEditar.Text = "Editar Videojuego ID: " & idVideojuego
        formEditar.Width = 400
        formEditar.Height = 350
        formEditar.StartPosition = FormStartPosition.CenterParent
        formEditar.FormBorderStyle = FormBorderStyle.FixedDialog
        formEditar.MaximizeBox = False
        formEditar.MinimizeBox = False

        ' Variables para almacenar los datos actuales
        Dim nombre As String = ""
        Dim categoria As String = ""
        Dim precio As Double = 0
        Dim empresa_desarrollo As String = ""
        Dim year_lanzamiento As Integer = 0

        ' Obtener los datos actuales del videojuego
        Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
            Try
                myCon.Open()

                ' Consulta para obtener los datos del videojuego
                Dim query As String = "SELECT * FROM VideoJuego WHERE idVideoJuego = @idVideojuego"
                Dim cmd As New MySqlCommand(query, myCon)
                cmd.Parameters.AddWithValue("@idVideojuego", idVideojuego)

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    nombre = reader("nombre").ToString()
                    categoria = reader("categoria").ToString()
                    precio = Convert.ToDouble(reader("precio"))
                    empresa_desarrollo = reader("empresa_desarrollo").ToString()
                    year_lanzamiento = Convert.ToInt32(reader("year_lanzamiento"))
                End If
            Catch ex As Exception
                MsgBox("Error al cargar datos: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Return
            End Try
        End Using

        ' Crear controles para el formulario de edición
        Dim lblNombre As New Label() With {.Text = "Nombre:", .Location = New Point(30, 30), .Width = 60}
        Dim txtNombre As New TextBox() With {.Text = nombre, .Location = New Point(100, 30), .Width = 200}

        Dim lblCategoria As New Label() With {.Text = "Categoria:", .Location = New Point(30, 70), .Width = 80}
        Dim txtCategoria As New TextBox() With {.Text = categoria, .Location = New Point(100, 70), .Width = 200}

        Dim lblPrecio As New Label() With {.Text = "Precio:", .Location = New Point(30, 110), .Width = 60}
        Dim txtPrecio As New TextBox() With {.Text = precio.ToString(), .Location = New Point(100, 110), .Width = 200}

        Dim lblEmpresa As New Label() With {.Text = "Empresa:", .Location = New Point(30, 150), .Width = 80}
        Dim txtEmpresa As New TextBox() With {.Text = empresa_desarrollo, .Location = New Point(100, 150), .Width = 200}

        Dim lblYear As New Label() With {.Text = "Año de Lanzamiento:", .Location = New Point(30, 190), .Width = 120}
        Dim txtYear As New TextBox() With {.Text = year_lanzamiento.ToString(), .Location = New Point(150, 190), .Width = 200}

        ' Agregar controles al formulario
        formEditar.Controls.Add(lblNombre)
        formEditar.Controls.Add(txtNombre)
        formEditar.Controls.Add(lblCategoria)
        formEditar.Controls.Add(txtCategoria)
        formEditar.Controls.Add(lblPrecio)
        formEditar.Controls.Add(txtPrecio)
        formEditar.Controls.Add(lblEmpresa)
        formEditar.Controls.Add(txtEmpresa)
        formEditar.Controls.Add(lblYear)
        formEditar.Controls.Add(txtYear)

        ' Crear y agregar el botón de guardar cambios
        Dim btnGuardar As New Button() With {.Text = "Guardar Cambios", .Location = New Point(150, 230)}
        AddHandler btnGuardar.Click, Sub(sender2, e2)
                                         ' Guardar cambios en la base de datos
                                         Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                                             myCon.Open()

                                             ' Preparar la consulta para actualizar el videojuego
                                             Dim queryUpdate As String = "UPDATE VideoJuego SET nombre = @nombre, categoria = @categoria, precio = @precio, empresa_desarrollo = @empresa_desarrollo, year_lanzamiento = @year_lanzamiento WHERE idVideoJuego = @idVideojuego"
                                             Dim cmdUpdate As New MySqlCommand(queryUpdate, myCon)
                                             cmdUpdate.Parameters.AddWithValue("@nombre", txtNombre.Text)
                                             cmdUpdate.Parameters.AddWithValue("@categoria", txtCategoria.Text)
                                             cmdUpdate.Parameters.AddWithValue("@precio", Convert.ToDouble(txtPrecio.Text))
                                             cmdUpdate.Parameters.AddWithValue("@empresa_desarrollo", txtEmpresa.Text)
                                             cmdUpdate.Parameters.AddWithValue("@year_lanzamiento", Convert.ToInt32(txtYear.Text))
                                             cmdUpdate.Parameters.AddWithValue("@idVideojuego", idVideojuego)

                                             ' Ejecutar la actualización
                                             cmdUpdate.ExecuteNonQuery()
                                         End Using

                                         ' Cerrar formulario de edición
                                         formEditar.Close()
                                     End Sub
        formEditar.Controls.Add(btnGuardar)

        ' Mostrar el formulario
        formEditar.ShowDialog()
    End Sub

    ' Método para eliminar videojuego
    Private Sub EliminarVideojuego(idVideojuego As Integer)
        ' Confirmación de eliminación
        Dim confirm As DialogResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este videojuego?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            ' Eliminar videojuego de la base de datos
            Using myCon As New MySqlConnection("server=localhost;user=Root;pwd=root;database=VideojuegosDB;SslMode=none")
                myCon.Open()

                ' Preparar la consulta para eliminar el videojuego
                Dim queryDelete As String = "DELETE FROM videojuegos WHERE idVideojuego = @idVideojuego"
                Dim cmdDelete As New MySqlCommand(queryDelete, myCon)
                cmdDelete.Parameters.AddWithValue("@idVideojuego", idVideojuego)

                ' Ejecutar la eliminación
                cmdDelete.ExecuteNonQuery()

                ' Mensaje de éxito
                MsgBox("Videojuego eliminado correctamente.", MsgBoxStyle.Information, "Eliminación Exitosa")
            End Using
        End If
    End Sub

    Private Sub btnCreateVG_Click(sender As Object, e As EventArgs) Handles btnCreateVG.Click
        'Se cierra la ventana actual
        Me.Hide()

        MsgBox("DLC's", MsgBoxStyle.Information, "DLC")
        ' vamos al form de CREAR VideoJuego
        Dim form9 As New Form9()

        ' Mostrar el formulario adecuado
        form9.Show()
    End Sub
End Class