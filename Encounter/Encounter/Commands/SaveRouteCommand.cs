using Encounter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;


namespace Encounter.Commands
{
    [Serializable]
    class SaveRouteCommand : CommandBase
    {
        private RouteViewModel _routeViewModel;
        public SaveRouteCommand(RouteViewModel routeViewModel)
        {
            _routeViewModel = routeViewModel;
        }
        public override void Execute(object parameter)
        {
            Files.Write(_routeViewModel);
        }
    }
}
