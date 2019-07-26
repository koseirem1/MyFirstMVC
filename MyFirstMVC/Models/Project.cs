using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class Project
    {
        
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [DataType(DataType.Html)]
        public string Body { get; set; }
        [Display(Name = "Resim")]
        public string Photo { get; set; }
        [Display(Name = "Kategoriler")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}