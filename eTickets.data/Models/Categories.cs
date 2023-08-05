using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.data.Models
{
    public class Categories
    {
        /*
         * Catgerys Table 
         * 
         * Id , Name
         */
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]

        public string Name { get; set; }

        public bool IsDelete { get; set; }

        public List<Movies> Movie { get; set; }
    }
}
