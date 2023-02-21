using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;

namespace TECsite.Data
{
    public class TECsiteData
    {
        public Dictionary<string, string[]>? userInfo;

        public TECsiteData()
        {
            string? userInfoStr = Environment.GetEnvironmentVariable("USERINFO");
            
            if (userInfoStr != null)
            {
                userInfoStr = userInfoStr.Replace("\"=>[", "\":[");
                userInfo = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(userInfoStr);
                 
            }
        }
    }
}
