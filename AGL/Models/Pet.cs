using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGL.Models
{
    public class Pet
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class RootObject
    {
        public string name { get; set; }

        [Required]
        public string gender { get; set; }
        public int age { get; set; }
        public List<Pet> pets { get; set; }
    }
}