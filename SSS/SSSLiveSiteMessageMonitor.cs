namespace AzureIntegrationDemo.SSS
{
    using System;
    using Newtonsoft.Json.Linq;

    public class SSSLiveSiteMessageMonitor
    {
        private const string LiveSiteQueryFormat = @"(to: {0} AND sent >{1}) AND(\""Sev 0:\"" OR \""Sev 1:\"" OR \""Sev 2:\"")";

        private readonly SSSClient client;
        private readonly string dl;
        private readonly int minsOffset;
        private readonly int threshold;
        private readonly Func<JObject, bool> determiner;

        public SSSLiveSiteMessageMonitor(
            SSSClient sssClient, string distributionList, int minutesOffset, int countThreshold, Func<JObject, bool> liveSiteDeterminer)
        {
            this.client = sssClient;
            this.dl = distributionList;
            this.minsOffset = minutesOffset;
            this.threshold = countThreshold;
            this.determiner = liveSiteDeterminer;
        }

        public bool Run()
        {
            var request = JObject.Parse(BuildRequest());

            var response = this.client.Execute(request);

            var determiner = this.determiner ?? this.DefaultDeterminer;

            return determiner(response);
        }

        private string GetEarlierDateTime(int offset)
        {
            var now = System.DateTimeOffset.UtcNow;
            var earlier = now.AddMinutes(-1 * offset).DateTime;
            return earlier.ToString("o");
        }

        private string BuildRequest()
        {
            var sentBefore = GetEarlierDateTime(this.minsOffset);
            var query = string.Format(LiveSiteQueryFormat, this.dl, sentBefore);
            var request = string.Format(SSSRequestFormats.ConversationRequestFormat, query);
            return request;
        }

        private bool DefaultDeterminer(JObject response)
        {
            var count = (((JArray)response?["EntitySets"]?[0]?["ResultSets"]?[0]?["Results"])?.Count ?? -1);
            return count >= this.threshold;
        }
    }
}
