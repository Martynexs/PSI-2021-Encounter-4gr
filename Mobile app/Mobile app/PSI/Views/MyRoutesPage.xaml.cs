using PSI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRoutesPage : ContentPage
    {
        MyRoutesViewModel _viewModel;
        public MyRoutesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MyRoutesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}