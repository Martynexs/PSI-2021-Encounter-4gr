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
        public MainWindow()
        {
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
            VisualWaypoint visualWaypoint = new VisualWaypoint(new Waypoint());
            var waypointPanel = visualWaypoint.getVisualWaypoint();
            visualWaypoint.button.Click += LoadWaypointEditor;
            DockPanel.SetDock(waypointPanel, Dock.Top);
            waypointsPanel.Children.Add(waypointPanel);
        }

        
    }

    class VisualWaypoint
    {
        private StackPanel stackPanel;
        private Ellipse ellipse;
        public  Button button;
        private Label numberLabel;
        private Label infoLabel;
        public  Waypoint waypoint;

        public VisualWaypoint(Waypoint waypoint)
        {
            this.waypoint = waypoint;
            waypoint.Number = 3;

            stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Height = 60;
            stackPanel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE6E6E6");

            var number = waypoint.Number;
            numberLabel = new Label
            {
                Content = number.ToString(),
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            };
            stackPanel.Children.Add(numberLabel);

            ellipse = new Ellipse
            {
                Height = 50, 
                Stroke = Brushes.Black, 
                StrokeThickness = 4, 
                Width = 52, 
                Fill = Brushes.Yellow, 
                Cursor = Cursors.Hand, 
                StrokeDashCap = PenLineCap.Round
            };
            //stackPanel.Children.Add(ellipse);

            button = new Button
            {
                Width = 57,
                Background = Brushes.Transparent, 
                BorderBrush = Brushes.Transparent
            };
            button.Tag = waypoint;
            button.Content = ellipse;
            stackPanel.Children.Add(button);


            string info = waypoint.Name + " (" + waypoint.Coordinates + ") ";
            infoLabel = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18, 
                Width = 175
            };
            infoLabel.Content = info;
            stackPanel.Children.Add(infoLabel);
            
        }

        public StackPanel getVisualWaypoint()
        {
            return stackPanel;
        }

    }






}
