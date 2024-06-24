namespace Users.WebApi
{
    using Asp.Versioning.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using System;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using System.Web.Mvc;
    using Users.WebApi.Common;
    using Users.WebApi.Config;

    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public IConfiguration Config { get; }

        public void Configuration(IAppBuilder builder)
        {
            var services = new ServiceCollection();
            IoCConfig.ConfigureServices(services);

            services.AddSingleton<IConfiguration>(provider => Config);

            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());

            // Set MVC Resolver
            DependencyResolver.SetResolver(resolver);

            // we only need to change the default constraint resolver for services
            // that want urls with versioning like: ~/v{apiVersion}/{controller}
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) },
            };

            var configuration = new HttpConfiguration();
            var httpServer = new HttpServer(configuration);

            // Web API configuration and services
            configuration.SuppressDefaultHostAuthentication();
            configuration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            configuration.DependencyResolver = resolver;

            AuthConfig.Configuration(builder);

            // reporting api versions will return the headers
            // "api-supported-versions" and "api-deprecated-versions"
            configuration.AddApiVersioning(options => options.ReportApiVersions = true);
            configuration.MapHttpAttributeRoutes(constraintResolver);

            // This is the call to our swashbuckle config that needs to be called
            SwaggerConfig.Register(configuration);

            // WebApi Route
            configuration.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
           );

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