using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracServisTakipSitesi.Models
{
    public class Uyeler
    {

        public int Id { get; set; }
        public string Ad{ get; set; }
        public string Email { get; set; }
        public string Adres{ get; set; }
        public string Sehir { get; set; }
        public string PostaKodu { get; set; }
        public string Telefon { get; set; }
        public string UserName { get; internal set; }
    }
}
