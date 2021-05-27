using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace advice_project.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        public List<string> Advices = new List<string>();

        public int TotalAmount { get; set; }

        static readonly HttpClient client = new HttpClient();
        
        public async Task<IActionResult> OnPostAsync()
        {
            Advices.Clear();
            var query = Request.Form["Query"];
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://api.adviceslip.com/advice/search/{query}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject responseObject = JObject.Parse(responseBody);

                if (responseObject["total_results"] != null)
                {
                    TotalAmount = (int)responseObject["total_results"];
                    for (int i = 0; i < TotalAmount; i++)
                    {
                        Advices.Add((string)responseObject["slips"][i]["advice"]);
                    }
                }
                else
                {
                    Advices.Add((string)responseObject["message"]["text"]);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nHTTPRequest Exception");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return Page();
        }
    }
}