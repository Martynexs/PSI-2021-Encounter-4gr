using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateRouteCommand { get; }
        public HomeViewModel(NavigationStore navigationStore)
        {
            NavigateRouteCommand = new NavigateCommand<RouteViewModel>(navigationStore, () => new RouteViewModel(navigationStore));
        }

    }
}
