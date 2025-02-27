using EmployeeTaskManager.Extension;
using System.Data;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;

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
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objIDbConnection.DeleteById<TSK01>(ID);
            }
            objResponse.IsError = false;
            objResponse.Message = "Tasks Deleted Successfully";
            return objResponse;
        }
    }
}
