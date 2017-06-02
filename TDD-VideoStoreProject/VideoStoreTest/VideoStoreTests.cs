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
                 "2233-9922"
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

    }
}
