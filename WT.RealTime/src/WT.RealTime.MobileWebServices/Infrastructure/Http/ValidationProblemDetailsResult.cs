﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WT.RealTime.MobileWebServices.Infrastructure.Http
{
    public class ValidationProblemDetailsResult : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext actionContext)
        {
            await actionContext.HttpContext.Response.WriteBadRequestResponseAsync(actionContext.ModelState);
        }
    }
}
