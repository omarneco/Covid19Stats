using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Covid19Stat.Services
{
    public  class Covid19Files : Interfaces.CovidFilesApi
    {
        public async Task<List<Models.Statistic>> getData()
        {
            Services.Covid19Stat stat = new Services.Covid19Stat();
            var data = await stat.TopTenRegion();

            return data;
        }
        public async Task<XDocument> CreateXml()
        {
            var data = await getData();
            var xmlfile = new XDocument(
                                          new XElement("data",
                                                    data.Select(r =>
                                                            new XElement("region",
                                                            new XAttribute("region_name", r.region_name),
                                                            new XElement("cases", r.cases),
                                                            new XElement("deaths", r.deaths)
                                                        )
                                                    )
                                        )
                            );

            return xmlfile;
        }

        public async Task<String> CreateJson()
        {
            var data = await getData();
            String jsonfile = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            return jsonfile;
        }

        public async Task<String> CreateCVS()
        {
            var data = await getData();
            StringBuilder cvsfile = new StringBuilder();
            foreach (dynamic item in data)
            {
                cvsfile.Append(item.region_name + ',' + 
                               item.cases + ',' + 
                               item.deaths + ',' + 
                               "\r\n"
                              );
            }

            return cvsfile.ToString();
        }
    }
}