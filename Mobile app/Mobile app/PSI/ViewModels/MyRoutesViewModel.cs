using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class MyRoutesViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;
        public ObservableCollection<Route> MyRoutes { get; }

        public Command LoadUserRoutesCommand { get; }
        public Command AddRouteCommand { get; }

        private Route _selectedRoute;

        private bool _userRoutesOnly = false;
        public MyRoutesViewModel()
        {
            MyRoutes = new ObservableCollection<Route>();
            LoadUserRoutesCommand = new Command(async () => await LoadUserRoutes());
            AddRouteCommand = new Command(AddRoute);
            _encounterProcessor = EncounterProcessor.Instanse;
            _encounterProcessor.UnauthorisedHttpRequestEvent += OnAuthenticationFailed;
            _session = Session.Instanse;

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
            await Shell.Current.GoToAsync($"{nameof(MyRouteDetailPage)}?{nameof(WaypointsViewModel.RoutesId)}={route.Id}");
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
        async Task LoadUserRoutes()
        {
            _userRoutesOnly = !_userRoutesOnly;

            if (_userRoutesOnly)
            {
                IsBusy = true;
                try
                {
                    MyRoutes.Clear();
                    var items = await _encounterProcessor.GetUserRoutes(_session.CurrentUser.Id);
                    foreach (var item in items)
                    {
                        MyRoutes.Add(item);
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
            IsBusy = false;
        }
        private async void AddRoute(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRoutePage));
        }
        private async void OnAuthenticationFailed()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
