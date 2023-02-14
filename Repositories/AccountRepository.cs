using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Account> GetAccountByIdAsync(Guid id)
        {
            string convId = id.ToString();
            return _context.Accounts.FirstOrDefaultAsync(x => x.Id == convId);
        }

        public Task<Account> GetAccountByUsernameAsync(string username)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<Account> GetAccountByEmailAsync(string email)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public async Task AddAsync(Account account)
        {
            if (!string.IsNullOrEmpty(account.UserName))
            {
                if (!string.IsNullOrEmpty(account.Email))
                {
                    await _context.Accounts.AddAsync(account);
                }
                else
                {
                    throw new Exception("You must supply an email.");
                }
            } else
            {
                throw new Exception("You must supply a username.");
            }
        }
    }
}
