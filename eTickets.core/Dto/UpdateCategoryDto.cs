using RestaurantStore.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.Dto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
		[SafeText]
		public string Name { get; set; }
    }
}
