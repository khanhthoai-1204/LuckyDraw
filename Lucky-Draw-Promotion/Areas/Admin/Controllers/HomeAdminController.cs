using AutoMapper;
using Lucky_Draw_Promotion.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Lucky_Draw_Promotion.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Administrator")]
    public class HomeAdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeAdminController(IMapper mapper,UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;


        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var _user = new User();
                IdentityResult result = await _userManager.CreateAsync(_user, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    return View(result);
                   
            }
            return View(user);
        }
                

        //if (ModelState.IsValid)
        //{
        //    // appUser = new AppUser
        //    //{

        //    //    Email = user.Email,

        //    //};

        //    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
        //    if (result.Succeeded)
        //        return RedirectToAction("Index");
        //    else
        //    {
        //        foreach (IdentityError error in result.Errors)
        //            ModelState.AddModelError("", error.Description);
        //    }
        //}
        //return View(user);
    }
        }

