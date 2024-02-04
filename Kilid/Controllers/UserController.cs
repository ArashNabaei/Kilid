using Kilid.Entities;
using Kilid.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kilid.Controllers
{
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _user;

        public UserController(IUserRepository user)
        {
            _user = user;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _user.CreateUser(user);
            return Ok();
        }


        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _user.GetUserById(id);
            return Ok(user);
        }
        
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _user.GetUsers();
            return Ok(users);
        }
        
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _user.DeleteUser(id);
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            await _user.UpdateUser(user);

            return Ok();
        }

        [HttpPut("Rent")]
        public async Task<IActionResult> Rent(int userId, int buildingId)
        {
            await _user.Rent(userId, buildingId);

            return Ok();
        }

        [HttpPut("Buy")]
        public async Task<IActionResult> Buy(int userId, int buildingId)
        {
            await _user.Buy(userId, buildingId);

            return Ok();
        }

        [HttpPut("FullRent")]
        public async Task<IActionResult> FullRent(int userId, int buildingId)
        {
            await _user.FullRent(userId, buildingId);

            return Ok();
        }

        [HttpGet("GetBuildingById")]
        public async Task<IActionResult> GetBuildingById(int buildingId)
        {
            var result = await _user.GetBuildingById(buildingId);

            return Ok(result);
        }

        [HttpGet("GetAllBuilding")]
        public async Task<IActionResult> GetAllBuilding()
        {
            var result = await _user.GetAllBuildings();

            return Ok(result);
        }

        [HttpGet("SearchByAddress")]
        public async Task<IActionResult> SearchByAddress(string pattern)
        {
            var result = await _user.SearchByAddress(pattern);

            return Ok(result);
        }

        [HttpGet("SearchByFilter")]
        public async Task<ActionResult<IEnumerable<Building>>> Search(
                [FromQuery] bool? isRentable,
                [FromQuery] bool? isBuyable,
                [FromQuery] bool? isFullRentable,
                [FromQuery] int? area,
                [FromQuery] float? price,
                [FromQuery] int? roomsCount,
                [FromQuery] int? age)
        {
            var buildings = await _user.SearchBuildingsByFilter(
                isRentable, isBuyable, isFullRentable,
                area, price, roomsCount, age);

            return Ok(buildings);
        }

    }
}
