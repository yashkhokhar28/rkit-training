using Microsoft.AspNetCore.Mvc;
using EmployeeTaskManager.BL;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using Microsoft.Extensions.Configuration;
using EmployeeTaskManager.Extension;
using EmployeeTaskManager.Models.POCO;
using EmployeeTaskManager.Models;

namespace EmployeeTaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLAuthController : ControllerBase
    {
        private readonly BLAuth _blAuth;
        private Response objResponse;
        private BLConverter objBLConverter;

        public CLAuthController(BLAuth blAuth)
        {
            _blAuth = blAuth;
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var response = _blAuth.GetAllUser();
            return response.IsError ? BadRequest(response) : Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var response = _blAuth.GetUserByID(id);
            return response.IsError ? NotFound(response) : Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var response = _blAuth.Delete(id);
            return response.IsError ? BadRequest(response) : Ok(response);
        }

        [HttpPost("Save")]
        public IActionResult SaveUser([FromBody] DTOUSR01 userDto, [FromQuery] EnmEntryType entryType)
        {
            _blAuth.EnmEntryType = entryType;
            _blAuth.PreSave(userDto.Convert<USR01>());
            var response = _blAuth.Save();
            return response.IsError ? BadRequest(response) : Ok(response);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] DTOUSR02 userDto)
        {
            var response = _blAuth.Login(userDto);
            return response.IsError ? Unauthorized(response) : Ok(response);
        }
    }
}
