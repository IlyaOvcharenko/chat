using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}