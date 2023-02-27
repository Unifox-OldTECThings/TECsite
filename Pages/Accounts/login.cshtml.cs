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
            string encryptedpsw = psw.Encrypt();
            if (Program.siteData.Users.Find(uname) == null)
            {
                rResponse = "Error: Username not found!";
                return null;
            }
            else if (Program.siteData.Users.Find(uname).Password != encryptedpsw)
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
