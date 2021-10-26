using Encounter.ViewModels;

namespace Encounter.Commands.AboutRoute
{
    public class AboutButtonCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        private AboutRouteViewModel _aboutRouteViewModel;

        public AboutButtonCommand(AboutRouteViewModel aboutRouteViewModel, RouteViewModel routeViewModel)
        {
            _routeViewModel = routeViewModel;
            _aboutRouteViewModel = aboutRouteViewModel;
        }

        public override void Execute(object parameter)
        {
            _aboutRouteViewModel.Visibility = System.Windows.Visibility.Visible;
            _routeViewModel.HideEditor();
        }
    }
}
