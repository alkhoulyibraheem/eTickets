﻿using eTickets.core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.ViewModels
{
	public class CustomerViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Actor Name is required")]
		[Display(Name = "Actor Name")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]

		public string Name { get; set; }

		[Display(Name = "Actor Day Of Birth")]
		[DataType(DataType.Date)]

		public string DOB { get; set; }
		[Display(Name = "Actor Image")]

		public string ImageURl { get; set; }

		[Display(Name = "Actor Gender")]

		public string Gender { get; set; }

		public UserViewModel User { get; set; }
	}
}
