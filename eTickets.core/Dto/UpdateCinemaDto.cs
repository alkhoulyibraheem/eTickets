using Microsoft.AspNetCore.Http;
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
    public class UpdateCinemaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cinema Name is required")]
        [Display(Name = "Cinema Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
		[SafeText]
		public string Name { get; set; }

        [Display(Name = "Cinema Logo")]
		public IFormFile Logo { get; set; }

        [Required(ErrorMessage = "Cinema Address is required")]
        [Display(Name = "Cinema Address")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Address should be between 2 and 100 characters")]
		[SafeText]
		public string Address { get; set; }
    }
}
