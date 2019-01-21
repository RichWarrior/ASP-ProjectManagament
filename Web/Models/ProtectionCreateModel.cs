using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProtectionViewModel
    {
        [Display(Name ="Yetki")]
        public List<RoleViewModel> roles { get; set; }
        [Display(Name ="Menü")]
        public List<MenuViewModel> menu { get; set; }
        public ProtectionViewModel()
        {
            roles = new List<RoleViewModel>();
            menu = new List<MenuViewModel>();
        }
    }
    public class ProtectionCreateModel
    {
        [Required]
        public string role_id { get; set; }
        [Required]
        public string menu_id { get; set; }
    }
}