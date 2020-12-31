using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AracServisTakipSitesi.Models;

namespace AracServisTakipSitesi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


   


        public DbSet<AracServisTakipSitesi.Models.Uyeler> Uyeler { get; set; }
    }
}

