using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class Rental
    {
       public DateTime DueDate { get; set; }
        public string MovieTitle { get; set; }
        public string CustomerSsn { get; set; }
    
        public Rental(DateTime dueDate, string movieTitle,string customerSsn)
        {
            DueDate = dueDate;
            MovieTitle = movieTitle;
            CustomerSsn = customerSsn;
        }
    }
}
