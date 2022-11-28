using AuthenAuthor.DbContext;
using AuthenAuthor.Models;
using AuthenAuthor.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace AuthenAuthor.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetById(int? id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(m => m.Id == id);
        return user;
    }

    public async Task<User> GetByUserName(string userName)
    {
        var user = await _context.Users.Where(u => u.UserName.ToLower().Equals(userName)).FirstOrDefaultAsync();
        return user;
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}