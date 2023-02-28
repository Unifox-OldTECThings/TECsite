using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace TECsite.Pages
{
    public class apiModel : PageModel
    {
        private readonly ILogger<apiModel> _logger;

        public apiModel(ILogger<apiModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("API page acessed");
        }
    }
}