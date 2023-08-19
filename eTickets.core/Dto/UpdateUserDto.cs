using eTickets.core.Enums;
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
    public class UpdateUserDto
    {
        public string Id { get; set; }

		[Required(ErrorMessage = "User Name is required")]
		[Display(Name = "User Full Name")]
		[SafeText]
		public string FullName { get; set; }

		[Required(ErrorMessage = "User Email is required")]
		[Display(Name = "User Email")]
		[SafeText]
		public string Email { get; set; }

        [Display(Name = "User Phone Number")]
		[RegularExpression(@"^(?:(?:(\+?972|\(\+?972\)|\+?\(972\))(?:\s|\.|-)?([1-9]\d?))|(0[23489]{1})|(0[57]{1}[0-9]))(?:\s|\.|-)?([^0\D]{1}\d{2}(?:\s|\.|-)?\d{4})$", ErrorMessage = "Invalid phone number format")]
		public string PhoneNumber { get; set; }

		
		[Display(Name = "User Image")]
        public IFormFile ImageURL { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "User Statu")]
        public status Status { get; set; }
    }
}
