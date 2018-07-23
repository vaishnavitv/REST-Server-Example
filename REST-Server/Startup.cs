using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NJsonSchema;
using NSwag.AspNetCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace REST_Server
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            // Use Swagger to wrap APIs and Document Them
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
            // Use our own Exception Handler to wrap error messages as JSON.
            app.UseExceptionHandler().WithConventions(
                (context) => {
                    context.MessageFormatter(
                        (exception, content) =>
                            JsonConvert.SerializeObject(new { Type = exception.GetType().ToString(), Message = exception.Message, StackTrace = env.IsDevelopment()? exception.StackTrace: "" })
                    );
                    context.ContentType = "application/json";
                    context.ForException<Exception>().ReturnStatusCode((int)HttpStatusCode.InternalServerError);
                }
           );
           app.UseMvc();
        }
    }
}
