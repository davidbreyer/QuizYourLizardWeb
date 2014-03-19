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
        public IQuestionRepository QuestionRepository { get; set; }

        public QuestionController(IQuestionRepository questionRepository)
        {
            QuestionRepository = questionRepository;
        }

        // GET api/question
        public IEnumerable<QuestionModel> Get()
        {
            var returnValue = QuestionRepository.GetAll().ToList();

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
           var returnValue = QuestionRepository.FindBy(x => x.QuizId == QuizId);

           foreach (var question in returnValue)
           {
               question.QuizName = question.Quiz.Name;
           }

           return returnValue.ToList();
        }

        // GET api/question/5
        public QuestionModel Get(Guid id)
        {
            
            var returnValue = QuestionRepository.FindBy(x => x.Id == id).SingleOrDefault();

            returnValue.QuizName = returnValue.Quiz.Name;

            return returnValue;
        }

        // POST api/question
        public void Post([FromBody]QuestionModel question)
        {
            question.Id = Guid.NewGuid();
            question.Updated = DateTimeOffset.Now;

            QuestionRepository.Add(question);
            QuestionRepository.Save();
        }

        // PUT api/question/5
        public void Put(Guid id, [FromBody]QuestionModel question)
        {
            question.Id = id;
            question.Updated = DateTimeOffset.Now;

            QuestionRepository.Edit(question);
            QuestionRepository.Save();
        }

        // DELETE api/question/5
        public void Delete(Guid id)
        {
            var questionToDelete = QuestionRepository.FindBy(x => x.Id == id).SingleOrDefault();

            QuestionRepository.Delete(questionToDelete);
            QuestionRepository.Save();
        }
    }
}
