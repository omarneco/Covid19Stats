using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Covid19Stat.Interfaces;
using Covid19Stat.Models;

namespace Covid19Stat.Services
{
    public class Covid19Stat : Covid19Api
    {
        List<Data> lcovid = new List<Data>();

        public async Task<String> GetCovid19Stat()
        {
            try
            {
                String json;
                DateTime date = DateTime.Today.AddDays(-1);

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/reports?date=" + date.ToString("yyyy-MM-dd")),
                    Headers =
                        {
                            { "x-rapidapi-key", "1086a5a2demshd48c582ebd6dc8ap1b8015jsndc6bda7790d7" },
                            { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
                        },
                };
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new HttpException(((int)response.StatusCode), "Covid statistics endpoint failed.");
                    }
                    response.EnsureSuccessStatusCode();

                    var body = await response.Content.ReadAsStringAsync();
                    json = body;
                }

                return json;
            }
            catch (HttpException ex)
            {
                throw new NotImplementedException("Covid endpoint failed: " + ex.Message);
            }
        }
        public async Task<List<Models.Data>> StoreCovid19Stat()
        {
            String json = await GetCovid19Stat();
            if (String.IsNullOrEmpty(json) ||
                json == "" ||
                json.Length == 11
                )
            {
                throw new Exception("Covid endpoint returns no values.");
            }

            try
            {
                JavaScriptSerializer serial = new JavaScriptSerializer();
                dynamic data = serial.Deserialize<dynamic>(json);
                foreach (var item in data["data"])
                {
                    Data stat = new Data()
                    {
                        date = item["date"],
                        confirmed = item["confirmed"],
                        deaths = item["deaths"],
                        recovered = item["recovered"],
                        confirmed_diff = item["confirmed_diff"],
                        deaths_diff = item["deaths_diff"],
                        recovered_diff = item["recovered_diff"],
                        active = item["active"],
                        active_diff = item["active_diff"],
                        fatality_rate = item["fatality_rate"],
                        region_code = item["region"]["iso"],
                        region_name = item["region"]["name"],
                        province_name = item["region"]["province"] == "" ? "Todas las Provincias" : item["region"]["province"]
                    };

                    lcovid.Add(stat);
                }

                return lcovid;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot not show data because: " + ex.Message);
            }
        }

        public async Task<List<Statistic>> TopTenRegion()
        {
            List<Data> list = new List<Data>();
            List<Statistic> region = new List<Statistic>();

            list = await StoreCovid19Stat();
            var lregion = list.GroupBy(i => i.region_name)
                        .OrderByDescending(l => l.Sum(d => d.confirmed))
                        .Take(10)
                      .Select(g => new
                      {
                          Region = g.Key,
                          Cases = g.Sum(i => i.confirmed),
                          Deaths = g.Sum(i => i.deaths)
                      }
                      );

            foreach(var item in lregion)
            {
                Statistic r = new Statistic()
                {
                    region_name = item.Region,
                    cases = item.Cases,
                    deaths = item.Deaths
                };
                region.Add(r);
            }

            return region;
        }

        [HttpPost]
        public async Task<List<Statistic>> TopTenProvince(String region)
        {
            List<Data> list = new List<Data>();
            List<Statistic> province = new List<Statistic>();

            list = await StoreCovid19Stat();
            var lregion = list.Where(i => i.region_code == region)
                        .GroupBy(i => i.province_name)
                        .OrderByDescending(l => l.Sum(d => d.confirmed))
                        .Take(10)
                      .Select(g => new
                      {
                          Province = g.Key,
                          Cases = g.Sum(i => i.confirmed),
                          Deaths = g.Sum(i => i.deaths)
                      }
                      );

            foreach (var item in lregion)
            {
                Statistic r = new Statistic()
                {
                    region_name = item.Province,
                    cases = item.Cases,
                    deaths = item.Deaths
                };
                province.Add(r);
            }

            return province;
        }
    }

}