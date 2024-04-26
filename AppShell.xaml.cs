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
        Routing.RegisterRoute("userDetails", typeof(PlayerDetailsView));
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ShellViewModel vm = (ShellViewModel)this.BindingContext;
        vm.RefreshProperties();

    }
}