# WebView2.Winforms.JSHelper
### Extensions to allow easier coding and debugging of ExecuteScriptAsync in WebView2

The inspiration was to try and get as close as possible to inline editing of javascript for executing in WebView2.

The library allow you to execute scripts from file in while in debug, and as a resource in release.
When in release it will execute a minfied version from resources, if there is one.

In debug you can then edit your scripts in a decent editor instead of strings or resources, and without having to recompile to update the script.

The library also include a RunSync function and method, as an extension of Task to run tasks synchronously.

# Usage

* Add your .js scripts to a folder in your project.
* Add the same file to your project as resource.

If you want to minify your script, then add the file with __\_min__ appended to the resource name.

![image](https://user-images.githubusercontent.com/30246320/117533128-ef77af00-afe2-11eb-9b6e-9206b894d668.png)

Execute your script using the __ExecuteScriptResourceAsync__ or __ExecuteScriptResourceSync__

e.g.

__Res = Web.CoreWebView2.ExecuteScriptResourceSync(_relative\_path_)__

```
Private Sub TestFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
	Web.EnsureCoreWebView2Async.RunSync
	Web.CoreWebView2.SetResourceManager(My.Resources.ResourceManager)
	Web.CoreWebView2.Navigate("https://w3.org/WAI/UA/2002/06/thead-test")
End Sub

Private Sub Web_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles Web.NavigationCompleted
	Web.CoreWebView2.ExecuteScriptResourceSync("Script\test.js")
End Sub
```
When in debug, this will load the script from the named __relative_path__ in release it will load it from the resource with the same name or minified version, if there is one.

# Debugging
When the DevTools window is open, adding the command __debugger__, to you script will pause the script at that line. You can then inspect and set other breakpoints in your script.
