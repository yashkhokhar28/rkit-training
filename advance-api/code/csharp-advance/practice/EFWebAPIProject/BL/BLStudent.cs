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
        #region Fields

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of operation (e.g., Add, Edit, Delete).
        /// </summary>
        public EntryType Type { get; set; }

        #endregion

        #region Constructor

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

        #endregion

        #region Get Operations

        /// <summary>
        /// Retrieves all students from the database.
        /// </summary>
        /// <returns>A list of students.</returns>
        public List<STU01> GetAll()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Select<STU01>();
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
                return objIDbConnection.SingleById<STU01>(id);
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

        #endregion

        #region Delete Operations

        /// <summary>
        /// Prepares a student object for deletion by retrieving it from the database.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <returns>The student object if found, otherwise null.</returns>
        public STU01 PreDelete(int id)
        {
            return IsSTU01Exist(id) ? Get(id) : null;
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

        #endregion

        #region Save Operations

        /// <summary>
        /// Prepares the student data before saving it (for both adding and editing).
        /// </summary>
        /// <param name="objDTOSTU01">The student data transfer object to prepare.</param>
        public void PreSave(DTOSTU01 objDTOSTU01)
        {
            // Convert DTO to POCO model
            _objSTU01 = objDTOSTU01.Convert<STU01>();

            if (Type == EntryType.E && objDTOSTU01.U01F01 > 0)
            {
                _id = objDTOSTU01.U01F01;
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
                else if (!IsSTU01Exist(_id))
                {
                    _objResponse.IsError = true;
                    _objResponse.Message = "User Not Found";
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
                    else if (Type == EntryType.E)
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

        #endregion

        #region Extra Methods

        /// <summary>
        /// Retrieves the first student based on ascending order of their ID.
        /// </summary>
        /// <returns>First student from the database</returns>
        public STU01 GetFirstStudent()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Single<STU01>("ORDER BY U01F01 ASC LIMIT 1");
            }
        }

        /// <summary>
        /// Retrieves the last student based on descending order of their ID.
        /// </summary>
        /// <returns>Last student from the database</returns>
        public STU01 GetLastStudent()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Single<STU01>("ORDER BY U01F01 DESC LIMIT 1");
            }
        }

        /// <summary>
        /// Retrieves a list of students with paging (limit and skip).
        /// </summary>
        /// <param name="skip">Number of records to skip</param>
        /// <param name="take">Number of records to retrieve</param>
        /// <returns>List of students based on the paging parameters</returns>
        public List<STU01> GetStudentsWithPaging(int skip, int take)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                var query = objIDbConnection.From<STU01>();
                query.Limit(skip, take); // Apply paging

                return objIDbConnection.Select<STU01>(query); // Return paged students
            }
        }

        /// <summary>
        /// Retrieves the total number of students in the database.
        /// </summary>
        /// <returns>Total count of students</returns>
        public long GetTotalStudentCount()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Count<STU01>(); // Count total students
            }
        }

        /// <summary>
        /// Inserts a list of students into the database.
        /// </summary>
        /// <param name="students">List of students to insert</param>
        public void InsertMultipleStudents(List<STU01> students)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                objIDbConnection.InsertAll(students); // Insert all students into the database
            }
        }

        /// <summary>
        /// Updates a list of students in the database.
        /// </summary>
        /// <param name="students">List of students to update</param>
        public void UpdateMultipleStudents(List<STU01> students)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                objIDbConnection.UpdateAll(students); // Update all students in the database
            }
        }

        /// <summary>
        /// Deletes students who are above a specified age.
        /// </summary>
        /// <param name="age">The age threshold</param>
        public void DeleteStudentsAboveAge(int age)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                objIDbConnection.Delete<STU01>(x => x.U01F03 > age); // Delete students whose age is greater than the provided age
            }
        }

        /// <summary>
        /// Retrieves a list of students whose names start with a specified prefix.
        /// </summary>
        /// <param name="prefix">The name prefix</param>
        /// <returns>List of students whose names start with the given prefix</returns>
        public List<STU01> GetStudentsByNamePrefix(string prefix)
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Select<STU01>(x => x.U01F02.StartsWith(prefix)); // Select students with name prefix
            }
        }

        /// <summary>
        /// Retrieves the average age of all students in the database.
        /// </summary>
        /// <returns>Average age of students</returns>
        public double GetAverageStudentAge()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.Scalar<double>("SELECT AVG(U01F03) FROM stu01"); // Get average age of students
            }
        }

        /// <summary>
        /// Retrieves a set of distinct student names from the database.
        /// </summary>
        /// <returns>HashSet of distinct student names</returns>
        public HashSet<string> GetDistinctStudentNames()
        {
            using (IDbConnection objIDbConnection = _dbFactory.OpenDbConnection())
            {
                return objIDbConnection.ColumnDistinct<string>("SELECT DISTINCT U01F02 FROM stu01"); // Get distinct student names
            }
        }

        #endregion
    }
}