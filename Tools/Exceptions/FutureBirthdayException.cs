using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupchyk01.Exceptions
{
    class FutureBirthdayException : Exception
    {
        public FutureBirthdayException() { }

        public FutureBirthdayException(string firstName)
            : base($"{ firstName} is not born yet.") { }

        public FutureBirthdayException(string firstName, string lastName)
            : base($"{ firstName} {lastName} is not born yet.") { }
    }
}
