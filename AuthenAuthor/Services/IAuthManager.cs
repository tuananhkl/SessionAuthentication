namespace AuthenAuthor.Services;

public interface IAuthManager
{
    Task<bool> ValidateUser(string userName, string password);
}