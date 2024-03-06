using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Exceptions
{
    internal class InvalidAuthDataException : Exception
    {
        public InvalidAuthDataException(string message) : base(message) { }
    }
}
