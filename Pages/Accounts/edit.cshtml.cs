using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using TECsite.Data;
using UniEncryption;
using TECsite.Models;

namespace TECsite.Pages.Accounts
{
    public class editModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;
        public string uname = "";
        public User? userData;

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
            uname = Request.Cookies["loggedIn"];
            userData = Program.siteData.Users.Find(uname);
            Console.WriteLine($"edit account page for {uname}");
        }

        public async Task<ActionResult> OnPostEdit(string? newuname = null, string? newdisuname = null, string? newemail = null, string? psw = null, bool remember = true)
        {
            string encryptedpsw = psw.Encrypt();
            uname = Request.Cookies["loggedIn"];
            userData = Program.siteData.Users.Find(uname);
            if (Program.siteData.Users.Find(newuname) != null)
            {
                rResponse = "Error: Username already exists!";
                return null;
            }
            else if (encryptedpsw != Program.siteData.Users.Find(uname).Password)
            {
                rResponse = "Error: Incorrect password!";
                return null;
            }
            else if (newuname == null && newdisuname == null && newemail == null)
            {
                rResponse = "Error: Nothing changed!";
                return null;
            }
            else
            {
                try
                {
                    User changedUser = Program.siteData.Users.Find(uname);

                    if (newuname != null)
                    {
                        Program.siteData.Users.Remove(changedUser);
                        Program.siteData.SaveChanges();
                        
                        User newchangedUser = new(newuname, (newdisuname ?? changedUser.DiscordUser), (newemail ?? changedUser.Email), changedUser.Password, changedUser.EmailConfirmed, changedUser.UserRole);
                        if (newemail != null)
                        {
                            newchangedUser.EmailConfirmed = false;
                            //send email verification
                        }
                        Program.siteData.Add<User>(newchangedUser);
                        Program.siteData.SaveChanges();

                        CookieOptions cookieOptions = new();
                        if (remember)
                        {
                            cookieOptions.Expires = DateTime.UtcNow.AddMonths(6);
                        }

                        Response.Cookies.Delete("loggedIn");
                        Response.Cookies.Append("loggedIn", newuname, cookieOptions);
                    }
                    else
                    {
                        if (newdisuname != null)
                        {
                            changedUser.DiscordUser = newdisuname;
                        }
                        if (newemail != null)
                        {
                            changedUser.Email = newemail;
                            changedUser.EmailConfirmed = false;
                            //send email verification
                        }

                        Program.siteData.Update<User>(changedUser);
                        Program.siteData.SaveChanges();
                    }

                    rResponse = "Success!";

                    Console.WriteLine("Edited Account");
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
