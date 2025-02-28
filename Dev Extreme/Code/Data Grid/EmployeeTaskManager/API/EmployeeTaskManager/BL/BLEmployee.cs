using EmployeeTaskManager.Extension;
using System.Data;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient; // Added for MySqlException

namespace EmployeeTaskManager.BL
{
    public class BLEmployee
    {
        private readonly IDbConnectionFactory objIDbConnectionFactory;

        private Response objResponse;

        private EMP01 objEMP01;

        private int id;

        public EnmEntryType EnmEntryType { get; set; }

        public BLEmployee(IDbConnectionFactory dbConnectionFactory)
        {
            objResponse = new Response();
            objIDbConnectionFactory = dbConnectionFactory;
        }

        public List<EMP01> GetAllEmployees()
        {
            List<EMP01> lstEmployees = new List<EMP01>();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                lstEmployees = objIDbConnection.Select<EMP01>();
            }
            return lstEmployees;
        }

        public EMP01 GetEmployeeByID(int ID)
        {
            EMP01 objEMP01 = new EMP01();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objEMP01 = objIDbConnection.SingleById<EMP01>(ID);
            }
            return objEMP01;
        }

        public void PreSave(DTOEMP01 objDTOEMP01)
        {
            objEMP01 = objDTOEMP01.Convert<EMP01>();
            if (EnmEntryType == EnmEntryType.E && objDTOEMP01.P01F01 > 0)
            {
                id = objDTOEMP01.P01F01;
            }
        }

        public Response ValidationSave()
        {
            if (EnmEntryType == EnmEntryType.E)
            {
                if (!(id > 0))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter Correct Id";
                }
                else if (GetEmployeeByID(id) == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Employee Not Found";
                }
            }
            return objResponse;
        }

        public Response Save()
        {
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                if (EnmEntryType == EnmEntryType.A)
                {
                    objIDbConnection.Insert(objEMP01);
                    objResponse.IsError = false;
                    objResponse.Message = "Employee Inserted Successfully";
                }
                if (EnmEntryType == EnmEntryType.E)
                {
                    objIDbConnection.Update(objEMP01);
                    objResponse.IsError = false;
                    objResponse.Message = "Employee Updated Successfully";
                }
            }
            return objResponse;
        }

        public Response Delete(int ID)
        {
            try
            {
                using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
                {
                    // Check if the employee exists
                    var employee = objIDbConnection.SingleById<EMP01>(ID);
                    if (employee == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Employee Not Found";
                        return objResponse;
                    }

                    // Check if the employee is assigned to any tasks
                    var taskCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM TSK01 WHERE K01F04 = @Id", new { Id = ID });
                    if (taskCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete employee because they are assigned to {taskCount} task(s).";
                        return objResponse;
                    }

                    // Check if the employee is a department manager
                    var deptCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM DPT01 WHERE T01F03 = @Id", new { Id = ID });
                    if (deptCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete employee because they are a manager of {deptCount} department(s).";
                        return objResponse;
                    }

                    // If no dependencies, proceed with deletion
                    objIDbConnection.DeleteById<EMP01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "Employee Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete employee due to a database constraint (tasks or departments assigned).";
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while deleting the employee: {ex.Message}";
            }
            return objResponse;
        }
    }
}