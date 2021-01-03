using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AracServisTakipSitesi.Models.ViewModel
{
    public class CarServiceViewModel
    {
        public Cars Cars { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
        public ServiceDetails ServiceDetails { get; set; }

        public List<ServiceType> ServiceTypesList{ get; set; }
        public List<ServiceShoppingCart> ServiceShoppingCart { get; set; }
    }
}
