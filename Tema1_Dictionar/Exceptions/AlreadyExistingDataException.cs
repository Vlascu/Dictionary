using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Exceptions
{
    internal class AlreadyExistingDataException : Exception
    {
        public AlreadyExistingDataException(string message) : base(message) { }
    }
}
