using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresantationAccessLeyer.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "We can enter only numeric value")]
        public int Simno { get; set; }
        public string EmpName { get; set; }
        [RegularExpression(@"[\d]{1,8}([.,][\d]{1,2})?", ErrorMessage = "Plz enter currect numric format..")]
        public decimal EmpSalary { get; set; }
    }
}