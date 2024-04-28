using System.Windows.Input;
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
        private ConnectingToServerView connectingToServerView;
        public LoginViewModel(TriviaWebAPIProxy service, AppShell shell, SignUpView signUpView, ConnectingToServerView connectingToServerView) //the building function
        {
            this.shell = shell;
            InServerCall = false;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.GoToSignUpCommand =  new Command(GoSignUp);
            this.signUpView = signUpView;
            this.connectingToServerView = connectingToServerView;
        }
        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            if (ShowEmailError || ShowPasswordError || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))//if theres an error in the fields
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"יש בעיה עם הנתונים", "ok");//shows the message
                return;
            }
            //Blocks the page while indicating a server call
            InServerCall=true;
            //await Shell.Current.GoToAsync("connectingToServer");
            await Application.Current.MainPage.Navigation.PushModalAsync(connectingToServerView);//while connecting to the server shows the user the animation
            User u  = await this.triviaService.LoginAsync(Email, Password);//checks if the user exists, returns null if does not exist
            await Application.Current.MainPage.Navigation.PopModalAsync();
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Login Failed!", "ok");//if the check returned null means that the user doesn't exist, shows a message
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"Login Succeeded!", "ok");//if the check returned not null means that the user exist, shows a message
              
                ShellViewModel shellVM = (ShellViewModel)shell.BindingContext;
                shellVM.RefreshProperties();
                Application.Current.MainPage = shell;
            }
        }

        private bool inServerCall;//if something calls to the server
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

        public bool NotInServerCall//not calls the server
        {
            get
            {
                return !this.InServerCall;
            }
        }
        // ---------------------------------------------------------------------------------------------
        private bool showEmailError;//if needed to show the email error
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }
        private string email;//email filled by the user
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
                ValidateEmail();
            }
        }

        private string emailError;//email error
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }
        private void ValidateEmail()//validate if the email fild is not empty longer than 5 chars and contains @
        {
            this.ShowEmailError = (string.IsNullOrEmpty(Email) || Email.Length < 5 || !Email.Contains('@'));
            EmailError = "Invalid Email";
        }
        private bool showPasswordError;//if needed to show the password error
        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }
        private string password;//password filled by the user
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
                ValidatePassword();
            }
        }
        private string passwordError;//password error
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()//validates if the password field is full and there are more the 4 characters in it
        {
            this.ShowPasswordError = (string.IsNullOrEmpty(Password) || Password.Length < 4);
            PasswordError = "Invalid Password";
        }
        public ICommand GoToSignUpCommand { get; set; } //command for users that didn't sign up yet
        async void GoSignUp()
        {

            await Application.Current.MainPage.Navigation.PushAsync(signUpView);//opens the sign up page
            
        }
    }
}
