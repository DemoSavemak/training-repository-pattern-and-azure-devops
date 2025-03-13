using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BAL;
using WebAPI.Model;
using WebAPI.Repository;

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
        [HttpPost("GetALL")]
        public IActionResult GetLogALL()
        {
            try
            {
                var ret = _StoreBAL.GetAll(x => x.ActiveFlag == true);
                var LogList = ret.OrderByDescending(x => x.ID).ToList();
                if (ret != null)
                {
                    return Ok(new ResponseModel { Message = Messsage.Successfully, Status = APIStatus.Successful, Data = LogList });

                }
                return BadRequest("Posted invalid data.");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //[HttpPost("AddLog")]
        //public async Task<IActionResult> AddLog([FromBody] LogRequest request)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string ipv4Address = String.Empty;

        //            foreach (IPAddress currentIPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
        //            {
        //                if (currentIPAddress.AddressFamily.ToString() == System.Net.Sockets.AddressFamily.InterNetwork.ToString())
        //                {
        //                    ipv4Address = currentIPAddress.ToString();
        //                    break;
        //                }
        //            }
        //            var obj = _mapper.Map<Log>(request);
        //            obj.IP = ipv4Address;
        //            obj.ActiveFlag = true;
        //            var Errorlog = await _LogBAL.Create(obj);
        //            if (Errorlog)
        //            {
        //                return Ok(new ResponseModel { Message = Messsage.c_AddedSuccess, Status = APIStatus.Successful });
        //            }
        //            return Ok(new ResponseModel { Message = Messsage.c_SystemError, Status = APIStatus.Error });

        //        }
        //        return Ok(new ResponseModel { Message = Messsage.ErrorWhileFetchingData, Status = APIStatus.SystemError });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}