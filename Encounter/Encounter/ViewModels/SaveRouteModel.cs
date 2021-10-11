using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    class SaveRouteModel : ViewModelBase
    {
        private RouteViewModel _routeViewModel;
        public ICommand SaveRoute { get; }

        public SaveRouteModel(RouteViewModel routeViewModel)
        {
            _routeViewModel = routeViewModel;
            //SaveRoute = new SaveRouteModel(this);

        }
    }
}
