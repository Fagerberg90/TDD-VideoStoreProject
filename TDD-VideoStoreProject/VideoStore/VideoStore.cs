using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace VideoStoreBL
{
    public class VideoStore : IVideoStore
    {
        public List<Movie> MoviesList { get; set; } = new List<Movie>();
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public IRentals Irentals { get; set; }



        public VideoStore(IRentals irental)
        {
            Irentals = irental;
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
            return CustomerList;
        }

        public void RegisterCustomer(Customer customer)
        {
            Regex rex = new Regex(@"\d{4}-\d{2}-\d{2}");
            if (!rex.IsMatch(customer.Ssn))
            {
                throw new NotvalidSsnException();
            }
            if (CustomerList.Any(x => x.Ssn == customer.Ssn))
            {
                throw new DuplicateCustomerException();
            }
            CustomerList.Add(customer);

        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {


            if (MoviesList.Any(x => x.Title == movieTitle))
            {
                var rex = new Regex(@"\d{4}-\d{2}-\d{2}");
                if (!rex.IsMatch(socialSecurityNumber))
                {
                    throw new NotvalidSsnException();
                }

                if (CustomerList.Any(x => x.Ssn == socialSecurityNumber))
                {

                    Irentals.AddRental(movieTitle, socialSecurityNumber);

                }
                else
                {
                    throw new CustomerDoesNotExistException();
                }
            }
            else
            {
                throw new MovieDoesNotExistException();

            }
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            if (MoviesList.Any(x => x.Title == movieTitle))
            {
                var rex = new Regex(@"\d{4}-\d{2}-\d{2}");
                if (!rex.IsMatch(socialSecurityNumber))
                {
                    throw new NotvalidSsnException();
                }

                if (CustomerList.Any(x => x.Ssn == socialSecurityNumber))
                {

                    Irentals.RemoveRental(movieTitle, socialSecurityNumber);

                }
                else
                {
                    throw new CustomerDoesNotExistException();
                }
            }
            else
            {
                throw new MovieDoesNotExistException();

            }
        }
    }
}
