using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecruitmentTask.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter a Name")]
        [RegularExpression("^[a-zA-Z0]+$", ErrorMessage = "You must enter a valid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter a Surame")]
        [RegularExpression("[a-zA-Z0-_']+", ErrorMessage = "You must enter a valid Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You must enter a Telephone")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "You must enter a valid Phone number format")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "You must enter a Address")]
        public string Address { get; set; }
    }
}