using AracServisTakipSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel.DataAnnotations;



namespace AracServisTakipSitesi.ViewModes
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> ApplicationUserList { get; set; }
   
    }
}
