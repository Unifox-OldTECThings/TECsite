using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TECsite.Data;
using TECsite.Models;

namespace TECsite.Pages.api
{
    public class accountsModel : PageModel
    {
        public string userinfo = String.Empty;

        private readonly ILogger<accountsModel> _logger;

        public accountsModel(ILogger<accountsModel> logger)
        {
            _logger = logger;
            Console.WriteLine("accounts api Page made");
        }

        public ActionResult OnGet()
        {
            Console.WriteLine("api accounts get");
            DbSet<User> users =  Program.siteData.Users;
            var enumusers = users.AsEnumerable();
            Dictionary<string, string[]> userinfo = new();
            for (int i = 0; i < users.Count(); i++)
            {
                string[] userdata = { enumusers.ElementAt(i).DiscordUser, enumusers.ElementAt(i).UserRole };
                userinfo.Add(enumusers.ElementAt(i).UserName, userdata);
            }
            string data = JsonConvert.SerializeObject(userinfo);
            Console.WriteLine("api accounts get done");
            return new OkObjectResult(data);
        }
    }
}
