//using Kotlin.Reflect;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;
//using static Java.Util.Jar.Attributes;

namespace TriviaAppClean.ViewModels
{
    public class AddQuestionViewModel:ViewModelBase
    {
        private string ifIncorrect;
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

        private bool showifIncorrect;
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

        private bool isPossible;//is possible to add the question
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

        private bool IsAccessApprove()//do the user can have acces to add question
        {
            if (((App)Application.Current).LoggedInUser.Rank == 2)
            {
                return false;
            }
            else if (((App)Application.Current).LoggedInUser.Questions.Count < ((App)Application.Current).LoggedInUser.Score / 100)
            {
                return false;

            }
            else
            {
                return true;

            }
        }


        private string questionContent;
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

        private string correctAnswer;
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

        private string badAnswer1;
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

        private string badAnswer2;
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

        private string badAnswer3;
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
        public AddQuestionViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.IfIncorrect = "Only mangers can add new questions and for every 100 points you are earning you cant addd a new question !";
            this.ShowIfIncorrect = IsAccessApprove();
            this.IsPossible = IsAccessApprove();
            this.CompleteAddingCommand = new Command(OnAddQuestion);
        }

        public ICommand CompleteAddingCommand { get; set; }//Adding the new question to the server

        private async void OnAddQuestion()
        {
            AmericanQuestion quest = new AmericanQuestion();
            quest.QText = questionContent;
            quest.CorrectAnswer = correctAnswer;
            quest.Bad1 = badAnswer1;
            quest.Bad2 = badAnswer2;
            quest.Bad3 = badAnswer3;
            quest.UserId = ((App)Application.Current).LoggedInUser.Id;
            quest.Status = 0;
            await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
            bool a = await this.triviaService.PostNewQuestion(quest);
            await Shell.Current.Navigation.PopModalAsync();

            if (a == true)
            {
                await Shell.Current.DisplayAlert("Add Qustion", "Question  add to the game was successfull!", "ok");
                this.ShowIfIncorrect = IsAccessApprove();
                this.isPossible = IsAccessApprove();
                this.QuestionContent = "";
                this.CorrectAnswer = "";
                this.BadAnswer1 = "";
                this.BadAnswer2 = "";
                this.BadAnswer3 = "";
            }
            else
            {
                await Shell.Current.DisplayAlert("Add Qustion", "Question add failed", "ok");
            }

        }

    }
}


