using ContactBookAPI.BL;
using ContactBookAPI.Models;
using ContactBookAPI.Models.DTO;
using ContactBookAPI.Models.ENUM;
using ContactBookAPI.Models.POCO;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLContactsController : ControllerBase
    {
        public BLContactBook objBLContactBook;

        public Response objResponse;

        public BLConverter objBLConverter;

        public CLContactsController()
        {
            objBLContactBook = new BLContactBook();
            objResponse = new Response();
            objBLConverter = new BLConverter();
        }

        [HttpGet]
        [Route("GetAllContacts")]
        public IActionResult Get()
        {
            List<CNT01> lstContacts = objBLContactBook.GetAllContacts();
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
            return Ok(objResponse);
        }

        [HttpGet]
        [Route("GetContactsByID")]
        public IActionResult GetByID(int ID)
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
            return Ok(objResponse);
        }

        [HttpDelete]
        [Route("DeleteContactsByID")]
        public IActionResult Delete(int ID)
        {
            int result = objBLContactBook.Delete(ID);
            if (result > 0)
            {
                objResponse.IsError = false;
                objResponse.Message = "Data Deleted Successfully";
            }
            else
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occure in Delete";
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates the current user's information.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object with updated information.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpPut]
        [Route("UpdateContacts")]
        public IActionResult UpdateContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            objBLContactBook.Type = EnmEntryType.E;
            objBLContactBook.PreSave(objDTOCNT01);

            Response objResponse = objBLContactBook.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);
            }

            objResponse = objBLContactBook.Save();  // Update user using BL
            return Ok(objResponse);
        }


        /// <summary>
        /// Updates the current user's information.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object with updated information.</param>
        /// <returns>The response indicating success or failure.</returns>
        [HttpPost]
        [Route("InsertContacts")]
        public IActionResult InsertContacts([FromBody] DTOCNT01 objDTOCNT01)
        {
            objBLContactBook.Type = EnmEntryType.A;
            objBLContactBook.PreSave(objDTOCNT01);

            Response objResponse = objBLContactBook.Validation();
            if (objResponse.IsError)
            {
                return Ok(objResponse.Message);
            }
            objResponse = objBLContactBook.Save();  // Update user using BL
            return Ok(objResponse);
        }
    }
}
