using Microsoft.AspNetCore.Mvc;
using EmployeeTaskManager.BL;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using Microsoft.Extensions.Configuration;
using EmployeeTaskManager.Extension;
using EmployeeTaskManager.Models.POCO;
using EmployeeTaskManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTaskManager.Controllers
{
    /// <summary>
    /// API Controller for handling authentication and user management operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLAuthController : ControllerBase
    {
        /// <summary>
        /// Business logic layer instance for authentication operations
        /// </summary>
        private readonly BLAuth _blAuth;

        /// <summary>
        /// Response object for returning operation results
        /// </summary>
        private Response objResponse;

        /// <summary>
        /// Constructor for CLAuthController
        /// </summary>
        /// <param name="blAuth">Business logic layer instance for authentication</param>
        public CLAuthController(BLAuth blAuth)
        {
            _blAuth = blAuth;
            objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all users from the system
        /// </summary>
        /// <returns>IActionResult with all users or error response</returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            objResponse = _blAuth.GetAllUser();
            return objResponse.IsError ? StatusCode(500, objResponse) : Ok(objResponse);
        }

        /// <summary>
        /// Retrieves a specific user by their ID
        /// </summary>
        /// <param name="ID">The ID of the user to retrieve</param>
        /// <returns>IActionResult with user data or not found response</returns>
        [HttpGet("ID")]
        public IActionResult GetUserById(int ID)
        {
            objResponse = _blAuth.GetUserByID(ID);
            return objResponse.IsError ? NotFound(objResponse) : Ok(objResponse);
        }

        /// <summary>
        /// Deletes a user from the system (Admin only)
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpDelete("ID")]
        [Authorize(Policy = "Admin")] // Restricts access to Admin role
        public IActionResult DeleteUser(int id)
        {
            objResponse = _blAuth.Delete(id);
            return objResponse.IsError ? BadRequest(objResponse) : Ok(objResponse);
        }

        /// <summary>
        /// Registers a new user in the system (Admin only)
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing user registration data</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPost("register")]
        [Authorize(Policy = "Admin")] // Restricts access to Admin role
        public IActionResult Register([FromBody] DTOUSR01 objDTOUSR01)
        {
            // Validate input data
            if (objDTOUSR01 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid user data.";
                return BadRequest(objResponse);
            }

            // Set operation type and process registration
            _blAuth.EnmEntryType = EnmEntryType.A;
            _blAuth.PreSave(objDTOUSR01);
            objResponse = _blAuth.Save();
            return objResponse.IsError ? BadRequest(objResponse) : Ok(objResponse);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        /// <param name="objDTOUSR02">DTO containing login credentials</param>
        /// <returns>IActionResult with authentication result and token or error</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTOUSR02 objDTOUSR02)
        {
            // Validate login credentials
            if (objDTOUSR02 == null || string.IsNullOrWhiteSpace(objDTOUSR02.R01F02) || string.IsNullOrWhiteSpace(objDTOUSR02.R01F03))
            {
                objResponse.IsError = true;
                objResponse.Message = "Username and password are required.";
                return BadRequest(objResponse);
            }

            // Process login request
            objResponse = _blAuth.Login(objDTOUSR02);
            return objResponse.IsError ? Unauthorized(objResponse) : Ok(objResponse);
        }

        /// <summary>
        /// Updates an existing user's information
        /// </summary>
        /// <param name="objDTOUSR01">DTO containing updated user data</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPut]
        public IActionResult EditUser([FromBody] DTOUSR01 objDTOUSR01)
        {
            // Validate input data
            if (objDTOUSR01 == null)
            {
                objResponse.IsError = true;
                objResponse.Message = "Invalid user data.";
                return BadRequest(objResponse);
            }

            // Set operation type and process update
            _blAuth.EnmEntryType = EnmEntryType.E;
            _blAuth.PreSave(objDTOUSR01);
            objResponse = _blAuth.Save();
            return objResponse.IsError ? BadRequest(objResponse) : Ok(objResponse);
        }
    }
}