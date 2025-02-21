using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ApplicationServices.Dtos.PersonDtos;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        #region [- Ctor -]
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }
        #endregion

        #region [- Login() -]

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginModel.UserName,
                    loginModel.Password,
                    loginModel.RememberMe,
                    false);
                if (result.Succeeded) { return LocalRedirect(loginModel.RedirectUrl); }
                ModelState.AddModelError("", "Invalid UserName or Password!");

                return Json(result);
            }
            return Ok();
        }
        #endregion

        #region [- Logout() -]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
        #endregion

        #region [- RegisterUser() -]

        [HttpPost]
        public async Task<IActionResult> RegisterUser(CreateUserModel createUserModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = createUserModel.UserName,
                    Email = createUserModel.Email,

                };
                var result = await _userManager.CreateAsync(user, createUserModel.Password);
                if (!result.Succeeded)
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError("", e.Description);
                    }
                }
                return Json(result);
            }
            return Ok();
        }
        #endregion

    }
}
