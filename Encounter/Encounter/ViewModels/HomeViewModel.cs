using Encounter.Commands;
using Encounter.Stores;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateNewRouteCommand { get; }

        public ICommand NavigateLoadRouteCommand { get; }

        public ICommand CreateNewRouteCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateNewRouteCommand = new NavigateCommand<NewRouteViewModel>(navigationStore, () => new NewRouteViewModel(navigationStore));
            NavigateLoadRouteCommand = new NavigateCommand<LoadRouteViewModel>(navigationStore, () => new LoadRouteViewModel(navigationStore));
        }

    }
}
