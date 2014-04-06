using QuizYourLizardApi.Models;
using QuizYourLizardApi.Pocos;
using QuizYourLizardApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizYourLizardApi.Controllers
{
    public class AnswerController : BaseApiController<QuizContext, AnswerModel, AnswerDto>
    {
        public AnswerController(IGenericAccessor<QuizContext, AnswerModel> answerAccessor)
        {
            Accessor = answerAccessor;
        }

        // GET api/question/
        [Route("api/question/{questionId}/answers/{answerCount}")]
        public IEnumerable<AnswerModel> Get(Guid QuestionId, int AnswerCount)
        {
            var returnValue = Accessor.Repository.FindBy(x => x.QuestionId == QuestionId);

            return returnValue.ToList();
        }
    }
}
