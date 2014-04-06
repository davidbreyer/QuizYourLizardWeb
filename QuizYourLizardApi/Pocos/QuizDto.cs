using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    public class QuizDto : ClientEntity
    {
        public string Name { get; set; }

        public virtual ICollection<QuestionDto> Questions { get; set; }
        public override string ApiUri { get { return Constants.QuizApiUri; } }
    }
}