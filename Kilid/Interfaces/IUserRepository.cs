using Kilid.Entities;

namespace Kilid.Interfaces
{
    public interface IUserRepository
    {

        Task CreateUser(User user);
        
        Task UpdateUser(int id, User user);
        
        Task<User> GetUserById(int id);

        Task<IEnumerable<User>> GetUsers();

        Task Rent(int userId, int buildingId);

        Task Buy(int userId, int buildingId);

        Task FullRent(int userId, int buildingId);

        Task<Building> GetBuildingById(int buildingId);

        Task<IEnumerable<Building>> GetAllBuildings();

        Task<IEnumerable<Building>> SearchByAddress(string pattern);

        Task<IEnumerable<Building>> SearchBuildingsByFilter(
            bool? isRentable, bool? isBuyable, bool? isFullRentable, int? area, 
            float? price, int? roomsCount, int? age);

        Task<User> GetUserByPhoneNumberAsync(string phoneNumber);

        Task AddUserAsync(User user);

        Task<IEnumerable<Search>> PopularSearches(string content);

    }
}
