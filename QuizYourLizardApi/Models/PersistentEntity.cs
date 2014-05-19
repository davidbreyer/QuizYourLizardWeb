using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuizYourLizardApi.Models
{
    public abstract class PersistentEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset Updated { get; set; }
        
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset Created { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public int SortOrder { get; set; }
    }
}