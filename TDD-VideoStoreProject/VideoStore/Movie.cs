using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{

    public enum MovieGenre
    {
        Comedy, Action, Thriller, Documentary, SciFi
    }
    public class Movie
    {
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public Movie(string title, MovieGenre genre)
        {
            this.Title = title;
            this.Genre = genre;
        }
        public Movie() { }
    }
}
