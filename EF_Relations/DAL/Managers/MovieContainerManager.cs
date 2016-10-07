using EF_Relations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Threading.Tasks;

namespace EF_Relations.DAL.Managers
{
    public static class MovieContainerManager
    {
        public static async Task<List<MovieContainer>> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movieContainers = await context.FullQuery().OrderBy(mc => mc.Id).ToListAsync();
                return movieContainers;
            }
        }

        public static async Task<MovieContainer> GetById(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movieContainer = await context.FullQuery().FirstAsync(mc => mc.Id == id);
                return movieContainer;
            }
        }

        public static async Task AddMovie(int id, Movie movie)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var container = await MovieContainerManager.GetById(id);
                if (container.Movie != null)
                {
                    return;
                }
                container.Movie = movie;
                await context.SaveChangesAsync();
            }
        }


        private static IQueryable<MovieContainer> FullQuery(this ApplicationDbContext context)
        {
            return context.MovieContainers
                .Include(mc => mc.Movie)
                .Include(mc => mc.Movie.Genres);
        }
    }
}