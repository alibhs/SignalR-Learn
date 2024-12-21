using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR_Learn.Business;

namespace SignalR_Learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly MyBusiness _myBusiness;

        public HomeController(MyBusiness myBusiness)
        {
            _myBusiness=myBusiness;
        }

        [HttpGet("message")]
        public async Task<IActionResult> Index(string message)
        {
           await _myBusiness.SendMessageAsyncAction(message);

                return Ok();
        }
    }
}
