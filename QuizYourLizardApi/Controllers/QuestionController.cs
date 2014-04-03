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
        IGenericAccessor<QuizContext, QuestionModel> QuestionAccessor { get; set; }

        public QuestionController(IGenericAccessor<QuizContext, QuestionModel> questionAccessor)//IQuestionRepository questionRepository)
        {
            QuestionAccessor = questionAccessor;
        }

        // GET api/question
        public IEnumerable<QuestionModel> Get()
        {
            var returnValue = QuestionAccessor.Repository.GetAll().ToList();

            foreach(var question in returnValue)
            {
                question.QuizName = question.Quiz.Name;
            }

            return returnValue;
        }

        //// GET api/quiz/
        [Route("api/quiz/{quizId}/questions/{questionCount}")]
        public IEnumerable<QuestionModel> Get(Guid QuizId, int QuestionCount)
        {
           var returnValue = QuestionAccessor.Repository.FindBy(x => x.QuizId == QuizId);

           foreach (var question in returnValue)
           {
               question.QuizName = question.Quiz.Name;
           }

           return returnValue.ToList();
        }

        // GET api/question/5
        public QuestionModel Get(Guid id)
        {
            
            var returnValue = QuestionAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

            returnValue.QuizName = returnValue.Quiz.Name;

            return returnValue;
        }

        // POST api/question
        public void Post([FromBody]QuestionModel question)
        {
            question.Id = Guid.NewGuid();
            question.Updated = DateTimeOffset.Now;

            QuestionAccessor.Repository.Add(question);
            QuestionAccessor.Commit();
        }

        // PUT api/question/5
        public void Put(Guid id, [FromBody]QuestionModel question)
        {
            question.Id = id;
            question.Updated = DateTimeOffset.Now;

            QuestionAccessor.Repository.Edit(question);
            QuestionAccessor.Commit();
        }

        // DELETE api/question/5
        public void Delete(Guid id)
        {
            var questionToDelete = QuestionAccessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

            QuestionAccessor.Repository.Delete(questionToDelete);
            QuestionAccessor.Commit();
        }
    }
}
