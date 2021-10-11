using Encounter.Stores;
using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands
{
    class LoadRouteCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private RouteViewModel _routeViewModel;
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public LoadRouteCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel();
            Files.Read(_routeViewModel);
        }
    }
}
