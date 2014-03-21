using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Models
{
    public class QuizModel : PersistentEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<QuestionModel> Questions { get; set; }
    }
}