using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    [ApiUriAttribute(ApiUri = Constants.QuizApiUri)]
    public class QuizDto : ClientEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<QuestionDto> Questions { get; set; }
    }
}