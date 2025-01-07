using EFWebAPIProject.BL;
using EFWebAPIProject.Models;
using EFWebAPIProject.Models.DTO;
using EFWebAPIProject.Models.ENUM;
using EFWebAPIProject.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFWebAPIProject.Controllers
{
    /// <summary>
    /// The CLStudentController handles HTTP requests related to student data management,
    /// such as adding, updating, deleting, and retrieving student records.
    /// </summary>
    [RoutePrefix("api/CLStudent")]
    public class CLStudentController : ApiController
    {
        /// <summary>
        /// The business logic layer (BL) object that interacts with the database.
        /// </summary>
        private BLStudent _objBLStudent;

        /// <summary>
        /// The response object used to return the status of operations.
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// Constructor for the CLStudentController. Initializes the BLStudent object.
        /// </summary>
        public CLStudentController()
        {
            _objBLStudent = new BLStudent();
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>A list of students in the response.</returns>
        [HttpGet]
        [Route("GetAllStudents")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(_objBLStudent.GetAll());
        }

        /// <summary>
        /// Deletes a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to be deleted.</param>
        /// <returns>A response indicating success or failure of the delete operation.</returns>
        [HttpDelete]
        [Route("DeleteStudent")]
        public IHttpActionResult DeleteStudent(int id)
        {
            // Sets the entry type to 'Delete'
            _objBLStudent.Type = EntryType.D;

            // Prepares the student record for deletion
            STU01 objSTU01 = _objBLStudent.PreDelete(id);

            // Validates if the student can be deleted
            Response _objResponse = _objBLStudent.ValidateOnDelete(objSTU01);

            // If validation is successful, perform the delete
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Delete(id);
            }

            // Returns the response from the delete operation
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objDTOSTU01">The student data to be added.</param>
        /// <returns>A response indicating success or failure of the add operation.</returns>
        [HttpPost]
        [Route("AddStudent")]
        public Response AddNewEmployee(DTOSTU01 objDTOSTU01)
        {
            // Sets the entry type to 'Add'
            _objBLStudent.Type = EntryType.A;

            // Prepares the student data for saving
            _objBLStudent.PreSave(objDTOSTU01);

            // Validates the data before saving
            _objResponse = _objBLStudent.Validation();

            // If validation is successful, save the new student
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Save();
            }

            // Returns the response from the save operation
            return _objResponse;
        }

        /// <summary>
        /// Updates the information of an existing student.
        /// </summary>
        /// <param name="objDTOSTU01">The student data to be updated.</param>
        /// <returns>A response indicating success or failure of the update operation.</returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public Response UpdateEmployeeData(DTOSTU01 objDTOSTU01)
        {
            // Sets the entry type to 'Edit'
            _objBLStudent.Type = EntryType.E;

            // Prepares the student data for saving
            _objBLStudent.PreSave(objDTOSTU01);

            // Validates the data before updating
            _objResponse = _objBLStudent.Validation();

            // If validation is successful, save the updated student information
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Save();
            }

            // Returns the response from the save operation
            return _objResponse;
        }
    }
}