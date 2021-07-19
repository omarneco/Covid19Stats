using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Covid19Stat.Interfaces;
using Covid19Stat.Models;
using System.Web.Script.Serialization;
using System.Web;

namespace Covid19Stat.Services
{
    public class Covid19Region : Covid19RegionApi
    {
        public async Task<String> GetCovidRegion()
        {
            try
            {
                String json;
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = System.Net.Http.HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
                    Headers =
                {
                    { "x-rapidapi-key", "1086a5a2demshd48c582ebd6dc8ap1b8015jsndc6bda7790d7" },
                    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                },
                };
                using (var response = await client.SendAsync(request))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new HttpException(((int)response.StatusCode), "Covid regions list endpoint failed.");
                    }
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    json = body;
                }
                return json;
            }
            catch(Exception ex)
            {
                throw new Exception("Covid regions list endpoint failed.");
            }
        }
        public async Task<List<Models.Region>> StoreCovidRegion()
        {
            List<Region> lregion = new List<Region>();
            String json = await GetCovidRegion();

            JavaScriptSerializer serial = new JavaScriptSerializer();
            dynamic data = serial.Deserialize<dynamic>(json);
            foreach (var item in data["data"])
            {
                Region region = new Region()
                {
                    iso = item["iso"],
                    name = item["name"]
                };

                lregion.Add(region);
            }

            return lregion;
        }
    }
}