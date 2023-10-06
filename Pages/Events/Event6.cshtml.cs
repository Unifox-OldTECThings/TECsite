﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace TECsite.Pages
{
    public class Event6Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event6Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Event 6 access");
        }
    }
}