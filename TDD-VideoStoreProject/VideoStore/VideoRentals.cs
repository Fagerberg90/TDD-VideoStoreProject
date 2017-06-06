using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class VideoRentals : IRentals
    {
        private List<Rental> Rentals { get; set; }
        private IDateTime dateTime;
        public VideoRentals(IDateTime dateTime)
        {
            this.dateTime = dateTime;
            Rentals = new List<Rental>();
        }

        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            try
            {
                if (GetRentalsFor(socialSecurityNumber).Any(x => x.DueDate.Date <= DateTime.Now.Date))
                {
                    throw new LateRentalsReturnException(ExeptionMessages.LateRentalsReturnExceptionMessage);
                }
                if (GetRentalsFor(socialSecurityNumber).Any(x => x.MovieTitle == movieTitle))
                {
                    throw new RentTwoCopiesOfSameMovieException();
                }
                if (GetRentalsFor(socialSecurityNumber).Count >= 3)
                {
                    throw new TooManyRentalsException();
                }
                else
                {
                    var newRental = new Rental(DateTime.Now.AddDays(3), movieTitle, socialSecurityNumber);

                    Rentals.Add(newRental);
                }
            }
            catch (CustomerDoesNotHaveAnyRentalsException)
            {
                var newRental = new Rental(DateTime.Now.AddDays(3), movieTitle, socialSecurityNumber);

                Rentals.Add(newRental);
            }
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            if (Rentals.Any(x => x.CustomerSsn == socialSecurityNumber))
            {
                var result = Rentals.Where(x => x.CustomerSsn == socialSecurityNumber).ToList();
                return result;
            }
            else
            {
                throw new CustomerDoesNotHaveAnyRentalsException();
            }
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            var rentalToBeRemoved =
                Rentals.FirstOrDefault(x => x.CustomerSsn == socialSecurityNumber && x.MovieTitle == movieTitle);

            Rentals.Remove(rentalToBeRemoved);
        }
    }
}
