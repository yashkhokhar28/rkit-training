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
        public BLContactBook objBLContactBook;

        /// <summary>
        /// Response object to hold the result data.
        /// </summary>
        private readonly Response _objResponse;

        /// <summary>
        /// Converter object for data transformation.
        /// </summary>
        private readonly BLConverter _objBLConverter;

        /// <summary>
        /// Logger for logging information, warnings, and errors.
        /// </summary>
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="CLContactsController"/> class.
        /// </summary>
        /// <param name="objResponse">The response object.</param>
        /// <param name="objBLConverter">The converter object.</param>
        public CLContactsController(
            Response objResponse,
            BLConverter objBLConverter)
        {
            objBLContactBook = new BLContactBook();
            _objResponse = objResponse;
            _objBLConverter = objBLConverter;
        }

        /// <summary>
        /// Retrieves all contacts from the database.
        /// </summary>
        /// <returns>A response object containing the list of contacts.</returns>
        [HttpGet]
        [Route("GetAllContacts")]
        public IActionResult Get()
        {
            _logger.Info("CLContactsController: Get method called");
            List<CNT01> lstContacts = objBLContactBook.GetAllContacts();
            if (lstContacts.Count > 0)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Ok";
                _objResponse.Data = _objBLConverter.ToDataTable(lstContacts);
            }
            else
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Not Found";
                _objResponse.Data = null;
            }
            return Ok(_objResponse);
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
            CNT01 objCNT01 = objBLContactBook.GetUserByID(ID);
            if (objCNT01 != null)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Ok";
                _objResponse.Data = _objBLConverter.ObjectToDataTable(objCNT01);
            }
            else
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Not Found";
                _objResponse.Data = null;
            }
            return Ok(_objResponse);
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
            int result = objBLContactBook.Delete(ID);
            if (result > 0)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Data Deleted Successfully";
            }
            else
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error Occurred in Delete";
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates the contact details in the database.
        /// </summary>
        /// <param name="objDTOCNT01">The contact DTO object.</param>
        /// <returns>A response object with the result of the update operation.</returns>
        [HttpPut]
        [Route("UpdateContacts")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public IActionResult UpdateContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            objBLContactBook.Type = EnmEntryType.E;
            objBLContactBook.PreSave(objDTOCNT01);

            Response objResponse = objBLContactBook.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);
            }

            objResponse = objBLContactBook.Save();
            return Ok(objResponse);
        }

        /// <summary>
        /// Inserts a new contact into the database.
        /// </summary>
        /// <param name="objDTOCNT01">The contact DTO object.</param>
        /// <returns>A response object with the result of the insert operation.</returns>
        [HttpPost]
        [Route("InsertContacts")]
        [ServiceFilter(typeof(CustomValidationFilter))]
        public IActionResult InsertContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            objBLContactBook.Type = EnmEntryType.A;
            objBLContactBook.PreSave(objDTOCNT01);

            Response objResponse = objBLContactBook.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);
            }
            objResponse = objBLContactBook.Save();
            return Ok(objResponse);
        }
    }
}