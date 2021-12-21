using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
