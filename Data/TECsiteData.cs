using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using TECsite.Models;
using NETCore.Encrypt.Internal;
using UniDatabase;

namespace TECsite.Data
{
    public class TECsiteData
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EventsInfo> EventsInfo { get; set; }
        protected AESKey UsersAESKey { get; set; }
        protected AESKey EventsAESKey { get; set; }

        public TECsiteData()
        {
            try
            {
                string[] keys = File.ReadAllLines("aesKeys.UniDBKeys");
                string[] userkey = keys[0].Split(',');
                string[] eventskey = keys[1].Split(",");
                UsersAESKey.Key = userkey[0]; UsersAESKey.IV = userkey[1];
                EventsAESKey.Key = eventskey[0]; EventsAESKey.IV = eventskey[1];
                Users = (DbSet<User>?)UniDatabase.UniDatabase.ReadDatabase<DbSet<User>>("TECUsers", UsersAESKey);
                EventsInfo = (DbSet<EventsInfo>?)UniDatabase.UniDatabase.ReadDatabase<DbSet<EventsInfo>>("TECEvents", EventsAESKey);
            }
            catch (Exception e){
                Console.Error.WriteLine(e.Message);
            }
        }

        private void saveKeys()
        {
            string[] userkey = { UsersAESKey.Key, UsersAESKey.IV };
            string[] eventskey = { EventsAESKey.Key, EventsAESKey.IV };
            string[] keys = { userkey.ToString(), eventskey.ToString() };

            if (File.Exists("aesKeys.UniDBKeys"))
            {
                File.Delete("aesKeys.UniDBKeys");
            }
            File.AppendAllLines("aesKeys.UniDBKeys", keys);
        }

        public void AddUser(User user)
        {
            if (Users != null)
            {
                Users.Add(user);
            }
            else
            {
                Users.Attach(user);
            }
            UsersAESKey = UniDatabase.UniDatabase.WriteDatabase("TECUsers", Users);
            saveKeys();
        }

        public void EditUser(User user)
        {
            Users.Update(user);
            UsersAESKey = UniDatabase.UniDatabase.WriteDatabase("TECUsers", Users);
            saveKeys();
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
            UsersAESKey = UniDatabase.UniDatabase.WriteDatabase("TECUsers", Users);
            saveKeys();
        }

        public void AddEvent(EventsInfo eventsInfo)
        {
            EventsInfo.Add(eventsInfo);
            EventsAESKey = UniDatabase.UniDatabase.WriteDatabase("TECEvents", EventsInfo);
            saveKeys();
        }

        public void EditEvent(EventsInfo eventsInfo)
        {
            EventsInfo.Update(eventsInfo);
            EventsAESKey = UniDatabase.UniDatabase.WriteDatabase("TECEvents", EventsInfo);
            saveKeys();
        }

        public void RemoveEvent(EventsInfo eventsInfo)
        {
            EventsInfo.Remove(eventsInfo);
            EventsAESKey = UniDatabase.UniDatabase.WriteDatabase("TECEvents", EventsInfo);
            saveKeys();
        }
    }
}
