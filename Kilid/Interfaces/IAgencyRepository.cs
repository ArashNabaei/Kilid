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

        Task UpdateAgencyProfile(Agency agency);

        Task UpdateManagerProfile(User user);

        Task UpdateAdvertisement(Advertisement advertisement);

        Task CreateAddress(Address address);

        Task UpdateAddress(Address address);

        Task<IEnumerable<Advertisement>> LastAdvertisements();

        Task DeleteAdvertisement(int id);

        Task DeleteAddress(int id);

    }
}
