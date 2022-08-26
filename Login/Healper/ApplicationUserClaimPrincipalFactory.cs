using Login.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Login.Healper
{
    public class ApplicationUserClaimPrincipalFactory:UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>
    {
        public ApplicationUserClaimPrincipalFactory(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IOptions<IdentityOptions> identityOption):base(userManager, roleManager, identityOption)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity= await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FirstName", user.FirstName??""));
            identity.AddClaim(new Claim("LastName", user.LastName??""));
            return identity;
        }
    }
}
