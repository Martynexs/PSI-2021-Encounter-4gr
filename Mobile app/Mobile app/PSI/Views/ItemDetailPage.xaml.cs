using PSI.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PSI.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}