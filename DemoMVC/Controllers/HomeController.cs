using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVC.Context;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class HomeController : Controller
    {
        public EFDemoContext context = null;

        public string search;

        public HomeController(EFDemoContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Filter(Filter filter)
        {
            search = filter.search.ToLower();

            if(filter.option == "Title")
            {

                if (filter.order == "asc")
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.Title.ToLower().Contains(search)
                        orderby EFDemo.Title
                        select EFDemo;

                    return View("Movies", movies);
                }
                else
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.Title.ToLower().Contains(search)
                        orderby EFDemo.Title descending
                        select EFDemo;

                    return View("Movies", movies);
                }
            } else if (filter.option == "Genre")
            {
                if (filter.order == "asc")
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.Genre.ToLower().Contains(search)
                        orderby EFDemo.Genre
                        select EFDemo;

                    return View("Movies", movies);
                }
                else
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.Genre.ToLower().Contains(search)
                        orderby EFDemo.Genre descending
                        select EFDemo;

                    return View("Movies", movies);
                }
            }
            else
            {
                if (filter.order == "asc")
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.ReleaseYear == Int32.Parse(search)
                        orderby EFDemo.ReleaseYear
                        select EFDemo;

                    return View("Movies", movies);
                }
                else
                {
                    var movies =
                        from EFDemo in context.Movies
                        where EFDemo.ReleaseYear == Int32.Parse(search)
                        orderby EFDemo.ReleaseYear descending
                        select EFDemo;

                    return View("Movies", movies);
                }
            }
        }

        public IActionResult Detailss(int Id)
        {
            var movie = context.Movies
                .Where(m => m.Id == Id)
                .FirstOrDefault();

            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(Movie movie)
        {
            context.Add(movie);
            context.SaveChanges();
            var movies = context.Movies.ToList();

            return View("Movies", movies);
        }

        public IActionResult Delete(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            var movies = context.Movies.ToList();

            return View("Movies", movies);
        }

        public IActionResult UpdateForm(Movie movie)
        {
            return View(movie);
        }

        public IActionResult Update(Movie movie, int Id)
        {
            var original = context.Movies
                .Where(m => m.Id == Id)
                .FirstOrDefault();

            context.Entry(original).CurrentValues.SetValues(movie);

            var movies = context.Movies.ToList();

            return View("Movies", movies);
        }

        public IActionResult Back()
        {
            var movies = context.Movies
                .Where(p => p.Title.ToLower().Contains(search))
                .ToList();

            return View("Index");
        }

        public IActionResult Movies()
        {
            var movies = context.Movies.ToList();

            return View(movies);
        }
    }
}