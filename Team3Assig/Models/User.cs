using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Models
{
    public class User 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Roles { get; set; }
    }
}
