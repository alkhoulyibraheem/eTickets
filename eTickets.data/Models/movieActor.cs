using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.data.Models
{
	public class movieActor
	{
		public int ActorId { get; set; }
		public Actors Actor { get; set; }
		public int MovieId { get; set; }
		public Movies Movie { get; set; }
		public bool IsDelete { get; set; }
	}
}
