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

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            objResponse = _blAuth.GetAllUser();
            return objResponse.IsError ? StatusCode(500, objResponse) : Ok(objResponse);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            objResponse = _blAuth.GetUserByID(id);
            return objResponse.IsError ? NotFound(objResponse) : Ok(objResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            objResponse = _blAuth.Delete(id);
            return objResponse.IsError ? BadRequest(objResponse) : Ok(objResponse);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] DTOUSR01 objDTOUSR01)
        {
            if (objDTOUSR01 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid user data.";
                return BadRequest(objResponse);
            }

            _blAuth.EnmEntryType = EnmEntryType.A;
            _blAuth.PreSave(objDTOUSR01);
            objResponse = _blAuth.Save();
            return objResponse.IsError ? BadRequest(objResponse) : Ok(objResponse);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOUSR02 objDTOUSR02)
        {
            if (objDTOUSR02 == null || string.IsNullOrWhiteSpace(objDTOUSR02.R01F02) || string.IsNullOrWhiteSpace(objDTOUSR02.R01F02))
            {
                objResponse.IsError = true;
                objResponse.Message = "Username and password are required.";
                return BadRequest(objResponse);
            }

            objResponse = _blAuth.Login(objDTOUSR02);
            return objResponse.IsError ? Unauthorized(objResponse) : Ok(objResponse);
        }
    }
}