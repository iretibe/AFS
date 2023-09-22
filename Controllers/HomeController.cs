using AFS.Data;
using AFS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AFS.Controllers
{
    public class HomeController : Controller
    {        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult AjaxMethod()
        {
            return Json(GetAllLeetSpeakData(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<tblLeetSpeek> GetAllLeetSpeakData()
        {
            using(AFS_DBEntities db = new AFS_DBEntities())
            {
                return db.tblLeetSpeeks.ToList();
            }
        }

        [HttpPost] 
        public async Task<ActionResult> CreateLeet(string inputText) 
        {
            string strFunTranslationApiValue = Convert.ToString(ConfigurationManager.AppSettings["FunTranslationApi"]);
            string strLeetSpeakJsonValue = Convert.ToString(ConfigurationManager.AppSettings["LeetSpeakJson"]);
            string strApiKeyValue = Convert.ToString(ConfigurationManager.AppSettings["ApiKey"]);

            LeetSpeakModel data = null;

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{strFunTranslationApiValue}{strLeetSpeakJsonValue}?text={inputText}&{strApiKeyValue}");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return View("Index");
            }

            var returns = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            data = JsonConvert.DeserializeObject<LeetSpeakModel>(returns);
            ContentModel contentModel = data.contents;

            using (AFS_DBEntities db = new AFS_DBEntities())
            {
                db.spInsertLeetSpeak(inputText, contentModel.translation, contentModel.translated, "BBB441C5-BE75-42DB-AA73-40C84A35D8E7");
            }

            return Json(GetAllLeetSpeakData(), JsonRequestBehavior.AllowGet);
        }
    }
}