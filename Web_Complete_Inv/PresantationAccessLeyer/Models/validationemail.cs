using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresantationAccessLeyer.Models
{  
        public class Registration
        {
            [Required(ErrorMessage = "Please enter your Name")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please enter your valid Email_id")]
            [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter your valid email which contains the @ Sign")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Please enter your valid phone number")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Please enter your address")]
            public string Address { get; set; }
        }

}