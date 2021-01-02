
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
    public class Admin1Controller : Controller
    {
        public UserManager<ApplicationUser> userManager { get; }
        public SignInManager<ApplicationUser> signInManager { get; }

        public Admin1Controller(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;


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



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userlogin)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userlogin.Email);
                if (user != null)
                {
                    //            if (await userManager.IsLockedOutAsync(user))
                    //            {
                    //                ModelState.AddModelError("", "Hesabınız bir süreliğine kilitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");

                    //                return View(userlogin);
                    //            }

                    //            if (userManager.IsEmailConfirmedAsync(user).Result == false)
                    //            {
                    //                ModelState.AddModelError("", "Email adresiniz onaylanmamıştır. Lütfen  epostanızı kontrol ediniz.");
                    //                return View(userlogin);
                    //            }

                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, userlogin.Password, false, false);

                    if (result.Succeeded)
                    {
                        //                await userManager.ResetAccessFailedCountAsync(user);

                        //                if (TempData["ReturnUrl"] != null)
                        //                {
                        //                    return Redirect(TempData["ReturnUrl"].ToString());
                        //                }

                        return RedirectToAction("Index", "Admin1");
                    }

                }
                else
                {
                    //                await userManager.AccessFailedAsync(user);

                    //                int fail = await userManager.GetAccessFailedCountAsync(user);
                    //                ModelState.AddModelError("", $" {fail} kez başarısız giriş.");
                    //                if (fail == 3)
                    //                {
                    //                    await userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(20)));

                    //                    ModelState.AddModelError("", "Hesabınız 3 başarısız girişten dolayı 20 dakika süreyle kitlenmiştir. Lütfen daha sonra tekrar deneyiniz.");
                    //                }
                    //                else
                    //                {
                    ModelState.AddModelError("", "Email adresiniz veya şifreniz yanlış.");
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            ModelState.AddModelError("", "Bu email adresine kayıtlı kullanıcı bulunamamıştır.");
                }
            }

            return View(userlogin);
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

                    return RedirectToAction("Login");
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


        public ActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login","Admin1");
        }



    }
}