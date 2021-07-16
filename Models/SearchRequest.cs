using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureIntegrationDemo.Models
{
    public class SearchRequest
    {

        public string Id { get; set; }

        public string Body { get; set; }

        public string Parameters { get; set; }

        public string Headers { get; set; }
        
        public string Token { get; set; }

        public SearchRequest(string headers, string body, string parameters, string token)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Headers = headers;
            this.Body = body;
            this.Parameters = parameters;
            this.Token = token;
        }
    }
}
