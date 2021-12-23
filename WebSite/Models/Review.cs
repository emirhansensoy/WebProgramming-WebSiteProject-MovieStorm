using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public int MovieId { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }
    }
}
