using QuizYourLizardApi.Models;
using QuizYourLizardApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizYourLizardApi.Controllers
{
    public class AnswerController : BaseApiController<QuizContext, AnswerModel>
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

    //public class AnswerController : ApiController
    //{
    //    IGenericAccessor<QuizContext, AnswerModel> AnswerAccessor { get; set; }
    //    public AnswerController(IGenericAccessor<QuizContext, AnswerModel> answerAccessor)
    //    {
    //        AnswerAccessor = answerAccessor;
    //    }

    //    // GET api/answer
    //    public IEnumerable<AnswerModel> Get()
    //    {
    //        return AnswerAccessor.Repository.GetAll().ToList();
    //    }

    //    //// GET api/question/
    //    [Route("api/question/{questionId}/answers/{answerCount}")]
    //    public IEnumerable<AnswerModel> Get(Guid QuestionId, int AnswerCount)
    //    {
    //        var returnValue = AnswerAccessor.Repository.FindBy(x => x.QuestionId == QuestionId);

    //        return returnValue.ToList();
    //    }

    //    // GET api/answer/5
    //    public AnswerModel Get(Guid id)
    //    {
    //        return AnswerAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
    //    }

    //    // POST api/answer
    //    public void Post([FromBody]AnswerModel answer)
    //    {
    //        answer.Id = Guid.NewGuid();
    //        answer.Updated = DateTimeOffset.Now;

    //        AnswerAccessor.Repository.Add(answer);
    //        AnswerAccessor.Commit();
            
    //    }

    //    // PUT api/answer/5
    //    public void Put(Guid id, [FromBody]AnswerModel answer)
    //    {
    //        answer.Id = id;
    //        answer.Updated = DateTimeOffset.Now;

    //        AnswerAccessor.Repository.Add(answer);
    //        AnswerAccessor.Commit();
    //    }

    //    // DELETE api/answer/5
    //    public void Delete(Guid id)
    //    {
    //        var answerToDelete = AnswerAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

    //        AnswerAccessor.Repository.Delete(answerToDelete);
    //        AnswerAccessor.Commit();
    //    }
    //}
}
