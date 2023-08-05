using eTickets.core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eTickets.core.ViewModels
{
    public class UserViewModel
    {
        public String Id { get; set; }
        [Display(Name = "User Full Name")]

        public string FullName { get; set; }
        [Display(Name = "User Name")]

        public String UserName { get; set; }
        [Display(Name = "User Phone Number")]

        public String PhoneNumber { get; set; }
        [Display(Name = "Whene User created")]

        public DateTime CreatedAt { get; set; }
        [Display(Name = "User Image")]

        public string ImageURL { get; set; }
        [Display(Name = "User Type")]

        public string UserType { get; set; }
        [Display(Name = "User Statu")]

        public string Status { get; set; }
    }
}
