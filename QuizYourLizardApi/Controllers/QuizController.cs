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

namespace QuizYourLizardApi.Controllers
{
    public class QuizController : ApiController
    {
        // GET api/quiz
        public IEnumerable<QuizModel> Get()
        {
            using (var quizRepo = new QuizRepository())
            {
                var returnValue = quizRepo.GetAll();
            
                return returnValue.ToList();
            }
        }

        // GET api/quiz/5
        public QuizModel Get(Guid id)
        {
            using (var quizRepo = new QuizRepository())
            {
                return quizRepo.FindBy(x => x.Id == id).SingleOrDefault();
            }
        }

        // POST api/quiz
        public void Post([FromBody]QuizModel quiz)
        {
            using (var quizRepo = new QuizRepository())
            {
                quiz.Id = Guid.NewGuid();
                quiz.Updated = DateTimeOffset.Now;

                quizRepo.Add(quiz);
                quizRepo.Save();
            }
        }

        // PUT api/quiz/5
        public void Put(Guid id, [FromBody]QuizModel quiz)
        {
            using (var quizRepo = new QuizRepository())
            {
                quiz.Id = id;
                quiz.Updated = DateTimeOffset.Now;

                quizRepo.Edit(quiz);
                quizRepo.Save();
            }
        }

        // DELETE api/quiz/5
        public void Delete(Guid id)
        {
            using (var quizRepo = new QuizRepository())
            {
                var quizToDelete = quizRepo.FindBy(x => x.Id == id).SingleOrDefault();

                quizRepo.Delete(quizToDelete);
                quizRepo.Save();
            }
        }
    }
}
