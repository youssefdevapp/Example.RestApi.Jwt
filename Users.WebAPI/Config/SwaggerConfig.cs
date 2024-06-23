using Swashbuckle.Application;
using System.Web.Http;

namespace Users.WebApi.Config
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register configuration
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config
            .EnableSwagger(c =>
            {
                c.ApiKey("apiKey")
                    .Description("API Key for accessing secure APIs")
                    .Name("Api-Key")
                    .In("header");
            })
            .EnableSwaggerUi(c =>
            {
                c.EnableApiKeySupport("Api-Key", "header");
            });
        }
    }
}