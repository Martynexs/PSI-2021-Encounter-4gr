using PSI.ViewModels;

namespace PSI.Models
{
    public class VisualAnswer : BaseViewModel
    {
        private string _color;
        private string _ptsEarned;
        private bool _allowSelecting;

        public long Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsMarked { get; set; }

        public string Color {
            get => _color;
            set { _color = value; OnPropertyChanged(); }
        }
        public string PtsEarned
        {
            get => _ptsEarned;
            set { _ptsEarned = value; OnPropertyChanged(); }
        }

        public bool AllowSelecting
        {
            get => _allowSelecting;
            set { _allowSelecting = value; OnPropertyChanged(); }
        }
    }
}
