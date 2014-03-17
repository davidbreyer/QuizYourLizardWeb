using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public class QuestionRepository :
        GenericRepository<QuizContext, QuestionModel>
    {
        public IQueryable<QuestionModel> GetAllQuestionsForQuiz(Guid Id)
        {
            return Context.Questions.Where(x => x.QuizId == Id);
        }
    }
}