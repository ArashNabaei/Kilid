using Kilid.Entities;
using Kilid.Interfaces;

namespace Kilid.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> SignUpAsync(string phoneNumber)
        {
            // Check if the user with the given phone number already exists
            var existingUser = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);
            if (existingUser != null)
            {
                // Handle error (user already exists)
                // You may want to throw an exception or return a specific result indicating the failure.
                return null;
            }

            // Create a new user for sign-up
            var newUser = new User
            {
                PhoneNumber = phoneNumber,
                // Other properties as needed
            };

            // Save the new user to the database
            await _userRepository.AddUserAsync(newUser);

            return newUser;
        }

        public async Task<User> SignInAsync(string phoneNumber, string password)
        {
            // Retrieve the user by phone number
            var user = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);

            // Check if the user exists and the password matches
            if (user != null && user.Password == password)
            {
                return user;
            }

            // Handle authentication failure
            // You may want to throw an exception or return a specific result indicating the failure.
            return null;
        }

    }
}
