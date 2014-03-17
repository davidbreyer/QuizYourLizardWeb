using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizYourLizardApi.Models;
using QuizYourLizardApi.Repositories;

namespace QuizYourLizardApi.Controllers
{
    public class QuestionController : ApiController
    {
        // GET api/question
        public IEnumerable<QuestionModel> Get()
        {
            using (var questionRepo = new QuestionRepository())
            {
                var returnValue = questionRepo.GetAll().ToList();

                return returnValue;
            }
        }

        //// GET api/quiz/
        [Route("api/quiz/{quizId}/questions/{questionCount}")]
        public IEnumerable<QuestionModel> Get(Guid QuizId, int QuestionCount)
        {
            using (var questionRepo = new QuestionRepository())
            {
                var returnValue = questionRepo.FindBy(x => x.QuizId == QuizId);

                return returnValue.ToList();
            }
        }

        // GET api/question/5
        public QuestionModel Get(Guid id)
        {
            using (var questionRepo = new QuestionRepository())
            {
                return questionRepo.FindBy(x => x.Id == id).SingleOrDefault();
            }
        }

        // POST api/question
        public void Post([FromBody]QuestionModel question)
        {
            using (var questionRepo = new QuestionRepository())
            {
                question.Id = Guid.NewGuid();
                question.Updated = DateTimeOffset.Now;

                questionRepo.Add(question);
                questionRepo.Save();
            }
        }

        // PUT api/question/5
        public void Put(Guid id, [FromBody]QuestionModel question)
        {
            using (var questionRepo = new QuestionRepository())
            {
                question.Id = id;
                question.Updated = DateTimeOffset.Now;

                questionRepo.Add(question);
                questionRepo.Save();
            }
        }

        // DELETE api/question/5
        public void Delete(Guid id)
        {
            using (var questionRepo = new QuestionRepository())
            {
                var quqestionToDelete = questionRepo.FindBy(x => x.Id == id).SingleOrDefault();

                questionRepo.Delete(quqestionToDelete);
                questionRepo.Save();
            }
        }
    }
}
