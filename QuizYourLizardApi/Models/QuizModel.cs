using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Models
{
    public class QuizModel : PersistentEntity
    {
        public string Name { get; set; }

        public ICollection<QuestionModel> Questions { get; set; }
    }
}