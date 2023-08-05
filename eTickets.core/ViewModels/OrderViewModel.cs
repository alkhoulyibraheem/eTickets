using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public List<movieViewModel> Movies { get; set; }

        public UserViewModel User { get; set; }



    }
}
