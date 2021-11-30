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
                    Source = FacebookLoginService.Instance.ConsentScreenUrl(),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 1
                };

                fbWebView.Navigated += FbWebViewOnNavigated;

                Content = fbWebView;
        }

        private async void FbWebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var givenToken = FacebookLoginService.Instance.FindOutTokenFromReturnUrl(e.Url); // only on phone with installed facebook
            // Since 2021 August - https://developers.facebook.com/blog/post/2021/06/28/deprecating-support-fb-login-authentication-android-embedded-browsers/

            var profileData = await FacebookLoginService.Instance.GetFbProfileData(givenToken);
            if (profileData != null)
            {
                DoSuccessfulFbLogin(profileData);
            }
        }

        private void GoogleLogin_Tapped(object sender, System.EventArgs e)
        {
            var consentUrl = GoogleLoginService.Instance.ConsentScreenUrl();
            var googleWebView = new WebView
            {
                Source = consentUrl,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };

            googleWebView.Navigated += GoogleWebViewOnNavigated;

            Content = googleWebView;
        }

        private async void GoogleWebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var givenToken = await GoogleLoginService.Instance.GetToken(e.Url);
            // Since 2021-09-30 - https://developers.googleblog.com/2021/06/upcoming-security-changes-to-googles-oauth-2.0-authorization-endpoint.html

            var profileData = await GoogleLoginService.Instance.GetGoogleProfileData(givenToken);
            if (profileData != null)
            {
                DoSuccessfulGoogleLogin(profileData);
            }
        }

        private async void TestFb_ButtonClicked(object sender, System.EventArgs e)
        {
            if (BindingContext is LoginViewModel)
            {
                LoginViewModel vm = (LoginViewModel)BindingContext;

                var testToken = vm.Username;

                if (testToken == "null" || testToken == null)
                {
                    testToken = "EAAS2nfg91GEBAMNG7flgrBC4xnFmCaKaDDbKSFwOdzZCqNEK465cINhBNFvU5TbF3gHPxAf5sFG4OJJBKP2jwzHrwka5HF6dH8auFFze7DRrz89dCSxY0IX1qihpeBN4HqWPoCsX9br4UGpZBYw2ZAViXNvs2VqgScrgsZATpelUMyOmsd51tHvmwLTq5GDzLAZA89dZCXzAZDZD";
                }

                var profileData = await FacebookLoginService.Instance.GetFbProfileData(testToken);
                if (profileData != null)
                {
                    DoSuccessfulFbLogin(profileData);
                }
            }
        }

        private async void TestGoogle_ButtonClicked(object sender, System.EventArgs e)
        {
            if (BindingContext is LoginViewModel)
            {
                LoginViewModel vm = (LoginViewModel)BindingContext;
                var testCode = vm.Username;

                if (testCode == "null" || testCode == null)
                {
                    testCode = "4%2F0AX4XfWj_PhsRjzrqprF1aD53u99c4AQNDYPULwJmIxp13fuKGW8uRH4DknjVTdV7tmCikw";
                }

                var accessToken = await GoogleLoginService.Instance.GetToken(testCode);
                var profileData = await GoogleLoginService.Instance.GetGoogleProfileData(accessToken);
                if (profileData != null)
                {
                    DoSuccessfulGoogleLogin(profileData);
                }
            }
        }

        

        private async void DoSuccessfulFbLogin(FacebookProfileData profile)
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "Hello, " + profile.Name + ", system will store your FB-ID (" + profile.Id + ") to recognize you later.", "OK");
        }

        private async void DoSuccessfulGoogleLogin(GoogleProfileData profile)
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "Hello, Google User system will store your GOOGLE-ID (" + profile.Id + ") to recognize you later.", "OK");
        }
    }
}