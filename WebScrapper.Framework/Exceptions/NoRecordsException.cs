using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Framework.Exceptions
{
    public class NoRecordsException : Exception
    {
        public NoRecordsException(string message) : base (message)
        {

        }
    }
}
