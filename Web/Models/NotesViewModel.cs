using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models
{
    public class NotesViewModel
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name ="Oluşturma Tarihi")]
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateView { get; set; }

    }
    public class NoteCreateModel
    {
        [Display(Name = "Başlık")]
        [Required]
        [MaxLength(50, ErrorMessage = "Başlık 50 Karekterden Uzun Olamaz")]
        public string Title { get; set; }
        [Display(Name = "Açıklama")]
        [Required]
        [AllowHtml]
        public string Description { get; set; }
    }
}