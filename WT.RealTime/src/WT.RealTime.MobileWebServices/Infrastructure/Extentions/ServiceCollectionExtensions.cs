using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WT.RealTime.MobileWebServices.Infrastructure.ApiVersion;

namespace WT.RealTime.MobileWebServices.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Jwt", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Jwt"
                             }
                         },
                         new string[] { }
                    }
                });

                c.SchemaFilter<EnumSchemaFilter>();

                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc(description.GroupName, ApiVersionExtensions.CreateInfoForApiVersion(description));
                }
                c.IncludeXmlComments(ApiVersionExtensions.XmlCommentsFilePath);
            });
            return services;
        }

        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddApiVersioning(x => x.ReportApiVersions = true);

            return services;
        }
    }
}
