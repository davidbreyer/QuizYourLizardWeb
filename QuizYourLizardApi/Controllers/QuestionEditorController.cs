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
        private readonly HttpClient Client;

        public QuestionEditorController()
            : this(new HttpClient())
        { }

        public QuestionEditorController(HttpClient httpClient)
        {
            Client = httpClient;
            Client.BaseAddress = new Uri(Functions.GetApiUri());
        }

        //
        // GET: /QuestionEditor/
        public ActionResult Index()
        {
            var model = Client.GetAsync(string.Format(Constants.QuestionApiUri)).Result
                    .Content.ReadAsAsync<List<QuestionModel>>().Result;

                return View(model);
        }

        //
        // GET: /QuestionEditor/Details/5
        public ActionResult Details(Guid id)
        {
            var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuestionApiUri, id)).Result
                .Content.ReadAsAsync<QuestionModel>().Result;

            return View(model);
        }

        //
        // GET: /QuestionEditor/Create
        public ActionResult Create()
        {
            ViewBag.AllQuizTypes = Client.GetAsync(Constants.QuizApiUri).Result
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
                var result = Client.PostAsync(Constants.QuestionApiUri, new
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
