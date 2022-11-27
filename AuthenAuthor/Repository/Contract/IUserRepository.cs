using AuthenAuthor.Models;

namespace AuthenAuthor.Repository.Contract;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();

    Task<User> GetById(int id);
    
    Task Add(User user);
    
    Task Update(User user);
    
    //Task Delete(int id);
    Task Delete(User user);
}