using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}