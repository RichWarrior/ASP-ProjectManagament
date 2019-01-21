using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name="Yetki Adı")]
        public string Name { get; set; }
        [Display(Name="Erişim Nokta Sayısı")]
        public int MenuCount { get; set; }
    }
}