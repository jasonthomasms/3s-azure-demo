namespace AzureIntegrationDemo.SSS
{
    using System;
    using System.Net.Http;
    using Newtonsoft.Json.Linq;

    public class SSSClient
    {
        private readonly string DefaultHost = "outlook.office365.com";
        private readonly string UrlFormat = @"https://{0}/search/api/v2/query?debug={1}";

        private readonly string url;
        private readonly HttpClient httpClient;

        public SSSClient(string host, string auth, bool debug)
        {
            this.url = string.Format(UrlFormat, host ?? DefaultHost, debug ? 1 : 0);
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth);
        }

        public JObject Execute(JObject searchRequest)
        {
            var content = new StringContent(searchRequest.ToString(), System.Text.Encoding.UTF8, "application/json");
            var message = new HttpRequestMessage(HttpMethod.Post, this.url) { Content = content };

            var response = this.httpClient.SendAsync(message).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"3S call failed with {response.StatusCode} , {response.ReasonPhrase}");
            }

            var responseText = response.Content.ReadAsStringAsync().Result;

            return JObject.Parse(responseText);
        }
    }
}
