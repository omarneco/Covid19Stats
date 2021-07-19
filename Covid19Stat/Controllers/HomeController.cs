using System;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Covid19Stat.Services;

namespace Covid19Stat.Controllers
{
    public class HomeController : Controller
    {
        String date = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
        Services.Covid19Stat stat = new Services.Covid19Stat();
        Services.Covid19Region regions = new Services.Covid19Region();
        Services.Covid19Files file = new Services.Covid19Files();
        Services.Covid19ProvinceFile fileprv = new Services.Covid19ProvinceFile();

        public async Task<ActionResult> Covid19()
        {
            try
            {
                ViewBag.Message = "Covid-19 Statistics by Region";

                dynamic model = new ExpandoObject();
                model.data = await stat.TopTenRegion();
                model.region = await regions.StoreCovidRegion();

                ViewBag.Date = date;

                return View(model);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Covid19Province(String Region)
        {
            try
            {
                ViewBag.Message = "Codigo Statisticts by Province";

                dynamic model = new ExpandoObject();
                model.province = await stat.TopTenProvince(Region);
                model.region = await regions.StoreCovidRegion();

                ViewBag.Region = Region;
                ViewBag.Date = date;

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19StatToXml()
        {
            try
            {
                var xmlfile = await file.CreateXml();
                string filename = Server.MapPath("/Files/") + "Covid19Region.xml";
                xmlfile.Save(filename);
                byte[] bytes = System.IO.File.ReadAllBytes(filename);

                return File(bytes,
                            "application/octet-stream",
                            "Covid19Region" + date + ".xml"
                         );
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19StatToJson()
        {
            try
            {
                var jsonfile = await file.CreateJson();
                string filename = Server.MapPath("/Files/") + "Covid19Region.json";
                System.IO.File.WriteAllText(filename, jsonfile);

                byte[] bytes = System.IO.File.ReadAllBytes(filename);

                return File(bytes,
                            "application/octet-stream",
                            "Covid19Region" + date + ".json"
                         );
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19StatCvs()
        {
            try
            {
                var cvsfile = await file.CreateCVS();

                return File(Encoding.ASCII.GetBytes(cvsfile.ToString()),
                            "text/csv",
                            "Covid19Region" + date + ".csv");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19ProvinceStatToXml(String Region)
        {
            try
            {
                var xmlfile = await fileprv.CreateXml(Region);
                string filename = Server.MapPath("/Files/") + "Covid19Province.xml";
                xmlfile.Save(filename);
                byte[] bytes = System.IO.File.ReadAllBytes(filename);

                return File(bytes,
                            "application/octet-stream",
                            "Covid19Province" + Region + date + ".xml"
                          );
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19ProvinceStatToJson(String Region)
        {
            try
            {
                var jsonfile = await fileprv.CreateJson(Region);
                string filename = Server.MapPath("/Files/") + "Covid19Province.json";
                System.IO.File.WriteAllText(filename, jsonfile);

                byte[] bytes = System.IO.File.ReadAllBytes(filename);

                return File(bytes,
                            "application/octet-stream",
                            "Covid19Province" + Region + date + ".json"
                          );
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Covid19ProvinceStatCvs(String Region)
        {
            try
            {
                var cvsfile = await fileprv.CreateCVS(Region);

                return File(Encoding.ASCII.GetBytes(cvsfile.ToString()),
                            "text/csv",
                            "Covid19Province" + date + Region + ".csv"
                          );
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

        }
    }

}