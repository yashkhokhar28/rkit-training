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
    public class CLEmployeeController : ControllerBase
    {
        private readonly BLEmployee objBLEmployee;
        private Response objResponse;
        private BLConverter objBLConverter;

        public CLEmployeeController(BLEmployee blEmployee)
        {
            objBLEmployee = blEmployee;
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            List<EMP01> lstEmployees = objBLEmployee.GetAllEmployees();
            if (lstEmployees.Count > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Employees Fetched Successfully";
                objResponse.Data = objBLConverter.ToDataTable(lstEmployees);
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
            EMP01 objEMP01 = objBLEmployee.GetEmployeeByID(ID);
            if (objEMP01 != null)
            {
                objResponse.IsError = false;
                objResponse.Message = "Employee Fetched Successfully";
                objResponse.Data = objBLConverter.ObjectToDataTable(objEMP01);
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
            objResponse = objBLEmployee.Delete(ID);
            return Ok(objResponse);
        }

        [HttpPost]
        public IActionResult AddTask(DTOEMP01 objDTOEMP01)
        {
            objBLEmployee.EnmEntryType = EnmEntryType.A;
            objBLEmployee.PreSave(objDTOEMP01);
            objResponse = objBLEmployee.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLEmployee.Save();
            }
            return Ok(objResponse);
        }

        [HttpPut]
        public IActionResult EditTask(DTOEMP01 objDTOEMP01)
        {
            objBLEmployee.EnmEntryType = EnmEntryType.E;
            objBLEmployee.PreSave(objDTOEMP01);
            objResponse = objBLEmployee.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLEmployee.Save();
            }
            return Ok(objResponse);
        }
    }
}
