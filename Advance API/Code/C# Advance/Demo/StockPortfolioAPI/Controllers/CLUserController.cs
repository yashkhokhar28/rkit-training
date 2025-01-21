using StockPortfolioAPI.BL;
using StockPortfolioAPI.Models;
using StockPortfolioAPI.Models.DTO;
using StockPortfolioAPI.Models.ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockPortfolioAPI.Controllers
{

    public class CLUserController : ApiController
    {
        public BLUser objBLUser;

        public Response objResponse;

        public CLUserController()
        {
            objBLUser = new BLUser();
            objResponse = new Response();
        }

        [HttpPost]
        [Route("api/user/register")]
        public IHttpActionResult Register([FromBody] DTOUSR01 objDTOUSR01)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            objBLUser.Type = EnmEntryType.A;
            objBLUser.PreSave(objDTOUSR01);
            Response objResponse = objBLUser.Validation();
            if (objResponse.IsError)
            {
                objResponse.IsError = true;
                objResponse.Message = "Error Occurred";
                return Ok(objResponse);
            }
            else
            {
                objBLUser.Save();
                objResponse.IsError = false;
                objResponse.Message = "Data Added Successfully";
                return Ok(objResponse);
            }
        }

        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login([FromBody] DTOUSR02 objDTOUSR02)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Response objResponse = objBLUser.Login(objDTOUSR02);
            return Ok(objResponse);

        }
    }
}