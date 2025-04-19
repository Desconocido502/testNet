Imports MySql.Data.MySqlClient
Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form15
    Private connectionString As String = "server=localhost;user=Root;pwd=root;database=videojuegosdb;SslMode=none"

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarJuegosPorCategoria()
        CargarTop3PorCategoria()
        CargarJuegosPorEmpresa()
    End Sub

    Private Sub CargarJuegosPorCategoria()
        Chart1.Series.Clear()
        Dim series = Chart1.Series.Add("Juegos por Categoría")
        series.ChartType = SeriesChartType.Column

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT categoria, COUNT(*) AS cantidad FROM VideoJuego GROUP BY categoria"
            Using cmd As New MySqlCommand(query, conn)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    series.Points.AddXY(reader("categoria").ToString(), Convert.ToInt32(reader("cantidad")))
                End While
            End Using
        End Using
    End Sub

    Private Sub CargarTop3PorCategoria()
        Chart2.Series.Clear()
        Dim series = Chart2.Series.Add("Top 3 Juegos por Categoría")
        series.ChartType = SeriesChartType.Column

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "
                SELECT v1.nombre, v1.categoria, v1.precio
                FROM VideoJuego v1
                WHERE (
                    SELECT COUNT(*) FROM VideoJuego v2
                    WHERE v2.categoria = v1.categoria AND v2.precio > v1.precio
                ) < 3
                ORDER BY v1.categoria, v1.precio DESC;
            "
            Using cmd As New MySqlCommand(query, conn)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    Dim label As String = $"{reader("categoria")} - {reader("nombre")}"
                    series.Points.AddXY(label, Convert.ToDecimal(reader("precio")))
                End While
            End Using
        End Using
    End Sub

    Private Sub CargarJuegosPorEmpresa()
        Chart3.Series.Clear()
        Dim series = Chart3.Series.Add("Juegos por Compañía")
        series.ChartType = SeriesChartType.Pie

        Using conn As New MySqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT empresa_desarrollo, COUNT(*) AS cantidad FROM VideoJuego GROUP BY empresa_desarrollo"
            Using cmd As New MySqlCommand(query, conn)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    series.Points.AddXY(reader("empresa_desarrollo").ToString(), Convert.ToInt32(reader("cantidad")))
                End While
            End Using
        End Using
    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form15)
        Me.Close()

        ' Mostrar Form10 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form2 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub
End Class
