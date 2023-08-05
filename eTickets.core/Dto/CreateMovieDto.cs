﻿using eTickets.core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.core.Dto
{
    public class CreateMovieDto
    {
        [Required(ErrorMessage = "Movie Name is required")]
        [Display(Name = "Movie Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name should be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Movie Description is required")]
        [Display(Name = "Movie Description")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Name should be between 10 and 200 characters")]
        public string Description { get; set; }

        [Display(Name = "Whene Movie Created")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Movie Price")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Price must be minimum 0.0")]
        public float Price { get; set; }

        [Display(Name = "Movie Image")]
        [Required(ErrorMessage = "Movie Image is required")]
        public IFormFile ImageURL { get; set; }

        [Display(Name = "Movie Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }




        public List<int> Actors { get; set; }

        public int DirectorId { get; set; }

        public int CinemaId { get; set; }

        public int CategoryId { get; set; }
    }
}
