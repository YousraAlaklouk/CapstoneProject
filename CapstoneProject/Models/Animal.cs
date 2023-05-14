using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace CapstoneProject.Models
{
    public class Animal
    {

        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Select the gender")]
        public string Gender { get; set; }

        [Display(Name = "Age")]

        [Required(ErrorMessage = "Age Required!!")]
        public string Age { get; set; }

        [Display(Name = "Type")]

        [Required(ErrorMessage = "Type Required!!")]
        public string Type { get; set; }



    }

}