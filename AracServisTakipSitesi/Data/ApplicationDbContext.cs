using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AracServisTakipSitesi.Models;

namespace AracServisTakipSitesi.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<AracServisTakipSitesi.Models.Uyeler> Uyeler { get; set; }
    
        public DbSet<Cars> Cars { get; set; }
    }
}

