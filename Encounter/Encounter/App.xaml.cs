using System.Collections.Generic;
using System.Windows;
using Encounter.IO;
using Encounter.Models;
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

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            /*
            var db = new DatabaseFunctions();

            
            //db.DeleteRoute("Example");

            var wp = new Waypoint
            {
                Index = 1,
                Name = "MO muziejus",
                Coordinates = new Coordinates(1.25, 3.25),
                Price = 3.68m
            };

            var rt = new Route
            {
                Name = "Example"
            };

            db.CreateRoute(rt);
            var listas = new List<Waypoint>();
            listas.Add(wp);

            db.SaveRoute(rt, listas);
            */

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
