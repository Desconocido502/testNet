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
End Class