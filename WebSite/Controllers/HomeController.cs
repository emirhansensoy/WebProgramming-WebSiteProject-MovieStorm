using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlLocalizer<HomeController> _localizer;
        public HomeController(ApplicationDbContext context,
            IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["WelcomeToMovieStorm"] = _localizer["WelcomeToMovieStorm"];
            ViewData["AboutMovieStorm"] = _localizer["AboutMovieStorm"];
            ViewData["AboutMovieStormContent"] = _localizer["AboutMovieStormContent"];

            return View();
        }

        public IActionResult Movies(int? id)
        {
            var movies = new List<Movie>();

            if (_context.Movies.Any())
            {
                movies = _context.Movies.ToList();

                if(id != null)
                {
                    movies = movies.Where(i => i.CategoryId == id).ToList();
                }
            }

            return View(movies);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewData["Reviews"] = _localizer["Reviews"];
            ViewData["NoReviews"] = _localizer["NoReviews"];
            ViewData["LeaveAReview"] = _localizer["LeaveAReview"];
            ViewData["Comment"] = _localizer["Comment"];
            ViewData["Rating"] = _localizer["Rating"];
            ViewData["Submit"] = _localizer["Submit"];
            ViewData["Delete"] = _localizer["Delete"];
            ViewData["Category"] = _localizer["Category"];
            ViewData["RatingOf"] = _localizer["RatingOf"];

            double msRating = 0;
            int sum = 0;
            if (_context.Reviews.Any(i => i.MovieId == id))
            {
                foreach (var item in _context.Reviews.Where(i=>i.MovieId == id))
                {
                    sum = sum + item.Rating;
                }
                msRating = (double)sum / (double)_context.Reviews.Count(i => i.MovieId == id);
            }
            msRating = Math.Round(msRating, 1);

            _context.Movies.First(i=>i.Id == id).MovieStormRating = msRating;
            _context.SaveChanges();

            MovieAndReviewsModel model = new MovieAndReviewsModel();
            
            var movie = _context.Movies.First(i => i.Id == id); 
            
            
            model.Movie = movie;
            model.Reviews = _context.Reviews.Where(i => i.MovieId == movie.Id).ToList();
            model.Categories = _context.Categories.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int id,Review review)
        {
            ViewData["Reviews"] = _localizer["Reviews"];
            ViewData["NoReviews"] = _localizer["NoReviews"];
            ViewData["LeaveAReview"] = _localizer["LeaveAReview"];
            ViewData["Comment"] = _localizer["Comment"];
            ViewData["Rating"] = _localizer["Rating"];
            ViewData["Submit"] = _localizer["Submit"];
            ViewData["Delete"] = _localizer["Delete"];
            ViewData["Category"] = _localizer["Category"];
            ViewData["RatingOf"] = _localizer["RatingOf"];

            if (User.Identity != null) review.UserName = User.Identity.Name;
            review.MovieId = id;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            
            double msRating = 0;
            int sum = 0;
            if (_context.Reviews.Any(i => i.MovieId == id))
            {
                foreach (var item in _context.Reviews.Where(i=>i.MovieId == id))
                {
                    sum = sum + item.Rating;
                }
                msRating = (double)sum / (double)_context.Reviews.Count(i => i.MovieId == id);
            }
            msRating = Math.Round(msRating, 1);
            
            _context.Movies.First(i=>i.Id == id).MovieStormRating = msRating;
            _context.SaveChanges();

            MovieAndReviewsModel model = new MovieAndReviewsModel();

            var movies = _context.Movies.ToList();
            var movie = movies.First(i => i.Id == id);
            
            model.Movie = movie;
            model.Reviews = _context.Reviews.Where(i => i.MovieId == movie.Id).ToList();
            model.Categories = _context.Categories.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(30)
                });

            return LocalRedirect(returnUrl);
        }

        public IActionResult DeleteFromDetails(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();

            double msRating = 0;
            int sum = 0;
            if (_context.Reviews.Any(i => i.MovieId == review.MovieId))
            {
                foreach (var item in _context.Reviews.Where(i=>i.MovieId == review.MovieId))
                {
                    sum += item.Rating;
                }
                msRating = (double)sum / (double)_context.Reviews.Count(i => i.MovieId == review.MovieId);
            }
            msRating = Math.Round(msRating, 1);
            
            _context.Movies.First(i=>i.Id == review.MovieId).MovieStormRating = msRating;
            _context.SaveChanges();

            return RedirectToAction("Details", "Home", new { id = review.MovieId });
        }

        public IActionResult DeleteFromMyReviews(int id)
        {
            var review = _context.Reviews.Find(id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();

            double msRating = 0;
            int sum = 0;
            if (_context.Reviews.Any(i => i.MovieId == review.MovieId))
            {
                foreach (var item in _context.Reviews.Where(i=>i.MovieId == review.MovieId))
                {
                    sum += item.Rating;
                }
                msRating = (double)sum / (double)_context.Reviews.Count(i => i.MovieId == review.MovieId);
            }
            msRating = Math.Round(msRating, 1);
            
            _context.Movies.First(i=>i.Id == review.MovieId).MovieStormRating = msRating;
            _context.SaveChanges();

            return RedirectToAction("Index", "Review");
        }

        public IActionResult ApiPage()
        {
            ViewData["GetMovies"] = _localizer["GetMovies"];
            ViewData["GetMovieById"] = _localizer["GetMovieById"];
            ViewData["GetReviews"] = _localizer["GetReviews"];
            ViewData["GetReviewById"] = _localizer["GetReviewById"];
            ViewData["APIKeys"] = _localizer["APIKeys"];

            int? portNumber = Request.Host.Port;
            
            return View(portNumber);
        }
    }
}
