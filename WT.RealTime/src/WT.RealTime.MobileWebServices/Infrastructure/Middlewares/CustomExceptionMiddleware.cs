using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WT.RealTime.Domain.Exceptions;
using WT.RealTime.MobileWebServices.Infrastructure.Http;

namespace WT.RealTime.MobileWebServices.Infrastructure.Middlewares
{
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly ILogger<CustomExceptionMiddleware> _logger;

		private const string InternalServerErrorMessage = "An unexpected error occurred!";
		private const string InternalServerErrorDetail = "Please contact the Equisoft product team.";

		public CustomExceptionMiddleware(
			RequestDelegate next,
			IWebHostEnvironment hostingEnvironment,
			ILogger<CustomExceptionMiddleware> logger)
		{
			_next = next ?? throw new ArgumentNullException(nameof(next));
			_hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (EntityNotFoundException ex)
			{
				_logger.LogWarning(ex, ex.Message);
				await httpContext.Response.WriteNotFoundResponseAsync(ex.Message, ex.Detail);
			}
			catch (CustomException ex)
			{
				_logger.LogWarning(ex, ex.Message);
				await httpContext.Response.WriteBadRequestResponseAsync(ex.Message, ex.Detail);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				await httpContext.Response.WriteInternalServerErrorResponseAsync(InternalServerErrorMessage,
					_hostingEnvironment.IsProduction() ? InternalServerErrorDetail : ex.Message);
			}
		}

	}
}
