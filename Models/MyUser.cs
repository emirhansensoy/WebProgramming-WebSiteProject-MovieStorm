using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class MyUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
