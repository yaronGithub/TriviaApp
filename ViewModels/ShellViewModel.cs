using TriviaAppClean.Models;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            
        }

        
        
        public bool IsAdmin
        {
            get
            {
                //Check if app was already created. otherwise return false;
                if (Application.Current == null)
                    return false;
                User currentUser = ((App)Application.Current).LoggedInUser;
                if (currentUser == null || currentUser.Rank < 2)
                {
                    return false;
                }
                return true;
            }
        }

        public bool IsMaster
        {
            get
            {
                //Check if app was already created. otherwise return false;
                if (Application.Current == null)
                    return false;
                User currentUser = ((App)Application.Current).LoggedInUser;
                if (currentUser != null && currentUser.Rank ==1)
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsTrainee
        {
            get
            {
                //Check if app was already created. otherwise return false;
                if (Application.Current == null)
                    return false;
                User currentUser = ((App)Application.Current).LoggedInUser;
                if (currentUser != null && currentUser.Rank == 0)
                {
                    return true;
                }
                return false;
            }
        }
        public void RefreshProperties()
        {
            OnPropertyChanged("IsAdmin");
            OnPropertyChanged("IsMaster");
            OnPropertyChanged("IsTrainee");
        }
    }
}
