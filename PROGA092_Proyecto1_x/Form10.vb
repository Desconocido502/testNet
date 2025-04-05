Public Class Form10
    Public idUsuario As Integer
    Private Sub PBUsers_Click(sender As Object, e As EventArgs) Handles PBUsers.Click
        'Se cierra la ventana actual
        Me.Hide()
        MsgBox("Compras Juegos.", MsgBoxStyle.Information, "Juegos")
        Dim form12 As New Form12()
        form12.idUsuario = idUsuario
        form12.Show()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        'Se cierra la ventana actual
        Me.Hide()
        MsgBox("Ver Mis Reseñas", MsgBoxStyle.Information, "Reseñas")
        Dim form13 As New Form13()
        form13.idUsuario = idUsuario
        form13.Show()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        'Se cierra la ventana actual
        Me.Hide()
        MsgBox("Historial de Compras", MsgBoxStyle.Information, "VideoJuegos")
        Dim form14 As New Form14()
        form14.idUsuario = idUsuario
        form14.Show()
    End Sub

    Private Sub btn_return_login_Click(sender As Object, e As EventArgs) Handles btn_return_login.Click
        ' Cerrar el formulario actual (Form2)
        Me.Close()

        ' Mostrar Form1 si se ha ocultado
        For Each form As Form In Application.OpenForms
            If TypeOf form Is Form1 Then
                form.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Dim form11 As New Form11()
        form11.idUsuario = Me.idUsuario
        form11.Show()
    End Sub
End Class