using EmployeeTaskManager.Extension;
using System.Data;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;
using ServiceStack;

namespace EmployeeTaskManager.BL
{
    public class BLTask
    {
        private readonly IDbConnectionFactory objIDbConnectionFactory;

        private Response objResponse;

        private TSK01 objTSK01;

        private int id;

        public EnmEntryType EnmEntryType { get; set; }

        public BLTask(IDbConnectionFactory dbConnectionFactory)
        {
            objResponse = new Response();
            objIDbConnectionFactory = dbConnectionFactory;
        }

        public List<TSK01> GetAllTask()
        {
            List<TSK01> lstTasks = new List<TSK01>();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                lstTasks = objIDbConnection.Select<TSK01>();
            }
            return lstTasks;
        }

        public TSK01 GetTaskByID(int ID)
        {
            TSK01 objTSK01 = new TSK01();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objTSK01 = objIDbConnection.SingleById<TSK01>(ID);
            }
            return objTSK01;
        }

        public void PreSave(DTOTSK01 objDTOTSK01)
        { 
            objTSK01 = objDTOTSK01.Convert<TSK01>();

            switch (objDTOTSK01.K01F06)
            {
                case 0:
                    objTSK01.K01F06 = EnmStatus.Pending;
                    break;
                case 1:
                    objTSK01.K01F06 = EnmStatus.InProgress;
                    break;
                case 2:
                    objTSK01.K01F06 = EnmStatus.Completed;
                    break;
                case 3:
                    objTSK01.K01F06 = EnmStatus.Overdue;
                    break;
                default:
                    objTSK01.K01F06 = EnmStatus.Overdue;
                    break;
            }

            // Convert Priority from integer to string
            switch (objDTOTSK01.K01F07)
            {
                case 0:
                    objTSK01.K01F07 = EnmPriority.Low;
                    break;
                case 1:
                    objTSK01.K01F07 = EnmPriority.Medium;
                    break;
                case 2:
                    objTSK01.K01F07 = EnmPriority.High;
                    break;
                default:
                    objTSK01.K01F07 = EnmPriority.Low;
                    break;
            }



            if (EnmEntryType == EnmEntryType.E && objDTOTSK01.K01F01 > 0)
            {
                id = objDTOTSK01.K01F01;
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
                else if (GetTaskByID(id) == null)
                {
                    objResponse.Message = "Task Not Found";
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
                    objIDbConnection.Insert(objTSK01);
                    objResponse.IsError = false;
                    objResponse.Message = "Tasks Inserted Successfully";
                }
                if (EnmEntryType == EnmEntryType.E)
                {
                    objIDbConnection.Update(objTSK01);
                    objResponse.IsError = false;
                    objResponse.Message = "Tasks Updated Successfully";
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
                    // Fetch the task to check if it's assigned
                    var task = objIDbConnection.SingleById<TSK01>(ID);
                    if (task == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Task Not Found";
                        return objResponse;
                    }

                    if (task.K01F04 > 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Cannot delete task because it is assigned to an employee.";
                        return objResponse;
                    }

                    objIDbConnection.DeleteById<TSK01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "Task Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete task due to a database constraint.";
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while deleting the task: {ex.Message}";
            }
            return objResponse;
        }
    }
}