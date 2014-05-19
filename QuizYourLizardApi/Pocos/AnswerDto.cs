using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    [ApiUriAttribute(ApiUri = Constants.AnswerApiUri)]
    public class AnswerDto : ClientEntity
    {
        public Guid QuestionId { get; set; }
        [Required]
        public string Text { get; set; }
        public bool Correct { get; set; }
        public string QuestionText { get; set; }
    }
}