using EFWebAPIProject.Models;
using EFWebAPIProject.Models.DTO;
using EFWebAPIProject.Models.ENUM;
using EFWebAPIProject.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using EFWebAPIProject.Extension;

namespace EFWebAPIProject.BL
{
    /// <summary>
    /// Business logic layer class for managing student data.
    /// Implements the IDataHandlerService interface for handling operations related to student entities (DTOSTU01).
    /// </summary>
    public class BLStudent : IDataHandlerService<DTOSTU01>
    {
        /// <summary>
        /// Private field for storing a student object (POCO model).
        /// </summary>
        private STU01 _objSTU01;

        /// <summary>
        /// Private field for storing the student ID.
        /// </summary>
        private int _id;

        /// <summary>
        /// Database connection factory for opening database connections.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Response object to store result and message of operations.
        /// </summary>
        private Response _objResponse;

        /// <summary>
        /// Gets or sets the type of operation (e.g., Add, Edit, Delete).
        /// </summary>
        public EntryType Type { get; set; }

        /// <summary>
        /// Constructor for initializing the BLStudent class.
        /// </summary>
        /// <exception cref="Exception">Thrown when the IDbConnectionFactory is not found.</exception>
        public BLStudent()
        {
            _objResponse = new Response();

            // Initialize the database connection factory
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // Throw exception if the factory is not found
            if (_dbFactory == null)
            {
                throw new Exception("IDbConnectionFactory not found");
            }
        }

        /// <summary>
        /// Retrieves all students from the database.
        /// </summary>
        /// <returns>A list of students.</returns>
        public List<STU01> GetAll()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                var students = objIDbConnection.Select<STU01>();
                return students;
            }
        }

        /// <summary>
        /// Retrieves a student by their ID.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <returns>The student object.</returns>
        public STU01 Get(int id)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                var student = objIDbConnection.SingleById<STU01>(id);
                return student;
            }
        }

        /// <summary>
        /// Checks if a student exists by their ID.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <returns>True if the student exists, otherwise false.</returns>
        public bool IsSTU01Exist(int id)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Exists<STU01>(id);
            }
        }

        /// <summary>
        /// Prepares a student object for deletion by retrieving it from the database.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <returns>The student object if found, otherwise null.</returns>
        public STU01 PreDelete(int id)
        {
            bool isStudentExist = IsSTU01Exist(id);
            if (isStudentExist)
            {
                return Get(id); // Get the student to be deleted
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Validates the student object before deletion.
        /// </summary>
        /// <param name="objSTU01">The student object to validate.</param>
        /// <returns>A response object indicating if the deletion is valid.</returns>
        public Response ValidateOnDelete(STU01 objSTU01)
        {
            if (objSTU01 == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Student Not Found";
            }
            else
            {
                _objResponse.Message = "Student Found";
                _objResponse.IsError = false;
            }
            return _objResponse;
        }

        /// <summary>
        /// Deletes a student from the database by their ID.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <returns>A response object indicating the result of the deletion operation.</returns>
        public Response Delete(int id)
        {
            try
            {
                using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
                {
                    objIDbConnection.Delete<STU01>(x => x.U01F01 == id);
                }
                _objResponse.Message = "Data Deleted Successfully";
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }

        /// <summary>
        /// Prepares the student data before saving it (for both adding and editing).
        /// </summary>
        /// <param name="objDTOSTU01">The student data transfer object to prepare.</param>
        public void PreSave(DTOSTU01 objDTOSTU01)
        {
            // Convert DTO to POCO model
            _objSTU01 = objDTOSTU01.Convert<STU01>();

            if (Type == EntryType.E)
            {
                // Set the student ID for edit operations
                if (objDTOSTU01.U01F01 > 0)
                {
                    _id = objDTOSTU01.U01F01;
                }
            }
        }

        /// <summary>
        /// Validates the data before saving it (for both adding and editing).
        /// </summary>
        /// <returns>A response object indicating whether the validation succeeded.</returns>
        public Response Validation()
        {
            if (Type == EntryType.E)
            {
                if (!(_id > 0))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "Enter Correct Id";
                }
                else
                {
                    bool isStudentExist = IsSTU01Exist(_id);
                    if (!isStudentExist)
                    {
                        _objResponse.IsError = true;
                        _objResponse.Message = "User Not Found";
                    }
                }
            }
            return _objResponse;
        }

        /// <summary>
        /// Saves the student data (either adding or editing) to the database.
        /// </summary>
        /// <returns>A response object indicating the result of the save operation.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Type == EntryType.A)
                    {
                        db.Insert(_objSTU01); // Insert new student
                        _objResponse.Message = "Data Added";
                    }
                    if (Type == EntryType.E)
                    {
                        db.Update(_objSTU01); // Update existing student
                        _objResponse.Message = "Data Updated";
                    }
                }
            }
            catch (Exception ex)
            {
                _objResponse.IsError = true;
                _objResponse.Message = ex.Message;
            }
            return _objResponse;
        }
    }
}