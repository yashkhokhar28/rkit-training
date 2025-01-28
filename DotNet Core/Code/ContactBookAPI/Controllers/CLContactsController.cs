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
    [Route("api/[controller]")]
    [ApiController]
    public class CLContactsController : ControllerBase
    {
        public BLContactBook objBLContactBook;
        private readonly Response _objResponse;
        private readonly BLConverter _objBLConverter;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CLContactsController(
            Response objResponse,
            BLConverter objBLConverter)
        {
            objBLContactBook = new BLContactBook();
            _objResponse = objResponse;
            _objBLConverter = objBLConverter;
        }

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