using QuizYourLizardApi.Models;
using QuizYourLizardApi.Pocos;
using QuizYourLizardApi.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizYourLizardApi.Controllers
{
    public abstract class BaseEditorController<T> : Controller
        where T : ClientEntity
    {
        protected IBaseProxy<T> Proxy { get; set; }

        //
        // GET: /BaseEditor 
        public virtual ActionResult Index()
        {
            var model = Proxy.GetAllEntities();

            return View(model);
        }

        //
        // GET: /QuizEditor/Details/5
        public virtual ActionResult Details(Guid id)
        {
            var model = Proxy.GetEntityById(id);

            return View(model);
        }

        //
        // GET: /QuizEditor/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /QuizEditor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = Proxy.CreateNewEntity(model);
                    if (result.IsSuccessStatus)
                    {
                        return RedirectToAction(@"Index");
                    }
                    else
                    {
                        string content = result.Content;
                        ModelState.AddModelError("", content);
                        
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
        public virtual ActionResult Edit(Guid id)
        {
            var model = Proxy.GetEntityById(id);
                return View(model);
        }

        //
        // POST: /QuizEditor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Guid id, T model)
        {

            if (ModelState.IsValid)
            {
                var result = Proxy.UpdateEntity(model);
                if (result.IsSuccessStatus)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = result.Content;
                    return View();
                }
            }
            
            return View();
        }

        //
        // GET: /QuizEditor/Delete/5
        public virtual ActionResult Delete(Guid id)
        {
            var model = Proxy.GetEntityById(id);
            return View(model);
        }

        //
        // POST: /QuizEditor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(Guid id, T model)
        {
            try
            {
                var result = Proxy.DeleteEntity(id);

                if (result.IsSuccessStatus)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string content = result.Content;
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