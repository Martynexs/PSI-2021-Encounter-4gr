using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Encounter
{
    public class WaypointView
    {
        private readonly ICommand _selectWaypointCommand;

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                _numberLabel.Content = Index.ToString() + ".";
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                SetInfoLabel();
            }
        }

        private Coordinates _coordinates;
        public Coordinates Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                SetInfoLabel();
            }
        }

        private readonly StackPanel _stackPanel;
        private readonly Ellipse _ellipse;
        private readonly Button _button;
        private readonly Label _numberLabel;
        private readonly Label _infoLabel;

        public WaypointView(ICommand buttonCommand)
        {
            _selectWaypointCommand = buttonCommand;

            _stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Height = 60,
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE6E6E6")
            };
            DockPanel.SetDock(_stackPanel, Dock.Top);

            _numberLabel = new Label
            {
                Width = 50,
                HorizontalContentAlignment = HorizontalAlignment.Right,
                FontSize = 18,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            };
            _stackPanel.Children.Add(_numberLabel);

            _ellipse = new Ellipse
            {
                Height = 50,
                Stroke = Brushes.Black,
                StrokeThickness = 4,
                Width = 52,
                Fill = Brushes.Yellow,
                Cursor = Cursors.Hand,
                StrokeDashCap = PenLineCap.Round
            };

            _button = new Button
            {
                Width = 57,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent
            };
            _button.Content = _ellipse;
            _button.Command = _selectWaypointCommand;
            _stackPanel.Children.Add(_button);

            _infoLabel = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18
            };
            _stackPanel.Children.Add(_infoLabel);
        }

        public StackPanel GetWaypointViewPanel()
        {
            return _stackPanel;
        }
        private void SetInfoLabel()
        {
            _infoLabel.Content = Name + " (" + Coordinates.Latitude.ToString() + ", " + Coordinates.Longitude.ToString() + ")";
        }
    }
}
