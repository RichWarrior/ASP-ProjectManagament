using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class WorkDefinitionModel
    {
        public string Id { get; set; }
        [Display(Name ="Proje Adı")]
        public string ProjectName { get; set; }
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Display(Name ="Bitiş Tarihi")]
        public DateTime ExpireDate { get; set; }
        public string ExpireDateView { get; set; }
        public bool completed { get; set; }
        [Display(Name ="Önem Durumu")]
        public int rank { get; set; }
    }
    public class WorkDefinitionCreateModel
    {
        public string Id { get; set; }
        [Display(Name ="Proje Adı")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Açıklama")]
        [AllowHtml]
        public string Description { get; set; }
        [Required]
        [Display(Name ="Bitiş Tarihi")]
        public string ExpireDate { get; set; }
        [Required]
        public string ProjectId { get; set; }

        [Display(Name ="Tamamlandı Olarak İşaretle")]
        public object completed { get; set; }

        [Display(Name ="Önem Sırası")]
        public int rank { get; set; }

        public List<ProjectViewModel> projects { get; set; }

        public WorkDefinitionCreateModel()
        {
            projects = new List<ProjectViewModel>();
        }
    }
    public class WorkDefinitionViewModel
    {
        public List<WorkDefinitionModel> works { get; set; }
        public List<ProjectViewModel> projects { get; set; }

        public WorkDefinitionViewModel()
        {
            works = new List<WorkDefinitionModel>();
            projects = new List<ProjectViewModel>();
        }
    }    
    public class Rank
    {
        public string value { get; set; }
        public string Key { get; set; }
    }
}