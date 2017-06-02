using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class MovieTitleEmptyException : Exception
    {

    }

    public class TooManyIdenticalMoviesException : Exception
    {
        
    }
    public class DuplicateCustomerException : Exception
    {

    }
}
