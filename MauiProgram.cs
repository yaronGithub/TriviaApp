using Microsoft.Extensions.Logging;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;
using TriviaAppClean.Models;

namespace TriviaAppClean;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<SignUpView>();
        builder.Services.AddTransient<AddQuestionView>();
        //builder.Services.AddTransient<EditQuestionView>(AmericanQuestion q);
        builder.Services.AddTransient<TriviaGameView>();
        builder.Services.AddTransient<PlayerListView>();
        builder.Services.AddTransient<QuestionView>();
        builder.Services.AddTransient<QuestionApprovalView>();
        builder.Services.AddTransient<ProfileView>();
        builder.Services.AddTransient<AllQuestionsView>();
        builder.Services.AddTransient<HighScoresView>();
        builder.Services.AddTransient<PlayerDetailsView>();
        builder.Services.AddTransient<ConnectingToServerView>();
        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<ShellViewModel>();
        builder.Services.AddTransient<AddQuestionViewModel>();
        builder.Services.AddTransient<EditQuestionViewModel>();
        builder.Services.AddTransient<TriviaGameViewModel>();
        builder.Services.AddTransient<PlayerListViewModel>();
        builder.Services.AddTransient<PlayerDetailsViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<QuestionViewModel>();
        builder.Services.AddTransient<QuestionApprovalViewModel>();
        builder.Services.AddTransient<AllQuestionsViewModel>();
        builder.Services.AddTransient<HighScoresViewModel>();
        return builder;
    }
}
