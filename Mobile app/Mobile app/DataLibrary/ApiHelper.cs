using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using DataLibrary.Exceptions;

namespace DataLibrary
{
    public class ApiHelper
    {
        private HttpClient ApiClient { get; set; }

        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void SetJWT(string jwt)
        {
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

        public async Task<T> HttpGet<T>(string url)
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var value = await response.Content.ReadAsAsync<T>();
                    return value;
                }

                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedHttpRequestException(response.ReasonPhrase);
                }

                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<T> HttpPost<T>(string url, T obj)
        {
            using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(url, obj))
            {
                if (response.IsSuccessStatusCode)
                {
                    obj = await response.Content.ReadAsAsync<T>();
                    return obj;
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedHttpRequestException(response.ReasonPhrase);
                }

                throw new Exception(response.ReasonPhrase);

            }
        }

        public async Task HttpPut<T>(string url, T obj)
        {
            using (HttpResponseMessage response = await ApiClient.PutAsJsonAsync(url, obj))
            {
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedHttpRequestException(response.ReasonPhrase);
                    }
                    else throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task HttpDelete(string url)
        {
            using (HttpResponseMessage response = await ApiClient.DeleteAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedHttpRequestException(response.ReasonPhrase);
                    }
                    else throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
