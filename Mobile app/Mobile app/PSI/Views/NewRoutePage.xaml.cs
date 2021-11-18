using DataLibrary.Models;
using PSI.ViewModels;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class NewRoutePage : ContentPage
    {
        public Route Item { get; set; }

        public NewRoutePage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}