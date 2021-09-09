using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lab03.Areas.Admin
{
    public class CreateModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}