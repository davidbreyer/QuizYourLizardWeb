﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace QuizYourLizardApi.Models
{
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
    }
}