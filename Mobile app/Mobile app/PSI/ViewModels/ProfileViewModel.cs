using DataLibrary;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;

        private Session _session;
        public Command UploadPhotoCommand { get; }
        public Command ChangePasswordCommand { get; }

        public Command SaveUserPhoto { get; }


        private string userName;
        private string userPhoto;
        private string userPhone;
        private string userEmail;

        public ProfileViewModel()
        {
            UploadPhotoCommand = new Command(OnUpload);
            ChangePasswordCommand = new Command(OnChange);
            SaveUserPhoto = new Command(SaveUsersPhoto);

            _encounterProcessor = EncounterProcessor.Instanse;
            _session = Session.Instanse;
            Name = _session.CurrentUser.Name;
            Email = _session.CurrentUser.Email;
            Photo = _session.CurrentUser.ProfilePicture;
            Phone = _session.CurrentUser.PhoneNumber;
        }
        public string Name
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Photo
        {
            get => userPhoto;
            set => SetProperty(ref userPhoto, value);
        }

        public string Phone
        {
            get => userPhone;
            set => SetProperty(ref userPhone, value);
        }

        public string Email
        {
            get => userEmail;
            set => SetProperty(ref userEmail, value);
        }
        private async void OnUpload()
        {
            await Shell.Current.GoToAsync($"{nameof(UploadProfilePhotoPage)}");
            //?{nameof(EditWaypointViewModel.WaypointId)}={waypointId}");
        }

        private async void OnChange()
        {
            await Shell.Current.GoToAsync($"{nameof(EditUserPasswordPopup)}");
            //?{ nameof(EditWaypointViewModel.WaypointId)}={waypointId}");
        }

        public async void LoadUserInfo()
        {
            try
            {
                var user = await _encounterProcessor.GetUser(_session.CurrentUser.Name);
                Name = user.Name;
                Email = user.Email;
                Photo = user.ProfilePicture;
                //Phone = user.OpeningHours;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
        }

        public async void SaveUsersPhoto()
        {
            try
            {
                var user = await _encounterProcessor.GetUser(_session.CurrentUser.Name);
                user.ProfilePicture = Photo;
                await _encounterProcessor.UpdateUser(_session.CurrentUser.Id, user);
                await Shell.Current.GoToAsync("..");

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
        }
    }
}
