using System.Collections.Generic;

namespace WebSite.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public int CategoryId { get; set; }

        public float ImdbRating { get; set; }

        public float MovieStormRating { get; set; }
    }
}
