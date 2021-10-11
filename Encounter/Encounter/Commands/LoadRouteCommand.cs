using Encounter.Stores;
using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands
{
    class LoadRouteCommand : CommandBase
    {
        NavigationStore _navigationStore;

        public LoadRouteCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            var waypoints = Files.Read();
            _navigationStore.CurrentViewModel = new RouteViewModel(_navigationStore, waypoints);
        }
    }
}
