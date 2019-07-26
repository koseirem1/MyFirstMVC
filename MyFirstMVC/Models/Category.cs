using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class Category
    {

       public int Id { get; set; }
        [Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Açıklamalar")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Project> Projects { get; set; }


    }
}