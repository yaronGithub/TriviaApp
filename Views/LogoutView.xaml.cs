namespace TriviaAppClean.Views;
using TriviaAppClean.ViewModels;

public partial class LogoutView : ContentPage
{
    public LogoutView(LogoutViewModel vm)
	{
        this.BindingContext = vm;
		InitializeComponent();
	}
}