using EFWebAPIProject.BL;
using EFWebAPIProject.Models;
using EFWebAPIProject.Models.DTO;
using EFWebAPIProject.Models.ENUM;
using EFWebAPIProject.Models.POCO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EFWebAPIProject.Controllers
{
    /// <summary>
    /// Handles HTTP requests related to student data management, 
    /// including CRUD operations, advanced queries, and batch updates.
    /// </summary>
    [RoutePrefix("api/CLStudent")]
    public class CLStudentController : ApiController
    {
        #region Private Fields

        private BLStudent _objBLStudent;
        private Response _objResponse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the BLStudent object.
        /// </summary>
        public CLStudentController()
        {
            _objBLStudent = new BLStudent();
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(_objBLStudent.GetAll());
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Response AddStudent(DTOSTU01 objDTOSTU01)
        {
            _objBLStudent.Type = EntryType.A;
            _objBLStudent.PreSave(objDTOSTU01);
            _objResponse = _objBLStudent.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Save();
            }

            return _objResponse;
        }

        /// <summary>
        /// Updates the information of an existing student.
        /// </summary>
        [HttpPut]
        [Route("Update")]
        public Response UpdateStudent(DTOSTU01 objDTOSTU01)
        {
            _objBLStudent.Type = EntryType.E;
            _objBLStudent.PreSave(objDTOSTU01);
            _objResponse = _objBLStudent.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Save();
            }

            return _objResponse;
        }

        /// <summary>
        /// Deletes a student by their ID.
        /// </summary>
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            _objBLStudent.Type = EntryType.D;
            var objSTU01 = _objBLStudent.PreDelete(id);
            _objResponse = _objBLStudent.ValidateOnDelete(objSTU01);

            if (!_objResponse.IsError)
            {
                _objResponse = _objBLStudent.Delete(id);
            }

            return Ok(_objResponse);
        }

        #endregion

        #region Advanced Queries

        /// <summary>
        /// Retrieves the first student record.
        /// </summary>
        [HttpGet]
        [Route("GetFirst")]
        public IHttpActionResult GetFirstStudent()
        {
            return Ok(_objBLStudent.GetFirstStudent());
        }

        /// <summary>
        /// Retrieves the last student record.
        /// </summary>
        [HttpGet]
        [Route("GetLast")]
        public IHttpActionResult GetLastStudent()
        {
            return Ok(_objBLStudent.GetLastStudent());
        }

        /// <summary>
        /// Retrieves a paginated list of students.
        /// </summary>
        [HttpGet]
        [Route("GetPaged")]
        public IHttpActionResult GetStudentsWithPaging(int skip, int take)
        {
            return Ok(_objBLStudent.GetStudentsWithPaging(skip, take));
        }

        /// <summary>
        /// Retrieves the total count of students.
        /// </summary>
        [HttpGet]
        [Route("GetTotalCount")]
        public IHttpActionResult GetTotalStudentCount()
        {
            return Ok(_objBLStudent.GetTotalStudentCount());
        }

        /// <summary>
        /// Retrieves students whose names start with a specific prefix.
        /// </summary>
        [HttpGet]
        [Route("SearchByNamePrefix")]
        public IHttpActionResult GetStudentsByNamePrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return BadRequest("Prefix is required.");
            }

            return Ok(_objBLStudent.GetStudentsByNamePrefix(prefix));
        }

        /// <summary>
        /// Retrieves the average age of students.
        /// </summary>
        [HttpGet]
        [Route("GetAverageAge")]
        public IHttpActionResult GetAverageStudentAge()
        {
            return Ok(_objBLStudent.GetAverageStudentAge());
        }

        /// <summary>
        /// Retrieves distinct student names.
        /// </summary>
        [HttpGet]
        [Route("GetDistinctNames")]
        public IHttpActionResult GetDistinctStudentNames()
        {
            return Ok(_objBLStudent.GetDistinctStudentNames());
        }

        #endregion

        #region Batch Operations

        /// <summary>
        /// Inserts multiple students.
        /// </summary>
        [HttpPost]
        [Route("InsertBatch")]
        public IHttpActionResult InsertMultipleStudents([FromBody] List<STU01> students)
        {
            if (students == null || students.Count == 0)
            {
                return BadRequest("No students provided for insertion.");
            }

            try
            {
                _objBLStudent.InsertMultipleStudents(students);
                return Ok("Students inserted successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Updates multiple students.
        /// </summary>
        [HttpPut]
        [Route("UpdateBatch")]
        public IHttpActionResult UpdateMultipleStudents([FromBody] List<STU01> students)
        {
            if (students == null || students.Count == 0)
            {
                return BadRequest("No students provided for update.");
            }

            try
            {
                _objBLStudent.UpdateMultipleStudents(students);
                return Ok("Students updated successfully.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes students above a certain age.
        /// </summary>
        [HttpDelete]
        [Route("DeleteAboveAge/{age:int}")]
        public IHttpActionResult DeleteStudentsAboveAge(int age)
        {
            if (age <= 0)
            {
                return BadRequest("Age must be greater than zero.");
            }

            _objBLStudent.DeleteStudentsAboveAge(age);
            return Ok("Students above the specified age deleted successfully.");
        }

        #endregion
    }
}