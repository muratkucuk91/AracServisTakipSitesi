using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracServisTakipSitesi.Data;
using AracServisTakipSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AracServisTakipSitesi.Pages.Uyeler
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public List<ApplicationUser>ApplicationUserList{ get; set; }



        public async Task <IActionResult> OnGet()
        {
            ApplicationUserList = await _db.ApplicationUser.ToListAsync();
            return Page();




    }
    }
}
