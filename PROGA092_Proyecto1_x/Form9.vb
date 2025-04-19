Imports MySql.Data.MySqlClient

Public Class Form9
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Cerrar el formulario actual (Form9)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form6 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim nombre As String = txtBoxName.Text.Trim()
        Dim categoria As String = cb_category.SelectedItem?.ToString()
        Dim precioText As String = txtBoxPrice.Text.Trim()
        Dim anioText As String = txtBoxYear.Text.Trim()
        Dim empresaDesarrolladora As String = txtBoxED.Text.Trim()

        ' Validaciones básicas
        If nombre = "" OrElse categoria = "" OrElse precioText = "" OrElse anioText = "" OrElse empresaDesarrolladora = "" Then
            MessageBox.Show("Por favor, complete todos los campos.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim precio As Decimal
        If Not Decimal.TryParse(precioText, precio) Then
            MessageBox.Show("El precio debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim anio As Integer
        If Not Integer.TryParse(anioText, anio) Then
            MessageBox.Show("El año debe ser un número entero válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Confirmación
        Dim mensaje As String = $"Nombre: {nombre}{vbCrLf}" &
                            $"Categoría: {categoria}{vbCrLf}" &
                            $"Precio: {precio}{vbCrLf}" &
                            $"Año de lanzamiento: {anio}{vbCrLf}" &
                            $"Empresa desarrolladora: {empresaDesarrolladora}"

        Dim resultado As DialogResult = MessageBox.Show(mensaje, "Confirmar Datos", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

        If resultado = DialogResult.OK Then
            Try
                Dim connectionString As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim command As New MySqlCommand("INSERT INTO videojuego (nombre, categoria, precio, year_lanzamiento, empresa_desarrollo) VALUES (@nombre, @categoria, @precio, @year_lanzamiento, @empresa_desarrollo)", connection)
                    command.Parameters.AddWithValue("@nombre", nombre)
                    command.Parameters.AddWithValue("@categoria", categoria)
                    command.Parameters.AddWithValue("@precio", precio)
                    command.Parameters.AddWithValue("@year_lanzamiento", anio)
                    command.Parameters.AddWithValue("@empresa_desarrollo", empresaDesarrolladora)

                    command.ExecuteNonQuery()
                End Using

                MessageBox.Show("Videojuego registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Limpiar campos
                txtBoxName.Text = ""
                cb_category.SelectedIndex = 0
                txtBoxPrice.Text = ""
                txtBoxYear.Text = ""
                txtBoxED.Text = ""

                ' Cerrar el formulario actual (Form9)
                Me.Close()

                ' Mostrar Form1 si se ha ocultado
                For Each form As Form In Application.OpenForms
                    If TypeOf form Is Form6 Then
                        form.Show()
                        Exit For
                    End If
                Next

            Catch ex As Exception
                MessageBox.Show("Error al registrar el videojuego: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Cargar categorías al ComboBox
        cb_category.Items.Add("Acción")
        cb_category.Items.Add("Aventura")
        cb_category.Items.Add("Estrategia")
        cb_category.Items.Add("Deportes")
        cb_category.Items.Add("Rol (RPG)")
        cb_category.SelectedIndex = 0 ' Selecciona la primera por defecto
    End Sub

End Class