using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Localization;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHtmlLocalizer<ReviewController> _localizer;

        public ReviewController(ApplicationDbContext context,
            IHtmlLocalizer<ReviewController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["MyReviews"] = _localizer["MyReviews"];
            ViewData["NoReviews"] = _localizer["NoReviews"];
            ViewData["Delete"] = _localizer["Delete"];
            
            MovieAndReviewsModel model = new MovieAndReviewsModel();

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
