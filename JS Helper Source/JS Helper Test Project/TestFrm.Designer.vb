<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestFrm
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
		Me.Web = New Microsoft.Web.WebView2.WinForms.WebView2()
		CType(Me.Web, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Web
		'
		Me.Web.CreationProperties = Nothing
		Me.Web.DefaultBackgroundColor = System.Drawing.Color.White
		Me.Web.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Web.Location = New System.Drawing.Point(0, 0)
		Me.Web.Name = "Web"
		Me.Web.Size = New System.Drawing.Size(800, 450)
		Me.Web.TabIndex = 0
		Me.Web.ZoomFactor = 1.0R
		'
		'TestFrm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(800, 450)
		Me.Controls.Add(Me.Web)
		Me.Name = "TestFrm"
		Me.Text = "Form1"
		CType(Me.Web, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents Web As Microsoft.Web.WebView2.WinForms.WebView2
End Class
