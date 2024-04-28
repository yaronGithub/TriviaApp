using System.Text.RegularExpressions;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class SignUpViewModel:ViewModelBase
    {
        private ConnectingToServerView cs;
        // ICommand SignUpCommand = new Command();
        private TriviaWebAPIProxy service;
        public SignUpViewModel(TriviaWebAPIProxy service, ConnectingToServerView cs)//the building function
        {
            this.cs = cs;
            this.service = service;
            this.SaveDataCommand = new Command(this.SaveData);
            this.NameError = "Must enter name";
            this.EmailError = "Must enter correct email";
            this.PasswordError = "Must enter password";
        }

      
        //all name properties
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion

        //all email properties
        #region אימייל
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
            string email = this.email;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            this.ShowEmailError = !regex.IsMatch(email);
            
        }
        #endregion
        //all password properties
        #region סיסמה
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
                ValidatePassword();
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
            
            this.ShowPasswordError = string.IsNullOrEmpty(Password) || this.password.Length <4;
        }

        #endregion
        private bool ValidateForm()//validates the form
        {
            //Validate all fields first
            ValidateEmail();
            ValidatePassword();
            ValidateName();

            //checks if any validation failed
            if (ShowEmailError || ShowNameError || ShowPasswordError)
                return false;
            return true;
        }
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()//saves the new user
        {
            if (ValidateForm())//if all fields are not null and don't have any problems
            {
                User user = new User()//creates a new user
                {
                    Name = this.Name,
                    Email = this.Email,
                    Password = this.Password
                };
                Application.Current.MainPage.Navigation.PushModalAsync(cs);
                bool success = await service.RegisterUser(user);//checks if the registration was successful or not
                Application.Current.MainPage.Navigation.PopModalAsync();
                if (!success)//if the adding wasnt successfull
                {
                    await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);//shows the message if there is a problem with the fields
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            else//if there are filds that are null or not ok
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);//shows the message if the registration was successful

        }
    }
}
