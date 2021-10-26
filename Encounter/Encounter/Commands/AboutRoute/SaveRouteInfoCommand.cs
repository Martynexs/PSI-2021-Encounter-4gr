using Encounter.Models;
using Encounter.ViewModels;

namespace Encounter.Commands.AboutRoute
{
    public class SaveRouteInfoCommand : CommandBase
    {
        private Route _route;
        private AboutRouteViewModel _aboutRouteViewModel;
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
