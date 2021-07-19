using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Covid19Stat.Services
{
    public class Covid19ProvinceFile :Interfaces.CovidProvinceFileApi
    {
        public async Task<List<Models.Statistic>> getData(String Region)
        {
            Services.Covid19Stat stat = new Services.Covid19Stat();
            var data = await stat.TopTenProvince(Region);

            return data;
        }
        public async Task<XDocument> CreateXml(String Region)
        {
            try
            {
                var data = await getData(Region);
                var xmlfile = new XDocument(
                                              new XElement(Region,
                                                        data.Select(r =>
                                                                new XElement("province",
                                                                new XAttribute("province_name", r.region_name),
                                                                new XElement("cases", r.cases),
                                                                new XElement("deaths", r.deaths)
                                                            )
                                                        )
                                            )
                                );

                return xmlfile;
            }
            catch(Exception ex)
            {
                throw new Exception("Cannot prepare covid19 XML top ten provinces file, because: " + ex.Message);
            }

        }

        public async Task<String> CreateJson(String Region)
        {
            try
            {
                var data = await getData(Region);
                String jsonfile = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                return jsonfile;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot prepare covid19 JSON top ten provinces file, because: " + ex.Message);
            }
        }

        public async Task<String> CreateCVS(String Region)
        {
            try
            {
                var data = await getData(Region);
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
            catch (Exception ex)
            {
                throw new Exception("Cannot prepare covid19 CVS top ten provinces file, because: " + ex.Message);
            }
        }
    }
}