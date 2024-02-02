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

        public async Task<int> Authentication(string username, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("username", username);
            parameters.Add("password", password);

            var query = "SELECT Id FROM EstateAgents WHERE Username = @username AND Password = @password;";

            var id =  await _dbContext.Connection.QueryFirstAsync<int>(query, parameters);

            return id;
        }

        public async Task CreateEstateAgent(int id, string phoneNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("phoneNumber", phoneNumber);

            var query = "INSERT INTO EstateAgents(Id, PhoneNumber)VALUES(@id, @phoneNumber);";

            await _dbContext.Connection.QueryFirstAsync(query, parameters);
        }

        public async Task<EstateAgent> GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var query = "SELECT * FROM EstateAgents WHERE Id = @id;";

            var estateAgent = await _dbContext.Connection.QueryFirstAsync<EstateAgent>(query, parameters);

            return estateAgent;
        }

        public async Task Update(int id, EstateAgent estateAgent)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("firstName", estateAgent.FirstName);
            parameters.Add("lastName", estateAgent.LastName);
            parameters.Add("username", estateAgent.Username);
            parameters.Add("password", estateAgent.Password);
            parameters.Add("email", estateAgent.Email);
            parameters.Add("phoneNumber", estateAgent.PhoneNumber);
            parameters.Add("city", estateAgent.City);
            parameters.Add("agencyName", estateAgent.AgencyName);
            parameters.Add("agencyPhoneNumber", estateAgent.AgencyPhoneNumber);
            parameters.Add("employeeCount", estateAgent.EmployeeCount);

            var query = "UPDATE EstateAgents " +
                "SET FirstName = @firstName, LastName = @lastName, " +
                "Username = @username, Password = @password, " +
                "Email = @email, PhoneNumber = @phoneNumber," +
                "City = @city, AgencyName = @agencyName," +
                "AgencyPhoneNumber = @agencyPhoneNumber, EmployeeCount = @employeeCount" +
                "WHERE Id = @id;";

            await _dbContext.Connection.QueryFirstAsync(query, parameters);

        }

        public async Task CreateAdvertisement(int AdvertisementId, int buildingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("id", AdvertisementId);
            parameters.Add("buildingId", buildingId);

            var query = "INSERT INTO Advertisements(Id, PhoneNumber)VALUES(@id, @buildingId);";

            await _dbContext.Connection.QueryFirstAsync(query, parameters);
        }

        public async Task<List<Advertisement>> GetAllAdvertisements()
        {
            var query = "SELECT * FROM Advertisements;";

            var advertisements = await _dbContext.Connection.QueryFirstAsync<List<Advertisement>>(query);

            return advertisements;
        }

        public async Task<List<EstateAgent>> GetAllEstateAgents()
        {
            var query = "SELECT * FROM EstateAgents;";

            var estateAgents = await _dbContext.Connection.QueryFirstAsync<List<EstateAgent>>(query);

            return estateAgents;
        }
    }
}
