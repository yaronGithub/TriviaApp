using System.Collections.ObjectModel;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
//using Windows.ApplicationModel.VoiceCommands;

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
        private void SortUsersByScoreDescending(List<User> userList)//sorts the users from the highest to the lowest score
        {
            userList.Sort((user1, user2) => user2.Score.CompareTo(user1.Score));
        }
        private TriviaWebAPIProxy usersService;
        public HighScoresViewModel(TriviaWebAPIProxy service)//the building function
        {
            this.usersService = service;
            users = new ObservableCollection<User>();
            list = new List<User>();
            ReadUsers();
        }

        private List<User> list;
        private async void ReadUsers()//function that reads the users
        {
            TriviaWebAPIProxy service = this.usersService;
            list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);
            FilterUsers();
        }

        private void FilterUsers()//function that filters the users based on their scores and checks if there are usernames that are empty and filters them
        {
            SortUsersByScoreDescending(list);
            if (UserName == null || String.IsNullOrEmpty(UserName))
            {
                this.Users = new ObservableCollection<User>(list);
                return;
            }

            this.Users = new ObservableCollection<User>();
            foreach (User u in list)
            {
                if (u.Name.Contains(UserName))
                {
                    Users.Add(u);//adding the user
                }
            }
            Users = Users;
        }
   
        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                FilterUsers();
                OnPropertyChanged();
            }
        }
    }
}