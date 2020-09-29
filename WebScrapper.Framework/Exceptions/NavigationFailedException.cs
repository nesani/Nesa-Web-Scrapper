using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Framework.Exceptions
{
    public class NavigationFailedException : Exception
    {
        public NavigationFailedException(string message) : base (message)
        {

        }
    }
}
