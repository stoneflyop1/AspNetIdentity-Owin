using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace Identity.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            var handler = new HttpClientHandler {
                CookieContainer = new System.Net.CookieContainer(), UseCookies = true };

            var client = new HttpClient(handler) {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["host"]) };

            // get token
            var dict = new Dictionary<string, string>
                {
                    {"UserName", ConfigurationManager.AppSettings["user"]},
                    {"Password", ConfigurationManager.AppSettings["pass"]},
                    {"grant_type", "password"}
                };
            var m = new FormUrlEncodedContent(dict);
            var res = client.PostAsync("/Token", m).Result;
            var resContent = res.Content.ReadAsStringAsync().Result;
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine("Token Fail: " + resContent);
                return;
            }
            var tokenContent = JsonConvert.DeserializeObject<Dictionary<string,string>>(resContent);
            var tokenType = tokenContent["token_type"];
            var tokenValue = tokenContent["access_token"];


            var webRes = client.GetAsync("Home/Data").Result;
            var webContent = webRes.Content.ReadAsStringAsync().Result;
            if (webRes.IsSuccessStatusCode)
            {
                Console.WriteLine("Mvc OK: " + webContent);
            }
            else
            {
                Console.WriteLine("Mvc Fail: " + webContent);
            }

            var request = new HttpRequestMessage(HttpMethod.Get, "api/Values");
            request.Headers.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue(tokenType, tokenValue);
            var valueRes = client.SendAsync(request).Result;
            var valueContent = valueRes.Content.ReadAsStringAsync().Result;
            if (valueRes.IsSuccessStatusCode)
            {
                Console.WriteLine("api OK: " + valueContent);
            }
            else
            {
                Console.WriteLine("api Fail: " + valueContent);
            }

            

        }
    }
}
