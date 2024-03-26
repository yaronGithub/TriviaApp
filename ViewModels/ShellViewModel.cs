using TriviaAppClean.Models;
namespace TriviaAppClean.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
        }
        public bool IsAdmin
        {
            get
            {
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
                User currentUser = ((App)Application.Current).LoggedInUser;
                if (currentUser != null || currentUser.Rank ==1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
