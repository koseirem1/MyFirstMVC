using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Models
{
    public class ContactViewModel
    {
        [Display (Name="Ad")]
        [MaxLength(50)]
        [Required(ErrorMessage ="Ad alanı gereklidir.")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Telefon")]
        [MaxLength(20)]
        [Required]
        [Phone]

        public string Telephone { get; set; }
        [Display(Name = "E-mail")]
        [MaxLength(100)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Mesajınız")]
        [MaxLength(40000)]
        [Required]

        public string Message { get; set; }
        [Display(Name ="Kvkk")]
        [Required]
        public bool PrivacyPolicyAccepted { get; set; }
    }
}