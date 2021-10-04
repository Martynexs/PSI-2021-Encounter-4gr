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

namespace Encounter.ViewModels
{
    class WaypointEditorViewModel : ViewModelBase
    {
        private WaypointStore _waypointStore;
        public WaypointViewModel SelectedWaypoint => _waypointStore.SelectedWaypoint;

        public ICommand CloseEditor { get; }

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

        private (double, double) _coordinates;
        public (double, double) Coordinates
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

        public WaypointEditorViewModel(WaypointStore waypointStore)
        {
            _waypointStore = waypointStore;
            EditorVisibility = Visibility.Hidden;
            _waypointStore.SelectedWaypointChanged += OnSelectedWaypointChanged;
            CloseEditor = new CloseEditorCommand(this);
        }

        public void OnSelectedWaypointChanged()
        {
            OnPropertyChnaged(nameof(SelectedWaypoint));
            EditorVisibility = System.Windows.Visibility.Visible;
            var waypoint = SelectedWaypoint;

            Index = waypoint.Index;
            Name = waypoint.Name;
            Type = waypoint.Type;
            Price = waypoint.Price;
            OpeningHours = waypoint.OpeningHours;
            ClosingTime = waypoint.ClosingTime;
            Description = waypoint.Description;
        }
    }
}
