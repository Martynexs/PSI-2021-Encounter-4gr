using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class RoutesViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;
        public ObservableCollection<Route> Routes { get; }
        public Command LoadRoutesCommand { get; }
        public Command LoadUserRoutesCommand { get; }
        public Command AddRouteCommand { get; }

        private Route _selectedRoute;

        public RoutesViewModel()
        {
            Title = "Routes";

            Routes = new ObservableCollection<Route>();

            LoadRoutesCommand = new Command(async () => await ExecuteLoadRoutesCommand());

            LoadUserRoutesCommand = new Command(LoadUserRoutes);

            AddRouteCommand = new Command(AddRoute);

            _encounterProcessor = EncounterProcessor.Instanse;
            _encounterProcessor.UnauthorisedHttpRequestEvent += OnAuthenticationFailed;

            _session = Session.Instanse;
        }

        async Task ExecuteLoadRoutesCommand()
        {
            IsBusy = true;

            try
            {
                Routes.Clear();
                var routes = await _encounterProcessor.GetAllRoutes();
                foreach (var route in routes)
                {
                    Routes.Add(route);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public Route SelectedRoute
        {
            get => _selectedRoute;
            set
            {
                _selectedRoute = value;
                OnRouteSelected(value);
            }
        }
        private async void OnRouteSelected(Route route)
        {
            await Shell.Current.GoToAsync($"{nameof(RouteDetailPage)}?{nameof(WaypointsViewModel.RoutesId)}={route.Id}");
        }

        private async void AddRoute(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRoutePage));
        }
        private async void OnAuthenticationFailed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void LoadUserRoutes()
        {
            IsBusy = true;

            try
            {
                Routes.Clear();
                var items = await _encounterProcessor.GetUserRoutes(_session.CurrentUser.Id);
                foreach (var item in items)
                {
                    Routes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}