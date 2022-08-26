using Login.Models;
using Login.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Login.Controllers
{
 
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountRepository accountRepository, SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region   SignUp  
        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

   [HttpPost]
        public async Task<IActionResult> SingUp(SignUpModel  model)
        {
            if(ModelState.IsValid)
            {
             var result=   await _accountRepository.CreateUserAsync(model);
                if(!result.Succeeded )
                {
                    foreach (var errormsg in result.Errors)
                    {
                        ModelState.AddModelError("",errormsg.Description);
                    }
                   
                }
                else
                {
                    ModelState.Clear();
                }

            }
            else
            {
                
            }
            return View();
        }
        #endregion

        #region SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signIn)

        {
            if(ModelState.IsValid)
            {
               var result = await _accountRepository.SiginPasswordAsync(signIn);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
          
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> SignIn(string ReturnUrl)

        {
            SignInModel model = new SignInModel()
            {
                ReturnUrl = ReturnUrl,
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };

            return View();

        }

        #endregion
        [Authorize]
        #region ChangePassword
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel passwordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePassword(passwordModel);
                if (result.Succeeded)
                {
                   // ViewBag.isSuccess = "true";
                    ModelState.Clear();
                    return View();

                }
            }
            else
            {
                //foreach (var errormsg in result.Errors)
                //{
                //    ModelState.AddModelError("", errormsg.Description);
                //}
            }

            return View();
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOut();
          //  _signInManager.IsSignedIn = false;
            return RedirectToAction("SignIn", "Account",new {ReturnUrl=" "});
        }

        #endregion

        #region ExternalUserLogin 
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string ExternalProvider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(ExternalProvider, redirectUrl);
            return new ChallengeResult(ExternalProvider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            SignInModel SigninViewModel = new SignInModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider:{remoteError}");
                return View("SignIn", SigninViewModel);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error Loading External login information .");
                return View("SignIn", SigninViewModel);
            }
            var SignInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (SignInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await  _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            FirstName= info.Principal.FindFirstValue(ClaimTypes.Email),
                            LastName= info.Principal.FindFirstValue(ClaimTypes.Email)

                        };
                        await _userManager.CreateAsync(user);
                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            return LocalRedirect(returnUrl);
        }
        #endregion

        #region ForgotPassword
        [AllowAnonymous,HttpGet()]
        public  IActionResult ForgotPassword()
        {
            return View();  
        }
        [AllowAnonymous, HttpPost()]
        public IActionResult ForgotPassword(ForgotPasswordModel model )
        {
            if(ModelState.IsValid)
            {
                //code here
                ModelState.Clear();
                model.EmailSent = true;
            }
           

            return View(model);
        }
        #endregion

        //https://youtu.be/fgzRnlB992s

    }
}
