using System;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }
    }
}
