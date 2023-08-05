using eTickets.core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.Dto
{
    public class CreateUserDto
    {
		[Required(ErrorMessage = "User Name is required")]
		[Display(Name = "User Full Name")]
        public string FullName { get; set; }

		[Required(ErrorMessage = "User Email is required")]
		[Display(Name = "User Email")]
        public string  Email { get; set; }

        [Display(Name = "User Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "User Image")]
		public IFormFile ImageURL { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "User Statu")]
        public status Status { get; set; }
    }
}
