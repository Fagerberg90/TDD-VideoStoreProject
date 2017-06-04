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

        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
             var newRental = new Rental(DateTime.Now,movieTitle,socialSecurityNumber);

              Rentals.Add(newRental);


        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            
        }
    }
}
