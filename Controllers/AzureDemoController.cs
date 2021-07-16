using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using AzureIntegrationDemo.Models;

namespace AzureIntegrationDemo.Controllers
{
    [Route("api/demo")]
    [ApiController]
    public class AzureDemoController : Controller
    {

        private readonly DemoContext _context;

        public AzureDemoController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/demo
        [HttpGet]
        public int Get5()
        {
            return 5;
        }

        // GET: api/demo/{id}
        [HttpGet("{id}")]
        public long MultiplyId(long id)
        {
            return id * 5;
        }

        public string GetHeaders()
        {
            string headers = string.Empty;
            foreach(var key in Request.Headers.Keys)
            {
                headers += $"{key}: {Request.Headers[key]}";
            }
            return headers;
        }

        // POST: /api/demo/search
        /// <summary>
        /// Adds search query info to internal database.
        /// </summary>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<IActionResult> PostSearchQuery()
        {
            var headers = GetHeaders();
            var body = string.Empty;
            var parameters = Request.QueryString.ToString().Substring(1);

            var minutes= int.Parse(Request.Query["minutes"].ToString());
            var threshold = int.Parse(Request.Query["threshold"].ToString());

            // Read request body as incoming string, story in body.
            using (var reader = new StreamReader(Request.Body))
            {
                var bodyReadTask = reader.ReadToEndAsync();
                bodyReadTask.Wait();
                body = bodyReadTask.Result;
                
                
            }

            // add search request to internal records and save
            var searchRequest = new SearchRequest(headers, body, parameters, Request.Headers["X-3S-Token"].ToString());
            _context.DemoItems.Add(searchRequest);
            await _context.SaveChangesAsync();

            var (isLiveSitePossible, responseContent) = Call3s(searchRequest, minutes, threshold);

            DoSomething();
            FlorinDeviceCall(isLiveSitePossible);

            return Content($"Heightened Live Site Potential: {isLiveSitePossible}.\nResponse:\n{responseContent}");
        }

        // GET: api/demo/searches
        /// <summary>
        /// Gets previous searches
        /// </summary>
        /// <returns></returns>
        [HttpGet("searches")]
        public string GetSearches()
        {
            string searches = string.Empty;

            if(_context.DemoItems.Count() == 0)
            {
                return "No searches have been made!";
            }

            foreach (var searchRequest in _context.DemoItems)
            {
                searches += $"{searchRequest.Id}\n{searchRequest.Parameters}\n{searchRequest.Body}\n\n\n";
            }
            // Maybe figure out a better way to send data. This is just sending text. Lets send back as json
            return searches;
        }

        public (bool, string) Call3s(SearchRequest request, int minutes, int threshold)
        {
            // make http request here to 3s. Maybe move this to its own helper class.

            /* TODO: Need to integrate
             */
            var sssClient = new SSS.SSSClient(null, request.Token, false);

            var liveSiteMonitor = new SSS.SSSLiveSiteMessageMonitor(sssClient, "3SFileSearchDRI", minutes, threshold, null);

            var (heightenedLiveSitePossible, responseContent) = liveSiteMonitor.Run();

            return liveSiteMonitor.Run();
        }

        public void DoSomething()
        {
            // Make a call with the query string to sentiment or something else. Move to its own helper class.
        }

        public void FlorinDeviceCall(bool isLiveSite)
        {

        }
    }
}
