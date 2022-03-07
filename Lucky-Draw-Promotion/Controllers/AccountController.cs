//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Lucky_Draw_Promotion.Areas.Admin.Models;
//using Lucky_Draw_Promotion.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Lucky_Draw_Promotion.Areas.Admin.Controllers;
//using Lucky_Draw_Promotion.Models.Account;
//using Lucky_Draw_Promotion.Data;
//using Microsoft.Extensions.Logging;
//using System.Net.Http;
//using System.Net;
//using Newtonsoft.Json.Linq;
//using Microsoft.AspNetCore.Authorization;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.Extensions.Configuration;
//using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

using AutoMapper;
using Lucky_Draw_Promotion.Data;
using Lucky_Draw_Promotion.Models;
using Lucky_Draw_Promotion.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
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
                ModelState.AddModelError("", "Vui lòng kiểm tra email và mật khẩu của bạn sau đó thử lại");
                return View();
                
            }

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
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else
            {
                // log email failed 
            }
            return View(email);
        }


        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }
}

