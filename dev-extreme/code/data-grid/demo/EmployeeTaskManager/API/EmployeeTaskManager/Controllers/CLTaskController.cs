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
    /// API Controller for managing task-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLTaskController : ControllerBase
    {
        /// <summary>
        /// Business logic layer instance for task operations
        /// </summary>
        private readonly BLTask objBLTask;

        /// <summary>
        /// Response object for returning operation results
        /// </summary>
        private Response objResponse;

        /// <summary>
        /// Converter instance for transforming objects to DataTables
        /// </summary>
        private BLConverter objBLConverter;

        /// <summary>
        /// Constructor for CLTaskController
        /// </summary>
        /// <param name="bLTask">Business logic layer instance for task operations</param>
        public CLTaskController(BLTask bLTask)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objBLTask = bLTask;
        }

        /// <summary>
        /// Retrieves all tasks with filtering, sorting, and pagination options
        /// </summary>
        /// <param name="taskLoadOptions">Query parameters for filtering, sorting, and paging</param>
        /// <returns>IActionResult with tasks data and total count or error response</returns>
        [HttpGet]
        public IActionResult GetAllTasks([FromQuery] TaskLoadOptions taskLoadOptions)
        {
            try
            {
                // Fetch tasks with options and total count
                var (tasks, totalCount) = objBLTask.GetTasksWithOptions(taskLoadOptions);
                objResponse.IsError = false;
                objResponse.Message = "Tasks Fetched Successfully";
                // Return data with total count for client-side pagination
                return Ok(new { Data = objBLConverter.ToDataTable(tasks), TotalCount = totalCount });
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"Error fetching tasks: {ex.Message}";
                objResponse.Data = null;
                return Ok(objResponse);
            }
        }

        /// <summary>
        /// Retrieves a specific task by its ID
        /// </summary>
        /// <param name="ID">The ID of the task to retrieve</param>
        /// <returns>IActionResult with task data or error response</returns>
        [HttpGet("ID")]
        public IActionResult GetTaskByID(int ID)
        {
            TSK01 objTSK01 = objBLTask.GetTaskByID(ID);
            if (objTSK01 != null)
            {
                objResponse.IsError = false;
                objResponse.Message = "Tasks Fetched Successfully";
                objResponse.Data = objBLConverter.ObjectToDataTable(objTSK01);
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
        /// Deletes a task from the system (Manager or Admin only)
        /// </summary>
        /// <param name="ID">The ID of the task to delete</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpDelete("ID")]
        [Authorize(Policy = "ManagerOrAdmin")] // Restricts access to Manager or Admin roles
        public IActionResult DeleteTask(int ID)
        {
            objResponse = objBLTask.Delete(ID);
            return Ok(objResponse);
        }

        /// <summary>
        /// Adds a new task to the system (Manager or Admin only)
        /// </summary>
        /// <param name="objDTOTSK01">DTO containing task data to add</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPost]
        [Authorize(Policy = "ManagerOrAdmin")] // Restricts access to Manager or Admin roles
        public IActionResult AddTask(DTOTSK01 objDTOTSK01)
        {
            // Set operation type to Add and prepare data
            objBLTask.EnmEntryType = EnmEntryType.A;
            objBLTask.PreSave(objDTOTSK01);

            // Validate and save if valid
            objResponse = objBLTask.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLTask.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates an existing task in the system (Manager or Admin only)
        /// </summary>
        /// <param name="objDTOTSK01">DTO containing updated task data</param>
        /// <returns>IActionResult indicating success or failure</returns>
        [HttpPut]
        [Authorize(Policy = "ManagerOrAdmin")] // Restricts access to Manager or Admin roles
        public IActionResult EditTask(DTOTSK01 objDTOTSK01)
        {
            // Set operation type to Edit and prepare data
            objBLTask.EnmEntryType = EnmEntryType.E;
            objBLTask.PreSave(objDTOTSK01);

            // Validate and save if valid
            objResponse = objBLTask.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLTask.Save();
            }
            return Ok(objResponse);
        }
    }
}