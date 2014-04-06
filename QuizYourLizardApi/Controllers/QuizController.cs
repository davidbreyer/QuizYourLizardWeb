using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizYourLizardApi.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using QuizYourLizardApi.Repositories;
using Microsoft.Practices.Unity;

namespace QuizYourLizardApi.Controllers
{
    public class QuizController : BaseApiController<QuizContext, QuizModel>
    {
        public QuizController(IGenericAccessor<QuizContext, QuizModel> quizAccessor)
        {
            Accessor = quizAccessor;
        }
    }
}
