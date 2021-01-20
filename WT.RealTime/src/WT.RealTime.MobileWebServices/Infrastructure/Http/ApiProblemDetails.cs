﻿using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WT.RealTime.MobileWebServices.Infrastructure.Http
{
	public class ApiProblemDetails : ValidationProblemDetails
	{
		public ApiProblemDetails(HttpStatusCode code, string title, string detail = null)
		{
			Type = $"https://httpstatuses.com/{(int)code}";
			Title = title;
			Status = (int)code;
			Detail = detail ?? title;
			Instance = $"urn:WebTech:{MapCodeToCodeMessage(code)}:{Guid.NewGuid()}";
		}

		public ApiProblemDetails(HttpStatusCode code, ModelStateDictionary modelState) : base(modelState)
		{
			Type = $"https://httpstatuses.com/{(int)code}";
			Status = (int)code;
			Instance = $"urn:WebTech:{MapCodeToCodeMessage(code)}:{Guid.NewGuid()}";
		}

		private static string MapCodeToCodeMessage(HttpStatusCode code)
		{
			switch (code)
			{
				case HttpStatusCode.BadRequest:
					return "badrequest";
				case HttpStatusCode.NotFound:
					return "notfound";
				case HttpStatusCode.Unauthorized:
					return "Unauthorized";
				case HttpStatusCode.InternalServerError:
					return "internalservererror";
			}

			throw new NotSupportedException($"HttpStatusCode {code} not supported");
		}
	}
}
