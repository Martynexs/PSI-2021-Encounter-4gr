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
    public class VisualWaypoint
    {
        private int _Index;
        public int Index
        {
            get => _Index;
            set { _Index = value; numberLabel.Content = Index.ToString(); button.Tag = value; }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set { _Name = value; SetInfoLabel(); }
        }

        private (double, double) _Coordinates;
        public (double, double) Coordinates
        {
            get => _Coordinates;
            set { _Coordinates = value; SetInfoLabel(); }
        }

        private void SetInfoLabel()
        {
            infoLabel.Content = Name + " (" + Coordinates.Item1.ToString() + ", " + Coordinates.Item2.ToString() + ")";
        }

        private StackPanel stackPanel;
        private Ellipse ellipse;
        public Button button;
        private Label numberLabel;
        private Label infoLabel;

        public VisualWaypoint()
        {
            stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Height = 60;
            stackPanel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE6E6E6");

            numberLabel = new Label
            {
                Width = 50,
                HorizontalContentAlignment = HorizontalAlignment.Right,
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

            button = new Button
            {
                Width = 57,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            };
            button.Tag = _Index;
            button.Content = ellipse;
            stackPanel.Children.Add(button);

            infoLabel = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18
            };
            stackPanel.Children.Add(infoLabel);
        }

        public StackPanel GetVisualWaypointPanel()
        {
            return stackPanel;
        }
    }
}
