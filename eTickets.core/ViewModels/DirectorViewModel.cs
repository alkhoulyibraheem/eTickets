using eTickets.core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.ViewModels
{
    public class DirectorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Director Name is required")]
        [Display(Name = "Director Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 100 characters")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Director Bio is required")]
        [Display(Name = "Director Bio")]
        [StringLength(200, MinimumLength = 7, ErrorMessage = "Name should be between 7 and 200 characters")]

        public string Bio { get; set; }
        [Display(Name = "Director Day Of Birth")]
        [DataType(DataType.Date)]

        public string DOB { get; set; }
        [Display(Name = "Director Image")]

        public string ImageURl { get; set; }
        [Display(Name = "Director Rating")]
        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0.0 and 5.0")]

        public float? Rating { get; set; }
        [Display(Name = "Director Gender")]

        public string Gender { get; set; }

        public UserViewModel User { get; set; }
    }
}
