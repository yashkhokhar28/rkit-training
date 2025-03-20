using EmployeeTaskManager.Extension;
using System.Data;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;

namespace EmployeeTaskManager.BL
{
    /// <summary>
    /// Business Logic class for managing department-related operations
    /// </summary>
    public class BLDepartment
    {
        /// <summary>
        /// Database connection factory for creating database connections
        /// </summary>
        private readonly IDbConnectionFactory objIDbConnectionFactory;

        /// <summary>
        /// Response object for returning operation results
        /// </summary>
        private Response objResponse;

        /// <summary>
        /// Department entity object for database operations
        /// </summary>
        private DPT01 objDPT01;

        /// <summary>
        /// Department ID for operations
        /// </summary>
        private int id;

        /// <summary>
        /// Gets or sets the entry type (Add/Edit) for department operations
        /// </summary>
        public EnmEntryType EnmEntryType { get; set; }

        /// <summary>
        /// Constructor for BLDepartment class
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory instance</param>
        public BLDepartment(IDbConnectionFactory dbConnectionFactory)
        {
            objResponse = new Response();
            objIDbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Retrieves all departments from the database
        /// </summary>
        /// <returns>List of all department entities</returns>
        public List<DPT01> GetAllDepartments()
        {
            List<DPT01> lstDepartments = new List<DPT01>();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                lstDepartments = objIDbConnection.Select<DPT01>();
            }
            return lstDepartments;
        }

        /// <summary>
        /// Retrieves a specific department by its ID
        /// </summary>
        /// <param name="ID">The ID of the department to retrieve</param>
        /// <returns>The department entity if found, otherwise a new instance</returns>
        public DPT01 GetDepartmentByID(int ID)
        {
            DPT01 objDPT01 = new DPT01();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objDPT01 = objIDbConnection.SingleById<DPT01>(ID);
            }
            return objDPT01;
        }

        /// <summary>
        /// Prepares department data for saving
        /// </summary>
        /// <param name="objDTODPT01">DTO containing department data</param>
        public void PreSave(DTODPT01 objDTODPT01)
        {
            objDPT01 = objDTODPT01.Convert<DPT01>();
            if (EnmEntryType == EnmEntryType.E && objDTODPT01.T01F01 > 0)
            {
                id = objDTODPT01.T01F01;
            }
        }

        /// <summary>
        /// Validates department data before saving
        /// </summary>
        /// <returns>Response object indicating validation result</returns>
        public Response ValidationSave()
        {
            if (EnmEntryType == EnmEntryType.E)
            {
                if (!(id > 0))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter Correct Id";
                }
                else if (GetDepartmentByID(id) == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Department Not Found";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Saves a department to the database (insert or update based on EnmEntryType)
        /// </summary>
        /// <returns>Response object indicating success or failure</returns>
        public Response Save()
        {
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                if (EnmEntryType == EnmEntryType.A)
                {
                    objIDbConnection.Insert(objDPT01);
                    objResponse.IsError = false;
                    objResponse.Message = "Department Inserted Successfully";
                }
                if (EnmEntryType == EnmEntryType.E)
                {
                    objIDbConnection.Update(objDPT01);
                    objResponse.IsError = false;
                    objResponse.Message = "Department Updated Successfully";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Deletes a department from the database after checking dependencies
        /// </summary>
        /// <param name="ID">The ID of the department to delete</param>
        /// <returns>Response object indicating success or failure with appropriate message</returns>
        public Response Delete(int ID)
        {
            try
            {
                using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
                {
                    // Verify department exists
                    var department = objIDbConnection.SingleById<DPT01>(ID);
                    if (department == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Department Not Found";
                        return objResponse;
                    }

                    // Check for assigned employees
                    var employeeCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM EMP01 WHERE P01F06 = @Id", new { Id = ID });
                    if (employeeCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete department because it has {employeeCount} employee(s) assigned.";
                        return objResponse;
                    }

                    // Check for assigned tasks
                    var taskCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM TSK01 WHERE K01F05 = @Id", new { Id = ID });
                    if (taskCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete department because it has {taskCount} task(s) assigned.";
                        return objResponse;
                    }

                    // Perform deletion if no dependencies exist
                    objIDbConnection.DeleteById<DPT01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "Department Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                // Handle foreign key constraint violations
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete department due to a database constraint (employees or tasks assigned).";
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while deleting the department: {ex.Message}";
            }
            return objResponse;
        }
    }
}