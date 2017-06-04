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
    [TestFixture]
    public class VideoStoreTests
    {
        private IVideoStore sut;
        private IRentals rentals;
        private Movie TestMovie;
        private Customer TestCustomer;

        [SetUp]
        public void Setup()
        {
            rentals = Substitute.For<IRentals>();
            sut = new VideoStore(rentals);
            TestCustomer = new Customer(
                 "Olle",
                 "Pettersson",
                 "1850-09-22"
                );

            TestMovie = new Movie(
                "Star Wars",
                 MovieGenre.SciFi
                );
        }

        [Test]
        public void MovieTitleCannotBeEmty()
        {
            TestMovie.Title = "";

            Assert.Throws<MovieTitleEmptyException>(() => {
                sut.AddMovie(TestMovie);
            });
        }

        [Test]
        public void CannotAddMoreThanThreeIdenticalMovies()
        {
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            sut.AddMovie(TestMovie);
            Assert.Throws<TooManyIdenticalMoviesException>(() => {
                sut.AddMovie(TestMovie);
            });

        }

        [Test]
        public void NoDuplicatedCustomers()
        {
            sut.RegisterCustomer(TestCustomer);
            Assert.Throws<DuplicateCustomerException>(() => {
                sut.RegisterCustomer(TestCustomer);
            });
        }

        [Test]
        public void AddingCustomerWithInvalidSsn()
        {
            TestCustomer.Ssn = "1555-555-55";
            Assert.Throws<NotvalidSsnException>(() => {
                sut.RegisterCustomer(TestCustomer);
            });
        }

        [Test]
        public void CanRegisterCustomer()
        {
            sut.RegisterCustomer(TestCustomer);
            var result = sut.GetCustomers();
            Assert.IsNotNull(result);
        }

        [Test]
        public void NotBeAbleToRentANonExistentMovie()
        {
            
            TestMovie.Title = "Kalle Anka";
            sut.RegisterCustomer(TestCustomer);
            rentals.AddRental(TestMovie.Title, TestCustomer.Ssn);

            Assert.Throws<MovieDoesNotExistException>(() =>
            {
                sut.RentMovie(TestMovie.Title, TestCustomer.Ssn);
            });
        }

    }
}
