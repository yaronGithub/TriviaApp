using Kotlin.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using static Java.Util.Jar.Attributes;

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy service;
        public AddQuestionViewModel(TriviaWebAPIProxy service)
        {
            this.service = service;
            this.SaveQuestionCommand = new Command(this.SaveQuestion);
        }

        #region תוכן שאלה
        private bool showQuestionContentError;

        public bool ShowQuestionContentError
        {
            get => showQuestionContentError;
            set
            {
                showQuestionContentError = value;
                OnPropertyChanged("ShowQuestionContentError");
            }
        }

        private string questionContent;

        public string QuestionContent
        {
            get => questionContent;
            set
            {
                questionContent = value;
                ValidateQuestionContent();
                OnPropertyChanged("QuestionContent");
            }
        }

        private string questionContentError;

        public string QuestionContentError
        {
            get => questionContentError;
            set
            {
                questionContentError = value;
                OnPropertyChanged("QuestionContentError");
            }
        }

        private void ValidateQuestionContent()
        {
            if (this.questionContent.Length < 10)
            {
                this.ShowQuestionContentError = true;
            }
            this.ShowQuestionContentError = false;
        }
        #endregion

        #region תשובה נכונה
        private bool showCorrectAnswerError;

        public bool ShowCorrectAnswerError
        {
            get => showCorrectAnswerError;
            set
            {
                showCorrectAnswerError = value;
                OnPropertyChanged("ShowCorrectAnswerError");
            }
        }

        private string correctAnswer;

        public string CorrectAnswer
        {
            get => correctAnswer;
            set
            {
                questionContent = value;
                ValidateCorrectAnswer();
                OnPropertyChanged("CorrectAnswer");
            }
        }

        private string correctAnswerError;

        public string CorrectAnswerError
        {
            get => correctAnswerError;
            set
            {
                correctAnswerError = value;
                OnPropertyChanged("CorrectAnswerError");
            }
        }

        private void ValidateCorrectAnswer()
        {
            this.ShowCorrectAnswerError = string.IsNullOrEmpty(CorrectAnswer);
        }
        #endregion

        #region תשובה לא נכונה 1
        private bool showWrongAnswer1Error;

        public bool ShowWrongAnswer1Error
        {
            get => showWrongAnswer1Error;
            set
            {
                showWrongAnswer1Error = value;
                OnPropertyChanged("ShowWrongAnswer1Error");
            }
        }

        private string wrongAnswer1;

        public string WrongAnswer1
        {
            get => wrongAnswer1;
            set
            {
                wrongAnswer1 = value;
                ValidateWrongAnswer1();
                OnPropertyChanged("WrongAnswer1");
            }
        }

        private string wrongAnswer1Error;

        public string WrongAnswer1Error
        {
            get => wrongAnswer1Error;
            set
            {
                wrongAnswer1Error = value;
                OnPropertyChanged("WrongAnswer1Error");
            }
        }

        private void ValidateWrongAnswer1()
        {
            this.ShowWrongAnswer1Error = string.IsNullOrEmpty(WrongAnswer1);
        }
        #endregion


        #region תשובה לא נכונה 2
        private bool showWrongAnswer2Error;

        public bool ShowWrongAnswer2Error
        {
            get => showWrongAnswer2Error;
            set
            {
                showWrongAnswer2Error = value;
                OnPropertyChanged("ShowWrongAnswer2Error");
            }
        }

        private string wrongAnswer2;

        public string WrongAnswer2
        {
            get => wrongAnswer2;
            set
            {
                wrongAnswer2 = value;
                ValidateWrongAnswer2();
                OnPropertyChanged("WrongAnswer2");
            }
        }

        private string wrongAnswer2Error;

        public string WrongAnswer2Error
        {
            get => wrongAnswer2Error;
            set
            {
                wrongAnswer2Error = value;
                OnPropertyChanged("WrongAnswer1Error");
            }
        }

        private void ValidateWrongAnswer2()
        {
            this.ShowWrongAnswer2Error = string.IsNullOrEmpty(WrongAnswer2);
        }
        #endregion


        #region תשובה לא נכונה 3
        private bool showWrongAnswer3Error;

        public bool ShowWrongAnswer3Error
        {
            get => showWrongAnswer3Error;
            set
            {
                showWrongAnswer3Error = value;
                OnPropertyChanged("ShowWrongAnswer3Error");
            }
        }

        private string wrongAnswer3;

        public string WrongAnswer3
        {
            get => wrongAnswer3;
            set
            {
                wrongAnswer3 = value;
                ValidateWrongAnswer3();
                OnPropertyChanged("WrongAnswer3");
            }
        }

        private string wrongAnswer3Error;

        public string WrongAnswer3Error
        {
            get => wrongAnswer3Error;
            set
            {
                wrongAnswer3Error = value;
                OnPropertyChanged("WrongAnswer3Error");
            }
        }

        private void ValidateWrongAnswer3()
        {
            this.ShowWrongAnswer3Error = string.IsNullOrEmpty(WrongAnswer3);
        }
        #endregion

        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateQuestionContent();
            ValidateCorrectAnswer();
            ValidateWrongAnswer1();
            ValidateWrongAnswer2();
            ValidateWrongAnswer3();

            //check if any validation failed
            if (ShowQuestionContentError || ShowCorrectAnswerError || ShowWrongAnswer1Error || ShowWrongAnswer2Error || ShowWrongAnswer3Error)
                return false;
            return true;
        }
        public Command SaveQuestionCommand { protected set; get; }
        private async void SaveQuestion()
        {
            if (ValidateForm())
            {
                User u = ((App)Application.Current).LoggedInUser;
                AmericanQuestion q = new AmericanQuestion()
                {
                    QText = this.QuestionContent,
                    CorrectAnswer = this.CorrectAnswer,
                    Bad1 = this.WrongAnswer1,
                    Bad2 = this.WrongAnswer2,
                    Bad3 = this.WrongAnswer3,
                    Status = 0,
                    UserId=u.Id
                };
                bool success = await service.PostNewQuestion(q);

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
    }
}


