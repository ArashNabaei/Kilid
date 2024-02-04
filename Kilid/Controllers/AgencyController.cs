using Kilid.Entities;
using Kilid.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kilid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agency;

        public AgencyController(IAgencyRepository agency)
        {
            _agency = agency;
        }

        #region Advertisement

        [HttpGet("GetAdvertisementById")]
        public async Task<IActionResult> GetAdvertisementById(int id)
        {
            var advertisement = await _agency.GetAdvertisementById(id);

            return Ok(advertisement);
        }

        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] Advertisement advertisement)
        {
            await _agency.CreateAdvertisement(advertisement);

            return Ok();

        }

        [HttpGet("GetAllAdvertisements")]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var result = await _agency.GetAllAdvertisements();

            return Ok(result);
        }

        [HttpGet("LastAdvertisements")]
        public async Task<IActionResult> LastAdvertisements()
        {
            var result = await _agency.LastAdvertisements();

            return Ok(result);
        }

        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement(Advertisement advertisement)
        {
            await _agency.UpdateAdvertisement(advertisement);

            return Ok();
        }

        [HttpDelete("DeleteAdvertisement")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            await _agency.DeleteAdvertisement(id);

            return Ok();
        }

        #endregion

        #region Agency

        [HttpGet("GetAllAgencies")]
        public async Task<IActionResult> GetAllAgencies()
        {
            var result = await _agency.GetAllAgencies();

            return Ok(result);
        }

        [HttpPut("UpdateManagerProfile")]
        public async Task<IActionResult> UpdateManagerProfile(User user)
        {
            await _agency.UpdateManagerProfile(user);

            return Ok();
        }

        [HttpPut("UpdateAgencyProfile")]
        public async Task<IActionResult> UpdateAgencyProfile(Agency agency)
        {
            await _agency.UpdateAgencyProfile(agency);

            return Ok();
        }

        [HttpGet("GetManagerById")]
        public async Task<IActionResult> GetManagerById(int id)
        {
            var estateAgent = await _agency.GetManagerById(id);

            return Ok(estateAgent);
        }

        #endregion

    }

}
