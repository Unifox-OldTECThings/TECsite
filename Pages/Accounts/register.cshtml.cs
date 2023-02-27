using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using TECsite.Data;
using TECsite.Models;
using UniEncryption;

namespace TECsite.Pages.Accounts
{
    public class registerModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = string.Empty;

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
            Console.WriteLine("register");
        }

        public async Task<ActionResult> OnPostRegister(string uname, string disuname, string email, string psw, string confpsw, bool remember = true)
        {
            if (Program.siteData.Users != null)
            {
                if (Program.siteData.Users.Find(uname) != null)
                {
                    rResponse = "Error: Username already exists!";
                    return null;
                }
                else if (psw != confpsw)
                {
                    rResponse = "Error: Passwords do not match!";
                    return null;
                }
                else
                {
                    string encryptedPass = psw.Encrypt();

                    User newuser = new(uname, disuname, email, false, encryptedPass, "User");

                    try
                    {
                        Program.siteData.AddUser(newuser);

                        rResponse = "Success!";
                        CookieOptions cookieOptions = new();
                        if (remember)
                        {
                            cookieOptions.Expires = DateTime.MaxValue;
                        }
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
            else
            {
                string encryptedPass = psw.Encrypt();

                User newuser = new(uname, disuname, email, false, encryptedPass, "User");

                try
                {
                    Program.siteData.AddUser(newuser);

                    rResponse = "Success!";
                    CookieOptions cookieOptions = new();
                    if (remember)
                    {
                        cookieOptions.Expires = DateTime.MaxValue;
                    }
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
