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

        [Test]
        public void GetBackMoviesAfterthreeDays()
        {
            sut.AddRental("Transporter2", "1988-02-15");

            var rentals = sut.GetRentalsFor("1988-02-15");
            var expected = DateTime.Now.AddDays(3).Date;
            Assert.AreEqual(expected, rentals[0].DueDate.Date);
        }



    }

}
