
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
            Title = title;
            Genre = genre;
        }
        public Movie() { }
    }
}
