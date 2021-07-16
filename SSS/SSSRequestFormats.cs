namespace AzureIntegrationDemo.SSS
{
    public static class SSSRequestFormats
    {
        public const string ConversationRequestFormat = @"
        {{
          ""EntityRequests"": [
            {{
              ""EntityType"": ""Conversation"",
              ""ContentSources"": [
                ""Exchange""
              ],
              ""Query"": {{
                ""QueryString"": ""{0}""
              }},
              ""Fields"": [
                ""From"",
                ""To"",
                ""Cc"",
                ""Subject"",
                ""Date""
              ],
              ""SupportedResultSourceFormats"": [
                ""EntityData""
              ],
              ""PreferredResultSourceFormat"": ""EntityData""
            }}
          ],
          ""TimeZone"": ""UTC"",
          ""Cvid"": ""beef5645-81de-407c-899c-530c147ddead"",
          ""Scenario"": {{
            ""Name"": ""owa""
          }}
        }}";
    }
}
