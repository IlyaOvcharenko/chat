using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.Auth
{
    public class RegistrationModel
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        [Remote("CheckLogin", "Auth", ErrorMessage = "Пользователь с таким именем уже существует", HttpMethod = "Post")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }
    }
}