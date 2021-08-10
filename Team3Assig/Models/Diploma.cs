using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Models
{
    public class Diploma
    {
        [ForeignKey("Student")]
        public int DiplomaId { get; set; }
        public string Thesis { get; set; }

        public string Abstract { get; set; }

        public bool Completeness { get; set; }

        public string Supervisor { get; set; }

        public virtual Student Student { get; set; }
    }
}
