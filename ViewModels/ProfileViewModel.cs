﻿using TriviaAppClean.Models;
using TriviaAppClean.Services;
using System.Text.RegularExpressions;

namespace TriviaAppClean.ViewModels
{
    public class ProfileViewModel:ViewModelBase
    {
            private TriviaWebAPIProxy service;
           User u = ((App)Application.Current).LoggedInUser;
            public ProfileViewModel(TriviaWebAPIProxy service)
            {
                this.service = service;
                this.SaveProfileCommand = new Command(this.SaveProfile);
                this.email = this.u.Email;
                this.name = this.u.Name;
                this.password=this.u.Password;
                 if (this.u.Rank == 2)
                 {
                     this.rank = "Admin";
                 }
                 else if (this.u.Rank == 1) { this.rank = "Master"; } 
                 else { this.rank = "Trainee"; }
            }   


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
            private bool ValidateForm()
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
            private async void SaveProfile()
            {
                if (ValidateForm())
                {
                    User user = new User()
                    {
                        Name = this.Name,
                        Email = this.Email,
                        Password = this.Password
                    };
                    bool success = await service.UpdateUser(user);

                    if (!success)
                    {
                        await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                }
                else
                    await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);

            }
            #region נקודות
        private int points;
        public int Points
        {
            get  => points; 
            //set
            //{ 
            //    points = value;
            //}
        }
        #endregion

            #region דרגה
        private string rank;
        public string Rank
        {
            get => rank;
            //set
            //{
            //   rank = value;
            //}
        }
        #endregion
    }

}
