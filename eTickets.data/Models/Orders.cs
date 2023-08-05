using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.data.Models
{
    public class Orders
    {

        /*
         * Actors Table 
         * 
         * Id , Price , Amount 
         */
        public int Id { get; set; }

        public List<Movies> Movies { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

		public List<MovieOrder> movieOrders { get; set; }

	}
}
