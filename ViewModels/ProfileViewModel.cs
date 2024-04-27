using TriviaAppClean.Models;
using TriviaAppClean.Services;
using System.Text.RegularExpressions;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy service;

        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            User u = ((App)Application.Current).LoggedInUser;//getting the current user using the game
            //fill the correct filds
            this.service = service;
            this.SaveProfileCommand = new Command(this.SaveProfile);
            Email = u.Email;
            Name = u.Name;
            Password = u.Password;
            Score = u.Score;
            List<Rank> ranks = service.GetRanks();//getting the user rank list
            Rank = ranks.Where(r => r.Id == u.Rank).First().Name;//getting the user rank from the list
            this.NameError = "זהו שדה חובה";
            this.PasswordError = "זהו שדה חובה";
            this.EmailError = "זהו שדה חובה";

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

                this.ShowPasswordError = string.IsNullOrEmpty(Password) || this.password.Length < 4;
            }

            #endregion
            private bool ValidateForm()//validate the form
            {
                //Validate all fields first
                ValidateEmail();
                ValidatePassword();
                ValidateName();

                //check if any validation failed
                if (ShowEmailError || ShowNameError || ShowPasswordError)
                    return false;
                return true;
            }
            public Command SaveProfileCommand { protected set; get; }
            private async void SaveProfile()//saves the changes
            {
                if (ValidateForm())//if all filds are not null
                {
                    User user = new User()//set the user filds
                    {
                        Name = this.Name,
                        Email = this.Email,
                        Password = this.Password
                    };
                await Shell.Current.GoToAsync("connectingToServer");

                    bool success = await service.UpdateUser(user);//true if the user set false else

                await Shell.Current.Navigation.PopModalAsync();
                    if (!success)//if the user set faild
                    {
                        await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);//shows a message
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                }
                else//if there are problem with one or more of the filds
                    await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);//shows a message

            }
        //all points properties
            #region נקודות
        private int score;
        public int Score
        {
            get  => score;
            set
            {
                score = value;
            }
        }
        #endregion
        //all rank properties
        #region דרגה
        private string rank;
        public string Rank
        {
            get => rank;
            set
            {
                rank = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }

}
