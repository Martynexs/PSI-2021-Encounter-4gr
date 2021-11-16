using DataLibrary;
using PSI.Views;
using Xamarin.Forms;

namespace PSI
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<EncounterProcessor>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
