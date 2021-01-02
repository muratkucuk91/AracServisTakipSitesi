using AracServisTakipSitesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mapster;

using AracServisTakipSitesi.ViewModes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AracServisTakipSitesi.Controllers
{


    [Authorize]
    public class MemberController : Controller


    {

        public UserManager<ApplicationUser> userManager { get; }
        public SignInManager<ApplicationUser> signInManager { get; }
      

        public MemberController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
           


        }
        public IActionResult Index()
        {

            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index");
            //}

            return View(userViewModel);

        }






        public IActionResult UserEdit()
        {
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;


            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            

            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UserEditAsync(UserViewModel userViewModel)


        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.SignInAsync(user, true);

                    ViewBag.success = "true";
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }

                public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

                bool exist = userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (exist)
                {
                    IdentityResult result = userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                    if (result.Succeeded)
                    {

                        userManager.UpdateSecurityStampAsync(user);
                        //await userManager.UpdateSecurityStampAsync(user);
                        signInManager.SignOutAsync();

                        signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

                        ViewBag.success = "true";
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eski şifreniz yanlış");
                }
            }

            return View(passwordChangeViewModel);
        }


        public void LogOut()
        {
            signInManager.SignOutAsync();
        }
    }
}

