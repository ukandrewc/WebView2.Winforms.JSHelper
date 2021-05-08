Imports Microsoft.Web.WebView2.Core
Imports WebView2JSHelper.WebView2JSHelper

Public Class TestFrm
	Private Sub TestFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Web.EnsureCoreWebView2Async.RunSync
		Web.CoreWebView2.SetResourceManager(My.Resources.ResourceManager)
		Web.CoreWebView2.Navigate("https://w3.org/WAI/UA/2002/06/thead-test")
	End Sub

	Private Sub Web_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles Web.NavigationCompleted
		Web.CoreWebView2.ExecuteScriptResourceSync("Script\test.js")
	End Sub

	Private Sub Web_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles Web.CoreWebView2InitializationCompleted
	End Sub
End Class
