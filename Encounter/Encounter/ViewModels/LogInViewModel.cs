using Encounter.Commands;
using Encounter.Stores;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        public ICommand DoLoginCommand { get; }
        private readonly User _user;

        public string Username
        {
            get => _user.Nickname;
            set => _user.Nickname = value;
        }

        public LogInViewModel(NavigationStore navigationStore)
        {
            _user = new User();
            DoLoginCommand = new LoginCommand(navigationStore, _user);
        }
    }
}
