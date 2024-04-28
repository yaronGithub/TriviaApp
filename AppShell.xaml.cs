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
        //MessagingCenter.Subscribe<ShellViewModel, string>(this, "ChangeTab", (sender, arg) =>
        //{
        //    if (arg.Equals("Profile"))
        //    {
        //        CurrentItem = Items[0]; // Change to the second tab
        //    }
        //    Add more conditions for other tabs if needed
        //});

    }

    private void RegisterRoutes()
	{
        Routing.RegisterRoute("userDetails", typeof(PlayerDetailsView));
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
        Routing.RegisterRoute(nameof(TriviaGameView), typeof(TriviaGameView));
    
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ShellViewModel vm = (ShellViewModel)this.BindingContext;
        vm.RefreshProperties();

    }
}