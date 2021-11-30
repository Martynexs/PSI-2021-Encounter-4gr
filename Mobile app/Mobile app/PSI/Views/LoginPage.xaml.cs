using PSI.Models;
using PSI.Services;
using PSI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PSI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        private void FacebookLogin_Tapped(object sender, System.EventArgs e)
        {
                var fbWebView = new WebView
                {
                    Source = FacebookLoginService.Instance.FbTokenApiUrl(),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 1
                };

                fbWebView.Navigated += FbWebViewOnNavigated;

                Content = fbWebView;
        }

        private async void FbWebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var givenToken = FacebookLoginService.Instance.ExtractTokenFromUrl(e.Url); // only on phone with installed facebook
            // Since 2021 August - https://developers.facebook.com/blog/post/2021/06/28/deprecating-support-fb-login-authentication-android-embedded-browsers/

            var profileData = await FacebookLoginService.Instance.GetFbProfileData(givenToken);
            if (profileData != null)
            {
                DoSuccessfulLogin(profileData);
            }
        }

        private async void GoogleLogin_Tapped(object sender, System.EventArgs e)
        {
            // not implemented message:
            await Application.Current.MainPage.DisplayAlert("Error", "Oops, try again later", "OK");
        }

        private async void TestFb_ButtonClicked(object sender, System.EventArgs e)
        {
            var testToken = "EAAS2nfg91GEBACLuOxDhdWnDHGmwXAMOCe9B491wVSb4obyc5d67ZBne7udvZBca6R299YpK5gyzGn7KqNTStqzLJgyjxlc7u1JMEhNohOkt2zAmwwWcUFttxKjMIziIxPVmuIBLDqIZAoUwUZAPp1h5jmNGvIXuZCONWnJ4iZAR1ITKfrAvDUKAEFHrLxKLD25RXy32CMJQZDZD";
            var profileData = await FacebookLoginService.Instance.GetFbProfileData(testToken);
            if (profileData != null)
            {
                DoSuccessfulLogin(profileData);
            }
        }

        private async void DoSuccessfulLogin(FacebookProfileData profile)
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "Hello, " + profile.Name + ", system will store your FB-ID (" + profile.Id + ") to recognize you later.", "OK");
        }
    }
}