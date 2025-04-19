Imports MySql.Data.MySqlClient

Public Class Form12
    Public idUsuario As Integer

    Private videojuegosDict As New Dictionary(Of String, Integer)

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                myCon.Open()

                ' VIDEOJUEGOS
                Dim queryVG As String = "SELECT idVideoJuego, nombre FROM Videojuego"
                Dim cmdVG As New MySqlCommand(queryVG, myCon)
                Dim readerVG = cmdVG.ExecuteReader()
                CBVG.Items.Clear()
                videojuegosDict.Clear()
                While readerVG.Read()
                    Dim id = readerVG.GetInt32("idVideoJuego")
                    Dim nombre = readerVG.GetString("nombre")
                    CBVG.Items.Add(nombre)
                    videojuegosDict(nombre) = id
                End While
                readerVG.Close()

            Catch ex As Exception
                MessageBox.Show("Error al cargar los datos: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_register_Click(sender As Object, e As EventArgs) Handles btn_register.Click
        Dim selectedVG = CBVG.SelectedItem?.ToString()
        Dim nit = txt_box_nit.Text.Trim()
        Dim comentario = txt_box_comment.Text.Trim()
        Dim montoStr = txt_box_pay.Text.Trim()

        If String.IsNullOrEmpty(selectedVG) OrElse String.IsNullOrEmpty(nit) OrElse String.IsNullOrEmpty(montoStr) Then
            MessageBox.Show("Debes llenar todos los campos obligatorios (videojuego, NIT y monto).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim monto As Decimal
        If Not Decimal.TryParse(montoStr, monto) Then
            MessageBox.Show("Monto inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not videojuegosDict.ContainsKey(selectedVG) Then
            MessageBox.Show("Videojuego no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim idVG = videojuegosDict(selectedVG)
        Dim tipoCompra = If(chkDlc.Checked, "Juego base y DLC", "Juego base")
        Dim fechaCompra = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        Dim cadenaConexion As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

        Using myCon As New MySqlConnection(cadenaConexion)
            Try
                myCon.Open()
                Dim nuevoNumFactura As Integer = 1
                Dim queryMaxFactura As String = "SELECT IFNULL(MAX(Num_factura), 0) + 1 FROM Compra"
                Dim cmdMaxFactura As New MySqlCommand(queryMaxFactura, myCon)
                nuevoNumFactura = Convert.ToInt32(cmdMaxFactura.ExecuteScalar())

                ' Insertar en Compra
                Dim queryCompra As String = "INSERT INTO Compra (Num_factura, Usuario_idUsuario, VideoJuego_idVideoJuego, fecha_hora, nit, Total_Pagar, comentario, tipo_compra) " &
                            "VALUES (@Num_factura, @Usuario_idUsuario, @VideoJuego_idVideoJuego, @fecha_hora, @nit, @Total_Pagar, @comentario, @tipo_compra); SELECT LAST_INSERT_ID();"

                Dim cmdCompra As New MySqlCommand(queryCompra, myCon)
                cmdCompra.Parameters.AddWithValue("@Num_factura", nuevoNumFactura)
                cmdCompra.Parameters.AddWithValue("@Usuario_idUsuario", idUsuario)
                cmdCompra.Parameters.AddWithValue("@VideoJuego_idVideoJuego", idVG)
                cmdCompra.Parameters.AddWithValue("@fecha_hora", fechaCompra)
                cmdCompra.Parameters.AddWithValue("@nit", nit)
                cmdCompra.Parameters.AddWithValue("@Total_Pagar", monto)
                cmdCompra.Parameters.AddWithValue("@comentario", comentario)
                cmdCompra.Parameters.AddWithValue("@tipo_compra", tipoCompra)

                Dim idCompra As Integer = Convert.ToInt32(cmdCompra.ExecuteScalar())


                MessageBox.Show("¡Compra y factura registradas con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Limpiar campos
                CBVG.SelectedIndex = -1
                chkDlc.Checked = False
                txt_box_nit.Clear()
                txt_box_pay.Clear()
                txt_box_comment.Clear()

            Catch ex As Exception
                MessageBox.Show("Error al registrar la compra: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form12)
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
