using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class ChangePasswordModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
     
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Confirm Password not match with New password")]
        public string ConfirmPassword { get; set; }
    }
}
