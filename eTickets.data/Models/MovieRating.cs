using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.data.Models
{
	public class MovieRating
	{
		public int Id { get; set; }

		public int Rating { get; set; }

		public int MoviId { get; set; }

		public string UserId { get; set; }
	}
}
