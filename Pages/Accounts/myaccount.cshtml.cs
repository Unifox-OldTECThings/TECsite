using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TECsite.Data;
using UniEncryption;

namespace TECsite.Pages.Accounts
{
    public class myaccountModel : PageModel
    {
        public string? uname = "";
        public string[]? userData = new string[0];

        public void OnGet()
        {
            TECsiteData siteData = new();
            uname = Request.Cookies["loggedIn"];
            userData = siteData.userInfo[uname];
            Console.WriteLine($"myaccount page for {uname}");
        }
    }
}
