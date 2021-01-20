using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WT.RealTime.MobileWebServices.Controllers.Api
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MobileController : ControllerBase
    {
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
            throw new Exception("What is this");
            return Ok();
        }
    }
}
