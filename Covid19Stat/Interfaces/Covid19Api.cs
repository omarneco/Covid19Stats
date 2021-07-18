using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Covid19Stat.Interfaces
{
    interface Covid19Api
    {
        Task<String> GetCovid19Stat();
        Task<List<Models.Data>> StoreCovid19Stat();
        Task<List<Models.Statistic>> TopTenRegion();
    }

    interface Covid19RegionApi
    {
        Task<String> GetCovidRegion();
        Task<List<Models.Region>> StoreCovidRegion();
    }

    interface CovidFilesApi
    {
        Task<XDocument> CreateXml();
        Task<String> CreateJson();
        Task<String> CreateCVS();
    }

    interface CovidProvinceFileApi
    {
        Task<XDocument> CreateXml(String Region);
        Task<String> CreateJson(String Region);
        Task<String> CreateCVS(String Region);
    }

}
