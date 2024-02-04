using Kilid.Entities;

namespace Kilid.Services
{
    public interface IAuthService
    {
        Task<User> SignUpAsync(string phoneNumber);
        Task<User> SignInAsync(string phoneNumber, string password);

    }
}
