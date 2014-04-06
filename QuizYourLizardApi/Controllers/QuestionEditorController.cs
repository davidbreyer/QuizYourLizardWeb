using Microsoft.Practices.Unity;
using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Pocos;
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
    public class QuestionEditorController : BaseEditorController<QuestionDto>
    {
        public IBaseProxy<QuizDto> QuizProxy { get; set; }

        public QuestionEditorController(IBaseProxy<QuestionDto> questionProxy
            , IBaseProxy<QuizDto> quizProxy)
        {
            Proxy = questionProxy;
            QuizProxy = quizProxy;
        }

        // GET: /QuestionEditor/Create
        public override ActionResult Create()
        {
            ViewBag.AllQuizTypes = QuizProxy.GetAllEntities();

            return base.Create();

        }

        // GET: /QuestionEditor/Edit/5
        public override ActionResult Edit(Guid id)
        {
            ViewBag.AllQuizTypes = QuizProxy.GetAllEntities();

            return base.Edit(id);
        }
    }
}
