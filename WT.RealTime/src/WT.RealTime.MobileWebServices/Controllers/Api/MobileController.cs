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
        /// <summary>
        /// Search applications
        /// </summary>
        /// <param name="sort">Specify which column will be used for sort, 
        ///                    by default the column will be sorted in Ascending order.
        ///                    add - as a prefix to do a Descending sort.</param>
        /// <param name="limit">The number of maximum messages to return.</param>                   
        /// <param name="offset">Skip a number of entries, to handle paging.</param>
        [HttpGet]
        //[ProducesResponseType(typeof(ApplicationListQueryResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] string sort = "-createdOn", [FromQuery] int limit = 10, int offset = 0)
        {
            var (messageBrokerPublisher, messageBrokerSubscriber) = MessageBrokerFactory.Create(MessageBrokerType.RabbitMq);
            try
            {
                var body = Encoding.UTF8.GetBytes("This is the message body");
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
