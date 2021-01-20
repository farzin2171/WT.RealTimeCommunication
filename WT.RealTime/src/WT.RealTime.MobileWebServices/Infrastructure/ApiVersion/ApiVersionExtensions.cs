using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace WT.RealTime.MobileWebServices.Infrastructure.ApiVersion
{
    public class ApiVersionExtensions
    {
		public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new OpenApiInfo
			{
				Title = $"WebTech /apply API {description.ApiVersion}",
				Version = description.ApiVersion.ToString()
			};

			if (description.IsDeprecated)
			{
				info.Description += " This API version has been deprecated.";
			}

			return info;
		}

		public static string XmlCommentsFilePath
		{
			get
			{
				var basePath = System.AppContext.BaseDirectory;
				var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				return Path.Combine(basePath, fileName);
			}
		}
	}
}
