using DataLibrary;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegistrationCommand { get; }

        private Session _session;

        public string Username { get; set; }
        public string Password { get; set; }


        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegistrationCommand = new Command(OnRegisterClicked);
            _session = Session.Instanse;
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            try
            {
                IsBusy = true;
                var user = await _session.LogIn(Username, Password);

                if (user == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid credentials", "Ussername or password incorrect.", "OK");
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Failed to log in", "Please try again.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async void OnRegisterClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");
        }
    }
}
