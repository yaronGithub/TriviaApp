using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private AppShell shell;
        private SignUpView signUpView;
        public LoginViewModel(TriviaWebAPIProxy service, AppShell shell, SignUpView signUpView) 
        {
            this.shell = shell;
            InServerCall = false;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.GoToSignUpCommand =  new Command(GoSignUp);
            this.signUpView = signUpView;
        }

        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            if (ShowEmailError || showPasswordError)
            {
                //await Shell.Current.DisplayAlert("Validation", "you have some errors!", "ok");
                if (ShowEmailError)
                {
                    EmailError = "email wrong";
                }
                if (ShowPasswordError)
                {
                    PasswordError = "Password error";
                }
                return;
            }
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall=true;
            //await Shell.Current.GoToAsync("connectingToServer");
            User u  = await this.triviaService.LoginAsync(Email, Password);
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                
                await Shell.Current.DisplayAlert("Login", "Login Failed!", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Login", $"Login Succeed!", "ok");
                Application.Current.MainPage = shell;
            }
        }

        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
        // ---------------------------------------------------------------------------------------------
        private bool showEmailError;
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }
        private void ValidateEmail()
        {
            this.ShowEmailError = (string.IsNullOrEmpty(Email) || Email.Length < 5);
        }
        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()
        {
            this.ShowPasswordError = (string.IsNullOrEmpty(Password) || Password.Length < 4);
        }
        public ICommand GoToSignUpCommand { get; set; } 
        async void GoSignUp()
        {
            await Application.Current.MainPage.Navigation.PushAsync(signUpView);
            //await Shell.Current.GoToAsync("//Views/SignUpView");
        }
    }
}
