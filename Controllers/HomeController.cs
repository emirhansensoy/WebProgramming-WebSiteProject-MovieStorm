using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Movies(int? id)
        {
            //var movies = MovieRepository.Movies;
            var movies = _context.Movies.ToList();

            if(id!=null)
            {
                movies = movies.Where(i => i.CategoryId == id).ToList();
            }

            return View(movies);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            MovieAndReviewsModel model = new MovieAndReviewsModel();
            
            var movie = _context.Movies.First(i => i.Id == id); 
            
            model.Movie = movie;
            model.Reviews = _context.Reviews.Where(i => i.MovieId == movie.Id).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Details(int id,Review review)
        {
            if (User.Identity != null) review.UserName = User.Identity.Name;
            review.MovieId = id;
            _context.Reviews.Add(review);
            _context.SaveChanges();

            MovieAndReviewsModel model = new MovieAndReviewsModel();

            var movies = _context.Movies.ToList();
            var movie = movies.First(i => i.Id == id);
            
            model.Movie = movie;
            model.Reviews = _context.Reviews.Where(i => i.MovieId == movie.Id).ToList();

            return View(model);
        }
        
        public IActionResult Delete(int id)
        {
            var _review = _context.Reviews.Find(id);
            _context.Reviews.Remove(_review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Home", new { id = _review.MovieId });
        }
    }
}
