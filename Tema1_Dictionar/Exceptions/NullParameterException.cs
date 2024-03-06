using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Exceptions
{
    internal class NullParameterException : Exception
    { 
        public NullParameterException(string message) : base(message) { }

    }
}
