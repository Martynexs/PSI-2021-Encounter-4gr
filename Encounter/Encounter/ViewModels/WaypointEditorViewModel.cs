using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Encounter.ViewModels
{
    class WaypointEditorViewModel : ViewModelBase
    {
        private RouteViewModel _routeViewModel;
        private WaypointStore _waypointStore;
        public WaypointViewModel SelectedWaypoint => _waypointStore.SelectedWaypoint;
        public List<LabelValueItem<WaypointType>> AllWaypointTypes { get; }

        public ObservableCollection<int> _indexes;
        public ObservableCollection<int> Indexes
        {
            get => _indexes;
            set
            {
                _indexes = value;
                OnPropertyChnaged();
            }
        }

        //Commands
        public ICommand CloseEditor { get; }
        public ICommand SaveWaypoint { get; }
        public ICommand DeleteWaypoint { get; }

        //Properties
        private Visibility _editorVisibility;
        public Visibility EditorVisibility
        {
            get => _editorVisibility;
            set
            {
                _editorVisibility = value;
                OnPropertyChnaged();
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChnaged();
            }
        }

        private string _name;
        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChnaged();
            }
        }

        private Coordinates _coordinates;
        public Coordinates Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                OnPropertyChnaged();
            }
        }

        private WaypointType _type;
        public WaypointType Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        private LabelValueItem<WaypointType> _typeItem;
        public LabelValueItem<WaypointType> TypeItem
        {
            get => _typeItem;
            set
            {
                Type = value.value;
                _typeItem = value;
                OnPropertyChnaged();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChnaged();
            }
        }

        private DateTime _openingHours;
        public DateTime OpeningHours
        {
            get => _openingHours;
            set
            {
                _openingHours = value;
                OnPropertyChnaged();
            }
        }

        private DateTime _closingTime;
        public DateTime ClosingTime
        {
            get => _closingTime;
            set
            {
                _closingTime = value;
                OnPropertyChnaged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChnaged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChnaged();
            }
        }

        //Builder
        public WaypointEditorViewModel(WaypointStore waypointStore, RouteViewModel routeViewModel)
        {
            _waypointStore = waypointStore;
            _routeViewModel = routeViewModel;

            EditorVisibility = Visibility.Hidden;
            AllWaypointTypes = WayPointTypeExtensions.GetAllTypes();
            _waypointStore.SelectedWaypointChanged += OnSelectedWaypointChanged;

            CloseEditor = new CloseEditorCommand(this);
            SaveWaypoint = new SaveWaypointCommand(routeViewModel, this, waypointStore);
            DeleteWaypoint = new DeleteWaypointCommand(waypointStore, routeViewModel, this);
        }

        //Functions
        public void OnSelectedWaypointChanged()
        {
            EditorVisibility = Visibility.Visible;

            var waypoint = SelectedWaypoint;

            Index = waypoint.Index;
            Name = waypoint.Name;
            Coordinates = waypoint.Coordinates;
            TypeItem = new LabelValueItem<WaypointType>(waypoint.Type.ToString(), waypoint.Type);
            Price = waypoint.Price;
            OpeningHours = waypoint.OpeningHours;
            ClosingTime = waypoint.ClosingTime;
            PhoneNumber= waypoint.PhoneNumber;
            Description = waypoint.Description;

            Indexes = new(Enumerable.Range(1, _routeViewModel.GetWaypointsCount()));

            OnPropertyChnaged(nameof(SelectedWaypoint));
        }

        private bool PhoneNumberMatches ()
        {
            Regex reg = new Regex("^(([+][3][7][0][0-9]{8})|([8][0-9]{8}))$");
            bool result = reg.IsMatch(PhoneNumber);
            return result;
        }
    }
}
