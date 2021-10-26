using Map3.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Map3.Views
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