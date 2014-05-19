using Newtonsoft.Json;
using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace QuizYourLizardApi.Models
{
    [Table("Question")]
    public class QuestionModel : PersistentEntity
    {
        public Guid QuizId { get; set; }
        public string Text { get; set; }

        public ICollection<AnswerModel> Answers { get; set; }

        //[DataMember]
        [Browsable(false)]
        [XmlIgnore]
        [SoapIgnore]
        [JsonIgnore]
        public virtual QuizModel Quiz { get; set; }

        [NotMapped]
        public string QuizName { get; set; }
    }
}