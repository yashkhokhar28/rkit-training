using EmployeeTaskManager.BL;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeTaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLTaskController : ControllerBase
    {
        private readonly BLTask objBLTask;
        private Response objResponse;
        private BLConverter objBLConverter;

        public CLTaskController(BLTask bLTask)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objBLTask = bLTask;
        }

        [HttpGet]
        public IActionResult GetAllTasks([FromQuery] TaskLoadOptions taskLoadOptions)
        {
            //List<TSK01> lstTasks = objBLTask.GetAllTask();
            //if (lstTasks.Count > 0)
            //{
            //    objResponse.IsError = false;
            //    objResponse.Message = "Tasks Fetched Successfully";
            //    objResponse.Data = objBLConverter.ToDataTable(lstTasks);
            //}
            //else
            //{
            //    objResponse.IsError = true;
            //    objResponse.Message = "Error Occured";
            //    objResponse.Data = null;
            //}
            //return Ok(objResponse);

            try
            {
                taskLoadOptions = taskLoadOptions ?? new TaskLoadOptions(); // Default if null
                var (tasks, totalCount) = objBLTask.GetTasksWithOptions(taskLoadOptions);
                objResponse.IsError = false;
                objResponse.Message = "Tasks Fetched Successfully";
                return Ok(new { Data = objBLConverter.ToDataTable(tasks), TotalCount = totalCount });
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"Error fetching tasks: {ex.Message}";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

        [HttpGet("ID")]
        [Authorize(Policy = "Admin")]
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
                objResponse.Message = "Error Occured";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

        [HttpDelete("ID")]
        [Authorize(Policy = "Admin")]
        public IActionResult DeleteTask(int ID)
        {
            objResponse = objBLTask.Delete(ID);
            return Ok(objResponse);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult AddTask(DTOTSK01 objDTOTSK01)
        {
            objBLTask.EnmEntryType = EnmEntryType.A;
            objBLTask.PreSave(objDTOTSK01);
            objResponse = objBLTask.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLTask.Save();
            }
            return Ok(objResponse);
        }

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public IActionResult EditTask(DTOTSK01 objDTOTSK01)
        {
            objBLTask.EnmEntryType = EnmEntryType.E;
            objBLTask.PreSave(objDTOTSK01);
            objResponse = objBLTask.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLTask.Save();
            }
            return Ok(objResponse);
        }
    }
}
