using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.Eceptions
{
    public class EntityNotFoundExecption : Exception
    {
        public EntityNotFoundExecption() : base("Entity Not Found Execption") { }
    }
}
