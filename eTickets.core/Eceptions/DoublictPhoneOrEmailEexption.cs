using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.Eceptions
{
    public class DoublictPhoneOrEmailEexption :Exception
    {
        public DoublictPhoneOrEmailEexption() : base("Doublict Phone Or Email") { }
    }
}
