using TriviaAppClean.Models;
namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(TheQuestion), "TheQuestion")]
    public class EditQuestionViewModel : ViewModelBase
    {
        private AmericanQuestion theQuestion;
        public AmericanQuestion TheQuestion
        {
            get
            {
                return this.theQuestion;
            }
            set
            {
                this.theQuestion = value;
                OnPropertyChanged();
            }
        }

    }
}
