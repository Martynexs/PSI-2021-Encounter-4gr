using PSI.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditQuizPopup : PopupPage
    {
        public EditQuizPopup()
        {
            InitializeComponent();
            BindingContext = new EditQuizViewModel();
        }
    }
}