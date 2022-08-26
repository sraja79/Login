using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class SignInModel
    {
        [Required]
        [Key]
        [Display(Name =" Login Mail ID")]
        public string Mail { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Remember Me")]
        public bool Rememberme { get; set; }
        public string ReturnUrl { get; set; } = " ";
        public IList<AuthenticationScheme>? ExternalLogin { get; set; }
    }
}
