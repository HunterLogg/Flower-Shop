using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lab03.Areas.Main
{
    public class LoginModel
    {
         [Required]
         public string Email { get; set; }
         public string emailRegister { get; set; }
         public string Password { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
    }
}