using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AracServisTakipSitesi.Models
{

    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        public string Email { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        public string Sifre { get; set; }
        public string Yetki { get; set; }


    }
}