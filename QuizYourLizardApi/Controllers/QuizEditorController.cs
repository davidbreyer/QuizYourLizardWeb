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
        static Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string _endPoint = url.GetLeftPart(UriPartial.Authority);

        //
        // GET: /QuizEditor/
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/quiz/")).Result
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
                client.BaseAddress = new Uri(_endPoint);

                var model = client.GetAsync(string.Format("/api/quiz/{0}", id)).Result
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_endPoint);
                    var result = client.PostAsync(string.Format("/api/quiz"), new
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
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/quiz/{0}", id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
            }
        }

        //
        // POST: /QuizEditor/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var result = client.PutAsync(string.Format("/api/quiz/{0}", id), new
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
                client.BaseAddress = new Uri(_endPoint);
                var model = client.DeleteAsync(string.Format("/api/quiz/{0}", id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return RedirectToAction("Index");
            }
        }

        //
        // POST: /QuizEditor/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
