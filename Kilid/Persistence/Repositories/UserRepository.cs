using Dapper;
using Kilid.Entities;
using Kilid.Interfaces;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace Kilid.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CreateUser(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("firstName", user.FirstName);
            parameters.Add("lastName", user.LastName);
            parameters.Add("email", user.Email);
            parameters.Add("phoneNumber", user.PhoneNumber);
            parameters.Add("password", user.Password);

            var query = "INSERT INTO Users (FirstName, LastName, Email, PhoneNumber, Password) " +
                        "VALUES (@firstName, @lastName, @email, @phoneNumber, @password);" +
                        "SELECT SCOPE_IDENTITY();";

            user.Id = await _dbContext.Connection.QuerySingleOrDefaultAsync<int>(query, parameters);
        }

        public async Task<User> GetUserById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT * FROM Users WHERE ID = @id;";
            
            var user = await _dbContext.Connection.QueryFirstAsync<User>(query, parameters);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var query = "SELECT * FROM Users";
            var users = await _dbContext.Connection.QueryAsync<User>(query);

            return users;
        }

        public async Task UpdateUser(int id, User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            parameters.Add("firstName", user.FirstName);
            parameters.Add("lastName", user.LastName);
            parameters.Add("email", user.Email);
            parameters.Add("password", user.Password);
            parameters.Add("phoneNumber", user.PhoneNumber);

            var query = "UPDATE Users SET FirstName = @firstName, LastName = @lastName, " +
                        "Password = @password, Email = @email, PhoneNumber = @phoneNumber " +
                        "WHERE Id = @id;";

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task Buy(int userId, int buildingId)
        {
            var buildingQuery = "SELECT * FROM Building " +
                "WHERE Id = @buildingId";

            var parameters = new DynamicParameters();
            parameters.Add("buildingId", buildingId);

            var building = await _dbContext.Connection.QueryFirstAsync<Building>(buildingQuery, parameters);

            if (!building.IsBuyable)
                throw new Exception("Can not buy this building.");

            var query = "UPDATE Users " +
                "SET BuildingId = @buildingId " +
                "WHERE Id = @userId;";

            var userParameters = new DynamicParameters();
            userParameters.Add("buildingId", buildingId);
            userParameters.Add("userId", userId);

            await _dbContext.Connection.ExecuteAsync(query, userParameters);

        }

        public async Task Rent(int userId, int buildingId)
        {
            var buildingQuery = "SELECT * FROM Building " +
                "WHERE Id = @buildingId";

            var parameters = new DynamicParameters();
            parameters.Add("buildingId", buildingId);

            var building = await _dbContext.Connection.QueryFirstAsync<Building>(buildingQuery, parameters);

            if (!building.IsRentable)
                throw new Exception("Can not rent this building.");

            var query = "UPDATE Users " +
                "SET BuildingId = @buildingId " +
                "WHERE Id = @userId;";

            var userParameters = new DynamicParameters();
            userParameters.Add("buildingId", buildingId);
            userParameters.Add("userId", userId);

            await _dbContext.Connection.ExecuteAsync(query, userParameters);
        }

        public async Task FullRent(int userId, int buildingId)
        {
            var buildingQuery = "SELECT * FROM Building " +
                "WHERE Id = @buildingId";

            var parameters = new DynamicParameters();
            parameters.Add("buildingId", buildingId);

            var building = await _dbContext.Connection.QueryFirstAsync<Building>(buildingQuery, parameters);

            if (!building.IsFullRentable)
                throw new Exception("Can not full rent this building.");

            var query = "UPDATE Users " +
                "SET BuildingId = @buildingId " +
                "WHERE Id = @userId;";

            var userParameters = new DynamicParameters();
            userParameters.Add("buildingId", buildingId);
            userParameters.Add("userId", userId);

            await _dbContext.Connection.ExecuteAsync(query, userParameters);
        }

        public async Task<Building> GetBuildingById(int buildingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", buildingId);

            var query = "SELECT * FROM Building WHERE Id = @id;";
            
            var user = await _dbContext.Connection.QueryFirstAsync<Building>(query, parameters);
            
            return user;
        }

        public async Task<IEnumerable<Building>> GetAllBuildings()
        {
            var query = "SELECT * FROM Building;";

            var buildings = await _dbContext.Connection.QueryAsync<Building>(query);

            return buildings;
        }

        public async Task<IEnumerable<Building>> SearchByAddress(string pattern)
        {
            var query = $"SELECT * FROM Building WHERE LOWER(Address) LIKE LOWER(@pattern);";

            var patternWithWildcards = $"%{pattern.ToLower()}%";

            var parameters = new DynamicParameters();
            parameters.Add("pattern", patternWithWildcards);


            var buildings = await _dbContext.Connection.QueryAsync<Building>(query, parameters);

            return buildings;
        }

        public async Task<IEnumerable<Building>> SearchBuildingsByFilter(
            bool? isRentable, bool? isBuyable, bool? isFullRentable,
            int? area, float? price, int? roomsCount, int? age)
        {
            var query = "SELECT * FROM Building WHERE 1=1";

            if (isRentable.HasValue)
                query += " AND IsRentable = @isRentable";

            if (isBuyable.HasValue)
                query += " AND IsBuyable = @isBuyable";

            if (isFullRentable.HasValue)
                query += " AND IsFullRentable = @isFullRentable";

            if (area.HasValue)
                query += " AND Area = @area";

            if (price.HasValue)
                query += " AND Price = @price";

            if (roomsCount.HasValue)
                query += " AND RoomsCount = @roomsCount";

            if (age.HasValue)
                query += " AND Age = @age";

            var parameters = new DynamicParameters();
            parameters.Add("isRentable", isRentable);
            parameters.Add("isBuyable", isBuyable);
            parameters.Add("isFullRentable", isFullRentable);
            parameters.Add("area", area);
            parameters.Add("price", price);
            parameters.Add("roomsCount", roomsCount);
            parameters.Add("age", age);

            var buildings = await _dbContext.Connection.QueryAsync<Building>(query, parameters);
            return buildings;
        }

        public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE PhoneNumber = @phoneNumber",
                new { phoneNumber }
            );

        }

        public async Task AddUserAsync(User user)
        {

            var query = "INSERT INTO Users (FirstName, LastName, Password, Email, PhoneNumber, BuildingId) " +
                        "VALUES (@firstName, @lastName, @password, @email, @phoneNumber, @buildingId)";

            var parameters = new DynamicParameters();
            parameters.Add("firstName", user.FirstName);
            parameters.Add("lastName", user.LastName);
            parameters.Add("email", user.Email);
            parameters.Add("phoneNumber", user.PhoneNumber);
            parameters.Add("password", user.Password);
            parameters.Add("buildingId", user.BuildingId);

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Search>> PopularSearches(string content)
        {
            var query = $"SELECT TOP 5 * " +
                $"FROM Search " +
                $"WHERE LOWER(Content) LIKE LOWER(CONCAT('%', @content, '%')) " +
                $"ORDER BY Count DESC;";

            var patternWithWildcards = $"%{content.ToLower()}%";

            var parameters = new DynamicParameters();
            parameters.Add("content", patternWithWildcards);


            var searches = await _dbContext.Connection.QueryAsync<Search>(query, parameters);

            return searches;
        }
    }
}
