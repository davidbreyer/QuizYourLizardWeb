﻿using Microsoft.Practices.Unity;
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
    public class QuizEditorController : BaseEditorController<QuizModel>
    {
        public QuizEditorController(IBaseProxy<QuizModel> quizProxy)
        {
            Proxy = quizProxy;
        }
    }
}
