using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TECsite.Data;
using UniEncryption;
using TECsite.EmailService;

namespace TECsite.Pages.Accounts
{
    public class ForgotPasswordModel : PageModel
    {
        private static readonly HttpClient client = new HttpClient();
        public string rResponse = String.Empty;

        public void OnGet()
        {
            Console.WriteLine("ForgotPsw");
        }

        public async Task<ActionResult> OnPostSendreset(string uname, string disuname, string email)
        {
            if (Program.siteData.Users.Find(uname) == null)
            {
                rResponse = "Error: Username not found!";
                return null;
            }
            else if (Program.siteData.Users.Find(uname).DiscordUser != disuname)
            {
                rResponse = "Error: Incorrect Discord Username!";
                return null;
            }
            else if (Program.siteData.Users.Find(uname).Email != email)
            {
                rResponse = "Error: Incorrect Email!";
                return null;
            }
            else
            {
                rResponse = "Success!";
                CookieOptions cookieOptions = new();

                DateTime cookieEnd = DateTime.Now;
                cookieEnd = cookieEnd.AddMinutes(10);
                cookieOptions.Expires = cookieEnd;

                Response.Cookies.Append("resetPsw", uname, cookieOptions);

                Console.WriteLine("getting sender");
                EmailSender emailSender = new EmailSender();
                Console.WriteLine("setting email and message");

                Console.WriteLine("setting to dict");
                Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
                nameadressdict.Add(uname, email);
                Console.WriteLine("Making Message");
                var message = new Message("The Energetic Convention", "theenergeticconvention@gmail.com", nameadressdict, "Password Reset Request", "Use the link below to reset your password! \nhttps://tec-site.herokuapp.com/Accounts/ResetPassword", null);
                Console.WriteLine("Sending Message");
                emailSender.SendEmail(message);

                Response.Cookies.Delete("loggedIn");

                Console.WriteLine($"Password reset for {uname}");
                return RedirectToPage("../Index");
            }
        }
    }
}
