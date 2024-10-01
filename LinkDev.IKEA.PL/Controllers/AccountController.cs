using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Sign Up

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
		public async Task< IActionResult> SignUp(SignUpViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
			var User= await _userManager.FindByNameAsync(model.UserName);

            if (User is { })
            {
            	 ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This user name is already used");
				return View(model);

			}

                User = new ApplicationUser
                {

                    FName = model.FirstName,
                    LName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,


                };

                var Result = await _userManager.CreateAsync(User, model.Password);

                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }

                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            

            return View(model);
		}



        #endregion


        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
		public async Task< IActionResult> SignIn(SignInViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is { })
            {
                var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if (Flag)
                {
                    var Result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not confirmed yet");

                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is loked");

                    //if(Result.RequiresTwoFactor)

                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
                   
			
            }
			ModelState.AddModelError(string.Empty, "Invalid login attempt");

			return View(model);


		}
        #endregion

        #region Sign Out
        public async Task < IActionResult > SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion



    }
}
