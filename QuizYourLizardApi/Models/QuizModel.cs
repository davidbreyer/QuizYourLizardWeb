using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Models
{
    [Table("Quiz")]
    public class QuizModel : PersistentEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<QuestionModel> Questions { get; set; }

        [NotMapped]
        public override string ApiUri { get { return Constants.QuizApiUri; } }
    }
}