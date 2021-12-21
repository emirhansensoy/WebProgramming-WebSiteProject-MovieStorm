using Microsoft.AspNetCore.Identity;

namespace WebSite.Models
{
    public class MyUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
