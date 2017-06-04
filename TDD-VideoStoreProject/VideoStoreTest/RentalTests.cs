using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreBL;
using NSubstitute;

namespace VideoStoreTest
{

    public class RentalTests
    {
        private IRentals sut;
        private IDateTime dateTime;

        [SetUp]
        public void Setup()
        {
            dateTime = Substitute.For<IDateTime>();
            dateTime.Now().Returns(dateTime.Now());
            sut = new VideoRentals(dateTime);

        }

        [Test]
        public void CanAddRental()
        {
            sut.AddRental("Transporter", "1988-02-15");
            var rentals = sut.GetRentalsFor("1988-02-15");

            Assert.AreEqual(rentals.Count, 1);
        }
        //Refactor later?
        [Test]
        public void GetBackMoviesAfterthreeDays()
        {
            sut.AddRental("Transporter2", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            var expected = DateTime.Now.AddDays(3).Date;
            Assert.AreEqual(expected, rentals[0].DueDate.Date);
        }

        [Test]
        public void GetRentalsBySsn()
        {
            sut.AddRental("Transporter2", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals[0].MovieTitle, "Transporter2");
        }

        [Test]
        public void CustomerCanRentMoreThanOneMovie()
        {
            sut.AddRental("Transporter2", "1988-02-15");
            sut.AddRental("Transporter3", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals.Count, 2);
        }

        [Test]
        public void CustomerCanNotRentMoreThanThreeMovies()
        {
            sut.AddRental("Transporter2", "1988-02-15");
            sut.AddRental("Transporter3", "1988-02-15");
            sut.AddRental("Transporter4", "1988-02-15");

            Assert.Throws<TooManyRentalsException>(() => {
                sut.AddRental("Transporter5", "1988-02-15");
            });
        }

        [Test]
        public void CustomerCanNotRentTwoCopiesOfSameMovie()
        {
            sut.AddRental("Transporter2", "1988-02-15");
 
            Assert.Throws<RentTwoCopiesOfSameMovieException>(() => {
                sut.AddRental("Transporter2", "1988-02-15");
            });
        }

        [Test]
        public void CustomerWithLateDueDateCanNotRentNewMovie()
        {
            sut.AddRental("Die hard", "19880606");
            var rentals = sut.GetRentalsFor("19880606");
            rentals[0].DueDate = DateTime.Now.AddDays(-4);

            Assert.Throws<LateRentalsReturnException>(() =>
            {
                sut.AddRental("Die hard2", "19880606");
            });
        }
    }

}
