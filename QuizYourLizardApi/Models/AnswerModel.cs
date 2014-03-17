using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace QuizYourLizardApi.Models
{
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
