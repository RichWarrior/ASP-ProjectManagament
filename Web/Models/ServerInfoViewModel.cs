using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ServerInfoViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name ="Anahtar")]
        [MaxLength(50,ErrorMessage ="Anahtar Adı 50 Karekterden Fazla Olamaz")]
        public string key_str { get; set; }
        [Required]
        [Display(Name ="Deger")]
        [MaxLength(50,ErrorMessage="Anahtarın Değeri 50 Karekterden Fazla Olamaz")]
        public string value_str { get; set; }
    }  
}