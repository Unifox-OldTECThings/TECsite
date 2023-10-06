﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace TECsite.Pages
{
    public class Event10Model : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public Event10Model(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Console.WriteLine("Event 10 access");
        }
    }
}