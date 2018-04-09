using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Evolent.Web.Helpers
{
    public class HttpClientHelper
    {
        public static HttpClient CreateHttpClient(string accessToken)
        {
            HttpClient objHttpClient = new HttpClient();
                //objHttpClient.DefaultRequestHeaders.Authorization =
                //                    new AuthenticationHeaderValue("Bearer", accessToken);
                objHttpClient.DefaultRequestHeaders.Accept.Clear();
                objHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return objHttpClient;
        }

    }
}