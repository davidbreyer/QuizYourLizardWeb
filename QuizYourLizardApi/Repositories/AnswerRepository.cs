using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Repositories
{
    public class AnswerRepository :
        GenericRepository<QuizContext, AnswerModel>
    {
    }
}