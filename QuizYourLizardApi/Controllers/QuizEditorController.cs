using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace QuizYourLizardApi.Controllers
{
    public class QuizEditorController : Controller
    {
        //static Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        //string _endPoint = url.GetLeftPart(UriPartial.Authority);

        //
        // GET: /QuizEditor/
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Functions.GetApiUri());
                var model = client.GetAsync(string.Format(Constants.QuizApiUri)).Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

                return View(model);
            }
        }

        //
        // GET: /QuizEditor/Details/5
        public ActionResult Details(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Functions.GetApiUri());

                var model = client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
            }
        }

        //
        // GET: /QuizEditor/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /QuizEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Functions.GetApiUri());
                    var result = client.PostAsync(string.Format(Constants.QuizApiUri), new
                    {
                        Name = Convert.ToString(collection["Name"])
                    }, new JsonMediaTypeFormatter()).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string content = result.Content.ReadAsStringAsync().Result;
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /QuizEditor/Edit/5
        public ActionResult Edit(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Functions.GetApiUri());
                var model = client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
            }
        }

        //
        // POST: /QuizEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Functions.GetApiUri());
                var result = client.PutAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id), new
                {
                    id = id,
                    Name = Convert.ToString(collection["Name"])
                }, new JsonMediaTypeFormatter()).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = result.Content.ReadAsStringAsync().Result;
                    return View();
                }
            }
        }

        //
        // GET: /QuizEditor/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Functions.GetApiUri());
                var model = client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
            }
        }

        //
        // POST: /QuizEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Functions.GetApiUri());
                    var model = client.DeleteAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                        .Content.ReadAsAsync<QuizModel>().Result;

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
