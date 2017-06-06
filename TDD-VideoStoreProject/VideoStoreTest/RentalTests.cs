using NUnit.Framework;
using System;
using VideoStoreBL;
using NSubstitute;

namespace VideoStoreTest
{

    public class RentalTests
    {
        private IRentals _sut;
        private IDateTime _dateTime;

        [SetUp]
        public void Setup()
        {
            _dateTime = Substitute.For<IDateTime>();
            _dateTime.Now().Returns(DateTime.Now);
            _sut = new VideoRentals(_dateTime);

        }

        [Test]
        public void CanAddRental()
        {
            _sut.AddRental("Avatar", "1988-02-15");
            var rentals = _sut.GetRentalsFor("1988-02-15");

            Assert.AreEqual(rentals.Count, 1);
            
        }
      
        [Test]
        public void GetBackMoviesAfterthreeDays()
        {
            _sut.AddRental("Avatar2", "1988-02-15");

            var rentals = _sut.GetRentalsFor("1988-02-15");
            var expected = _dateTime.Now().AddDays(3).Date;
            Assert.AreEqual(expected, rentals[0].DueDate.Date);
        }

        [Test]
        public void GetRentalsBySsn()
        {
            _sut.AddRental("Avatar2", "1988-02-15");

            var rentals = _sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals[0].MovieTitle, "Avatar2");
        }

        [Test]
        public void CustomerCanRentMoreThanOneMovie()
        {
            _sut.AddRental("Avatar2", "1988-02-15");
            _sut.AddRental("Avatar3", "1988-02-15");

            var rentals = _sut.GetRentalsFor("1988-02-15");
            Assert.AreEqual(rentals.Count, 2);
        }

        [Test]
        public void CustomerCanNotRentMoreThanThreeMovies()
        {
            _sut.AddRental("Avatar2", "1988-02-15");
            _sut.AddRental("Avatar3", "1988-02-15");
            _sut.AddRental("Avatar4", "1988-02-15");

            Assert.Throws<TooManyRentalsException>(() => {
                _sut.AddRental("Avatar5", "1988-02-15");
            });
        }

        [Test]
        public void CustomerCanNotRentTwoCopiesOfSameMovie()
        {
            _sut.AddRental("Avatar2", "1988-02-15");
 
            Assert.Throws<RentTwoCopiesOfSameMovieException>(() => {
                _sut.AddRental("Avatar2", "1988-02-15");
            });
        }

        [Test]
        public void CustomerWithLateDueDateCanNotRentNewMovie()
        {
            _sut.AddRental("Die hard", "19880606");
            var rentals = _sut.GetRentalsFor("19880606");
            rentals[0].DueDate = DateTime.Now.AddDays(-4);

            Assert.Throws<LateRentalsReturnException>(() =>
            {
                _sut.AddRental("Die hard2", "19880606");
            });
        }
    }

}
