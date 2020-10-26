using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Models
{
    public class Branch
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Your Branch Code")] 
        public string BR_Code { get; set; }

        [Required(ErrorMessage = "Enter Your Branch Name")]
        public string br_name { get; set; }
        [Required(ErrorMessage = "Enter Your Branch Address")]
        public string br_address { get; set; }
        [Required(ErrorMessage = "Enter Your Branch Brgy")]

        public string br_brgy{ get; set; }
        [Required(ErrorMessage = "Enter Your Branch City")]

        public string br_city { get; set; }
        public string br_mngr { get; set; }

        
        public string permitno { get; set; }
        
        public DateTime DateOpened { get; set; }
        [DataType(DataType.ImageUrl)]
        public bool isActive { get; set; }

    }
}