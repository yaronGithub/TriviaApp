﻿using TriviaAppClean.Models;
using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class App : Application
{
	//Use this class to store global application data that should be accessible throughout the entire app!
	public User LoggedInUser { get; set; }
	public App(LoginView login/*TriviaGameView gm*/)
	{
		LoggedInUser = null;
		InitializeComponent();

		this.MainPage = new NavigationPage(login);
		//this.MainPage = new NavigationPage(gm);
	}
	//Application.Current.Navigation.PushAsync(page);
}
