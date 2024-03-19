using TriviaAppClean.Models;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class App : Application
{
	//Use this class to store global application data that should be accessible throughout the entire app!
	public User LoggedInUser { get; set; }
	public App(LoginViewModel loginVM)
	{
		LoggedInUser = null;
		InitializeComponent();

		MainPage = new LoginView(loginVM);
	}
}
