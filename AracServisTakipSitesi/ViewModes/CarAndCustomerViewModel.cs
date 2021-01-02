using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using AracServisTakipSitesi.Models;



namespace AracServisTakipSitesi.ViewModes
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Cars> Cars { get; set; }
    }

}