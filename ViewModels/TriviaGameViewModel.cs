﻿using System.Runtime.Intrinsics.X86;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class TriviaGameViewModel : ViewModelBase
    {
        private ProfileView profileView;
        private TriviaWebAPIProxy service;
        public TriviaGameViewModel(TriviaWebAPIProxy service, ProfileView profileView)
        {
            this.service = service;
            this.CorrectCommand = new Command(this.IfCorrect);
            //this.WrongCommand = new Command(this.IfWrong);
            this.WrongCommand = new Command<string>(this.IfWrong);
            this.NextCommand = new Command(this.IfNextAsync);
            this.QuitCommand = new Command(this.IfQuit);
            InitQues();
            CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W3Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            Enabled = true;
            Visible = false;
            this.profileView = profileView;
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
        private Color correctColor;
        public Color CorrectColor
        {
            get => correctColor;
            set
            {
                correctColor = value;
                OnPropertyChanged("CorrectColor");
            }
        }
        private Color w1Color;
        public Color W1Color
        {
            get => w1Color;
            set
            {
                w1Color = value;
                OnPropertyChanged("W1Color");
            }
        }
        private Color w2Color;
        public Color W2Color
        {
            get => w2Color;
            set
            {
                w2Color = value;
                OnPropertyChanged("W2Color");
            }
        }
        private Color w3Color;
        public Color W3Color
        {
            get => w3Color;
            set
            {
                w3Color = value;
                OnPropertyChanged("W3Color");
            }
        }
        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }
        private bool visible;
        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                OnPropertyChanged("Visible");
            }
        }
        public Command SaveQuestionCommand { protected set; get; }
        public Command CorrectCommand {  protected set; get; }
        public void IfCorrect()
        {
            User u = ((App)Application.Current).LoggedInUser;
            u.Score += 100;
            Dialog = "Correct Answer!";
            DialogColor = Colors.Green;
            CorrectColor = Colors.Green;
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            Enabled = false;
            Visible = true;
        }
        //public Command WrongCommand { protected set; get; }
        //public Command WrongCommand { protected set; get; }
        public ICommand WrongCommand { get; }
        public void IfWrong(string w)
        {
            //User u = ((App)Application.Current).LoggedInUser;
            Dialog = "Wrong Answer!";
            DialogColor = Colors.Red;
            CorrectColor = Colors.Green;
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            if (w == "w1") { W1Color = Colors.Red; }
            else if (w == "w2") { W2Color = Colors.Red; }
            else if (w == "w3") { W3Color = Colors.Red; }
            Enabled = false;
            Visible = true;
        }
        public Command NextCommand {  protected set; get; }
        public async void IfNextAsync()
        {
            Visible = false;
            Dialog = "";
            AmericanQuestion amq = await service.GetRandomQuestion();
            QuestionContent = amq.QText;
            CorrectAnswer = amq.CorrectAnswer;
            WrongAnswer1 = amq.Bad1;
            WrongAnswer2 = amq.Bad2;
            WrongAnswer3 = amq.Bad3;
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            W3Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1);
            Enabled = true;
        }

        public Command QuitCommand { protected set; get; }
        public async void IfQuit()
        {
            await Shell.Current.GoToAsync("ProfileView");
        }
    }
}