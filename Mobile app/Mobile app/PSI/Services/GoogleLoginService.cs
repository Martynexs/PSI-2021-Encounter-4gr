using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Services
{
    public class GoogleLoginService
    {
        public static GoogleLoginService Instance = new GoogleLoginService();
        private readonly HttpClient _httpClient = new HttpClient();

        private static string _clientKey = "753305973135-cniqi79qed1ob3r178louts5nf2pjt6o.apps.googleusercontent.com";
        private static string _clientSecret = "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";

        public string RedirectUrl()
        {
            return "https://encounter.is";
        }

        public string ConsentScreenUrl()
        {
            return "https://accounts.google.com/o/oauth2/v2/auth/oauthchooseaccount?" +
                "response_type=code&scope=openid&redirect_uri=https%3A%2F%2Fencounter.is" +
                "&client_id=753305973135-cniqi79qed1ob3r178louts5nf2pjt6o.apps.googleusercontent.com&flowName=GeneralOAuthFlow";
        }

        // example URL: https://encounter.is/?code=4%2F0AX4XfWiyfBJbRglePAsfW5VhvUHhYe1-XbLL_VifHHnAyRoWpYJ0khNjlKjD08ps4HynHg&scope=openid&authuser=0&prompt=consent#
        // returns 4%2F0AX4XfWiyfBJbRglePAsfW5VhvUHhYe1-XbLL_VifHHnAyRoWpYJ0khNjlKjD08ps4HynHg
        public string FindOutTokenFromReturnUrl(string url)
        {
            if (!url.Contains("code="))
            {
                return "";
            }

            string[] fields = url.Split('?')[1].Split('&');

            var code = fields.FirstOrDefault(s => s.Contains("code=")).Split('=')[1];
            return code;
        }

        public async Task<string> GetToken(string code)
        {
            var requestUrl = "https://www.googleapis.com/oauth2/v4/token?code=" + code + "&client_id=" + _clientKey + "&client_secret=" + _clientSecret + "&redirect_uri=" + RedirectUrl()
                + "&grant_type=authorization_code";

            var response = await _httpClient.PostAsync(requestUrl, null);
            var json = await response.Content.ReadAsStringAsync();

            var accessToken = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");
            return accessToken;
        }

        public async Task<GoogleProfileData> GetGoogleProfileData(string token)
        {
            //var url = "https://www.googleapis.com/plus/v1/people/me?access_token=" + token;
            //var url = "https://people.googleapis.com/v1/people/me?access_token=" + token + "&personFields=clientData";
            var url = "https://www.googleapis.com/oauth2/v2/userinfo?access_token=" + token;

            var resultJson = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<GoogleProfileData>(resultJson);
        }
    }
}
