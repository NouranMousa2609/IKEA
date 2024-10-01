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

        public IActionResult SignIn()
        {
            return View();
        }
        #endregion
    }
}
