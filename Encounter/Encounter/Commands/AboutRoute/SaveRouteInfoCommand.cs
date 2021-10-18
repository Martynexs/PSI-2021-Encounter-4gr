using Encounter.Models;
using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter.Commands.AboutRoute
{
    public class SaveRouteInfoCommand : CommandBase
    {
        Route _route;
        AboutRouteViewModel _aboutRouteViewModel;
        public SaveRouteInfoCommand(AboutRouteViewModel aboutRouteViewModel, Route route)
        {
            _route = route;
            _aboutRouteViewModel = aboutRouteViewModel;

        }
        public override void Execute(object parameter)
        {
            _route.Name = _aboutRouteViewModel.Name;
            _route.Description = _aboutRouteViewModel.Description;
            _route.Location = _aboutRouteViewModel.Location;
        }
    }
}
