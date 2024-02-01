using Kilid.Entities;

namespace Kilid.Interfaces
{
    public interface IEstateAgentRepository
    {
        Task<int> Authentication(string username, string password);

        Task CreateEstateAgent(int id, string phoneNumber);

        Task<EstateAgent> GetById(int id);

        Task Update(int id, EstateAgent estateAgent);

        Task CreateAdvertisement(int advertisementId, int buildingId);

        Task<List<Advertisement>> GetAllAdvertisements();

        Task<List<EstateAgent>> GetAllEstateAgents();

    }
}
