using QuizYourLizardApi.Models;
using QuizYourLizardApi.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizYourLizardApi.Controllers
{
    public class AnswerEditorController : BaseEditorController<AnswerModel>
    {
        public AnswerEditorController(IBaseProxy<AnswerModel> answerProxy)
        {
            Proxy = answerProxy;
        }
    }
}