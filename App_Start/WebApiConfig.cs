using Autofac;
using Autofac.Integration.WebApi;
using DiceGamingSystem.Exceptions;
using DiceGamingSystem.Filters;
using DiceGamingSystem.Handlers;
using DiceGamingSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace DiceGamingSystem
{
    public static class WebApiConfig
    {
        public static void Register ( HttpConfiguration config )
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi" ,
                routeTemplate: "api/{controller}/{id}" ,
                defaults: new { id = RouteParameter.Optional }
            );
            config.MessageHandlers.Add(new MessageHandler());
            config.Filters.Add(new UsersAuthorizationFilterAttribute());
            config.Filters.Add(new NotFoundExceptionFilterAttribute());
            config.Filters.Add(new BadRequestExceptionFilterAttribute());
            config.Filters.Add(new DefaultExceptionFilterAttribute());

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterType<UsersRepository>().AsSelf().SingleInstance();
            builder.RegisterType<SessionsRepository>().AsSelf().SingleInstance();

            var container = builder.Build();

            var repo = container.Resolve<UsersRepository>();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.EnsureInitialized();
        }
    }
}
