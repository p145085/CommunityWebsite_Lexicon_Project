using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? Username { get; set; }
        //[Required]
        //[Display(Name = "Email")]
        //public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
