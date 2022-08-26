using Login.Models;
using Login.Service;
using Microsoft.AspNetCore.Identity;

namespace Login.Repository
{
    public class AccountRepository :IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountRepository(UserManager<ApplicationUser> user,SignInManager<ApplicationUser> signInManager, IUserService  userService) 
        {
            _user = user;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            var result = await _user.CreateAsync(user, model.Password);
            return result;

        }
        public async Task<SignInResult> SiginPasswordAsync(SignInModel model)
        {
            var user = new ApplicationUser
            {
                
                UserName=model.Mail,
                
                
            };
          var result=  await _signInManager.PasswordSignInAsync(model.Mail, model.Password, model.Rememberme, false);
            return result;
        }
        public async Task GenerateForgotPasswordAsync(ApplicationUser user)
        {
            var token = await _user.GenerateEmailConfirmationTokenAsync(user);
            if(!string.IsNullOrEmpty(token))
            {

            }
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();

        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel passwordModel)
        {
            var userid =  _userService.GetUserId();
            var user = await _user.FindByIdAsync(userid);
          return await _user.ChangePasswordAsync(user, passwordModel.CurrentPassword, passwordModel.NewPassword);
            
        }
    }
}
