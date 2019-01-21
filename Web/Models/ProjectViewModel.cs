using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name="Proje Adı")]
        [MaxLength(50,ErrorMessage ="Proje Adı 50 Karekterden Uzun Olamaz!")]
        public string Name { get; set; }
    }
}