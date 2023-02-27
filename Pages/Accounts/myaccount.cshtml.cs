using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TECsite.Data;
using UniEncryption;
using TECsite.Models;

namespace TECsite.Pages.Accounts
{
    public class myaccountModel : PageModel
    {
        public string? uname = "";
        public User? userData;

        public void OnGet()
        {
            uname = Request.Cookies["loggedIn"];
            userData = Program.siteData.Users.Find(uname);
            Console.WriteLine($"myaccount page for {uname}");
        }
    }
}
