using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Reflection;
using TECsite.Models;

namespace TECsite.Pages.api
{
    [IgnoreAntiforgeryToken]
    public class EventsModel : PageModel
    {
        public string events = string.Empty;
        private readonly string[] eventsList = { "opening", "prophunt", "furrygames", "mcminigames", "udontycoon", "udontag", "minesweeper", "amongus", "bsbattles", "cah", "fandf", "puttputt", "closing", "furmeet", "jackbox", "djstream" };

        public ActionResult OnGet()
        {
            Console.WriteLine("api events get");
            events = JsonConvert.SerializeObject(eventsList);
            Console.WriteLine("api events get done");
            return new OkObjectResult(events);
        }

        public ActionResult OnPost([FromBody] EventsInfo data, [FromHeader(Name = "EventAuth")] string authHeader) 
        {
            Console.WriteLine("EventPost");

            if(authHeader == "TECEvents")
            {
                Console.WriteLine("auth good!");
                Program.siteData.Add<EventsInfo>(data);
                Program.siteData.SaveChanges();
            }

            string requestdata = JsonConvert.SerializeObject(data);
            Console.WriteLine(requestdata);
            return new OkObjectResult(requestdata);
        }
    }
}
