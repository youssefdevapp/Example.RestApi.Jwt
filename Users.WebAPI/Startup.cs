namespace Users.WebApi
{
    using Asp.Versioning.Routing;
    using Owin;
    using System;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using Users.WebApi.Config;

    public partial class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            // we only need to change the default constraint resolver for services
            // that want urls with versioning like: ~/v{apiVersion}/{controller}
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) },
            };
            var configuration = new HttpConfiguration();
            var httpServer = new HttpServer(configuration);

            // reporting api versions will return the headers
            // "api-supported-versions" and "api-deprecated-versions"
            configuration.AddApiVersioning(options => options.ReportApiVersions = true);
            configuration.MapHttpAttributeRoutes(constraintResolver);

            // This is the call to our swashbuckle config that needs to be called
            SwaggerConfig.Register(configuration);

            builder.UseWebApi(httpServer);
        }

        public static string ContentRootPath
        {
            get
            {
                var app = AppDomain.CurrentDomain;

                if (string.IsNullOrEmpty(app.RelativeSearchPath))
                {
                    return app.BaseDirectory;
                }

                return app.RelativeSearchPath;
            }
        }
    }
}