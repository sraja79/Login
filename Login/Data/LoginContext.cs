using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Login.Models;

namespace Login.Data
{
    public class LoginContext:IdentityDbContext<ApplicationUser>
    {
        public LoginContext(DbContextOptions<LoginContext> options):base(options)
        {

        }
        public DbSet<Login.Models.ForgotPasswordModel>? ForgotPasswordModel { get; set; }
        //public DbSet<Login.Models.SignUpModel>? SignUpModel { get; set; }
        //public DbSet<Login.Models.SignInModel>? SignInModel { get; set; }
        //public DbSet<Login.Models.ChangePasswordModel>? ChangePasswordModel { get; set; }
    }
}
