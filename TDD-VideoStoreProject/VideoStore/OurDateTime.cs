using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBL
{
    public class OurDateTime : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
