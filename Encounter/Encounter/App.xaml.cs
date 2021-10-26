using System.Windows;
using Encounter.IO;
using Encounter.Stores;
using Encounter.ViewModels;

namespace Encounter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new LogInViewModel(navigationStore);

            DatabaseIO.OpenConnection();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DatabaseIO.CloseConnection();

            base.OnExit(e);
        }
    }
}
