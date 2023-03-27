using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using TECsite.Models;
using NETCore.Encrypt.Internal;
using UniDatabase;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace TECsite.Data
{
    public class TECsiteData : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<EventsInfo> EventsInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // connect to sqlite database
            optionsBuilder.UseSqlite("Data Source=.\\Data\\TECData.db");
            optionsBuilder.UseApplicationServiceProvider(Program.services);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventsInfo>()
                .Property(p => p.UserPings)
                .HasConversion(v => JsonConvert.SerializeObject(v),
                               v => JsonConvert.DeserializeObject<string[]>(v));
        }
    }
}
