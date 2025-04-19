Imports MySql.Data.MySqlClient

Public Class Form7
    ' Variable para almacenar el formulario anterior
    Public formularioAnterior As Form

    ' Diccionario para guardar el mapeo entre nombre y ID de videojuego
    Private videojuegosDict As New Dictionary(Of String, Integer)

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                Dim query As String = "SELECT idVideoJuego, nombre FROM Videojuego"
                Dim cmd As New MySqlCommand(query, myCon)

                myCon.Open()
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                ComboBox1.Items.Clear()
                videojuegosDict.Clear()

                While reader.Read()
                    Dim id As Integer = reader.GetInt32("idVideoJuego")
                    Dim nombre As String = reader.GetString("nombre")
                    ComboBox1.Items.Add(nombre)
                    videojuegosDict(nombre) = id
                End While

                reader.Close()
            Catch ex As Exception
                MessageBox.Show("Error al cargar los videojuegos: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim nombreDLC As String = txt_box_name.Text.Trim()
        Dim precioStr As String = txt_box_price.Text.Trim()
        Dim anioStr As String = txt_box_year.Text.Trim()
        Dim videojuegoNombre As String = ComboBox1.SelectedItem?.ToString()

        If String.IsNullOrEmpty(nombreDLC) OrElse String.IsNullOrEmpty(precioStr) OrElse
           String.IsNullOrEmpty(anioStr) OrElse String.IsNullOrEmpty(videojuegoNombre) Then
            MessageBox.Show("Por favor, complete todos los campos.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim precio As Decimal
        Dim anio As Integer

        If Not Decimal.TryParse(precioStr, precio) Then
            MessageBox.Show("El precio debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not Integer.TryParse(anioStr, anio) Then
            MessageBox.Show("El año debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not videojuegosDict.ContainsKey(videojuegoNombre) Then
            MessageBox.Show("No se pudo encontrar el videojuego seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim videojuegoId As Integer = videojuegosDict(videojuegoNombre)

        ' Confirmación
        Dim mensaje As String = $"Nombre DLC: {nombreDLC}{vbCrLf}" &
                                $"Precio: {precio}{vbCrLf}" &
                                $"Año: {anio}{vbCrLf}" &
                                $"Videojuego: {videojuegoNombre}"
        Dim resultado As DialogResult = MessageBox.Show(mensaje, "Confirmar Registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

        If resultado = DialogResult.OK Then
            Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

            Using myCon As New MySqlConnection(cadenaConexion)
                Try
                    myCon.Open()
                    Dim query As String = "INSERT INTO dlc (nombre, precio, year_Lanzamiento, VideoJuego_idVideoJuego) VALUES (@nombre, @precio, @year_Lanzamiento, @VideoJuego_idVideoJuego)"
                    Dim cmd As New MySqlCommand(query, myCon)

                    cmd.Parameters.AddWithValue("@nombre", nombreDLC)
                    cmd.Parameters.AddWithValue("@precio", precio)
                    cmd.Parameters.AddWithValue("@year_Lanzamiento", anio)
                    cmd.Parameters.AddWithValue("@VideoJuego_idVideoJuego", videojuegoId)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("DLC registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Limpiar campos
                    txt_box_name.Text = ""
                    txt_box_price.Text = ""
                    txt_box_year.Text = ""
                    ComboBox1.SelectedIndex = -1
                Catch ex As Exception
                    MessageBox.Show("Error al registrar el DLC: " & ex.Message)
                End Try
            End Using
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Cerrar el formulario actual (Form7)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form5 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub
End Class
