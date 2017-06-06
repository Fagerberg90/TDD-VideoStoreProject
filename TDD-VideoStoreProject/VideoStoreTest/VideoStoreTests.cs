using NUnit.Framework;
using VideoStoreBL;
using NSubstitute;

namespace VideoStoreTest
{
    [TestFixture]
    public class VideoStoreTests
    {
        private IVideoStore _sut;
        private IRentals _rentals;
        private Movie _testMovie;
        private Customer _testCustomer;

        [SetUp]
        public void Setup()
        {
            _rentals = Substitute.For<IRentals>();
            _sut = new VideoStore(_rentals);
            _testCustomer = new Customer(
                 "Goran",
                 "Pettersson",
                 "1850-09-22"
                );
            _testMovie = new Movie(
                "Star Wars",
                 MovieGenre.SciFi
                );
        }

        [Test]
        public void MovieTitleCannotBeEmpty() //yesy
        {
            _testMovie.Title = "";

            Assert.Throws<MovieTitleEmptyException>(() =>
            {
                _sut.AddMovie(_testMovie);
            });
        }

        [Test]
        public void CannotAddMoreThanThreeIdenticalMovies()
        {
            _sut.AddMovie(_testMovie);
            _sut.AddMovie(_testMovie);
            _sut.AddMovie(_testMovie);
            Assert.Throws<TooManyIdenticalMoviesException>(() =>
            {
                _sut.AddMovie(_testMovie);
            });

        }

        [Test]
        public void NoDuplicatedCustomers()
        {
            _sut.RegisterCustomer(_testCustomer);
            Assert.Throws<DuplicateCustomerException>(() =>
            {
                _sut.RegisterCustomer(_testCustomer);
            });
        }

        [Test]
        public void AddingCustomerWithInvalidSsn()
        {
            _testCustomer.Ssn = "1555-555-55";
            Assert.Throws<NotvalidSsnException>(() =>
            {
                _sut.RegisterCustomer(_testCustomer);
            });
        }

        [Test]
        public void CanRegisterCustomer()
        {
            _sut.RegisterCustomer(_testCustomer);
            var result = _sut.GetCustomers();
            Assert.IsNotNull(result);
        }

        [Test]
        public void NotBeAbleToRentANonExistentMovie()
        {

            _testMovie.Title = "Kalle Anka";
            _sut.RegisterCustomer(_testCustomer);
            _rentals.AddRental(_testMovie.Title, _testCustomer.Ssn);

            Assert.Throws<MovieDoesNotExistException>(() =>
            {
                _sut.RentMovie(_testMovie.Title, _testCustomer.Ssn);
            });
        }

        [Test]
        public void NonRegistredCustomerCanNotRentMovie()
        {

            _testMovie.Title = "Kalle Anka";

            _sut.AddMovie(_testMovie);
            _rentals.AddRental(_testMovie.Title, _testCustomer.Ssn);

            Assert.Throws<CustomerDoesNotExistException>(() =>
            {
                _sut.RentMovie(_testMovie.Title, _testCustomer.Ssn);
            });
        }

        [Test]
        public void AbleToReturnMovie()
        {
            _sut.AddMovie(_testMovie);
            _sut.RegisterCustomer(_testCustomer);
            _sut.RentMovie(_testMovie.Title, _testCustomer.Ssn);
            _sut.ReturnMovie(_testMovie.Title, _testCustomer.Ssn);

            _rentals.Received(1).RemoveRental(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void RemoveRentalDoesNotReceiveAnyCallWhenYouPassBadSsn()
        {
            _sut.AddMovie(_testMovie);
            _sut.RegisterCustomer(_testCustomer);
            _sut.RentMovie(_testMovie.Title, _testCustomer.Ssn);
            
            Assert.Throws<NotvalidSsnException>(() =>
            {
                _sut.ReturnMovie(_testMovie.Title, "555-5-5");
            });
            _rentals.DidNotReceive().RemoveRental(Arg.Any<string>(), Arg.Any<string>());
        }

    }
}
