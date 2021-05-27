using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace advice_project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string AdviceText { get; set; }

        public string TodayDate { get; set; }

        static readonly HttpClient client = new HttpClient();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.adviceslip.com/advice");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                responseBody = responseBody.Trim('{', '}').Split(":")[3];

                AdviceText = responseBody;
                TodayDate = DateTime.UtcNow.Date.ToString("dd.MM.yyyy");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nHTTPRequest Exception");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Page();
        }

        public IActionResult check()
        {
            return Page();
        }

    }
}