﻿using eTickets.core.Enums;
using eTickets.core.ViewModels;
using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.Dto
{
    public class CreateActorDto
    {
        
       
        [Required(ErrorMessage = "Actor Bio is required")]
        [Display(Name = "Actor Bio")]
        [SafeText]
        public string Bio { get; set; }

        [Display(Name = "Actor Day Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Actor Image is required")]
        [Display(Name = "Actor Image")]
        public IFormFile ImageURl { get; set; }

        [Display(Name = "Actor Rating")]
        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0.0 and 5.0")]
        public float? Rating { get; set; }

        [Display(Name = "Actor Gender")]
        public Gender Gender { get; set; }

        public CreateUserDto User { get; set; }
    }
}
