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
            Waypoint waypoint = (Waypoint)button.Tag;
            var nr = waypoint.Number;
            //button.Content = nr.ToString();
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
   
    }






}
