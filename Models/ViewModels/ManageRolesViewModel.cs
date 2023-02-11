using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class ManageRolesViewModel
    {
        public IdentityRole Role { get; set; }
        public IList<Account> AccountsWithRole { get; set; }
        public IList<Account> AccountsWithNoRole { get; set; }
    }
}
