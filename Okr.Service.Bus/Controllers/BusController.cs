using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okr.Service.Bus.Model;
using Okr.Service.Bus.Repository;

namespace Okr.Service.Bus.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BusController:ControllerBase
    {
        private readonly IOkrServiceBus _okrServiceBus;

        public BusController(IOkrServiceBus okrServiceBus)
        {
            _okrServiceBus = okrServiceBus;

        }

        [Authorize(Roles = "Admin,Write")]
        [HttpPost("api/publishMessage")]
        public IActionResult sendMessage([FromBody] UserBusModel model)
        {
            var result=_okrServiceBus.SendMessage(model);

            return new OkObjectResult(result);
        }

        [Authorize(Roles = "Admin,Write,Read")]
        [HttpPost("api/consumer")]
        public IActionResult consumerMessage([FromBody] string queueName)
        {
            var result = _okrServiceBus.Consumer(queueName);

            return new OkObjectResult(result);
        }

    }
}
