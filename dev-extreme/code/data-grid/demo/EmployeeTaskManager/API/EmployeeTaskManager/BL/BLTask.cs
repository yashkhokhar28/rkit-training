using EmployeeTaskManager.Extension;
using System.Data;
using EmployeeTaskManager.Models;
using EmployeeTaskManager.Models.DTO;
using EmployeeTaskManager.Models.ENUM;
using EmployeeTaskManager.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace EmployeeTaskManager.BL
{
    /// <summary>
    /// Business Logic class for managing task-related operations
    /// </summary>
    public class BLTask
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
        /// Task entity object for database operations
        /// </summary>
        private TSK01 objTSK01;

        /// <summary>
        /// Task ID for operations
        /// </summary>
        private int id;

        /// <summary>
        /// Gets or sets the entry type (Add/Edit) for task operations
        /// </summary>
        public EnmEntryType EnmEntryType { get; set; }

        /// <summary>
        /// Constructor for BLTask class
        /// </summary>
        /// <param name="dbConnectionFactory">Database connection factory instance</param>
        public BLTask(IDbConnectionFactory dbConnectionFactory)
        {
            objResponse = new Response();
            objIDbConnectionFactory = dbConnectionFactory;
        }

        /// <summary>
        /// Retrieves all tasks from the database
        /// </summary>
        /// <returns>List of all task entities</returns>
        public List<TSK01> GetAllTask()
        {
            List<TSK01> lstTasks = new List<TSK01>();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                lstTasks = objIDbConnection.Select<TSK01>();
            }
            return lstTasks;
        }

        /// <summary>
        /// Retrieves tasks with filtering, sorting, and pagination options
        /// </summary>
        /// <param name="taskLoadOptions">Options for filtering, sorting, and paging tasks</param>
        /// <returns>Tuple containing the list of tasks and total count of matching records</returns>
        public (List<TSK01> Tasks, long TotalCount) GetTasksWithOptions(TaskLoadOptions taskLoadOptions)
        {
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                var query = objIDbConnection.From<TSK01>();

                // Apply filtering based on provided filter criteria
                if (!string.IsNullOrEmpty(taskLoadOptions.Filter))
                {
                    var filter = JsonConvert.DeserializeObject<List<object>>(taskLoadOptions.Filter);
                    if (filter.Count >= 3)
                    {
                        string field = filter[0].ToString().ToLowerInvariant();
                        string operation = filter[1].ToString().ToLowerInvariant();
                        string value = filter[2].ToString();

                        switch (operation)
                        {
                            case "contains":
                                query.Where($"{field} LIKE @0", $"%{value}%");
                                break;
                            case "=":
                                query.Where(field, value);
                                break;
                            case "<>":
                                query.Where($"{field} != @0", value);
                                break;
                            case ">":
                                query.Where($"{field} > @0", value);
                                break;
                            case "<":
                                query.Where($"{field} < @0", value);
                                break;
                        }
                    }
                }

                // Calculate total count before applying pagination
                long totalCount = objIDbConnection.Count(query);

                // Apply sorting based on provided sort criteria
                if (!string.IsNullOrEmpty(taskLoadOptions.Sort))
                {
                    var sort = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(taskLoadOptions.Sort);
                    foreach (var s in sort)
                    {
                        string field = s["selector"].ToString().ToLowerInvariant();
                        bool desc = (bool)s["desc"];
                        query = desc ? query.OrderByDescending(field) : query.OrderBy(field);
                    }
                }

                // Apply pagination for infinite scrolling
                if (taskLoadOptions.Skip.HasValue && taskLoadOptions.Take.HasValue)
                {
                    query.Skip(taskLoadOptions.Skip.Value).Take(taskLoadOptions.Take.Value);
                }

                // Execute the constructed query
                var tasks = objIDbConnection.Select(query);

                return (tasks, totalCount);
            }
        }

        /// <summary>
        /// Retrieves a specific task by its ID
        /// </summary>
        /// <param name="ID">The ID of the task to retrieve</param>
        /// <returns>The task entity if found, otherwise a new instance</returns>
        public TSK01 GetTaskByID(int ID)
        {
            TSK01 objTSK01 = new TSK01();
            using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
            {
                objTSK01 = objIDbConnection.SingleById<TSK01>(ID);
            }
            return objTSK01;
        }

        /// <summary>
        /// Prepares task data for saving
        /// </summary>
        /// <param name="objDTOTSK01">DTO containing task data</param>
        public void PreSave(DTOTSK01 objDTOTSK01)
        {
            objTSK01 = objDTOTSK01.Convert<TSK01>();

            // Note: Commented out status and priority conversion logic
            // Could be re-implemented based on requirements
            if (EnmEntryType == EnmEntryType.E && objDTOTSK01.K01F01 > 0)
            {
                id = objDTOTSK01.K01F01;
            }
        }

        /// <summary>
        /// Validates task data before saving
        /// </summary>
        /// <returns>Response object indicating validation result</returns>
        public Response ValidationSave()
        {
            // Validate assigned employee and department references
            using (IDbConnection db = objIDbConnectionFactory.OpenDbConnection())
            {
                if (objTSK01.K01F04 > 0)
                {
                    var employee = db.SingleById<USR01>(objTSK01.K01F04);
                    if (employee == null || employee.R01F01 == 0)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Invalid Employee ID: Employee does not exist.";
                        return objResponse;
                    }
                    // Check if employee belongs to the specified department
                    if (objTSK01.K01F05 > 0 && employee.R01F08 != objTSK01.K01F05)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Employee does not belong to the selected department.";
                        return objResponse;
                    }
                }
                if (objTSK01.K01F05 > 0 && db.SingleById<DPT01>(objTSK01.K01F05) == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Invalid Department ID: Department does not exist.";
                    return objResponse;
                }
            }

            // Validate ID for edit operations
            if (EnmEntryType == EnmEntryType.E)
            {
                if (!(id > 0))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Enter Correct Id";
                    return objResponse;
                }
                else if (GetTaskByID(id) == null)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Task Not Found";
                    return objResponse;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Saves a task to the database (insert or update based on EnmEntryType)
        /// </summary>
        /// <returns>Response object indicating success or failure</returns>
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

        /// <summary>
        /// Deletes a task from the database after checking if it's unassigned
        /// </summary>
        /// <param name="ID">The ID of the task to delete</param>
        /// <returns>Response object indicating success or failure</returns>
        public Response Delete(int ID)
        {
            try
            {
                using (IDbConnection objIDbConnection = objIDbConnectionFactory.OpenDbConnection())
                {
                    // Verify task exists and check assignment status
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

                    // Perform deletion
                    objIDbConnection.DeleteById<TSK01>(ID);
                    objResponse.IsError = false;
                    objResponse.Message = "Task Deleted Successfully";
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                // Handle foreign key constraint violations
                objResponse.IsError = true;
                objResponse.Message = "Cannot delete task due to a database constraint.";
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                objResponse.IsError = true;
                objResponse.Message = $"An error occurred while deleting the task: {ex.Message}";
            }
            return objResponse;
        }
    }
}