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
        private void SortUsersByScoreDescending(List<User> userList)
        {
            userList.Sort((user1, user2) => user2.Score.CompareTo(user1.Score));
        }
        private TriviaWebAPIProxy usersService;
        public HighScoresViewModel(TriviaWebAPIProxy service)
        {
            this.usersService = service;
            users = new ObservableCollection<User>();
            list = new List<User>();
            ReadUsers();
        }

        private List<User> list;
        private async void ReadUsers()
        {
            TriviaWebAPIProxy service = this.usersService;
            list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);
            FilterUsers();
        }

        private void FilterUsers()
        {
            SortUsersByScoreDescending(list);
            if (UserName == null || String.IsNullOrEmpty(UserName))
            {
                this.Users = new ObservableCollection<User>(list);
                return;
            }

            this.Users = new ObservableCollection<User>();
            foreach (User u in Users)
            {
                if (u.Name.Contains(UserName))
                {
                    Users.Add(u);
                }
            }
            Users = Users;
        }
        //private Object selectedUser;
        //public Object SelectedUser
        //{
        //    get
        //    {
        //        return selectedUser;
        //    }
        //    set
        //    {
        //        this.selectedUser = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);

        //async void OnSingleSelectUser()
        //{
        //    if (SelectedUser != null)
        //    {
        //        var navParam = new Dictionary<string, object>()
        //        {
        //            {"selectedUser", SelectedUser }
        //        };
        //        await Shell.Current.GoToAsync($"userDetails", navParam);
        //        SelectedUser = null;
        //    }
        //}
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
