using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.data.Models
{
	public class MovieOrder
	{
		public int Amount { get; set; }

		public float Price { get; set; }

		public int MovieId { get; set; }

		public Movies Movie { get; set; }

		public int orderId { get; set; }

		public Orders order { get; set; }

		public bool IsDelete { get; set; }
	}
}
