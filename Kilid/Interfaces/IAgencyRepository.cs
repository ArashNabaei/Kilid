using Kilid.Entities;

namespace Kilid.Interfaces
{
    public interface IAgencyRepository
    {
        Task<User> GetManagerById(int id);

        Task<Advertisement> GetAdvertisementById(int id);

        Task CreateAdvertisement(Advertisement advertisement);

        Task<IEnumerable<Advertisement>> GetAllAdvertisements();

        Task<IEnumerable<Agency>> GetAllAgencies();

        Task UpdateAgencyProfile(int id, Agency agency);

        Task UpdateManagerProfile(int id,User user);

        Task UpdateAdvertisement(int id, Advertisement advertisement);

        Task<IEnumerable<Advertisement>> LastAdvertisements();

    }
}
