using TECsite.Models;

namespace TECsite.Data
{
    public class SQLiteCommands
    {
        public static string CreateEventInfo = "CREATE TABLE \"EventsInfo\" (\r\n    \"EventNumber\" INTEGER NOT NULL CONSTRAINT \"PK_EventsInfo\" PRIMARY KEY AUTOINCREMENT,\r\n    \"EventDescription\" TEXT NOT NULL,\r\n    \"EventName\" TEXT NOT NULL,\r\n    \"EventType\" TEXT NOT NULL,\r\n    \"QuestCompatable\" INTEGER NOT NULL,\r\n    \"UserPings\" TEXT NOT NULL\r\n);";
        public static string CreateUsers = "CREATE TABLE \"Users\" (\r\n    \"UserName\" TEXT NOT NULL CONSTRAINT \"PK_Users\" PRIMARY KEY,\r\n    \"DiscordUser\" TEXT NOT NULL,\r\n    \"Email\" TEXT NOT NULL,\r\n    \"EmailConfirmed\" INTEGER NOT NULL,\r\n    \"Password\" TEXT NOT NULL,\r\n    \"UserRole\" TEXT NOT NULL\r\n);";
        
        public static string AddUser(string userName, string discordUser, string email, string password, bool emailConfirmed = false, string userRole = "User")
        {
            return ("INSERT INTO Users\nVALUES (\"" + userName + "\", \"" + discordUser + "\", \"" + email + "\", \"" + emailConfirmed + "\", \"" + password + "\", \"" + userRole + "\");");
        }

        public static string AddEvent(string eventName, string eventDescription, string eventType, int questCompatable, string[] userPings = null)
        {
            return ("INSERT INTO EventsInfo\nVALUES (\"" + eventDescription + "\", \"" + eventName + "\", \"" + eventType + "\", \"" + questCompatable + "\", \"" + userPings + "\");");
        }

        public static string FindUsername(string username)
        {
            return ("SELECT UserName FROM Users WHERE Users.UserName = \"" + username + "\");");
        }
    }
}
