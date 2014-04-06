using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    public class AnswerDto : ClientEntity
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool Correct { get; set; }
        public string QuestionText { get; set; }
        public override string ApiUri { get { return Constants.AnswerApiUri; } }
    }
}