<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form8
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btn_cancel = New System.Windows.Forms.Button()
        Me.btnCreateVG = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(319, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(290, 38)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Dashboard DLC's"
        '
        'btn_cancel
        '
        Me.btn_cancel.BackColor = System.Drawing.Color.SteelBlue
        Me.btn_cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cancel.ForeColor = System.Drawing.Color.White
        Me.btn_cancel.Location = New System.Drawing.Point(12, 25)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(141, 52)
        Me.btn_cancel.TabIndex = 40
        Me.btn_cancel.Text = "Regresar"
        Me.btn_cancel.UseVisualStyleBackColor = False
        '
        'btnCreateVG
        '
        Me.btnCreateVG.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCreateVG.BackColor = System.Drawing.Color.NavajoWhite
        Me.btnCreateVG.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateVG.Location = New System.Drawing.Point(847, 92)
        Me.btnCreateVG.Name = "btnCreateVG"
        Me.btnCreateVG.Size = New System.Drawing.Size(126, 51)
        Me.btnCreateVG.TabIndex = 39
        Me.btnCreateVG.Text = "Crear nuevo dlc"
        Me.btnCreateVG.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 149)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(961, 380)
        Me.DataGridView1.TabIndex = 41
        '
        'Form8
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 541)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btnCreateVG)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form8"
        Me.Text = "Form8"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_cancel As Button
    Friend WithEvents btnCreateVG As Button
    Friend WithEvents DataGridView1 As DataGridView
End Class
