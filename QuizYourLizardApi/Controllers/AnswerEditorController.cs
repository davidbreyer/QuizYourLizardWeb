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
    public class AnswerEditorController : BaseEditorController<AnswerDto>
    {
        public IBaseProxy<QuestionDto> QuestionProxy { get; set; }

        public AnswerEditorController(IBaseProxy<AnswerDto> answerProxy
            , IBaseProxy<QuestionDto> questionProxy)
        {
            Proxy = answerProxy;
            QuestionProxy = questionProxy;
        }

        // GET: /AnswerEditor/Create
        public override ActionResult Create()
        {
            ViewBag.AllQuestionTypes = QuestionProxy.GetAllEntities();

            return base.Create();

        }

        // GET: /AnswerEditor/Edit/5
        public override ActionResult Edit(Guid id)
        {
            ViewBag.AllQuestionTypes = QuestionProxy.GetAllEntities();

            return base.Edit(id);
        }
    }
}