using AracServisTakipSitesi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mapster;

using AracServisTakipSitesi.ViewModes;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using AracServisTakipSitesi.Data;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AracServisTakipSitesi.Utility;

namespace AracServisTakipSitesi.Controllers
{


   
    public class MemberController : Controller


    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public List<ApplicationUser> ApplicationUserList { get; private set; }

        //protected ApplicationUser CurrentUser => userManager.FindByNameAsync(User.Identity.Name).Result;

        public MemberController(
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

            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index");
            //}

            return View(userViewModel);

        }



        public IActionResult MemberList()
        {
            ApplicationUserList = _db.ApplicationUser.ToList();
            return View(ApplicationUserList);
        }





        public IActionResult CustomerCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CustomerCreate(ApplicationUser db)
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


                    if (db.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.AdminEndUser);

                        
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                    }
                    }
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return RedirectToAction("MemberList", "Member");

        }


        public IActionResult UserEdit()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;


            UserViewModel userViewModel = user.Adapt<UserViewModel>();

            

            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> UserEditAsync(UserViewModel userViewModel)


        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);

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
                var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

                bool exist = _userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (exist)
                {
                    IdentityResult result = _userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                    if (result.Succeeded)
                    {

                        _userManager.UpdateSecurityStampAsync(user);
                        //await userManager.UpdateSecurityStampAsync(user);
                        _signInManager.SignOutAsync();

                        _signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

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
            _signInManager.SignOutAsync();
        }
    }
}

