using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Web.Mvc;
using Users.Application;
using Users.Application.Interfaces;
using Users.Infrastucture;
using Users.Infrastucture.Interfaces;
using Users.WebApi.Common;
using Users.WebApi.Handlers;

namespace Users.WebApi.Config
{
    public class IoCConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddSingleton<TokenCreationHandler>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}