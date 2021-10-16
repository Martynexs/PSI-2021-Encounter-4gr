using System;
using Encounter.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter.ViewModels;
using System.Windows;
using Encounter.Models;

namespace Encounter.Commands
{
    class LoginCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private User _user;

        public LoginCommand(NavigationStore navigationStore, User user)
        {
            _navigationStore = navigationStore;
            _user = user;
        }

        public override void Execute(object parameter)
        {
            if (_user.Nickname == null)
            {
                MessageBox.Show("Please Enter login");
                return;
            }

            Session.CreateNewSession(_user);
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
        }
    }
}
