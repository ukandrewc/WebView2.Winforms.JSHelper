Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Resources

Public Module WebView2JSHelper

	Structure JSHelperData
		''' <summary>
		''' Relative path to the script from the base or project folder
		''' </summary>
		Dim ScriptPath As String
		''' <summary>
		''' Base folder for script files (usually project folder)
		''' </summary>
		Dim BaseFolder As String
		''' <summary>
		''' Resource manager for script resources
		''' </summary>
		Dim Resource As ResourceManager
		''' <summary>
		''' Culture to extract resources from
		''' </summary>
		Dim Culture As CultureInfo
	End Structure

	Private ResM As ResourceManager
	Private Data As JSHelperData

	<Extension>
	Function AddScriptResourceToExecuteOnDocumentCreatedAsync(CoreWeb As CoreWebView2, Helper As JSHelperData) As Task(Of String)
		SetHelperData(Helper)
		Return CoreWeb.AddScriptToExecuteOnDocumentCreatedAsync(ResourceScript())
	End Function

	<Extension>
	Function AddScriptResourceToExecuteOnDocumentCreatedAsync(CoreWeb As CoreWebView2, Path As String) As Task(Of String)
		Dim Helper = New JSHelperData
		Helper.ScriptPath = Path
		Return AddScriptResourceToExecuteOnDocumentCreatedAsync(CoreWeb, Helper)
	End Function

	<Extension>
	Function ExecuteScriptResourceAsync(CoreWeb As CoreWebView2, HelperData As JSHelperData) As Task(Of String)
		SetHelperData(HelperData)
		Return CoreWeb.ExecuteScriptAsync(ResourceScript())
	End Function

	<Extension>
	Function ExecuteScriptResourceAsync(CoreWeb As CoreWebView2, Path As String) As Task(Of String)
		Dim Helper = New JSHelperData
		Helper.ScriptPath = Path
		Return ExecuteScriptResourceAsync(CoreWeb, Helper)
	End Function

	<Extension>
	Function ExecuteScriptResourceSync(CoreWeb As CoreWebView2, HelperData As JSHelperData) As String
		SetHelperData(HelperData)
		Return CoreWeb.ExecuteScriptAsync(ResourceScript()).RunSync
	End Function

	<Extension>
	Function ExecuteScriptResourceSync(CoreWeb As CoreWebView2, Path As String) As String
		Dim Helper = New JSHelperData
		Helper.ScriptPath = Path
		Return ExecuteScriptResourceSync(CoreWeb, Helper)
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

	Private Function ResourceScript() As String
		Dim Name = ""
		Dim Ret = ""
		If IsFromFile Then
			Name = IO.Path.Combine(Data.BaseFolder, Data.ScriptPath)
			Try
				Ret = IO.File.ReadAllText(Name)
			Catch ex As Exception
				Console.WriteLine(ex.Message)
			End Try
		Else
			Name = IO.Path.GetFileNameWithoutExtension(Data.ScriptPath)
			If Data.Resource.GetString($"{Name}_min", Data.Culture) IsNot Nothing Then
				Name &= "_min"
			End If
			Ret = Data.Resource.GetString(Name, Data.Culture)
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

	Private Sub SetHelperData(Helper As JSHelperData)
		If Helper.BaseFolder = "" Then
			Dim Info = New IO.DirectoryInfo(My.Application.Info.DirectoryPath)
			While Info.Name <> "bin"
				Info = Info.Parent
			End While
			Data.BaseFolder = Info.Parent.FullName
		Else
			Data.BaseFolder = Helper.BaseFolder
		End If

		If Helper.Resource Is Nothing Then
			Data.Resource = ResM
		Else
			Data.Resource = Helper.Resource
		End If
		Data.Resource.IgnoreCase = True

		If Data.Culture Is Nothing Then
			Data.Culture = CultureInfo.CurrentCulture
		Else
			Data.Culture = Helper.Culture
		End If

		Data.ScriptPath = Helper.ScriptPath
	End Sub

	<Extension>
	Sub SetResourceManager(CoreWeb As CoreWebView2, Manager As ResourceManager)
		ResM = Manager
	End Sub

End Module
