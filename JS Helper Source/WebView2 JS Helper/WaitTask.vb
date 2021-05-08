Imports System.Windows.Threading

Friend Class WaitTask
	Private Frame As New DispatcherFrame

	Function Wait(Task As Task(Of String)) As String
		Wait = ""
		If Task IsNot Nothing Then
			Task.ContinueWith(
				Sub()
					If Task.IsFaulted Then
						Console.WriteLine(Task.Exception.Message)
					Else
						Wait = Task.Result
					End If
					Frame.Continue = False
				End Sub)
			Frame.Continue = True
			Dispatcher.PushFrame(Frame)
		End If
	End Function

	Function Wait(Of T)(Task As Task(Of T)) As T
		If Task IsNot Nothing Then
			Task.ContinueWith(
				Sub()
					If Task.IsFaulted Then
						Console.WriteLine(Task.Exception.Message)
					Else
						Wait = Task.Result
					End If
					Frame.Continue = False
				End Sub)
			Frame.Continue = True
			Dispatcher.PushFrame(Frame)
		End If
	End Function

	Sub Wait(Task As Task)
		If Task IsNot Nothing Then
			Task.ContinueWith(
				Sub()
					If Task.IsFaulted Then
						Console.WriteLine(Task.Exception.Message)
					End If
					Frame.Continue = False
				End Sub)
			Frame.Continue = True
			Dispatcher.PushFrame(Frame)
		End If
	End Sub

End Class