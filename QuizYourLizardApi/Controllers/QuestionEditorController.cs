using Microsoft.Practices.Unity;
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
    public class QuestionEditorController : Controller
    {
        static Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string _endPoint = url.GetLeftPart(UriPartial.Authority);

        private readonly HttpClient Client;

        public QuestionEditorController()
            : this(new HttpClient())
        { }

        public QuestionEditorController(HttpClient httpClient)
        {
            Client = httpClient;
            Client.BaseAddress = new Uri(_endPoint);
        }

        //
        // GET: /QuestionEditor/
        public ActionResult Index()
        {
            //using (var client = new HttpClient())
            //{
                //Client.BaseAddress = new Uri(_endPoint);
            var model = Client.GetAsync(string.Format(Constants.QuestionApiUri)).Result
                    .Content.ReadAsAsync<List<QuestionModel>>().Result;

                return View(model);
           // }
        }

        //
        // GET: /QuestionEditor/Details/5
        public ActionResult Details(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_endPoint);
                var model = client.GetAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id)).Result
                    .Content.ReadAsAsync<QuestionModel>().Result;

                return View(model);
            }
        }

        //
        // GET: /QuestionEditor/Create
        public ActionResult Create()
        {
           Client.BaseAddress = new Uri(_endPoint);
            ViewBag.AllQuizTypes = Client.GetAsync(string.Format(Constants.QuizApiUri)).Result
                .Content.ReadAsAsync<List<QuizModel>>().Result;

            return View();
            
        }

        //
        // POST: /QuestionEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                    Client.BaseAddress = new Uri(_endPoint);
                    var result = Client.PostAsync(string.Format(Constants.QuestionApiUri), new
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
            catch
            {
                return View();
            }
        }

        //
        // GET: /QuestionEditor/Edit/5
        public ActionResult Edit(Guid id)
        {
            Client.BaseAddress = new Uri(_endPoint);
            var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id)).Result
                .Content.ReadAsAsync<QuestionModel>().Result;

            ViewBag.AllQuizTypes = Client.GetAsync(string.Format(Constants.QuizApiUri)).Result
                .Content.ReadAsAsync<List<QuizModel>>().Result;

            return View(model);
        }

        //
        // POST: /QuestionEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            Client.BaseAddress = new Uri(_endPoint);
            var result = Client.PutAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id), new
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

        //
        // GET: /QuestionEditor/Delete/5
        public ActionResult Delete(Guid id)
        {
            Client.BaseAddress = new Uri(_endPoint);
            var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id)).Result
                .Content.ReadAsAsync<QuestionModel>().Result;

            return View(model);
            
        }

        //
        // POST: /QuestionEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
               Client.BaseAddress = new Uri(_endPoint);
               var model = Client.DeleteAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id)).Result
                .Content.ReadAsAsync<QuizModel>().Result;
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
