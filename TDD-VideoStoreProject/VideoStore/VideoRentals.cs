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
            var newRental = new Rental(DateTime.Now.AddDays(3), movieTitle, socialSecurityNumber);

            Rentals.Add(newRental);


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
                throw new CustomerDoesNotHaveAnyRentals();
            }


            



        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {

        }
    }
}
