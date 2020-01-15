using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpRequestDurations;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreWebHttpRequestDurations
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UsePrometheusServer(q => q.UseDefaultCollectors = false);
            app.UsePrometheusRequestDurations(q =>
            {
                q.IncludePath = true;
                q.IncludeMethod = true;
                q.IgnoreRoutesConcrete = new[]
                {
                    "/favicon.ico",
                    "/robots.txt",
                    "/"
                };
                q.IgnoreRoutesStartWith = new[]
                {
                    "/swagger"
                };
                q.CustomNormalizePath = new Dictionary<Regex, string>
                {
                    { new Regex(@"\/[0-9]{1,}(?![a-z])"), "/id" }
                };

                // Just for example. Not for Production
                q.CustomLabels = new Dictionary<string, Func<string>>
                {
                    {
                        "application_name", () =>  env.ApplicationName
                    },
                    {
                        "date", () => DateTime.UtcNow.ToString("yyyy-MM-dd")
                    }
                };
            });

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseMvc();
        }
    }
}
