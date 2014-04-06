using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Pocos
{
    public abstract class ClientEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public abstract string ApiUri { get; }
    }
}