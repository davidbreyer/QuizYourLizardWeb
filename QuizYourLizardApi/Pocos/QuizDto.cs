using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    public class QuizDto : ClientEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<QuestionDto> Questions { get; set; }
        public override string ApiUri { get { return Constants.QuizApiUri; } }
    }
}