using DataLibrary.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PSI.Models
{
    public class VisualAnswer : QuizAnswer, INotifyPropertyChanged
    {
        private string _color;

        public VisualAnswer()
        {
            Color = "black";
        }

        public bool IsMarked { get; set; }


        public string Color { get => _color;
            set {
                _color = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
