using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-Posta Adresi")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public object RememberMe { get; set; }
    } 
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Compare("password",ErrorMessage ="Şifreler Uyuşmuyor")]
        [DataType(DataType.Password)]
        public string re_password { get; set; }
    }
    public class ProfileViewModel
    {
        [Display(Name ="Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Tekrar-Şifre")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Şifreler Uyuşmuyor")]
        public string Re_Password { get; set; }
    }
    public class ForgotViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
