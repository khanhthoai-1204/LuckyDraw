using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lucky_Draw_Promotion.Areas.Admin.Models;
using Lucky_Draw_Promotion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucky_Draw_Promotion.Areas.Admin.Controllers;
using Lucky_Draw_Promotion.Models.Account;
using Lucky_Draw_Promotion.Data;


namespace Lucky_Draw_Promotion.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _SinInManager;
        private readonly UserManager<User> _userManager;
        private IPasswordHasher<User> passwordHasher;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DataContext context, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, IPasswordHasher<User> passwordHash)
        {
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _SinInManager = signInManager;
            passwordHasher = passwordHash;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (_userManager.GetUserId(User) != null)
            {
                return RedirectToLocal(returnUrl);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel,  string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await _SinInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, false);
           

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng vui lòng nhập lại");
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (_userManager.GetUserId(User) != null)
            {
                await _SinInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }
            await _SinInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });

        }

        //[HttpGet]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.GetUserAsync(User);
        //        if (user == null)
        //        {

        //            return RedirectToAction("Login");
        //        }

        //        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            await _SinInManager.RefreshSignInAsync(user);
        //            ViewBag.IsSuccess = true;
        //            ModelState.Clear();
        //            return RedirectToAction("ChangePassword", "Account");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }

        //    }
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "Xác nhận mật khẩu không trùng với mật khẩu mới");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Tài khoản mật khẩu và xác nhận mật khẩu không được trống");
        //        return View();
        //    }
        //    return View(model);

        //}

    }
    }
