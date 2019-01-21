using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class AdminDashModel
    {
        [Display(Name ="Gönderilmeyi Bekleyen Şifre")]
        public Nullable<int> ACTION_COUNT { get; set; }
        [Display(Name ="Kullanıcı Sayısı")]
        public Nullable<int> USER_COUNT { get; set; }
        [Display(Name ="Not Sayısı")]
        public Nullable<int> NOTE_COUNT { get; set; }
        [Display(Name ="Proje Sayısı")]
        public Nullable<int> PROJECT_COUNT { get; set; }
        [Display(Name ="Sunucu Parametre Sayısı")]
        public Nullable<int> PARAMETER_COUNT { get; set; }
        [Display(Name ="İş Tanımı Sayısı")]
        public Nullable<int> WORKDEF_COUNT { get; set; }

    }
}