using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BAL;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreBAL _StoreBAL;
        public StoreController(IStoreBAL StoreBAL)
        {
            _StoreBAL = StoreBAL;
        }

        [HttpPost("GetStoreALL")]
        public IActionResult GetStoreALL()
        {
            try
            {
                var StoreList = _StoreBAL.GetAll(x => x.ActiveFlag == true).ToList();
                if (StoreList != null)
                {
                    return Ok(new ResponseModel { Message = Messsage.Successfully, Status = APIStatus.Successful, Data = StoreList });
                }
                return BadRequest("Posted invalid data.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}