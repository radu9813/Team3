using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string EmailAddress { get; set; }

        public virtual Diploma Diploma { get; set; }

        public int GetAge()
        {
            int age = DateTime.Now.Subtract(Birthdate).Days;

            return age / 365;
        }
    }
}
