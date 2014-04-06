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
    public class QuizController : BaseApiController<QuizContext, QuizModel>
    {
        public QuizController(IGenericAccessor<QuizContext, QuizModel> quizAccessor)
        {
            Accessor = quizAccessor;
        }
    }

    //public class QuizController : ApiController, IBaseApiController<QuizModel>
    //{
    //    private IGenericAccessor<QuizContext, QuizModel> Accessor;

    //    public QuizController(IGenericAccessor<QuizContext, QuizModel> quizAccessor)
    //    {
    //        Accessor = quizAccessor;
    //    }

    //     GET api/quiz
    //    public IEnumerable<QuizModel> Get()
    //    {
    //        var returnValue = Accessor.Repository.GetAllAsync();
    //        return returnValue.ToList();
    //    }

    //     GET api/quiz/5
    //    public QuizModel Get(Guid id)
    //    {
    //        return Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();
    //    }

    //     POST api/quiz
    //    public void Post([FromBody]QuizModel quiz)
    //    {
    //        quiz.Id = Guid.NewGuid();
    //        quiz.Updated = DateTimeOffset.Now;

    //        Accessor.Repository.Add(quiz);
    //        Accessor.Commit();
    //    }

    //     PUT api/quiz/5
    //    public void Put(Guid id, [FromBody]QuizModel quiz)
    //    {
    //        quiz.Id = id;
    //        quiz.Updated = DateTimeOffset.Now;

    //        Accessor.Repository.Edit(quiz);
    //        Accessor.Commit();
    //    }

    //     DELETE api/quiz/5
    //    public void Delete(Guid id)
    //    {
    //        var quizToDelete = Accessor.Repository.FindBy(x => x.Id == id).SingleOrDefault();

    //        Accessor.Repository.Delete(quizToDelete);
    //        Accessor.Commit();
    //    }
    //}
}
