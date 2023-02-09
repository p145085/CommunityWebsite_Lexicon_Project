using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CommunityWebsite_Lexicon_Project.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? PasswordConfirm { get; set; }
    }
}
