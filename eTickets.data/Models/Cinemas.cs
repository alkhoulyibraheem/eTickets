using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.data.Models
{
    public class Cinemas
    {
        /*
         * Cinemas Table 
         * 
         * Id , Name , Logo , Address 
         */
        public int Id { get; set; }
        [Required(ErrorMessage = "Cinema Name is required")]
        [Display(Name = "Cinema Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        
        public string Name { get; set; }
        [Display(Name = "Cinema Image")]

        public string Logo { get; set; }
        [Required(ErrorMessage = "Cinema Address is required")]
        [Display(Name = "Cinema Address")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Address should be between 2 and 100 characters")]
        
        public string Address { get; set; }

        public bool IsDelete { get; set; }

        public List<Movies> Movies { get; set; }
    }
}
