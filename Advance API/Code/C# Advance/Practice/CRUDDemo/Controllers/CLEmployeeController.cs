using CRUDDemo.BL;
using CRUDDemo.Models;
using CRUDDemo.Models.DTO;
using CRUDDemo.Models.POCO;
using System.Collections.Generic;
using System.Web.Http;

namespace CRUDDemo.Controllers
{
    /// <summary>
    /// API Controller for handling employee-related operations.
    /// </summary>
    [RoutePrefix("api/CLEmployee")]
    public class CLEmployeeController : ApiController
    {
        /// <summary>
        /// Business logic handler for employee operations.
        /// </summary>
        public BLEmployee objBLEmployee;

        /// <summary>
        /// Response object to standardize API responses.
        /// </summary>
        public Response objResponse;

        /// <summary>
        /// Helper for data conversion tasks.
        /// </summary>
        public BLConverter objBLConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLEmployeeController"/> class.
        /// </summary>
        public CLEmployeeController()
        {
            objBLEmployee = new BLEmployee();
            objBLConverter = new BLConverter();
            objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>A standardized <see cref="Response"/> object containing the employee data.</returns>
        [HttpGet]
        [Route("GetAllEmployee")]
        public Response GetAllEmployee()
        {
            int result = objBLEmployee.GetAll().Count;
            if (result > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Ok";
                List<EMP01> lstEmployees = objBLEmployee.GetAll();
                objResponse.Data = objBLConverter.ToDataTable(lstEmployees);
                return objResponse;
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return objResponse;
            }
        }

        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A standardized <see cref="Response"/> object indicating the result of the deletion.</returns>
        [HttpDelete]
        [Route("DeleteEmployee/{id:int}")]
        public Response DeleteEmployee(int id)
        {
            int result = objBLEmployee.Delete(id);
            if (result != 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Data Deleted Successfully";
                return objResponse;
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return objResponse;
            }
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="objDTOEMP01">The data transfer object (DTO) containing employee details.</param>
        /// <returns>A standardized <see cref="Response"/> object indicating the result of the addition.</returns>
        [HttpPost]
        [Route("AddEmployee")]
        public Response AddEmployee([FromBody] DTOEMP01 objDTOEMP01)
        {
            objBLEmployee.Type = Models.ENUM.EnmEntryType.A;
            objBLEmployee.PreSave(objDTOEMP01);
            Response objResponse = objBLEmployee.Validation();
            if (objResponse.IsError)
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return objResponse;
            }
            else
            {
                objBLEmployee.Save();
                objResponse.IsError = false;
                objResponse.Message = "Data Added Successfully";
                return objResponse;
            }
        }

        /// <summary>
        /// Updates an existing employee in the database.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="objDTOEMP01">The data transfer object (DTO) containing updated employee details.</param>
        /// <returns>A standardized <see cref="Response"/> object indicating the result of the update.</returns>
        [HttpPut]
        [Route("UpdateEmployee")]
        public Response UpdateEmployee(int id, [FromBody] DTOEMP01 objDTOEMP01)
        {
            objBLEmployee.Type = Models.ENUM.EnmEntryType.E;
            objBLEmployee.PreSave(objDTOEMP01);
            Response objResponse = objBLEmployee.Validation();
            if (objResponse.IsError)
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return objResponse;
            }
            else
            {
                objBLEmployee.Save();
                objResponse.IsError = false;
                objResponse.Message = "Data Updated Successfully";
                return objResponse;
            }
        }
    }
}