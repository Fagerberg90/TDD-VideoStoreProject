using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    public class Movie
    {
        public string MovieTitle { get; set; }

        public Movie(string movieTitle)
        {
            this.MovieTitle = movieTitle;
        }

    }
}
