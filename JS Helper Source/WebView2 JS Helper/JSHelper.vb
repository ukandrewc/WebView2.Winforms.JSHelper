Imports System.Runtime.CompilerServices
Imports Microsoft.Web.WebView2.Core

Public Module WebView2JSHelper

	Private Property ResourceManager As Resources.ResourceManager

	<Extension>
	Function ExecuteScriptResourceAsync(CoreWeb As CoreWebView2, Path As String) As Task(Of String)
		Return CoreWeb.ExecuteScriptAsync(ResourceScript(Path))
	End Function

	<Extension>
	Function ExecuteScriptResourceSync(CoreWeb As CoreWebView2, Path As String) As String
		Return CoreWeb.ExecuteScriptAsync(ResourceScript(Path)).RunSync
	End Function

	Private ReadOnly Property IsFromFile() As Boolean
		Get
#If DEBUG Then
			Return True
#Else
				Return False
#End If
		End Get
	End Property

	Private Function ResourceScript(Path As String) As String
		Dim Name = ""
		Dim Ret = ""
		If IsFromFile Then
			Dim Info = New IO.DirectoryInfo(My.Application.Info.DirectoryPath)
			While Info.Name <> "bin"
				Info = Info.Parent
			End While
			Name = IO.Path.Combine(Info.Parent.FullName, Path)
			Try
				Ret = IO.File.ReadAllText(Name)
			Catch ex As Exception
				Console.WriteLine(ex.Message)
			End Try
		Else
			Name = IO.Path.GetFileNameWithoutExtension(Path)
			If ResourceManager.GetString($"{Name}_min") IsNot Nothing Then
				Name &= "_min"
			End If
			Ret = ResourceManager.GetString(Name)
			If Ret Is Nothing Then
				Console.WriteLine($"Cannot find resource file: {Name}")
			End If
		End If
		If Ret > "" Then
			Console.WriteLine($"Script resource loaded from: {Name}")
		Else
			Ret = ""
		End If
		Return Ret
	End Function

	<Extension>
	Function RunSync(Task As Task(Of String)) As String
		Dim W As New WaitTask
		Return W.Wait(Task)
	End Function

	<Extension>
	Sub RunSync(Task As Task)
		Dim W As New WaitTask
		W.Wait(Task)
	End Sub

	<Extension>
	Sub SetResourceManager(CoreWeb As CoreWebView2, Manager As Resources.ResourceManager)
		ResourceManager = Manager
		ResourceManager.IgnoreCase = True
	End Sub

End Module

