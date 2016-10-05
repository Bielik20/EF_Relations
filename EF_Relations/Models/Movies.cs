using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RunningTime { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}