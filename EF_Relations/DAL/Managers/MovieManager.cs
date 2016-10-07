using EF_Relations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using EF_Relations.ViewModels.MovieVM;
using System.Threading.Tasks;

namespace EF_Relations.DAL.Managers
{
    public static class MovieManager
    {
        public static List<Movie> GetAll()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movies = context.FullQuery().OrderBy(x => x.Title).ToList();
                return movies;
            }
        }

        public static Movie GetById(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movie = context.FullQuery().First(m => m.Id == id);
                return movie;
            }
        }

        private static IQueryable<Movie> FullQuery(this ApplicationDbContext context)
        {
            return context.Movies
                .Include(m => m.Genres);
        }

        public static async Task Add(CreateMovieVM model)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movie = new Movie()
                {
                    Id = model.Id,
                    Title = model.Title,
                    ReleaseDate = model.ReleaseDate,
                    RunningTime = model.RunningTime,
                };

                var selectedGenres = model.Genres.Where(x => x.IsChecked).Select(x => x.Id).ToList();
                foreach (var genreId in selectedGenres)
                {
                    var genre = await context.Genres.FindAsync(genreId);
                    movie.Genres.Add(genre);
                }
                context.Movies.Add(movie);

                await context.SaveChangesAsync();
            }
        }


        public static void Edit(int id, string title, DateTime releaseDate, decimal price, List<int> genres)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movie = context.Movies.Where(x => x.Id == id).First();
                movie.Title = title;
                movie.ReleaseDate = releaseDate;
                movie.Genres.Clear();
                foreach (var genreID in genres)
                {
                    var genre = context.Genres.Find(genreID);
                    movie.Genres.Add(genre);
                }

                context.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var movie = context.Movies.Where(x => x.Id == id).First();
                context.Movies.Remove(movie);
                context.SaveChanges();
            }
        }
    }
}