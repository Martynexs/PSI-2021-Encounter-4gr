using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Encounter
{
    class VisualWaypoint : IComparable<VisualWaypoint>
    {
        private int Number;
        private StackPanel stackPanel;
        private Ellipse ellipse;
        public Button button;
        private Label numberLabel;
        private Label infoLabel;
        public Waypoint waypoint;

        public VisualWaypoint(Waypoint waypoint)
        {
            this.waypoint = waypoint;
            waypoint.Number = 3;

            Number = waypoint.Number;

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

        public int CompareTo(VisualWaypoint w)
        {
            if (w == null) return 1;
            if (w.Number > this.Number) return -1;
            if (w.Number < this.Number) return 1;
            return 0;
        }
    }
}
