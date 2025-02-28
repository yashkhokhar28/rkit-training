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
    public class BLDepartment
    {
        private readonly IDbConnectionFactory objIDbConnectionFactory;

        private Response objResponse;

        private DPT01 objDPT01;

        private int id;

        public EnmEntryType EnmEntryType { get; set; }

        public BLDepartment(IDbConnectionFactory dbConnectionFactory)
        {
            objResponse = new Response();
            objIDbConnectionFactory = dbConnectionFactory;
        }

        public List<DPT01> GetAllDepartments()
        {
            List<DPT01> lstDepartments = new List<DPT01>();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                lstDepartments = objIDbConnection.Select<DPT01>();
            }
            return lstDepartments;
        }

        public DPT01 GetDepartmentByID(int ID)
        {
            DPT01 objDPT01 = new DPT01();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objDPT01 = objIDbConnection.SingleById<DPT01>(ID);
            }
            return objDPT01;
        }

        public void PreSave(DTODPT01 objDTODPT01)
        {
            objDPT01 = objDTODPT01.Convert<DPT01>();
            if (EnmEntryType == EnmEntryType.E && objDTODPT01.T01F01 > 0)
            {
                id = objDTODPT01.T01F01;
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
                else if (GetDepartmentByID(id) == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Department Not Found";
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

        public Response Delete(int ID)
        {
            try
            {
                using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
                {
                    // Check if the department exists
                    var department = objIDbConnection.SingleById<DPT01>(ID);
                    if (department == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Department Not Found";
                        return objResponse;
                    }

                    // Check if the department has any employees
                    var employeeCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM EMP01 WHERE P01F06 = @Id", new { Id = ID });
                    if (employeeCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete department because it has {employeeCount} employee(s) assigned.";
                        return objResponse;
                    }

                    // Check if the department has any tasks
                    var taskCount = objIDbConnection.Scalar<long>("SELECT COUNT(*) FROM TSK01 WHERE K01F05 = @Id", new { Id = ID });
                    if (taskCount > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = $"Cannot delete department because it has {taskCount} task(s) assigned.";
                        return objResponse;
                    }

                    // If no dependencies, proceed with deletion
                    objIDbConnection.DeleteById<DPT01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "Department Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete department due to a database constraint (employees or tasks assigned).";
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while deleting the department: {ex.Message}";
            }
            return objResponse;
        }
    }
}