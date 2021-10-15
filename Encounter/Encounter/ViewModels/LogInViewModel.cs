using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter.Commands;
using Encounter.Stores;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        public ICommand DoLoginCommand { get; }
        private readonly User _User;

        public string Username
        {
            get => _User.Nickname;
            set => _User.Nickname = value;
        }

    public LogInViewModel(NavigationStore navigationStore)
        {
            _User = new User();
            DoLoginCommand = new LoginCommand(navigationStore, _User);
        }
    }
}
