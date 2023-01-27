using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MockBank.WebApi.Helpers
{
    public class ConfigureSwaggerOptions: IConfigureOptions<SwaggerGenOptions>
    {
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "MockBank.WebApi",
            Version = description.ApiVersion.ToString(),
            Description = "Web Api for Third party Mock Bank.",
            Contact = new OpenApiContact
            {
                Name = "IT Department",
                Email = "developer@mock.xyz",
                Url = new Uri("https://mock.xyz/support")
            }
        };

        if (description.IsDeprecated)
            info.Description += " <strong>This API version of mock api has been deprecated.</strong>";

        return info;
    }
    }
}