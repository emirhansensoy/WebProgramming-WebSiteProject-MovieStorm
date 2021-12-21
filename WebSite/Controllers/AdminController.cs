using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(RoleManager<IdentityRole> roleManager,
                                        ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Role role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);

            if(!roleExist)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }

            return View();
        }

        public IActionResult ListMovies()
        {
            return View(_context.Movies);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
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
            var model = _context.Movies.First(i=>i.Id == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult UpdateMovie(int id, Movie movie)
        {
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
