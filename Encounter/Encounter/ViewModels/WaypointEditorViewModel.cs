using Encounter.Commands;
using Encounter.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Encounter.ViewModels
{
    public class WaypointEditorViewModel : ViewModelBase
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public String Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private Coordinates _coordinates;
        public Coordinates Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private DateTime _openingHours;
        public DateTime OpeningHours
        {
            get => _openingHours;
            set
            {
                _openingHours = value;
                OnPropertyChanged();
            }
        }

        private DateTime _closingTime;
        public DateTime ClosingTime
        {
            get => _closingTime;
            set
            {
                _closingTime = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        //Builder
        public WaypointEditorViewModel(WaypointStore waypointStore, RouteViewModel routeViewModel)
        {
            _waypointStore = waypointStore;
            _routeViewModel = routeViewModel;

            EditorVisibility = Visibility.Hidden;
            AllWaypointTypes = WayPointTypeExtensions.GetLabelValueItems();

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

            OnPropertyChanged(nameof(SelectedWaypoint));
        }
    }
}
