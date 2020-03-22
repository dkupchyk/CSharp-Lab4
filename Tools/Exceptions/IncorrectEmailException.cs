using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kupchyk01.Exceptions
{
    class IncorrectEmailException : Exception
    {
        public IncorrectEmailException() { }

        public IncorrectEmailException(string firstName)
            : base($"{ firstName} has not valid email address.") { }

        public IncorrectEmailException(string firstName, string lastName)
            : base($"{ firstName} {lastName} has not valid email address.") { }
    }
}
