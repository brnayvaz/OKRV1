using Microsoft.AspNetCore.Mvc;
using Okr.Services.Identity.Model;
using Okr.Services.Identity.Repository;
using System.Text.Json.Serialization;

namespace Okr.Services.Identity.Controllers
{
    public class UserController: ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        [HttpPost("api/createUser")]
        public IActionResult createUser([FromBody] UserModel user)
        {
            try
            {
                var userModel=_userService.CreateUser(user);
                return new  OkObjectResult(userModel);
            }
            catch (Exception)
            {

                return new OkObjectResult("Hata Oluştu");
            }
        }


        [HttpGet("api/getAllUser")]
        public IActionResult getAllUser()
        {
            try
            {
                var userModel = _userService.GetAllUser();
                return new OkObjectResult(userModel);
            }
            catch (Exception)
            {

                return new OkObjectResult("Hata Oluştu");
            }
        }

        [HttpGet("api/userHomePage")]
        public IActionResult userHomePage()
        {
            try
            {
                
                return new OkObjectResult("Test User");
            }
            catch (Exception)
            {

                return new OkObjectResult("Hata Oluştu");
            }
        }

        [HttpPost("api/login")]
        public async Task<object> userLogin([FromBody] UserModel userModel)
        {
            try
            {
                string tokenString = _userService.GetTokenLoginUser(userModel);

                return tokenString;
            }
            catch (Exception)
            {

                return new OkObjectResult("Hata Oluştu");
            }
        }

    }
}
