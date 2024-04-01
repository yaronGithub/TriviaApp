using System.Collections.ObjectModel;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

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
                if (string.IsNullOrEmpty(UserName))
                {
                    List<User> newUsers = this.users.ToList();
                    SortUsersByScoreDescending(newUsers);
                    this.users = new ObservableCollection<User>(newUsers);
                    OnPropertyChanged();
                }
                else
                {
                    List<User> newUsers = new List<User>();
                    foreach (User user in this.users)
                    {
                        if (user.Name == UserName)
                        {
                            newUsers.Add(user);
                        }
                        SortUsersByScoreDescending(newUsers);
                        this.users = new ObservableCollection<User>(newUsers);
                        OnPropertyChanged();
                    }
                }
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
            ReadUsers();
        }
        private async void ReadUsers()
        {
            TriviaWebAPIProxy service = this.usersService;
            List<User> list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);
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
                Users = Users;
                OnPropertyChanged();
            }
        }
    }
}