Public Class Form14
    Public idUsuario As Integer
    Private Sub Form14_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        ' Cerrar el formulario actual (Form14)
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