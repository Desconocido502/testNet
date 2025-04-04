Public Class Form5
    ' Variable para almacenar el formulario inicial
    Public formularioAnterior As Form
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form5)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form2 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub PBUsers_Click(sender As Object, e As EventArgs) Handles PBUsers.Click
        'Se cierra la ventana actual
        Me.Hide()

        MsgBox("DashBoard Videojuegos.", MsgBoxStyle.Information, "Videojuegos")
        ' vamos al form de dashboard videojuegos
        Dim form6 As New Form6()

        ' Mostrar el formulario adecuado
        form6.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'Se cierra la ventana actual
        Me.Hide()

        MsgBox("DLC's", MsgBoxStyle.Information, "DLC")
        ' vamos al form de CREAR DLC
        Dim form8 As New Form8()

        ' Mostrar el formulario adecuado
        form8.Show()
    End Sub
End Class