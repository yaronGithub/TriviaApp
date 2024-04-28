//using Kotlin.Reflect;
using System.Windows.Input;
using System.Xml.Linq;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;
//using static Java.Util.Jar.Attributes;

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel:ViewModelBase
    {
        private string ifIncorrect;//error for incorrect field
        public string IfIncorrect
        {
            get
            {
                return ifIncorrect;
            }
            set
            {
                ifIncorrect = value;
                OnPropertyChanged();
            }
        }

        private bool showifIncorrect;//if needed to show incorrect
        public bool ShowIfIncorrect
        {
            get
            {
                return showifIncorrect;
            }
            set
            {
                showifIncorrect = value;
                OnPropertyChanged();
            }
        }

        private bool isPossible;//is it possible to add the question
        public bool IsPossible
        {
            get
            {
                return isPossible;
            }
            set
            {
                isPossible = value;
                OnPropertyChanged();
            }
        }

        private bool IsAccessApprove()//can the user have access to add question
        {
            if (((App)Application.Current).LoggedInUser.Rank == 2 || ((App)Application.Current).LoggedInUser.Score >= 100)
            {
                return true;
            }
            else
            {
                return false;

            }
        }


        private string questionContent;//question content filled by the user
        public string QuestionContent
        {
            get
            {
                return questionContent;
            }
            set
            {
                questionContent = value;
                OnPropertyChanged();
            }
        }

        private string correctAnswer;//correct answer filled by the user
        public string CorrectAnswer
        {
            get
            {
                return correctAnswer;
            }
            set
            {
                correctAnswer = value;
                OnPropertyChanged();
            }
        }

        private string badAnswer1;//bad answer 1 filled by the user
        public string BadAnswer1
        {
            get
            {
                return badAnswer1;
            }
            set
            {
                badAnswer1 = value;
                OnPropertyChanged();
            }
        }

        private string badAnswer2;//bad answer 2 filled by the user
        public string BadAnswer2
        {
            get
            {
                return badAnswer2;
            }
            set
            {
                badAnswer2 = value;
                OnPropertyChanged();
            }
        }

        private string badAnswer3;//bad answer 3 filled by the user
        public string BadAnswer3
        {
            get
            {
                return badAnswer3;
            }
            set
            {
                badAnswer3 = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        private ConnectingToServerView connectingToServerView;
        public AddQuestionViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)//the building function
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.IfIncorrect = "Must fill every field";
            this.IsPossible = IsAccessApprove();
            this.CompleteAddingCommand = new Command(OnAddQuestion);
        }

        private bool ValidateForm()//making sure that all form fields are full
        {
            //Validate all fields first
           if(string.IsNullOrEmpty(QuestionContent)==false && string.IsNullOrEmpty(CorrectAnswer)==false && string.IsNullOrEmpty(BadAnswer1)==false && string.IsNullOrEmpty(BadAnswer2)==false && string.IsNullOrEmpty(BadAnswer3)==false)
           {
                ShowIfIncorrect = false;
           }
            else
            {
                ShowIfIncorrect = true;
            }

            //check if any validation failed
            if (ShowIfIncorrect)
                return false;
            return true;
        }
        public ICommand CompleteAddingCommand { get; set; }//Adding the new question to the server

        private async void OnAddQuestion()
        {
            if (ValidateForm())//if the function returns true
            {
                AmericanQuestion quest = new AmericanQuestion();//creating a new empty question
                //filling the fields based on what the user enterd
                quest.QText = questionContent;
                quest.CorrectAnswer = correctAnswer;
                quest.Bad1 = badAnswer1;
                quest.Bad2 = badAnswer2;
                quest.Bad3 = badAnswer3;
                quest.UserId = ((App)Application.Current).LoggedInUser.Id;
                if (((App)Application.Current).LoggedInUser.Rank == 2) { quest.Status = 1; }//if the user is manager then the question is automatically approved
                else//if not the question status is waiting for approval
                {
                    quest.Status = 0;
                }
                if (IsAccessApprove())//if the user has access
                {
                    await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
                    bool a = await this.triviaService.PostNewQuestion(quest);
                    await Shell.Current.Navigation.PopModalAsync();

                    if (a == true)//if the question was added
                    {
                        await Shell.Current.DisplayAlert("Add Qustion", "Question add to the game was successfull!", "ok");
                        this.ShowIfIncorrect = IsAccessApprove();
                        this.isPossible = IsAccessApprove();
                        this.QuestionContent = "";
                        this.CorrectAnswer = "";
                        this.BadAnswer1 = "";
                        this.BadAnswer2 = "";
                        this.BadAnswer3 = "";
                    }
                    else//the user does have access but the adding failed
                    {
                        await Shell.Current.DisplayAlert("Add Qustion", "Question add failed", "ok");
                    }
                }
                else//the user doesn't have the rank to add question
                {
                    await Shell.Current.DisplayAlert("Add Qustion", "Cannot add question yet", "ok");
                }
            }
            else//some of the fields are incorrect
            {
                await App.Current.MainPage.DisplayAlert("Add Question", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
            }

        }

    }
}


