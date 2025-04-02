<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.btn_return_login = New System.Windows.Forms.Button()
        Me.btnConexion = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PBGames = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PBUsers = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel1.SuspendLayout()
        CType(Me.PBGames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PBUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(396, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(337, 32)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Dashboard Administrador"
        '
        'btn_return_login
        '
        Me.btn_return_login.BackColor = System.Drawing.Color.SteelBlue
        Me.btn_return_login.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_return_login.ForeColor = System.Drawing.Color.White
        Me.btn_return_login.Location = New System.Drawing.Point(29, 26)
        Me.btn_return_login.Name = "btn_return_login"
        Me.btn_return_login.Size = New System.Drawing.Size(214, 51)
        Me.btn_return_login.TabIndex = 10
        Me.btn_return_login.Text = "Cerrar sesión"
        Me.btn_return_login.UseVisualStyleBackColor = False
        '
        'btnConexion
        '
        Me.btnConexion.BackColor = System.Drawing.Color.Navy
        Me.btnConexion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConexion.ForeColor = System.Drawing.Color.White
        Me.btnConexion.Location = New System.Drawing.Point(854, 26)
        Me.btnConexion.Name = "btnConexion"
        Me.btnConexion.Size = New System.Drawing.Size(170, 51)
        Me.btnConexion.TabIndex = 11
        Me.btnConexion.Text = "Conectar"
        Me.btnConexion.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.PBGames)
        Me.Panel1.Location = New System.Drawing.Point(587, 196)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 136)
        Me.Panel1.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(44, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 32)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Juegos"
        '
        'PBGames
        '
        Me.PBGames.Image = Global.PROGA092_Proyecto1_x.My.Resources.Resources.games
        Me.PBGames.Location = New System.Drawing.Point(66, 12)
        Me.PBGames.Name = "PBGames"
        Me.PBGames.Size = New System.Drawing.Size(64, 64)
        Me.PBGames.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBGames.TabIndex = 12
        Me.PBGames.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.PBUsers)
        Me.Panel2.Location = New System.Drawing.Point(321, 196)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 136)
        Me.Panel2.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(39, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 32)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Usuarios"
        '
        'PBUsers
        '
        Me.PBUsers.Image = Global.PROGA092_Proyecto1_x.My.Resources.Resources.users
        Me.PBUsers.Location = New System.Drawing.Point(66, 12)
        Me.PBUsers.Name = "PBUsers"
        Me.PBUsers.Size = New System.Drawing.Size(64, 64)
        Me.PBUsers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBUsers.TabIndex = 12
        Me.PBUsers.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(52, 472)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(169, 17)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Desea crear una cuenta?"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LinkLabel1.Location = New System.Drawing.Point(244, 472)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(75, 17)
        Me.LinkLabel1.TabIndex = 16
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Registralo!"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 532)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnConexion)
        Me.Controls.Add(Me.btn_return_login)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form2"
        Me.Text = "Administrador"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PBGames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PBUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents btn_return_login As Button
    Friend WithEvents btnConexion As Button
    Friend WithEvents PBGames As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents PBUsers As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
