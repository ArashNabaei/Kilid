using Kilid.Entities;
using Kilid.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kilid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _Agency;

        public AgencyController(IAgencyRepository agency)
        {
            _Agency = agency;
        }


        [HttpGet("GetEstateAgentById")]
        public async Task<IActionResult> GetEstateAgentById(int id)
        {
            var estateAgent = await _Agency.GetEstateAgentById(id);

            return Ok(estateAgent);
        }

        [HttpGet("GetAdvertisementById")]
        public async Task<IActionResult> GetAdvertisementById(int id)
        {
            var advertisement = await _Agency.GetAdvertisementById(id);

            return Ok(advertisement);
        }


        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] Advertisement advertisement)
        {
            await _Agency.CreateAdvertisement(advertisement);

            return Ok();

        }

        [HttpGet("GetAllAdvertisements")]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var result = await _Agency.GetAllAdvertisements();

            return Ok(result);
        }

        [HttpGet("GetAllAgencies")]
        public async Task<IActionResult> GetAllAgencies()
        {
            var result = await _Agency.GetAllAgencies();

            return Ok(result);
        }

        [HttpPut("UpdateEstateAgentProfile")]
        public async Task<IActionResult> UpdateEstateAgentProfile(User user)
        {
            await _Agency.UpdateEstateAgentProfile(user);

            return Ok();
        }

        [HttpPut("UpdateAgencyProfile")]
        public async Task<IActionResult> UpdateAgencyProfile(Agency agency)
        {
            await _Agency.UpdateAgencyProfile(agency);

            return Ok();
        }

        [HttpPut("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement(Advertisement advertisement)
        {
            await _Agency.UpdateAdvertisement(advertisement);

            return Ok();
        }

    }

}
