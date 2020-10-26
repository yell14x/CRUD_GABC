using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Your First Name")]
        [StringLength(50, ErrorMessage = "First Name should be less than or equal to fifty characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Your Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name should be less than or equal to fifty characters.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Enter Your Last Name")]
        [StringLength(50, ErrorMessage = "Last name should be less than or equal to fifty characters.")]
        public string LastName { get; set; }

        public DateTime DateHired { get; set; }
        [DataType(DataType.ImageUrl)]
        [Required(ErrorMessage = "Enter Your Image Link")]
        public string EmpImage { get; set; }
        public string Br_Assigned { get;set; }

    }
}