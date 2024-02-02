using Kilid.Entities;
using Kilid.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kilid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateAgentController : ControllerBase
    {
        private readonly IAgencyRepository _estateAgent;

        public EstateAgentController(IAgencyRepository estateAgent)
        {
            _estateAgent = estateAgent;
        }

        [HttpGet("Authentication")]
        public async Task<IActionResult> Authenticate(string username, string password)
        {
            var result = await _estateAgent.Authentication(username, password);

            return Ok(result);
        }

        [HttpGet("Information")]
        public async Task<IActionResult> Information(int id)
        {
            var result = await _estateAgent.GetById(id);

            return Ok(result);
        }

        [HttpPost("AddEstateAgent")]
        public async Task<IActionResult> AddEstateAgent(int id, string phoneNumber)
        {
            await _estateAgent.CreateEstateAgent(id, phoneNumber);

            return Ok();
        }

        [HttpGet("GetEstateAgent")]
        public async Task<IActionResult> GetEstateAgent(int id)
        {
            var result = await _estateAgent.GetById(id);

            return Ok(result);
        }

        [HttpPut("UpdateEstateAgent")]
        public async Task<IActionResult> UpdateEstateAgent(int id, EstateAgent estateAgent)
        {
            await _estateAgent.Update(id, estateAgent);

            return Ok();
        }

        [HttpGet("GetAllAdvertisements")]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var result = await _estateAgent.GetAllAdvertisements();

            return Ok(result);
        }

        [HttpGet("GetAllEstateAgents")]
        public async Task<IActionResult> GetAllEstateAgents()
        {
            var result = await _estateAgent.GetAllEstateAgents();

            return Ok(result);
        }

    }

}
