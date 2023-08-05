using eTickets.core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.Dto
{
	public class CreateCustomerDto
	{

		[Required(ErrorMessage = "Customer Name is required")]
		[Display(Name = "Customer Name")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
		public string Name { get; set; }

		[Display(Name = "Customer Day Of Birth")]
		[DataType(DataType.Date)]
		public DateTime DOB { get; set; }

		[Display(Name = "Customer Image")]
		[Required(ErrorMessage = "Customer Image is required")]
		public IFormFile ImageURl { get; set; }

		[Display(Name = "Customer Gender")]
		public Gender Gender { get; set; }

		public DateTime CreateAt { get; set; }

		public CreateUserDto User { get; set; }
	}
}
