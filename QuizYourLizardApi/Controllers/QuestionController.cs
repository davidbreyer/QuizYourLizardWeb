using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Repositories;
using QuizYourLizardApi.Pocos;

namespace QuizYourLizardApi.Controllers
{
    public class QuestionController : BaseApiController<QuizContext, QuestionModel, QuestionDto>
    {
        public QuestionController(IGenericAccessor<QuizContext, QuestionModel> questionAccessor)
        {
            Accessor = questionAccessor;
        }
    }
}
