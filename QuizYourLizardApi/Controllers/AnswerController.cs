﻿using QuizYourLizardApi.Models;
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
        IAnswerRepository AnswerRepository { get; set; }
        public AnswerController(IAnswerRepository answerRepository)
        {
            AnswerRepository = answerRepository;
        }

        // GET api/answer
        public IEnumerable<AnswerModel> Get()
        {
            return AnswerRepository.GetAll().ToList();
        }

        //// GET api/question/
        [Route("api/question/{questionId}/answers/{answerCount}")]
        public IEnumerable<AnswerModel> Get(Guid QuestionId, int AnswerCount)
        {
            var returnValue = AnswerRepository.FindBy(x => x.QuestionId == QuestionId);

            return returnValue.ToList();
        }

        // GET api/answer/5
        public AnswerModel Get(Guid id)
        {
            return AnswerRepository.FindBy(x => x.Id == id).SingleOrDefault();
        }

        // POST api/answer
        public void Post([FromBody]AnswerModel answer)
        {
            answer.Id = Guid.NewGuid();
            answer.Updated = DateTimeOffset.Now;

            AnswerRepository.Add(answer);
            AnswerRepository.Save();
            
        }

        // PUT api/answer/5
        public void Put(Guid id, [FromBody]AnswerModel answer)
        {
            answer.Id = id;
            answer.Updated = DateTimeOffset.Now;

            AnswerRepository.Add(answer);
            AnswerRepository.Save();
        }

        // DELETE api/answer/5
        public void Delete(Guid id)
        {
            var answerToDelete = AnswerRepository.FindBy(x => x.Id == id).SingleOrDefault();

            AnswerRepository.Delete(answerToDelete);
            AnswerRepository.Save();
        }
    }
}
