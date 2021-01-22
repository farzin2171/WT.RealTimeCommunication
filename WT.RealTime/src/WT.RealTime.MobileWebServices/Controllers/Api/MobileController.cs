using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WT.RealTime.Domain.Enums;
using WT.RealTime.Domain.Models;
using WT.RealTime.MobileWebServices.Infrastructure;
using WT.RealTime.MobileWebServices.Models.InputModels;
using System.Text.Json;

namespace WT.RealTime.MobileWebServices.Controllers.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MobileController : ControllerBase
    {
        private readonly ILogger<MobileController> _logger;

        public MobileController(ILogger<MobileController> logger)
        {
            _logger = logger;
        }
       
        //[ProducesResponseType(typeof(ApplicationListQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<IActionResult> Post(LocationModel location)
        {
            var (messageBrokerPublisher, messageBrokerSubscriber) = MessageBrokerFactory.Create(MessageBrokerType.RabbitMq);
            try
            {
                var message = JsonSerializer.Serialize(location);
                var body = Encoding.UTF8.GetBytes(message);
                await messageBrokerPublisher.Publish(new Message(body, Guid.NewGuid().ToString("N"), "application/json", "My MessageBroker", "corr_" + Guid.NewGuid().ToString("N")));

            }
            finally
            {
                messageBrokerPublisher.Dispose();
                messageBrokerSubscriber.Dispose();
            }
            _logger.LogInformation("this is message");
                return Ok();
        }
    }
}
