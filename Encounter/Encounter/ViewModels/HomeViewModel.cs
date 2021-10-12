using Encounter.Commands;
using Encounter.Stores;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateRouteCommand { get; }
        public ICommand LoadRouteCommand { get; }

        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateRouteCommand = new NavigateCommand<RouteViewModel>(navigationStore, () => new RouteViewModel(navigationStore));
            LoadRouteCommand = new LoadRouteCommand(navigationStore);
        }

    }
}
