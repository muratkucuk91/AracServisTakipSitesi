using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AracServisTakipSitesi.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VIN { get; set; }

        [Required]
        public string Marka { get; set; }
        [Required]
        public string Model { get; set; }
        public string Style { get; set; }
        [Required]
        public int Yıl { get; set; }

        [Required]
        public double Kilometre { get; set; }
        public string Renk { get; set; }



        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }




    }
}
