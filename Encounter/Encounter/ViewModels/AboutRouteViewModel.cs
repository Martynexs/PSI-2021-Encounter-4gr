using Encounter.Commands.AboutRoute;
using Encounter.Models;
using Encounter.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class AboutRouteViewModel : ViewModelBase
    {

        RouteViewModel _routeViewModel;
        Route _route;
        User _user;

        public ICommand Close { get; set; }

        public ICommand SaveRoute { get; set; }


        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged(); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private double _rating;

        public double Rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(); }
        }

        private int _userRating;

        public int UserRating
        {
            get { return _userRating; }
            set { _userRating = value; OnPropertyChanged(); }
        }

        private Visibility _visibility;

        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }


        public AboutRouteViewModel(RouteViewModel routeViewModel, Route route)
        {
            _routeViewModel = routeViewModel;
            _route = route;
            _user = Session.GetUser();

            Visibility = Visibility.Hidden;

            Name = _route.Name;
            Description = _route.Description;
            Location = _route.Location;

            Close = new CloseAboutCommand(this);
            SaveRoute = new SaveRouteInfoCommand();
        }


    }
}
