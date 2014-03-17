using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Models
{
    public class PersistentEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}