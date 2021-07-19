namespace AzureIntegrationDemo.IOT
{
    using System;
    using System.Net.Http;

    public class IOTClient
    {
        private readonly string Url = "https://mkrwifi1010florin.azureiotcentral.com/api/devices/9stmu3tj5k/commands/START?api-version=1.0";
        private readonly HttpClient httpClient;
        private readonly string SasToken = "SharedAccessSignature sr=8e314e1a-48b5-44c8-86de-75762f569c67&sig=SpY0yWjJEBdsuBvx%2BKp%2B9Z3nHRitQdDKKf5wuu8Wgtk%3D&skn=test2&se=1657874937844";

        public IOTClient()
        {
            httpClient = new HttpClient();
        }

        public string SendNotification()
        {
            var content = new StringContent(
                "{}",
                System.Text.Encoding.UTF8, 
                "application/json");
            
            var message = new HttpRequestMessage(HttpMethod.Post, this.Url)
            {
                Content = content 
            };
            message.Headers.Add("Authorization", SasToken);

            var response = this.httpClient.SendAsync(message).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"IOT call failed with {response.StatusCode} , {response.ReasonPhrase}");
            }

            var responseText = response.Content.ReadAsStringAsync().Result;

            return responseText;
        }
    }
}
