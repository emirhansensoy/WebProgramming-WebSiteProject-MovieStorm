using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSite.Data;
using WebSite.Models;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IHtmlLocalizer<AdminController> _localizer;

        public AdminController(RoleManager<IdentityRole> roleManager,
                                        ApplicationDbContext context,
                                        IHtmlLocalizer<AdminController> localizer)
        {
            _roleManager = roleManager;
            _context = context;
            _localizer = localizer;
        }

        public IActionResult ListMovies()
        {
            ViewData["AddMovie"] = _localizer["AddMovie"];
            ViewData["ListMovies"] = _localizer["ListMovies"];
            ViewData["Update"] = _localizer["Update"];
            ViewData["Details"] = _localizer["Details"];
            ViewData["Delete"] = _localizer["Delete"];
            ViewData["Title"] = _localizer["Title"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["ImageURL"] = _localizer["ImageURL"];
            ViewData["CategoryId"] = _localizer["CategoryId"];
            ViewData["ImdbRating"] = _localizer["ImdbRating"];
            ViewData["MovieStormRating"] = _localizer["MovieStormRating"];

            return View(_context.Movies);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewData["AddMovie"] = _localizer["AddMovie"];
            ViewData["BackToList"] = _localizer["BackToList"];
            ViewData["Title"] = _localizer["Title"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["ImageURL"] = _localizer["ImageURL"];
            ViewData["CategoryId"] = _localizer["CategoryId"];
            ViewData["ImdbRating"] = _localizer["ImdbRating"];
            ViewData["MovieStormRating"] = _localizer["MovieStormRating"];
            ViewData["Create"] = _localizer["Create"];

            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            ViewData["AddMovie"] = _localizer["AddMovie"];
            ViewData["BackToList"] = _localizer["BackToList"];
            ViewData["Title"] = _localizer["Title"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["ImageURL"] = _localizer["ImageURL"];
            ViewData["CategoryId"] = _localizer["CategoryId"];
            ViewData["ImdbRating"] = _localizer["ImdbRating"];
            ViewData["MovieStormRating"] = _localizer["MovieStormRating"];
            ViewData["Create"] = _localizer["Create"];

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction("ListMovies", "Admin");
        }
        
        public IActionResult DeleteMovie(int id)
        {
            var _movie = _context.Movies.Find(id);
            _context.Movies.Remove(_movie);
            _context.SaveChanges();

            return RedirectToAction("ListMovies", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateMovie(int id) 
        {
            ViewData["UpdateMovie"] = _localizer["UpdateMovie"];
            ViewData["BackToList"] = _localizer["BackToList"];
            ViewData["Save"] = _localizer["Save"];
            ViewData["Title"] = _localizer["Title"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["ImageURL"] = _localizer["ImageURL"];
            ViewData["CategoryId"] = _localizer["CategoryId"];
            ViewData["ImdbRating"] = _localizer["ImdbRating"];
            ViewData["MovieStormRating"] = _localizer["MovieStormRating"];

            var model = _context.Movies.First(i=>i.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult UpdateMovie(int id, Movie movie)
        {
            ViewData["UpdateMovie"] = _localizer["UpdateMovie"];
            ViewData["BackToList"] = _localizer["BackToList"];
            ViewData["Save"] = _localizer["Save"];
            ViewData["Title"] = _localizer["Title"];
            ViewData["Description"] = _localizer["Description"];
            ViewData["ImageURL"] = _localizer["ImageURL"];
            ViewData["CategoryId"] = _localizer["CategoryId"];
            ViewData["ImdbRating"] = _localizer["ImdbRating"];
            ViewData["MovieStormRating"] = _localizer["MovieStormRating"];

            var model = _context.Movies.First(x => x.Id == id); 
            
            model.CategoryId = movie.CategoryId;
            model.Id = movie.Id;
            model.ImageURL = movie.ImageURL;
            model.Title = movie.Title;
            model.Description = movie.Description;
            model.ImdbRating = movie.ImdbRating;
            model.MovieStormRating = movie.MovieStormRating;
            _context.SaveChanges();

            return RedirectToAction("ListMovies", "Admin");
        }
    }
}
