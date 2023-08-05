using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.ViewModels
{
    public class categoryViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
