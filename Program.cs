//usings
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using TECsite.Data;
using TECsite.EmailService;
using TECsite.Models;
using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using UniEncryption;
using System.Diagnostics;

namespace TECsite {

    public class Program {

        public static TECsiteData siteData = new TECsiteData();

        /// <summary>
        /// main timekeep variable
        /// </summary>
        public static DateTime mainNow = DateTime.Now;

        public static TimeSpan fiveMin = mainNow.AddMinutes(5) - mainNow;

        /// <summary>
        /// Used to get a DateTime from a string
        /// </summary>
        /// <param name="eventdate">The date and time as: dd/MM/yyyy hh:mm:ss tt</param>
        /// <returns>The parsed DateTime</returns>
        public static DateTime GetEventTime(string eventdate)
        {
            return DateTime.ParseExact(eventdate, "dd/MM/yyyy hh:mm:ss tt", null);
        }

        /// <summary>
        /// Used to reformat from yyyy/mm/dd to dd/mm/yyyy
        /// </summary>
        /// <param name="date">The date to be reformatted</param>
        /// <returns>The reformatted date</returns>
        public static string reformatDate(string date)
        {
            char[] splitdel = { ' ', '/' };
            string[] splitdate = date.Split(splitdel);
            string finaldate = splitdate[2] + "/" + splitdate[1] + "/" + splitdate[0] + " " + splitdate[3] + " " + splitdate[4];
            Console.WriteLine(finaldate);
            return finaldate;
        }

        //TODO: maybe put this into an SQL database table so its not annoyingly here
        //hardcoded event times
        public static string Event1Time = "31/03/2023 04:00:00 PM";
        public static string Event2Time = "31/03/2023 05:00:00 PM";
        public static string Event3Time = "31/03/2023 06:00:00 PM";
        public static string Event4Time = "31/03/2023 07:00:00 PM";
        public static string Day1EndTime = "31/03/2023 08:00:00 PM";
        public static string Event5Time = "01/04/2023 08:00:00 AM";
        public static string Event6Time = "01/04/2023 09:00:00 AM";
        public static string Event7Time = "01/04/2023 11:00:00 AM";
        public static string Event8Time = "01/04/2023 12:00:00 PM";
        public static string Event9Time = "01/04/2023 12:30:00 PM";
        public static string Event10Time = "01/04/2023 01:00:00 PM";
        public static string Event11Time = "01/04/2023 02:00:00 PM";
        public static string Event12Time = "01/04/2023 03:00:00 PM";
        public static string Event13Time = "01/04/2023 04:00:00 PM";
        public static string Event14Time = "01/04/2023 05:00:00 PM";
        public static string Event15Time = "01/04/2023 06:00:00 PM";
        public static string Event16Time = "01/04/2023 07:00:00 PM";
        public static string Event17Time = "01/04/2023 08:00:00 PM";
        public static string Day2EndTime = "01/04/2023 09:00:00 PM";
        public static string Event18Time = "02/04/2023 08:00:00 AM";
        public static string Event19Time = "02/04/2023 09:00:00 AM";
        public static string Event20Time = "02/04/2023 10:00:00 AM";
        public static string Event21Time = "02/04/2023 11:00:00 AM";
        public static string Event22Time = "02/04/2023 12:00:00 PM";
        public static string Event23Time = "02/04/2023 01:00:00 PM";
        public static string Event24Time = "02/04/2023 03:00:00 PM";
        public static string Event25Time = "02/04/2023 04:00:00 PM";
        public static string Event26Time = "02/04/2023 05:00:00 PM";
        public static string ConEndTime = "02/04/2023 06:00:00 PM";

        /// <summary>
        /// Used to update the dns server with the correct ip address
        /// </summary>
        /// <param name="IP">IP address to set</param>
        /// <param name="DNSid">The DNS record ID</param>
        /// <param name="name">The URL address that points to the IP</param>
        /// <returns>A <see cref="Task"/> object to be run</returns>
        public static async Task<string> UpdateDNS(string IP, string DNSid, string name)
        {
            //the content to be sent
            string reqContent = "{\n    \"content\": \"" + IP + "\",\n    \"name\": \"" + name + "\",\n    \"ttl\": 1,\n    \"type\": \"A\",\n    \"proxied\": true\n}";
            StringContent reqStr = new StringContent(reqContent, System.Text.Encoding.ASCII, "application/json");
            Debug.WriteLine(await reqStr.ReadAsStringAsync());

            //set contentType header properly
            reqStr.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            Debug.WriteLine(reqStr.Headers);

            //get a client and add auth headers
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("CFBToken"));
            client.DefaultRequestHeaders.Add("X-Auth-Email", "awsomejojop@gmail.com");
            Debug.WriteLine(client.DefaultRequestHeaders.ToString());

            //The DNS server url to send to
            string reqUrl = "https://api.cloudflare.com/client/v4/zones/97e43709fe00a86833d432e2bd9c06e9/dns_records/" + DNSid;
            Debug.WriteLine(reqUrl);

            //the response from the DNS server
            var resp = await client.PutAsync(reqUrl, reqStr);
            Debug.WriteLine(await resp.Content.ReadAsStringAsync());

            //return the response
            return resp.ToString();
        }

        /// <summary>
        /// Used to create a configured <see cref="IWebHostBuilder"/>
        /// </summary>
        /// <param name="args">unused, put in args from main func var</param>
        /// <param name="myIP">The local IP address to listen on</param>
        /// <param name="root">The root directory of the app</param>
        /// <returns>A configured <see cref="IWebHostBuilder"/></returns>
        public static IHostBuilder CreateHostBuilder(string[] args, string myIP, string root)
        {
            //the web host builder to be returned
            var host = new HostBuilder()
                //configure some logging to see whats going on inside, could be commented out later ig
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddEventLog();
                    logging.SetMinimumLevel(LogLevel.Trace); //write EVERYTHING, im trying to get stuff figured out
                })
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseKestrel()
#if DEBUG
                    .UseUrls("https://localhost:443", "http://localhost:80") //set the addresses to listen on from provided IP
#else
                    .UseUrls("https://" + myIP + ":443", "http://" + myIP + ":80") //set the addresses to listen on from provided IP
#endif
                    .CaptureStartupErrors(true) //Capture the startup errors if something happens ig
                    //.UseIISIntegration() //IDK lol
                    .UseStartup<Startup>(); //required ig, use the startup for stuff
                })
                .UseContentRoot(root); //set the content root

            //and return the host builder
            return host;
        }

        /// <summary>
        /// The main function to start up on
        /// </summary>
        /// <param name="args">required args string ig</param>
        /// <returns>idk lol, exit status ig?</returns>
        public static async Task Main(string[] args)
        {
            //get the root directory, grab the .env file, and load up our environment variables
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);


            string hostName = Dns.GetHostName(); // Retrive the Name of HOST computer
            Debug.WriteLine(hostName);

            // Get the IPs of the computer, and grab the last one, which seems to be the correct IPv4 for the computer
            IPAddress[] myIPs = Dns.GetHostEntry(hostName).AddressList;
            string myIP = myIPs[myIPs.Length - 1].ToString();
            Debug.WriteLine("My IP Address is :" + myIP);

            //create a host builder of the site, and build the app
            var builder = CreateHostBuilder(args, myIP, root);
            var app = builder.Build();

            //not sure what this is needed for, but keeping just in case ig
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }

            //in case we need to check email service is working
            /*
            EmailSender _emailSender = new EmailSender();
            Console.WriteLine("sending startup email for test");
            Dictionary<string, string> nameadressdict = new Dictionary<string, string>();
            nameadressdict.Add("Unifox", "awsomejojop@gmail.com");
            var message = new Message("The Energetic Convention", "theenergeticconvention@gmail.com", nameadressdict, "Startup", "<html><style>a {color: rgb(255, 210, 8)} a:hover {color: rgb(220, 180, 0)} body {margin-bottom: 30px; background-repeat: no-repeat; background-size: cover; background-position-y: 25 %;background-color: rgba(33, 37, 41, 1); opacity: 1; width: 99 %; height: 90 %;} p {color: rgb(255, 255, 255)}</style><body>test text <a href='https://discord.gg/Rte9sbK76D'>test link</a></body><html>", null);
            _emailSender.SendEmail(message);
            Console.WriteLine("email sent");
            */

#if DEBUG
            //try to run the site, do not await to allow code to continue running
            try
            {
                app.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //catch any exceptions that happen possibly
            }

            int waitime = 0;
            //and finally keep the current time updated every half second
            while (true)
            {
                mainNow = DateTime.Now;
                Console.WriteLine(mainNow.ToString());
                if (waitime < 6) //wait 3 seconds before checking the time, to make sure it doesnt shut down upon startup
                {
                    waitime++;
                }
                else if (mainNow.ToLongTimeString() == "12:00:00 AM") //restart the site every midnight. use windows task scheduler to start a new instance while this on shuts down
                {
                    Console.WriteLine("shutoff");//Environment.Exit(0);
                }
                Thread.Sleep(500);
            }

#else
            //grab the routers IP and the last known router IP for later
            string strRIP = await new HttpClient().GetStringAsync("https://ipinfo.io/ip");
            Debug.WriteLine("Router IP:" + strRIP);
            string routerIP = Environment.GetEnvironmentVariable("RIP") ?? "0.0.0.0"; //default to 0.0.0.0 as an error ip

            //write an error message if unable to get the router IP
            if (strRIP == "0.0.0.0")
            {
                Console.Error.WriteLine("ERROR! NO ROUTER IP FOUND");
            }
            //else update the DNS server if the router IP changed
            else if (routerIP != strRIP)
            {
                for (int i = 0; i < 2; i++) //need to update both records
                {
                    string dnsID, webname; //create a DNS ID and web URL to be set for update

                    //set the previous accordingly for the update
                    if (i == 0)
                    {
                        dnsID = "ac5e3653e2c8a4e76ec141b416e6beb6";
                        webname = "thenergeticon.com";
                    }
                    else
                    {
                        dnsID = "55b26fe22e78fb9ef7dc394e6f6d88de";
                        webname = "www.thenergeticon.com";
                    }

                    //and commense the update
                    string CFresp = await UpdateDNS(strRIP, dnsID, webname);
                    Debug.WriteLine(CFresp);
                }

                //Update the .env file with the router IP, as well as the environment variable
                DotEnv.Write(dotenv, "RIP = " + strRIP);
                Environment.SetEnvironmentVariable("RIP", strRIP);
            }

            //try to run the site, do not await to allow code to continue running
            try
            {
                app.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //catch any exceptions that happen possibly
            }

            int waitime = 0;
            //and finally keep the current time updated every half second
            while (true)
            {
                mainNow = DateTime.Now;
                Console.WriteLine(mainNow.ToString());
                if (waitime < 6) //wait 3 seconds before checking the time, to make sure it doesnt shut down upon startup
                {
                    waitime++;
                }
                else if (mainNow.ToLongTimeString() == "12:00:00 AM") //restart the site every midnight. use windows task scheduler to start a new instance while this on shuts down
                {
                    Console.WriteLine("shutoff");//Environment.Exit(0);
                }
                Thread.Sleep(500);
            }
#endif
        }

    }
}