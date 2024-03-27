using TriviaAppClean.Models;

namespace TriviaAppClean.ViewModels
{
    [QueryProperty(nameof(SelectedUser), "selectedUser")]
    public class PlayerDetailsViewModel : ViewModelBase
    {
        private User selectedUser;
        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                this.selectedUser = value;
                OnPropertyChanged();
            }
        }
    }
}
