namespace EF_Relations.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF_Relations.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EF_Relations.Models.ApplicationDbContext";
        }

        protected override void Seed(EF_Relations.Models.ApplicationDbContext context)
        {
            // enable-migrations
            // add-migration <NAZWA>
            // update-database
            // Update-Database -TargetMigration $InitialDatabase

            var genres = new List<Genre>
            {
                new Genre { Name = "Classic" },
                new Genre { Name = "Commedy" },
                new Genre { Name = "Horror" }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            var containers = new List<MovieContainer>
            {
                new MovieContainer { Description = "This is original container for movie" },
                new MovieContainer { Description = "This is second container" },
                new MovieContainer { Description = "This third" }
            };
            context.MovieContainers.AddRange(containers);
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "Godfather",
                    RunningTime = 180,
                    ReleaseDate = DateTime.Parse("2005-09-01"),
                    Id = containers[2].Id
                },
                new Movie
                {
                    Title = "Rain",
                    RunningTime = 120,
                    ReleaseDate = DateTime.Parse("2002-06-01"),
                    MovieContainer = containers[1]
                },
                new Movie
                {
                    Title = "Gone with the wind",
                    RunningTime = 240,
                    ReleaseDate = DateTime.Parse("2003-08-01"),
                    MovieContainer = containers[0]
                }
            };
            context.Movies.AddRange(movies);
            context.SaveChanges();

            movies[0].Genres.Add(genres[0]);
            movies[0].Genres.Add(genres[1]);
            movies[2].Genres.Add(genres[2]);
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
