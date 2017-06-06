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
            dateTime.Now().Returns(DateTime.Now);
            sut = new VideoRentals(dateTime);

        }

        [Test]
        public void CanAddRental()
        {
            sut.AddRental("Avatar", "1988-02-15");
            var rentals = sut.GetRentalsFor("1988-02-15");

            Assert.AreEqual(rentals.Count, 1);
        }
      
        [Test]
        public void GetBackMoviesAfterthreeDays()
        {
            sut.AddRental("Avatar2", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            var expected = dateTime.Now().AddDays(3).Date;
            Assert.AreEqual(expected, rentals[0].DueDate.Date);
        }

        [Test]
        public void GetRentalsBySsn()
        {
            sut.AddRental("Avatar2", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals[0].MovieTitle, "Avatar2");
        }

        [Test]
        public void CustomerCanRentMoreThanOneMovie()
        {
            sut.AddRental("Avatar2", "1988-02-15");
            sut.AddRental("Avatar3", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals.Count, 2);
        }

        [Test]
        public void CustomerCanNotRentMoreThanThreeMovies()
        {
            sut.AddRental("Avatar2", "1988-02-15");
            sut.AddRental("Avatar3", "1988-02-15");
            sut.AddRental("Avatar4", "1988-02-15");

            Assert.Throws<TooManyRentalsException>(() => {
                sut.AddRental("Avatar5", "1988-02-15");
            });
        }

        [Test]
        public void CustomerCanNotRentTwoCopiesOfSameMovie()
        {
            sut.AddRental("Avatar2", "1988-02-15");
 
            Assert.Throws<RentTwoCopiesOfSameMovieException>(() => {
                sut.AddRental("Avatar2", "1988-02-15");
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
