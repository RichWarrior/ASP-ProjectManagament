using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name ="E-Posta Adresi")]
        public string email { get; set; }
        [Required]
        [Display(Name ="Yetkisi")]
        public string role { get; set; }
    }
    public class UsersCreateModel
    {
        [Display(Name ="E-Posta Adresi")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public List<RoleViewModel> roles { get; set; }
        [Display(Name ="Yetki")]
        [Required]
        public string role_id { get; set; }

        public UsersCreateModel()
        {
            roles = new List<RoleViewModel>();
        }
    }    
}