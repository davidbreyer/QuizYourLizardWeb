using QuizYourLizardApi.CrossCutting;
using QuizYourLizardApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    [ApiUriAttribute(ApiUri = Constants.QuestionApiUri)]
    public class QuestionDto : ClientEntity
    {
        public Guid QuizId { get; set; }
        [Required]
        public string Text { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
        public string QuizName { get; set; }
    }
}