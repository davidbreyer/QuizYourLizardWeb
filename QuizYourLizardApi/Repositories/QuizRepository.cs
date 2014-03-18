using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public interface IQuizRepository : IGenericRepository<QuizModel>
    {
    }
    public class QuizRepository :
        GenericRepository<QuizContext, QuizModel>, IQuizRepository
    {
    }
}