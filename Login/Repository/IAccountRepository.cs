using Login.Models;
using Microsoft.AspNetCore.Identity;

namespace Login.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpModel model);
        Task<SignInResult> SiginPasswordAsync(SignInModel model);

         Task SignOut();
        Task<IdentityResult> ChangePassword(ChangePasswordModel passwordModel);


    }
}