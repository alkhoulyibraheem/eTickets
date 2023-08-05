using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.Eceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException() : base("Operation Failed Exception") { }
    }
}
