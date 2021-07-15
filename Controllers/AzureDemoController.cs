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

        // GET: api/demo/headers
        [HttpGet("headers")]
        public string GetHeaders()
        {
            string headers = string.Empty;
            foreach(var key in Request.Headers.Keys)
            {
                headers += key + "=" + Request.Headers[key] + "\n";
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

            // Read request body as incoming string, story in body.
            using (var reader = new StreamReader(Request.Body))
            {
                var bodyReadTask = reader.ReadToEndAsync();
                bodyReadTask.Wait();
                body = bodyReadTask.Result;
                
                
            }

            // add search request to internal records and save
            var searchRequest = new SearchRequest(headers, body, parameters);
            _context.DemoItems.Add(searchRequest);
            await _context.SaveChangesAsync();

            Call3s(searchRequest);
            DoSomething();

            return NoContent();
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
                searches += $"{searchRequest.Id}\n{searchRequest.Parameters}\n{searchRequest.Headers}\n{searchRequest.Body}\n\n\n";
            }
            // Maybe figure out a better way to send data. This is just sending text. Lets send back as json
            return searches;
        }

        public void Call3s(SearchRequest request)
        {
            // make http request here to 3s. Maybe move this to its own helper class.
        }

        public void DoSomething()
        {
            // Make a call with the query string to sentiment or something else. Move to its own helper class.
        }
    }
}
