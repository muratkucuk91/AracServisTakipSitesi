using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AracServisTakipSitesi.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Ad{ get; set; }
        public string Adres{ get; set; }
        public string Sehir { get; set; }
        public string PostaKodu { get; set; }
        public string Telefon { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
