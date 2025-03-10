using EmployeeTaskManager.BL;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTaskManager.Controllers
{
    /// <summary>
    /// API Controller for managing department-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLDepartmentController : ControllerBase
    {
        /// <summary>
        /// Business logic layer instance for department operations
        /// </summary>
        private readonly BLDepartment objBLDepartment;

        /// <summary>
        /// Response object for returning operation results
        /// </summary>
        private Response objResponse;

        /// <summary>
        /// Converter instance for transforming objects to DataTables
        /// </summary>
        private BLConverter objBLConverter;

        /// <summary>
        /// Constructor for CLDepartmentController
        /// </summary>
        /// <param name="bLDepartment">Business logic layer instance for department operations</param>
        public CLDepartmentController(BLDepartment bLDepartment)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objBLDepartment = bLDepartment;
        }

        /// <summary>
        /// Retrieves all departments from the system
        /// </summary>
        /// <returns>IActionResult with list of departments or error response</returns>
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            List<DPT01> lstDepartments = objBLDepartment.GetAllDepartments();
            if (lstDepartments.Count > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Departments Fetched Successfully";
                objResponse.Data = objBLConverter.ToDataTable(lstDepartments);
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves a specific department by its ID
        /// </summary>
        /// <param name="ID">The ID of the department to retrieve</param>
        /// <returns>IActionResult with department data or error response</returns>
        [HttpGet("ID")]
        public IActionResult GetDepartmentByID(int ID)
        {
            DPT01 objDPT01 = objBLDepartment.GetDepartmentByID(ID);
            if (objDPT01 != null)
            {
                objResponse.IsError = false;
                objResponse.Message = "Department Fetched Successfully";
                objResponse.Data = objBLConverter.ObjectToDataTable(objDPT01);
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes a department from the system (Admin only)
        /// </summary>
        /// <param name="ID">The ID of the department to delete</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpDelete("ID")]
        [Authorize(Policy = "Admin")] // Restricts access to Admin role
        public IActionResult DeleteDepartment(int ID)
        {
            objResponse = objBLDepartment.Delete(ID);
            return Ok(objResponse);
        }

        /// <summary>
        /// Adds a new department to the system (Admin only)
        /// </summary>
        /// <param name="objDTODPT01">DTO containing department data to add</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPost]
        [Authorize(Policy = "Admin")] // Restricts access to Admin role
        public IActionResult AddDepartment(DTODPT01 objDTODPT01)
        {
            // Set operation type to Add and prepare data
            objBLDepartment.EnmEntryType = EnmEntryType.A;
            objBLDepartment.PreSave(objDTODPT01);

            // Validate and save if valid
            objResponse = objBLDepartment.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLDepartment.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates an existing department in the system (Admin only)
        /// </summary>
        /// <param name="objDTODPT01">DTO containing updated department data</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPut]
        [Authorize(Policy = "Admin")] // Restricts access to Admin role
        public IActionResult EditDepartment(DTODPT01 objDTODPT01)
        {
            // Set operation type to Edit and prepare data
            objBLDepartment.EnmEntryType = EnmEntryType.E;
            objBLDepartment.PreSave(objDTODPT01);

            // Validate and save if valid
            objResponse = objBLDepartment.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLDepartment.Save();
            }
            return Ok(objResponse);
        }
    }
}