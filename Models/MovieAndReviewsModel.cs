using System.Collections.Generic;

namespace WebSite.Models
{
    public class MovieAndReviewsModel
    {
        public Movie Movie { get; set; }

        public Review Review { get; set; }
        public IList<Review> Reviews { get; set; }
    }
}
