using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.core.ViewModels
{
	public class MovieOrderViewModel
	{
		public int Amount { get; set; }

		public float Price { get; set; }

		public movieViewModel Movie { get; set; }

		public OrderViewModel order { get; set; }




	}
}
