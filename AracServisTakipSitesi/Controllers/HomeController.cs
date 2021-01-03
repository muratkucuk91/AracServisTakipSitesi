using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AracServisTakipSitesi.Data;
using AracServisTakipSitesi.Models;
using AracServisTakipSitesi.Utility;
using AracServisTakipSitesi.ViewModes;
using System.Linq;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

namespace AracServisTakipSitesi.Controllers
{


    
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        //protected ApplicationUser CurrentUser => userManager.FindByNameAsync(User.Identity.Name).Result;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
          ILogger<LoginModel> logger,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _db = db;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index","Home");
            //}

            return View();
        }

        public IActionResult LogIn(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(ApplicationUser Input)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == Input.Email);
                if (user != null )
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);


                    if (result.Succeeded)
                    {
                        //                await userManager.ResetAccessFailedCountAsync(user);

                        //                if (TempData["ReturnUrl"] != null)
                        //                {
                        //                    return Redirect(TempData["ReturnUrl"].ToString());
                        //                }

                        return RedirectToAction("Index", "Member");
                    }


                }
                else
                {

                    ModelState.AddModelError("", "Email adresiniz veya şifreniz yanlış.");

                }
                }
                return View();
            }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(ApplicationUser db)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {

                    UserName = db.UserName,
                    Email = db.Email,
                    PhoneNumber = db.PhoneNumber,
                    Sehir = db.Sehir,
                    PostaKodu = db.PostaKodu,
                    Password = db.Password,
                    Adres = db.Adres,
                    Ad = db.Ad,
                };
                var result = await _userManager.CreateAsync(user, db.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.AdminEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.CustomerEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser));
                    }
                   

                        await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
               

                }
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction("LogIn", "Home");

        }
            //public IActionResult ResetPassword()
            //{
            //    TempData["durum"] = null;
            //    return View();
            //}

            //[HttpPost]
            //public IActionResult ResetPassword(PasswordResetViewModel passwordResetViewModel)
            //{
            //    if (TempData["durum"] == null)
            //    {
            //        ApplicationUser user = userManager.FindByEmailAsync(passwordResetViewModel.Email).Result;

            //        if (user != null)

            //        {
            //            string passwordResetToken = userManager.GeneratePasswordResetTokenAsync(user).Result;

            //            string passwordResetLink = Url.Action("ResetPasswordConfirm", "Home", new
            //            {
            //                userId = user.Id,
            //                token = passwordResetToken
            //            }, HttpContext.Request.Scheme);

            //            //  www.bıdıbıdı.com/Home/ResetPasswordConfirm?userId=sdjfsjf&token=dfjkdjfdjf

            //            Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink, user.Email);

            //            ViewBag.status = "success";
            //            TempData["durum"] = true.ToString();
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Sistemde kayıtlı email adresi bulunamamıştır.");
            //        }
            //        return View(passwordResetViewModel);
            //    }
            //    else
            //    {
            //        return RedirectToAction("ResetPassword");
            //    }
            //}

            //public IActionResult ResetPasswordConfirm(string userId, string token)
            //{
            //    TempData["userId"] = userId;
            //    TempData["token"] = token;

            //    return View();
            //}

            //[HttpPost]
            //public async Task<IActionResult> ResetPasswordConfirm([Bind("PasswordNew")] PasswordResetViewModel passwordResetViewModel)
            //{
            //    string token = TempData["token"].ToString();
            //    string userId = TempData["userId"].ToString();

            //    ApplicationUser user = await userManager.FindByIdAsync(userId);

            //    if (user != null)
            //    {
            //        IdentityResult result = await userManager.ResetPasswordAsync(user, token, passwordResetViewModel.PasswordNew);

            //        if (result.Succeeded)
            //        {
            //            await userManager.UpdateSecurityStampAsync(user);

            //            ViewBag.status = "success";
            //        }
            //        else
            //        {
            //            AddModelError(result);
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "hata meydana gelmiştir. Lütfen daha sonra tekrar deneyiniz.");
            //    }

            //    return View(passwordResetViewModel);
            //}

            //public async Task<IActionResult> ConfirmEmail(string userId, string token)
            //{
            //    var user = await userManager.FindByIdAsync(userId);

            //    IdentityResult result = await userManager.ConfirmEmailAsync(user, token);

            //    if (result.Succeeded)
            //    {
            //        ViewBag.status = "Email adresiniz onaylanmıştır. Login ekranından giriş yapabilirsiniz.";
            //    }
            //    else
            //    {
            //        ViewBag.status = "Bir hata meydana geldi. lütfen daha sonra tekrar deneyiniz.";
            //    }
            //    return View();
            //}

            //public IActionResult FacebookLogin(string ReturnUrl)

            //{
            //    string RedirectUrl = Url.Action("ExternalResponse", "Home", new { ReturnUrl = ReturnUrl });

            //    var properties = signInManager.ConfigureExternalAuthenticationProperties("Facebook", RedirectUrl);

            //    return new ChallengeResult("Facebook", properties);
            //}

            //public IActionResult GoogleLogin(string ReturnUrl)

            //{
            //    string RedirectUrl = Url.Action("ExternalResponse", "Home", new { ReturnUrl = ReturnUrl });

            //    var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", RedirectUrl);

            //    return new ChallengeResult("Google", properties);
            //}

            //public IActionResult MicrosoftLogin(string ReturnUrl)

            //{
            //    string RedirectUrl = Url.Action("ExternalResponse", "Home", new { ReturnUrl = ReturnUrl });

            //    var properties = signInManager.ConfigureExternalAuthenticationProperties("Microsoft", RedirectUrl);

            //    return new ChallengeResult("Microsoft", properties);
            //}

            //public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
            //{
            //    ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            //    if (info == null)
            //    {
            //        return RedirectToAction("LogIn");
            //    }
            //    else
            //    {
            //        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

            //        if (result.Succeeded)
            //        {
            //            return Redirect(ReturnUrl);
            //        }
            //        else
            //        {
            //            ApplicationUser user = new ApplicationUser();

            //            user.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
            //            string ExternalUserId = info.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            //            if (info.Principal.HasClaim(x => x.Type == ClaimTypes.Name))
            //            {
            //                string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;

            //                userName = userName.Replace(' ', '-').ToLower() + ExternalUserId.Substring(0, 5).ToString();

            //                user.UserName = userName;
            //            }
            //            else
            //            {
            //                user.UserName = info.Principal.FindFirst(ClaimTypes.Email).Value;
            //            }

            //            ApplicationUser user2 = await userManager.FindByEmailAsync(user.Email);

            //            if (user2 == null)
            //            {
            //                IdentityResult createResult = await userManager.CreateAsync(user);

            //                if (createResult.Succeeded)
            //                {
            //                    IdentityResult loginResult = await userManager.AddLoginAsync(user, info);

            //                    if (loginResult.Succeeded)
            //                    {
            //                        //     await signInManager.SignInAsync(user, true);

            //                        await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

            //                        return Redirect(ReturnUrl);
            //                    }
            //                    else
            //                    {
            //                        AddModelError(loginResult);
            //                    }
            //                }
            //                else
            //                {
            //                    AddModelError(createResult);
            //                }
            //            }
            //            else
            //            {
            //                IdentityResult loginResult = await userManager.AddLoginAsync(user2, info);

            //                await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);

            //                return Redirect(ReturnUrl);
            //            }
            //        }
            //    }

            //    List<string> errors = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();

            //    return View("Error", errors);
            //}

            //public ActionResult Error()
            //{
            //    return View();
            //}

            //public ActionResult Policy()
            //{
            //    return View();
            //}
        }
    }

