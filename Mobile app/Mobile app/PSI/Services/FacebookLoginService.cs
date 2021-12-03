using DataLibrary;
using Newtonsoft.Json;
using PSI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSI.Services
{
    public class FacebookLoginService
    {
        public static FacebookLoginService Instance = new FacebookLoginService();
        private readonly HttpClient _httpClient = new HttpClient();

        public string FbReturnUrl()
        {
            return "https://encounter.is";
        }

        public string ConsentScreenUrl()
        {
            return "https://www.facebook.com/v12.0/dialog/oauth?client_id=1326689497764961&display=popup&response_type=token&redirect_uri=" + FbReturnUrl();
        }

        public string FindOutTokenFromReturnUrl(string url)
        {
            if (!url.Contains("#access_token"))
            {
                return null;
            }

            var result = url.Replace(FbReturnUrl() + "#access_token", "");

            return url.Remove(result.IndexOf("&data_access_expiration_time"));
        }

        public async Task<FacebookProfileData> GetFbProfileData(string fbToken)
        {
            if (fbToken == null)
            {
                return null;
            }
            var url = "https://graph.facebook.com/v2.7/me/?fields=name,picture,email&access_token=" + fbToken;

            var resultJson = await _httpClient.GetStringAsync(url);

            return JsonConvert.DeserializeObject<FacebookProfileData>(resultJson);
        }
    }
}
