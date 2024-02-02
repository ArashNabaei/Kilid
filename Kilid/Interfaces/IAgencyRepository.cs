using Kilid.Entities;

namespace Kilid.Interfaces
{
    public interface IAgencyRepository
    {
        Task<User> GetEstateAgentById(int id);

        Task<Advertisement> GetAdvertisementById(int id);

        Task CreateAdvertisement(Advertisement advertisement);

        Task<IEnumerable<Advertisement>> GetAllAdvertisements();

        Task<IEnumerable<Agency>> GetAllAgencies();

        Task UpdateAgencyProfile(Agency agency);

        Task UpdateEstateAgentProfile(User user);

        Task UpdateAdvertisement(Advertisement advertisement);

    }
}
