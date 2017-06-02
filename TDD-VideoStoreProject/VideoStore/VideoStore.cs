using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class VideoStore : IVideoStore
    {
        public List<Movie> MoviesList { get; set; }

        public IRentals Irentals { get; set; }

        

        public VideoStore(IRentals irental)
        {
            this.Irentals = irental;   
        }


        public void AddMovie(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Title))
            {
                throw new MovieTitleEmptyException();
            }
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}
