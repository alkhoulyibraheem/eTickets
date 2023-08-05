using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;
using System.Xml.Linq;

namespace eTickets.core.ViewModels
{
    public class movieViewModel
    {
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

        public string CreatedAt { get; set; }


        [Display(Name = "Movie Price")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Price must be minimum 0.0")]
        public float Price { get; set; }

        [Display(Name = "Movie Image")]

        public string ImageURL { get; set; }
        [Display(Name = "Movie Start Date")]
        [DataType(DataType.Date)]

        public string StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [DataType(DataType.Date)]

        public string EndDate { get; set; }

		public int NumberOfStars { get; set; }

		public int NumberRater { get; set; }






		public List<ActorViewModel> Actors { get; set; }

        public DirectorViewModel Director { get; set; }

        public cinemaViewModel Cinema { get; set; }

        public categoryViewModel Category { get; set; }
    }
}
