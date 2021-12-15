using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace Entities
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Company Name: ")]
        public string CompanyName { get; set; }

      
        [Required]
        [EmailAddress]
        //[Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }
     
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password:")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
