using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class MovieTitleEmptyException : Exception { }
    
    public class TooManyIdenticalMoviesException : Exception { }

    public class DuplicateCustomerException : Exception { }

    public class NotvalidSsnException : Exception { }

    public class CustomerDoesNotExistException : Exception { }

    public class MovieDoesNotExistException : Exception { }

    public class CustomerDoesNotHaveAnyRentalsException : Exception { }

    public class TooManyRentalsException : Exception { }
    
    public class RentTwoCopiesOfSameMovieException : Exception { }

    public class LateRentalsReturnException : Exception
    {
        public LateRentalsReturnException(string message) : base(message)
        {

        }
    }
    public static class ExeptionMessages
    {
        public const string LateRentalsReturnExceptionMessage = "Customer still has delayed movies that need to be returned before renting a new one.";
    }

}
