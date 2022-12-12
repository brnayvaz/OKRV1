using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okr.Service.App.Repository;
using System.Linq.Expressions;

namespace Okr.Service.App.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AppController:ControllerBase
    {

        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/adminPage")]
        public IActionResult getAdminPage()
        {

            var claimsName = User.Identity.Name;
            var clientInfo = User.FindFirst("ClientInfo").Value;
            var role = User.Claims.ElementAt(1).Value;
            return new OkObjectResult("UserName = " + claimsName + " Role = " + role + " Result = yetki başarılı");
        }

        [Authorize(Roles = "Read,Admin")]
        [HttpGet("api/readPage")]
        public IActionResult getReadPage()
        {

            var claimsName = User.Identity.Name;
            var clientInfo = User.FindFirst("ClientInfo").Value;
            var role = User.Claims.ElementAt(1).Value;
            return new OkObjectResult("UserName = " + claimsName + " Role = " + role + " Result = yetki başarılı");
        }

        [Authorize(Roles = "Write,Admin")]
        [HttpGet("api/writePage")]
        public IActionResult getWritePage()
        {

            var claimsName = User.Identity.Name;
            var clientInfo = User.FindFirst("ClientInfo").Value;
            var role = User.Claims.ElementAt(1).Value;
            return new OkObjectResult("UserName = " + claimsName + " Role = " + role + " Result = yetki başarılı");
        }

        [AllowAnonymous]
        [HttpGet("api/freePage")]
        public IActionResult freePage()
        {
            var claimsName = User?.Identity?.Name;
            var clientInfo = User?.FindFirst("ClientInfo")?.Value;
            return new OkObjectResult("UserName = " + claimsName + " Result = yetki başarılı");
        }

        [Authorize(Roles = "Write,Admin")]
        [HttpPost("api/parallelTask")]
        public async Task<object> parallelTask([FromBody] int numberService)
        {
            switch (numberService)
            {
                case 1:
                    _appService.Service1();
                    break;
                case 2:
                    _appService.Service2();
                    break;
                case 3:
                    _appService.Service3();
                    break;
                default:
                    // code block
                    break;
            }

            return new OkObjectResult("");
        }

    }
}
