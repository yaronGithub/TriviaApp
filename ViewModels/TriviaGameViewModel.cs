using Microsoft.Maui.Controls;
using System.Runtime.Intrinsics.X86;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class TriviaGameViewModel : ViewModelBase
    {
        private ProfileView profileView;
        private TriviaWebAPIProxy service; // For communicating with the server
        public TriviaGameViewModel(TriviaWebAPIProxy service, ProfileView profileView)
        {
            this.service = service; // Initializing
            this.CorrectCommand = new Command(this.IfCorrect); // Initializing
            this.WrongCommand = new Command<string>(this.IfWrong); // Initializing
            this.NextCommand = new Command(this.IfNextAsync); // Initializing
            this.QuitCommand = new Command(this.IfQuit); // Initializing
            InitQues();
            CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default Color
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default Color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default Color
            W3Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default Color
            Enabled = true;
            Visible = false;
            this.profileView = profileView;
        }
        private async void InitQues()
        {
            /* This method is called by the constructor in order to initialize the first question */
            AmericanQuestion amq = await service.GetRandomQuestion(); // A question from the server
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
        private string dialog; // Stores the msg for the player (wromg or correct)
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
        private Color correctColor; // Color of the correct answer button
        public Color CorrectColor
        {
            get => correctColor;
            set
            {
                correctColor = value;
                OnPropertyChanged("CorrectColor");
            }
        }
        private Color w1Color; // Color of Wrong1
        public Color W1Color
        {
            get => w1Color;
            set
            {
                w1Color = value;
                OnPropertyChanged("W1Color");
            }
        }
        private Color w2Color; // Color of Wrong2
        public Color W2Color
        {
            get => w2Color;
            set
            {
                w2Color = value;
                OnPropertyChanged("W2Color");
            }
        }
        private Color w3Color; // Color of Wrong3
        public Color W3Color
        {
            get => w3Color;
            set
            {
                w3Color = value;
                OnPropertyChanged("W3Color");
            }
        }
        private bool enabled; // Is the button enabled or not
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }
        private bool visible; // Is the button visible or not
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
            /* 
             * If the player clicked the button of the correct answer than this method is called.
             * This method updates the dialog for the player. Additionally it updates the score and sets the color of the correct answer to be green.
             */
            User u = ((App)Application.Current).LoggedInUser;
            u.Score += 100;
            Dialog = "Correct Answer!";
            DialogColor = Colors.Green; // Text Color of the dialog is set up to green
            CorrectColor = Colors.Green; // Sets the color of the correct answer to be green
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            Enabled = false; // the buttons are disabled to prevent confusion.
            Visible = true; // The 'Next' button is now visible so that the player can continue his game.
        }
        public ICommand WrongCommand { get; }
        public void IfWrong(string w)
        {
            /*
             * If the player clicked a wrong answer this method is called.
             * This method updates the dialog for the player. Additionally it updates the score and sets the color of the correct answer to be green.
             * Further more, the color of the clicked button is changed to red.
             */
            //User u = ((App)Application.Current).LoggedInUser;
            Dialog = "Wrong Answer!";
            DialogColor = Colors.Red;
            CorrectColor = Colors.Green;
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            if (w == "w1") { W1Color = Colors.Red; } // The color of the clicked button is changed to red.
            else if (w == "w2") { W2Color = Colors.Red; } // The color of the clicked button is changed to red.
            else if (w == "w3") { W3Color = Colors.Red; } // The color of the clicked button is changed to red.
            Enabled = false; // the buttons are disabled to prevent confusion.
            Visible = true; // The 'Next' button is now visible so that the player can continue his game.
        }
        public Command NextCommand {  protected set; get; }
        public async void IfNextAsync()
        {
            /*
             * If the Next button is clicked than this method is called.
             * This method updates the question to a new one.
             * */
            Visible = false;
            Dialog = "";
            AmericanQuestion amq = await service.GetRandomQuestion(); // A new Question from the server.
            QuestionContent = amq.QText;
            CorrectAnswer = amq.CorrectAnswer;
            WrongAnswer1 = amq.Bad1;
            WrongAnswer2 = amq.Bad2;
            WrongAnswer3 = amq.Bad3;
            W1Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W2Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            W3Color = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            CorrectColor = Color.FromRgba(0.31764707, 0.16862746, 0.83137256, 1); // Default color
            Enabled = true; // The buttons are now enabled
        }

        public Command QuitCommand { protected set; get; }
        public async void IfQuit()
        {
            /*
             * If the quit button is clicked than this method is called.
             * This method navigates to a different page.
             */
            //MessagingCenter.Send(this, "ChangeTab", "Profile");
            await Shell.Current.GoToAsync($"//tabs1/{nameof(ProfileView)}"); // Navigating
        }
    }
}