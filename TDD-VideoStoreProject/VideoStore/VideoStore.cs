using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class VideoStore : IVideoStore
    {
        public List<Movie> MoviesList { get; set; }
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
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
            if (MoviesList == null)
            {
                MoviesList = new List<Movie>();
            }
            if (MoviesList.Where(a => a.Title == movie.Title).ToList().Count >= 3)
            {
                throw new TooManyIdenticalMoviesException();
            }
            else
            {
                MoviesList.Add(movie);
            }
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(Customer customer)
        {
            if (CustomerList.Any(x => x.Ssn == customer.Ssn))
            {
                throw new DuplicateCustomerException();
            }
            else
            {
                CustomerList.Add(customer);
            }
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
