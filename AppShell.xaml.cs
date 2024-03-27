using TriviaAppClean.Views;
using TriviaAppClean.Models;
using TriviaAppClean.ViewModels;

namespace TriviaAppClean;

public partial class AppShell : Shell
{
	public AppShell(ShellViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }
	
}
