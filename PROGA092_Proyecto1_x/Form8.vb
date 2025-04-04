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
        form7.formularioAnterior = Me  ' Establece Form1 como el formulario anterior
        ' Mostrar Form3
        form7.Show()
        ' Cerrar el formulario actual (Form1)
        Me.Hide()
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class