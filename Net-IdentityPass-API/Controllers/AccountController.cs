using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Net_IdentityPass_API.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger;


        public AccountController(ILogger<AccountController> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
           
                var user = await _userManager.FindByEmailAsync(model.Email);

                //var userRole = GetUserRole(user).Result;
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false);

                if (user != null && result.Succeeded)
                {
                   
                     return RedirectToAction("settings", "user");
                   
                }

                ModelState.AddModelError(string.Empty, "Please check your credentials and try again");
            

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            try
            {
               

                    var user = new ApplicationUser
                    {
                        FullName = model.CompanyName,
                        UserName = model.Email,
                        Email = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    //var ownerId = _userManager.GetUserId(User);

                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("settings", "user");


                    }



                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }



                
            }
            catch (Exception error)
            {
                // throw error;
                return View(error);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"this email is currently in use");
            }


        }


    }
}
