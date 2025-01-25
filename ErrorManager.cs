using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suduko
{
    public class InputException : Exception
    {
        public InputException(string message) : base(message) { }
    }

    public class UnvalidBoard : Exception
    {
        public UnvalidBoard(string message) : base(message) { }
    }

}
