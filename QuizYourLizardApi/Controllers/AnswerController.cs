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
    public class AnswerController : ApiController
    {
        // GET api/answer
        public IEnumerable<AnswerModel> Get()
        {
            using (var answerRepo = new AnswerRepository())
            {
                return answerRepo.GetAll().ToList();
            }
        }

        //// GET api/question/
        [Route("api/question/{questionId}/answers/{answerCount}")]
        public IEnumerable<AnswerModel> Get(Guid QuestionId, int AnswerCount)
        {
            using (var answerRepo = new AnswerRepository())
            {
                var returnValue = answerRepo.FindBy(x => x.QuestionId == QuestionId);

                return returnValue.ToList();
            }
        }

        // GET api/answer/5
        public AnswerModel Get(Guid id)
        {
            using (var answerRepo = new AnswerRepository())
            {
                return answerRepo.FindBy(x => x.Id == id).SingleOrDefault();
            }
        }

        // POST api/answer
        public void Post([FromBody]AnswerModel answer)
        {
            using (var answerRepo = new AnswerRepository())
            {
                answer.Id = Guid.NewGuid();
                answer.Updated = DateTimeOffset.Now;

                answerRepo.Add(answer);
                answerRepo.Save();
            }
        }

        // PUT api/answer/5
        public void Put(Guid id, [FromBody]AnswerModel answer)
        {
            using (var answerRepo = new AnswerRepository())
            {
                answer.Id = id;
                answer.Updated = DateTimeOffset.Now;

                answerRepo.Add(answer);
                answerRepo.Save();
            }
        }

        // DELETE api/answer/5
        public void Delete(Guid id)
        {
            using (var answerRepo = new AnswerRepository())
            {
                var answerToDelete = answerRepo.FindBy(x => x.Id == id).SingleOrDefault();

                answerRepo.Delete(answerToDelete);
                answerRepo.Save();
            }
        }
    }
}
