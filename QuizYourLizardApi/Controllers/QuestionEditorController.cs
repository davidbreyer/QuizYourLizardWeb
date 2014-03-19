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
    public class QuestionEditorController : Controller
    {
        static Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string _endPoint = url.GetLeftPart(UriPartial.Authority);
        //
        // GET: /QuestionEditor/
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/question")).Result
                    .Content.ReadAsAsync<List<QuestionModel>>().Result;

                return View(model);
            }
        }

        //
        // GET: /QuestionEditor/Details/5
        public ActionResult Details(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/question/{0}", id)).Result
                    .Content.ReadAsAsync<QuestionModel>().Result;

                return View(model);
            }
        }

        //
        // GET: /QuestionEditor/Create
        public ActionResult Create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                ViewBag.AllQuizTypes = client.GetAsync(string.Format("/api/quiz")).Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

                return View();
            }
        }

        //
        // POST: /QuestionEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_endPoint);
                    var result = client.PostAsync(string.Format("/api/question"), new
                    {
                        Text = Convert.ToString(collection["Text"])
                        , QuizId = Convert.ToString(collection["QuizId"])
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
        // GET: /QuestionEditor/Edit/5
        public ActionResult Edit(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/question/{0}", id)).Result
                    .Content.ReadAsAsync<QuestionModel>().Result;

                ViewBag.AllQuizTypes = client.GetAsync(string.Format("/api/quiz")).Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

                return View(model);
            }
        }

        //
        // POST: /QuestionEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var result = client.PutAsync(string.Format("/api/question/{0}", id), new
                {
                    id = id,
                    Text = Convert.ToString(collection["Text"]),
                    QuizId = Convert.ToString(collection["QuizId"])
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
        // GET: /QuestionEditor/Delete/5
        public ActionResult Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("/api/question/{0}", id)).Result
                    .Content.ReadAsAsync<QuestionModel>().Result;

                return View(model);
            }
        }

        //
        // POST: /QuestionEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_endPoint);
                    var model = client.DeleteAsync(string.Format("/api/question/{0}", id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
