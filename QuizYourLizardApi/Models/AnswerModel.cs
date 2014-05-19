using Newtonsoft.Json;
using QuizYourLizardApi.CrossCutting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace QuizYourLizardApi.Models
{
    [Table("Answer")]
    public class AnswerModel : PersistentEntity
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool Correct { get; set; }

        //[DataMember]
        [Browsable(false)]
        [XmlIgnore]
        [SoapIgnore]
        [JsonIgnore]
        public virtual QuestionModel Question { get ; set; }
    }
}
