using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Data;
using WebSite.Models;

namespace WebSite.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

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
    }
}
