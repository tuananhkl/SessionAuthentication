using AuthenAuthor.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AuthenAuthor.Services;

public class AuthManager : IAuthManager
{
    private readonly AppDbContext _context;

    public AuthManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateUser(string userName, string password)
    {
        var user = await _context.Users.Where(u => u.UserName.Equals(userName) 
                                                   && u.Password.Equals(password)).FirstOrDefaultAsync();
        if (user is not null)
        {
            return true;
        }
        
        return false;
    }
}