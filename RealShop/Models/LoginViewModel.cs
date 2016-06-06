using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealShop.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Введите логин")]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required(ErrorMessage="Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string password { get; set; }

    }
}