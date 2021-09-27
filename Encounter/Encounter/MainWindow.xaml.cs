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
        WaypointController WaypointController;
        public MainWindow()
        {
            WaypointController = new WaypointController();
            InitializeComponent();
        }

        private void LoadWaypointEditor(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Visible;
            Button button = (Button)sender;
            int waypointID = (int)button.Tag;
            Waypoint selectedWaypoint = WaypointController.GetWaypoint(waypointID);
            if (selectedWaypoint != null)
            {
                Number.Text = waypointID.ToString();
                IndexBox.Text = waypointID.ToString();
                Name.Text = selectedWaypoint.Name;
                Coordinates.Text = selectedWaypoint.Coordinates;
                Type.Text = selectedWaypoint.Type;
                Price.Text = selectedWaypoint.Price;
                Opening.Text = selectedWaypoint.OpeningHours;
                Closing.Text = selectedWaypoint.ClosingTime;
                Description.Text = selectedWaypoint.Description;
            }
        }

        private void ExitWaypointEditor(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Hidden;
        }

        private void CreateNewWaypoint(object sender, RoutedEventArgs e)
        {
            var waypointID = WaypointController.CreateNewWaypoint();
            var visualWaypoint = WaypointController.GetVisualWaypoint(waypointID);
            var waypointPanel = visualWaypoint.GetVisualWaypointPanel();

            //Add an action to the button click that opens and loads Waypoint editor
            visualWaypoint.button.Click += LoadWaypointEditor;
            DockPanel.SetDock(waypointPanel, Dock.Top);
            waypointsPanel.Children.Add(waypointPanel);
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            var Names = Name.Text;
            var Coordinate = Coordinates.Text;
            var Types = Type.Text;
            var Prices = Price.Text;
            var Open = Opening.Text;
            var Close = Closing.Text;
            var Descriptions = Description.Text;
            var Numbers = Int32.Parse(Number.Text);
            var Index = Int32.Parse(IndexBox.Text);
            WaypointController.UpdateWaypoint(Numbers, Names, Coordinate, Types, Prices, Open, Close, Descriptions, Index);
            editGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            var Index = Int32.Parse(Number.Text);
            WaypointController.DeleteWaypoint(Index);
            waypointsPanel.Children.RemoveAt(Index-1);
            editGrid.Visibility = Visibility.Hidden;
        }
    }






}
