using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace QuizYourLizardApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            using (var client = new HttpClient())
            {
                var model = client.GetAsync("http://localhost:29323/api/quiz").Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

                return View(model);
            }
        }
    }
}
