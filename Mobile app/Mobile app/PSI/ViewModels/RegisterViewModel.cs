using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }

        private string userName;
        private string userPassword;
        private string userConfirmPassword;
        private string userPhone;
        private string userEmail;
        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked, ValidateSave);
            CancelCommand = new Command(OnCancel);
            _encounterProcessor = EncounterProcessor.Instanse;
            this.PropertyChanged +=
            (_, __) => RegisterCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(userName)
                && !String.IsNullOrWhiteSpace(userPassword)
                && !String.IsNullOrWhiteSpace(userConfirmPassword)
                && !String.IsNullOrWhiteSpace(userPhone)
                && !String.IsNullOrWhiteSpace(userEmail);
        }
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string UserPassword
        {
            get => userPassword;
            set => SetProperty(ref userPassword, value);
        }

        public string UserConfirmPassword
        {
            get => userConfirmPassword;
            set => SetProperty(ref userConfirmPassword, value);
        }

        public string UserPhone
        {
            get => userPhone;
            set => SetProperty(ref userPhone, value);
        }

        public string UserEmail
        {
            get => userEmail;
            set => SetProperty(ref userEmail, value);
        }

        private async void OnRegisterClicked()
        {
            if (userPassword != UserConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Password doesn't match", "Please check it and try again", "OK");
            }
            else
            {
                try
                {
                    var user = new User();
                    user.Name = userName;
                    user.Email = userEmail;
                    user.PhoneNumber = userPhone;
                    user.Password = userPassword;
                    user.Name = userName;
                    await _encounterProcessor.RegisterUser(user);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Failed to create user");
                }
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                await Application.Current.MainPage.DisplayAlert("Registration completed successfully", "Please log in", "OK");
            }
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
