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

        Editor AddwayPoint = new Editor();

        private void LoadWaypointEditor(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Visible;
            Button button = (Button)sender;
            Waypoint waypoint = (Waypoint)button.Tag;
            var nr = waypoint.Number;
            button.Content = nr.ToString();
            string str_myobject = button.Content.ToString();
            int int_myobject = int.Parse(str_myobject);
            if (AddwayPoint.list.Count > int_myobject - 1)
            {
                Waypoint rightobject = AddwayPoint.list[int_myobject - 1];
                Number.Text = int_myobject.ToString();
                IndexBox.Text = int_myobject.ToString();
                Name.Text = rightobject.Name;
                Coordinates.Text = rightobject.Coordinates;
                Type.Text = rightobject.Type;
                Price.Text = rightobject.Price;
                Opening.Text = rightobject.OpeningHours;
                Closing.Text = rightobject.ClosingTime;
                Description.Text = rightobject.Description;
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
            AddwayPoint.AddWayPoint();
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
            AddwayPoint.EditWayPoint(Numbers, Names, Coordinate, Types, Prices, Open, Close, Descriptions, Index);
            editGrid.Visibility = Visibility.Hidden;
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            var Index = Int32.Parse(Number.Text);
            AddwayPoint.RemoveWayPoint(Index);
            WaypointController.DeleteWaypoint(Index);
            editGrid.Visibility = Visibility.Hidden;
        }
    }






}
