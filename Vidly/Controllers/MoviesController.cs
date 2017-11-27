using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _dbcontext;
        public MoviesController()
        {
             _dbcontext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbcontext.Dispose();
        }

        [HttpPost]
        public ActionResult Save(MovieFormViewModel objViewModel)
        {
            if(objViewModel.Movie.Id==0)
            {
                _dbcontext.Movies.Add(objViewModel.Movie);
            }
            else
            {
                var MovieinDB = _dbcontext.Movies.Single(c => c.Id == objViewModel.Movie.Id);
                MovieinDB.Name = objViewModel.Movie.Name;
                MovieinDB.MovieReleaseDate = objViewModel.Movie.MovieReleaseDate;
                MovieinDB.GenreId = objViewModel.Movie.GenreId;
                MovieinDB.NumberInStocks = objViewModel.Movie.NumberInStocks;

            }

            try
            {
                _dbcontext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw;
            }
            
            return RedirectToAction("Index","Movies");
        }

        public ActionResult MovieForm()
        {
            var Genre = _dbcontext.Genre.ToList();
            var MovieFormViewModel = new MovieFormViewModel {
                Genre=Genre
            };
            return View("MovieForm",MovieFormViewModel);
        }
        public ActionResult Index()
        {
            var movies = _dbcontext.Movies.Include(c=>c.Genre).ToList();
            return View(movies);
        }

        [Route("Movies/details/{id}")]
        public ActionResult Details(int? id)
        {
            var movies = _dbcontext.Movies.Include(c => c.Genre).SingleOrDefault(row => row.Id == id);
            if (movies == null)
                return HttpNotFound();
            return View(movies);
        }
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek!" };

            var customer = new List<Customer>
            {
                new Customer {Name="Hari",Id=1 },
                new Customer {Name="Anna",Id=2 }

            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customer
            };

            return View(viewModel);
            //return View(movie);
            /*
             Methods to pass into view but not good

            ViewData["Movies"] = movie;
            ViewBag.Movie = movie;

             */

            /*
            below listed are types of diffrent return types in actionresult
            // return Content("HARI");
            // return HttpNotFound();
            //return new EmptyResult();
            //redirects to index action in home controller with query string ?page=1&sortby=name
            //return RedirectToAction("Index", "Home",new {page=1,sortby="name"});
            */
        }

        //learning paramater to action
        public ActionResult Edit(int id)
        {
            var Movies = _dbcontext.Movies.SingleOrDefault(c => c.Id == id);

            if (Movies == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = Movies,
                Genre = _dbcontext.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        ////learning multiple param to action old method we should not do this way we need to replace it with custom routue
        //public ActionResult Index(int? pageindex,string sortby)
        //{
        //    if (!pageindex.HasValue)
        //        pageindex = 1;
        //    if (string.IsNullOrWhiteSpace(sortby))
        //        sortby = "Name";

        //    return Content(string.Format("pageindex={0}&sortby{1}", pageindex, sortby));

        //}

        //[Route("Movies/released/{year}/{month}")]
        // [Route("Movies/released/{year}/{month:regex(\\d{2})}")]
        [Route("Movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content(year + "/" + month);
        }
    }
}