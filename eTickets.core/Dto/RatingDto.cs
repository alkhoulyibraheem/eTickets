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

		public int Rating { get; set; }

		public int MoviId { get; set; }

		public string UserId { get; set; }
	}
}
