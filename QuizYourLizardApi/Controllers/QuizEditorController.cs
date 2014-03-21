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
        private readonly HttpClient Client;

        public QuizEditorController()
            : this(new HttpClient())
        { }

        public QuizEditorController(HttpClient httpClient)
        {
            Client = httpClient;
            Client.BaseAddress = new Uri(Functions.GetApiUri());
        }

        //
        // GET: /QuizEditor/
        public ActionResult Index()
        {
           var model = Client.GetAsync(Constants.QuizApiUri).Result
                    .Content.ReadAsAsync<List<QuizModel>>().Result;

                return View(model);
           
        }

        //
        // GET: /QuizEditor/Details/5
        public ActionResult Details(Guid id)
        {
                var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

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
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                    var result = Client.PostAsync(Constants.QuizApiUri, new
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
            catch
            {
                return View();
            }
        }

        //
        // GET: /QuizEditor/Edit/5
        public ActionResult Edit(Guid id)
        {
            Client.BaseAddress = new Uri(Functions.GetApiUri());
                var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
         
        }

        //
        // POST: /QuizEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            
                var result = Client.PutAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id), new
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

        //
        // GET: /QuizEditor/Delete/5
        public ActionResult Delete(Guid id)
        {
                var model = Client.GetAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
                    .Content.ReadAsAsync<QuizModel>().Result;

                return View(model);
            
        }

        //
        // POST: /QuizEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                    var model = Client.DeleteAsync(string.Format("{0}/{1}", Constants.QuizApiUri, id)).Result
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
