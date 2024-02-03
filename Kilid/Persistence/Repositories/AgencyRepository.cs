using Kilid.Interfaces;
using Dapper;
using Kilid.Entities;

namespace Kilid.Persistence.Repositories
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly DbContext _dbContext;

        public AgencyRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetManagerById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT * FROM Users WHERE Id = @id;";

            var estateAgent = await _dbContext.Connection.QueryFirstAsync<User>(query, parameters);

            return estateAgent;
        }

        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT * FROM Advertisements WHERE Id = @id;";

            var advertisement = await _dbContext.Connection.QueryFirstAsync<Advertisement>(query, parameters);

            return advertisement;
        }

        public async Task CreateAdvertisement(Advertisement advertisement)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", advertisement.Id);
            parameters.Add("buildingId", advertisement.BuildingId);
            parameters.Add("title", advertisement.Title);
            parameters.Add("description", advertisement.Description);
            parameters.Add("conditions", advertisement.Conditions);
            parameters.Add("features", advertisement.Features);

            var query = "INSERT INTO Advertisements(Id, BuildingId, Title, Description, Conditions, Features) " +
                "VALUES(@id, @buildingId, @title, @description, @conditions, @features)";

           await _dbContext.Connection.QueryFirstOrDefaultAsync(query, parameters);
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements()
        {
            var query = "SELECT * FROM Advertisements;";

            var advertisements = await _dbContext.Connection.QueryAsync<Advertisement>(query);

            return advertisements;
        }

        public async Task<IEnumerable<Agency>> GetAllAgencies()
        {
            var query = "SELECT * FROM Agency;";

            var agencies = await _dbContext.Connection.QueryAsync<Agency>(query);

            return agencies;
        }

        public async Task UpdateManagerProfile(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", user.Id);
            parameters.Add("firstName", user.FirstName);
            parameters.Add("lastName", user.LastName);
            parameters.Add("password", user.Password);
            parameters.Add("email", user.Email);
            parameters.Add("phoneNumber", user.PhoneNumber);

            var query = "UPDATE Users SET FirstName = @firstName, LastName = @lastName, " +
                        "Password = @password, Email = @email, PhoneNumber = @phoneNumber " +
                        "WHERE Id = @id;";

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task UpdateAgencyProfile(Agency agency)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", agency.Id);
            parameters.Add("managerId", agency.ManagerId);
            parameters.Add("name", agency.Name);
            parameters.Add("city", agency.City);
            parameters.Add("phoneNumber", agency.PhoneNumber);
            parameters.Add("employeeCount", agency.EmployeeCount);

            var query = "UPDATE Agency SET ManagerId = @managerId, Name = @name, " +
                        "City = @city, PhoneNumber = @phoneNumber, EmployeeCount = @employeeCount " +
                        "WHERE Id = @id;";

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task UpdateAdvertisement(Advertisement advertisement)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", advertisement.Id);
            parameters.Add("buildingId", advertisement.BuildingId);
            parameters.Add("title", advertisement.Title);
            parameters.Add("description", advertisement.Description);
            parameters.Add("conditions", advertisement.Conditions);
            parameters.Add("features", advertisement.Features);

            var query = "UPDATE Advertisements SET Title = @title, BuildingId = @buildingId " +
                "Description = @description, Conditions = @conditions, Features = @features" +
                        "WHERE Id = @id;";

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task CreateAddress(Address address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", address.Id);
            parameters.Add("city", address.City);
            parameters.Add("neighbor", address.Neighbor);

            var query = "INSERT INTO Address(Id, City, Neighbor) " +
                "VALUES(@id, @city, @neighbor)";

            await _dbContext.Connection.QueryFirstOrDefaultAsync(query, parameters);
        }

        public async Task UpdateAddress(Address address)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", address.Id);
            parameters.Add("city", address.City);
            parameters.Add("neighbor", address.Neighbor);

            var query = "UPDATE Address SET City = @city, Neighbor= @neighbor " +
                        "WHERE Id = @id;";

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Advertisement>> LastAdvertisements()
        {
            var query = "SELECT TOP 5 * FROM Advertisements ORDER BY Id DESC;";

            var advertisements = await _dbContext.Connection.QueryAsync<Advertisement>(query);

            return advertisements;
        }

        public async Task DeleteAdvertisement(int id)
        {
            var query = "DELETE FROM Advertisements " +
                "WHERE Id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAddress(int id)
        {
            var query = "DELETE FROM Address " +
                "WHERE Id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            await _dbContext.Connection.ExecuteAsync(query, parameters);
        }
    }
}
