using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class MenuViewModel
    {
        public string Id { get; set; }
        [Display(Name="Ad")]
        [MaxLength(128,ErrorMessage ="Menü Adı 128 Karekterden Fazla Olamaz")]
        public string Name { get; set; }
        [Display(Name="Ikon")]
        [MaxLength(128, ErrorMessage = "Ikon Adı 128 Karekterden Fazla Olamaz")]
        public string Icon { get; set; }
        [Display(Name="Kontrolcu")]
        [MaxLength(128, ErrorMessage = "Kontrolcu Adı 128 Karekterden Fazla Olamaz")]
        public string Controller { get; set; }
        [Display(Name="Aksıyon")]
        [MaxLength(128, ErrorMessage = "Aksıyon Adı 128 Karekterden Fazla Olamaz")]
        public string Action { get; set; }
        [Display(Name="Alt Menü Sayisi")]
        public string Nested { get; set; }
        [Display(Name="Sıra")]
        public int Rank { get; set; }
    }
    public class SubMenuCreateModel
    {
        public MenuViewModel menu { get; set; }
        public List<MenuViewModel> mainMenu { get; set; }

        public SubMenuCreateModel()
        {
            menu = new MenuViewModel();
            mainMenu = new List<MenuViewModel>();
        }
    }
}