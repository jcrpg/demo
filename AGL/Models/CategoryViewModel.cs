using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGL.Models
{
    public class CategoryViewModel
    {
        public string gender { get; set; }
        public List<List<Pet>> pets { get; set; }

    }
}