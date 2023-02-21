using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TECsite.Data;
using UniEncryption;

namespace TECsite.Pages.Accounts
{
    public class loginModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;

        public void OnGet()
        {
            Console.WriteLine("login"); 
        }

        public ActionResult OnPostLogin(string uname, string psw, bool remember = true)
        {
            TECsiteData siteData = new();
            string encryptedpsw = psw.Encrypt();
            if (!siteData.userInfo.ContainsKey(uname))
            {
                rResponse = "Error: Username not found!";
                return null;
            }
            else if (!siteData.userInfo[uname].Contains(encryptedpsw))
            {
                rResponse = "Error: Incorrect Password!";
                return null;
            }
            else
            {
                rResponse = "Success!";
                CookieOptions cookieOptions = new();
                if (remember)
                {
                    cookieOptions.Expires = DateTime.MaxValue;
                }
                Response.Cookies.Append("loggedIn", uname, cookieOptions);
                Console.WriteLine($"logged in {uname}");
                return RedirectToPage("../Index");
            }
        }
    }
}
