using amoboe.Models;
using amoboe.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace amoboe.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            //if(registerVM == null) return View();
            AppUser user = new AppUser()
            {
                Name=registerVM.Name,
                Email=registerVM.Email,
                Surname=registerVM.Surname,
                UserName=registerVM.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(user,registerVM.Password);
            if (result.Succeeded)
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,"user yoxdu");
                }
                return View();
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index","Home");
           
                
            

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginVM loginVM)
        {
            if(loginVM == null) return View();
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(loginVM.Password) ;
                if(appUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Not Found");
                }
                return View();

            }
            await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, true);
            return RedirectToAction("Index", "Home");
            
        }
        public async Task<IActionResult> Logout()
        {   await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
