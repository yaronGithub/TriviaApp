﻿using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class TriviaGameViewModel : ViewModelBase
    {
        private TriviaWebAPIProxy service;
        public TriviaGameViewModel(TriviaWebAPIProxy service)
        {
            this.service = service;
            this.CorrectCommand = new Command(this.IfCorrectAsync);
            this.WrongCommand = new Command(this.IfWrongAsync);
            InitQues();
            //this.SaveQuestionCommand = new Command(this.SaveQuestion);
        }
        private async void InitQues()
        {
            AmericanQuestion amq = await service.GetRandomQuestion();
            QuestionContent = amq.QText;
            CorrectAnswer = amq.CorrectAnswer;
            WrongAnswer1 = amq.Bad1;
            WrongAnswer2 = amq.Bad2;
            WrongAnswer3 = amq.Bad3;
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
                correctAnswer = value;
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
                OnPropertyChanged("Dialog");
            }
        }
        private Color dialogColor;
        public Color DialogColor
        {
            get => dialogColor;
            set
            {
                dialogColor = value;
                OnPropertyChanged("DialogColor");
            }
        }
        public Command SaveQuestionCommand { protected set; get; }
        public Command CorrectCommand {  protected set; get; }
        public async void IfCorrectAsync()
        {
            User u = ((App)Application.Current).LoggedInUser;
            u.Score += 100;
            Dialog = "Correct Answer!";
            DialogColor = Colors.Green;
            AmericanQuestion amq = await service.GetRandomQuestion();
            QuestionContent = amq.QText;
            CorrectAnswer = amq.CorrectAnswer;
            WrongAnswer1 = amq.Bad1;
            WrongAnswer2 = amq.Bad2;
            WrongAnswer3 = amq.Bad3;
            Dialog = "";
        }
        public Command WrongCommand { protected set; get; }
        public async void IfWrongAsync()
        {
            //User u = ((App)Application.Current).LoggedInUser;
            Dialog = "Wrong Answer!";
            DialogColor = Colors.Red;
            AmericanQuestion amq = await service.GetRandomQuestion();
            QuestionContent = amq.QText;
            CorrectAnswer = amq.CorrectAnswer;
            WrongAnswer1 = amq.Bad1;
            WrongAnswer2 = amq.Bad2;
            WrongAnswer3 = amq.Bad3;
            Dialog = "";
        }
    }
}