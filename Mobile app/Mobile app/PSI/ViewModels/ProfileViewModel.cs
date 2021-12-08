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
            LoadUserInfo();
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
                var user = await _encounterProcessor.GetUser(_session.CurrentUser.Username);
                Name = user.Username;
                Email = user.Email;
                Photo = user.ProfilePicture;
                Phone = user.PhoneNumber;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load User");
            }
        }

        public async void SaveUsersPhoto()
        {
            try
            {
                var user = await _encounterProcessor.GetUser(_session.CurrentUser.Username);
                user.ProfilePicture = Photo;
                await _encounterProcessor.UpdateUser(_session.CurrentUser.Id, user);
                await Shell.Current.GoToAsync("..");

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Waypoint");
            }
            await Shell.Current.GoToAsync("..");
            LoadUserInfo();
        }
    }
}