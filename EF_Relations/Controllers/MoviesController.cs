using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EF_Relations.Models;
using System.Threading.Tasks;
using EF_Relations.DAL.Managers;
using EF_Relations.ViewModels.MovieVM;

namespace EF_Relations.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        public async Task<ActionResult> Containers()
        {
            return View(await MovieContainerManager.GetAll());
        }

        public ActionResult CreateContainer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContainer(MovieContainer movieContainer)
        {
            if (ModelState.IsValid)
            {
                db.MovieContainers.Add(movieContainer);
                db.SaveChanges();
                return RedirectToAction("Containers");
            }

            return View(movieContainer);
        }

        public async Task<ActionResult> DetailsContainer(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var container = await MovieContainerManager.GetById(id);
            if (container == null)
            {
                return HttpNotFound();
            }
            return View(container);
        }

        public ActionResult CreateMovie(int containerId)
        {
            var model = new CreateMovieVM();
            model.Id = containerId;
            model.ReleaseDate = DateTime.Today;

            var allGenres = GenreManager.GetAll();
            foreach (var genre in allGenres)
            {
                model.Genres.Add(new CheckBoxListItem()
                {
                    Id = genre.Id,
                    Display = genre.Name,
                    IsChecked = false
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie(CreateMovieVM model)
        {
            if (ModelState.IsValid)
            {
                await MovieManager.Add(model);
                return RedirectToAction("DetailsContainer", new { id = model.Id });
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteContainer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var container = await MovieContainerManager.GetById((int)id);
            if (container == null)
            {
                return HttpNotFound();
            }

            return View(container);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("DeleteContainer")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteContainerConfirmed(int id)
        {
            await MovieContainerManager.DeleteContainer(id);
            
            return RedirectToAction("Containers");
        }



        //---------------------------------


        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,RunningTime")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
