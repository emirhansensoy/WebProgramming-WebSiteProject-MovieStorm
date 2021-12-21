using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            MovieAndReviewsModel model = new MovieAndReviewsModel();
            /*
            model.Reviews = _context.Reviews.Where(i => i.UserName.Equals(User.Identity.Name)).ToList();

            model.Movies = new List<Movie>();

            foreach (var item in _context.Reviews.Where(i => i.UserName.Equals(User.Identity.Name)).ToList())
            {
                model.Movies.Add(_context.Movies.First(i => i.Id == item.MovieId));
            }
            */
            int movieCount = 0;
            var movieIdList = new List<int>();
            model.Reviews = _context.Reviews.Where(i => i.UserName.Equals(User.Identity.Name)).ToList();
            model.Movies = new List<Movie>();
            foreach (var x in model.Reviews)
            {
                if (!movieIdList.Contains(x.MovieId))
                {
                    movieIdList.Add(x.MovieId);
                    movieCount++;
                }
            }
            for(int i = 0; i < movieCount; i++)
            {
                model.Movies.Add(_context.Movies.First(x => x.Id == movieIdList[i]));
            }

            return View(model);
        }
    }
}
