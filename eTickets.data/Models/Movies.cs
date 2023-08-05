using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.data.Models
{
    public class Movies
    {

        /*
         * Movies Table 
         * 
         * Id , Name , Description , CreatedAt , Price , ImageURL , StartDate , EndDate 
         */
        public int Id { get; set; }
        [Required(ErrorMessage = "Movie Name is required")]
        [Display(Name = "Movie Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Actors Name is required")]
        [Display(Name = "Movie Description")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Name should be between 10 and 200 characters")]

        public string Description { get; set; }
        [Display(Name = "Whene Movie Created")]
        [DataType(DataType.Date)]

        public DateTime CreatedAt { get; set; }

        public bool IsDelete { get; set; }


        [Display(Name = "Movie Price")]
        [Range(0.0,float.MaxValue, ErrorMessage = "Price must be minimum 0.0")]
        public float Price { get; set; }

        [Display(Name = "Movie Image")]

        public string ImageURL { get; set; }
        [Display(Name = "Movie Start Date")]
        [DataType(DataType.Date)]

        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }

        public int NumberOfStars { get; set; }
		public int NumberRater { get; set; }







		public List<Actors> Actors { get; set; }

		public List<movieActor> movieActors { get; set; }

		public int DirectorId { get; set; }

        public Directors Director { get; set; }

        public int CinemaId { get; set; }

        public Cinemas Cinema { get; set; }

        public int CategoryId { get; set; }

        public Categories Category { get; set; }

        public List<Orders> Orders { get; set; }

		public List<MovieOrder> movieOrders { get; set; }
	}
}
