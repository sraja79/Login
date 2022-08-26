using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class ForgotPasswordModel
    {
        [Required , EmailAddress,Display(Name ="Registered Email Address"),Key]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
