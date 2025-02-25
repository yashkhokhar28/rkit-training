using ContactBookAPI.BL;
using ContactBookAPI.Filters;
using ContactBookAPI.Models;
using ContactBookAPI.Models.DTO;
using ContactBookAPI.Models.ENUM;
using ContactBookAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ContactBookAPI.Controllers
{
    /// <summary>
    /// Controller for managing contacts in the Contact Book API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLContactsController : ControllerBase
    {
        /// <summary>
        /// Business logic object for contact book operations.
        /// </summary>
        private readonly BLContactBook objBLContactBook;

        /// <summary>
        /// Response object to hold the result data.
        /// </summary>
        private readonly Response objResponse;

        /// <summary>
        /// Converter object for data transformation.
        /// </summary>
        private readonly BLConverter objBLConverter;

        /// <summary>
        /// Logger for logging information, warnings, and errors.
        /// </summary>
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="CLContactsController"/> class.
        /// </summary>
        public CLContactsController()
        {
            objBLContactBook = new BLContactBook();
            objBLConverter = new BLConverter();
            objResponse = new Response();
        }

        /// <summary>
        /// Retrieves all contacts from the database.
        /// </summary>
        /// <returns>A response object containing the list of contacts.</returns>
        [HttpGet]
        [Route("GetAllContacts")]
        public IActionResult Get()
        {
            _logger.Info("GetAllContacts method called.");

            try
            {
                List<CNT01> lstContacts = objBLContactBook.GetAllContacts();
                _logger.Debug($"Retrieved {lstContacts.Count} contacts from the database.");

                if (lstContacts.Count > 0)
                {
                    objResponse.IsError = false;
                    objResponse.Message = "Ok";
                    objResponse.Data = objBLConverter.ToDataTable(lstContacts);
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Not Found";
                    objResponse.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in GetAllContacts method.");
                objResponse.IsError = true;
                objResponse.Message = "An error occurred while fetching contacts.";
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves a contact by ID from the database.
        /// </summary>
        /// <param name="ID">The contact ID.</param>
        /// <returns>A response object containing the contact details.</returns>
        [HttpGet]
        [Route("GetContactsByID/{ID}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public IActionResult GetByID(int ID)
        {
            _logger.Info($"GetContactsByID method called with ID: {ID}");

            try
            {
                CNT01 objCNT01 = objBLContactBook.GetUserByID(ID);

                if (objCNT01 != null)
                {
                    objResponse.IsError = false;
                    objResponse.Message = "Ok";
                    objResponse.Data = objBLConverter.ObjectToDataTable(objCNT01);
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Not Found";
                    objResponse.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error retrieving contact with ID: {ID}");
                objResponse.IsError = true;
                objResponse.Message = "An error occurred while fetching the contact.";
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes a contact by ID from the database.
        /// </summary>
        /// <param name="ID">The contact ID.</param>
        /// <returns>A response object with the result of the delete operation.</returns>
        [HttpDelete]
        [Route("DeleteContactsByID/{ID}")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public IActionResult Delete(int ID)
        {
            _logger.Info($"DeleteContactsByID method called with ID: {ID}");

            try
            {
                int result = objBLContactBook.Delete(ID);

                if (result > 0)
                {
                    objResponse.IsError = false;
                    objResponse.Message = "Data Deleted Successfully";
                    _logger.Info($"Contact with ID {ID} deleted successfully.");
                }
                else
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Error Occurred in Delete";
                    _logger.Warn($"Failed to delete contact with ID: {ID}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting contact with ID: {ID}");
                objResponse.IsError = true;
                objResponse.Message = "An error occurred while deleting the contact.";
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Updates the contact details in the database.
        /// </summary>
        /// <param name="objDTOCNT01">The contact DTO object.</param>
        /// <returns>A response object with the result of the update operation.</returns>
        [HttpPut]
        [Route("UpdateContacts")]
        public IActionResult UpdateContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            _logger.Info("UpdateContacts method called.");
            _logger.Debug($"Updating contact: {objDTOCNT01}");

            try
            {
                objBLContactBook.Type = EnmEntryType.E;
                objBLContactBook.PreSave(objDTOCNT01);

                Response objResponse = objBLContactBook.Validation();
                if (objResponse.IsError)
                {
                    _logger.Warn($"Validation failed: {objResponse.Message}");
                    return Ok(objResponse.Message);
                }

                objResponse = objBLContactBook.Save();
                _logger.Info("Contact updated successfully.");
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating contact.");
                objResponse.IsError = true;
                objResponse.Message = "An error occurred while updating the contact.";
                return Ok(objResponse);
            }
        }

        /// <summary>
        /// Inserts a new contact into the database.
        /// </summary>
        /// <param name="objDTOCNT01">The contact DTO object.</param>
        /// <returns>A response object with the result of the insert operation.</returns>
        [HttpPost]
        [Route("InsertContacts")]
        public IActionResult InsertContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            _logger.Info("InsertContacts method called.");
            _logger.Debug($"Inserting contact: {objDTOCNT01}");

            try
            {
                objBLContactBook.Type = EnmEntryType.A;
                objBLContactBook.PreSave(objDTOCNT01);

                Response objResponse = objBLContactBook.Validation();
                if (objResponse.IsError)
                {
                    _logger.Warn($"Validation failed: {objResponse.Message}");
                    return Ok(objResponse.Message);
                }

                objResponse = objBLContactBook.Save();
                _logger.Info("New contact inserted successfully.");
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error inserting contact.");
                objResponse.IsError = true;
                objResponse.Message = "An error occurred while inserting the contact.";
                return Ok(objResponse);
            }
        }
    }
}
