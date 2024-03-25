using System.Collections.ObjectModel;
using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    public class HighScoresViewModel : ViewModelBase
    {
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged();
            }
        }

    }
}
