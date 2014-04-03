using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Proxies;
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
        private readonly HttpClient Client;
        IQuizProxy QuizProxy { get; set; }

        public QuizEditorController(IQuizProxy quizProxy, HttpClient httpClient)
        {
            Client = httpClient;
            QuizProxy = quizProxy;
        }

        //
        // GET: /QuizEditor/
        public ActionResult Index()
        {
            var model = QuizProxy.GetAllQuizzes();

            return View(model);
        }

        //
        // GET: /QuizEditor/Details/5
        public ActionResult Details(Guid id)
        {
            var model = QuizProxy.GetQuizById(id);

            return View(model);
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
        public ActionResult Create(QuizModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = Client.PostAsync(Constants.QuizApiUri, model
                        , new JsonMediaTypeFormatter()).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(@"Index");
                    }
                    else
                    {
                        string content = result.Content.ReadAsStringAsync().Result;
                        return View();
                    }
                }

                return View();
                
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
                var model = Client.GetAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
         
        }

        //
        // POST: /QuizEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, QuizModel model)
        {

            if (ModelState.IsValid)
            {
                var result = Client.PutAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id), model
                    , new JsonMediaTypeFormatter()).Result;
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
            
            return View();
        }

        //
        // GET: /QuizEditor/Delete/5
        public ActionResult Delete(Guid id)
        {
                var model = Client.GetAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
        }

        //
        // POST: /QuizEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, QuizModel model)
        {
            try
            {
                var result = Client.DeleteAsync(string.Format(@"{0}/{1}", Constants.QuizApiUri, id)).Result;

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
    }
}
