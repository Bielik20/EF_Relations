using EF_Relations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Relations.DAL.Managers
{
    public static class GenreManager
    {
        public static List<Genre> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Genres.OrderBy(x => x.Name).ToList();
            }
        }

        public static Genre GetByID(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Genres.Where(x => x.Id == id).First();
            }
        }

        public static List<Genre> GetForMovie(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Movies.Where(x => x.Id == id).First().Genres.ToList();
            }
        }
    }
}