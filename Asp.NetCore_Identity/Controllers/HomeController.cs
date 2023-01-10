using Asp.NetCore_Identity.Entities;
using Asp.NetCore_Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Asp.NetCore_Identity.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<AppRole> RoleManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            RoleManager = roleManager;
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
        public async Task<IActionResult> Create(UserCreateModel userCreateModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = userCreateModel.Email,  
                    Gender = userCreateModel.Gender,
                    UserName = userCreateModel.UserName
                };

                
                var identityResult = await userManager.CreateAsync(user, userCreateModel.Password);
                if (identityResult.Succeeded)
                {
                    await RoleManager.CreateAsync(new()
                    {
                        Name = "Admin",
                        CreatedDate = DateTime.UtcNow
                    });
                    await userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(userCreateModel);
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, true);
                if (signInResult.Succeeded)
                {
                    // bu iş başarılı

                    var user = await userManager.FindByNameAsync(model.UserName);
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                        return RedirectToAction("AdminPanel");                   
                    else
                        return RedirectToAction("Panel");
                    
                }
                else if (signInResult.IsLockedOut)
                {
                    // hesap kilitli
                }
                else if (signInResult.IsNotAllowed)
                {
                    // email veya phone number doğrulanmış mı
                }
                
            }
            return View(model);
        }
        [Authorize]
        public IActionResult GetUserInfo()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public IActionResult Panel()
        {
            return View();
        }
    }
}
