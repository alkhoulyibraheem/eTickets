using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.core.Dto
{
	public class RatingDto
	{
		public int Id { get; set; }
		[Display(Name = "Rating Movie form 5 star")]
		[Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
		public int NumberOfStars { get; set; }
	}
}
