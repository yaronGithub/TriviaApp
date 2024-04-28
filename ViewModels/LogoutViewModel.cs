﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Views;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using System.Windows.Input;

namespace TriviaAppClean.ViewModels
{
    public class LogoutViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private AppShell shell;
        private LoginView loginView;
        private ConnectingToServerView connectingToServerView;


        public LogoutViewModel(TriviaWebAPIProxy service, AppShell shell, LoginView loginView, ConnectingToServerView connectingToServerView) //the building function
        {
                // this.shell = shell;
                //InServerCall = false;
                this.triviaService = service;
                this.GoToLogInCommand = new Command(GoLogIn);
                this.loginView = loginView;
                this.connectingToServerView = connectingToServerView;
        }
          

            //private bool inServerCall;//if something calls to the server
            //public bool InServerCall
            //{
            //    get
            //    {
            //        return this.inServerCall;
            //    }
            //    set
            //    {
            //        this.inServerCall = value;
            //        OnPropertyChanged("NotInServerCall");
            //        OnPropertyChanged("InServerCall");
            //    }
            //}

            //public bool NotInServerCall//not calls the server
            //{
            //    get
            //    {
            //        return !this.InServerCall;
            //    }
            //}
      
            public ICommand GoToLogInCommand { get; set; } //command for users that want to log out
            async void GoLogIn()
            { 
                bool result = await Application.Current.MainPage.DisplayAlert("Logout", $"Are you sure you want to log out?", "ok", "cancel");//if the check returned not null means that the user exist, shows a message
                if(result)
                {

                    Application.Current.MainPage = new NavigationPage(loginView);

                }
            }
    }
}



