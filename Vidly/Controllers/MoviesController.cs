using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie { Name = "Shrek!" };
            //below listed are types of diffrent return types in actionresult

            //return View(movie);
            // return Content("HARI");
            // return HttpNotFound();
            //return new EmptyResult();
            //redirects to index action in home controller with query string ?page=1&sortby=name
            return RedirectToAction("Index", "Home",new {page=1,sortby="name"});
        }

        //learning paramater to action
        public ActionResult edit(int id)
        {

            return Content("id=" + id);
        }

        //learning multiple param to action old method we should not do this way we need to replace it with custom routue
        public ActionResult Index(int? pageindex,string sortby)
        {
            if (!pageindex.HasValue)
                pageindex = 1;
            if (string.IsNullOrWhiteSpace(sortby))
                sortby = "Name";

            return Content(string.Format("pageindex={0}&sortby{1}", pageindex, sortby));

        }

        //[Route("Movies/released/{year}/{month}")]
        // [Route("Movies/released/{year}/{month:regex(\\d{2})}")]
        [Route("Movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content(year + "/" + month);
        }
    }
}