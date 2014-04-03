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
    public class QuizController : ApiController
    {
        private IGenericAccessor<QuizContext, QuizModel> QuizAccessor;

        public QuizController(IGenericAccessor<QuizContext, QuizModel> quizAccessor)
        {
            QuizAccessor = quizAccessor;
        }

        // GET api/quiz
        public IEnumerable<QuizModel> Get()
        {
            var returnValue = QuizAccessor.Repository.GetAllAsync();
            return returnValue.ToList();
        }

        // GET api/quiz/5
        public QuizModel Get(Guid id)
        {
            return QuizAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
        }

        // POST api/quiz
        public void Post([FromBody]QuizModel quiz)
        {
            quiz.Id = Guid.NewGuid();
            quiz.Updated = DateTimeOffset.Now;

            QuizAccessor.Repository.Add(quiz);
            QuizAccessor.Commit();
        }

        // PUT api/quiz/5
        public void Put(Guid id, [FromBody]QuizModel quiz)
        {
            quiz.Id = id;
            quiz.Updated = DateTimeOffset.Now;

            QuizAccessor.Repository.Edit(quiz);
            QuizAccessor.Commit();
        }

        // DELETE api/quiz/5
        public void Delete(Guid id)
        {
            var quizToDelete = QuizAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

            QuizAccessor.Repository.Delete(quizToDelete);
            QuizAccessor.Commit();
        }
    }
}
