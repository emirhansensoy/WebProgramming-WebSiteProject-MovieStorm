using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }
    }
}
