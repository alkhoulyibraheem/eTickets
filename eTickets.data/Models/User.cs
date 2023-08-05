using eTickets.core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.data.Models
{
    public class User : IdentityUser
    {
        public bool IsDelete { get; set; }

        public DateTime CreatedAt { get; set; }

        public string FullName { get; set; }

        public String ImageURL { get; set; }

        public UserType UserType{ get; set; }

        public status Status { get; set; }

		public Actors Actor { get; set; }

        public Directors Director { get; set; }

		public Customer Customer { get; set; }


		public List<Orders> Orders { get; set; }


    }
}
