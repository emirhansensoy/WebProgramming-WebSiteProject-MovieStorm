using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Data;

namespace WebSite.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            if(RouteData.Values["action"].ToString() == "Movies")
            {
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            }
            
            return View(_context.Categories.ToList());
        }
    }
}
