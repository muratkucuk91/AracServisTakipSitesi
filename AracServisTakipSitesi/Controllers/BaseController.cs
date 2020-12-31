using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AracServisTakipSitesi.Models;

namespace AracServisTakipSitesi.Controllers
{
    public class BaseController : Controller
    {
        protected UserManager<ApplicationUser> userManager { get; }
        protected SignInManager<ApplicationUser> signInManager { get; }

       

        protected ApplicationUser CurrentUser => userManager.FindByNameAsync(User.Identity.Name).Result;

        public BaseController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
          
        }

        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
    }
}