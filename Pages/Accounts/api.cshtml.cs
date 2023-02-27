using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TECsite.Data;
using TECsite.Models;

namespace TECsite.Pages.Accounts
{
    public class apiModel : PageModel
    {
        public string userinfo = String.Empty;

        private readonly ILogger<PrivacyModel> _logger;

        public apiModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
            Console.WriteLine("accounts api Page made");
        }

        public ActionResult OnGet()
        {
            Console.WriteLine("api accounts get");
            DbSet<User> users = Program.siteData.Users;
            Dictionary<string, string[]> userinfo = new();
            for (int i = 0; i < users.Count(); i++)
            {
                string[] userdata = { users.ElementAt(i).DiscordUser, users.ElementAt(i).UserRole };
                userinfo.Add(users.ElementAt(i).UserName, userdata);
            }
            string data = JsonConvert.SerializeObject(userinfo);
            Console.WriteLine("api accounts get done");
            return new OkObjectResult(data);
        }
    }
}
