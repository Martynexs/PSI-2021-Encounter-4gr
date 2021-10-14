using Encounter.IO;
using Encounter.Stores;
using Encounter.ViewModels;
using System;

namespace Encounter.Commands.RouteVM
{
    public class DeleteRouteCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        private NavigationStore _navigationStore;

        public DeleteRouteCommand(RouteViewModel routeVM, NavigationStore navigation)
        {
            _routeViewModel = routeVM;
            _navigationStore = navigation;
        }

        public override void Execute(object parameter)
        {
            DatabaseFunctions.DeleteRoute(_routeViewModel.Route);
            _navigationStore.CurrentViewModel = new HomeViewModel(_navigationStore);
        }

        

    }
}
