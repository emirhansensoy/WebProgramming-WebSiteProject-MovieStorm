using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using WebSite.Data;

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
            var userName = User.FindFirstValue(ClaimTypes.Name);

            return View(_context.Reviews.Where(i => i.UserName == userName).ToList());
        }
    }
}
