using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Encounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private WaypointController _waypointController;
        public MainWindow()
        {
           // _waypointController = new WaypointController();
            InitializeComponent();
        }

        /*
        private void LoadWaypointEditor(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Visible;
            Button button = (Button)sender;
            int waypointID = (int)button.Tag;
            var selectedWaypoint = _waypointController.GetWaypoint(waypointID-1);
            if (selectedWaypoint != null)
            {
                Number.Text = waypointID.ToString();
                IndexBox.Text = waypointID.ToString();
                Name.Text = selectedWaypoint.Name;
                Coordinates.Text = selectedWaypoint.Coordinates.ToString();
                //Type.Text = selectedWaypoint.Type;
                Price.Text = selectedWaypoint.Price.ToString();
                Opening.Text = selectedWaypoint.OpeningHours.ToString();
                Closing.Text = selectedWaypoint.ClosingTime.ToString();
                Description.Text = selectedWaypoint.Description;
            }
        }

        private void ExitWaypointEditor(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Hidden;
        }

        private void CreateNewWaypoint(object sender, RoutedEventArgs e)
        {
            var waypoint = _waypointController.CreateNewWaypoint();
            waypoint.GetWaypointView().Button.Click += LoadWaypointEditor;
            var waypointPanel = waypoint.GetWaypointPanel();

            DockPanel.SetDock(waypointPanel, Dock.Top);
            waypointsPanel.Children.Add(waypointPanel);
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            var name = Name.Text;
            var coordinates = Coordinates.Text;
            var type = Type.Text;
            var price = Price.Text;
            var open = Opening.Text;
            var close = Closing.Text;
            var description = Description.Text;
            var number = int.Parse(Number.Text);
            var index = int.Parse(IndexBox.Text);

            var waypoint = _waypointController.GetWaypoint(index-1);
            waypoint.Name = name;
           // waypoint.Coordinates = coordinates;
           // waypoint.Type = type;
           // waypoint.Price = price;
           // waypoint.OpeningHours = open;
           // waypoint.ClosingTime = close;
            waypoint.Description = description;

            if (number != index)
            {
                var tempPanel = waypointsPanel.Children[index - 1];
                waypointsPanel.Children.RemoveAt(index - 1);
                waypointsPanel.Children.Insert(number - 1, tempPanel);
                _waypointController.ChangeWaypointIndex(index - 1, number - 1);
            }

            editGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(Number.Text);
            _waypointController.RemoveWaypoint(index-1);
            waypointsPanel.Children.RemoveAt(index-1);
            editGrid.Visibility = Visibility.Hidden;
        }
        */
    }
        





}
