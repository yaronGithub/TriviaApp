namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}