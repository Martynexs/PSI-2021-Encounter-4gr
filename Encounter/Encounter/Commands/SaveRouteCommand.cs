using Encounter.ViewModels;

namespace Encounter.Commands
{
    public class SaveRouteCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        public SaveRouteCommand(RouteViewModel routeViewModel)
        {
            _routeViewModel = routeViewModel;
        }
        public override void Execute(object parameter)
        {
            CsvIO.Write(_routeViewModel);
        }
    }
}
