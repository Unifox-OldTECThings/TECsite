using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TECsite.Data;
using UniEncryption;
using Newtonsoft.Json;
using TECsite.Models;
using System.Linq.Expressions;

namespace TECsite.Pages.Accounts
{
    public class ResetPasswordModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;
        public bool isCookie = false;

        private void WaitUntil(DateTime dateTime)
        {
            while (true)
            {
                if (DateTime.Now >= dateTime)
                {
                    return;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public void OnGet()
        {
            Console.WriteLine("ForgotPsw");
            isCookie = (Request.Cookies["resetPsw"] != null);
        }

        public async Task<ActionResult> OnPostSendreset(string psw, string confpsw, bool remember)
        {
            string encryptedpsw = psw.Encrypt();
            string encryptedconfpsw = confpsw.Encrypt();
            if (encryptedpsw != encryptedconfpsw)
            {
                rResponse = "Error: Passwords don't match!";
                return null;
            }
            else if (psw == null && confpsw == null)
            {
                rResponse = "Error: Nothing changed!";
                return null;
            }
            else
            {
                string uname = Request.Cookies["resetPsw"];

                try
                {
                    User changedUser = Program.siteData.Users.Find(uname);
                    changedUser.Password = encryptedpsw;
                    Program.siteData.EditUser(changedUser);

                    rResponse = "Success!";
                    CookieOptions cookieOptions = new();
                    if (remember)
                    {
                        cookieOptions.Expires = DateTime.MaxValue;
                    }
                    Response.Cookies.Delete("resetPsw");
                    Response.Cookies.Delete("loggedIn");
                    Response.Cookies.Append("loggedIn", uname, cookieOptions);
                    return RedirectToPage("../Index");
                }
                catch (Exception e)
                {
                    rResponse = e.Message;
                    return null;
                }
            }
        }
    }
}
