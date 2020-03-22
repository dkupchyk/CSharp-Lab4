using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupchyk01.Exceptions
{
    class PastBirthdayException: Exception
    {
        public PastBirthdayException() { }

        public PastBirthdayException(string firstName)
            : base($"{ firstName} is too old to be alive.") { }

        public PastBirthdayException(string firstName, string lastName)
            : base($"{ firstName} {lastName} is too old to be alive.") { }
    }
}
