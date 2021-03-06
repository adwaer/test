﻿using System;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using backend.WebApi.config;
using backend.WebApi.Middlewares;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace backend.WebApi.config
{
    public sealed class Startup
    {
        private static HttpConfiguration HttpConfiguration { get; set; }
        //public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration = new HttpConfiguration();
            var container = IocConfig.Configure(HttpConfiguration);

            ConfigureApp(container, app);
            app.Run(Run);
        }

        private Task Run(IOwinContext context)
        {
            context.Response.ContentType = "text/html";
            return context.Response.WriteAsync("Service online");
        }

        private void ConfigureApp(IContainer container, IAppBuilder app)
        {
            HttpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            HttpConfiguration.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            app.UseAutofacMiddleware(container)
                .UseAutofacWebApi(HttpConfiguration)
                .UseCors(CorsOptions.AllowAll)
                .Use<AuthMiddleware>()
                .UseWebApi(HttpConfiguration);

            HttpConfiguration.MapHttpAttributeRoutes();
            SwaggerConfig.Register(HttpConfiguration);
            RouteConfig.Register(HttpConfiguration.Routes);
        }

    }
}