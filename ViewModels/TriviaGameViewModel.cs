using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class TriviaGameViewModel : ViewModelBase
    {
        private TriviaWebAPIProxy service;
        public TriviaGameViewModel(TriviaWebAPIProxy service)
        {
            this.service = service;
            //this.SaveQuestionCommand = new Command(this.SaveQuestion);
        }

        #region תוכן שאלה
       

        private string questionContent;

        public string QuestionContent
        {
            get => questionContent;
            set
            {
                questionContent = value;
                OnPropertyChanged("QuestionContent");
            }
        }
        #endregion

        #region תשובה נכונה
        private string correctAnswer;

        public string CorrectAnswer
        {
            get => correctAnswer;
            set
            {
                questionContent = value;
                OnPropertyChanged("CorrectAnswer");
            }
        }
        #endregion

        #region תשובה לא נכונה 1

        private string wrongAnswer1;

        public string WrongAnswer1
        {
            get => wrongAnswer1;
            set
            {
                wrongAnswer1 = value;
                OnPropertyChanged("WrongAnswer1");
            }
        }
        #endregion


        #region תשובה לא נכונה 2

        private string wrongAnswer2;

        public string WrongAnswer2
        {
            get => wrongAnswer2;
            set
            {
                wrongAnswer2 = value;
                OnPropertyChanged("WrongAnswer2");
            }
        }

        #endregion


        #region תשובה לא נכונה 3
      

        private string wrongAnswer3;

        public string WrongAnswer3
        {
            get => wrongAnswer3;
            set
            {
                wrongAnswer3 = value;
                OnPropertyChanged("WrongAnswer3");
            }
        }
        #endregion
        private string dialog;
        public string Dialog
        {
            get => dialog;
            set
            {
                dialog = value;
                OnPropertyChanged();
            }
        }
        private AmericanQuestion currentQuestion;
        public AmericanQuestion CurrentQuestion
        {
            get => currentQuestion;
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public Command SaveQuestionCommand { protected set; get; }
        public Command CorrectCommand {  protected set; get; }
        public async void IfCorrect()
        {
            User u = ((App)Application.Current).LoggedInUser;
            u.Score += 100;
            Dialog = "Correct Answer!";
            CurrentQuestion = await service.GetRandomQuestion();
            QuestionContent = CurrentQuestion.QText;
            CorrectAnswer = CurrentQuestion.CorrectAnswer;
            WrongAnswer1 = CurrentQuestion.Bad1;
            WrongAnswer2 = CurrentQuestion.Bad2;
            WrongAnswer3 = CurrentQuestion.Bad3;
        }
        public Command WrongCommand { protected set; get; }
        public void IfWrong()
        {
            User u = ((App)Application.Current).LoggedInUser;
            Dialog = "Wrong Answer!";
        }
    }
}