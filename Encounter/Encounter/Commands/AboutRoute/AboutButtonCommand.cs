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
        private AboutRouteViewModel _aboutRouteViewModel;

        public AboutButtonCommand(AboutRouteViewModel aboutRouteViewModel)
        {
            _aboutRouteViewModel = aboutRouteViewModel;
        }

        public override void Execute(object parameter)
        {
            _aboutRouteViewModel.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
