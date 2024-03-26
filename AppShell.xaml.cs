using TriviaAppClean.Views;
using TriviaAppClean.Models;
using TriviaAppClean.ViewModels;

namespace TriviaAppClean;

public partial class AppShell : Shell
{
	public AppShell()
	{
		this.BindingContext = new ShellViewModel();
		InitializeComponent();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
    }
	public bool IsAdmin
	{
		get
		{
			User currentUser = ((App)Application.Current).LoggedInUser;
			if(currentUser==null || currentUser.Rank < 2)
			{
				return false;
			}
			return true;
		}
	}
}
