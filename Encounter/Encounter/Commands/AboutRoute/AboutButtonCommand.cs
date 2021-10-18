using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Encounter.Commands.AboutRoute
{
    class AboutButtonCommand : CommandBase
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
