using EmployeeTaskManager.BL;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllTasks()
        {
            List<TSK01> lstTasks = objBLTask.GetAllTask();
            if (lstTasks.Count > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Tasks Fetched Successfully";
                objResponse.Data = objBLConverter.ToDataTable(lstTasks);
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occured";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

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
                objResponse.Message = "Error Occured";
                objResponse.Data = null;
            }
            return Ok(objResponse);
        }

        [HttpDelete("ID")]
        public IActionResult DeleteTask(int ID)
        {
            objResponse = objBLTask.Delete(ID);
            return Ok(objResponse);
        }

        [HttpPost]
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
