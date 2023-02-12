using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<Account> GetAccountByUsernameAsync(string username);
        Task<Account> GetAccountByEmailAsync(string email);
        Task AddAsync(Account account);
    }
}
