using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TECsite.Data;
using TECsite.Models;

namespace TECsite.Models
{
    public class EventsInfo
    {
        public string EventName { get; set; }
        public string EventId { get; set; }
        public string EventType { get; set; }
        public int EventNumber { get; set; }
        public string EventDescription { get; set; }
        public string EventCategory { get; set; }
        public string[] UserPings { get; set; }
    }
}