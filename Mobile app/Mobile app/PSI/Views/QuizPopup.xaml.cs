using PSI.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizPopup : PopupPage
    {
        public QuizPopup()
        {
            InitializeComponent();
            BindingContext = new QuizViewModel();
        }
    }
}