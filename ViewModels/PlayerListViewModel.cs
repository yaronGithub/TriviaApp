﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class PlayerListViewModel : ViewModelBase
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
        private TriviaWebAPIProxy usersService;
        public PlayerListViewModel(TriviaWebAPIProxy service)//the building function
        {
            this.usersService = service;
            users = new ObservableCollection<User>();
            ReadUsers();
        }
        private async void ReadUsers()//read the users from the server
        {
            TriviaWebAPIProxy service = this.usersService;
            List<User> list = await service.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);
        }
        private Object selectedUser;
        public Object SelectedUser
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
        public ICommand SingleSelectCommand => new Command(OnSingleSelectUser);

        async void OnSingleSelectUser()
        {
            if (SelectedUser != null)
            {
                var navParam = new Dictionary<string, object>()
                {
                    {"selectedUser", SelectedUser }
                };
                await Shell.Current.GoToAsync($"userDetails", navParam);
                SelectedUser = null;
            }
        }
    }
}