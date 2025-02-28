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
    public class CLDepartmentController : ControllerBase
    {
        private readonly BLDepartment objBLDepartment;
        private Response objResponse;
        private BLConverter objBLConverter;

        public CLDepartmentController(BLDepartment bLDepartment)
        {
            objResponse = new Response();
            objBLConverter = new BLConverter();
            objBLDepartment = bLDepartment;
        }

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

        [HttpDelete("ID")]
        public IActionResult DeleteDepartment(int ID)
        {
            objResponse = objBLDepartment.Delete(ID);
            return Ok(objResponse);
        }

        [HttpPost]
        public IActionResult AddDepartment(DTODPT01 objDTODPT01)
        {
            objBLDepartment.EnmEntryType = EnmEntryType.A;
            objBLDepartment.PreSave(objDTODPT01);
            objResponse = objBLDepartment.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLDepartment.Save();
            }
            return Ok(objResponse);
        }

        [HttpPut]
        public IActionResult EditDepartment(DTODPT01 objDTODPT01)
        {
            objBLDepartment.EnmEntryType = EnmEntryType.E;
            objBLDepartment.PreSave(objDTODPT01);
            objResponse = objBLDepartment.ValidationSave();
            if (!objResponse.IsError)
            {
                objResponse = objBLDepartment.Save();
            }
            return Ok(objResponse);
        }
    }
}