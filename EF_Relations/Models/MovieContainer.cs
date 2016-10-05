using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class MovieContainer
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Movie Movie { get; set; }
    }
}