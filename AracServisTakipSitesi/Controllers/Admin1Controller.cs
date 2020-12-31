
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AracServisTakipSitesi.Models;
using AracServisTakipSitesi.ViewModes;
using AracServisTakipSitesi.Controllers;

namespace AracServisTakipSitesi.Controllers
{
    //[Authorize(Roles = "admin")]
    public class Admin1Controller : BaseController
    {
        public Admin1Controller(UserManager<ApplicationUser> userManager) : base(userManager, null)
        {
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public IActionResult Login(string ReturnUrl)
        {
            //TempData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        public IActionResult CustomerCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CustomerCreate(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                //if (userManager.Users.Any(u => u.PhoneNumber == userViewModel.PhoneNumber))
                //{
                //    ModelState.AddModelError("", "Bu telefon numarası kayıtlıdır.");
                //    return View(userViewModel);
                //}

                ApplicationUser user = new ApplicationUser();
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {

                    return RedirectToAction("LogIn");
                }

                else

                {

                foreach(IdentityError item in result.Errors)
                {

                        ModelState.AddModelError("", item.Description);

                }
                }
                    //string confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    //string link = Url.Action("ConfirmEmail", "Home", new
                    //{
                    //    userId = user.Id,
                    //    token = confirmationToken
                    //}, protocol: HttpContext.Request.Scheme

                    //);

                    //Helper.EmailConfirmation.SendEmail(link, user.Email);

                    //return RedirectToAction("LogIn");
               
            }

            return View(userViewModel);
        }






    }
}